using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Xchanger_RestApi.Models
{
    public partial class Item
    {
        public Item()
        {
            ExchangeItems = new HashSet<Exchange>();
            ExchangeItems2s = new HashSet<Exchange>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
        [StringLength(60)]
        public string Title { get; set; }
        [Required]
        [Column("description")]
        [StringLength(1700)]
        public string Description { get; set; }
        [Column("publication_date", TypeName = "date")]
        public DateTime PublicationDate { get; set; }
        [Column("new")]
        public bool New { get; set; }
        [Column("Categories_id")]
        public int CategoriesId { get; set; }
        [Column("Users_id")]
        public int UsersId { get; set; }

        [ForeignKey(nameof(CategoriesId))]
        [InverseProperty(nameof(Category.Items))]
        public virtual Category Categories { get; set; }
        [ForeignKey(nameof(UsersId))]
        [InverseProperty(nameof(User.Items))]
        public virtual User Users { get; set; }
        [InverseProperty(nameof(Exchange.Items))]
        public virtual ICollection<Exchange> ExchangeItems { get; set; }
        [InverseProperty(nameof(Exchange.Items2))]
        public virtual ICollection<Exchange> ExchangeItems2s { get; set; }
    }
}
