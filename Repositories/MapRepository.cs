using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpotMeAPI.Data;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;

namespace SpotMeAPI.Repositories
{
    public class MapRepository : IMapRepository
    {
        private readonly ApplicationDbContext _context;
        public MapRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserLocation?> GetLocationById(int id)
        {
            return await _context.UsersLocations.FirstOrDefaultAsync(location => location.UserId == id);
        }

        public async Task AddUserLocation(UserLocation location)
        {
            _context.UsersLocations.Add(location);
            await _context.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserMapDto>?> GetAllMapFriends(int userId)
        {
            var mapFriends = await _context.Friendships
            .Where(fr => fr.UserId == userId || fr.FriendId == userId)
            .Include(fr => fr.Friend)
            .Include(fr => fr.User)
            .Select(fr => new UserMapDto {
                FirstName = (fr.UserId == userId ? fr.Friend.FirstName : fr.User.FirstName) ?? string.Empty,
                LastName = (fr.UserId == userId ? fr.Friend.LastName : fr.User.LastName) ?? string.Empty,
                Username = (fr.UserId == userId ? fr.Friend.Username : fr.User.Username) ?? string.Empty,

                Latitude = fr.UserId == userId ? (fr.Friend.Location != null ? fr.Friend.Location.Latitude 
                : (double?)null) : (fr.User.Location != null ? fr.User.Location.Latitude : (double?)null), 

                Longitude = fr.UserId == userId ? (fr.Friend.Location != null ? fr.Friend.Location.Longitude 
                : (double?)null) : (fr.User.Location != null ? fr.User.Location.Longitude : (double?)null)
            })
            .Where(dto => dto.Latitude != null && dto.Longitude != null)
            .ToListAsync();
            
            var user = await _context.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Location)
                .Select(u => new UserMapDto {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    Latitude = u.Location != null ? u.Location.Latitude : null,
                    Longitude = u.Location != null ? u.Location.Longitude : null
                }).FirstOrDefaultAsync();

                if(user != null && user.Latitude != null && user.Longitude != null)
                {
                    mapFriends.Add(user);
                }

                return mapFriends;
        }
    }
}