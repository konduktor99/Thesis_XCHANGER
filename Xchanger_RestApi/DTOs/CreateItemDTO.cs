using Microsoft.AspNetCore.Http;
using System;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public partial class CreateItemDTO
    {
        public CreateItemDTO()
        {
          
        }

      
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsNew { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public IFormFile[] Files { get; set; }
        public byte[] ImgBytes { get; set; }
     //   public virtual Category Cat { get; set; }
     //   public virtual User Usr { get; set; }
      
    }
}
