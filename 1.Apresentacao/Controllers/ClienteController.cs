using System.Collections.Generic;
using _3.Dominio.Entidades.Validations;
using _5._1.Logger;
using Aplicacao.DTO;
using Aplicacao.DTO.Output;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Apresentacao.Controllers
{

    //https://localhost:5001/swagger/index.html

    [ApiController]
    [Route("[controller]")]
    public class ClienteController:ControllerBase
    {

      private readonly IClienteService _service;
      //private readonly ILogger _logger;
     
      //Injeta o mapper
      private readonly IMapper _mapper;

      Validations validacao = new Validations();


      public ClienteController(IClienteService service, IMapper mapper)//, ILogger logger)
      {
          _service = service;
          _mapper =mapper;
          //_logger = logger;
      }



        [HttpPost("cadastra")]
        //[Route("cadastra")]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)] //Mapeio de forma automatica todos os status que podem retornar, sem precisar ficar tratando com if
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult cadastra([FromBody] DTOInputCliente clienteDTO){

           //_logger.Information("Iniciando cadastro Cliente");

            //Mas meu service espera um Cliente e não DTOCreatedCliente - devemos converter
            //como ? Primeira opção manual

            // 1-           PARA NÃO FAZER DE FORMA MANUAL USAREMOS O IMAPPER(AUTOMAPPER)
            // Cliente cliente = new Cliente(){

               //  nome = clienteDTO.nome,
               //  CPF = clienteDTO.CPF,
               //  email = clienteDTO.email
                
            // };


            //USANDO AUTOMAÇÃO COM O MAPPER
            //Conversão inputDTO para Cliente
            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);

             var resultado = _service.cadastra(cliente);


             if(!resultado.IsValid){

                var falhas = validacao.AddFalhas(resultado);

                return BadRequest(falhas);

             }else{

                //204
                return NoContent();

             }
        }


                //List<Cliente> - porque não ? O ienumerable impede que se em algum momento esse metodo
                //não retornar extamente uma lista de clientes, o mesmo não quebra pois esta declarado
                //que será uma entidade IEnumerable que implementa a classe List tb
        [HttpGet("busca")]
        //[Route("busca")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DTOOutputCliente>> busca(){

            var clientes = _service.busca();


            //CONVERSÃO MANUAL
            //1 - 
         //   List<DTOOutputCliente> dtoClientes;

           // foreach(Cliente cliente in clientes){

           // DTOOutputCliente dtoCliente = new DTOOutputCliente(){

               // nome = cliente.nome,
               // CPF = cliente.CPF,
             //   email = cliente.email,
                //campo extra do dto
               // horaConsulta = DateTime.Now

                //  };

            //   dtoClientes.Add(dtoCliente);
          //  }


            List<DTOOutputCliente> clienteDTO2 = _mapper.Map<List<DTOOutputCliente>>(clientes);


            return Ok(clienteDTO2);

        }


        //Aqui indico que no meu endpoint sera passado um numero de id
        //[HttpGet("{id}")]
        [HttpGet("buscaid/{id}")]
        //[Route("buscaid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DTOOutputCliente> buscaId(int id){

            var cliente = _service.buscaId(id);

             DTOOutputCliente clienteDTO = _mapper.Map<DTOOutputCliente>(cliente);

            return clienteDTO;

        }

        [HttpPut("atualiza/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult atualiza([FromBody] DTOInputCliente clienteDTO, int id){

            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);

            _service.atualiza(id ,cliente);

            return NoContent();

        }
    



   


        
    }
}