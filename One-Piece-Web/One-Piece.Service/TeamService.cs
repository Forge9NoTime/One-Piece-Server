namespace One_Piece.Service
{
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
    }
}
