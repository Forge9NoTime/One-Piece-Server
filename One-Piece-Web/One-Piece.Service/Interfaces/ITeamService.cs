namespace One_Piece.Service.Interfaces
{
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Team;

    public interface ITeamService
    {

        Task<bool> ExistsByIdAsync(Guid id);

        Task<TeamDetailsViewModel> GetDetailsByIdAsync(Guid teamId);

        Task<string> CreateAndReturnIdAsync(TeamFormModel formModel, string organizerId);

        Task<bool> isOrganizerWithIdCreatorOfTeamWithIdAsync(Guid teamId, Guid organizerId);

        Task<AllTeams> AllTeamsAsync(TeamsAllQueryModel queryModel);

        Task<ICollection<TeamAllViewModel>> GetAllTeamsAsync();

        Task JoinTeamAsync(Guid teamId, Guid userId);

        Task<TeamFormModel> GetTeamForEditByIdAsync(Guid teamId);

        Task EditTeamByIdAndFormModelAsync(string teamId, TeamFormModel formModel);
    }
}
