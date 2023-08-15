namespace OnePiece.Web.ViewModels.Mission
{
    using OnePiece.Web.ViewModels.Mission.Enums;
    using System.ComponentModel.DataAnnotations;

    using static OnePiece.Common.GeneralApplicationConstants;

    public class AllMissionsQueryModel
    {
        public AllMissionsQueryModel()
        {
            this.CurrentPage = DefaultPage;
            this.MissionsPerPage = EntitiesPerPage;

            this.MissionTypes = new HashSet<string>();
            this.Missions = new HashSet<MissionAllViewModel>();
        }

        [Display(Name = "Mission Type")]
        public string? MissionType { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchString { get; set; }

        [Display(Name = "Sort By")]
        public MissionSorting MissionSorting { get; set; }

        public int CurrentPage { get; set; }

        public int MissionsPerPage { get; set; }

        public int TotalMissionsCount { get; set; }

        public IEnumerable<string> MissionTypes { get; set; }

        public IEnumerable<MissionAllViewModel> Missions { get; set; }
             
    }
}
