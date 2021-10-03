using System.Collections.Generic;
using System.Linq;
using Dominio.Entidades;
using Dominio.RepoInterfaces;
using Dominio.Services.Interface;
using FluentValidation.Results;

namespace _3.Dominio.Entidades.Validations.Services
{
    public class ClienteService:IClienteService
    {

        private readonly IClienteRepositorio _repo;


        public ClienteService( IClienteRepositorio repo)
        {
            _repo = repo;
        }


        public ValidationResult cadastra(Cliente cliente){

            //Valida o corpo novo que entrou, como estou usando uma abstract class na entidade cliente, então fica assim "cliente.Validate"
            //Validação inicial das rules -se ja tiver erros iniciais, os mesmos já vão ser retornados
            cliente.validationResult = cliente.Validate(cliente);

            if(!cliente.validationResult.IsValid){return cliente.validationResult;}


            //Regras de negocio

            //Trago todos os registros, buscando se entre eles ja existe esse email cadastrado
            IEnumerable<Cliente> emails = _repo.busca().Where(e => e.email == cliente.email);
            //Valido e adiciono
            if(emails.Any()){
                
                //Metodo addError criado na classe abstract entity (com ela tb ja fazemos o DTO das falhas, aparecendo apenas alguns campos)
                cliente.AddError("Cliente", "Esse e-mail já está cadastrado. Recupere a sua senha");

                return cliente.validationResult;
            }


            _repo.cadastra(cliente);

            return cliente.validationResult;

        }

        public IEnumerable<Cliente> busca(){

            var result = _repo.busca();
            
            return result;

        }

        public Cliente buscaId(int id){

            var result = _repo.buscaId(id);

            return result;
        }

        public bool atualiza (int id, Cliente clienteUpdate){

            _repo.atualiza(id, clienteUpdate);

                return true;

        }


        public bool delete(int id){

            //Busca id passado no endpoint pra ver se o registro existe
            var clienteexiste = _repo.buscaId(id);

              if(clienteexiste == null){

                return false;

            }

            _repo.deletar(id);

            return true;

        }

        
    }
}