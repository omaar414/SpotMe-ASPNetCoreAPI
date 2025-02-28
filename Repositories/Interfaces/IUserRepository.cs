using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;

namespace SpotMeAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);
        Task AddUser(User model);
        Task<bool> UsernameExists(string username);
        bool IsValidPassword(string modelPassword, string userPassword);
    }
}