namespace OnePiece.Web.ViewModels.Volunteer
{
    using System.ComponentModel.DataAnnotations;

    using static Common.EntityValidationConstants.Volunteer;

    public class BecomeVolunteerFormModel
    {
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(FullNameMaxLenght, MinimumLength = FullNameMinLenght)]
        public string FullName { get; set; } = null!;

        [Phone]
        [Required]
        [Display(Name = "Phone")]
        [StringLength(MaxPhoneNumberLenght, MinimumLength = MinPhoneNumberLenght)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; } = null!;

        [Required]
        [Range(MinAge, MaxAge)]
        [Display(Name = "Age")]
        public int Age { get; set; }

    }
}
