namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Organizer;

    public class Organizer : AuditableEntity
    {
        [Required]
        [Range(MinPhoneNumberLenght, MaxPhoneNumberLenght)]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        [StringLength(ExactEGNLenght)]
        public string EGN { get; set; } = null!;
        [Required]
        [Range(PlaceOfResidencyMinLenght, PlaceOfResidencyMaxLenght)]
        public string PlaceOfResidence { get; set; } = null!;
        [Required]
        [Range(AffiliatedOrganizationMinLenght, AffiliatedOrganizationMaxLenght)]
        public string AffiliatedOrganization { get; set; } = null!;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public ICollection<Mission>? CreatedMissions { get; set; } = null!;

        public ICollection<Team>? CreatedTeams { get; set; } = null!;


    }
}
