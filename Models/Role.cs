
namespace SecureApiWithJwt.Models
{
    public class Role
    {
        public int Id { get; set; }          // Unique identifier for the role
        public required string Name { get; set; }    // Role name (e.g., Admin, Student, Instructor)
        public required string Description { get; set; } // Optional: Description of the role

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
