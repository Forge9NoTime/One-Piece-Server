namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using One_Piece.Service.Interfaces;
    using OnePiece.Web.Infrastructure.Extentions;
    using OnePiece.Web.ViewModels.Volunteer;

    [Authorize]
    public class VolunteerController : Controller
    {
        private readonly IVolunteerService volunteerService;
        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(userId);
            if (isVolunteer)
            {
                return this.BadRequest();
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeVolunteerFormModel model)
        {
            string? userId = this.User.GetId();
            bool isVolunteer = await this.volunteerService.VolunteerExistsByUserIdAsync(userId);
            if (isVolunteer)
            {
                return this.BadRequest();
            }

            bool isPhoneNumberTaken =
                await this.volunteerService.VolunteerExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "A volunteer with the provided phone number already exists!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.volunteerService.Create(userId, model);

            return this.RedirectToAction("All", "Missions");
        }
    }
}
