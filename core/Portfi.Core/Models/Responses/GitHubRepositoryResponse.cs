using System.Text.Json.Serialization;

namespace Portfi.Core.Models.Responses;

/// <summary>
/// Represents a GitHub repository with selected properties.
/// </summary>
public class GitHubRepository
{
    /// <summary>
    /// Gets or sets the unique identifier of the repository.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the repository.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the full name of the repository, including the owner's username.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("full_name")]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the URL to the repository on GitHub.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the repository.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the primary programming language of the repository.
    /// </summary>
    [JsonRequired]
    [JsonPropertyName("language")]
    public string Language { get; set; } = null!;
}