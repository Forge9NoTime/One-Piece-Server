namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.ViewModels.Mission;

    [Authorize]
    public class MissionController : Controller
    {
        private readonly IMissionService missionService;

        public MissionController(IMissionService missionService)
        {
            this.missionService = missionService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            MissionFormModel formModel = new MissionFormModel()
            {

            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MissionFormModel model)
        {
            MissionFormModel formModel = new MissionFormModel()
            {

            };

            if (!ModelState.IsValid)
            {

            }

            return View(formModel);
        }
    }
}
