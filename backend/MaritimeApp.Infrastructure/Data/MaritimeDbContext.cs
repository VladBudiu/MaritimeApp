using MaritimeApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaritimeApp.Infrastructure.Data;

public class MaritimeDbContext : DbContext
{
    public MaritimeDbContext(DbContextOptions<MaritimeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Port> Ports => Set<Port>();
    public DbSet<ShipType> ShipTypes => Set<ShipType>();
    public DbSet<Ship> Ships => Set<Ship>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<ShipCompanyRelation> ShipCompanyRelations => Set<ShipCompanyRelation>();
    public DbSet<ShipSpecification> ShipSpecifications => Set<ShipSpecification>();
    public DbSet<ShipLocationLog> ShipLocationLogs => Set<ShipLocationLog>();
    public DbSet<VisitedCountry> VisitedCountries => Set<VisitedCountry>();
    public DbSet<Voyage> Voyages => Set<Voyage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ✅ Table & column name fixes (match lowercase PostgreSQL schema)
        modelBuilder.Entity<Ship>(entity =>
        {
            entity.ToTable("ship");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ImoNumber).HasColumnName("imo_number");
            entity.Property(e => e.ShipTypeId).HasColumnName("ship_type_id");
            entity.Property(e => e.FlagCountryId).HasColumnName("flag_country_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Port>(entity =>
        {
            entity.ToTable("port");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
        });


        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("company");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<ShipType>(entity =>
        {
            entity.ToTable("shiptype");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TypeName).HasColumnName("type_name");
        });

        modelBuilder.Entity<ShipSpecification>(entity =>
        {
            entity.ToTable("shipspecification");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.YearBuilt).HasColumnName("year_built");
            entity.Property(e => e.MaxSpeedKnots).HasColumnName("max_speed_knots");
            entity.Property(e => e.GrossTonnage).HasColumnName("gross_tonnage");
            entity.Property(e => e.Deadweight).HasColumnName("deadweight");
            entity.Property(e => e.LengthMeters).HasColumnName("length_meters");
            entity.Property(e => e.BeamMeters).HasColumnName("beam_meters");
            entity.Property(e => e.DraftMeters).HasColumnName("draft_meters");
        });

        modelBuilder.Entity<ShipLocationLog>(entity =>
        {
            entity.ToTable("shiplocationlog");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.SpeedKnots).HasColumnName("speed_knots");
            entity.Property(e => e.RecordedAt).HasColumnName("recorded_at");
        });

        modelBuilder.Entity<ShipCompanyRelation>(entity =>
        {
            entity.ToTable("shipcompanyrelation");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(20).HasConversion<string>();
            entity.HasIndex(r => new { r.ShipId, r.CompanyId, r.Role }).IsUnique();
        });

        modelBuilder.Entity<VisitedCountry>(entity =>
        {
            entity.ToTable("visitedcountry");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.VisitedOn).HasColumnName("visited_on");
        });

        modelBuilder.Entity<Voyage>(entity =>
        {
            entity.ToTable("voyage", table =>
            {
                table.HasCheckConstraint("CK_Voyage_DepartureArrival", "[departure_port_id] <> [arrival_port_id]");
            });

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ShipId).HasColumnName("ship_id");
            entity.Property(e => e.VoyageDate).HasColumnName("voyage_date");
            entity.Property(e => e.DeparturePortId).HasColumnName("departure_port_id");
            entity.Property(e => e.ArrivalPortId).HasColumnName("arrival_port_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.EndTime).HasColumnName("end_time");

            entity.HasOne(v => v.DeparturePort)
                .WithMany(p => p.DepartingVoyages)
                .HasForeignKey(v => v.DeparturePortId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(v => v.ArrivalPort)
                .WithMany(p => p.ArrivingVoyages)
                .HasForeignKey(v => v.ArrivalPortId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
