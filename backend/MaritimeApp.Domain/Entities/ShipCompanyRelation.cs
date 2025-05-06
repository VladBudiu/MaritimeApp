namespace MaritimeApp.Domain.Entities;

public class ShipCompanyRelation
{
    public int Id { get; set; }

    public int ShipId { get; set; }
    public Ship Ship { get; set; } = null!;

    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public string Role { get; set; } = null!; 
}
