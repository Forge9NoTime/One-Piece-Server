namespace OnePiece.Web.ViewModels.Team
{
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class TeamAllViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Team Name")]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; } = null!;

        public int MembersCount { get; set; }
    }
}
