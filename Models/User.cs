namespace SecureApiWithJwt.Models
{
    public class User
    {
        public int Id { get; set; }           // Unique identifier
        public required string FullName { get; set; }  // User's full name
        public required string Email { get; set; }     // Email for login
        public required string PasswordHash { get; set; } // Hashed password for security
        public DateTime BirthDate { get; set; }  // Date of birth
        public int RoleId { get; set; }          // Foreign key to Role
        public Role? UserRole { get; set; }      // Navigation property
    }
}
