namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Interfaces;
    using One_Piece.Data;
    using OnePiece.Web.ViewModels.Home;

    public class MissionService : IMissionService
    {

        private readonly OnePieceDbContext dbContext;

        public MissionService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IndexViewModel>> LastFiveMissionsAsync()
        {
            IEnumerable<IndexViewModel> lastFiveMissions = await this.dbContext
                .Missions
                .OrderByDescending(m => m.CreatedOn)
                .Take(5)
                .Select(m => new IndexViewModel()
                {
                    Id = m.Id.ToString(),
                    Title = m.Title,
                    Location = m.Location,
                    ThreatLevel = m.ThreatLevel.ToString(),
                    Description = m.Description
                })
                .ToArrayAsync();

            return lastFiveMissions;
        }
    }
}
