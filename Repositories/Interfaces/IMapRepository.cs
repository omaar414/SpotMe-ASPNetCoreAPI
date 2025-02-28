using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;

namespace SpotMeAPI.Repositories.Interfaces
{
    public interface IMapRepository
    {
        Task<UserLocation?> GetLocationById(int id);
        Task AddUserLocation(UserLocation location);
        Task Save();
        Task<IEnumerable<UserMapDto>?> GetAllMapFriends(int userId);
        
    }
}