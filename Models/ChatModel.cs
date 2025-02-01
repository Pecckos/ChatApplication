


namespace PecckosChatProgram.Models
{
    public class ChatMessage
    {
        public int id { get; set; }
        public User User { get; set; } //Navigation property
        public int? UserId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

    }

}


