using System.Collections.Generic;
using System.Linq;
using Dominio.Entidades;
using Dominio.RepoInterfaces;
using Repositorio.Data;

namespace Repositorio.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {

        private readonly DataContext _context;

        public ClienteRepositorio(DataContext context)
        {
            _context = context;
        }


        //cadastra
        public void cadastra(Cliente cliente){

            _context.Clientes.Add(cliente);

            _context.SaveChanges();

        }


        //busca tudo
        public IEnumerable<Cliente> busca(){

            //Para utilizar ienumerable não uso o fynd async mas apenas chamando a entidade ja trago os resultados
           var result = _context.Clientes;

            return result;
        }


        //busca por id
        public Cliente buscaId(int id){

            Cliente result = _context.Clientes.Find(id);

            return result;

        }


        //Atualizar
        public void atualiza(int id, Cliente clienteAtualizado){

            //_context.Clientes.Update(clienteAtualizado);
            Cliente cliente = _context.Clientes.First(c => c.id == id);

            cliente.CPF = clienteAtualizado.CPF;
            cliente.email = clienteAtualizado.email;
            cliente.nome = clienteAtualizado.nome;

             _context.SaveChanges();

        }


          public void deletar(int id){

            Cliente cliente = buscaId(id);

            _context.Clientes.Remove(cliente);

            _context.SaveChanges();

        }





        
    }
}