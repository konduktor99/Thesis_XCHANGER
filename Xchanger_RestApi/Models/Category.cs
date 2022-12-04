using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Xchanger_RestApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }

        [InverseProperty(nameof(Item.Categories))]
        public virtual ICollection<Item> Items { get; set; }
    }
}
