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
    using System.ComponentModel.DataAnnotations;
    using One_Piece.Service.Statistics;

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

        public async Task<string> CreateAndReturnIdAsync(MissionFormModel formModel, string organizerId)
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

            return newMission.Id.ToString();
        }

        public async Task<AllMissionsFilteredAndPagedServiceModel> AllAsync(AllMissionsQueryModel queryModel)
        {
            //IQueryable<Mission> missionsQuery = this.dbContext
            //   .Missions
            //   .AsQueryable();

            ICollection<Mission> missionsQuery = await this.dbContext
                .Missions
                .ToListAsync();


            return new AllMissionsFilteredAndPagedServiceModel()
            {
                TotalMissionsCount = missionsQuery.Count,
                Missions = missionsQuery
                .Select(m => new MissionAllViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Location = m.Location,
                    Description = m.Description,
                })
                .ToList()
            };

            //if (!string.IsNullOrWhiteSpace(queryModel.MissionType))
            //{
            //    missionsQuery = missionsQuery
            //        .Where(m => m.MissionType.TypeName == queryModel.MissionType);
            //}

            //if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            //{
            //    string wildCard = $"%{queryModel.SearchString.ToLower()}%";

            //    missionsQuery = missionsQuery
            //        .Where(m => EF.Functions.Like(m.Title, wildCard) ||
            //        EF.Functions.Like(m.Location, wildCard) ||
            //        EF.Functions.Like(m.Description, wildCard));
            //}

            //missionsQuery = queryModel.MissionSorting switch
            //{
            //    MissionSorting.Newest => missionsQuery
            //    .OrderByDescending(m => m.CreatedOn),
            //    MissionSorting.Oldest => missionsQuery
            //    .OrderBy(m => m.CreatedOn),
            //    MissionSorting.ThreatLevelAscending => missionsQuery
            //    .OrderBy(m => m.MissionThreatLevelId),
            //    MissionSorting.ThreatLevelDescending => missionsQuery
            //    .OrderByDescending(m => m.MissionThreatLevelId),
            //    _ => missionsQuery
            //    .OrderByDescending(m => m.CreatedOn)
            //};

            //IEnumerable<MissionAllViewModel> allMissions = await missionsQuery
            //    .Skip((queryModel.CurrentPage - 1) * queryModel.MissionsPerPage)
            //    .Take(queryModel.MissionsPerPage)
            //    .Select(m => new MissionAllViewModel
            //    {
            //        Id = m.Id,
            //        Title = m.Title,
            //        Location = m.Location,
            //        Description = m.Description,
            //    })
            //    .ToArrayAsync();
            //int totalMissions = missionsQuery.Count();
        }

        public async Task<IEnumerable<MissionAllViewModel>> AllByOrganizerIdAsync(string organizerId)
        {
            IEnumerable<MissionAllViewModel> allOrganizerMissions = await this.dbContext
                .Missions
                .Where(m => m.OrganizerId.ToString() == organizerId)
                .Select(m => new MissionAllViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Location = m.Location,
                    Description = m.Description,
                })
                .ToArrayAsync();

            return allOrganizerMissions;
        }

        public async Task<MissionDetailsViewModel> GetDetailsByIdAsync(Guid missionId)
        {
            Mission? mission = await this.dbContext
                .Missions
                .Include(m => m.MissionType)
                .Include(m => m.Organizer)
                .ThenInclude(o => o.User)
                .FirstAsync(m => m.Id == missionId);

            return new MissionDetailsViewModel
            {
               Id = mission.Id,
               Title = mission.Title,
               Description = mission.Description,
               Location = mission.Location,
               MissionType = mission.MissionType.TypeName,
               Organizer = new OnePiece.Web.ViewModels.Organizer.OrganizerInfoOnMissionViewModel()
               {
                   Email = mission.Organizer.User.Email,
                   PhoneNumber = mission.Organizer.PhoneNumber
               }

            };
        }

        public async Task<bool> ExistsByIdAsync(Guid missionId)
        {
            bool result = await this.dbContext
                .Missions
                .AnyAsync(m => m.Id == missionId);

            return result;
        }

        public async Task<MissionFormModel> GetMissionForEditByIdAsync(Guid missionId)
        {
            Mission? mission = await this.dbContext
                .Missions
                .Include(m => m.MissionType)
                .FirstAsync(m => m.Id == missionId);

            return new MissionFormModel
            {
                Title = mission.Title,
                Location = mission.Location,
                Description = mission.Description,
                MissionThreatLevelId = mission.MissionThreatLevelId,
                MissionTypeId = mission.MissionTypeId
            };
        }

        public async Task<bool> isOrganizerWithIdCreatorOfMissionWithIdAsync(Guid missionId, Guid organizerId)
        {
            Mission mission = await this.dbContext
                .Missions
                .FirstAsync(m => m.Id == missionId);

            return mission.OrganizerId == organizerId;
        }

        public async Task EditMissionByIdAndFormModelAsync(string missionId, MissionFormModel formModel)
        {
            Mission mission = await this.dbContext
                .Missions
                .FirstAsync(m => m.Id.ToString() == missionId);

            mission.Title = formModel.Title;
            mission.Location = formModel.Location;
            mission.Description = formModel.Description;
            mission.MissionThreatLevelId = formModel.MissionThreatLevelId;
            mission.MissionTypeId = formModel.MissionTypeId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<MissionPreDeleteDetailsViewModel> GetMissionForDeleteByIdAsync(string missionId)
        {
            Mission mission = await this.dbContext
                .Missions
                .FirstAsync(m => m.Id.ToString() == missionId);

            return new MissionPreDeleteDetailsViewModel
            {
                Title = mission.Title,
                Location = mission.Location
            };
        }

        public async Task DeleteMissionByIdAsync(Guid missionId)
        {
            Mission missionToDelete = await this.dbContext
                .Missions
                .FirstAsync(m => m.Id == missionId);

            dbContext.Remove(missionToDelete);
                
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel()
            {
                TotalMissions = await this.dbContext.Missions.CountAsync(),
                TotalTeams = await this.dbContext.Teams.CountAsync(),
                HighThreatMissions = await this.dbContext.Missions
                .Where(m => m.MissionThreatLevelId == 4)
                .CountAsync()
            };
        }
    }
}
