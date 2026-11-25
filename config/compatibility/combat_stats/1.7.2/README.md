# 1.7.2 MemoryPack Schema

This schema describes the SkillData and BuffData fields reachable in the 1.7.2 Windows client. It contains 851 types and 18 explicit union registrations recovered from the current formatter constructors. Every registered union target resolves in the schema.

Regenerate with `extract_memorypack_schema.py`, then use `extract_memorypack_unions.py` to read the actual `Dictionary<Type, ushort>.Add` tags. Include newly registered union target types with `--extra-types` and repeat extraction until all targets resolve. Never infer union tags from type ordering.

The update used the installed 1.7.2 `GameAssembly.dll` and metadata for this targeted schema extraction. Normal combat export consumes this checked-in schema and current Table, JsonData, and StringPool inputs.
