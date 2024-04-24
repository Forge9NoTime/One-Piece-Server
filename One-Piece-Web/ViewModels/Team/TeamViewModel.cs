namespace OnePiece.Web.ViewModels.Team
{
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.EntityValidationConstants.Team;

    public class TeamViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int MembersCount { get; set; }
        public Guid TeamTypeId { get; set; }
        public string TeamType { get; set; } = null!;
    }
}
