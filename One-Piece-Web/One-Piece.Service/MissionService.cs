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
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Mission.Enums;

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

        public async Task<AllMissionsFilteredAndPagedServiceModel> AllAsync(AllMissionsQueryModel queryModel)
        {
            IQueryable<Mission> missionsQuery = this.dbContext
                .Missions
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.MissionType))
            {
                missionsQuery = missionsQuery
                    .Where(m => m.MissionType.TypeName == queryModel.MissionType);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCard = $"%{queryModel.SearchString.ToLower()}%";

                missionsQuery = missionsQuery
                    .Where(m => EF.Functions.Like(m.Title, wildCard) ||
                    EF.Functions.Like(m.Location, wildCard) ||
                    EF.Functions.Like(m.Description, wildCard));
            }

            missionsQuery = queryModel.MissionSorting switch
            {
                MissionSorting.Newest => missionsQuery
                .OrderByDescending(m => m.CreatedOn),
                MissionSorting.Oldest => missionsQuery
                .OrderBy(m => m.CreatedOn),
                MissionSorting.ThreatLevelAscending => missionsQuery
                .OrderBy(m => m.MissionThreatLevelId),
                MissionSorting.ThreatLevelDescending => missionsQuery
                .OrderByDescending(m => m.MissionThreatLevelId),
                _ => missionsQuery
                .OrderByDescending(m => m.CreatedOn)
            };

            IEnumerable<MissionAllViewModel> allMissions = await missionsQuery
                .Skip((queryModel.CurrentPage - 1) * queryModel.MissionsPerPage)
                .Take(queryModel.MissionsPerPage)
                .Select(m => new MissionAllViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Location = m.Location,
                    Description = m.Description,
                })
                .ToArrayAsync();
            int totalMissions = missionsQuery.Count();

            return new AllMissionsFilteredAndPagedServiceModel()
            {
                TotalMissionsCount = totalMissions,
                Missions = allMissions
            };
        }
    }
}
