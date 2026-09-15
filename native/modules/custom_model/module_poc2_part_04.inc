const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-2.12)", "0.0.52-poc2.12-multi-lod-bone-elevation",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &InitializeWithAssetDeliveryTrace,
    &ConfigurationChanged,
    &ShutdownWithAssetDeliveryTrace};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
