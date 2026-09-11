const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-2.5)", "0.0.27-poc2.5-resource-one-shot-probe",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &InitializeWithResourceAssetProbe,
    &ConfigurationChanged,
    &ShutdownWithResourceAssetProbe};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
