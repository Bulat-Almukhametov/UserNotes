using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using UserNotes.Dtos;
using UserNotes.Infrastructure;

namespace UserNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        const string HASHING_NUMBER_KEY = "HashingNumber";
        private UserNotesDbContext _Context;

        public AuthController(UserNotesDbContext context)
        {
            _Context = context;
        }

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
            var identity = await GetIdentity(credentials.Nickname, credentials.PasswordHash);
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

        private async Task<ClaimsIdentity> GetIdentity(string nickname, string passwordHash)
        {
            var user = await _Context.Users.FirstAsync(u => EF.Functions.Like(nickname, u.Login));
            if (user != null && CheckHashWithValue(passwordHash, user.PasswordHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IsAdmin ? "admin" : "ordinal"),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        private bool CheckHashWithValue(string hash, string hashedPassword)
        {
            int? sessionHashingNumber = HttpContext.Session.GetInt32(HASHING_NUMBER_KEY);
            if (sessionHashingNumber.HasValue)
            {
                int hashingNumber = sessionHashingNumber.Value;
                string hashingValue = hashingNumber + hashedPassword;
                string hashedValue = Sha256(hashingValue);

                if (String.Equals(hash, hashedValue, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static string Sha256(string hashingValue)
        {
            HashAlgorithm algorithm = SHA256.Create();
            var hashedBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(hashingValue));
            string hashedValue = BitConverter.ToString(hashedBytes).Replace("-", String.Empty);
            return hashedValue;
        }
    }
}