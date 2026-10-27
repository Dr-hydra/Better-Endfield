#pragma once
#include "bem_package_reader.h"
namespace BetterEndfieldNext::CustomModel {
// Versioned JSON boundary shared by CLI and future JNI adapters. A plan embeds
// its request and source generations; build re-plans before reading payloads.
bool PlanBemComposition(const nlohmann::json& request, nlohmann::json& plan, std::string& error);
bool BuildBemComposition(const nlohmann::json& plan, const nlohmann::json& defaults,
    const std::filesystem::path& output_directory, nlohmann::json& report,
    const std::function<void()>& checkpoint, std::string& error);
// Temporary preview snapshots validate the current selection only. Final builds
// always retain exhaustive reachable-variant validation.
bool DraftBemComposition(const nlohmann::json& plan, const nlohmann::json& defaults,
    const std::filesystem::path& output_directory, nlohmann::json& report,
    const std::function<void()>& checkpoint, std::string& error);
}
