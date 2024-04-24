﻿namespace One_Piece.Service.Interfaces
{
    using OnePiece.Services.Data.Models.Mission;
    using OnePiece.Web.ViewModels.Team;

    public interface ITeamService
    {

        Task<bool> ExistsByIdAsync(Guid id);

        Task<TeamDetailsViewModel> GetDetailsByIdAsync(Guid teamId);

        Task<string> CreateAndReturnIdAsync(TeamFormModel formModel, string organizerId);

        Task<AllTeams> AllTeamsAsync(TeamsAllQueryModel queryModel);
    }
}
