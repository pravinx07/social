using System.IdentityModel.Tokens.Jwt;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using backend.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace backend.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            // This grabs your secret key from appsettings.json and encrypts it
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));

        }
        public string CreateToken(IdentityUser user)
        {
            // 1, Claims (the payload): This is the data hiden inside the token
            // in express {email:user.email , username: user.username}

            var claims = new List<Claim>
            {
new Claim(JwtRegisteredClaimNames.Email, user.Email),
new Claim(JwtRegisteredClaimNames.GivenName, user.UserName)

            };

            // 2. Credentials : How we sign the token using HMAC SHA512 encryption

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);

            // 3. Descriptor: the blueprint for this token (Payload, expiration, Signature, Issuer , Audiance)

            var tokenDecriptor = new SecurityTokenDescriptor

            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"]



            };

            //4. Generator: The machine that actually builds the token based on the blueprint

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);

            // return the final string token
            return tokenHandler.WriteToken(token);
        }

    }
}