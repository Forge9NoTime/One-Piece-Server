namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.ViewModels.MissionType;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MissionTypeService : IMissionTypeService
    {
        private readonly OnePieceDbContext dbContext;

        public MissionTypeService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<MissionSelectTypeFormModel>> ALlMissionTypesAsync()
        {
            IEnumerable<MissionSelectTypeFormModel> allMissionTypes = await this.dbContext
                .MissionTypes
                .AsNoTracking()
                .Select(mt => new MissionSelectTypeFormModel()
                {
                    MissionTypeID = mt.Id,
                    Name = mt.TypeName
                })
                .ToArrayAsync();

            return allMissionTypes;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            bool result = await this.dbContext
                .MissionTypes
                .AnyAsync(mt => mt.Id == id);

            return result;
        }
    }
}
