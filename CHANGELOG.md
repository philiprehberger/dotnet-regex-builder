# Changelog

## 0.1.0 (2026-03-21)

- Initial release
- Fluent `PatternBuilder` API for constructing regex patterns
- Static `Pattern` entry point with `Start()`, `StartOfLine()`, `EndOfLine()`
- Character classes: `Digit`, `Digits`, `Word`, `Words`, `Whitespace`, `Any`, `AnyOf`, `NoneOf`
- Quantifiers: `Times`, `Optional`, `OneOrMore`, `ZeroOrMore`, `Lazy`
- Grouping: `Group` (non-capturing) and `CaptureGroup` (named)
- Alternation via `Or`
- Compile to `Regex` with optional `RegexOptions`
