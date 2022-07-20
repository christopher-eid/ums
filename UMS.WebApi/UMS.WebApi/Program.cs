

using System.Configuration;
using System.Reflection;
using Application.User.Commands.AdminCreateCourse;
using AutoMapper;
using MailKit;
using MediatR;
using Serilog;
using UMS.Infrastructure;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Services;
using UMS.Persistence.Models;
using UMS.WebApi.Configurations;
using UMS.WebApi.Middleware;


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

//to configure signalR
builder.Services.AddSignalR();
builder.Services.AddScoped<IChatHub, ChatHub>();

//to configure serilog
SerilogConfiguration.ConfigureLogging();
builder.Host.UseSerilog();

/*
builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information() //set to error if we want only error level
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:9200")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200") ){
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
    }));*/





var app = builder.Build();

//configure path for signalR
app.MapHub<ChatHub>("/chatHub");


//configure middleware used to log everytime an http request is sent

app.UseMiddleware<CustomMiddleware>();




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





