namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.ThreatLevel;

    public class MissionThreatLevel
    {
        public MissionThreatLevel()
        {
            Missions = new HashSet<Mission>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
