using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace Xchanger_RestApi.Models
{
    public  class UserRegisterDTO
    {


        [Required]
        [StringLength(20)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Login zawiera niebezpieczne znaki")]
        public string Login { get; set; }
        [Required]
        [StringLength(35)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Email zawiera niebezpieczne znaki")]
        public string Email { get; set; }
        [Required]
        [StringLength(35)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Numer telefonu zawiera niebezpieczne znaki")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(30)]
        [RegularExpression("^[^<>{}\\[\\]']+$")]
        public string Password { get; set; }

    }
}
