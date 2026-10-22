# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

`Gog.NET` is a hand-maintained Refit + System.Text.Json client SDK for the **public,
anonymous** GOG.com APIs (v2 games, v1 products), packaged as `GogNet`.

## Commands

```powershell
dotnet build GogNet.sln
dotnet test GogNet.sln
dotnet test --filter "FullyQualifiedName~GamesApiTests"   # single class
```

Tests run on `net10.0` and `net48`; the net48 leg needs a Windows targeting pack,
which is why CI is `windows-latest`.

## Deliberate deviations

- **No `scripts/check-spec-version.cs` and no spec-drift workflow.** GOG publishes no
  OpenAPI document, so there is nothing upstream to diff; `api/gog-api-v2.0.0.yaml` is
  hand-authored (see `api/README.md`) and drift is caught by the fixture tests instead.

## Conventions that matter here

- **No auth anywhere.** These endpoints are anonymous. Do not add token handling, and do not
  add authenticated GOG endpoints (embed.gog.com, account libraries) to this SDK — that
  belongs with the consumer's own OAuth code.
- Models are `partial` classes with `[JsonPropertyName]` on every property plus the standard
  `ToJson`/`ToString`/`Equals`/`GetHashCode`/`Validate` boilerplate. Match the surrounding
  files when adding one; don't trim it.
- GOG image hrefs are **protocol-relative** (`//images...`) and sometimes templated
  (`{formatter}`, `{ext}`). Never hand a raw `href` to an image loader — use
  `Link.ResolveHref()` / `Game.BoxArtUrl` / `Product.TileLogoUrl`, which normalise both.
- The `ids` parameter of `GET /v2/games` is **one comma-joined string**, not repeated
  params. The slice tests lock that down; keep them passing.
- Keep `Refit` and `Refit.Reflection` on the same version (in `Directory.Packages.props`).
- `<Version>` in `src/GogNet/GogNet.csproj` is the release version: bump the 4th component
  for client-only changes, the first three only when the vendored spec version changes.
