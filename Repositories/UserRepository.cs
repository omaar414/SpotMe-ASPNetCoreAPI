using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpotMeAPI.Data;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;

namespace SpotMeAPI.Repositories
{   
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddUser(User model)
        {
            _context.Users.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public bool IsValidPassword(string modelPassword, string passwordHash)
        {
           return BCrypt.Net.BCrypt.Verify(modelPassword, passwordHash);
        }

        public async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(user => user.Username == username);
        }

        
    }
}