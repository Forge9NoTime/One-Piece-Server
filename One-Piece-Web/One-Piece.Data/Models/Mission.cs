﻿namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Mission;

    public class Mission : AuditableEntity
    {
        [Required]
        [Range(TitleMinLenght, TitleMaxLenght)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(LocationMaxLenght)]
        public string Location { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; } = null!;

        [ForeignKey("MissionThreatLevel")]
        public int MissionThreatLevelId { get; set; }
        public MissionThreatLevel MissionThreatLevel { get; set; } = null!;

        [ForeignKey("MissionType")]
        public Guid MissionTypeId { get; set; }
        public MissionType MissionType { get; set; } = null!;

        [ForeignKey("Organizer")]
        public Guid OrganizerId { get; set; }
        public Organizer Organizer { get; set; } = null!;

        public ICollection<Team>? Teams { get; set; }

    }
}
