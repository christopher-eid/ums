namespace UMS.Persistence.Tenants;

public class TenantService : ITenantService
{
    public Tenant Tenant { get;  set; } = default!;


    public Tenant GetTenant()
    {
        return Tenant;
    }
    
    public void SetTenant(Tenant tenant)
    {
        Tenant = tenant;
    }
}