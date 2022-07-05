using NpgsqlTypes;
using UMS.Persistence.Models;

namespace Domain.Models
{
    public partial class CourseDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? MaxStudentsNumber { get; set; }
        public NpgsqlRange<DateOnly>? EnrolmentDateRange { get; set; }

    }
}