# Historical PoC only; never a production module or release input.
add_executable(BetterEndfield.CustomModelLifecycleTests EXCLUDE_FROM_ALL
    tests/custom_model_lifecycle_tests.cpp
)
better_endfield_target_defaults(BetterEndfield.CustomModelLifecycleTests)
