namespace OnePiece.Web.Infrastructure.Extentions
{
    using System.Security.Claims;

    public static class ClaimPrincipalExtentions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
