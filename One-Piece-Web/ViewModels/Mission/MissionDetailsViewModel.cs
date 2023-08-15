namespace OnePiece.Web.ViewModels.Mission
{
    using OnePiece.Web.ViewModels.Organizer;

    public class MissionDetailsViewModel : MissionAllViewModel
    {
        public string MissionType { get; set; } = null!;

        public OrganizerInfoOnHouseViewModel Organizer { get; set; } = null!;
    }
}
