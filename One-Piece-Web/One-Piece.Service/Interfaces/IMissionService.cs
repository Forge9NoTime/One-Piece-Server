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

        Task<bool> ExistsByIdAsync(Guid missionId);

        Task<MissionDetailsViewModel> GetDetailsByIdAsync(Guid missionId);

        Task<MissionFormModel> GetMissionForEditByIdAsync(Guid missionId);

        Task<bool> isOrganizerWithIdCreatorOfMissionWithIdAsync(Guid missionId, Guid organizerId);

        Task EditMissionByIdAndFormModel(string missionId, MissionFormModel formModel);
    }
}
