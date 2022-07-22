namespace UMS.Persistence.Tenants;

public interface ITenantService
{
    public Tenant GetTenant();
    public void SetTenant(Tenant t);
}