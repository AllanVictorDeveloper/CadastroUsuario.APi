using Microsoft.IdentityModel.Tokens;

namespace CadastroUsuario.Api.Middleware
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }


            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                //await HandleUnauthorizedResponseAsync(context);
            }

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                await HandleForbbidenResponseAsync(context);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            return Task.CompletedTask;
        }



        private Task HandleForbbidenResponseAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

            var result = new
            {
                message = "Você não está autorizado para acessar este recurso.",
                status = 403
            };

            return context.Response.WriteAsJsonAsync(result);
        }


    }
}
