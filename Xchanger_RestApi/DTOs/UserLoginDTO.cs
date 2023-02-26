using System;
using System.ComponentModel.DataAnnotations;

namespace Xchanger_RestApi.Models
{
    public class UserLoginDTO
    {


        [Required]
        [StringLength(20)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Login zawiera niebezpieczne znaki")]
        public string Login { get; set; }

        [Required]
        [StringLength(30)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Hasło zawiera niebezpieczne znaki")]
        public string Password { get; set; }

    }
}