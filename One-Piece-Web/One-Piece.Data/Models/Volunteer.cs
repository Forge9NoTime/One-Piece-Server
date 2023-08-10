namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Volunteer;

    public class Volunteer : AuditableEntity
    {
        [Required]
        [MaxLength(FullNameMaxLenght)]
        public string FullName { get; set; } = null!;

        [Required]
        [Range(MinPhoneNumberLenght, MaxPhoneNumberLenght)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        [Range(MinAge, MaxAge)]
        public int Age { get; set; }

        [ForeignKey("Team")]
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        public Guid? TransportId { get; set; }
        public Transport? Transport { get; set; }

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
