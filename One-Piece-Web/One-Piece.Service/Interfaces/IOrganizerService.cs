namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.Organizer;

    public interface IOrganizerService
    {
        Task<string> GetOrganizerIdByUserIdAsync(string userId);

        Task<bool> OrganizerExistsByUserIdAsync(string userId);

        Task<bool> HasMissionWithIdAsync(string userId, string missionId);

        Task<bool> HasTeamWithIdAsync(string userId, string teamId);

        Task Create(string userId, BecomeOrganizerFormModel model);
    }
}
