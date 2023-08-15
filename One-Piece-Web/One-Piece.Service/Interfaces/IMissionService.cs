namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.Home;
    using OnePiece.Web.ViewModels.Mission;

    public interface IMissionService
    {
        Task<IEnumerable<IndexViewModel>> AllMissionsAsync();

        Task CreateAsync(MissionFormModel formModel, string organizerId);
    }
}
