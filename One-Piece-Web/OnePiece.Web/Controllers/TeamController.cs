namespace OnePiece.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using One_Piece.Service.Interfaces;
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.Infrastructure.Extentions;
    using OnePiece.Web.ViewModels.Team;
    using static OnePiece.Common.NotificationMessagesConstants;

    [Authorize]
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IVolunteerService volunteerService;
        private readonly ITeamTypeService teamTypeService;
        private readonly IOrganizerService organizerService;
        private readonly IMissionService missionService;

        public TeamController(ITeamService teamService, IVolunteerService volunteerService, ITeamTypeService teamTypeService, IOrganizerService organizerService, IMissionService missionService)
        {
            this.teamService = teamService;
            this.volunteerService = volunteerService;
            this.teamTypeService = teamTypeService;
            this.organizerService = organizerService;
            this.missionService = missionService;
        }
        private IActionResult GeneralError()
        {
            this.TempData[ErrorMessage] = "Unexpected error occurred! Please try again later or contact administrator!";
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isOrganizer =
                await this.organizerService.OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isOrganizer)
            {
                this.TempData[ErrorMessage] = "You must become an organizer in order to add teams!";

                return this.RedirectToAction("Become", "Organizer");
            }

            try
            {
                TeamFormModel formModel = new TeamFormModel()
                {
                    Missions = await this.missionService.AllMissionsAsync(),
                    TeamTypes = await this.teamTypeService.AllTeamTypesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Add(TeamFormModel model)
        {
            bool isOrganizer =
                await this.organizerService.OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isOrganizer)
            {
                this.TempData[ErrorMessage] = "You must become an organizer in order to add teams!";

                return this.RedirectToAction("Become", "Organizer");
            }

            bool teamTypeExists =
                await this.teamTypeService.ExistsByIdAsync(model.TeamTypeId);

            if (!teamTypeExists)
            {
                ModelState.AddModelError(nameof(model.TeamTypeId), "Selected team type does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.TeamTypes = await this.teamTypeService.AllTeamTypesAsync();
                model.Missions = await this.missionService.AllMissionsAsync();

                return this.View(model);
            }

            try
            {
                string? organizerId =
                    await this.organizerService.GetOrganizerIdByUserIdAsync(this.User.GetId()!);

                string teamId = await this.teamService.CreateAndReturnIdAsync(model, organizerId!);

                return this.RedirectToAction("Details", "Team", new { id = teamId });
            }
            catch (Exception)
            {

                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new team! Please try again later or contact an administrator!");

                model.TeamTypes = await this.teamTypeService.AllTeamTypesAsync();
                model.Missions = await this.missionService.AllMissionsAsync();

                return this.View(model);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            bool teamExists = await this.teamService
                .ExistsByIdAsync(id);
            if (!teamExists)
            {
                this.TempData[ErrorMessage] = "Team with the provided id does not exist!";

                return this.RedirectToAction("All", "Team");
            }

            try
            {
                TeamDetailsViewModel viewModel = await this.teamService
                .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] TeamsAllQueryModel queryModel)
        {
            AllTeams serviceModel =
                await this.teamService.AllTeamsAsync(queryModel);


            queryModel.Teams = serviceModel.Teams.ToList();
            queryModel.TeamTypes = await this.teamTypeService.AllTeamTypeNamesAsync();
            return this.View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Join()
        {
            var user = this.User.GetId();

            if (user == null)
            {
                return this.View("Error400");
            }

            try
            {
                JoinTeamFormModel formModel = new JoinTeamFormModel()
                {
                    Teams = await this.teamService.GetAllTeamsAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Join(JoinTeamFormModel model)
        {
            var userId = this.User.GetId();

            if (userId == null)
            {
                return this.View("Error400");
            }

            try
            {
                model.Teams = await this.teamService.GetAllTeamsAsync();

                if (!ModelState.IsValid)
                {
                    return this.View(model);
                }

                await this.teamService.JoinTeamAsync(new Guid(model.TeamId), new Guid(userId));

                return this.RedirectToAction("Details", "Team", new { id = model.TeamId });

            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, TeamFormModel model)
        {
            bool teamExists = await this.teamService
                .ExistsByIdAsync(id);
            if (!teamExists)
            {
                this.TempData[ErrorMessage] = "Team with the provided id does not exist!";

                return this.RedirectToAction("All", "Team");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the team!";

                return this.RedirectToAction("All", "Team");
            }

            string organizerId =
                await this.organizerService.GetOrganizerIdByUserIdAsync(User.GetId()!);
            bool isOrganizerCreator = await this.teamService
                .isOrganizerWithIdCreatorOfTeamWithIdAsync(id, new Guid(organizerId)!);
            if (!isOrganizerCreator)
            {
                this.TempData[ErrorMessage] = "You must be the organizer of the mission you want to edit!";

                return this.RedirectToAction("All", "Team");
            }

            try
            {
                TeamFormModel formModel = await this.teamService
                .GetTeamForEditByIdAsync(id);
                formModel.TeamTypes = await this.teamTypeService.AllTeamTypesAsync();
                formModel.Missions = await this.missionService.AllMissionsAsync();

                return this.View(formModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string id, TeamFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.TeamTypes = await this.teamTypeService.AllTeamTypesAsync();
                model.Missions = await this.missionService.AllMissionsAsync();
                return this.View(model);
            }

            try
            {
                await this.teamService.EditTeamByIdAndFormModelAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the team. Please try again later or contact administrator!");
                model.TeamTypes = await this.teamTypeService.AllTeamTypesAsync();
                model.Missions = await this.missionService.AllMissionsAsync();
            }

            return this.RedirectToAction("Details", "Team", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool teamExists = await this.teamService
                .ExistsByIdAsync(id);
            if (!teamExists)
            {
                this.TempData[ErrorMessage] = "Team with the provided id does not exist!";

                return this.RedirectToAction("All", "Team");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the team!";

                return this.RedirectToAction("All", "Team");
            }

            try
            {
                TeamPreDeleteDetailsViewModel viewModel = await this.teamService.GetTeamForDeleteByIdAsync(id);
                return this.View(viewModel);
            }
            catch (Exception)
            {
                return this.GeneralError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, TeamPreDeleteDetailsViewModel model)
        {
            bool teamExists = await this.teamService
              .ExistsByIdAsync(id);
            if (!teamExists)
            {
                this.TempData[ErrorMessage] = "Team with the provided id does not exist!";

                return this.RedirectToAction("All", "Team");
            }

            bool isUserOrganizer = await this.organizerService
                .OrganizerExistsByUserIdAsync(this.User.GetId()!);
            if (!isUserOrganizer)
            {
                this.TempData[ErrorMessage] = "You are not the organizer of the team!";

                return this.RedirectToAction("All", "Team");
            }

            try
            {
                await this.teamService.DeleteTeamByIdAsync(id);

                return this.RedirectToAction("All", "Team");
            }
            catch (Exception)
            {

                return this.GeneralError();
            }
        }
    }
}
