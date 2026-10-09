// Exercise the production Raw Input readers without sending desktop input.
#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <array>

using namespace BetterEndfieldNext::CameraModule;

namespace {
RAWINPUT packet{};
UINT data_result = 0, buffer_result = 0, original_calls = 0;
alignas(RAWINPUT) std::array<BYTE, 256> packets{};
UINT packet_bytes = 0;

UINT WINAPI ReadData(HRAWINPUT, UINT command, LPVOID data, PUINT size, UINT) {
    ++original_calls;
    if (data_result == UINT(-1)) {
        SetLastError(ERROR_INSUFFICIENT_BUFFER);
        return data_result;
    }
    const UINT bytes = command == RID_HEADER ? sizeof(RAWINPUTHEADER) : packet.header.dwSize;
    if (!data) { *size = bytes; return 0; }
    std::memcpy(data, &packet, std::min(*size, bytes));
    *size = bytes;
    return command == RID_HEADER ? bytes : data_result;
}

UINT WINAPI ReadBuffer(PRAWINPUT data, PUINT size, UINT) {
    ++original_calls;
    if (buffer_result == UINT(-1)) return buffer_result;
    if (!data) { *size = packet_bytes; return 0; }
    std::memcpy(data, packets.data(), std::min(*size, packet_bytes));
    return buffer_result;
}

void Reset() {
    g_input_focused = true;
    g_mouse_capture = true;
    g_keyboard_hook_active = false;
    g_raw_keyboard_seen = false;
    g_pad_down = 0;
    g_nav_down = 0;
    ClearMouseInput();
    original_calls = 0;
    g_original_raw_input_data = &ReadData;
    g_original_raw_input_buffer = &ReadBuffer;
}

RAWINPUT Mouse(LONG x, LONG y, short wheel = 0, USHORT flags = MOUSE_MOVE_RELATIVE) {
    RAWINPUT input{};
    input.header.dwType = RIM_TYPEMOUSE;
    input.header.dwSize = offsetof(RAWINPUT, data) + sizeof(RAWMOUSE);
    input.data.mouse.usFlags = flags;
    input.data.mouse.lLastX = x;
    input.data.mouse.lLastY = y;
    if (wheel) {
        input.data.mouse.usButtonFlags = RI_MOUSE_WHEEL;
        input.data.mouse.usButtonData = static_cast<USHORT>(wheel);
    }
    return input;
}

RAWINPUT Key(USHORT scan, USHORT flags, USHORT vk) {
    RAWINPUT input{};
    input.header.dwType = RIM_TYPEKEYBOARD;
    input.header.dwSize = offsetof(RAWINPUT, data) + sizeof(RAWKEYBOARD);
    input.data.keyboard.MakeCode = scan;
    input.data.keyboard.Flags = flags;
    input.data.keyboard.VKey = vk;
    return input;
}

UINT Deliver(const RAWINPUT& input, UINT command = RID_INPUT) {
    packet = input;
    data_result = packet.header.dwSize;
    RAWINPUT output{};
    UINT size = sizeof(output);
    return DetourRawInputData(nullptr, command, &output, &size, sizeof(RAWINPUTHEADER));
}
}

