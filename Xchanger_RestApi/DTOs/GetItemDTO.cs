using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public partial class GetItemDTO
    {
        public GetItemDTO()
        {
          
        }

      
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public bool IsNew { get; set; }
        public bool Active { get; set; }
        public string Location { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        //public IFormFile Files { get; set; }
        public List<byte[]> ImgBytesList { get; set; }
        public dynamic Category { get; set; }
        public dynamic User { get; set; }
        //   public virtual Category Cat { get; set; }
        //   public virtual User Usr { get; set; }

    }
}
