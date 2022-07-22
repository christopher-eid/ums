namespace UMS.Persistence.Tenants;

public class Tenant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ConnectionString { get; set; }
    public string Type { get; set; } = "normal";
}