using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public partial class CreateItemDTO
    {
        [Required]
        [StringLength(45)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Nazwa zawiera niebezpieczne znaki")]
        public string Title { get; set; }

        [Required]
        [StringLength(850)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Opis zawiera niebezpieczne znaki")]
        public string Description { get; set; }

        public bool IsNew { get; set; }
        [StringLength(45)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Nazwa lokalizacji zawiera niebezpieczne znaki")]

        public string Location { get; set; }

        public int CategoryId { get; set; }

        public IFormFile[] Files { get; set; }
    }
}
