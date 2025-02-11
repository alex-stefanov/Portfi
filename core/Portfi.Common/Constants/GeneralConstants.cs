namespace Portfi.Common.Constants;

/// <summary>
/// Contains constant values related to general settings and defaults.
/// </summary>
public static class GeneralConstants
{
    /// <summary>
    /// The name of the first authentication cookie.
    /// </summary>
    public const string NameOfAuthCookie1 = "sb-zwuxrlpqnokmjcbmlxla-auth-token.0";

    /// <summary>
    /// The name of the second authentication cookie.
    /// </summary>
    public const string NameOfAuthCookie2 = "sb-zwuxrlpqnokmjcbmlxla-auth-token.1";

    /// <summary>
    /// Ids of example portfolio holders.
    /// </summary>
    public static HashSet<string> IdsOfExamplePortfolioHolders =
    [
        "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p",
        "q6r7s8t9-u0v1-w2x3-y4z5-0a1b2c3d4e5f",
        "6g7h8i9-j0k1-l2m3-n4o5-p6q7r8s9t0u",
        "v1w2x3y4-z5a6-b7c8-d9e0-f1g2h3i4j5k6",
        "l7m8n9o0-p1q2-r3s4-t5u6-v7w8-x9y0z1a2b3c4d5e6f7g8h9i0j1k2l3m4n5o6p7q8r9s0t1u2v3w4x5y6z7"
    ];
}