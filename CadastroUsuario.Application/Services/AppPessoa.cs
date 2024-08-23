

using AutoMapper;
using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;
using CadastroUsuario.Application.Interfaces.Services;
using CadastroUsuario.Domain.Entities;
using CadastroUsuario.Domain.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace CadastroUsuario.Application.Services
{
    public class AppPessoa : AppBase<Pessoa>, IAppPessoa
    {
        private readonly IMapper _imapper;
        private readonly IServicePessoa _service;
        private readonly IServiceFoto _serviceFoto;


        public AppPessoa(IMapper imapper, IServicePessoa service, IServiceFoto serviceFoto) : base(service)
        {
            _imapper = imapper;
            _service = service;
            _serviceFoto = serviceFoto;
        }

        public PessoaResponse Atualizar(PessoaCadastroRequest request)
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

        public async Task CadastrarAsync(PessoaCadastroRequest request)
        {
            try
            {
                // Remove pontos e traços
                request.CPF = request.CPF.Replace(".", "").Replace("-", "");

                // Conversão da imagem para blob
                using var memoryStream = new MemoryStream();
                await request.Foto.CopyToAsync(memoryStream);
                var imagemBlob = memoryStream.ToArray();

                // Geração do hash do nome
                var nomeHash = GerarHash(request.Foto.FileName);

                // Criação da entidade Pessoa
                var pessoa = new Pessoa(request.Nome, request.SobreNome, request.CPF, request.DataNascimento, request.Sexo);

                // Criação da entidade Foto
                var foto = new Foto
                {
                    Pessoa = pessoa,
                    Imagem = imagemBlob,
                    NomeHash = nomeHash,
                    Extensao = Path.GetExtension(request.Foto.FileName),
                    Principal = true, // Definindo a primeira foto como principal
                    DataCadastro = DateTime.Now
                };

                // Salvando no banco de dados
                await _service.Inserir(pessoa);
                await _serviceFoto.Inserir(foto);


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

        private string GerarHash(string input)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
