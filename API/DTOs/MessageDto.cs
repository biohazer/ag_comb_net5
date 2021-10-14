using System;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class MessageDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; }
        // public AppUser Sender { get; set; }
        public string SenderPhotoUrl { get; set; }//new
        public int RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        // public AppUser Recipient { get; set; }
        public string RecipientPhotoUrl { get; set; }//new
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } //= DateTime.Now;

        [JsonIgnore]
        public bool SenderDeleted { get; set; }
        [JsonIgnore]
        public bool RecipientDeleted { get; set; }
    }
}