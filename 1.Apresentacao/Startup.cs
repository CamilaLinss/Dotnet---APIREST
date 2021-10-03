
using _3.Dominio.Entidades.Validations.Services;
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
using Microsoft.OpenApi.Models;
using Repositorio.Data;
using Repositorio.Repositorio;

namespace _1.Apresentacao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

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
