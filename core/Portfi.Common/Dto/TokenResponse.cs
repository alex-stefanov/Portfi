using Newtonsoft.Json;

namespace Portfi.Common.Dto;

/// <summary>
/// Represents a response containing access and refresh tokens.
/// </summary>
public class TokenResponse
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    [JsonProperty("access_token", Required = Required.Always)]
    public required string AccessToken { get; set; }

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    [JsonProperty("refresh_token", Required = Required.Always)]
    public required string RefreshToken { get; set; }
}