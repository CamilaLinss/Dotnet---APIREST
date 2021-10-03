using System.ComponentModel.DataAnnotations;

namespace Aplicacao.DTO
{
    public class DTOInputCliente
    {
        
       // [Required(ErrorMessage = "O campo Nome é obrigatorio")]//Opcionalmente podemos mudar sua msg de erro padrão com o error message
        //Limita caracteres
        //[StringLength(40)]
        public string nome {get;set;}

       // [Required(ErrorMessage = "O campo CPF é obrigatorio")]
        public int CPF {get;set;}

        //limita quantidade - de 1 até 600 o numero pode chegar
        //[Range(1,600)]

        //[Required]
        public string email {get;set;}

        



    }
}