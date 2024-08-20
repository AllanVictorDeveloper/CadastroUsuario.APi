namespace SisConFin.Application.DTOs.Response
{
    public class UsersResponse
    {
        public string? Name { get; set; }
        public string? Profile { get; set; }

        public UsersResponse(string name,string profile)
        {
            Name = name;
            Profile = profile;
        }
    }
}