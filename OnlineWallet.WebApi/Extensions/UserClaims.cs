using OnlineWallet.Domain.Enums;
using System.Security.Claims;

namespace OnlineWallet.WebApi.Extensions
{
    public static class UserClaims
    {
        public static Guid GetUserId(this HttpContext context)
        {
            var idAsString = context.User.Claims.ToList().FirstOrDefault(x=> x.Type == ClaimTypes.NameIdentifier)?.Value;
            
            var userId = string.IsNullOrEmpty(idAsString) ? Guid.Empty : Guid.Parse(idAsString);
            return userId;
        }
    }
}
