using Application.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.User.Commands.TeacherRegisterCourse;

public class TeacherRegisterCourseCommandHandler : IRequestHandler<TeacherRegisterCourseCommand, TeacherCourseDto>
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;
    private readonly IMailService1 _mailService1;
    private readonly IChatHub _chatHub;
    public TeacherRegisterCourseCommandHandler( UmsContext umsContext, IMapper mapper, IMailService1 mailService1, IChatHub chatHub)
    {
        _umsContext = umsContext;
        _mapper = mapper;
        _chatHub = chatHub;
        _mailService1 = mailService1;

    }

    public async Task<TeacherCourseDto> Handle(TeacherRegisterCourseCommand request, CancellationToken cancellationToken)
    {
        
        //checking if course exists
        var existingCourse = _umsContext.Courses.FirstOrDefault(x => x.Id == request.CourseId);
        if (existingCourse == null)
        {
            throw new AlreadyExistingException("Incorrect Course ID");
        }
        
        
        //teacher authorization based on provided ID
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.TeacherId & x.RoleId == 2);
        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("Incorrect ID");
        }

        
        //checking if registration already exists
        var alreadyReg =
            _umsContext.TeacherPerCourses.FirstOrDefault(x =>
                x.CourseId == request.CourseId & x.TeacherId == request.CourseId);

        if (alreadyReg != null)
        {
            throw new AlreadyExistingException("You already registered this class");
        }
        
        TeacherPerCourse newTeacherPerCourse = new TeacherPerCourse()
        {
            TeacherId = (long)request.TeacherId, //i had to put the command attributes as nullable in order to use required with decorators
            CourseId = (long)request.CourseId    //so here i switched it back to long instead of long?
        };

        var newTeacherPerCourseResponse = await _umsContext.TeacherPerCourses.AddAsync(newTeacherPerCourse);

        
        
        _umsContext.SaveChanges();
       
        TeacherCourseDto teacherCourseResult = _mapper.Map<TeacherCourseDto>(newTeacherPerCourseResponse.Entity);

        
        
        
        
        //sending mail and push notification to all students t 
        
        Task t = _chatHub.SendMessage("Admin Course Creation", "A new course has been added");
        
        var studentsWithEnableNotifications =  _umsContext.Users.Where(x => x.EnableNotifications == 1 & x.RoleId==3).ToList();
        
        foreach (var VARIABLE in studentsWithEnableNotifications)
        {
            MailRequest c = new MailRequest()
            {
                ToEmail = VARIABLE.Email,
                Subject = "New course available for registration",
                Body = "Dear Student " + VARIABLE.Name + ", " + "\n" + "This email is to inform you that the course - " 
                       + existingCourse.Name + " - will be given by the instructor " + existingTeacher.Name + " . Session times will be later announced."
            };
            await _mailService1.SendEmailAsync(c);
            
            
            
           
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        return teacherCourseResult;
    }
}