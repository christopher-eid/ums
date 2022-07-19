using Application.Models;
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
    private readonly IMapper _mapper;

    public AdminCreateCourseCommandHandler(UmsContext db, IMapper mapper)
    {
        _umsContext = db;
        _mapper = mapper;
    }
   

    public async Task<CourseDto> Handle(AdminCreateCourseCommand request, CancellationToken cancellationToken)
    {
        
        //create a npsql range using lower and upper bound entered by admin
        NpgsqlRange<DateOnly>? enrolmentDateRange = new NpgsqlRange<DateOnly>(DateOnly.FromDateTime(request.LowerBound), DateOnly.FromDateTime(request.UpperBound));
        //do not give an id to course since it is auto incremented in database
        Course newCourse = new Course()
        {
            Name = request.Name, 
            MaxStudentsNumber = request.MaxStudentsNumber,
            EnrolmentDateRange = enrolmentDateRange
        };
        var res = await _umsContext.Courses.AddAsync(newCourse);
        _umsContext.SaveChanges();

        CourseDto courseResult = new CourseDto()
        {
            Id = res.Entity.Id,
            Name = res.Entity.Name,
            LowerBound = res.Entity.EnrolmentDateRange.Value.LowerBound.ToDateTime(new TimeOnly()),
            MaxStudentsNumber = res.Entity.MaxStudentsNumber,
            UpperBound = res.Entity.EnrolmentDateRange.Value.LowerBound.ToDateTime(new TimeOnly())
        };
        // we need to use DateTime when returning the object, not dateOnly,
        //since DateOnly is specific to .net and other technologies may not have it
        return courseResult;
    }
}