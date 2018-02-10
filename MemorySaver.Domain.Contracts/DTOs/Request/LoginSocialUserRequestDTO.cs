using System;
using System.Collections.Generic;
using System.Text;

namespace MemorySaver.Domain.ServiceContracts.DTOs.Request
{
    public class LoginSocialUserRequestDTO
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public LoginSocialUserRequestDTO(string email, string name)
        {
            string[] names = name.Split(new string[] {" "}, 2, StringSplitOptions.None);

            Email = email;
            FirstName = names[0];
            LastName = names[1];
        }
    }
}
