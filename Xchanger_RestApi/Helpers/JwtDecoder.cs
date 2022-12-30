using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Xchanger_RestApi.Helpers
{
    public class JwtDecoder
    {

        public static int? GetClaimFromJwt(string jwt, string claimKey)
        {
            if (jwt == null)
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(jwt);
            var tokenS = jsonToken as JwtSecurityToken;
            return Int32.Parse(tokenS.Claims.First(claim => claim.Type == claimKey).Value);
        }
    }
}
