namespace One_Piece.Service
{
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Data.Models;
    using One_Piece.Service.Interfaces;
    using OnePiece.Web.ViewModels.Organizer;
    using System;
    using System.Threading.Tasks;

    public class OrganizerService : IOrganizerService
    {
        private readonly OnePieceDbContext dbContext;

        public OrganizerService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(string userId, BecomeOrganizerFormModel model)
        {
            Organizer newOrganizer = new Organizer()
            {
                UserId = Guid.Parse(userId),
                EGN = model.EGN,
                PlaceOfResidence = model.PlaceOfResidence,
                AffiliatedOrganization = model.AffiliatedOrganization
            };

            await this.dbContext.Organizers.AddAsync(newOrganizer);
            await this.dbContext.SaveChangesAsync();
        }


        public async Task<string> GetOrganizerIdByUserIdAsync(string userId)
        {
            Organizer? organizer = await this.dbContext
                .Organizers
                .Include(o => o.CreatedMissions)
                .FirstOrDefaultAsync(o => o.UserId.ToString() == userId);
            if (organizer == null)
            {
                return null;
            }

            return organizer.Id.ToString();
        }

        public async Task<bool> HasMissionWithIdAsync(string userId, string missionId)
        {
            Organizer? organizer = await this.dbContext
                .Organizers
                .FirstOrDefaultAsync(o => o.UserId.ToString() == userId);
            if (organizer == null)
            {
                return false;
            }

            return organizer.CreatedMissions.Any(m => m.Id.ToString() == missionId);
        }

        public async Task<bool> OrganizerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Organizers
                .AnyAsync(v => v.UserId.ToString() == userId);

            return result;
        }
    }
}

