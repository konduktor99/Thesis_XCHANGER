using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public partial class SearchDTO
    {
        [StringLength(100)]
        [RegularExpression("^[^<>{}\\[\\]']+$", ErrorMessage = "Wprowadzono niedozwolone znaki")]
        public string Query { get; set; }
    }
}
