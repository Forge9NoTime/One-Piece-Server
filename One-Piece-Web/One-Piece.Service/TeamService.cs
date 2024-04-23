namespace One_Piece.Service
{
    using One_Piece.Data;
    using One_Piece.Service.Interfaces;

    public class TeamService : ITeamService
    {
        private readonly OnePieceDbContext dbContext;

        public TeamService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
