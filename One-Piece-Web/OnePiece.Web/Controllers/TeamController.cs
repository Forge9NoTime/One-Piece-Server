namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TeamController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }
    }
}
