using Microsoft.AspNetCore.Identity;

namespace ReleaseForge.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