int main() {
    Reset();
    // Relative movement works regardless of the cursor's current/locked position.
    CHECK(Deliver(Mouse(12, -7, 120)) == packet.header.dwSize);
    CHECK(original_calls == 1 && g_mouse_dx == 12 && g_mouse_dy == -7 && g_mouse_wheel == 120);
    Deliver(Mouse(-3, 2, -120));
    CHECK(g_mouse_dx == 9 && g_mouse_dy == -5 && g_mouse_wheel == 0);
    Deliver(Mouse(65535, 32768, 120, MOUSE_MOVE_ABSOLUTE));
    CHECK(g_mouse_dx == 9 && g_mouse_dy == -5 && g_mouse_wheel == 120);
    ClearMouseInput();
    g_mouse_capture = false;
    Deliver(Mouse(5, 6, 120));
    CHECK(g_mouse_dx == 0 && g_mouse_dy == 0 && g_mouse_wheel == 0);
    g_mouse_capture = true;
    g_input_focused = false;
    Deliver(Mouse(5, 6, 120));
    CHECK(g_mouse_dx == 0 && g_mouse_dy == 0 && g_mouse_wheel == 0);

    Reset();
    g_playback = FreePlayback::None;
    g_free_target = {};
    g_free_target.fov = 60;
    g_mouse_sensitivity = 0.5f;
    g_mouse_invert_y = false;
    g_free_smoothing = 0;
    g_free_last_step = NowSeconds() - 0.02;
    Deliver(Mouse(8, -6, 120));
    StepFreeCamera();
    CHECK(nearly(g_free_target.yaw, 4) && nearly(g_free_target.pitch, -3));
    CHECK(nearly(g_free_view.fov, 60 - kFreeFovWheelStep));
    CHECK(g_mouse_dx == 0 && g_mouse_dy == 0 && g_mouse_wheel == 0);

    Reset();
    // Queries, header reads, failures and short packets do not duplicate input.
    packet = Mouse(8, 9);
    data_result = packet.header.dwSize;
    UINT size = 0;
    CHECK(DetourRawInputData(nullptr, RID_INPUT, nullptr, &size, sizeof(RAWINPUTHEADER)) == 0);
    CHECK(size == packet.header.dwSize && g_mouse_dx == 0);
    Deliver(packet, RID_HEADER);
    CHECK(g_mouse_dx == 0);
    data_result = UINT(-1);
    RAWINPUT output{};
    size = sizeof(output);
    CHECK(DetourRawInputData(nullptr, RID_INPUT, &output, &size, sizeof(RAWINPUTHEADER)) == UINT(-1));
    CHECK(GetLastError() == ERROR_INSUFFICIENT_BUFFER && g_mouse_dx == 0);
    data_result = sizeof(RAWINPUTHEADER);
    size = sizeof(output);
    DetourRawInputData(nullptr, RID_INPUT, &output, &size, sizeof(RAWINPUTHEADER));
    CHECK(g_mouse_dx == 0);

    constexpr USHORT nav_scans[]{0x48, 0x50, 0x4B, 0x4D, 0x49, 0x51};
    for (size_t index = 0; index < std::size(nav_scans); ++index) {
        Reset();
        Deliver(Key(nav_scans[index], RI_KEY_E0, static_cast<USHORT>(kNavVirtualKeys[index])));
        CHECK(KeyDown(kNavVirtualKeys[index]) && g_pad_down == 0);
        Deliver(Key(nav_scans[index], RI_KEY_E0 | RI_KEY_BREAK,
            static_cast<USHORT>(kNavVirtualKeys[index])));
        CHECK(!KeyDown(kNavVirtualKeys[index]) && g_nav_down == 0);
    }
    constexpr USHORT pad_scans[]{0x52, 0x4F, 0x50, 0x51, 0x4B, 0x4C, 0x4D,
        0x47, 0x48, 0x49, 0x53, 0x4E, 0x4A, 0x37, 0x35, 0x1C};
    constexpr USHORT numlock_off[]{VK_INSERT, VK_END, VK_DOWN, VK_NEXT, VK_LEFT,
        VK_CLEAR, VK_RIGHT, VK_HOME, VK_UP, VK_PRIOR, VK_DELETE, VK_ADD,
        VK_SUBTRACT, VK_MULTIPLY, VK_DIVIDE, VK_RETURN};
    for (bool numlock : {false, true}) {
        for (size_t index = 0; index < std::size(pad_scans); ++index) {
            Reset();
            const USHORT flags = index >= 14 ? RI_KEY_E0 : 0;
            const USHORT vk = numlock ? static_cast<USHORT>(
                index == 15 ? VK_RETURN : kPadVirtualKeys[index]) : numlock_off[index];
            Deliver(Key(pad_scans[index], flags, vk));
            CHECK(KeyDown(kPadVirtualKeys[index]) && g_nav_down == 0);
            Deliver(Key(pad_scans[index], flags | RI_KEY_BREAK, vk));
            CHECK(!KeyDown(kPadVirtualKeys[index]) && g_pad_down == 0);
        }
    }
    Reset();
    Deliver(Key(0x1C, 0, VK_RETURN));
    CHECK(!KeyDown(kVkNumpadEnter));
    Deliver(Key(0x49, RI_KEY_E1, VK_PAUSE));
    CHECK(g_nav_down == 0 && g_pad_down == 0);

    Reset();
    // Buffered Unity reads deliver both devices, with Windows packet alignment.
    const RAWINPUT first = Mouse(4, -2, -120);
    const RAWINPUT second = Key(0x49, RI_KEY_E0, VK_PRIOR);
    std::memcpy(packets.data(), &first, first.header.dwSize);
    const size_t next = (first.header.dwSize + alignof(RAWINPUT) - 1) & ~(alignof(RAWINPUT) - 1);
    std::memcpy(packets.data() + next, &second, second.header.dwSize);
    packet_bytes = static_cast<UINT>(next + second.header.dwSize);
    buffer_result = 2;
    alignas(RAWINPUT) std::array<BYTE, 256> result{};
    size = static_cast<UINT>(result.size());
    CHECK(DetourRawInputBuffer(reinterpret_cast<PRAWINPUT>(result.data()), &size,
        sizeof(RAWINPUTHEADER)) == 2);
    CHECK(original_calls == 1 && g_mouse_dx == 4 && g_mouse_dy == -2 && g_mouse_wheel == -120);
    CHECK(KeyDown(VK_PRIOR));
    ClearMouseInput();
    g_nav_down = 0;
    size = 0;
    CHECK(DetourRawInputBuffer(nullptr, &size, sizeof(RAWINPUTHEADER)) == 0);
    CHECK(size == packet_bytes && g_mouse_dx == 0 && g_nav_down == 0);
    size = first.header.dwSize;
    DetourRawInputBuffer(reinterpret_cast<PRAWINPUT>(result.data()), &size, sizeof(RAWINPUTHEADER));
    CHECK(g_mouse_dx == 4 && g_nav_down == 0); // second packet is outside the supplied buffer

    // Hook procedures in a DLL must use that DLL, rather than the process EXE.
    const HMODULE user32 = GetModuleHandleW(L"user32.dll");
    CHECK(user32 && user32 != GetModuleHandleW(nullptr));
    const auto callback = reinterpret_cast<HOOKPROC>(GetProcAddress(user32, "DefWindowProcW"));
    MEMORY_BASIC_INFORMATION region{};
    // Some Windows builds forward this export to another DLL; the memory
    // allocation independently identifies the DLL that actually owns the code.
    CHECK(callback && VirtualQuery(reinterpret_cast<LPCVOID>(callback), &region, sizeof(region)));
    CHECK(InputHookModule(callback) == region.AllocationBase);
    CHECK(InputHookModule(callback) != GetModuleHandleW(nullptr));
    CHECK(InputHookModule(&KeyboardHook) == GetModuleHandleW(nullptr)); // test TU is an EXE
    std::cout << "PASS production Windows camera input: " << checks << " checks\n";
}
