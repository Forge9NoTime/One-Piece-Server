namespace OnePiece.Web.ViewModels.Team
{
    using OnePiece.Web.ViewModels.Mission.Enums;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.GeneralApplicationConstants;

    public class TeamsAllQueryModel
    {
        public TeamsAllQueryModel()
        {
            this.TeamTypes = new HashSet<string>();
            this.Teams = new HashSet<TeamAllViewModel>();
        }

        [Display(Name = "Team Type")]
        public string? TeamType { get; set; }

        public IEnumerable<string> TeamTypes { get; set; }

        public IEnumerable<TeamAllViewModel> Teams { get; set; }
    }
}
