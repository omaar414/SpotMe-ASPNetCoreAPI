using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotMeAPI.DTOs.User
{
    public class UserDtoV2
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;

        
        public string LastName { get; set;} = string.Empty;
        
        public string Username { get; set; } = string.Empty;
    }
}