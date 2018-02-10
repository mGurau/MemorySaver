using System;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Response
{
    public class LoginUserResponseDTO
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
