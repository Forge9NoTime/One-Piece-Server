namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Data.Models;
    using One_Piece.Service.Interfaces;
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
                .Include(t => t.TeamType)
                .Include(t => t.Organizer)
                .ThenInclude(o => o.User)
                .FirstAsync(t => t.Id == teamId);

            return new TeamDetailsViewModel
            {
                Id = team.Id,
                Name = team.Name,
                TeamType = team.TeamType!.TypeName,
                Organizer = new OnePiece.Web.ViewModels.Organizer.OrganizerInfoOnMissionViewModel()
                {
                    Email = team.Organizer.User.Email,
                    PhoneNumber = team.Organizer.PhoneNumber
                }
            };
        }
    }
}
