using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpotMeAPI.DTOs.User.Location;
using SpotMeAPI.Models;
using SpotMeAPI.Repositories.Interfaces;

namespace SpotMeAPI.Controllers
{
    [ApiController]
    [Route("api/map")]
    public class MapController : ControllerBase
    {
        private readonly IMapRepository _mapRepository;
        public MapController(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        [HttpPost("location/update")]
        [Authorize]
        public async Task<IActionResult> UpdateLocation([FromBody]LocationDto model)
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }
            

            var existingLocation = await _mapRepository.GetLocationById(userId);

            if (existingLocation == null)
            {
                var newLocation = new UserLocation {
                    UserId = userId,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    UpdatedAt = DateTime.UtcNow
                };

                await _mapRepository.AddUserLocation(newLocation);
            }
            else
            {
                existingLocation.Latitude = model.Latitude;
                existingLocation.Longitude = model.Longitude;
                existingLocation.UpdatedAt = DateTime.UtcNow;

                await _mapRepository.Save();
            }

            return Ok(new { message = "Location updated successfully" });
        }

        [HttpGet("location/friends")]
        [Authorize]
        public async Task<IActionResult> GetAllFriends()
        {
            var IdFromToken = User.FindFirst("userId");   
            if (IdFromToken == null) return Unauthorized(new { message = "User not authenticated" });

            if (!int.TryParse(IdFromToken.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user ID" });
            }

            var mapFriends = await _mapRepository.GetAllMapFriends(userId);

            return Ok(mapFriends);

        }
        

        
    }

}