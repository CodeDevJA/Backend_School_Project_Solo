using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

public static class Utility
{
    private static IHttpContextAccessor _httpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ??
            throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public static Guid GetUserIdGuid()
    {
        var user = _httpContextAccessor?.HttpContext?.User;

        if (user == null)
        {
            throw new InvalidOperationException("No user context available.");
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException("User ID claim is missing.");
        }

        if (Guid.TryParse(userId, out Guid parsedUserId))
        {
            return parsedUserId;
        }

        throw new InvalidOperationException("User ID claim is not a valid GUID.");
    }
}

// To use this utility follow these steps:

// 1. Register the IHttpContextAccessor in Program.cs:
// builder.Services.AddHttpContextAccessor();

// 2. And initialize the utility (in Program.cs):
// Utility.Configure(app.Services.GetRequiredService<IHttpContextAccessor>());

// 3. Now you can simply call:
// Guid userId = Utility.GetUserIdGuid();
