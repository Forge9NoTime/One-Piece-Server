namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Transport;

    public class Transport : AuditableEntity
    {
        [Required]
        [Range(MinAvailableSpots, MaxAvailableSpots)]
        public int AvailableSpots { get; set; }

        [Required]
        public string LicensePlate { get; set; } = null!;

        [ForeignKey("Team")]
        public Guid? TeamId { get; set; }
        public Team? Team { get; set; }

        [ForeignKey("Volunteer")]
        public Guid? VolunteerId { get; set; }
        public Volunteer? Volunteer { get; set; }

    }
}
