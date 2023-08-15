namespace One_Piece.Service.Interfaces
{
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Home;
    using OnePiece.Web.ViewModels.Mission;

    public interface IMissionService
    {
        Task<IEnumerable<IndexViewModel>> AllMissionsAsync();

        Task CreateAsync(MissionFormModel formModel, string organizerId);

        Task<AllMissionsFilteredAndPagedServiceModel> AllAsync(AllMissionsQueryModel queryModel);

        Task<IEnumerable<MissionAllViewModel>> AllByOrganizerIdAsync(string organizerId);

        Task<MissionDetailsViewModel> GetDetailsByIdAsync(Guid missionId);
    }
}
