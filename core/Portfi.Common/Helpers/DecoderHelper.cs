using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DTO = Portfi.Common.Dto;
using GCONST = Portfi.Common.Constants.GeneralConstants;

namespace Portfi.Common.Helpers;

/// <summary>
/// Provides helper methods for decoding authentication tokens from cookies.
/// </summary>
public static class DecoderHelper
{
    /// <summary>
    /// Attempts to retrieve and decode an authentication token from the provided cookie collection.
    /// </summary>
    /// <param name="cookies">The request cookie collection containing authentication token parts.</param>
    /// <returns>The decoded authentication token.</returns>
    /// <exception cref="ArgumentNullException">Thrown when one or both authentication token parts are missing from the cookies.</exception>
    public static DTO.TokenResponse TryGetDecodedToken(
        this IRequestCookieCollection cookies)
    {
        string tokenPart1 = cookies[GCONST.NameOfAuthCookie1]
            ?? string.Empty;

        string tokenPart2 = cookies[GCONST.NameOfAuthCookie2]
            ?? string.Empty;

        if (string.IsNullOrEmpty(tokenPart1)
            || string.IsNullOrEmpty(tokenPart2))
        {
            throw new ArgumentNullException("No cookie found");
        }

        string base64token = tokenPart1 + tokenPart2;

        string decodedTokenJson = DecodeBase64(base64token.Replace("base64-", ""));

        var token = JsonConvert.DeserializeObject<DTO.TokenResponse>(decodedTokenJson);

        return token
            ?? throw new ArgumentNullException("Token couldn't be decoded.");
    }

    private static string DecodeBase64(
        string base64String)
    {
        byte[] data = Convert
            .FromBase64String(base64String);

        return Encoding.UTF8
            .GetString(data);
    }
}

