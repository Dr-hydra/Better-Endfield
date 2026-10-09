#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/../../.."
BUILD="$(mktemp -d)"; trap 'rm -rf "$BUILD"' EXIT
javac -encoding UTF-8 -d "$BUILD" \
 android/app/src/main/java/dev/betterendfield/next/BemImportStream.java \
 android/app/src/main/java/dev/betterendfield/next/BemImportArchive.java \
 native/tests/bem_import/BemImportStreamTest.java \
 native/tests/bem_import/BemImportArchiveTest.java
java -ea -cp "$BUILD" dev.betterendfield.next.BemImportStreamTest
java -ea -cp "$BUILD" dev.betterendfield.next.BemImportArchiveTest
python3 native/tests/bem_import/manifest_test.py
