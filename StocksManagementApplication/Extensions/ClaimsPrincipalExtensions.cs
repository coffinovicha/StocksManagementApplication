using System.Security.Claims;

namespace StocksManagementApplication.UI.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserRole(this ClaimsPrincipal user) 
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value;
        }
    }
}
