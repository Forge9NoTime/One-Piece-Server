namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Interfaces;
    using One_Piece.Data;
    using OnePiece.Web.ViewModels.Home;
    using OnePiece.Web.ViewModels.Mission;
    using One_Piece.Data.Models;

    public class MissionService : IMissionService
    {

        private readonly OnePieceDbContext dbContext;

        public MissionService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> AllMissionsAsync()
        {
            IEnumerable<IndexViewModel> allMissions = await this.dbContext
                .Missions
                .OrderByDescending(m => m.CreatedOn)
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Location = m.Location,
                    MissionThreatLevel = m.MissionThreatLevel.ToString(),
                    MissionType = m.MissionType.ToString(),
                    Description = m.Description
                })
                .ToArrayAsync();

            return allMissions;
        }

        public async Task CreateAsync(MissionFormModel formModel, string organizerId)
        {
            Mission newMission = new Mission()
            {
                Title = formModel.Title,
                Location = formModel.Location,
                Description = formModel.Description,
                MissionThreatLevelId = formModel.MissionThreatLevelId,
                OrganizerId = Guid.Parse(organizerId),
                MissionTypeId = formModel.MissionTypeId
            };

            await this.dbContext.Missions.AddAsync(newMission);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
