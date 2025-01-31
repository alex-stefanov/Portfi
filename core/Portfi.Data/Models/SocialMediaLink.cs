using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ENUMS = Portfi.Common.Enums;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a social media link associated with a portfolio.
/// </summary>
public class SocialMediaLink
{
    /// <summary>
    /// Gets or sets the unique identifier for the social media link.
    /// </summary>
    [Key]
    [JsonPropertyName("id")]
    [Description("unique identifier of the social media link.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated portfolio.
    /// </summary>
    [ForeignKey(nameof(Portfolio))]
    [JsonPropertyName("portfolio_id")]
    [Description("The id of the coresponding portfolio.")]
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Gets or sets the portfolio associated with the social media link.
    /// </summary>
    [JsonIgnore]
    public Portfolio Portfolio { get; set; } = null!;

    /// <summary>
    /// Gets or sets the platform type for the social media link (Enum: Facebook, Twitter, etc.).
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