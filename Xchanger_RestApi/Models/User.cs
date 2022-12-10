using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Xchanger_RestApi.Models
{
    public partial class User
    {
        public User()
        {
            Items = new HashSet<Item>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("login")]
        [StringLength(40)]
        public string Login { get; set; }
        [Required]
        [Column("email")]
        [StringLength(60)]
        public string Email { get; set; }
        [Column("phone_number")]
        [StringLength(9)]
        public string PhoneNumber { get; set; }
        [Required]
        [Column("password_hash")]

        public byte[] PasswordHash { get; set; }
        [Column("password_salt")]
        public byte[]? PasswordSalt { get; set; }

        [Column("refresh_token")]
        [StringLength(64)]
        public string? RefreshToken { get; set; }

        [Column("join_date")]
        public DateTime? JoinDate { get; set; }

        [Column("refresh_token_expire")]
        public DateTime? RefreshTokenExpireTime { get; set; }

        [Column("refresh_token_create")]
        public DateTime? RefreshTokenCreateTime { get; set; }

        //  [Column("admin")]
        // public bool? Admin { get; set; }

        [InverseProperty(nameof(Item.Users))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(Exchange.Initiator))]
        public virtual ICollection<Exchange> Exchanges { get; set; }
    }
}
