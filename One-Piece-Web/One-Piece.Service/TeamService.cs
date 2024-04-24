namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Data.Models;
    using One_Piece.Service.Interfaces;
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Team;

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
                }
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
    }    
} 
