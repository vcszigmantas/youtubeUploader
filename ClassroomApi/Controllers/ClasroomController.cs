using ClassroomApi.Service;
using Google.Apis.Classroom.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassroomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasroomController : ControllerBase
    {
        private readonly IGoogleClassroomService _googleClassroomService;

        public ClasroomController(IGoogleClassroomService googleClassroomService)
        {
            _googleClassroomService = googleClassroomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var courses = await _googleClassroomService.GetCoursesAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }
    }
}
