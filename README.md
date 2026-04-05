# Philiprehberger.RegexBuilder

[![CI](https://github.com/philiprehberger/dotnet-regex-builder/actions/workflows/ci.yml/badge.svg)](https://github.com/philiprehberger/dotnet-regex-builder/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/Philiprehberger.RegexBuilder.svg)](https://www.nuget.org/packages/Philiprehberger.RegexBuilder)
[![Last updated](https://img.shields.io/github/last-commit/philiprehberger/dotnet-regex-builder)](https://github.com/philiprehberger/dotnet-regex-builder/commits/main)

Fluent, readable regex construction — build patterns programmatically and compile to standard Regex objects.

## Installation

```bash
dotnet add package Philiprehberger.RegexBuilder
```

## Usage

```csharp
using Philiprehberger.RegexBuilder;

var emailRegex = Pattern.Start()
    .Words()
    .Literal("@")
    .Words()
    .Literal(".")
    .Word().Times(2, 6)
    .Build();

bool isValid = emailRegex.IsMatch("user@example.com"); // true
```

### Build a Pattern

```csharp
using Philiprehberger.RegexBuilder;

var pattern = Pattern.StartOfLine()
    .Literal("Hello")
    .Whitespace()
    .Words()
    .EndOfLine()
    .Build();

bool match = pattern.IsMatch("Hello World"); // true
```

### Character Classes

```csharp
using Philiprehberger.RegexBuilder;

var hexColor = Pattern.Start()
    .Literal("#")
    .AnyOf("0123456789abcdefABCDEF").Times(6)
    .Build();

bool isHex = hexColor.IsMatch("#ff00cc"); // true

var noDigits = Pattern.Start()
    .NonDigit().OneOrMore()
    .Build();

bool alpha = noDigits.IsMatch("hello"); // true
```

### Groups and Quantifiers

```csharp
using Philiprehberger.RegexBuilder;

var datePattern = Pattern.Start()
    .CaptureGroup("year", b => b.Digit().Times(4))
    .Literal("-")
    .CaptureGroup("month", b => b.Digit().Times(2))
    .Literal("-")
    .CaptureGroup("day", b => b.Digit().Times(2))
    .Build();

var match = datePattern.Match("2026-03-21");
string year = match.Groups["year"].Value;  // "2026"
string month = match.Groups["month"].Value; // "03"

var timePattern = Pattern.Start()
    .NamedGroup("hour", b => b.Digit().Times(2))
    .Literal(":")
    .NamedGroup("minute", b => b.Digit().Times(2))
    .Build();

var time = timePattern.Match("14:30");
string hour = time.Groups["hour"].Value;   // "14"
string minute = time.Groups["minute"].Value; // "30"

var protocol = Pattern.Start()
    .Group(b => b.Literal("http").Group(b2 => b2.Literal("s")).Optional())
    .Literal("://")
    .Build();

bool matchesHttp = protocol.IsMatch("http://example.com");  // true
bool matchesHttps = protocol.IsMatch("https://example.com"); // true
```

## API

### `Pattern`

| Method | Description |
|--------|-------------|
| `Start()` | Begin building a new pattern |
| `StartOfLine()` | Begin with a start-of-line anchor (`^`) |
| `EndOfLine()` | Begin with an end-of-line anchor (`$`) |

### `PatternBuilder`

| Method | Description |
|--------|-------------|
| `Literal(string text)` | Match exact text (auto-escaped) |
| `Digit()` | Match a single digit (`\d`) |
| `Digits()` | Match one or more digits (`\d+`) |
| `NonDigit()` | Match a non-digit (`\D`) |
| `Word()` | Match a word character (`\w`) |
| `Words()` | Match one or more word characters (`\w+`) |
| `NonWord()` | Match a non-word character (`\W`) |
| `Whitespace()` | Match a whitespace character (`\s`) |
| `NonWhitespace()` | Match a non-whitespace character (`\S`) |
| `Any()` | Match any character (`.`) |
| `AnyOf(string chars)` | Match any character in the set |
| `NoneOf(string chars)` | Match any character not in the set |
| `Times(int n)` | Repeat previous element exactly `n` times |
| `Times(int min, int max)` | Repeat previous element between `min` and `max` times |
| `Optional()` | Make previous element optional (`?`) |
| `OneOrMore()` | Repeat previous element one or more times (`+`) |
| `ZeroOrMore()` | Repeat previous element zero or more times (`*`) |
| `Group(Action<PatternBuilder> inner)` | Non-capturing group |
| `CaptureGroup(string name, Action<PatternBuilder> inner)` | Named capturing group |
| `NamedGroup(string name, Action<PatternBuilder> inner)` | Named capturing group (alias for `CaptureGroup`) |
| `Or(Action<PatternBuilder> alt)` | Alternation with another pattern |
| `StartOfLine()` | Append start-of-line anchor (`^`) |
| `EndOfLine()` | Append end-of-line anchor (`$`) |
| `Lazy()` | Make previous quantifier lazy |
| `Build(RegexOptions? options)` | Compile to a `Regex` object |
| `ToString()` | Return the raw pattern string |

## Development

```bash
dotnet build src/Philiprehberger.RegexBuilder.csproj --configuration Release
```

## Support

If you find this project useful:

⭐ [Star the repo](https://github.com/philiprehberger/dotnet-regex-builder)

🐛 [Report issues](https://github.com/philiprehberger/dotnet-regex-builder/issues?q=is%3Aissue+is%3Aopen+label%3Abug)

💡 [Suggest features](https://github.com/philiprehberger/dotnet-regex-builder/issues?q=is%3Aissue+is%3Aopen+label%3Aenhancement)

❤️ [Sponsor development](https://github.com/sponsors/philiprehberger)

🌐 [All Open Source Projects](https://philiprehberger.com/open-source-packages)

💻 [GitHub Profile](https://github.com/philiprehberger)

🔗 [LinkedIn Profile](https://www.linkedin.com/in/philiprehberger)

## License

[MIT](LICENSE)
