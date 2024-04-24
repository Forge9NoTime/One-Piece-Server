namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.TeamType;

    public interface ITeamTypeService
    {
        Task<IEnumerable<TeamSelectTypeFormModel>> AllTeamTypesAsync();

        Task<bool> ExistsByIdAsync(Guid id);

        Task<IEnumerable<string>> AllTeamTypeNamesAsync();
    }
}

