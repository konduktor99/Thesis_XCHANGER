using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Xchanger_RestApi.Models
{
    public partial class GetExchangeDTO
    {
       
        public int Id { get; set; }
        public DateTime? AcceptDate { get; set; }
        public DateTime? ReplyDate { get; set; }
        public DateTime RequestDate { get; set; }
        public byte State { get; set; }
        public string Mess1 { get; set; }
        public string Mess2 { get; set; }
        //public int InitiatorId { get; set; }
        public dynamic Initiator { get; set; }
        public dynamic Receiver { get; set; }
        public dynamic Item { get; set; }
        public dynamic Item2 { get; set; }
        public byte[] ImgBytes1 { get; set; }
        public byte[] ImgBytes2 { get; set; }
    }
}
