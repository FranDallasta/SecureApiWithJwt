namespace SecureApiWithJwt.Models
{
    public class LoginRequest
    {
        public required string Email { get; set; }    // User's email
        public required string Password { get; set; } // User's password
    }
}
