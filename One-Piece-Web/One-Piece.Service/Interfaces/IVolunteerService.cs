namespace One_Piece.Service.Interfaces
{
    using OnePiece.Web.ViewModels.Volunteer;

    public interface IVolunteerService
    {
        Task<bool> VolunteerExistsByUserIdAsync(string userId);

        Task<bool> VolunteerExistsByPhoneNumberAsync(string phoneNumber);

        Task Create(string userId, BecomeVolunteerFormModel model);
    }
}
