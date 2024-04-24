namespace OnePiece.Web.ViewModels.Team
{
    using OnePiece.Web.ViewModels.Organizer;

    public class TeamDetailsViewModel : TeamAllViewModel
    {
        public string TeamType { get; set; } = null!;

        public OrganizerInfoOnMissionViewModel Organizer { get; set; } = null!;
    }
}
