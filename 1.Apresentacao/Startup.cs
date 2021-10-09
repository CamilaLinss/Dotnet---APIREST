
using System.Configuration;
using _3.Dominio.Entidades.Validations.Services;
using _5._1.Logger;
using Aplicacao.Profiles;
using AutoMapper;
using Dominio.RepoInterfaces;
using Dominio.Services.Interface;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repositorio.Data;
using Repositorio.Repositorio;
using Serilog;
using Serilog.Extensions.Logging;

namespace _1.Apresentacao
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)  //IConfiguration configuration,
        {
            //Configuration = configuration;


            //appsettings.json - Pega as configurações do arquivo e tb se baseia no ambiente
            //IWebHostEnvironment env - pega a variavel de ambiente capturada na program a pega do arquivo de launchSettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", false, true)
                .AddEnvironmentVariables();

            //Com essa nova config de ambiente, dou um novo valor para o config, e não preciso mais da injeção de dependencia feita pelo dotnet na mesma
            Configuration = builder.Build();


            //Serilog - Lendo configurações do appsetting.json
            //Log.Logger = new LoggerConfiguration()
               //.ReadFrom.Configuration(Configuration).CreateLogger();

        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Logger.GetLogger();


            //ENTITY FRAMEWORK MYSQL.DATA CONTEXT
            services.AddDbContext<DataContext>(opt => opt.UseMySQL(Configuration.
                    GetConnectionString("connection")));

            //DEPENDENCIAS
            services.AddScoped<DataContext>();

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();


            //AUTOMAPPER PARA OS DTOS (Tutorial muito bom: https://www.youtube.com/watch?v=_ekvCMGuywg)
             var config = new AutoMapper.MapperConfiguration(opt =>
            {
                opt.AddProfile(new ClienteProfile());
               // cfg.AddProfile(new ClienteProfile2());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //FluentValidation
            services.AddControllers()   //Reconhece automatico as classes de validação criadas que usam a abstractValidation
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
           
                    //!! OUTRA OPÇÃO no fluent caso não queira essa config, é registrar a classe validadora uma a uma
                    //segunda opção:    services.AddTransient<IValidator<Cliente>, ClienteValidator(NOME DA CLASSE COM AS RULES)>();



            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "_1.Apresentacao", Version = "v1" });
            });


        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //app.UseSerilogRequestLogging();   Opção para diminuir verbosidade da slogs no serilog

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "_1.Apresentacao v1"));
            }

            //Detalha as exception  - Swagger coloque esse trecho e acesse https://localhost:5001/swagger/v1/swagger.json para mais detalhes da exception
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
