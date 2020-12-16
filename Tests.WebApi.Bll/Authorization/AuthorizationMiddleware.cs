using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Tests.WebApi.Bll.Options;
using Tests.WebApi.Contexts;
using Tests.WebApi.Utilities.Exceptions;

namespace Tests.WebApi.Bll.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly MainContext _db;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                ParseToken(context, token);
            }
            await _next(context);
        }

        private void ParseToken(HttpContext context, string token)
        {
            try
            {
                string securityKey = AuthOption.KEY;
                if (securityKey == null)
                {
                    throw ExceptionFactory.SoftException(ExceptionEnum.SecurityKeyIsNull, "Invalid security key");
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKeyBytes = Encoding.ASCII.GetBytes(securityKey);

                SecurityToken SignatureValidator(string encodedToken, TokenValidationParameters parameters)
                {
                    var jwt = new JwtSecurityToken(encodedToken);

                    var hmac = new HMACSHA256(securityKeyBytes);

                    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(hmac.Key), SecurityAlgorithms.HmacSha256Signature, SecurityAlgorithms.Sha256Digest);

                    var signKey = signingCredentials.Key as SymmetricSecurityKey;

                    var encodedData = jwt.EncodedHeader + "." + jwt.EncodedPayload;
                    var compiledSignature = Encode(encodedData, signKey.Key);

                    if (compiledSignature != jwt.RawSignature)
                    {
                        throw new Exception("Token signature validation failed.");
                    }
                    return jwt;
                }

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(securityKeyBytes),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireSignedTokens = false, //погугли
                    ClockSkew = TimeSpan.Zero,
                    SignatureValidator = SignatureValidator,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.User = new GenericPrincipal(new AuthorizedUserModel
                {
                    Id = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value),
                    RoleId = int.Parse(jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value),
                }, new[] { "" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string Encode(string input, byte[] key)
        {
            HMACSHA256 sha = new HMACSHA256(key);
            byte[] byteArray = Encoding.UTF8.GetBytes(input);
            MemoryStream stream = new MemoryStream(byteArray);
            byte[] hashValue = sha.ComputeHash(stream);
            return Base64UrlEncoder.Encode(hashValue);
        }
    }
}
