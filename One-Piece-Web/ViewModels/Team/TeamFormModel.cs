namespace OnePiece.Web.ViewModels.Team
{
    using OnePiece.Web.ViewModels.Home;
    using OnePiece.Web.ViewModels.Mission;
    using OnePiece.Web.ViewModels.TeamType;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class TeamFormModel
    {
        public TeamFormModel()
        {
            this.TeamTypes = new HashSet<TeamSelectTypeFormModel>();
            this.Missions = new HashSet<IndexViewModel>();
        }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; } = null!;

        [Display(Name = "Team Type")]
        public Guid TeamTypeId { get; set; }

        [Display(Name = "Mission")]
        public Guid MissionId { get; set; }
        public IEnumerable<TeamSelectTypeFormModel> TeamTypes { get; set; }

        public IEnumerable<IndexViewModel> Missions { get; set; }
    }
}
