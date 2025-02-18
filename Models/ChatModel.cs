using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PecckosChatProgram.Models
{
    public class ChatMessage
    {
        [Key]
        public int id { get; set; }
        
        public User User { get; set; } //Navigation property
        public int UserId { get; set; }
       
        [Required]
        public string MessagesChat { get; set; }
        public DateTime TimeStamp { get; set; }

    }

}


