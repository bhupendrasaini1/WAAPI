using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WAAPI.Helpers
{
    public class TokenService
    {
        public AuthToken Generate(Users user)
        {
            List<Claim> claims = new() {
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (JwtRegisteredClaimNames.UniqueName, user.WindowsUser.ToString()),
                new Claim ("UserId", user.Id.ToString())             
            };

            JwtSecurityToken token = new TokenBuilder()
            .AddAudience(TokenConstants.Audience)
            .AddIssuer(TokenConstants.Issuer)
            .AddExpiry(TokenConstants.ExpiryInMinutes)
            .AddKey(TokenConstants.key)
            .AddClaims(claims)
            .Build();
            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthToken()
            {
                Name = user.Name,
                AccessToken = accessToken,
                ExpiresIn = TokenConstants.ExpiryInMinutes,
                Email = user.Email,
            };
        }
    }
}
