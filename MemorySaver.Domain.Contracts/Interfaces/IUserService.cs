using System;
using System.Collections.Generic;
using MemorySaver.Domain.Entities;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;

namespace MemorySaver.Domain.ServiceContracts.Interfaces
{
    public interface IUserService
    {
        bool CreateUser(CreateUserRequestDTO newUser);

        LoginUserResponseDTO Login(LoginUserRequestDTO loginUser);

        IEnumerable<Chest> GetUserChests(Guid userId);

        LoginUserResponseDTO VerifySocialUser(LoginSocialUserRequestDTO socialUser);
    }
}
