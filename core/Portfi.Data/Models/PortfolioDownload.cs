using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a download of a portfolio by a user.
/// </summary>
public class PortfolioDownload
{
    /// <summary>
    /// Unique identifier for the download.
    /// </summary>
    [Key]
    [Required]
    [JsonPropertyName("id")]
    [Description("The unique identifier of the download.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the person who downloaded the portfolio.
    /// </summary>
    [Required]
    [JsonPropertyName("downloader_id")]
    [Description("The unique identifier of the person who downloaded the portfolio.")]
    public string DownloaderId { get; set; } = null!;

    /// <summary>
    /// Unique identifier of the portfolio that was downloaded.
    /// </summary>
    [Required]
    [ForeignKey(nameof(Portfolio))]
    [JsonPropertyName("portfolio_id")]
    [Description("The unique identifier of the portfolio that was downloaded.")]
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Navigation property for the portfolio that was downloaded.
    /// </summary>
    [JsonIgnore]
    public Portfolio Portfolio { get; set; } = null!;
}
