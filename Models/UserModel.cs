using System.Collections.Generic;
using PecckosChatProgram.Data;
using PecckosChatProgram.Models;

namespace PecckosChatProgram.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }


}
