using System.Collections.Generic;
using Aplicacao.DTO;
using Aplicacao.DTO.Output;
using AutoMapper;
using Dominio.Entidades;

namespace Aplicacao.Profiles
{
    //AUTO MAPPER
    //PASTA CRIADA PARA DEFINIR AS CLASSES DE DTO
    public class ClienteProfile:Profile
    {

        public ClienteProfile()
        {
            //Converte DTOInputCliente para Cliente
            CreateMap<DTOInputCliente, Cliente>();

            //Converte Cliente para DTOOutputCliente
            CreateMap<Cliente, DTOOutputCliente>();

           // CreateMap<IEnumerable<Cliente>, IEnumerable<DTOOutputCliente>>();

            
        }
        
    }
}