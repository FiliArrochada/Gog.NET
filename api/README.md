# Vendored specs

GOG publishes **no official OpenAPI spec** for its public APIs, so there is nothing
upstream to vendor verbatim. `gog-api-v2.0.0.yaml` in this folder is therefore
**hand-authored** from the live responses and the community documentation
(<https://gogapidocs.readthedocs.io>, source: <https://github.com/Yepoleb/gogapidocs>),
covering only the endpoints this SDK implements.

Consequences, deliberately diverging from the workspace SDK guideline:

- The file is append-only all the same: when GOG changes shape, add
  `gog-api-v<next>.yaml` next to it rather than editing history.
- There is **no `scripts/check-spec-version.cs` and no spec-drift workflow** — with no
  machine-readable upstream there is nothing to diff against. Drift is caught by the
  fixture tests in `src/GogNet.Test/Fixtures`, which deserialize captured real responses.
