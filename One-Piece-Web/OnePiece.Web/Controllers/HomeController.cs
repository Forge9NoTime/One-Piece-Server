namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using System.Diagnostics;

    using ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IMissionService missionService;

        public HomeController(IMissionService missionService)
        {
            this.missionService = missionService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<IndexViewModel> viewModel =
                await this.missionService.AllMissionsAsync();

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}