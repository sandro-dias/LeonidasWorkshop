using Application.Data.Repository;
using Application.Data.Specification;
using Application.Middleware;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Middlewares
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationMiddleware(IWorkshopRepository workshopRepository)
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;

        public async Task<string> AuthenticateWorkshop(string cNPJ, string password)
        {
            var workshop = await _workshopRepository.FirstOrDefaultAsync(new GetWorkshopByPassword(cNPJ, password));
            if (workshop == null)
                return string.Empty;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, workshop.Name),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
