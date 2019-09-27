using System;

namespace Infrastructure.All.MessageBroking.Models
{
    public class Message
    {
        public string Type { get; set; }

        public Guid UserId { get; set; }
        
        public Guid AccountId { get; set; }

        public string Data { get; set; }
    }
}