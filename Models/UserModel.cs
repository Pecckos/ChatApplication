using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PecckosChatProgram.Models
{
    public class User 
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>(); // NAvigation property for related messages

        [Required] //Stored hashpassword for security reasons in the database. it will not be plain text. 
        public string PasswordHash { get; set; }

        [Required] // field is mandatory
        [EmailAddress] // validates that the value is valid email format
        public string Email { get; set; }

        [NotMapped]
        [Required]
        [DataType(DataType.Password)] // specifies that this is a password field
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password incorrect, try again")] //Compares with the password field. 
        public string ConfirmPassword { get; set; }
         
    }


}
