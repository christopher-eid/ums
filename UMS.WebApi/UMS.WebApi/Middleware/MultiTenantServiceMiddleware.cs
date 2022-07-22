using System.ComponentModel.Design;
using Microsoft.Extensions.Options;
using Npgsql;
using UMS.Persistence.Tenants;

namespace UMS.WebApi.Middleware;

public class MultiTenantServiceMiddleware : IMiddleware
{
    private readonly ITenantService _tenantService;
    private readonly ILogger<MultiTenantServiceMiddleware> _logger;

    public MultiTenantServiceMiddleware(ITenantService setter,ILogger<MultiTenantServiceMiddleware> logger) 
    {
        _tenantService = setter;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       
      var cs = "Host=localhost;Port=5432;Database=tenantsForUmsMultitenancy;Username=postgres;Password=123456"; //connection string to the database of tenants

      using var con = new NpgsqlConnection(cs);
      con.Open();

      string sql = "SELECT * FROM \"Tenants\""; //put backslashes because we want to use double quotes since T is capital
      using var cmd = new NpgsqlCommand(sql, con);

      using NpgsqlDataReader rdr = cmd.ExecuteReader();

      //IN CASE REQUESTED TENANT WAS NOT FOUND, DEFAULT_TENANT WILL BE USED
      Tenant tenantUsed = new Tenant()
      {
          Id = 3,
          Name = "DEFAULT_TENANT",
          Type = "standard",
          ConnectionString = "Host = localhost;Port = 5432;Database = defaultums;Username = postgres;Password = 123456",
      };
      
      //going through the table of tenants to find the requested tenant
      while (rdr.Read())
      {
          context.Request.Headers.TryGetValue("tenant", out var tenantIdSpecified);
          
          string  tenantId = tenantIdSpecified.ToString();
          if (rdr.GetInt32(0).ToString() == tenantId)
          {
              tenantUsed = new Tenant()
              {
                  Id = rdr.GetInt32(0),
                  Name = rdr.GetString(1),
                  Type = rdr.GetString(2),
                  ConnectionString = rdr.GetString(3)
              };
          }
      }

        
      _logger.LogInformation("Using the tenant {tenant}", tenantUsed.Name);
      _tenantService.SetTenant(tenantUsed);


      
      await next(context);
    }
}