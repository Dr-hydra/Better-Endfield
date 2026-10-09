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
"$CXX" "${flags[@]}" -Inative/shared/include native/tests/android_rebuild/hook_test.cpp "$A/core/hook_broker.cpp" -o "$BUILD/hooks"
"$BUILD/hooks"
"$CXX" "${flags[@]}" native/tests/android_rebuild/runtime_test.cpp "$A/core/runtime.cpp" native/tests/android_rebuild/log_stub.cpp -ldl -o "$BUILD/runtime"
"$BUILD/runtime"
javac -d "$BUILD/classes" "$A/../java/dev/betterendfield/next/RuntimeSnapshot.java" native/tests/android_rebuild/snapshot_test.java
java -ea -cp "$BUILD/classes" dev.betterendfield.next.SnapshotTest
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
eiem=(-Inative/modules/camera/eiem -Inative/modules/camera/eiem/upstream -Inative/modules/camera/eiem/compat)
objects=()
for source in native/modules/camera/eiem/eiem_body.cpp native/modules/camera/eiem/eiem_slot{0,1,2,3}.cpp; do
 object="$BUILD/$(basename "$source").o"
 "$CXX" "${flags[@]}" "${eiem[@]}" -D__ANDROID__ -fno-char8_t -Inative/shared/include -c "$source" -o "$object"
 objects+=("$object")
done
"$CXX" "${flags[@]}" -D__ANDROID__ -Inative/shared/include -Inative/modules/ui native/tests/android_rebuild/camera_test.cpp "${objects[@]}" native/shared/host/pose_lease.cpp "$S/android_win32.cpp" "$S/android_frame.cpp" -ldl -o "$BUILD/camera"
"$BUILD/camera"
for test in ui_layout ui_mouse_diagnostics ui_pc_mouse_runtime; do
 "$CXX" "${flags[@]}" -D__ANDROID__ -Inative/shared/include -Inative/modules/ui "native/tests/android_rebuild/${test}_test.cpp" "$S/android_win32.cpp" "$S/android_frame.cpp" "$S/touch_input_android.cpp" -ldl -o "$BUILD/$test"
 "$BUILD/$test"
done
"$CXX" "${flags[@]}" native/tests/android_rebuild/pc_mouse_state_test.cpp -o "$BUILD/pc_mouse_state"
"$BUILD/pc_mouse_state"
echo 'PASS Android shared-source syntax checks'
