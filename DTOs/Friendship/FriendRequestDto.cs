using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotMeAPI.DTOs.Friendship
{
    public class FriendRequestDto
    {
        public int FriendRequestId { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set;} = string.Empty;
        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; } = string.Empty;
        public DateTime? DateSent { get; set; } = DateTime.UtcNow;
    }
}