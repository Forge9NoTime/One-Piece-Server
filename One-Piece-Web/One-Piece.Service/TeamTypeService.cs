namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.ViewModels.TeamType;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class TeamTypeService : ITeamTypeService
    {
        private readonly OnePieceDbContext dbContext;

        public TeamTypeService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            bool result = await this.dbContext
                .TeamTypes
                .AnyAsync(mt => mt.Id == id);

            return result;
        }

        public async Task<IEnumerable<string>> AllTeamTypeNamesAsync()
        {
            IEnumerable<string> allTeamTypeNames = await this.dbContext
                 .TeamTypes
                 .Select(tt => tt.TypeName)
                 .ToArrayAsync();

            return allTeamTypeNames;
        }

        public async Task<IEnumerable<TeamSelectTypeFormModel>> AllTeamTypesAsync()
        {
            IEnumerable<TeamSelectTypeFormModel> allTeamTypes = await this.dbContext
                .TeamTypes
                .AsNoTracking()
                .Select(tt => new TeamSelectTypeFormModel()
                {
                    TeamTypeID = tt.Id,
                    Name = tt.TypeName
                })
                .ToArrayAsync();

            return allTeamTypes;
        }
    }
}
