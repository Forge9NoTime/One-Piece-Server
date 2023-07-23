namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class Team : AuditableEntity
    {
        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        public int MembersCount { get; set; }

        [ForeignKey("TeamType")]
        public Guid TeamTypeId { get; set; }
        public TeamType? TeamType { get; set; }

        [ForeignKey("Mission")]
        public Guid MissionId { get; set; }
        public Mission? Mission { get; set; }

        public ICollection<Volunteer>? Volunteers { get; set; }

        public ICollection<Transport>? Transports { get; set; }

    }
}
