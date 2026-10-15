// Read a PE file into an RVA-addressed byte vector. Never LoadLibrary or run code.
#include <Windows.h>
#include "native_mesh_layout.h"
#include <cstring>
#include <filesystem>
#include <fstream>
#include <iostream>
#include <iterator>
#include <stdexcept>
#include <vector>

template<class T> T Read(const std::vector<uint8_t>& data, size_t at) {
    if (at > data.size() || sizeof(T) > data.size() - at) throw std::runtime_error("truncated PE");
    T value{}; std::memcpy(&value, data.data() + at, sizeof(value)); return value;
}
int wmain(int argc, wchar_t** argv) {
    try {
        if (argc != 2) throw std::runtime_error("usage: bem-current-player-layout UnityPlayer.dll");
        std::ifstream file(std::filesystem::path(argv[1]), std::ios::binary);
        if (!file) throw std::runtime_error("cannot read UnityPlayer");
        std::vector<uint8_t> raw((std::istreambuf_iterator<char>(file)), {});
        const auto dos = Read<IMAGE_DOS_HEADER>(raw, 0);
        if (dos.e_magic != IMAGE_DOS_SIGNATURE || dos.e_lfanew <= 0) throw std::runtime_error("invalid DOS header");
        const auto nt = Read<IMAGE_NT_HEADERS64>(raw, dos.e_lfanew);
        if (nt.Signature != IMAGE_NT_SIGNATURE || nt.FileHeader.Machine != IMAGE_FILE_MACHINE_AMD64 ||
            nt.OptionalHeader.SizeOfImage > 512u * 1024u * 1024u || nt.OptionalHeader.SizeOfHeaders > raw.size())
            throw std::runtime_error("invalid x64 PE header");
        std::vector<uint8_t> mapped(nt.OptionalHeader.SizeOfImage);
        if (nt.OptionalHeader.SizeOfHeaders > mapped.size()) throw std::runtime_error("invalid headers size");
        std::memcpy(mapped.data(), raw.data(), nt.OptionalHeader.SizeOfHeaders);
        const size_t sectionOffset = dos.e_lfanew + sizeof(DWORD) + sizeof(IMAGE_FILE_HEADER) + nt.FileHeader.SizeOfOptionalHeader;
        for (unsigned n = 0; n < nt.FileHeader.NumberOfSections; ++n) {
            const auto section = Read<IMAGE_SECTION_HEADER>(raw, sectionOffset + n * sizeof(IMAGE_SECTION_HEADER));
            if (section.PointerToRawData > raw.size() || section.SizeOfRawData > raw.size() - section.PointerToRawData ||
                section.VirtualAddress > mapped.size() || section.SizeOfRawData > mapped.size() - section.VirtualAddress)
                throw std::runtime_error("invalid section bounds");
            std::memcpy(mapped.data() + section.VirtualAddress, raw.data() + section.PointerToRawData, section.SizeOfRawData);
        }
        BetterEndfieldNext::CustomModel::NativeMeshLayout layout{};
        std::string error;
        if (!BetterEndfieldNext::CustomModel::ResolveNativeMeshLayout(mapped, layout, error)) {
            std::cerr << error << '\n'; return 2;
        }
        std::cout << "{\"success\":true,\"source\":\"current UnityPlayer.dll, read-only RVA mapping\","
            << "\"bones_per_vertex_offset\":" << layout.bones_per_vertex_offset
            << ",\"agreeing_serializers\":" << layout.agreeing_serializers << "}\n";
        return 0;
    } catch (const std::exception& exception) { std::cerr << exception.what() << '\n'; return 1; }
}
