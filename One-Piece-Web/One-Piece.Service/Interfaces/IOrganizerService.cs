﻿namespace One_Piece.Service.Interfaces
{
    public interface IOrganizerService
    {
       Task<string> GetOrganizerIdByUserIdAsync(string userId);

       Task<bool> OrganizerExistsByUserIdAsync(string userId);
    }
}
