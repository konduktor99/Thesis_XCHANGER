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
        [Column("accept_date", TypeName = "date")]
        public DateTime? AcceptDate { get; set; }
        [Column("request_date", TypeName = "date")]
        public DateTime RequestDate { get; set; }
        [Column("state")]
        public byte State { get; set; }
        [Column("Items_id")]
        public int ItemId { get; set; }
        [Column("Items_2_id")]
        public int? Item2Id { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty(nameof(Item.ExchangeItems))]
        public virtual Item Items { get; set; }
        [ForeignKey(nameof(Item2Id))]
        [InverseProperty(nameof(Item.ExchangeItems2s))]
        public virtual Item Items2 { get; set; }
    }
}
