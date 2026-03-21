using System.Text.RegularExpressions;

namespace Philiprehberger.RegexBuilder;

/// <summary>
/// Entry point for the fluent regex builder API.
/// Provides static factory methods to begin constructing a pattern.
/// </summary>
public static class Pattern
{
    /// <summary>
    /// Starts building a new regex pattern.
    /// </summary>
    /// <returns>A new <see cref="PatternBuilder"/> instance.</returns>
    public static PatternBuilder Start() => new();

    /// <summary>
    /// Starts building a new regex pattern anchored to the start of the line.
    /// </summary>
    /// <returns>A new <see cref="PatternBuilder"/> with a start-of-line anchor.</returns>
    public static PatternBuilder StartOfLine() => new PatternBuilder().StartOfLine();

    /// <summary>
    /// Starts building a new regex pattern anchored to the end of the line.
    /// </summary>
    /// <returns>A new <see cref="PatternBuilder"/> with an end-of-line anchor.</returns>
    public static PatternBuilder EndOfLine() => new PatternBuilder().EndOfLine();
}
