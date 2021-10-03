using System;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTO.Output
{
    public class DTOOutputCliente
    {


        [Key] //chave do banco
       // [Required]  //Obrigatorio
        public int id {get;set;}

       // [Required(ErrorMessage = "O campo Nome é obrigatorio")]//Opcionalmente podemos mudar sua msg de erro padrão com o error message
        //Limita caracteres
       // [StringLength(40)]
        public string nome {get;set;}

       // [Required(ErrorMessage = "O campo CPF é obrigatorio")]
        public int CPF {get;set;}

        //limita quantidade - de 1 até 600 o numero pode chegar
        //[Range(1,600)]

       // [Required]
        public string email {get;set;}


        //ADICIONAR UM CAMPO A MAIS QUE NÃO TEM NA ENTIDADE ORIGINAL
        public DateTime horaConsulta {get;set;}
        
        
    }
}