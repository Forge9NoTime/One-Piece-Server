namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class Team : AuditableEntity
    {
        [Required]
        [StringLength(NameMaxLenght, MinimumLength = NameMinLenght)]
        public string Name { get; set; } = null!;

        [Required]
        public int MembersCount { get; set; }

        [ForeignKey("TeamType")]
        public Guid TeamTypeId { get; set; }
        public TeamType? TeamType { get; set; }

        [ForeignKey("Mission")]
        public Guid? MissionId { get; set; }
        public Mission? Mission { get; set; }

        [ForeignKey("Organizer")]
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;

        public ICollection<Volunteer>? Volunteers { get; set; }

    }
}
