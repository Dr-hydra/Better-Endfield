const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-2.5)", "0.0.34-poc2.5-preheat-control-trace",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &InitializeWithPreheatControlTrace,
    &ConfigurationChanged,
    &ShutdownWithPreheatControlTrace};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
