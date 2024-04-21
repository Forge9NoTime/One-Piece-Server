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

        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator!";
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllMissionsQueryModel queryModel)
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

            try
            {
                MissionFormModel formModel = new MissionFormModel()
                {
                    ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync(),
                    MissionTypes = await this.missionTypeService.ALlMissionTypesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

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

                string missionId = await this.missionService.CreateAndReturnIdAsync(model, organizerId!);

                return this.RedirectToAction("Details", "House", new { id = missionId });
            }
            catch (Exception)
            {

                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new mission! Please try again later or contact an administrator!");

                model.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();
                model.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();

                return this.View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<MissionAllViewModel> myMissions =
                new List<MissionAllViewModel>();

            string userId = this.User.GetId()!;
            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(userId);

            try
            {
                if (isUserOrganizer)
                {
                    string organizerId = await this.organizerService.GetOrganizerIdByUserIdAsync(userId);

                    myMissions.AddRange(await this.missionService.AllByOrganizerIdAsync(organizerId));
                }

                return this.View(myMissions);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            bool missionExists = await this.missionService
                .ExistsByIdAsync(id);
            if (!missionExists)
            {
                this.TempData[ErrorMessage] = "Mission with the provided id does not exist!";

                return this.RedirectToAction("All", "Mission");
            }

            try
            {
                MissionDetailsViewModel viewModel = await this.missionService
                .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, MissionFormModel model)
        {
            bool missionExists = await this.missionService
                .ExistsByIdAsync(id);
            if (!missionExists)
            {
                this.TempData[ErrorMessage] = "Mission with the provided id does not exist!";

                return this.RedirectToAction("All", "Mission");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the mission!";

                return this.RedirectToAction("All", "Mission");
            }

            string organizerId =
                await this.organizerService.GetOrganizerIdByUserIdAsync(User.GetId()!);
            bool isOrganizerCreator = await this.missionService
                .isOrganizerWithIdCreatorOfMissionWithIdAsync(id, new Guid (organizerId)!);
            if (!isOrganizerCreator)
            {
                this.TempData[ErrorMessage] = "You must be the organizer of the mission you want to edit!";

                return this.RedirectToAction("Mine", "Mission");
            }

            try
            {
                MissionFormModel formModel = await this.missionService
                .GetMissionForEditByIdAsync(id);
                formModel.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();
                formModel.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, MissionFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();
                model.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();
                return this.View(model);
            }

            try
            {
                await this.missionService.EditMissionByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the mission. Please try again later or contact administrator!");
                model.MissionTypes = await this.missionTypeService.ALlMissionTypesAsync();
                model.ThreatLevels = await this.threatLevelService.ALlThreatLevelsAsync();
            }

            return this.RedirectToAction("Details", "Mission", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool missionExists = await this.missionService
                .ExistsByIdAsync(id);
            if (!missionExists)
            {
                this.TempData[ErrorMessage] = "Mission with the provided id does not exist!";

                return this.RedirectToAction("All", "Mission");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the mission!";

                return this.RedirectToAction("All", "Mission");
            }

            try
            {
                MissionPreDeleteDetailsViewModel viewModel = await this.missionService.GetMissionForDeleteByIdAsync(id.ToString());
                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, MissionPreDeleteDetailsViewModel model)
        {
            bool missionExists = await this.missionService
              .ExistsByIdAsync(id);
            if (!missionExists)
            {
                this.TempData[ErrorMessage] = "Mission with the provided id does not exist!";

                return this.RedirectToAction("All", "Mission");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the mission!";

                return this.RedirectToAction("All", "Mission");
            }

            try
            {
                await this.missionService.DeleteMissionByIdAsync(id);

                return this.RedirectToAction("Mine", "Mission");
            }
            catch (Exception)
            {

                return this.GeneralError();
            }
        }
    }
}
