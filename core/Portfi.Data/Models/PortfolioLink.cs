using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a link associated with a portfolio.
/// </summary>
public class PortfolioLink
{
    /// <summary>
    /// Gets or sets the unique identifier for the portfolio link.
    /// </summary>
    [Key]
    [JsonPropertyName("id")]
    [Description("Unique identifier of the link.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the associated portfolio.
    /// </summary>
    [ForeignKey(nameof(Portfolio))]
    [JsonPropertyName("portfolio_id")]
    [Description("The id of the corresponding portfolio.")]
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Gets or sets the portfolio associated with the portfolio link.
    /// </summary>
    [JsonIgnore]
    public Portfolio Portfolio { get; set; } = null!;

    /// <summary>
    /// Gets or sets the value associated with the portfolio link (e.g., URL or other data).
    /// </summary>
    [Required]
    [JsonPropertyName("value")]
    [Description("The link itself.")]
    public string Value { get; set; } = null!;

    /// <summary>
    /// Gets or sets the creation date of the portfolio link.
    /// </summary>
    [Required]
    [JsonPropertyName("creation_date")]
    [Description("The time when the link was created.")]
    public DateTimeOffset CreationDate { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the portfolio link.
    /// </summary>
    [Required]
    [JsonPropertyName("expiration_date")]
    [Description("The time when the link is set to expire.")]
    public DateTimeOffset ExpirationDate { get; set; }

    /// <summary>
    /// Gets a value indicating whether the portfolio link is expired based on the expiration date.
    /// </summary>
    [NotMapped]
    [JsonPropertyName("is_expired")]
    public bool IsExpired => ExpirationDate < DateTimeOffset.Now;
}