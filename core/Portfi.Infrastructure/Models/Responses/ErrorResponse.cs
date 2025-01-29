using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Portfi.Infrastructure.Models.Responses;

/// <summary>
/// Represents an error response.
/// </summary>
public record class ErrorResponse
{
    /// <summary>
    /// Message explaining the error.
    /// </summary>
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    /// <summary>
    /// When the error occurred.
    /// </summary>
    [JsonPropertyName("date")]
    [DataType(DataType.DateTime)]
    public required DateTime Date { get; set; }
}
