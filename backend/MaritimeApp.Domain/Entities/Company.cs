namespace MaritimeApp.Domain.Entities;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<ShipCompanyRelation> ShipRelations { get; set; } = new List<ShipCompanyRelation>();
}
