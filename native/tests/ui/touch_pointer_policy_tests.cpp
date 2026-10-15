#include <Windows.h>
#include <cstdio>
#include <cstring>
#include <stdexcept>
#include <string>
#include <unordered_map>

#include "../../modules/ui/touch_pointer_policy.cpp"

namespace {
using namespace BetterEndfieldNext::UiModule::TouchPointerPolicy;

struct UiModule { void* header[2]{}; int32_t mode = 1; };
int32_t enum_value = 2;
bool missing_contract = false;
bool cannot_pin = false;
int last_mode = -1;
int processed = 0;
uint32_t next_handle = 0;
std::unordered_map<uint32_t, void*> roots;
std::unordered_map<void*, bool> pressed;
void* expected_method = reinterpret_cast<void*>(0x8888);

void Check(bool ok, const char* message) { if (!ok) throw std::runtime_error(message); }
void LogTest(const char*) {}
void __fastcall ProcessOriginal(void* instance, void* method) {
    Check(method == expected_method, "Process MethodInfo changed");
    last_mode = static_cast<UiModule*>(instance)->mode;
    ++processed;
}
int32_t __fastcall RouteOriginal(void* instance, void* control, bool create, void* method) {
    Check(method == expected_method, "route MethodInfo changed");
    last_mode = static_cast<UiModule*>(instance)->mode;
    // A tiny routing model exercises the reported failure: a mouse event must
    // not replace the record holding a pending touch release.
    if (last_mode == 1) pressed.clear();
    pressed[control] = create;
    return 37;
}
BE_Result BE_CALL ResolveField(void*, const BE_FieldDescriptorV1* descriptor, BE_ResolvedFieldV1* result) {
    if (missing_contract) return BE_Result_NotFound;
    Check(std::string(descriptor->assembly_name) == "Unity.InputSystem.dll", "wrong assembly");
    if (std::string(descriptor->field_name) == "m_PointerBehavior") {
        result->field_info = reinterpret_cast<void*>(1);
        result->offset = offsetof(UiModule, mode);
    } else {
        Check(std::string(descriptor->field_name) == "AllPointersAsIs", "wrong enum field");
        result->field_info = &enum_value;
    }
    return BE_Result_Ok;
}
BE_Result BE_CALL ResolveMethod(void*, const BE_MethodDescriptorV1* descriptor, BE_ResolvedMethodV1* result) {
    const bool route = std::string(descriptor->method_name) == "GetPointerStateIndexFor";
    if (route) {
        Check(descriptor->parameter_count == 2, "wrong route arity");
        Check(std::string(descriptor->parameter_types) == "UnityEngine.InputSystem.InputControl|System.Boolean", "wrong route signature");
    }
    result->method_info = expected_method;
    result->method_pointer = route ? reinterpret_cast<void*>(&RouteOriginal) : reinterpret_cast<void*>(&ProcessOriginal);
    return BE_Result_Ok;
}
BE_Result BE_CALL Hook(void*, const char*, void* target, void*, void** original) {
    *original = target;
    return BE_Result_Ok;
}
void* BE_CALL GetField(void*, const void* field, void*) { return const_cast<void*>(field); }
void* BE_CALL UnboxValue(void*, void* boxed) { return boxed; }
uint32_t BE_CALL Pin(void*, void* instance, int pinned) {
    Check(pinned == 1, "retained pointer was not pinned");
    if (cannot_pin) return 0;
    roots[++next_handle] = instance;
    return next_handle;
}
void BE_CALL Unpin(void*, uint32_t handle) {
    Check(roots.erase(handle) == 1, "invalid or duplicate root release");
}

BE_HostApiV1 Host() {
    BE_HostApiV1 host{};
    host.abi_version = 1;
    host.resolve_field = &ResolveField;
    host.resolve_method = &ResolveMethod;
    host.create_hook = &Hook;
    host.field_get_value_object = &GetField;
    host.object_unbox = &UnboxValue;
    host.gchandle_new = &Pin;
    host.gchandle_free = &Unpin;
    return host;
}
}

int main() {
    try {
        using namespace BetterEndfieldNext::UiModule::TouchPointerPolicy;
        auto host = Host();
        Check(Start(&host, &LogTest), "could not start policy");
        UiModule first{}, second{};
        second.mode = 0;
        auto finger1 = reinterpret_cast<void*>(11);
        auto finger2 = reinterpret_cast<void*>(22);
        auto mouse = reinterpret_cast<void*>(33);
        Check(DetourRoute(&first, finger1, true, expected_method) == 37 && first.mode == 1 && roots.empty(), "disabled policy changed input");
        std::puts("PASS disabled mode preserves behavior, routing parameters and return value");

        SetEnabled(true);
        pressed.clear();
        DetourRoute(&first, finger1, true, expected_method);
        DetourRoute(&first, finger2, true, expected_method);
        DetourRoute(&first, finger1, false, expected_method);
        DetourRoute(&first, mouse, true, expected_method);
        Check(last_mode == 2 && pressed.size() == 3 && !pressed.at(finger1) && pressed.at(finger2), "touch release or second finger lost on mouse event");
        std::puts("PASS policy applies before first input; mouse events preserve pending release and other fingers in routing model");

        DetourProcess(&second, expected_method);
        Check(second.mode == 2 && processed == 1 && roots.size() == 2, "separate UI module not covered");
        SetEnabled(false);
        DetourProcess(&first, expected_method);
        DetourProcess(&second, expected_method);
        Check(first.mode == 1 && second.mode == 0 && roots.empty(), "hot disable did not restore each original setting");
        std::puts("PASS independent UI modules and hot disable restore their own modes and free roots");

        UiModule native{};
        native.mode = 2;
        SetEnabled(true);
        DetourProcess(&native, expected_method);
        Check(roots.empty(), "preexisting independent mode acquired restoration ownership");
        DetourProcess(&first, expected_method);
        first.mode = 0;
        DetourProcess(&first, expected_method);
        SetEnabled(false);
        DetourProcess(&first, expected_method);
        Check(first.mode == 0 && native.mode == 2 && roots.empty(), "later game setting not preserved");
        std::puts("PASS game-selected modes are preserved when the override is removed");

        SetEnabled(true);
        cannot_pin = true;
        DetourProcess(&second, expected_method);
        Check(second.mode == 0 && roots.empty(), "failed pin changed object state");
        cannot_pin = false;
        DetourProcess(&second, expected_method);
        Stop();
        Check(second.mode == 0 && roots.empty(), "shutdown left an override/root");
        std::puts("PASS failed retention leaves input unchanged; shutdown restores and releases all ownership");

        missing_contract = true;
        Check(!Start(&host, &LogTest), "missing metadata accepted");
        SetEnabled(true);
        DetourProcess(&second, expected_method);
        Check(second.mode == 0, "missing metadata wrote an object");
        missing_contract = false;
        enum_value = 7; // Verify discovery, rather than assuming Unity's current integer.
        Check(Start(&host, &LogTest), "restart failed");
        DetourProcess(&second, expected_method);
        Check(second.mode == 7, "enum value was hard-coded");
        Stop();
        Check(second.mode == 0 && roots.empty(), "final restore failed");
        std::puts("PASS missing metadata fails closed; enum values resolve dynamically");
        return 0;
    } catch (const std::exception& error) {
        std::fprintf(stderr, "FAIL %s\n", error.what());
        return 1;
    }
}
