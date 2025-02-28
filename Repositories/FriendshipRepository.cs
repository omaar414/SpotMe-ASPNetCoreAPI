using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpotMeAPI.Data;
using SpotMeAPI.DTOs.Friendship;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;

namespace SpotMeAPI.Repositories
{
    public class FriendshipRepository : IFriendshipRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task AddFriendRequest(FriendRequest friendRequest)
        {
            _context.FriendRequests.Add(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> FriendshipExists(int userId, int friendId)
        {
            return await _context.Friendships.AnyAsync(f => f.UserId == userId && f.FriendId == friendId || f.UserId == friendId && f.FriendId == userId);
        }

        public async Task<bool> FriendRequestExists(int userId, int friendId)
        {
            return await _context.FriendRequests.AnyAsync(f => f.SenderId == userId && f.ReceiverId == friendId);
        }

        public async Task<FriendRequest?> GetFriendRequestById(int friendRequestId)
        {
            return await _context.FriendRequests.FirstOrDefaultAsync(fr => fr.Id == friendRequestId);
        }

        public async Task AddFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFriendRequest(FriendRequest friendRequest)
        {
            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FriendRequestDto>?> GetAllFriendRequestsById(int userId)
        {
            return await _context.FriendRequests
            .Where(fr => fr.ReceiverId == userId)
            .Include(fr => fr.Sender)
            .Include(fr => fr.Receiver)
            .Select(fr => new FriendRequestDto {
                FriendRequestId = fr.Id,
                SenderId = fr.SenderId,
                SenderUsername = fr.Sender != null ? fr.Sender.Username : "Unknown",
                ReceiverId = fr.ReceiverId,
                ReceiverUsername = fr.Receiver != null ? fr.Receiver.Username : "Unknown",
                DateSent = fr.DateSent
            }).ToListAsync();
        }

        public async Task<IEnumerable<UserDtoV2>?> GetAllFriends(int userId)
        {
            return await _context.Friendships
            .Where(fr => fr.UserId == userId || fr.FriendId == userId) 
            .Select(fr => new UserDtoV2 {
                UserId = (fr.UserId == userId ? fr.Friend.Id : fr.User.Id ),
                FirstName = (fr.UserId == userId ? fr.Friend.FirstName : fr.User.FirstName),
                LastName = (fr.UserId == userId ? fr.Friend.LastName : fr.User.LastName),
                Username = (fr.UserId == userId ? fr.Friend.Username : fr.User.Username),
            })
            .ToListAsync();

        }

        public async Task<Friendship?> GetFriendshipById(int userId, int friendId)
        {
            return await _context.Friendships.FirstOrDefaultAsync(fr => fr.UserId == userId && fr.FriendId == friendId || fr.UserId ==friendId && fr.FriendId == userId);
        }

        public async Task DeleteFriendship(Friendship friendship)
        {
           _context.Friendships.Remove(friendship);
           await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SearchUserDto>> SearchUsersByUsername(string username)
        {
            return await _context.Users
                .Where(u => u.Username.ToLower().Contains(username.ToLower()))
                .Select(u => new SearchUserDto {
                    Username = u.Username
                })
                .ToListAsync();
        }
    }
}