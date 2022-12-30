using System;
using System.ComponentModel.DataAnnotations;


namespace Xchanger_RestApi.DTOs
{
    public class ReplyExchangeDTO
    {
        public int? Item2Id { get; set; }
        [StringLength(90)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Nazwa zawiera niebezpieczne znaki")]
        public string Mess2 { get; set; }
    }
}
