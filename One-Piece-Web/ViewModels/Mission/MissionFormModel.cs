namespace OnePiece.Web.ViewModels.Mission
{
    using OnePiece.Web.ViewModels.MissionType;
    using OnePiece.Web.ViewModels.ThreatLevel;
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Mission;

    public class MissionFormModel
    {
        public MissionFormModel()
        {
            this.ThreatLevels = new HashSet<MissionSelectThreatLevelFormModel>();
            this.MissionTypes = new HashSet<MissionSelectTypeFormModel>();
        }

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

        [Display(Name = "Threat Level")]
        public int MissionThreatLevelId { get; set; }
        public IEnumerable<MissionSelectThreatLevelFormModel> ThreatLevels { get; set; }

        [Display(Name = "Mission Type")]
        public Guid MissionTypeId { get; set; }
        public IEnumerable<MissionSelectTypeFormModel> MissionTypes { get; set; }
    }
}
