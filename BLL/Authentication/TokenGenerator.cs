using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace BLL.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public (string token, int expiration) GenerateToken(User user , IEnumerable<string> Roles )
        {
            List<Claim> UserClaims = new List<Claim> 
            { 
                    new (JwtRegisteredClaimNames.Sub , user.Id.ToString()) ,
                    new (JwtRegisteredClaimNames.Email , user.Email) ,
                    new (JwtRegisteredClaimNames.Name   , user.UserName) ,
                    new (JwtRegisteredClaimNames .Jti , new Guid().ToString()) , 
                    new (nameof(Roles), Roles != null ? string.Join(" , ", Roles) : null)//if have any role or roles will produce string with , between them

            }; 

            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@#$ew786324AhmedAdel9872346AAkvcjfiqwkzxAK"));

            SigningCredentials signing = new SigningCredentials(SignKey , SecurityAlgorithms.HmacSha256 );

             var ExpireIn = 30; 
             var ExpirationDate = DateTime.Now.AddMinutes(ExpireIn);

            var Token = new JwtSecurityToken(
                claims: UserClaims,
                expires: ExpirationDate,
                signingCredentials: signing
                );
             
            return (new JwtSecurityTokenHandler().WriteToken(Token) , ExpireIn);

        }

        public string? ValidateToken(string token)
        {
            var TokenHandler= new JwtSecurityTokenHandler();
            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("@#$ew786324AhmedAdel9872346AAkvcjfiqwkzxAK"));
            try
            {
                TokenHandler.ValidateToken(token,
                    new TokenValidationParameters
                    {
                        IssuerSigningKey = SignKey,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero

                    } , out SecurityToken validatedToken); 
                var JwtToken = (JwtSecurityToken)validatedToken;


                return JwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;




            }
            catch
            { 
                return null ;
            }



        }
    }
}
