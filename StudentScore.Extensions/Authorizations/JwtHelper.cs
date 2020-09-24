using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace StudentScore.Extensions.Authorizations
{
    public class JwtHelper
    {
        private TokenModelJwt _tokenModel;

        public JwtHelper(IOptions<TokenModelJwt> tokenModel)
        {
            _tokenModel = tokenModel.Value;
        }
        public static string IssueJwt(TokenModelJwt tokenModel)
        {
            string iss = ConfigField.Iss;
            string aud = ConfigField.Aud;
            string secret = ConfigField.Secret;


            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Exp,
                    $"{new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds()}"),
                new Claim(JwtRegisteredClaimNames.Iss, iss),
                new Claim(JwtRegisteredClaimNames.Aud, aud),
                new Claim(ClaimTypes.Role,tokenModel.Role)
            };
            var nbf = new DateTimeOffset(DateTime.Now).DateTime;
            var exp = new DateTimeOffset(DateTime.Now.AddSeconds(1000)).DateTime;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(issuer:iss, claims: claims, notBefore:nbf, expires:exp,signingCredentials: credentials);
            var jwtHandler = new JwtSecurityTokenHandler();
            var encodeJwt = jwtHandler.WriteToken(jwt);
            return encodeJwt;
        }

        public static TokenModelJwt SerializeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role;
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("jwthelper进入异常");
                Console.WriteLine(e);
                throw;
            }
            var tm = new TokenModelJwt
            {
                Uid = Int64.Parse(jwtToken.Id),
                Role = role != null ? role.ToString() : ""
            };
            return tm;


        }
    }

   
}
