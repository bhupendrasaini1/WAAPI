namespace WAAPI.Helpers
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string WindowsUser { get; set; }
    }
    public class IdentityUser
    {
        public string Username { get; set; }
        public bool IsAuthenticated { get; set; }
        public string AuthenticationType { get; set; }
    }
}
