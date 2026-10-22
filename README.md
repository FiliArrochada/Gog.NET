# Gog.NET

[![CI](https://github.com/FiliArrochada/Gog.NET/actions/workflows/ci.yml/badge.svg)](https://github.com/FiliArrochada/Gog.NET/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/GogNet.svg)](https://www.nuget.org/packages/GogNet/)

A **hand-maintained** .NET client for the public GOG.com APIs — Refit interfaces plus
System.Text.Json models, kept in sync with the API by hand (no code generator in the repo).
It covers the anonymous endpoints a launcher needs: **v2 games** for real box art and store
links, and **v1 products** for the older Galaxy tile logos.

Nothing here touches an account: no tokens, no user data, no authenticated endpoints.

- **API version:** GOG v2 games / v1 products, as captured in [`api/gog-api-v2.0.0.yaml`](api/gog-api-v2.0.0.yaml)
- **SDK version:** `2.0.0.1` — `Major.Minor.Patch` tracks the vendored spec version; the
  4th component is bumped for client-only changes against the same API version.
- **Spec provenance:** GOG publishes no OpenAPI document; the vendored spec is hand-authored
  from live responses and the community docs — see [`api/README.md`](api/README.md).

## Supported frameworks

`net10.0`, `net48`.

Dependencies: `Refit` 15, `Refit.Reflection` (version-locked to Refit — net48 has no
source-generated stubs), `System.Text.Json`, `System.ComponentModel.Annotations`.

## Install

```powershell
dotnet add package GogNet
```

## Usage

```csharp
using GogNet.Api;
using GogNet.Client;
using Refit;

using var http = GogRefitSettings.CreateHttpClient();          // https://api.gog.com/
var games = RestService.For<IGamesApi>(http, GogRefitSettings.Create());

// Real portrait box art for an installed GOG game (product id from goggame-<id>.info)
var game = await games.GetGameAsync("1207658930");
string? coverUrl = game.BoxArtUrl;                             // null when GOG has none

// Older products sometimes only have the Galaxy tile logo
var products = RestService.For<IProductsApi>(http, GogRefitSettings.Create());
var product = await products.GetProductAsync("1207658930", expand: "images");
string? fallback = product.TileLogoUrl;
```

`GetGamesAsync` takes the ids **pre-joined with commas** because that is what GOG expects:

```csharp
var many = await games.GetGamesAsync("1207658930,1425308948");
foreach (var g in many.Games)
    Console.WriteLine($"{g.Slug}: {g.BoxArtUrl}");
```

Consumers own the `HttpClient`; add `DelegatingHandler`s (retry, caching, logging) there.

## Tests

`dotnet test GogNet.sln` — xUnit v3, never hits the network. Refit vertical-slice tests
assert the outgoing method/path/query against a capturing handler, model tests round-trip
through the shared serializer options, and fixture tests deserialize captured real
responses (these are what catch upstream shape drift, since there is no spec to diff).

Single class: `dotnet test --filter "FullyQualifiedName~GamesApiTests"`.

## License

MIT
