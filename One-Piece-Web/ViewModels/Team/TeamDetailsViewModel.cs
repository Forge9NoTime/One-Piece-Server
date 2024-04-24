namespace OnePiece.Web.ViewModels.Team
{
    using OnePiece.Web.ViewModels.Organizer;
    using OnePiece.Web.ViewModels.Volunteer;

    public class TeamDetailsViewModel : TeamAllViewModel
    {
        public string TeamType { get; set; } = null!;

        public OrganizerInfoOnMissionViewModel Organizer { get; set; } = null!;

        public ICollection<VolunteerViewModel> Volunteers { get; set; }
    }
}
