namespace OnePiece.Web.ViewModels.Team
{
    using OnePiece.Web.ViewModels.TeamType;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class TeamFormModel
    {
        public TeamFormModel()
        {
            this.TeamTypes = new HashSet<TeamSelectTypeFormModel>();
        }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; } = null!;

        [Display(Name = "Team Type")]
        public Guid TeamTypeId { get; set; }
        public IEnumerable<TeamSelectTypeFormModel> TeamTypes { get; set; }
    }
}
