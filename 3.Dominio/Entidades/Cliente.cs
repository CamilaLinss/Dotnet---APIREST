
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using _3.Dominio.Entidades.Abstract;
using FluentValidation;
using FluentValidation.Results;

namespace Dominio.Entidades
{

    [Table("cliente")]
    public class Cliente : Entity<Cliente>
    {


        //Anotações de tratamento de campos por DATAANNOTATIONS DA DOTNET EF (O fluentvalidation é uma alternativa quando o projeto não usa EF, alem de ter tratativas mais completas)
        [Key] //chave do banco
        //[Required]  //Obrigatorio     //COM O FLUENTVALIDATION NÃO PRECISO DESSAS ANOTAÇÕES
        public int id {get;set;}

       // [Required(ErrorMessage = "O campo Nome é obrigatorio")]//Opcionalmente podemos mudar sua msg de erro padrão com o error message
        //Limita caracteres
        //[StringLength(40)]
        public string nome {get;set;}

        //[Required(ErrorMessage = "O campo CPF é obrigatorio")]
        //[StringLength(13)]
        public int CPF {get;set;}

        //limita quantidade - de 1 até 600 o numero pode chegar
        //[Range(1,600)]

        //[Required]
        public string email {get;set;}



        public Cliente()
        {

             RuleFor(x => x.nome)
                .NotNull().WithMessage("O Nome é necessário")
                .NotEmpty().WithMessage("O campo Nome não pode ficar vazio")
                .MaximumLength(10).WithMessage("O campo Nome so aceita até 10 caracteres");

            RuleFor(x => x.email)
                .NotNull().WithMessage("O email é necessário")
                .NotEmpty().WithMessage("O campo email não pode ficar vazio")
                .EmailAddress().WithMessage("Formato de e-mail Inválido");

            //RuleFor(x => x.password)
                //.Must(pass => pass.).... Tratativa muito boa para password

            //RuleForEach (para listas)


        }





    }
}