using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ENUMS = Portfi.Infrastructure.Common.Enums;
using static Portfi.Infrastructure.Common.Constants.PortfolioConstants;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a portfolio containing information about a person.
/// </summary>
public class Portfolio
{
    /// <summary>
    /// Unique identifier of the portfolio.
    /// </summary>
    [Required]
    [JsonPropertyName("id")]
    [Description("The unique identifier of the portfolio.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the person (Google ID or normal ID).
    /// </summary>
    [Required]
    [JsonPropertyName("person_id")]
    [Description("The unique identifier of the person (Google ID or normal ID).")]
    public string PersonId { get; set; } = null!;

    /// <summary>
    /// Biography or description of the person.
    /// </summary>
    [Required]
    [JsonPropertyName("biography")]
    [Description("A biography or description of the person.")]
    public string Biography { get; set; } = null!;

    /// <summary>
    /// Rating of the portfolio.
    /// </summary>
    [Required]
    [JsonPropertyName("rating")]
    [Description("The rating of the portfolio.")]
    public double Rating { get; set; } = DefaultRatingValue;

    /// <summary>
    /// Avatar or profile picture URL.
    /// </summary>
    [Required]
    [JsonPropertyName("avatar")]
    [Description("The avatar or profile picture URL.")]
    public string Avatar { get; set; } = DefaultAvatarValue;

    /// <summary>
    /// Name of the background theme.
    /// </summary>
    [Required]
    [JsonPropertyName("background_theme")]
    [Description("The name of the background theme.")]
    public string BackgroundTheme { get; set; } = DefaultBackgroundThemeValue;

    /// <summary>
    /// Name of the main color.
    /// </summary>
    [Required]
    [JsonPropertyName("main_color")]
    [Description("The name of the main color.")]
    public string MainColor { get; set; } = DefaultMainColorValue;

    /// <summary>
    /// Likes of the portfolio.
    /// </summary>
    [Required]
    [JsonPropertyName("likes")]
    [Description("How many likes a portfolio has")]
    public int Likes { get; set; }

    /// <summary>
    /// Indicates whether the portfolio is public.
    /// </summary>
    [JsonPropertyName("is_public")]
    [Description("Indicates whether the portfolio is public.")]
    public bool IsPublic { get; set; } = DefaultIsPublicValue;

    /// <summary>
    /// Date on which the portfolio was created on.
    /// </summary>
    [Required]
    [JsonPropertyName("created_on")]
    [Description("When the creation of the portfolio was.")]
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// URL to the CV/document.
    /// </summary>
    [JsonPropertyName("cv")]
    [Description("An URL to the CV/document.")]
    public string? CV { get; set; }

    /// <summary>
    /// Dictionary of social media links with their respective platforms.
    /// </summary>
    [JsonPropertyName("social_media_links")]
    [Description("A dictionary of social media links with their respective platforms.")]
    public Dictionary<string, ENUMS.SocialMediaPlatform> SocialMediaLinks { get; set; } = [];

    /// <summary>
    /// Collection of projects in the portfolio.
    /// </summary>
    [JsonPropertyName("projects")]
    [Description("A collection of projects in the portfolio.")]
    public ICollection<Project> Projects { get; set; } = [];

    /// <summary>
    /// Collection of views for the portfolio.
    /// </summary>
    [JsonPropertyName("views")]
    [Description("A collection of views for the portfolio.")]
    public ICollection<PortfolioView> PortfolioViews { get; set; } = [];

    /// <summary>
    /// Collection of downloads of the portfolio.
    /// </summary>
    [JsonPropertyName("downloads")]
    [Description("A collection of downloads of the portfolio.")]
    public ICollection<PortfolioDownload> PortfolioDownloads { get; set; } = [];

    /// <summary>
    /// Collection of portfolio links with their active status.
    /// </summary>
    [JsonPropertyName("portfolio_links")]
    [Description("A collection of portfolio links with their active status.")]
    public ICollection<Dictionary<string, bool>> PortfolioLinks { get; set; } = [];
}
