using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotMeAPI.DTOs.Friendship;
using SpotMeAPI.DTOs.User;
using SpotMeAPI.Models;

namespace SpotMeAPI.Repositories.Interfaces
{
    public interface IFriendshipRepository
    {
        Task<User?> GetUserByUsername(string username);
        Task<User?> GetUserById(int id);
        Task AddFriendRequest(FriendRequest friendRequest);
        Task<bool> FriendshipExists(int userId, int friendId);
        Task<bool> FriendRequestExists(int userId, int friendId);
        Task<FriendRequest?> GetFriendRequestById(int friendRequestId);
        Task AddFriendship(Friendship friendship);
        Task DeleteFriendRequest(FriendRequest friendRequest);
        Task<IEnumerable<FriendRequestDto>?> GetAllFriendRequestsById(int userId);
        Task<IEnumerable<UserDtoV2>?> GetAllFriends(int userId);
        Task <Friendship?> GetFriendshipById(int userId, int friendId);
        Task DeleteFriendship(Friendship friendship);
        Task <IEnumerable<SearchUserDto>> SearchUsersByUsername(string username);
    }
}