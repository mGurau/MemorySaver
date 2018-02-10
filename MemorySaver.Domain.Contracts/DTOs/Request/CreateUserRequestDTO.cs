namespace MemorySaver.Domain.ServiceContracts.DTOs.Request
{
    public class CreateUserRequestDTO
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
    }
}
