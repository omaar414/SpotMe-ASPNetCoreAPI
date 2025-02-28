using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotMeAPI.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public User? Sender { get; set; }
        public int ReceiverId { get; set; }
        public User? Receiver { get; set; }

        public string status { get; set; } = "Pending"; // "Accepted", "Rejected", "Pending"
        public DateTime? DateSent { get; set; } = DateTime.UtcNow;
    }
}