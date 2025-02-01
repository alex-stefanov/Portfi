using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ENUMS = Portfi.Common.Enums;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a project in the portfolio.
/// </summary>
public class Project
{
    /// <summary>
    /// Unique identifier of the project.
    /// </summary>
    [Key]
    [Required]
    [JsonPropertyName("id")]
    [Description("The unique identifier of the project.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the portfolio that contains the project.
    /// </summary>
    [Required]
    [ForeignKey(nameof(Portfolio))]
    [JsonPropertyName("portfolio_id")]
    [Description("The unique identifier of the portfolio that contains the project.")]
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Navigation property for the portfolio that contains the project.
    /// </summary>
    [JsonIgnore]
    public Portfolio Portfolio { get; set; } = null!;

    /// <summary>
    /// Link to the source code of the project.
    /// </summary>
    [Required]
    [JsonPropertyName("source_code_link")]
    [Description("The link to the source code of the project.")]
    public string SourceCodeLink { get; set; } = null!;

    /// <summary>
    /// Optional link to the live project.
    /// </summary>
    [JsonPropertyName("hosted_link")]
    [Description("An optional link to the live project.")]
    public string? HostedLink { get; set; }

    /// <summary>
    /// Optional description of the project.
    /// </summary>
    [JsonPropertyName("description")]
    [Description("An optional description of the project.")]
    public string? Description { get; set; }

    /// <summary>
    /// Optional collection of image URL's related to the project.
    /// </summary>
    [JsonPropertyName("images")]
    [Description("An optional collection of images related to the project.")]
    public HashSet<string> Images { get; set; } = [];

    /// <summary>
    /// Collection of categories associated with the project (up to 5).
    /// </summary>
    [JsonPropertyName("categories")]
    [Description("A list of categories associated with the project (up to 5).")]
    public HashSet<ENUMS.ProjectCategory> Categories { get; set; } = [];
}
