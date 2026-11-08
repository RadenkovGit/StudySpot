using Microsoft.EntityFrameworkCore;
using StudySpot.Data.Entities;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.Data;

public class StudySpotDbContext : DbContext
{
    public StudySpotDbContext(DbContextOptions<StudySpotDbContext> options)
        : base(options)
    {
    }

    public DbSet<PlaceType> PlaceTypes { get; set; } = null!;

    public DbSet<StudyPlace> StudyPlaces { get; set; } = null!;

    public DbSet<StudySession> StudySessions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PlaceType>()
            .HasIndex(t => t.Name)
            .IsUnique();

        builder.Entity<StudyPlace>()
            .HasOne(p => p.PlaceType)
            .WithMany(t => t.StudyPlaces)
            .HasForeignKey(p => p.PlaceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<StudySession>(entity =>
        {
            entity.HasOne(s => s.StudyPlace)
                .WithMany(p => p.StudySessions)
                .HasForeignKey(s => s.StudyPlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable(t => t.HasCheckConstraint(
                "CK_StudySessions_DurationMinutes",
                $"[DurationMinutes] BETWEEN {SessionMinDurationMinutes} AND {SessionMaxDurationMinutes}"));
        });
    }
}
