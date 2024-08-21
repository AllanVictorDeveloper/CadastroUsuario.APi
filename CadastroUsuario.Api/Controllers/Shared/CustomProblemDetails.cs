using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CadastroUsuario.Api.Controllers.Shared
{
    public class CustomProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; private set; }

        public CustomProblemDetails(HttpStatusCode status, string? detail = null, IEnumerable<string>? errors = null) : this()
        {
            Title = status switch
            {
                HttpStatusCode.BadRequest => detail,
                HttpStatusCode.Unauthorized => detail,
                HttpStatusCode.Forbidden => detail,
                HttpStatusCode.InternalServerError => detail,
                _ => "Ocorreu um erro."
            };
            
            Status = (int)status;
            Detail = detail;

            if (errors is not null)
            {
                if (errors.Count() == 1)
                    Detail = errors.First();
                else if (errors.Count() > 1)
                    Detail = "Ocorreram v�rios problemas.";                

                Errors.AddRange(errors);
            }
        }

        public CustomProblemDetails(HttpStatusCode status, HttpRequest request, string? detail = null, IEnumerable<string>? errors = null) : this(status, detail, errors) =>
            Instance = request.Path;

        private CustomProblemDetails() =>
            Errors = new List<string>();
    }
}