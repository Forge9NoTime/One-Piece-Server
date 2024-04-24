namespace OnePiece.Services.Data.Models.Mission
{
    using OnePiece.Web.ViewModels.Team;

    public class AllTeams
    {
        public AllTeams()
        {
            this.Teams = new HashSet<TeamAllViewModel>();
        }

        public IEnumerable<TeamAllViewModel> Teams { get; set; }
    }
}
