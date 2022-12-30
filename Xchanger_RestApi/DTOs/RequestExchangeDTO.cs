using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public class RequestExchangeDTO
    {
        public int ItemId { get; set; }

        [StringLength(90)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Nazwa zawiera niebezpieczne znaki")]
        public string Mess1 { get; set; }
    }
}
