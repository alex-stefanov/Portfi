using System.ComponentModel;

namespace Portfi.Infrastructure.Common.Enums;

/// <summary>
/// Represents the category of a project.
/// </summary>
public enum ProjectCategory
{
    /// <summary>
    /// C# programming language.
    /// </summary>
    [Description("C# programming language.")]
    CSharp = 0,

    /// <summary>
    /// HTML markup language.
    /// </summary>
    [Description("HTML markup language.")]
    HTML = 1,

    /// <summary>
    /// CSS styling language.
    /// </summary>
    [Description("CSS styling language.")]
    CSS = 2,

    /// <summary>
    /// JavaScript programming language.
    /// </summary>
    [Description("JavaScript programming language.")]
    JavaScript = 3,

    /// <summary>
    /// Python programming language.
    /// </summary>
    [Description("Python programming language.")]
    Python = 4,

    /// <summary>
    /// Java programming language.
    /// </summary>
    [Description("Java programming language.")]
    Java = 5,

    /// <summary>
    /// React JavaScript franework.
    /// </summary>
    [Description("React JavaScript framework.")]
    React = 6,

    /// <summary>
    /// Angular JavaScript framework.
    /// </summary>
    [Description("Angular JavaScript framework.")]
    Angular = 7,

    /// <summary>
    /// SQL database language.
    /// </summary>
    [Description("SQL database language.")]
    SQL = 8,

    /// <summary>
    /// Other category.
    /// </summary>
    [Description("Other category.")]
    Other = 9
}
