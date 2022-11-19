using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xchanger_RestApi.Models;

namespace Xchanger_RestApi.DTOs
{
    public class ExchangeDTO
    {

        public int Id { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime RequestDate { get; set; }
        public byte State { get; set; }
        public int ItemsId { get; set; }
        public int? Items2Id { get; set; }
        public  Item Items { get; set; }
      //  public virtual Item Items2 { get; set; }

    }
}
