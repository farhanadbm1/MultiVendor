using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiVendorEcommerceAPI.Controllers
{
    [ApiController]
    [Route("MultiVendorEcommerceAPI/[controllers]")]
    public class ProtectedController : ControllerBase
    {
        [Authorize] // Only authenticated users can access this endpoint
        [HttpGet("data")]
        public IActionResult GetProtectedData()
        {
            return Ok("This is protected data!");
        }

        [Authorize(Roles = "Admin")] // Only users with the "Admin" role can access this endpoint
        [HttpGet("admin-data")]
        public IActionResult GetAdminData()
        {
            return Ok("This is admin-only data!");
        }
    }
}