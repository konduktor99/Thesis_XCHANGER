using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xchanger_RestApi.DTOs
{
    public class UserDTO
    {


      
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public int? PasswordSalt { get; set; }
        //public virtual ICollection<Item> Items { get; set; }
    }
}
