using System.Security.Claims;
using API.Source.Model.Common;

namespace API.Source.Extension;

public static class UserClaimExtension
{
    public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        Claim? claim = claimsPrincipal.Claims.FirstOrDefault(claim => claim.Type == AppClaimType.UserId);
        
        if (claim is null)
        {
            throw new System.Exception("Could not find claim " + AppClaimType.UserId);
        }

        if (!long.TryParse(claim.Value, out var userId))
        {
            throw new System.Exception("Claim type " + AppClaimType.UserId + " is not type of long");
        }
        
        return userId;
    }
}