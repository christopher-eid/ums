using Application.Exceptions;
using Application.Models;
using Application.User.Commands.TeacherRegisterCourse;
using AutoMapper;
using Domain.Models;
using MediatR;
using UMS.Persistence.Models;

namespace Application.User.Commands.TeacherCreateTimeSlot;

public class TeacherCreateTimeSlotCommandHandler : IRequestHandler<TeacherCreateTimeSlotCommand, TeacherSessionDto>
{
    private readonly UmsContext _umsContext;
    private readonly IMapper _mapper;

    public TeacherCreateTimeSlotCommandHandler( UmsContext umsContext, IMapper mapper)
    {
        _umsContext = umsContext;
        _mapper = mapper;
    }

    public async Task<TeacherSessionDto> Handle(TeacherCreateTimeSlotCommand request, CancellationToken cancellationToken)
    {
        
        
        //teacher authorization based on provided ID
        var existingTeacher = _umsContext.Users.FirstOrDefault(x => x.Id == request.TeacherId & x.RoleId == 2);

        if (existingTeacher == null)
        {
            throw new InvalidIdentifierException("Provided ID is not valid");
        }


        
        //mapping
        SessionTime sessionToAdd = _mapper.Map<SessionTime>(request);

        
        
        var newSessionResponse = await _umsContext.SessionTimes.AddAsync(sessionToAdd);

        _umsContext.SaveChanges();
        
        /*
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<SessionTime, TeacherSessionDto>()
        );
        
        
        var mapper = new AutoMapper.Mapper(config);*/
        TeacherSessionDto teacherSessionResult = _mapper.Map<TeacherSessionDto>(newSessionResponse.Entity);

        
        return teacherSessionResult;
    }
}