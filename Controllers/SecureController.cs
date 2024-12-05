using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureApiWithJwt.Controllers
{
    [ApiController]
    [Route("secure")]
    [Authorize] // Ensures only authenticated users can access these endpoints
    public class SecureController : ControllerBase
    {
        // Accessible to all authenticated users
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userName = User.FindFirst("FullName")?.Value;
            var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            return Ok(new { Message = "Profile retrieved successfully", UserName = userName, Role = role });
        }

        // Admin-only endpoint
        [HttpGet("admin-panel")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult AdminPanel()
        {
            return Ok(new { Message = "Welcome to the Admin Panel!" });
        }

        // Instructor and Admin endpoint
        [HttpGet("instructor-schedule")]
        [Authorize(Policy = "InstructorPolicy")]
        public IActionResult InstructorSchedule()
        {
            return Ok(new { Message = "Welcome to the Instructor's Schedule!" });
        }

        // Student and Admin endpoint
        [HttpGet("student-records")]
        [Authorize(Policy = "StudentPolicy")]
        public IActionResult StudentDashboard()
        {
            return Ok(new { Message = "Welcome to the Student Dashboard!" });
        }
    }
}
