using System.Text;
using System.Text.RegularExpressions;

namespace Philiprehberger.RegexBuilder;

/// <summary>
/// Fluent builder for constructing regular expression patterns programmatically.
/// All methods return <c>this</c> to enable method chaining.
/// </summary>
public class PatternBuilder
{
    private readonly StringBuilder _pattern = new();

    /// <summary>
    /// Appends a literal string to the pattern, automatically escaping special regex characters.
    /// </summary>
    /// <param name="text">The literal text to match.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Literal(string text)
    {
        _pattern.Append(Regex.Escape(text));
        return this;
    }

    /// <summary>
    /// Appends a single digit character class (<c>\d</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Digit()
    {
        _pattern.Append(@"\d");
        return this;
    }

    /// <summary>
    /// Appends one or more digits (<c>\d+</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Digits()
    {
        _pattern.Append(@"\d+");
        return this;
    }

    /// <summary>
    /// Appends a non-digit character class (<c>\D</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder NonDigit()
    {
        _pattern.Append(@"\D");
        return this;
    }

    /// <summary>
    /// Appends a word character class (<c>\w</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Word()
    {
        _pattern.Append(@"\w");
        return this;
    }

    /// <summary>
    /// Appends one or more word characters (<c>\w+</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Words()
    {
        _pattern.Append(@"\w+");
        return this;
    }

    /// <summary>
    /// Appends a non-word character class (<c>\W</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder NonWord()
    {
        _pattern.Append(@"\W");
        return this;
    }

    /// <summary>
    /// Appends a whitespace character class (<c>\s</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Whitespace()
    {
        _pattern.Append(@"\s");
        return this;
    }

    /// <summary>
    /// Appends a non-whitespace character class (<c>\S</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder NonWhitespace()
    {
        _pattern.Append(@"\S");
        return this;
    }

    /// <summary>
    /// Appends a wildcard (<c>.</c>) that matches any character except newline.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Any()
    {
        _pattern.Append('.');
        return this;
    }

    /// <summary>
    /// Appends a character class that matches any one of the specified characters.
    /// </summary>
    /// <param name="chars">The characters to include in the character class.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder AnyOf(string chars)
    {
        _pattern.Append($"[{Regex.Escape(chars)}]");
        return this;
    }

    /// <summary>
    /// Appends a negated character class that matches any character not in the specified set.
    /// </summary>
    /// <param name="chars">The characters to exclude from the character class.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder NoneOf(string chars)
    {
        _pattern.Append($"[^{Regex.Escape(chars)}]");
        return this;
    }

    /// <summary>
    /// Appends an exact repetition quantifier (<c>{n}</c>) to the previous element.
    /// </summary>
    /// <param name="n">The exact number of times to match.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Times(int n)
    {
        _pattern.Append($"{{{n}}}");
        return this;
    }

    /// <summary>
    /// Appends a range repetition quantifier (<c>{min,max}</c>) to the previous element.
    /// </summary>
    /// <param name="min">The minimum number of times to match.</param>
    /// <param name="max">The maximum number of times to match.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Times(int min, int max)
    {
        _pattern.Append($"{{{min},{max}}}");
        return this;
    }

    /// <summary>
    /// Appends an optional quantifier (<c>?</c>) to the previous element.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Optional()
    {
        _pattern.Append('?');
        return this;
    }

    /// <summary>
    /// Appends a one-or-more quantifier (<c>+</c>) to the previous element.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder OneOrMore()
    {
        _pattern.Append('+');
        return this;
    }

    /// <summary>
    /// Appends a zero-or-more quantifier (<c>*</c>) to the previous element.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder ZeroOrMore()
    {
        _pattern.Append('*');
        return this;
    }

    /// <summary>
    /// Appends a non-capturing group containing the pattern built by the inner action.
    /// </summary>
    /// <param name="inner">An action that builds the group's inner pattern.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Group(Action<PatternBuilder> inner)
    {
        var innerBuilder = new PatternBuilder();
        inner(innerBuilder);
        _pattern.Append($"(?:{innerBuilder})");
        return this;
    }

    /// <summary>
    /// Appends a named capturing group containing the pattern built by the inner action.
    /// </summary>
    /// <param name="name">The name of the capturing group.</param>
    /// <param name="inner">An action that builds the group's inner pattern.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder CaptureGroup(string name, Action<PatternBuilder> inner)
    {
        var innerBuilder = new PatternBuilder();
        inner(innerBuilder);
        _pattern.Append($"(?<{name}>{innerBuilder})");
        return this;
    }

    /// <summary>
    /// Appends a named capturing group containing the pattern built by the inner action.
    /// Generates a <c>(?&lt;name&gt;...)</c> pattern. This is an alias for <see cref="CaptureGroup"/>.
    /// </summary>
    /// <param name="name">The name of the capturing group.</param>
    /// <param name="inner">An action that builds the group's inner pattern.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder NamedGroup(string name, Action<PatternBuilder> inner)
    {
        return CaptureGroup(name, inner);
    }

    /// <summary>
    /// Appends an alternation (<c>|</c>) with the pattern built by the alternative action.
    /// </summary>
    /// <param name="alt">An action that builds the alternative pattern.</param>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Or(Action<PatternBuilder> alt)
    {
        var altBuilder = new PatternBuilder();
        alt(altBuilder);
        _pattern.Append($"|{altBuilder}");
        return this;
    }

    /// <summary>
    /// Appends a start-of-line anchor (<c>^</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder StartOfLine()
    {
        _pattern.Append('^');
        return this;
    }

    /// <summary>
    /// Appends an end-of-line anchor (<c>$</c>) to the pattern.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder EndOfLine()
    {
        _pattern.Append('$');
        return this;
    }

    /// <summary>
    /// Appends a lazy modifier (<c>?</c>) to the previous quantifier, making it match as few characters as possible.
    /// </summary>
    /// <returns>This builder instance for chaining.</returns>
    public PatternBuilder Lazy()
    {
        _pattern.Append('?');
        return this;
    }

    /// <summary>
    /// Compiles the pattern into a <see cref="Regex"/> object.
    /// </summary>
    /// <param name="options">Optional regex options to apply.</param>
    /// <returns>A compiled <see cref="Regex"/> instance.</returns>
    public Regex Build(RegexOptions? options = null)
    {
        return options.HasValue
            ? new Regex(_pattern.ToString(), options.Value)
            : new Regex(_pattern.ToString());
    }

    /// <summary>
    /// Returns the raw regex pattern string.
    /// </summary>
    /// <returns>The regex pattern as a string.</returns>
    public override string ToString() => _pattern.ToString();
}
