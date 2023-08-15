namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.MissionType;

    public interface IMissionTypeService
    {
        Task<IEnumerable<MissionSelectTypeFormModel>> ALlMissionTypesAsync();

        Task<bool> ExistsByIdAsync(Guid id);
    }
}
