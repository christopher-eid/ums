using NpgsqlTypes;
using UMS.Persistence.Models;

namespace Domain.Models
{
    public class TeacherCourseDto
    {
        public long Id { get; set; }
        public long? TeacherId { get; set; }
        public long? CourseId { get; set; }

    }
}