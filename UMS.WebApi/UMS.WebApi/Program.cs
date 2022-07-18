

using System.Configuration;
using System.Reflection;
using Application.User.Commands.AdminCreateCourse;
using AutoMapper;
using MailKit;
using MediatR;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using UMS.Infrastructure.Abstraction.Interfaces;
using UMS.Infrastructure.Services;
using UMS.Persistence.Models;
using UMS.WebApi;
using LoggerExtensions = Serilog.LoggerExtensions;


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

/*
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;


var settings = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
    .CertificateFingerprint("<FINGERPRINT>")
    .Authentication(new BasicAuthentication("<USERNAME>", "<PASSWORD>"));

var client = new ElasticsearchClient(settings);*/

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Information() //set to error if we want only error level
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200") ){
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6
    }));

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





/*
void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
*/






