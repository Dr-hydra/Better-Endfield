const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-2.5)", "0.0.25-poc2.5-ai-shadow-coverage",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &InitializeWithAiShadowCoverage,
    &ConfigurationChanged,
    &ShutdownWithAiShadowCoverage};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
