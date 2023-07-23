namespace One_Piece.Data.Models
{
    using One_Piece.Data.Contracts;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.TeamType;
    public class TeamType : AuditableEntity
    {
        [Required]
        [Range(NameMinLenght, NameMaxLenght)]
        public string TypeName { get; set; } = null!;

        public ICollection<Team>? Teams;
    }
}
