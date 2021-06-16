using System;
using System.Security.Claims;

namespace Application.Helpers
{
    public static class Extensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            if (user is null)
            {
                return Guid.Empty;
            }

            return Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}