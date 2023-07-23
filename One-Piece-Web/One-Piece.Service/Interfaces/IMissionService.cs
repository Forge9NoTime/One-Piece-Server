namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.Home;

    public interface IMissionService
    {
        Task<IEnumerable<IndexViewModel>> LastFiveMissionsAsync();
    }
}
