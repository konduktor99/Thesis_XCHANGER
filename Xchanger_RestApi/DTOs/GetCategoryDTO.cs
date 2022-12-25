using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public class GetCategoryDTO
    {
        public GetCategoryDTO()
        {
          
        }

      
        public int Id { get; set; }
        public string Name { get; set; }
      

    }
}
