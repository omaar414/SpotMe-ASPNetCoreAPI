using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotMeAPI.Data;
using SpotMeAPI.DTOs.Friendship;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;

namespace SpotMeAPI.Controllers
{
    [ApiController]
    [Route("api/friendship")]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendshipRepository _friendshipRepository;
        public FriendshipController(IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
        }

        [HttpPost("send-request")]
        [Authorize]
        public async Task<IActionResult> SendRequest([FromBody] RequestDto request)
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var user = await _friendshipRepository.GetUserById(userId);
            if (user == null) return Unauthorized(new{ message = "User not found" });

            if(user.Username == request.Username) { return BadRequest(new { message = "You can't send a request to yourself" }); }

            var requestedUser = await _friendshipRepository.GetUserByUsername(request.Username);
            if(requestedUser == null) {return NotFound(new{ message = "User not found"});}

            var friendship = await _friendshipRepository.FriendshipExists(user.Id, requestedUser.Id);
            if (friendship) {return BadRequest(new{ message = "Already friends"});}

            var friendRequest = await _friendshipRepository.FriendRequestExists(user.Id, requestedUser.Id);
            if (friendRequest) {return BadRequest(new{ message = "Already sent"});}

            var newFriendRequest = new FriendRequest {
                SenderId = user.Id,
                ReceiverId = requestedUser.Id,
                status = "Pending",
                DateSent = DateTime.UtcNow,
            };

            await _friendshipRepository.AddFriendRequest(newFriendRequest);
            return Ok(new { message = "Friend request sent"});
        }

        [HttpPost("accept-request/{friendRequestId}")]
        [Authorize]
        public async Task<IActionResult> AcceptRequest([FromRoute] int friendRequestId)
        {
            var friendRequest = await _friendshipRepository.GetFriendRequestById(friendRequestId);
            if (friendRequest == null) {return NotFound(new{ message = "Friend request not found"});}

            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var user = await _friendshipRepository.GetUserById(userId);
            if (user == null) return Unauthorized(new{ message = "User not found" });

            if(user.Id != friendRequest.ReceiverId || friendRequest.status != "Pending") {return BadRequest(new{ message = "Invalid request"});}

            var newFriendship = new Friendship {
                UserId = user.Id,
                FriendId = friendRequest.SenderId,
            };

            await _friendshipRepository.AddFriendship(newFriendship);
            await _friendshipRepository.DeleteFriendRequest(friendRequest);

            return Ok(new {message = "Friend request accepted, friendship created"});

        }

        [HttpPost("deny-request/{friendRequestId}")]
        [Authorize]
        public async Task<IActionResult> DenyRequest([FromRoute] int friendRequestId)
        {
            var friendRequest = await _friendshipRepository.GetFriendRequestById(friendRequestId);
            if (friendRequest == null) {return NotFound(new{ message = "Friend request not found"});}

            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var user = await _friendshipRepository.GetUserById(userId);
            if (user == null) return Unauthorized(new{ message = "User not found" });

            if(user.Id != friendRequest.ReceiverId || friendRequest.status != "Pending") {return BadRequest(new{ message = "Invalid request"});}

            await _friendshipRepository.DeleteFriendRequest(friendRequest);

            return Ok(new {message = "Friend request denied"});


        }

        [HttpDelete("delete-friend/{username}")]
        [Authorize]
        public async Task<IActionResult> DeleteFriend([FromRoute]string username)
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var user = await _friendshipRepository.GetUserById(userId);
            if (user == null) return Unauthorized(new{ message = "User not found" });
            if(user.Username == username) { return BadRequest(new { message = "You can't delete yourself" }); }

            var UserToDelete = await _friendshipRepository.GetUserByUsername(username);
            if(UserToDelete == null) {return NotFound(new{ message = "User to delete not found"});}

            var friendship = await _friendshipRepository.GetFriendshipById(user.Id, UserToDelete.Id);
            if(friendship == null) { return BadRequest(new {message = "You can't delete a none friend "});}

            await _friendshipRepository.DeleteFriendship(friendship);

            return Ok(new {message = "Friend deleted"});
        }


        [HttpGet("friend-requests")]
        [Authorize]
        public async Task<IActionResult> GetFriendRequests()
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var friendRequestList = await _friendshipRepository.GetAllFriendRequestsById(userId);

            return Ok(friendRequestList);

        }
        
        [HttpGet("friends")]
        [Authorize]
        public async Task<IActionResult> GetFriends()
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var friendsList = await _friendshipRepository.GetAllFriends(userId);

            return Ok(friendsList);
        }

        [HttpGet("search-user/{username}")]
        [Authorize]
        public async Task<IActionResult> SearchUser([FromRoute] string username)
        {
            if(username == null) {return BadRequest(new { message = "Search request cannot be empty" });}
            
            var usersList = await _friendshipRepository.SearchUsersByUsername(username);
            if(!usersList.Any()) {return NotFound(new { message = "No users found" });}
            
            return Ok(usersList);

        }

       

    }
}