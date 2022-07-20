using Application.Exceptions;
using Application.Models;
using Application.User.Commands.TeacherRegisterCourse;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Abstraction.Models;
using UMS.Persistence.Models;

namespace Application.User.Commands.TeacherAssignCourseToTime;

public class TeacherAssignCourseToTimeCommandHandler : IRequestHandler<TeacherAssignCourseToTimeCommand, CourseSessionDto>
{
    private readonly UmsContext _umsContext;

    private readonly IMapper _mapper;

    private readonly IChatHub _chatHub;
    private readonly IMailService1 _mailService1;
    public TeacherAssignCourseToTimeCommandHandler( UmsContext umsContext, IMapper mapper, IChatHub chatHub, IMailService1 mailService1)
    {
        _umsContext = umsContext;
        _mapper = mapper;
        _chatHub = chatHub;
        _mailService1 = mailService1;
    }

    public async Task<CourseSessionDto> Handle(TeacherAssignCourseToTimeCommand request, CancellationToken cancellationToken)
    {
        
        
        
        //teacher authorization based on provided ID
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.TeacherId & x.RoleId == 2);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("Provided ID is not valid");
        }

        
        
       
        long newTeacherPerCourseId = (long)request.TeacherPerCourseId;  //i had to put the command attributes as nullable in order to use required with decorators
                                                                         //so here i switched it back to long instead of long?
        long newSessionTimeId = (long)request.SessionTimeId;
        
        
        var existingCourseAndTeacher = _umsContext.TeacherPerCourses.FirstOrDefault(x => x.Id == request.TeacherPerCourseId);
        if (existingCourseAndTeacher == null)
        {
            throw new NotPossibleException("You did not register this course");
        }
        
        var existingTimeSlot = _umsContext.SessionTimes.FirstOrDefault(x => x.Id == request.SessionTimeId);
        if (existingTimeSlot == null)
        {
            throw new NotPossibleException("You did not register this time slot");
        }


        if (existingCourseAndTeacher.TeacherId != request.TeacherId)
        {
            throw new NotPossibleException("You are not the corresponding teacher of this course");
        }
        
        
        
        TeacherPerCoursePerSessionTime newCourseToTimeSlot = _mapper.Map<TeacherPerCoursePerSessionTime>(request);


        var response = await _umsContext.TeacherPerCoursePerSessionTimes.AddAsync(newCourseToTimeSlot);
        
        
        _umsContext.SaveChanges();
        
        
        CourseSessionDto detailsToReturn = _mapper.Map<CourseSessionDto>(response.Entity);


        
        
        
        
        
        //sending mail and push notification to all students t 
        
        
        var concernedCourse = _umsContext.Courses.FirstOrDefault(x => x.Id == existingCourseAndTeacher.CourseId);
        var concernedTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == existingCourseAndTeacher.TeacherId);


        
        
        Task t = _chatHub.SendMessage("Admin Course Creation", "A new course has been added");
        
        var studentsWithEnableNotifications =  _umsContext.Users.Where(x => x.EnableNotifications == 1 & x.RoleId==3).ToList();
        
        foreach (var VARIABLE in studentsWithEnableNotifications)
        {
            MailRequest c = new MailRequest()
            {
                ToEmail = VARIABLE.Email,
                Subject = "New Session time for course",
                Body = "Dear Student " + VARIABLE.Name + ", " + "\n" + "This email is to inform you that the course - " 
                       + concernedCourse.Name + " - will be given by the instructor " + concernedTeacher.Name + " from " + existingTimeSlot.StartTime + " to " + existingTimeSlot.EndTime 
            };
            await _mailService1.SendEmailAsync(c);
            
            
            
           
        }
        
        
        
        
        
        
        
        
        
        return detailsToReturn;

    }
}