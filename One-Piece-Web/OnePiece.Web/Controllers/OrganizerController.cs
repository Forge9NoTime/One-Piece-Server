namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.Infrastructure.Extentions;
    using OnePiece.Web.ViewModels.Organizer;

    [Authorize]
    public class OrganizerController : Controller
    {
        private readonly IOrganizerService organizerService;
        public OrganizerController(IOrganizerService organizerService)
        {
            this.organizerService = organizerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isOrganizer = await this.organizerService.OrganizerExistsByUserIdAsync(userId);
            if (isOrganizer)
            {
                return this.BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeOrganizerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isOrganizer = await this.organizerService.OrganizerExistsByUserIdAsync(userId);
            if (isOrganizer)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.organizerService.Create(userId, model);

            return this.RedirectToAction("All", "Mission");
        }
    }
}
