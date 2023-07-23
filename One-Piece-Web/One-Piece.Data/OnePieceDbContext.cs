namespace One_Piece.Data
{
    using Microsoft.AspNetCore.Identity;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore;

    using Models;
    using One_Piece.Data.Models.Enums;

    public class OnePieceDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public OnePieceDbContext()
        {

        }

        public OnePieceDbContext(DbContextOptions<OnePieceDbContext> options) : base(options)
        {

        }

        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamType> TeamTypes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionType> MissionTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Volunteer>()
                .HasOne<Transport>(v => v.Transport)
                .WithOne(t => t.Volunteer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Volunteer>()
                .HasOne<Team>(v => v.Team)
                .WithMany(t => t.Volunteers)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Team>()
                .HasMany<Transport>(te => te.Transports)
                .WithOne(t => t.Team)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transport>()
                .HasOne<Volunteer>(tr => tr.Volunteer)
                .WithOne(v => v.Transport)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transport>()
                .HasOne<Team>(t => t.Team)
                .WithMany(te => te.Transports)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Mission>().HasData(this.GenerateMissions());

            //modelBuilder.Entity<MissionType>().HasData(this.GenerateMissionTypes());

        }
        private MissionType[] GenerateMissionTypes()
        {
            ICollection<MissionType> missionTypes = new HashSet<MissionType>();

            MissionType missionType;

            missionType = new MissionType()
            {
                Id = Guid.NewGuid(),
                TypeName = "Rescue"
            };
            missionTypes.Add(missionType);

            return missionTypes.ToArray();
        }

            private Mission[] GenerateMissions()
        {
            ICollection<Mission> missions = new HashSet<Mission>();

            Mission mission;

            mission = new Mission()
            {
                Id = Guid.NewGuid(),
                Title = "Fallen Tree",
                Location = "Grad Lom, ulica Bluzludja 3",
                ThreatLevel = ThreatType.Low,
                Description = "Padnalo durvo zaprechva putq",
                MissionTypeId = Guid.Parse("67725CB9-9A78-489B-A159-113BCEFCEE03"),
                OrganizerId = Guid.Parse("CADBC7BC-450A-44D8-AB10-3F110F51929D")
            };
            missions.Add(mission);

            mission = new Mission()
            {
                Id = Guid.NewGuid(),
                Title = "Flood",
                Location = "Grad Lom, ulica Vasil Kolarov",
                ThreatLevel = ThreatType.Medium,
                Description = "Ulicata e navodnena, ima mnogo otpaduci na putq",
                MissionTypeId = Guid.Parse("67725CB9-9A78-489B-A159-113BCEFCEE03"),
                OrganizerId = Guid.Parse("CADBC7BC-450A-44D8-AB10-3F110F51929D")
            };
            missions.Add(mission);

            mission = new Mission()
            {
                Id = Guid.NewGuid(),
                Title = "Strong Wind",
                Location = "Grad Lom, Tundja 10",
                ThreatLevel = ThreatType.Low,
                Description = "Padnalo durvo vurhu kola",
                MissionTypeId = Guid.Parse("67725CB9-9A78-489B-A159-113BCEFCEE03"),
                OrganizerId = Guid.Parse("CADBC7BC-450A-44D8-AB10-3F110F51929D")
            };
            missions.Add(mission);

            mission = new Mission()
            {
                Id = Guid.NewGuid(),
                Title = "Storm",
                Location = "Grad Lom, ulica Borunska 3",
                ThreatLevel = ThreatType.Low,
                Description = "Pochistvane na plaja sled burq",
                MissionTypeId = Guid.Parse("67725CB9-9A78-489B-A159-113BCEFCEE03"),
                OrganizerId = Guid.Parse("CADBC7BC-450A-44D8-AB10-3F110F51929D")
            };
            missions.Add(mission);

            mission = new Mission()
            {
                Id = Guid.NewGuid(),
                Title = "Disaster",
                Location = "Grad Lom, ulica Bluzludja 3",
                ThreatLevel = ThreatType.Low,
                Description = "Nqmam internet vtori den A1 ei mamka vi",
                MissionTypeId = Guid.Parse("67725CB9-9A78-489B-A159-113BCEFCEE03"),
                OrganizerId = Guid.Parse("CADBC7BC-450A-44D8-AB10-3F110F51929D")
            };
            missions.Add(mission);

            return missions.ToArray();
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
