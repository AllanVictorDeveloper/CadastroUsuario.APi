namespace SisConFin.Application.DTOs.Request
{
    public class UserToken
    {
        public string? Name { get; set; }
        public string? Profile { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
        
    }
}