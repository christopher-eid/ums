using Microsoft.EntityFrameworkCore;
using Npgsql;
using UMS.Persistence.Models;
using UMS.Persistence.Tenants;

namespace UMS.WebApi.Configurations;

public static class MigrationConfiguration
{


    
    
    public static async Task Initialize(WebApplication app)
    {
        
        
        // initialize the databases
        
        
        var cs = "Host=localhost;Port=5432;Database=tenantsForUmsMultitenancy;Username=postgres;Password=123456"; //connection string to the database of tenants

        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql = "SELECT * FROM \"Tenants\""; //put backslashes because we want to use double quotes since T is capital
        using var cmd = new NpgsqlCommand(sql, con);

        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        Tenant tenant = new Tenant()
        {
            Id = 3,
            Name = "DEFAULT_TENANT",
            Type = "standard",
            ConnectionString = "Host = localhost;Port = 5432;Database = defaultums;Username = postgres;Password = 123456",
        };
        
        using var scope = app.Services.CreateScope();
        var tenantSetter = scope.ServiceProvider.GetRequiredService<ITenantService>();
        tenantSetter.SetTenant(tenant);
                

        var db = scope.ServiceProvider.GetRequiredService<UmsContext>();
        await db.Database.MigrateAsync();
        
        
        
        
        //creating a database only for the vip tenants
        while (rdr.Read())
        {
            if (rdr.GetString(2) == "vip")
            {
                tenant = new Tenant()
                {
                    Id = rdr.GetInt32(0),
                    Name = rdr.GetString(1),
                    Type = rdr.GetString(2),
                    ConnectionString = rdr.GetString(3)
                };

                using var scope1 = app.Services.CreateScope();
                var tenantSetter1 = scope1.ServiceProvider.GetRequiredService<ITenantService>();
                tenantSetter1.SetTenant(tenant);
                

                var db1 = scope1.ServiceProvider.GetRequiredService<UmsContext>();
                await db1.Database.MigrateAsync();
                await db1.SaveChangesAsync();
            }

        }
        
        /*
        var tenantConfig = app.Configuration.Get<TenantConfigurationSection>()!;
        foreach (var tenant in tenantConfig.Tenants)
        {
            using var scope = app.Services.CreateScope();
            var tenantSetter = scope.ServiceProvider.GetRequiredService<ITenantSetter>();
            tenantSetter.SetTenant(tenant);

            var db = scope.ServiceProvider.GetRequiredService<Database>();
            await db.Database.MigrateAsync();

            // unique data
            if (!db.Animals.Any())
            {
                var unique = data.Where(a => a.Tenant == tenant.Name).ToList();
                db.Animals.AddRange(unique);
                await db.SaveChangesAsync();
            }
        }*/
    }
}