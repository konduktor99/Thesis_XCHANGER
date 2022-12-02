using System;

namespace Xchanger_RestApi.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ExpireTime { get; set; }
    }
}