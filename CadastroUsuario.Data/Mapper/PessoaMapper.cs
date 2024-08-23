using AutoMapper;
using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Domain.Entities;


namespace CadastroUsuario.Infra.Data.Mapper
{
    public class PessoaMapper : Profile
    {

        public PessoaMapper()
        {
            CreateMap<Pessoa, PessoaCadastroRequest>().ReverseMap();
            CreateMap<Pessoa, PessoaResponse>().ReverseMap();

        }
    }
}
