using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using WAAPI.Helpers;
using WebApi.Helpers;

namespace WAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        DataContext _dataContext;
        public UserController(DataContext dataContext, ILogger<UserController> logger)
        {
            this._dataContext = dataContext;
            _logger = logger;
        }        

        [AllowAnonymous]
        [Route("AuthenticateUser")]
        [HttpGet]
        
        public AuthToken AuthenticateUser()
        {
            AuthToken token = new();
            try
            {
                TokenService service = new TokenService();
                Request.Headers.TryGetValue("LoginUser", out var headerValue);
                var usr = _dataContext.Users.Where(x => x.WindowsUser == headerValue.ToString()).FirstOrDefault();
                if (usr != null)
                {
                    token = service.Generate(usr);
                }
                else
                    token = null;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);

            }
            return token;
        }

        [HttpGet]
        [Route("GetUserPurchases")]
        [AllowAnonymous]
        public  ActionResult GetUserPurchases()
        {
            string windowUser = GetToken(Request);/*Get jwt token from request header and validating  */
            if(string.IsNullOrEmpty(windowUser))/*if jwt token not valid then sent response as unauthorized */
                return Unauthorized();
            
            var usrs = _dataContext.Users.Where(x => x.WindowsUser == windowUser).FirstOrDefault();
            if (usrs == null)
                return Ok(new List<UserPurchases>());
            int userId = usrs.Id;
            List<UserPurchases> userPurchases = _dataContext.UserPurchases.Where(x => x.UserId == userId).ToList();
            return Ok(userPurchases);
        }
        
        [NonAction]
        private string GetToken(HttpRequest httpRequest)
        {
            var handler = new JwtSecurityTokenHandler();
            string authHeader = httpRequest.Headers["Authorization"];
            authHeader = authHeader.Replace("Bearer ", "");
            var jsonToken = handler.ReadToken(authHeader);
            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
            string? id = tokenS.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

            if (id != null)
                return id;
            else
                return string.Empty;

        }
    }
}
