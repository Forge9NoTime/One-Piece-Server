namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Item;

    public class Item : AuditableEntity
    {
        [Required]
        [MaxLength(ItemMaxLenght)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }

        [ForeignKey("ItemType")]
        public Guid ItemTypeId { get; set; }
        public ItemType? ItemType { get; set; }
    }
}
