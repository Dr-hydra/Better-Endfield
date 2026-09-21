#pragma once
#include <cstddef>
#include <string>

namespace betterendfield {
bool SubmitRuntimeCommand(const char* payload, size_t size);
bool ConsumeRuntimeCommand(std::string& payload);
void AcknowledgeRuntimeCommand(const char* status);
std::string CopyRuntimeCommandStatus();
}
