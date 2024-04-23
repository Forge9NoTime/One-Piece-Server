namespace OnePiece.Web.ViewModels.Organizer
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Organizer;

    public class BecomeOrganizerFormModel
    {
        [Required]
        [Display(Name = "ЕГН")]
        [StringLength(ExactEGNLenght)]
        public string EGN { get; set; } = null!;
        [Required]
        [Display(Name = "Place Of Residence")]
        [StringLength(PlaceOfResidencyMaxLenght, MinimumLength = PlaceOfResidencyMinLenght)]
        public string PlaceOfResidence { get; set; } = null!;
        [Required]
        [Display(Name = "Affiliated Organization")]
        [StringLength(AffiliatedOrganizationMaxLenght, MinimumLength = AffiliatedOrganizationMinLenght)]
        public string AffiliatedOrganization { get; set; } = null!;
    }   
}
