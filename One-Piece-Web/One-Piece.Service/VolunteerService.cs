namespace One_Piece.Service
{
    using System.Threading.Tasks;

    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using One_Piece.Data;
    using One_Piece.Data.Models;
    using OnePiece.Web.ViewModels.Volunteer;

    public class VolunteerService : IVolunteerService
    {
        private readonly OnePieceDbContext dbContext;

        public VolunteerService(OnePieceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> VolunteerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dbContext
              .Volunteers
              .AnyAsync(v => v.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<bool> VolunteerExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Volunteers
                .AnyAsync(v => v.UserId.ToString() == userId);

            return result;
        }

        public async Task Create(string userId, BecomeVolunteerFormModel model)
        {
            Volunteer newVolunteer = new Volunteer()
            {
                FullName = model.FullName,
                UserId = Guid.Parse(userId),
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                Age = model.Age
            };

            await this.dbContext.Volunteers.AddAsync(newVolunteer);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
