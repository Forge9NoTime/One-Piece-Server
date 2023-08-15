namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.ViewModels.ThreatLevel;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ThreatLevelService : IThreatLevelService
    {
        private readonly OnePieceDbContext dbContext;

        public ThreatLevelService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<MissionSelectThreatLevelFormModel>> ALlThreatLevelsAsync()
        {
            IEnumerable<MissionSelectThreatLevelFormModel> allThreatLevels = await this.dbContext
                .MissionThreatLevels
                .AsNoTracking()
                .Select(tl => new MissionSelectThreatLevelFormModel()
                {
                    Id = tl.Id,
                    Name = tl.Name
                })
                .ToArrayAsync();

            return allThreatLevels;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            bool result = await this.dbContext
                .MissionThreatLevels
                .AnyAsync(mtl => mtl.Id == id);

            return result;
        }
    }
}
