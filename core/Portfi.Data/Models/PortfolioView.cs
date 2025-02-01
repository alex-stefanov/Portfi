using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Portfi.Data.Models;

/// <summary>
/// Represents a view of a portfolio by a user.
/// </summary>
public class PortfolioView
{
    /// <summary>
    /// Unique identifier of the view.
    /// </summary>
    [Key]
    [Required]
    [JsonPropertyName("id")]
    [Description("The unique identifier of the view.")]
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier of the person who viewed the portfolio.
    /// </summary>
    [Required]
    [JsonPropertyName("viewer_id")]
    [Description("The unique identifier of the person who viewed the portfolio.")]
    public string ViewerId { get; set; } = null!;

    /// <summary>
    /// Unique identifier of the portfolio that was viewed.
    /// </summary>
    [Required]
    [ForeignKey(nameof(Portfolio))]
    [JsonPropertyName("portfolio_id")]
    [Description("The unique identifier of the portfolio that was viewed.")]
    public Guid PortfolioId { get; set; }

    /// <summary>
    /// Navigation property for the portfolio that was viewed.
    /// </summary>
    [JsonIgnore]
    public Portfolio Portfolio { get; set; } = null!;

    /// <summary>
    /// Indicates whether the view is still active.
    /// </summary>
    [JsonPropertyName("is_active")]
    [Description("Indicates whether the view is still active.")]
    public bool IsActive => DateTime.UtcNow < ExpirationDate;

    /// <summary>
    /// The last date on which the view occurred.
    /// </summary>
    [Required]
    [JsonPropertyName("last_occurred_date")]
    [Description("The last date on which the view occurred.")]
    public DateTime LastOccurredDate { get; set; }

    /// <summary>
    /// The expiration date of the view (90 days after the last occurred date).
    /// </summary>
    [JsonPropertyName("expiration_date")]
    [Description("The expiration date of the view (90 days after the last occurred date).")]
    public DateTime ExpirationDate => LastOccurredDate.AddDays(90);
}
