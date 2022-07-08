

using System.Configuration;
using System.Reflection;
using Application.User.Commands.AdminCreateCourse;
using AutoMapper;
using MailKit;
using MediatR;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Services;
using UMS.Persistence.Models;
using UMS.WebApi;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddMediatR(typeof(AdminCreateCourseCommand).GetTypeInfo().Assembly);
builder.Services.AddTransient<UmsContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.Load("UMS.Application"));


//to configure mail service
IConfiguration configuration = builder.Configuration;
builder.Services.Configure<MailSettings1>( configuration.GetSection("MailSettings1"));
builder.Services.AddTransient<IMailService1, MailService1>();
//builder.Services.AddTransient<IMailService1, MailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();