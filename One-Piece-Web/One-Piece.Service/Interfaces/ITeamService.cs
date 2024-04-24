namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.Team;

    public interface ITeamService
    {
        Task<string> CreateAndReturnIdAsync(TeamFormModel formModel, string organizerId);
    }
}
