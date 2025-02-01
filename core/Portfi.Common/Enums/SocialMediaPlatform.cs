using System.ComponentModel;

namespace Portfi.Common.Enums;

/// <summary>
/// Represents the type of social media platform.
/// </summary>
public enum SocialMediaPlatform
{
    /// <summary>
    /// GitHub platform.
    /// </summary>
    [Description("GitHub platform.")]
    GitHub = 0,

    /// <summary>
    /// Email.
    /// </summary>
    [Description("Email.")]
    Email = 1,

    /// <summary>
    /// LinkedIn platform.
    /// </summary>
    [Description("LinkedIn platform.")]
    LinkedIn = 2,

    /// <summary>
    /// Instagram platform.
    /// </summary>
    [Description("Instagram platform.")]
    Instagram = 3,

    /// <summary>
    /// X platform (Twitter).
    /// </summary>
    [Description("X platform (Twitter).")]
    X = 4,

    /// <summary>
    /// Facebook platform.
    /// </summary>
    [Description("Facebook platform.")]
    Facebook = 5,

    /// <summary>
    /// YouTube platform.
    /// </summary>
    [Description("YouTube platform.")]
    YouTube = 6,

    /// <summary>
    /// TikTok platform.
    /// </summary>
    [Description("TikTok platform.")]
    TikTok = 7,

    /// <summary>
    /// Pinterest platform.
    /// </summary>
    [Description("Pinterest platform.")]
    Pinterest = 8,

    /// <summary>
    /// Other platform.
    /// </summary>
    [Description("Other platform.")]
    Other = 9
}