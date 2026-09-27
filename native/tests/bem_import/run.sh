#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/../../.."
BUILD="$(mktemp -d)"; trap 'rm -rf "$BUILD"' EXIT
javac -encoding UTF-8 -d "$BUILD" \
 android/app/src/main/java/dev/betterendfield/android/BemImportStream.java \
 native/tests/bem_import/BemImportStreamTest.java
java -ea -cp "$BUILD" dev.betterendfield.android.BemImportStreamTest
python3 native/tests/bem_import/manifest_test.py
