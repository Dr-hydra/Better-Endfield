const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-2.5)", "0.0.22-poc2.5-upload-ab-diag",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &InitializeWithTwinDiag,
    &ConfigurationChanged,
    &ShutdownWithTwinDiag};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
