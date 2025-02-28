using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpotMeAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [MaxLength(25, ErrorMessage = " Name cannot be more than 25 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Required (ErrorMessage = "Last Name is required")]
        [MaxLength(25, ErrorMessage = "Last Name cannot be more than 25 characters")]
        public string LastName { get; set;} = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required (ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserLocation? Location { get; set; }

    }
}