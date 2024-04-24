namespace OnePiece.Web.ViewModels.Team
{
    public class JoinTeamFormModel
    {
        public JoinTeamFormModel()
        {
            this.Teams = new HashSet<TeamAllViewModel>();
        }
        public string TeamId { get; set; }
        public IEnumerable<TeamAllViewModel> Teams { get; set; }
    }
}
