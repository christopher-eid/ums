using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NpgsqlTypes;
using UMS.Persistence.Models;

namespace Application.User.Commands.AdminCreateCourse;

public class AdminCreateCourseCommandHandler : IRequestHandler<AdminCreateCourseCommand,CourseDto >
{
    private readonly UmsContext _umsContext;

    public AdminCreateCourseCommandHandler(UmsContext db)
    {
        _umsContext = db;
    }
   

    public async Task<CourseDto> Handle(AdminCreateCourseCommand request, CancellationToken cancellationToken)
    {
        
        //create a npsql range using lower and upper bound entered by admin
        NpgsqlRange<DateOnly>? enrolmentDateRange = new NpgsqlRange<DateOnly>(request.LowerBound, request.UpperBound);
        //do not give an id to course since it is auto incremented in database
        Course newCourse = new Course()
        {
            Name = request.Name, 
            MaxStudentsNumber = request.MaxStudentsNumber,
            EnrolmentDateRange = enrolmentDateRange
        };
        var res = await _umsContext.Courses.AddAsync(newCourse);
        _umsContext.SaveChanges();
        
        
        //using mapper so we return courseDTO for the user so we do not expose the internal classes we have
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<Course, CourseDto>()
        );
        
        
        var mapper = new Mapper(config);
        CourseDto courseResult = mapper.Map<CourseDto>(res.Entity);
        
        return courseResult;
    }
}