using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PecckosChatProgram.Models
{
    public class RegistrationViewModel 
    {
      [Required]
      public string UserName { get; set ;}

      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Compare("Password", ErrorMessage = "Passwords do not match")]
      [DataType(DataType.Password)]
      public string ConfirmPassword  { get; set; }
    
         
    }


}