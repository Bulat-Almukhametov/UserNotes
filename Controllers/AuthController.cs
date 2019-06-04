using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UserNotes.Dtos;

namespace UserNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        const string HASHING_NUMBER_KEY = "HashingNumber";

        [HttpPost("[action]")]
        public async Task<int> GetHashingNumber()
        {
            var rnd = new Random();
            int number = rnd.Next();

            HttpContext.Session.SetInt32(HASHING_NUMBER_KEY, number);

            return number;
        }
        [HttpPost("[action]")]
        public async Task Login(CredentialsDto credentials)
        {
            var identity = GetIdentity(credentials.Nickname, credentials.PasswordHash);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            string encodedJwt = CreateToken(identity);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private static string CreateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

        private ClaimsIdentity GetIdentity(string nickname, string passwordHash)
        {
            if (String.Equals(nickname, "Mickey Mouse", StringComparison.OrdinalIgnoreCase)
                && CheckHashWithValue(passwordHash, "Disney Land"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, "Mickey Mouse"),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private bool CheckHashWithValue(string hash, string value)
        {
            int? sessionHashingNumber = HttpContext.Session.GetInt32(HASHING_NUMBER_KEY);
            if (sessionHashingNumber.HasValue)
            {
                int hashingNumber = sessionHashingNumber.Value;
                string hashingValue = hashingNumber + value.ToLower();

                HashAlgorithm algorithm = SHA256.Create();
                var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashingValue));
                string hashedValue = BitConverter.ToString(hashedBytes).Replace("-", String.Empty);

                if (String.Equals(hash, hashedValue, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}