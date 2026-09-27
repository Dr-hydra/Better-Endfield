#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")/../../.."
BUILD="$(mktemp -d)"; trap 'rm -rf "$BUILD"' EXIT
CXX="${CXX:-clang++}"
JAVA_HOME="${JAVA_HOME:-$(dirname "$(dirname "$(readlink -f "$(command -v javac)")")")}"
A=android/app/src/main/cpp
S=native/shared/android_compat
flags=(-std=c++20 -pthread -g -I"$A" -I"$S" -I"$S/include" -Inative/tests/android_rebuild/stubs)
"$CXX" "${flags[@]}" native/tests/android_rebuild/input_test.cpp "$S/android_win32.cpp" "$S/android_frame.cpp" -ldl -o "$BUILD/input"
"$BUILD/input"
"$CXX" "${flags[@]}" native/tests/android_rebuild/hook_test.cpp "$A/core/hook_broker.cpp" -o "$BUILD/hooks"
"$BUILD/hooks"
"$CXX" "${flags[@]}" native/tests/android_rebuild/runtime_test.cpp "$A/core/runtime.cpp" -ldl -o "$BUILD/runtime"
"$BUILD/runtime"
javac -d "$BUILD/classes" "$A/../java/dev/betterendfield/android/RuntimeSnapshot.java" native/tests/android_rebuild/snapshot_test.java
java -ea -cp "$BUILD/classes" dev.betterendfield.android.SnapshotTest
"$CXX" "${flags[@]}" -fPIC -shared -I"$JAVA_HOME/include" -I"$JAVA_HOME/include/linux" native/tests/android_rebuild/jni_test.cpp -o "$BUILD/libjni-isolation.so"
javac -d "$BUILD/game" native/tests/android_rebuild/Loader.java
javac -d "$BUILD/module" native/tests/android_rebuild/Bridge.java
javac -d "$BUILD/runner" native/tests/android_rebuild/JniIsolationTest.java
java -ea -Xcheck:jni -cp "$BUILD/runner" JniIsolationTest "$BUILD/game" "$BUILD/module" "$BUILD/libjni-isolation.so"
# Shared translation units must really compile on the Android branch of the platform shim.
for source in native/modules/{camera,ui,actions,model}/module.cpp; do
 "$CXX" "${flags[@]}" -D__ANDROID__ -Inative/shared/include -Inative/modules/ui -fsyntax-only "$source"
done
"$CXX" "${flags[@]}" -D__ANDROID__ -Inative/shared/include -Inative/modules/custom_model -I"$JAVA_HOME/include" -I"$JAVA_HOME/include/linux" -fsyntax-only "$A/native_bridge.cpp"
"$CXX" "${flags[@]}" -D__ANDROID__ -Inative/shared/include -Inative/modules/ui native/tests/android_rebuild/camera_test.cpp "$S/android_win32.cpp" "$S/android_frame.cpp" -ldl -o "$BUILD/camera"
"$BUILD/camera"
echo 'PASS Android shared-source syntax checks'
