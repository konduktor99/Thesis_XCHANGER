using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Xchanger_RestApi.Models
{
    public partial class Exchange
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accept_date")]
        public DateTime? AcceptDate { get; set; }
        [Column("reply_date")]
        public DateTime? ReplyDate { get; set; }
        [Column("request_date")]
        public DateTime RequestDate { get; set; }
        [Column("state")]
        public byte State { get; set; }
        [Column("item_id")]
        public int ItemId { get; set; }
        [Column("item_2_id")]
        public int? Item2Id { get; set; }
        [Column("mess_1")]
        [StringLength(90)]
        public string Mess1 { get; set; }
        [Column("mess_2")]
        [StringLength(90)]
        public string Mess2 { get; set; }
        [Column("initiator_id")]
        public int InitiatorId { get; set; }

        [ForeignKey(nameof(InitiatorId))]
        [InverseProperty(nameof(User.Exchanges))]
        public virtual User Initiator { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty(nameof(Item.ExchangeItems))]
        public virtual Item Items { get; set; }
        [ForeignKey(nameof(Item2Id))]
        [InverseProperty(nameof(Item.ExchangeItems2))]
        public virtual Item Items2 { get; set; }
    }
}
