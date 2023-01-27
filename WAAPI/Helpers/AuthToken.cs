using System;

namespace WAAPI
{
    public class AuthToken
    {
        //public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long ExpiresIn { get; set; }

        public string AccessToken { get; set; }
        //public DateTime? LastLogin { get; set; }
        //public string RefreshToken { get; set; }
        //public DateTime RefreshTokenExpiryTime { get; set; }
    }
}