//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;
//using StudentScore.Models;

//namespace StudentScore.Extensions.Authorizations
//{
//    public class JwtToken
//    {
//        public static TokenInfoViewModel BuildJwtToken(Claim[] claims)
//        {
//            var now = new DateTime();
//            // 实例化JwtSecurityToken
//            var jwt = new JwtSecurityToken(
//                issuer: ConfigField.Iss,
//                audience: ConfigField.Aud,
//                claims: claims,
//                notBefore:now,
//                expires: now.AddSeconds(1000),
//                signingCredentials: new SigningCredentials(
//                    new SymmetricSecurityKey(
//                        Encoding.UTF8.GetBytes(ConfigField.Secret)), SecurityAlgorithms.HmacSha256)
//            );
//            var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwt);

//            var responseJson = new TokenInfoViewModel()
//            {
//                success = true,
//                token = jwtToken,
//                expires_in = now.AddSeconds(1000).Second,
//                token_type = "Bearer"
//            };
//            return responseJson;
//        }
//    }

//    public class TokenInfoViewModel
//    {
//        public bool success { get; set; }
//        public string token { get; set; }
//        public double expires_in { get; set; }
//        public string token_type { get; set; }
//    }

//}