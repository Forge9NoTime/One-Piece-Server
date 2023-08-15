namespace OnePiece.Web.ViewModels.Organizer
{
    using System.ComponentModel.DataAnnotations;

    public class OrganizerInfoOnHouseViewModel
    {
        public string Email { get; set; } = null!;

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
