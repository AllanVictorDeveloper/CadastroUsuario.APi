

using CadastroUsuario.Application.DTOs.Request;
using CadastroUsuario.Application.DTOs.Response;

namespace CadastroUsuario.Application.Interfaces.Services;

public interface IIdentityService
{
    Task<UsuarioCadastroResponse> CadastrarUsuario(UsuarioCadastroRequest usuarioCadastro);
    Task<UsuarioLoginResponse> Login(UsuarioLoginRequest usuarioLogin);

    Task Logout();
}