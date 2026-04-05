# Changelog

## 0.2.0 (2026-04-05)

- Add `NamedGroup(string name, Action<PatternBuilder> inner)` method for named capture groups (`(?<name>...)`)

## 0.1.1 (2026-03-31)

- Standardize README to 3-badge format with emoji Support section
- Update CI actions to v5 for Node.js 24 compatibility
- Add GitHub issue templates, dependabot config, and PR template

## 0.1.0 (2026-03-21)

- Initial release
- Fluent `PatternBuilder` API for constructing regex patterns
- Static `Pattern` entry point with `Start()`, `StartOfLine()`, `EndOfLine()`
- Character classes: `Digit`, `Digits`, `Word`, `Words`, `Whitespace`, `Any`, `AnyOf`, `NoneOf`
- Quantifiers: `Times`, `Optional`, `OneOrMore`, `ZeroOrMore`, `Lazy`
- Grouping: `Group` (non-capturing) and `CaptureGroup` (named)
- Alternation via `Or`
- Compile to `Regex` with optional `RegexOptions`
