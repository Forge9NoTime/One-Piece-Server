namespace One_Piece.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

    using One_Piece.Data.Contracts;

    using static OnePiece.Common.EntityValidationConstants.Order;

    public class Order : AuditableEntity
    {
        [Required]
        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; }

        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        [ForeignKey("Mission")]
        public Guid MissionId { get; set; }
        public Mission? Mission { get; set; }
    }
}
 