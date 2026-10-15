# Actions contract audit, Endfield 1.6.3 Windows

Read directly from the installed `G:\Arknights Endfield\GameAssembly.dll` and
`Endfield_Data\il2cpp_data\Metadata\global-metadata.dat`. The audit expands the
actions module's `constexpr char` names before comparing descriptors. It does
not rely on the older literal-only contract audit, start the game, alter the
installation, or calculate hashes.

Result: **`UnityEngine.AnimatorStateInfo.get_loop` is the only missing descriptor.**
The current metadata defines seven methods on that struct: `IsName`,
`get_fullPathHash`, `get_shortNameHash`, `get_normalizedTime`, `get_length`,
`get_tagHash`, and `IsTag`. There is no alternate signature for `get_loop`.

| Contract | Result |
| --- | --- |
| Method descriptors | 85 matched; `StateLoop` missing |
| Instance field descriptors | 13 matched, with valid instance offsets |
| Enum constants | 12 matched, with readable default values |
| Static animator hash fields | 4 matched, all static `System.Int32` |
| Static/instance hook ABI expectations | 29 matched |
| Explicitly requested IL2CPP PE exports | 8 matched |

`Animator.CrossFade` retains all eight expected parameters and a native entry
at RVA `0x0d4c2dac`. The movement constants are Sprint=2, Dash=20, Grounded=1;
the perform/interrupt constants are End=5, SpDashEnd=12, ForceExit=2. The
remaining constants are recorded in `actions-contract-audit.json`.

The generic definitions for `Dictionary<TKey,TValue>.ContainsKey`,
`Dictionary<TKey,TValue>.get_Item`, and `Beyond.ObjectPtr<T>.Lock` are present
with the expected argument counts. Their definition RVAs are zero, as these
methods require runtime generic instantiations. The field-type comparison
normalizes generic declaration arity suffixes, retaining the original metadata
type in the report.

This confirms the static method/field/enum contract only. Native hook execution,
generic invocations, runtime hash initialization, and actual dash behavior still
require a fresh game run. Character IDs, perform IDs, VFX names, animation timing,
and action logic IDs are asset data and cannot be verified from PE metadata.

Reproduce:

```powershell
python 'G:\Better Endfield\research\host\1.6.3\Windows\actions-contract-fix\audit_actions_contracts.py' --repo 'G:\Better Endfield' --game 'G:\Arknights Endfield' --output 'G:\Better Endfield\research\host\1.6.3\Windows\actions-contract-fix\actions-contract-audit.json'
```
