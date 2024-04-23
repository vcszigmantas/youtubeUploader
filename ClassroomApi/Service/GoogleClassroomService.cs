using Google.Apis.Auth.OAuth2;
using Google.Apis.Classroom.v1;
using Google.Apis.Classroom.v1.Data;
using Google.Apis.Services;
using System.IO;

namespace ClassroomApi.Service
{
    public class GoogleClassroomService : IGoogleClassroomService
    {
        private readonly ClassroomService _classroomService;

        public GoogleClassroomService()
        {
            string credentialsFilePath = "client_secret_722021585334-0a96avhqjagsfe5hhce8v5i9kdm8m0ih.apps.googleusercontent.com.json";

            using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                // Load credentials and initialize the ClassroomService here
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { ClassroomService.Scope.ClassroomCourses },
                    "user",
                    CancellationToken.None).Result;

                _classroomService = new ClassroomService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Your Application Name",
                });
            }
        }

        public async Task<List<Course>> GetCoursesAsync()
        {
            var request = _classroomService.Courses.List();
            var response = await request.ExecuteAsync();
            return response.Courses.ToList();
        }
    }
}
