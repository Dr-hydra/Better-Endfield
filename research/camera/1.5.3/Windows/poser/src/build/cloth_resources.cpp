#include "cloth_resources.h"

int wmain(int argc, wchar_t **argv) {
  try {
    eiem_build::fs::path repo = L".", dll, report;
    bool pack = false;
    for (int n = 1; n < argc; ++n) {
      const std::wstring option = argv[n];
      if (option == L"--pack") pack = true;
      else if ((option == L"--repo" || option == L"--dll" || option == L"--report") && n + 1 < argc) {
        const auto value = eiem_build::fs::path(argv[++n]);
        if (option == L"--repo") repo = value;
        else if (option == L"--dll") dll = value;
        else report = value;
      } else throw std::runtime_error("usage: cloth_resources --repo <root> [--pack] [--dll <file> [--report <file>]]");
    }
    eiem_build::Require(pack || !dll.empty(), "select --pack or --dll");
    eiem_build::Require(report.empty() || !dll.empty(), "--report requires --dll");
    if (pack) eiem_build::PackAll(repo);
    if (!dll.empty()) eiem_build::Verify(repo, dll, report);
    return 0;
  } catch (const std::exception &error) {
    std::cerr << "ERROR: cloth resource build: " << error.what() << '\n';
    return 1;
  }
}
