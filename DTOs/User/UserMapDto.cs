using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotMeAPI.Models;

namespace SpotMeAPI.DTOs.User
{
    public class UserMapDto
    {
        public string FirstName { get; set; } = string.Empty;

        
        public string LastName { get; set;} = string.Empty;
        
        public string Username { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }



    }
}