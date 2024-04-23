namespace One_Piece.Data.Seeder
{
    using One_Piece.Data.Models;

    public class Seeder
    {
        public async Task SeedDatabase(OnePieceDbContext dbContext)
        {
            if (!dbContext.MissionTypes.Any())
            {
                ICollection<MissionType> missionTypes = new HashSet<MissionType>()
                {

                  new MissionType()
                  {
                      Id = Guid.NewGuid(),
                      TypeName = "Rescue"
                  },
                  new MissionType()
                  {
                      Id = Guid.NewGuid(),
                      TypeName = "Clean Up"
                  }

                };
                await dbContext.MissionTypes.AddRangeAsync(missionTypes);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.TeamTypes.Any())
            {
                ICollection<TeamType> teamTypes = new HashSet<TeamType>()
                {

                  new TeamType()
                  {
                      Id = Guid.NewGuid(),
                      TypeName = "Rescue"
                  },
                  new TeamType()
                  {
                      Id = Guid.NewGuid(),
                      TypeName = "Clean Up"
                  }

                };
                await dbContext.TeamTypes.AddRangeAsync(teamTypes);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.MissionThreatLevels.Any())
            {
                ICollection<MissionThreatLevel> missionThreatLevels = new HashSet<MissionThreatLevel>()
                {

                    new MissionThreatLevel()
                    {
                        Name = "Low"
                    },
                    new MissionThreatLevel()
                    {
                        Name = "Medium"
                    },
                    new MissionThreatLevel()
                    {
                        Name = "High"
                    },
                    new MissionThreatLevel()
                    {
                        Name = "Critical"
                    },

                };
                await dbContext.MissionThreatLevels.AddRangeAsync(missionThreatLevels);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}
