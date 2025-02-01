


namespace PecckosChatProgram.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public List<ChatMessage> Messages { get; set; } = new(); // NAvigation property for related messages
         
    }


}
