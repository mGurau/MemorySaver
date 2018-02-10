using MemorySaver.Domain.ServiceContracts.Interfaces;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;
using MemorySaver.Domain.Interfaces.RepositoriesInterfaces;
using AutoMapper;
using EnsureThat;
using MemorySaver.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MemorySaver.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            Ensure.That(userRepository).IsNotNull();
            Ensure.That(mapper).IsNotNull();

            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public bool CreateUser(CreateUserRequestDTO newUser)
        {
            User userToBeCreated = new User
            {
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Password = newUser.Password
            };

            userRepository.Add(userToBeCreated);
            return userRepository.SaveChages();
        }

        public IEnumerable<Chest> GetUserChests(Guid userId)
        {
            var result = userRepository.GetUserChests(userId);

            return result;
        }

        public LoginUserResponseDTO VerifySocialUser(LoginSocialUserRequestDTO socialUser)
        {
            var user = userRepository.GetByEmail(socialUser.Email);

            if (user != null)
            {
                LoginUserResponseDTO loggedInUser = new LoginUserResponseDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return loggedInUser;
            }
            else
            {
                User userToBeCreated = new User
                {
                    Email = socialUser.Email,
                    FirstName = socialUser.FirstName,
                    LastName = socialUser.LastName,
                    Password = "password"
                };
                userRepository.Add(userToBeCreated);
                userRepository.SaveChages();

                LoginUserResponseDTO loggedInUser = new LoginUserResponseDTO
                {
                    Id = userToBeCreated.Id,
                    FirstName = userToBeCreated.FirstName,
                    LastName = userToBeCreated.LastName,
                    Email = userToBeCreated.Email
                };
                return loggedInUser;
            }
        }

        public LoginUserResponseDTO Login(LoginUserRequestDTO loginCredentials)
        {
            var user = userRepository.GetByEmail(loginCredentials.Email);
            if (user != null && user.Password == loginCredentials.Password)
            {
                LoginUserResponseDTO loggedInUser = new LoginUserResponseDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                };
                return loggedInUser;
            }

            return null;
        }
    }
}
