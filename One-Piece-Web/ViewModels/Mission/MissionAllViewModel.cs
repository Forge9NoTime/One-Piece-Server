namespace OnePiece.Web.ViewModels.Mission
{
    using System.ComponentModel.DataAnnotations;
    using static OnePiece.Common.EntityValidationConstants.Mission;

    public class MissionAllViewModel
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(TitleMaxLenght, MinimumLength = TitleMinLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [Display(Name = "Description")]
        [StringLength(DescriptionMaxLenght, MinimumLength = DescriptionMinLenght)]
        public string Description { get; set; } = null!;

        [Required]
        [Display(Name = "Location")]
        [StringLength(LocationMaxLenght, MinimumLength = LocationMinLenght)]
        public string Location { get; set; } = null!;

    }
}
