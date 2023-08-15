namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.ThreatLevel;

    public interface IThreatLevelService
    {
        Task<IEnumerable<MissionSelectThreatLevelFormModel>> ALlThreatLevelsAsync();

        Task<bool> ExistsByIdAsync(int id);
    }
}
