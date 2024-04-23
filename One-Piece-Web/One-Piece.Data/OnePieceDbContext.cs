namespace One_Piece.Data
{
    using Microsoft.AspNetCore.Identity;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore;

    using Models;

    public class OnePieceDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public OnePieceDbContext()
        {

        }

        public OnePieceDbContext(DbContextOptions<OnePieceDbContext> options) : base(options)
        {

        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionThreatLevel> MissionThreatLevels { get; set; }
        public DbSet<MissionType> MissionTypes { get; set; }
        public DbSet<Organizer> Organizers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Volunteer>()
                .HasOne<Team>(v => v.Team)
                .WithMany(t => t.Volunteers)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MissionType>().HasData(this.GenerateMissionTypes());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=OnePiece;Integrated Security=True;Encrypt=False;TrustServerCertificate=true");
            }
        }
    }
}
