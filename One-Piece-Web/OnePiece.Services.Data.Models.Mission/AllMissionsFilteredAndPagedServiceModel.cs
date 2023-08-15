namespace OnePiece.Services.Data.Models.Mission
{
    using OnePiece.Web.ViewModels.Mission;

    public class AllMissionsFilteredAndPagedServiceModel
    {
        public AllMissionsFilteredAndPagedServiceModel()
        {
            this.Missions = new HashSet<MissionAllViewModel>();
        }
         public int TotalMissionsCount { get; set; }

        public IEnumerable<MissionAllViewModel> Missions { get; set; }
    }
}
