namespace Application.Models
{
    public class CourseDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public int? MaxStudentsNumber { get; set; }
        public DateTime LowerBound { get; set; } //use DateTime instead of DateOnly 
    
        public DateTime UpperBound { get; set; }

    }
}