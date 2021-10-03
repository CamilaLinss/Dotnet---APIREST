using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using _3.Dominio.Entidades.Validations;
using FluentValidation;
using FluentValidation.Results;

namespace _3.Dominio.Entidades.Abstract
{

    
    public abstract class Entity<T> : AbstractValidator<T>
    {

        //Criando uma entidade base para usar nos models, assim não precisando criar uma classe a 
        //parte de cada entidade para o fluent validation

        //Herda a classe principal do FluentValidation

        //Cria campo que recebe os resultados das validações
        //[NotMapped]   //OBS: Quando é usado o EF, pode ser que ele entenda que esse é um campo do banco de dados não mapeado, para isso preciso adicionar essa anotação // Nesse caso não precisei da anotação pois deixei explicito no OnModelBuild
        public ValidationResult validationResult { get; set; }
  

        protected Entity()
        {
            //Instancia sempre é criada ao chamar a entidade filha
            validationResult = new ValidationResult();

        }


        //Adiciona uma falha no validationResult
        public void AddError(string propertyName, string errorMessage){

            validationResult.Errors.Add(new ValidationFailure(propertyName, errorMessage));
        }

        //Sobrecarga
        public void AddError(string errorMessage){

            validationResult.Errors.Add(new ValidationFailure("", errorMessage));

        }


        
    }
}