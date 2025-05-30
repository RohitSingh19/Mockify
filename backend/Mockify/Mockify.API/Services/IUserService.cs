﻿using Mockify.API.DTO;
using Mockify.API.Helper;
using Mockify.API.Models.DB;

namespace Mockify.API.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<User> GetUser(string email);
    }
}
