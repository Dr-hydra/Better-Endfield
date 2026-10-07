#include "../loaders/injector/launch_guard.h"
#include <fstream>
#include <iostream>

int wmain(int argc, wchar_t* argv[]) {
    if (argc != 2) return 2;
    const auto root = std::filesystem::absolute(argv[1]);
    std::filesystem::create_directories(root);
    const auto game = root / L"Endfield.exe";
    const auto proxy = root / L"xinput1_4.dll";
    if (std::filesystem::exists(proxy)) return 3;
    if (better_endfield::HasLocalXInputProxy(game)) return 4;
    { std::ofstream file(proxy, std::ios::binary); file << "Synthetic proxy; never loaded."; }
    if (!better_endfield::HasLocalXInputProxy(game)) return 5;
    std::filesystem::remove(proxy);
    std::filesystem::create_directory(proxy);
    if (!better_endfield::HasLocalXInputProxy(game)) return 6;
    std::filesystem::remove(proxy);
    std::cout << "Injector launch guard: 3 checks passed.\n";
    return 0;
}
