namespace MemorySaver.Domain.ServiceContracts.DTOs.Request
{
    public class LoginUserRequestDTO
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public LoginUserRequestDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
