﻿using Application.Models;
using AutoMapper;
using Domain.Models;
using UMS.Persistence.Models;

namespace Application.Mapper;

public class CourseSessionMapper : Profile
{
    public CourseSessionMapper()
    {
        CreateMap<TeacherPerCoursePerSessionTime, CourseSessionDto>();

    }
}