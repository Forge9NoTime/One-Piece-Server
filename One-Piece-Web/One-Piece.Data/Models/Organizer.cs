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

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
    }
}
