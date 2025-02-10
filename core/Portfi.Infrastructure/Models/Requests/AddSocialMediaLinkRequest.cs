using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ENUMS = Portfi.Common.Enums;

namespace Portfi.Infrastructure.Models.Requests;

public class AddSocialMediaLinkRequest
{
    /// <summary>
    /// Gets or sets the platform type for the social media link (Enum: Facebook, X(Twitter), etc.).
    /// </summary>
    [Required]
    [JsonPropertyName("type")]
    [Description("The type of link.")]
    public ENUMS.SocialMediaPlatform Type { get; set; }

    /// <summary>
    /// Gets or sets the social media link (URL or other data).
    /// </summary>
    [Required]
    [JsonPropertyName("value")]
    [Description("The link itself.")]
    public string Value { get; set; } = null!;
}