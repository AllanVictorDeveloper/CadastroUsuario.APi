

using AutoMapper;
using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Services;

namespace CadastroUsuario.Application.Services
{
    public class AppPessoa : AppBase<Pessoa>, IAppPessoa
    {
        private readonly IMapper _imapper;
        private readonly IServicePessoa _service;


        public AppPessoa(IServicoBase<Pessoa> servico, IMapper imapper, IServicePessoa service) : base(servico)
        {
            _imapper = imapper;
            _service = service;
        }

        public PessoaResponse Atualizar(PessoaRequest request)
        {
            try
            {
                var mapperRequest = _imapper.Map<Pessoa>(request);

                var pessoaSave = _service.Atualizar(mapperRequest);

                return _imapper.Map<PessoaResponse>(pessoaSave);
            }
            catch (Exception ex)
            {
                throw new Exception(message: "Erro ao atualizar, tente novamente.", ex.InnerException);
            }

        }

        public void Cadastrar(PessoaRequest request)
        {
            try
            {
                var mapperRequest = _imapper.Map<Pessoa>(request);

                var pessoaSave = _service.Inserir(mapperRequest);



            }
            catch (Exception ex)
            {
                throw new Exception(message: "Erro ao cadastrar, tente novamente.", ex.InnerException);
            }
        }

        public PessoaResponse RetornarPorId(int Id)
        {
            try
            {
                var pessoaResult = _service.RetornaPorId(Id);

                return _imapper.Map<PessoaResponse>(pessoaResult);
            }
            catch (Exception ex)
            {
                throw new Exception(message: "Erro ao retornar a consulta, tente novamente.", ex.InnerException);
            }
        }

        public ICollection<PessoaResponse> RetornarTodos()
        {
            var listPessoa = _service.ListarTudo();

            return _imapper.Map<ICollection<PessoaResponse>>(listPessoa);
        }
    }
}
