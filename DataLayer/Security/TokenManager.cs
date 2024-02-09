using AutoMapper;
using DataLayer.DALModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataLayer.Security
{
    public class TokenManager : ITokenRepository
    {
        private readonly RwaDatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public TokenManager(RwaDatabaseContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;

        }
        public static string GetToken()
        {
            var tokenKey = Encoding.UTF8.GetBytes("12345678901234567890123456789012");

            // Create a token descriptor (represents a token, kind of a "template" for token)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token using that descriptor, serialize it and return it
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);
            return serializedToken;
        }
        public Token AssignToken(string username, string password)
        {
            IEnumerable<User> users = _dbContext.Users.Include(x=>x.UserType);
            var isAuthenticated = HashManager.Authenticate(users, username, password);
            var user = users.FirstOrDefault(x=>x.Username.Equals(username));
            string userType = user.UserType.Type;
            if (!isAuthenticated)
            {
                throw new InvalidOperationException("Authentication failed");
            }

            // Get secret key bytes
            var jwtKey = Token.GetKey();
            var jwtKeyBytes = Encoding.UTF8.GetBytes(jwtKey);

            // Create a token descriptor (represents a token, kind of a "template" for token)
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(ClaimTypes.Role, userType)
                }),
                Issuer = Token.GetIssuer(),
                Audience = Token.GetAudience(),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(jwtKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token using that descriptor, serialize it and return it
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);

            return new Token
            {
                SerializedToken = serializedToken
            };

        }
    }
}
