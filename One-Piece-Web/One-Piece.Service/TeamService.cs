namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Data.Models;
    using One_Piece.Service.Interfaces;
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Team;
    using OnePiece.Web.ViewModels.Volunteer;

    public class TeamService : ITeamService
    {
        private readonly OnePieceDbContext dbContext;

        public TeamService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            bool result = await this.dbContext
                .Teams
                .AnyAsync(t => t.Id == id);

            return result;
        }
        public async Task<string> CreateAndReturnIdAsync(TeamFormModel formModel, string organizerId)
        {
            Team NewTeam = new Team()
            {
                Name = formModel.Name,
                MissionId = formModel.MissionId,
                OrganizerId = Guid.Parse(organizerId),
                TeamTypeId = formModel.TeamTypeId
            };

            await this.dbContext.Teams.AddAsync(NewTeam);
            await this.dbContext.SaveChangesAsync();

            return NewTeam.Id.ToString();
        }
        public async Task<TeamDetailsViewModel> GetDetailsByIdAsync(Guid teamId)
        {
            Team? team = await this.dbContext
                .Teams
                .Include(t => t.Volunteers)
                .Include(t => t.Mission)
                .Include(t => t.TeamType)
                .Include(t => t.Organizer)
                .ThenInclude(o => o.User)
                .FirstAsync(t => t.Id == teamId);

            return new TeamDetailsViewModel
            {
                Id = team.Id,
                Name = team.Name,
                MissionTitle = team.Mission.Title,
                MissionId = team.MissionId.ToString(),
                TeamType = team.TeamType!.TypeName,
                Organizer = new OnePiece.Web.ViewModels.Organizer.OrganizerInfoOnMissionViewModel()
                {
                    Email = team.Organizer.User.Email,
                    PhoneNumber = team.Organizer.PhoneNumber
                },
                Volunteers = team.Volunteers.Select(x => new VolunteerViewModel
                {
                    FullName = x.FullName
                }).ToList()
            };
        }
        public async Task<AllTeams> AllTeamsAsync(TeamsAllQueryModel queryModel)
        {

            ICollection<Team> teamsQuery = await this.dbContext
                .Teams
                .ToListAsync();


            return new AllTeams()
            {
                Teams = teamsQuery
                .Select(t => new TeamAllViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    MembersCount = t.MembersCount
                })
                .ToList()
            };
        }

        public async Task JoinTeamAsync(Guid teamId, Guid userId)
        {
            var team = await this.dbContext.Teams
                .Include(x => x.Volunteers)
                .FirstOrDefaultAsync(x => x.Id == teamId);
            var volunteer = await this.dbContext.Volunteers.FirstOrDefaultAsync(x => x.UserId == userId);

            if (team == null || volunteer == null)
            {
                //to do throw exception
                return;
            }

            team.Volunteers.Add(volunteer);
            this.dbContext.Teams.Update(team);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<TeamAllViewModel>> GetAllTeamsAsync()
        {
            return await this.dbContext.Teams
                .Include(x => x.Mission)
                .Select(x => new TeamAllViewModel
                {
                    Id = x.Id,
                    MissionTitle = x.Mission.Title,
                    MissionId = x.MissionId.ToString(),
                    Name = x.Name,
                    MembersCount = x.MembersCount
                })
                .ToListAsync();
        }

        public async Task<bool> isOrganizerWithIdCreatorOfTeamWithIdAsync(Guid teamId, Guid organizerId)
        {
            Team team = await this.dbContext
                .Teams
                .FirstAsync(t => t.Id == teamId);

            return team.OrganizerId == organizerId;
        }

        public async Task<TeamFormModel> GetTeamForEditByIdAsync(Guid teamId)
        {
            Team? team = await this.dbContext
                .Teams
                .Include(t => t.TeamType)
                .FirstAsync(t => t.Id == teamId);

            return new TeamFormModel
            {
                Name = team.Name,
                TeamTypeId = team.TeamTypeId,
                MissionId = team.MissionId,
            };
        }

        public async Task EditTeamByIdAndFormModelAsync(string teamId, TeamFormModel formModel)
        {
            Team team = await this.dbContext
                .Teams
                .FirstAsync(t => t.Id.ToString() == teamId);

            team.Name = formModel.Name;
            team.TeamTypeId = formModel.TeamTypeId;
            team.MissionId = formModel.MissionId;


            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteTeamByIdAsync(Guid teamId)
        {
            Team? team = await this.dbContext
                .Teams
                .FirstOrDefaultAsync(t => t.Id == teamId);

            if (team == null)
            {
                return;
            }

            this.dbContext.Teams.Remove(team);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<TeamPreDeleteDetailsViewModel> GetTeamForDeleteByIdAsync(Guid teamId)
        {
            var team = await this.dbContext
                 .Teams
                 .Include(t => t.Mission)
                 .Include(t => t.TeamType)
                 .Select(t => new TeamPreDeleteDetailsViewModel
                 {
                     Id = t.Id,
                     Name = t.Name,
                     MissionName = t.Mission.Title,
                     TeamType = t.TeamType.TypeName
                 })
                 .FirstOrDefaultAsync(t => t.Id == teamId);

            return team;
        }
    }    
} 
