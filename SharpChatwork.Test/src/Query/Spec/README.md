# Spec Conformance Tests

This folder contains tests that verify the **specification** as documented in `docs/apis/` —
not the current implementation. Each test name and `[Skip]` reason cites the doc that defines
the expected behavior.

Tests marked `[Skip("impl-bug: ...")]` describe a known divergence between the implementation
and the documented Chatwork API. They will start passing automatically once the underlying
implementation bug is fixed; this is intentional.

Existing tests in `Query/*.cs` lock the **current** behavior of the implementation, so a fix
to one of these bugs will require updating the matching regression test.
