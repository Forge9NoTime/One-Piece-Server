namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.Infrastructure.Extentions;
    using OnePiece.Web.ViewModels.Mission;
    using static OnePiece.Common.NotificationMessagesConstants;

    [Authorize]
    public class MissionController : Controller
    {
        private readonly IMissionService missionService;
        private readonly IThreatLevelService threatLevelService;
        private readonly IVolunteerService volunteerService;
        private readonly IMissionTypeService missionTypeService;
        private readonly IOrganizerService organizerService;

        public MissionController(IThreatLevelService threatLevelService, IMissionService missionService
            , IVolunteerService volunteerService, IMissionTypeService missionTypeService, IOrganizerService organizerService)
        {
            this.threatLevelService = threatLevelService;
            this.missionService = missionService;
            this.volunteerService = volunteerService;
            this.missionTypeService = missionTypeService;
            this.organizerService = organizerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery]AllMissionsQueryModel queryModel)
        {
            AllMissionsFilteredAndPagedServiceModel serviceModel =
                await this.missionService.AllAsync(queryModel);


            queryModel.Missions = serviceModel.Missions;
            queryModel.TotalMissionsCount = serviceModel.TotalMissionsCount;
            queryModel.MissionTypes = await this.missionTypeService.AllMissionTypeNamesAsync();
            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isVolunteer =
                await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer)
            {
                this.TempData[ErrorMessage] = "You must become an organizer in order to add missions!";

                return this.RedirectToAction("Become", "Volunteer");
            }


            MissionFormModel formModel = new MissionFormModel()
            {
                ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync(),
                MissionTypes = await this.missionTypeService.ALlMissionTypesAsync()
            };

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(MissionFormModel model)
        {
            bool isVolunteer =
                await this.volunteerService.VolunteerExistsByUserIdAsync(this.User.GetId()!);
            if (!isVolunteer)
            {
                this.TempData[ErrorMessage] = "You must become an organizer in order to add missions!";

                return this.RedirectToAction("Become", "Volunteer");
            }

            bool threatLevelExists =
                await this.threatLevelService.ExistsByIdAsync(model.MissionThreatLevelId);

            if (!threatLevelExists)
            {
                ModelState.AddModelError(nameof(model.MissionThreatLevelId), "Selected threat level does not exist!");
            }

            bool missionTypeExists =
                await this.missionTypeService.ExistsByIdAsync(model.MissionTypeId);

            if (!missionTypeExists)
            {
                ModelState.AddModelError(nameof(model.MissionTypeId), "Selected mission type does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();
                model.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();

                return this.View(model);
            }

            try
            {
                string? organizerId =
                    await this.organizerService.GetOrganizerIdByUserIdAsync(this.User.GetId()!);

                await this.missionService.CreateAsync(model, organizerId!);
            }
            catch (Exception ex)
            {

                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new mission! Please try again later or contact an administrator!");

                model.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();
                model.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();

                return this.View(model);
            }

            return this.RedirectToAction("All", "Mission");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<MissionAllViewModel> myMissions =
                new List<MissionAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(userId);
            if (isUserOrganizer)
            {
                string organizerId = await this.organizerService.GetOrganizerIdByUserIdAsync(userId);

                myMissions.AddRange(await this.missionService.AllByOrganizerIdAsync(organizerId));
            }

            return this.View(myMissions);
        }
    }
}
