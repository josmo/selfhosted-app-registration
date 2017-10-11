using System.Collections.Generic;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Tokens;

namespace NancyService.helpers
{
    public class JwtToken
    {
        public string  sub;
        public string nameid;
        public long nbf;
        public long exp;
        public long iat;
        public string iss;
        public string aud;
        public IEnumerable<string> permissions;

    }
}