namespace One_Piece.Data.Models
{
    using One_Piece.Data.Contracts;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.ItemType;
    public class ItemType : AuditableEntity
    {
        [Required]
        [Range(NameMinLenght, NameMaxLenght)]
        public string TypeName { get; set; } = null!;

        public ICollection<Item>? Items;
    }
}

