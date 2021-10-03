using _3.Dominio.Entidades.Abstract;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> opt):base (opt)
        {
        //A string de conexão tambem pode ser configurada aqui
        //string config no appsettings.json
        }


        public DbSet<Cliente> Clientes {get;set;}



        //É uma boa pratica mapear os tipos de dados com os tipos que esta no banco dados, para que
        //seja otimizado a qualidade da consulta (mais velocidade) pois quando não é deixado 
        //explicito, o ORM vai atribuir os tipos de forma automatica e causar mais lentidão nas consultas
        //quando os tipos "conflitarem" com os tipos do banco
        protected override void OnModelCreating(ModelBuilder builder)
        {
    
            builder.Entity<Cliente>().ToTable("cliente");

            //Quando usei a classe entity como abstract, acabei trazendo mais campos derivados da 
            //classe mae, portanto especifiquei os campos a serem ignorados no mapeamento (poderia usar a anotação [NotMapped] direto no atributo ignorado)
            builder.Entity<Cliente>().Ignore(c => c.validationResult);
            builder.Entity<Cliente>().Ignore(c => c.CascadeMode);

 
       // builder.Entity<Community>(entity =>
       // {
            //entity.HasKey(c => c.Id);
           // entity.Property(c => c.Name).IsRequired();
           // entity.Property(c => c.FrontId).IsRequired();
            // entity.Property(c => c.Ranks).IsRequired(false);
            //entity.HasMany(c => c.Ranks).WithOne().HasForeignKey(c => c.CommunityId).IsRequired();
       // });
        }


        
    }
}