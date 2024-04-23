using Google.Apis.Classroom.v1.Data;

namespace ClassroomApi.Service
{
    public interface IGoogleClassroomService
    {
        Task<List<Course>> GetCoursesAsync();
    }
}
