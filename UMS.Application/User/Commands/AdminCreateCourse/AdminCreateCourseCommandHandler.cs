using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NpgsqlTypes;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.User.Commands.AdminCreateCourse;

public class AdminCreateCourseCommandHandler : IRequestHandler<AdminCreateCourseCommand,CourseDto >
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    private readonly IMailService1 _mailService1;
    private readonly IChatHub _chatHub;

    public AdminCreateCourseCommandHandler(UmsContext db, IMapper mapper, IMailService1 mailService1, IChatHub chatHub)
    {
        _umsContext = db;
        _mapper = mapper;
        _mailService1 = mailService1;
        _chatHub = chatHub;
    }
   

    public async Task<CourseDto> Handle(AdminCreateCourseCommand request, CancellationToken cancellationToken)
    {
        
        
        
        var validAdmin = _umsContext.Users.FirstOrDefault(x => x.Id == request.AdminId & x.RoleId == 1);

        if (validAdmin == null)
        {
            throw new InvalidIdentifierException("Provided Admin ID is invalid");
        }
        
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
        
        
        
        
        //sending mail and push notification to all students t 
        
        Task t = _chatHub.SendMessage("Admin Course Creation", "A new course has been added");
        
        var studentsWithEnableNotifications =  _umsContext.Users.Where(x => x.EnableNotifications == 1 & x.RoleId==3).ToList();
        
        foreach (var VARIABLE in studentsWithEnableNotifications)
        {
            MailRequest c = new MailRequest()
            {
                ToEmail = VARIABLE.Email,
                Subject = "New course available for registration",
                Body = "Dear Student " + VARIABLE.Name + ", " + "\n" + "This email is to inform you that a new course - " 
                + res.Entity.Name + " - has been added to the course offering."
            };
            await _mailService1.SendEmailAsync(c);
            
            
            
           
        }
        
        
        
        
        
        return courseResult;
    }
}