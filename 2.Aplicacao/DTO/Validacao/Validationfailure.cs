namespace _2.Aplicacao.DTO.Validacao
{
    public class Validationfailure
    {

        public Validationfailure(string prop , string message)
        {
            PropertyName = prop;
            ErrorMessage = message;
            
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}