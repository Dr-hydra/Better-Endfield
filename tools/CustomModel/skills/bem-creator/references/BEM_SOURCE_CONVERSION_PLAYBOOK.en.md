# BEM source conversion playbook

This document is the **executor's work instruction**: given an EFMI / 3DMigoto source Mod, how to
read the facts step by step, how to decide, and where to stop when the evidence does not exist.
`bem_tool` answers "is the format legal"; this playbook answers "is the source usable, can identity
be established, which losses are acceptable". The two do not overlap and do not substitute for
each other.

Conversion is not a pipeline that runs to completion on its own. `convert` advances the part that
can be pushed mechanically; every remaining gate needs evidence read, a decision made, and a record
kept. **Every exit condition below is a hard gate: if it is not met, the next stage is not allowed.**

**Input**: a source Mod directory (active INIs, DDS files, possibly author-supplied HLSL and config).
**Output**: one `.bem` package, plus a conversion record stating the evidence level and the loss for
every substitution.

The artifact is produced through the tool's own route: write an editable project (`project.json` +
`payloads/`), then `pack` / `build`, then `validate`. **This document does not repeat those commands**;
it covers only "where the content comes from and on what grounds it is believed correct" — every
decision that precedes the command. See the [creator guide](BEM_CREATOR_GUIDE.en.md) for usage.

**Not covered**: field definitions (see [format specification](BEM_FORMAT_SPEC.en.md)), CLI usage,
automation tiers and recipes (see [source Mod conversion](BEM_SOURCE_MOD_CONVERSION.en.md)).

### 0.0.1 The work loop

Every gate uses the same loop. Do not skip steps.

| Step | Action | Output |
| --- | --- | --- |
| Gather | Run the matching script / read the INI / query the native snapshot | Facts, not conclusions |
| Decide | Test the facts against the criteria table | One reviewable conclusion, with evidence level L1–L4 |
| Land | Write the conclusion into a manifest field, or into the conversion record | Field value / exclusion declaration / failure record |
| Pass | All exit conditions met | Enter the next gate; otherwise stay and gather more |

**"Looks right" never replaces evidence.** What cannot be evidenced is L4 by definition: not booked,
not guessed, failure recorded. That is not a failed conversion, that is a correct result.

---

## 0. Three meta-rules

Everything below is an expansion of these three. If only three sentences are retained, retain these.

### 0.1 Readable is not correct

A package that parses and passes two-sided offline validation only proves **structural validity**.
It does not prove **visual correctness**.

Many cases pass validation and still must be withdrawn:

- A donor material copied wholesale replaces parameters nobody listed; the picture changed but the
  format is legal
- One texture bound to a semantically opposite property (source `Lightmap` landing on native `_BumpMap`)
- The right DDS in the right slot, but that slot served a different purpose in the source

**Test**: after validation, answer "for every replaced draw, on what grounds does each material
property correspond to that source item". If that cannot be answered, the work is unfinished, not successful.

### 0.2 Inferences must be graded and traceable

When no identity is exact, conversion relies on inference. Inference is not a sin; **not labelling
it and not distinguishing its strength** is.

| Level | Basis | Disposition |
| --- | --- | --- |
| **L1 exact** | Source and target byte-identical: IB bytes, index count, vertex position triangles, DDS surface | Book directly, no disclosure needed |
| **L2 computed** | Recomputed from the native snapshot with a unique hit: VB0 default descriptor, mip CRC32C + DX11 descriptor, full original IB CRC | Book, marked "computed" |
| **L3 semantic** | Same-character remainder elimination, equal index count, material semantics, source naming, author comments | Book, **each item must state its reasoning and its uncertainty** |
| **L4 assumption** | Only "it should be that one" | Never booked. Keep the failure record and wait for evidence |

L3 is an acceptable endpoint, on one condition: **every link in the reasoning chain must be
reviewable by a third party.** "Inferred from character, colour space and the unique native material
slot" is reviewable; "should be the body normal" is not.

### 0.3 Case-level inference does not become a global rule

Inference may be reused across packages of the same character, series or author, with limits:

- Reusing a verified local mapping **within one conversion task** is fine (e.g. several outfits of
  the same character sharing one set of legacy component identities)
- Writing it into a **global alias library** is not, unless the alias itself reaches L1/L2

The reason is concrete: "the only remaining target" is only unique for that one case; `ps-t15` may
be a normal map in one package and a lightmap in another. **Promoting a case to a rule copies one
guess a hundred times.**

---

## 1. The four stages

A conversion must clear four gates in order. **No gate may be skipped.** Skipping any one makes
everything after it wasted work.

### Stage zero: mechanical triage — "does this package have a hard defect"

Exit condition: every mechanical blocker has a conclusion. Five minutes here saves hours of
adaptation work that could never have succeeded.

By the measured distribution of the reference corpus, the largest failure class (46% of 48 failed
packages) is **physically missing source DDS**: an active INI declares `this = Resource_X` and the
file exists in neither the source package nor any sibling package of the same character. This
condition is fully mechanically decidable, yet was discovered by hand 22 times.

Triage answers four questions:

| Question | Criterion | Conclusion |
| --- | --- | --- |
| Any declared resource whose file does not exist? | Check every `[Resource*]` `filename` on disk | See the three-tier classification below |
| Missing resources that **nobody uses**? | Does the name appear on a draw path: `ps-tN`, `this`, `vbN`, `ib` | Unused = template residue, harmless |
| Does any index draw exceed buffer capacity? | `max(first_index + count) × stride` vs file size | Report the fact; repairability per §3.1 |
| Which family does the entry belong to? | `TextureOverride_(EntryPoint_)?ComponentN` / `..._HASH_N_N` / `LODN.HASH_N_N` | Decides whether the standard pipeline applies |

**"Missing" must be split into three tiers.** Lumping them together kills packages that could be done:

1. **Referenced on a draw path** → real blocker. Visual output is lost.
2. **Referenced only through a framework namespace** (`Resource\RabbitFx\Diffuse = ref ...`) → grade in
   three steps, in this order:
   - **Does the name carry an identity?** The `Resource-<hash>-<n>-<slot>` form plus a `hash = <hash>`
     TextureOverride in the same package means it references a game texture by name. 142 of the 250 such
     absences across the corpus are this case, and they coexist with successful conversions — **do not
     hunt for a donor in sibling packages**; resolve the identity instead.
   - **Otherwise read the slot name** (table in A.3): `Diffuse`/`Normalmap`/`Lightmap` carry baseline
     data, so a missing file there means the material lost a base map and is a blocker;
     `GlowMap`/`FXMap`/`Rainmap`/`DiscardMap` are extra channels and drop to a warning only when the
     conversion explicitly abandons that channel.
   - **When the slot is unreadable too**, fall back to the role word in the filename (`-NormalMap.dds`
     and such). That is weak evidence and the record must say the role came from the filename, not a slot.
   One successful package in the reference corpus carried 40 such absences and still completed — but that
   is a result of slots being abandoned or identities being derivable from the name, not proof that
   framework slots are harmless by nature.
3. **Referenced by nobody** → harmless. SSMT-generated exports routinely leave dozens of template
   resource blocks whose files were never written.

Two more easy misjudgements:

- **`TextureOverride_TextureN` is not a mesh entry.** 3388 of them across the corpus, more than the
  Component families combined (2310 + 1747). It is a texture-slot override; treating it as an entry
  misreads the whole package structure.
- **`Resource_..._IB` need not be named `_index`.** The criterion for an index buffer is "bound by
  `ib = ref`", not its name.

**CrossIB invalidates the IB check**: when a section contains `CustomShader_ExtractCB1` /
`RecordBones` / `RedirectCB1`, later draws consume indices redirected from elsewhere and `ib = ref X`
no longer bounds them. Without excluding this, a 2178-index buffer is reported as needing 240756.

Tool: `bem_triage.py <package dir>` (see Appendix C).

### Stage one: identity — "which thing in the game does this source data correspond to"

Exit condition: every source entry (component, hash, legacy IB) lands on a specific native
component / mesh / material slot, and the grounds can be stated.

Common misjudgements:

- Guessing identity from the `ComponentN` number. The number is the author's own labelling and has
  no relation to in-game component numbering.
- Guessing from mesh names. Something called `body` in the source is not necessarily the native body.
- Guessing from appearance. A noise map is not an effect map because it looks like noise.

### Stage two: scope — "which parts come in, which are declared excluded"

Exit condition: every source feature has a disposition: carried / hidden / excluded (with reason) /
failed (with the gap).

Common misjudgements:

- Handling only the default state. Every reachable state in the outer directory must be covered, or
  a folding reason explicitly declared.
- Using "exclusion authority" to absorb things that are not understood. Authority covers explicitly
  listed items only, never "I could not figure it out".
- Deleting required data as if it were an effect (see §8).

### Stage three: expression — "which version, which mechanism"

Exit condition: BEM version and mechanism chosen, state count within limits, no mutually exclusive
constraint triggered.

Common misjudgements:

- Expanding into N separate appearances on the first attempt instead of using conditional draws.
- Using a morph parameter to carry animation and also binding a manual slider to it (they overwrite each other).
- Not declaring a required capability, so an older parser rejects the package outright.

---

## 2. Resource identity anchoring

### 2.1 Order of evidence

From cheapest to most expensive; stop as soon as one hits.

1. **Query existing catalogs**: character catalog, approved alias library, native mip identity scan.
   Reuse an exact entry instead of redoing a large geometric comparison.
2. **Compare bytes**: source IB vs native IB, source DDS surface vs native mip, source VB stream vs
   native stream. Byte equality is the strongest evidence; one comparison settles it.
3. **Compare geometry**: position-triangle overlap count, unique position count, index range.
   Compare the **position set**, not vertex order — authors routinely reorder vertices.
4. **Compute descriptors**: recompute VB0/IB default descriptors from the native snapshot, or
   recompute texture identity from mip CRC32C + DX11 descriptor. A hit requires **uniqueness with no conflict**.
5. **Reuse across packages**: legacy entries already at L1/L2 in other packages of the same character
   may be borrowed. Cross-generation mapping (old mesh → current mesh) depends on this most.

### 2.2 When an inference may be booked

Three conditions must hold together:

- The direction is unique (no second plausible target)
- The reasoning chain is reviewable (the grounds can be written down)
- The conclusion will not spread as a general rule

If one is missing, downgrade: keep the candidates, record the failure, state the recovery condition
(what evidence would unlock it).

### 2.3 Forbidden matching methods

| Forbidden | Why |
| --- | --- |
| Matching by `ComponentN` number | Unrelated to in-game numbering |
| Matching by resource file name / material name | Same names across components are common, and authors rename and misspell |
| Matching by index count alone | Many distinct meshes share an index count |
| Falling back to "the remaining slot" | The remaining one is not necessarily it; twin meshes (brows / lashes) mapping two source entries to one component are common, and remainder elimination misses the real target |
| Asserting identity from image similarity | Byte equality only proves "the replacement image is the same", not "the original triggering resource is the same" |
| Filling gaps from neighbouring files or reused pixels | A missing source file cannot be substituted with a similar image |

### 2.4 A recurring trap: same name is not the same thing

Same material name bound across components, same path with different bytes, same bytes serving
different purposes — all three occur in real data. The criterion always returns to the
**consumer**: which draw, which material slot, samples this property. No consumer, no identity.

---

## 3. Source data integrity

This chapter is the easiest to misjudge. IB/VB overruns in source Mods are very common, but
**not all overruns are the same**, and the handling differs completely.

### 3.1 The four overrun criteria

When facing "requested draw count > actual IB length", check these four in order:

| # | Criterion | Meaning |
| --- | --- | --- |
| 1 | **Is the gap exactly at the buffer end** | Mid-buffer gap = data misaligned, unrepairable; at the end = possibly just an inflated draw count |
| 2 | **Is the remaining data a whole number of triangles** | Actual IB length divisible by 3, and the last triangle's indices within the vertex range |
| 3 | **Is the end closed** | Count boundary edges (edges used by one triangle only). A closed mesh has very few; hundreds means it really was truncated |
| 4 | **Is there a paired gap** | One component's IB has N indices too many while another has exactly N too few = segment counts swapped, mutually correctable |

### 3.2 Disposition tree

```
requested draw > actual IB
├─ criterion 1 fails (gap in the middle)
│   └─ unrepairable. The data itself is misaligned or missing; record failure
├─ criterion 2 fails (length not divisible by 3 / index out of range)
│   └─ unrepairable. The IB is already corrupt
├─ criterion 3 fails (open end, hundreds of boundary edges)
│   └─ unrepairable. The real file was truncated; the missing triangles cannot come back
├─ criterion 4 holds (another place has the same surplus in reverse)
│   └─ repairable: change both draws to their complete existing IBs. The two gaps must be exactly equal
└─ all four pass (at the end, whole triangles, closed, no pair)
    └─ repairable: clamp the draw count to the actual IB length, keeping all existing geometry
        · the missing triangles are still gone; the report must state "the original overrun instruction is not equivalent"
        · this criterion applies **to this package's data only**; never bulk-adjust counts because "the buffer is long enough"
```

**Why the repair/unrepairable split matters**: the repairable kind yields a visually complete model
(a few triangles missing is invisible); the unrepairable kind, force-repaired, yields holes, spikes
and streaks that are far harder to diagnose than an honest failure.

### 3.3 Errors in the source itself

Source INIs and scripts contain many author mistakes. Recognising them is normal conversion work,
not guessing:

| Error type | Recognition | Repair principle |
| --- | --- | --- |
| Hash typo | That hash's index count / geometry does not match the declared component, while another entry on the same character matches exactly | Retarget per geometric evidence, keeping the author's intent explicable |
| Wrong segment count | The requested count equals exactly the length of an earlier segment (usually one segment counted twice) | Change only the in-memory draw count, never the file |
| Misspelled resource name | References a resource name that does not exist, while exactly one near-identical defined name exists in the same file | Repair against the actual file on disk, note it in the report |
| Misspelled keyword | e.g. `skip` written as `ski`, turning "hide" into "self-draw once then keep drawing" | **Do not repair it to `skip`.** The real behaviour is two draws; reproduce it faithfully |
| Undeclared resource | Calls an IB/VB resource never declared, while the same package holds exactly one named buffer that fits | Read it from the same package, noting that this restores author intent and does not assert the source ran as written |
| Injected advertising | Download-page / banner lines inside source files | Scrub only in the adaptation copy, never run source scripts |

**The misspelled-keyword case needs care**: the instinct is "obviously a typo, change it to `skip`",
but that discards one real draw and yields a package missing a whole appearance layer.
**Reproducing the erroneous behaviour faithfully is closer to the source's intent than correcting
the author.**

### 3.4 When to call it a missing source file

When the required DDS/mesh simply does not exist in the source snapshot, it is unrepairable. Key points:

- The file the resource path points at does not exist (as opposed to failing to parse)
- No sibling package of the same character carries it either
- The native property it corresponds to has been located; only the author's version is absent

Record this as "missing source file + the consumer property that was located", not as "identity
unknown" — the former tells the author to supply a file; the latter makes people think more
investigation is needed.

---

## 4. Vertex streams and mesh structure alignment

### 4.1 Three cardinality relations

| Relation | Typical case | Handling |
| --- | --- | --- |
| **One to one** | One source mesh replaces one native component | Direct mapping; verify stream layout and index range |
| **One to many** | One source draw must feed several native material slots | Classify triangle by triangle into target slots; never dump the whole block into one slot |
| **Many to one** | Several source entries point at one native component | Common for twin meshes (brows / lashes, hair segments). Confirm the index ranges do not overlap, then decide which to keep or replace |

**One-to-many is the easiest to get wrong**: when a single draw covers two materials, guessing the
split point from material names will always fail. The correct method is to assign every triangle to
one native submesh by **vertex position**, producing an unambiguous classification. If any triangle
falls in both, the mapping direction is wrong — go back and recheck identity.

### 4.2 Splitting merged meshes

Authors often merge several parts into one mesh while the native side keeps them separate. Criteria:

- Compare index counts and triangle counts against the sum of both sides
- Use the position sets to decide which vertices belong to which block
- Each split block must match the boundary of a native submesh

After splitting, **verify bone weights block by block** — a merged mesh mixes weights across parts,
and a wrong boundary yields wrongly bound parts.

### 4.3 Multiple sections on one hash

When two source sections declare the same hash and there is no `match_index_count` to distinguish
them, they must not be assigned by section order. Criteria:

- Each section's index count → which native submesh segment it corresponds to
- Section-name suffixes are **not** runtime matching conditions and cannot be used as grounds
- Each section's own conditions (which one hides, which one draws) must be reproduced as well

### 4.4 Stream layout

- Stream count, per-stream attributes and stride must match the target native layout item by item;
  mismatches must be converted, not forced
- Skinning weight format must be converted: native `Float32x4`/`UInt32x4` → BEM
  `UNORM16x4`/`UInt8x4`; check the maximum error afterwards
- Aliases such as `vb3 = vb0` hold only when vb0 is already bound, and the three streams must still
  be validated separately
- When an attribute such as UV1 is missing, **do not invent it**. State the absence and that the
  visual impact is unverified

---

## 5. Skeleton and skinning

### 5.1 Palette capacity

Each mesh's bone palette is indexed by `UInt8`, giving an effective ceiling around 256; in practice
**≤120 slots** is safer. When compressing a merged skeleton, compute the slots each component needs
first; if it exceeds the limit, split the component — never truncate indices.

### 5.2 Group offsets in merged skeletons

Sources use one global bone numbering; the native side has a separate palette per component. Conversion:

1. Find the group sizes and group offsets the source provides (or derive them from weight references)
2. Map global numbering to in-group numbering per group
3. Map in-group numbering to native bone names
4. Verify per vertex that the total weighted references match the source (a strong check: a mismatch
   means a group was missed)

When the source does not give a bone-name order, the in-group order is an L3 inference and must be labelled.

### 5.3 Zero-weight slots

It is common for a source vertex to carry a bone index in slot 4 with a weight of exactly 0. Criteria:

- Weight exactly 0 → the index is inert, may be zeroed
- Weight non-zero → the index is a real binding, must not be touched

**Change only inert slots, never any non-zero weight.** This is a different matter from "guessing bone
correspondence"; do not conflate them.

### 5.4 Skeleton table shifts across versions

When a game update inserts a secondary bone into the middle of a character's bone table, every bone
index **≥ the insertion point** in Mods extracted from the older version is shifted by one. This is
not the author's mistake; it is a generational break.

Recognition:

- Extremities such as toes or head are "pulled" toward the torso (bound to an arm or toe instead)
- Hair, horns and accessories fly through the air
- Comments in the source INI like "add 1 above index N" (the author was patching it at runtime with
  a compute shader on PC; the fix does not survive extraction)

Repair: remap the shifted range **top-down** (move high indices first, to avoid overwriting). With an
insertion point at 60, the range [60, 66] must be remapped starting at 66→67 and working downward.

**This only occurs on specific characters' specific bone tables** and must not be applied elsewhere.
The currently known case is **Yvonne**'s coat table (`cloth_01`, where a secondary bone
`Bip001_R_UpperArm_tz_minus` was inserted at position 60), shifting every index ≥60 by one: the left
toe bound to the right upper arm, the head horn and hairband bound to the right toe, the right calf
and foot displaced along with it. Whether other characters are affected requires comparing bone
counts and source versions individually; never apply the same remap because "it looks shifted".

### 5.5 Runtime bone redirection is not an effect

When the source extracts CB1 with a compute shader, records bones, and redirects per part offset
(common in cross-IB drawing), that is **core draw-path machinery**. Deleting it does not remove "one
effect"; the mesh skinning breaks or disappears.

Criterion: does this shader supply bones/matrices to later draws? If yes, it cannot be deleted. If it
only changes appearance and supplies nothing, it may be an excludable effect.

---

## 6. Material routing

This chapter has the highest error density. Most "package loads but looks wrong" cases live here.

### 6.1 Six hard rules

1. **Check the target material's own texture table before using a donor.** The target native
   material may already hold the same-named textures and slots. Borrowing someone else's whole
   material at that point replaces parameters nobody listed.
2. **A donor is a whole-material copy, not per-item override.** Using another component's material
   makes that entire material (including parameters you never intended to change) take effect.
   Either accept it or fill in every item.
3. **Same material name across components must be bound per draw, not replaced globally by name.**
   When two components share a material name but need different textures, a global replacement
   bleeds one into the other.
4. **Check texture consumers for every `keep` component.** Replacing only the main mesh and leaving a
   neighbouring component on `keep`, while that component consumes exactly the texture being
   rewritten → the effect silently misses. Either replace it too with the same texture, or record
   the non-coverage explicitly.
5. **Same bytes ≠ same semantics.** Two images can be byte-identical and still play different roles in
   that material. The criterion is the property name and the draw binding, not the file content.
6. **Slot residue must be suppressed explicitly.** When the native material carries emission, clear-coat
   mask and similar slots, a replacement draw binding only D/N/P makes the renderer fall back to
   sampling those residues, producing inexplicable teal-green metallic reflections or self-illumination.
   Either disable the channel via keywords or weights, or bind a 1×1 pure-black placeholder.
   The typical case is **Si's coat** (`M_actor_jsspsi_cloth_01`): that material carries `_EmissionMap`
   (`T_actor_jsspsi_cloth_01_E`) and `_ClearCoatMask` (`T_actor_jsspsi_cloth_03_M`), and the PC side
   clears them with `ps-t13 = null` / `ps-t20 = null`. After conversion to BEM those two slots are
   sampled again, making the coat and sleeves flash a harsh teal metallic sheen under strong light or
   at certain angles. Suppression restores it.

### 6.2 Error pattern library

| Symptom | Root cause | Investigation |
| --- | --- | --- |
| A part changed colour without a texture swap | Whole-material donor brought in extra parameters | List every property of the donor material and compare item by item |
| Two parts bleed colour into each other | Global replacement by material name | Bind per draw instead |
| Face replaced but brows still use the old texture | `keep` component with a missed consumer | Find which draws sample that texture |
| Normals / lighting completely wrong | Property semantics mismatched (`Lightmap` ↔ `_BumpMap`) | Check each source shader slot against native properties; never go by file name |
| Metallic / teal sheen under strong light | Native clear-coat or emission slot residue | Disable the channel or use a black placeholder |
| Transparency became opaque | Blend factors not expressed | base colour alpha + blend keyword + depth write |

### 6.3 The boundary of `ps-t*` to native property mapping

Sources write textures into register slots like `ps-t13`, `ps-t18`; BEM uses named material
properties. This mapping has **no general rule**:

- Different packages of the same character may point the same slot at different properties
- Different authors of the same series may swap normal and light maps
- Without compiled shader reflection or an in-game capture, only an L3 inference supported by three
  things at once is possible: source naming, already-proven packages of the same character, and the
  unique native material slot

**Therefore: infer per package, label per package, never promote to a global rule.**

**One exception, and it only settles half the question**: when the slot is defined by a framework the
mapping is definite — `Resource\RabbitFX\Diffuse` and its siblings are property declarations (see A.3),
no inference needed. But keep the two levels apart: the framework saying "`t70` carries Diffuse" does
**not** mean "the BEM native material has a property called Diffuse". The first is the source's
vocabulary; the second still has to be checked against the donor's native slot.

Two further cases must be recognised separately:

- **`ps-tN = null` (explicit slot clearing)**: BEM's material override has no "clear a texture slot"
  capability. Either achieve an equivalent through keywords/weights, or record an honest failure.
  This is not something arithmetic can route around.
- **`ps-t` writing pixels with no named property**: the source is changing a slot the native material
  has no name for. Find the consumer first; if there is none, it is a failure.

### 6.4 Multiple materials on one submesh

When a native submesh carries two materials and the source has a single draw segment, BEM must emit
two draw segments replaying the same index range. The pass count usually cannot be confirmed by
in-game capture, so it is a limitation that must be recorded. Never claim material equivalence
merely because "the format can express it".

---

## 7. State space

### 7.1 Count reachable states correctly

Three traps, each subtler than the last:

1. **A persistent key's initial value may not be in the enumeration loop.** The source INI saves the
   last value; the initial value may be a position the loop never produces. Miss it and a whole block
   of states disappears.
2. **When several persistent keys share one physical key, enumerate by saved value, not by "one press,
   one step".** The conservative approach is to build every combination into the package.
3. **When conditional draws are compressed with symbolic nodes, the combination count is a product.**
   Five groups of 3/5/6/9/5 give 4050, not 28.

### 7.2 When folding is allowed

| Case | Criterion | Disposition |
| --- | --- | --- |
| A key makes no visual difference within the chosen scope | Compare draw signatures state by state; identical | Fold, and state the reason in the report |
| A key only affects already-excluded content | All its effects fall inside exclusions | Fold, without inventing exclusions |
| A key only changes "whether a later key is available" (a lock key) | Two saved values draw identically | Fold |

**Folding requires comparison evidence.** Never fold because "it looks the same".

### 7.3 Conditional draws instead of expansion

With the raised limits, most combination counts need no expansion into separate appearances:

- 64 choices per group, 64 groups, 4096 candidates
- Use `when` to control draws; express hiding with an empty draw (prefer component-level `hide`)
- A package expressing tens of thousands to tens of millions of combinations is normal, not an anomaly

**Deciding whether to expand**: if two states differ only in "whether a component shows", use a
condition; only if they differ in entirely different geometry should a separate appearance be considered.

---

## 8. Exclusion boundary

This chapter decides "may this be deleted". The cost of deleting wrongly is far higher than not
deleting: deleting core drawing yields a broken model, while not deleting only carries a little
unused content.

### 8.1 Decision tree

```
may this source content be deleted?
├─ is it part of the chosen character's base appearance?
│   ├─ yes → cannot delete
│   │   (base mesh, base D/P/N/E textures, active bone/material programs, hand-drawn work carried by DrawMesh)
│   └─ no → continue
├─ does it supply data to the base drawing?
│   ├─ yes → cannot delete
│   │   (CrossIB bones, CB1 redirection, bone palette, RabbitFX inputs whose semantics are Diffuse)
│   └─ no → continue
├─ does it belong to an explicitly authorised excludable category?
│   ├─ low LOD (the current runtime chain locks LOD0) → may delete, state the scope
│   ├─ global rain/snow overlay (shared across packages) → may delete, but requires a two-field declaration
│   ├─ pure real-time shader execution (no effect on static appearance) → may delete
│   ├─ author UI / notifications / advertising → may delete
│   └─ none of these → keep, or record failure
└─ cannot tell whether it is base appearance
    └─ find the consumer first. None found → record failure, do not delete
```

### 8.2 Three things that look like effects but must not be deleted

| Content | Why it cannot go |
| --- | --- |
| `DrawMesh`-style calls | Frequently carry the base hand-drawn work / base mesh, not an extra effect |
| RabbitFX `Diffuse` | This is **base colour**. Deleting it as a glow effect leaves a white model |
| Partial mip overrides | Replacing only some mip levels means the author cared about sampling results; it is not a negligible detail |

### 8.3 Animation: no native channel, but approximation is possible

Earlier tool versions recorded this as "a new animation system, excludable". That is obsolete. The
current situation is **possible, at a quality cost**; the question is not "can it" but "is it worth it".

**The approximation**: solve the animation frame by frame into per-vertex position offsets, bake them
as deformation frames, then sweep those frames with a clock waveform.

**Three hard numbers that set quality**:

| Spec | Value | Consequence |
| --- | --- | --- |
| Frames per deformation channel | 2..64 | Source animation longer than 64 frames must be downsampled |
| Inter-frame evaluation | Linear interpolation | Sparser frames put the "straight line" further from the true arc |
| Waveforms | A closed set of 5 | Timing curves (ease in/out, hold, variable speed) can only pick the closest |

**Six losses, by severity**:

1. **Volume collapse / joint creasing** — rotation should follow an arc; linear interpolation follows
   the chord. Large arm swings and elbow/knee bends thin the limb and self-intersect at the armpit and
   behind the knee.
2. **Frame count cut** — fast motion develops a stroboscopic feel; fine detail disappears.
3. **Lighting does not follow** — the spec is explicit: morph drivers move vertex positions only and
   never touch normals, tangents or UVs. After a turn, specular and shadow stay in the original
   orientation and shading appears "stuck" to the model. **This is a design boundary, not an
   implementation defect**, and no parameter routes around it.
4. **Bones do not actually move** — everything attached to bones fails: weapon sockets, ribbon physics,
   collision, ground contact.
5. **Timing curves go wrong** — beats like "hold three frames then burst" are lost. Sine-like
   waveforms come closest for ease in/out.
6. **Multiple animations compete** — drivers per package and bindings per driver are both limited;
   one motion consumes one driver.

**Criteria**:

| Source animation characteristics | Conclusion |
| --- | --- |
| Small amplitude, ≤64 frames, mostly material/morph change, no bone sockets | Approximation is nearly invisible; worth doing |
| Material parameters varying over time (glow breathing, colour fade, texture panning) | Natively supported; do it directly, it is not approximation |
| Large amplitude, depends on joint rotation, long duration, uses bones for physics or sockets | Approximation is obviously fake; exclude honestly and declare it, do not ship a "moves but looks bad" package |
| Animation only, no static content | Record failure. Do not fabricate an all-`keep` empty package and call it success |

**One coupled constraint**: a morph parameter used for animation must not also be driven by a manual
slider over the same vertex range. Per-frame runtime rewrites and the whole-curve upload at load time
overwrite each other, showing as ghosting in place, flicker, or a washed-out surface. To get "slider
sets the baseline, animation oscillates", split into two parameters bound to non-overlapping vertex ranges.

**Another easily missed state semantic**: a driver has four states (stopped / playing / paused /
finished), and **only "stopped" releases the parameter**. Paused and "finished" both keep delivering
the frozen phase or the last-frame sample every frame, so properties and vertices stay put. A
one-shot animation therefore **freezes on its last frame** rather than returning to the original pose
— if the design intent is "push out then spring back", that is a job for an oscillating waveform, not
a one-shot one.

One more trap affecting keywords only: with no static override, a keyword is treated as off at
runtime. If the target shader originally had that keyword on, stopping the driver leaves a state
inconsistent with the author's intent. When semantics must be guaranteed, declare the keyword
explicitly in the material override as well.

---

## 9. Acceptance and disclosure

### 9.1 What two-sided offline validation proves

| Proves | Does not prove |
| --- | --- |
| Package structure is legal, fields self-consistent | The picture matches the source Mod |
| Geometry and materials readable for the chosen state | Material property mapping is correct (all could be present yet semantically inverted) |
| Indices / vertices / textures in range | Bone animation is visually correct |
| Capability declarations match the version | Appearance of transparency, blending, shader effects |

So **offline validation is necessary, not sufficient**. Completion also includes the question from
§0.1: "on what grounds does this property correspond to that source item".

### 9.2 What must be disclosed

Every package report must carry at least:

- `render_verified` = false (when no in-game verification was done)
- `full_source_behavior_complete` = false (when any source behaviour is not carried)
- `excluded_features`: every actually excluded source feature, item by item — **omit when empty, never invent**
- The reasoning chain and uncertainty points of every L3 inference
- A separate marker field when global rain/snow or cross-character effects are not carried
- Out-of-scope content (backup LODs, effects on other characters, unverified pass counts)

**An institutional lesson**: field coverage does not depend on "being required to write it", it
depends on "what happens if you do not". In the reference corpus, fields checked by the registration
gate reached 100% coverage, while `excluded_features` / `limitations`, which the gate ignored,
reached only 84–89%; a single audit found 135 gaps. **Anything that "should be written" must become
"cannot be registered without it".**

### 9.3 Writing the package: atomic replace + readback

Three actions, or an interruption leaves a half-written artifact or silently changes content on write:

```
write to a temp file → os.replace (atomic) → read back → validate the read-back structure and geometry
```

**First, separate what already exists from what you must write yourself** — do not reimplement the
first, and do not assume the last is already there:

| Stage | Status | Evidence |
| --- | --- | --- |
| Atomic write | **Built in, do not reimplement** | `bem_v1.atomic_write()` (`bem_v1.py:42-54`) uses `mkstemp` + `os.replace`; `write_package()` calls it at `bem_v1.py:236` |
| Payload dedup + index remap | **Built in, and it rewrites the manifest** | `write_package()` at `bem_v1.py:187-217` merges byte-identical payloads and rewrites `draw['indices']`, `stream['payload']`, `texture['payload']` and deformation-frame `payload` through `mapping` |
| Readback validation after write | **Not built in** — neither `pack` nor `convert` does it | Call `bem_projects.validated()` or `bem_v1.read_package()` yourself |

**There is a trap here**: because the write dedups and remaps, the manifest you read back is **not**
equal to the in-memory `builder.m`. A naive `assert m == builder.m` fails spuriously on any package
with two byte-identical payloads. Likewise `builder.payloads` is the pre-dedup list while the
read-back gives you the post-dedup one.

Write the check as "re-validate", not "byte equality":

```python
from pathlib import Path
from bem_projects import validated      # wraps read_package + check_geometry

def write_and_verify(builder, output: Path):
    """builder is bem_v1.Builder: manifest in builder.m, payloads in builder.payloads.

    Get the attribute names right: the manifest is .m, not .manifest; bem_merge's
    MergeResult has no write() at all — it only computes which sections take effect.
    """
    builder.write(output)              # atomic replace is already handled inside
    m, p = validated(output)           # actually re-read the file, rerun structure + geometry

    # Semantic comparison: counts must match (dedup only shrinks, never grows)
    assert len(m['meshes']) == len(builder.m['meshes']), 'READBACK: mesh count changed'
    assert len(m['textures']) == len(builder.m['textures']), 'READBACK: texture count changed'
    assert len(p) <= len(builder.payloads), 'READBACK: payload count grew; dedup is broken'

    # Idempotence: write the read-back model again; bytes must be identical.
    # This is stronger than "equals the in-memory model" — it also proves the
    # dedup and the index remap are stable.
    import hashlib, tempfile, os
    fd, again = tempfile.mkstemp(suffix='.bem'); os.close(fd)
    try:
        from bem_v1 import write_package
        write_package(again, m, p)
        assert Path(again).read_bytes() == output.read_bytes(), 'READBACK: rewrite differs'
    finally:
        os.unlink(again)
    return m, p
```

Why not "write in place, then check": if power fails halfway, the disk holds a file whose header
declares everything while payloads are missing — and every "file exists" check passes.
`os.replace` is atomic within one partition; readers see either the old file or the new one.
`atomic_write` already covers that half, so you do not manage it; the half you must manage is
**the readback**.

### 9.4 The ledger is the truth; reports cannot self-certify

If a batch of conversions runs for months across hundreds of packages, there must be one
**authoritative state**, and **it must not trust any self-reported number**.

Two designs from the reference corpus are worth copying:

1. **Re-read the package at registration; do not trust the report.** If the report says 16777216
   appearances, recompute the product over `option_groups[*].choices`; if it disagrees, refuse the
   registration.
2. **Withdrawal moves, never deletes.** Move the artifact to an archive area, keep the old report
   path, never `rm`.

Its limitation must also be acknowledged: **the ledger governs the moment of registration, not
afterwards**. In the reference corpus one "finished" artifact was later replaced with a 50 MB Windows
PE executable while the ledger still said success — correct at registration, never looked at again.

So **authoritative state + periodic independent audit** go together, not as alternatives. The audit
checks dimensions the ledger does not: is the file actually a BEM container, is the package ID
right, is the appearance count out of control, are the disclosure fields present.

### 9.5 Withdrawal

When a registered package is found to have a structural error (donor borrowed wrongly, property
semantics inverted, base content deleted as an effect), **withdraw rather than patch**:

- Move the package out of the finished directory, mark the ledger entry failed
- Keep the old package and report, state the withdrawal reason
- Regenerate candidates and go through the same validation

Patch-style fixes make "why is this package like this" untraceable, which is worse than failure.

---

## 10. Failures must be classified too

Failure is not one state. Four classes, completely different recovery conditions; writing the wrong
class makes later work wasted.

| Class | Characteristics | Recovery condition |
| --- | --- | --- |
| **Missing source file** | The path points at a nonexistent file; the native property is already located | Author supplies the file; no further identity work needed |
| **Identity unproven** | Candidates exist but none reaches L1/L2, and several are possible | Needs native capture (DX11 descriptor, shader reflection, in-game frame) |
| **Capability missing** | The source behaviour is clear but BEM cannot express it (clearing a texture slot, bone channels, custom algorithms) | Wait for a format upgrade; or use a declared lossy downgrade |
| **Quality not deliverable** | Technically possible, but the downgrade degrades visibly | Not waiting on technology, a trade-off: accept the downgrade and declare it, or do not ship |

### 10.1 Actual distribution: what the 48 failed packages consist of

Every failure record in the 551-package reference corpus, classified by root cause rather than by
error-code wording:

| Root cause | Packages | Share | Key criterion |
| --- | --- | --- | --- |
| **Physically missing source DDS** | 22 | **46%** | Declared but absent, and no sibling package of the same character has it |
| Identity unproven | 8 | 17% | Hash has zero hits in the native index; never guess by ordinal |
| Source IB corrupt / overrun | 4 | 8% | Gap not at the end / remainder not whole triangles / open end |
| Cross-format machinery (CrossIB, CB1, compute shader) | 3 | 6% | Runtime bone hijacking, multi-stage redirection |
| Non-character object (prop / weapon / sheath) | 3 | 6% | Target absent from the character catalog |
| Runtime-generated material | 2 | 4% | Skin/makeup generated per pixel by a shader |
| Multiple characters in one package | 1 | 2% | A single target cannot express it |

**The one thing to remember from this table**: the largest class (46%) has the simplest criterion,
while the hardest one to attack is identity unproven (17%). So the best return on effort is making
"check for missing files" the first step of every job, not attacking identity first.

### 10.2 One counter-intuitive number

In the same set of failure records, 6 packages are marked "exceeds the old format limit". After the
limits were raised, **their blocker no longer exists, but nobody went back**:

| Package | Original blocker | Current limit |
| --- | --- | --- |
| Single 12800² single-mip DDS | 144 MiB > 64 | 256 MiB |
| 38 textures per choice | > 32 | 64 |
| 1551 draws | > 512 | 4096 |
| 40 / 48 choices | > 16 per group | 64 per group |

**A failure record must state what is missing, and what is missing expires.** A failure list nobody
recycles permanently locks packages that are now doable. So failure records must be searchable by
blocker class — stable classes (missing source file) should be re-run in bulk after any upgrade,
while version-dependent classes (over limit) must carry the version number.

### 10.3 Reporting requirements

Every failure record must state "what would unlock this", concretely enough to go and look for. "Identity
unclear" is not a valid failure record; "`ps-t15` needs shader reflection to confirm its property and
draw range" is actionable.

Also: **state that this is the judgement as of the current version**. Every conclusion limited by
format capability needs a reviewable anchor (which limit, which spec section), or nobody will know
in six months whether to redo it.

---

## 11. Landing: where each decision goes

A decision must land in a concrete field, otherwise it was never made. This table maps each gate's
conclusion to its place in the manifest. Field definitions themselves are in the
[format specification](BEM_FORMAT_SPEC.en.md); this only says which conclusion goes where.

| Conclusion | Landing | Note |
| --- | --- | --- |
| Which target component this source entry corresponds to | `meshes[].component` | Decided by identity anchoring, never by the source's `ComponentN` number |
| Which target skeleton this source geometry uses | `meshes[].bones[]` | `component` + `index` + `name` must all agree |
| Index range of each draw segment | `meshes[].draws[]` `indices` payload + `count` | 1.1+ carries one payload per draw; `count` a multiple of 3 |
| Which material this draw uses | `draws[].material_component` + `material_slot` + `material_name` | All three must point at one donor native material slot |
| Which textures this draw uses | `draws[].textures` → top-level texture table | One draw must not reference the same item twice |
| Result of texture identity anchoring | Texture table `original_name` + `format` + `srgb` | `original_name` is the full name of the donor's replaced Texture object |
| State → choice | `option_groups[]` + `draws[].when` | Conditional draws beat expanding into N appearances |
| Shape key → continuous parameter | `parameters[]` + `mesh_deformations` | Position deltas are absolute offsets, interpolated per `tick` |
| Material override | `draws[].material_overrides` + `required_capabilities` | Declare `material-overrides-v1`; property names start with `_` |
| Required capability version | `required_capabilities[]` | A missing declaration means rejection by older parsers, not a downgrade |
| Why something was not done | Conversion record (not the manifest) | Must state what is missing, concretely enough to act on |
| Exclusion / hiding declarations | Conversion record (not the manifest) | Covers explicitly listed items only, never "could not figure it out" |

**The last row is the easiest to get wrong**: the manifest cannot express "why I did not do something".
Those conclusions belong in the conversion record shipped alongside the package. The absence of a
field does not mean the explanation is unnecessary.

## 12. Situations that require stopping

The following situations generally indicate severe flaws in the source data.
- **In Collaborative Mode**: Stop immediately when encountering these cases and present clear evidence for user decision.
- **In Fully-Autonomous Mode (Novice Default)**: The Agent is authorised to intervene as a reverse-engineer. For issues with a deterministic repair route (e.g. 4-criteria valid IB overruns, missing non-vital DDS resolvable via peer donors, 16K texture mip drops), the Agent writes and runs dedicated build scripts (Section 13) without interrupting the user. **Halt only when vital core assets (primary diffuse, base mesh geometry, target character catalog) are completely missing and unrecoverable**.

| Situation | Why stop | What the report must carry |
| --- | --- | --- |
| Identity only reaches L4 ("it should be that one") | A wrongly bound texture builds, loads, and renders wrong — worse than a missing texture | Candidate list + exclusion reason per candidate |
| A missing DDS exists in no sibling package of the same character | Nothing can come from nothing; swapping donors needs authorisation | Missing list + searched scope (which packages, which directories) |
| IB overrun with none of the four repair criteria holding | A retry cannot restore triangles that do not exist | Capacity, requirement, missing index count, verbatim |
| Bone table over 256 and no offline remap possible | The spec requires refusal; no truncation, no automatic splitting | Source bone total + remapped table length |
| State space exceeds the enumeration budget | Missed states are silently missed; the report still looks complete | Key domain + enumerated count + budget ceiling |
| Material slot semantics cannot be confirmed | A donor replaces the whole material, including unlisted parameters | The slot + every known property on it + which evidence is missing |
| The source depends on runtime-only behaviour | A static INI cannot see the result; any conclusion is a guess | Which class of behaviour, in which section |

### 12.1 The last category needs special mention

One class of source behaviour **has no visible result in static text at all**: it is either computed by
a framework at runtime or decided by a shader on the GPU. Their shared signature — the source contains
only a "call" and its "parameters", never a "result".

The way to judge is to look at **where the called section is defined**:

- Defined inside the source package → read it as an ordinary command list; it can be read to the end
  (`bem_merge.py` computes this)
- Defined in the loader's own library → read it per the library's semantics; those are definite
  (`references/efmi/` holds the originals)
- Defined in a third-party framework whose body is publicly obtainable → read it via the interface facts
  (A.3): slots and delegated draws both expand, but "the framework's slot name" is not "the native
  material property name"
- **Obtainable nowhere** → the channel's actual effect is unknowable. The only correct conclusion is
  "bindings unknown, cannot be read per draw", disclosed as such; never assume "it probably maps the slot
  onto some property"

None of the four may be papered over with "same name, same meaning": a similar name is not evidence.

---



---

## 13. Dedicated Build & Assembly Script Patterns for Autonomous Agents

When a source Mod is non-standard (multi-state key cycles, companion INIs, missing secondary DDS, end-of-buffer IB overrun, custom shaders) causing CLI `bem_tool convert` to raise exceptions, **in Fully-Autonomous Mode, the Agent must not stop to bug novice users. The Agent must act as a reverse-engineer and write a dedicated Python build script (e.g. `workspace/build_mod.py`) in the workspace to assemble and self-heal the package.**

### 13.1 Standard Assembly Skeleton

Assembly scripts run in the workspace and reuse `bem_v1`, `component_auto` and `bem_merge`, sanitising
and repairing through an in-memory `Overlay`.

**First understand why the Overlay is necessary.** `component_auto.prepare()` starts with
`sections(src.read_exact(ini).decode('utf-8-sig'))` (`component_auto.py:170`), and
`efmi_source.sections()` raises `ValueError(f"duplicate INI section: {name}")` the moment it meets a
repeated section (`efmi_source.py:20-21`). In other words: **if the entry INI contains any duplicate
section, `prepare` fails at parse time**, regardless of whether the content is correct. `bem_merge`
computes the sections that actually take effect (first occurrence wins, include expansion, namespace
rewriting, watermark stripping) and collapses them into a duplicate-free dict; feeding that to
`prepare` clears a purely syntactic gate. That is the only reason the Overlay exists — it is not a
way to bypass validation.

**This is not theoretical. Four real packages, side by side** (`prepare` uses the "entry file only"
view; the Overlay supplies the "loader merged" view):

| Package | Files | Live sections | Unique names | Namespace collisions | Entry-file view | Merged view |
| --- | --- | --- | --- | --- | --- | --- |
| 伊冯-围裙 | 1 | 134 | 134 | 0 | **raises on parse** `duplicate INI section: Constants` | matches `chr_0017_yvonne`, 11 entries |
| 伊冯-26W | 3 | 477 | 293 | **182** | **raises on parse** `duplicate INI section: Constants` | parses, then `SOURCE_IDENTITY` (no ComponentN base entry) |
| 弧光-02 | 1 | 99 | 99 | 0 | parses | parses; both report `CHARACTER_CATALOG` (catalog does not cover it) |
| 梨诺-颜色切换 | 1 | 274 | 274 | 0 | parses | parses; both report `CHARACTER_CATALOG` |

How to read it: **for packages like 伊冯-围裙, conversion is impossible without the Overlay** — the
single-file view cannot even parse the sections, while the merged view matches the character catalog
and yields 11 component entries. Conversely, 弧光-02 and 梨诺-颜色切换 are `manual_only`; the Overlay
does not help, and the error is missing catalog data rather than a syntax problem — **these two
failures must be reported separately**, or "catalog does not cover it" gets misdiagnosed as
"malformed package". 伊冯-26W is a third case: syntax passes, identity does not, so it follows the stop
conditions in §12.

**Two hard constraints you must respect:**

1. **Locate paths relatively; never paste a local absolute path.** The script has to run on someone
   else's machine. `F:/…` belongs in your own scratch debugging only. Walk up from `Path(__file__)`
   to find `tools/CustomModel`, and fail loudly if it is not there — do not degrade silently.
2. **`standard_worker.normalize_entries` does not exist.** Earlier documentation referenced this
   symbol; nothing in this tree defines it (`grep -rn "def normalize_entries" tools/CustomModel/*.py`
   is empty). Section normalisation now belongs to `bem_merge`. Copying the old skeleton fails with an
   `ImportError` on the import line, which looks like an environment problem and is easy to misread.

```python
"""Dedicated assembly script for non-standard mod conversion."""
import sys, copy, json
from pathlib import Path

# --- 1. Locate the tool root relatively; fail loudly if absent -----------------
def find_tools_root(start: Path) -> Path:
    for base in [start, *start.parents]:
        candidate = base / "CustomModel"
        if (candidate / "bem_v1.py").is_file():
            return candidate
        if (base / "bem_v1.py").is_file() and base.name == "CustomModel":
            return base
    raise SystemExit("TOOLS_ROOT_NOT_FOUND: run this inside the worktree, or pass --tools-root")

TOOLS_ROOT = find_tools_root(Path(__file__).resolve().parent)
sys.path.insert(0, str(TOOLS_ROOT))
sys.path.insert(0, str(TOOLS_ROOT / "skills/bem-creator/scripts"))

import bem_v1 as bem
import component_auto
import bem_merge
from convert_efmi_poc import Source
from efmi_source import value
from bem_projects import validated


class Overlay(Source):
    """In-memory INI overlay that never touches source files on disk."""

    def __init__(self, src: Source, ini: str, sec_dict: dict[str, str]):
        self.source, self.path, self.names = src, src.path, src.names
        self.ini = ini
        self.raw = "\n\n".join("[" + n + "]\n" + b for n, b in sec_dict.items()).encode("utf-8")

    def read_exact(self, name: str) -> bytes:
        if name.replace("\\", "/") == self.ini.replace("\\", "/"):
            return self.raw
        return self.source.read_exact(name)


def flatten_live_sections(merged: bem_merge.MergeResult) -> dict[str, str]:
    """All surviving sections, keyed by written name, as raw bodies.

    Flattens every document (not just documents()[0]): entries frequently live in
    an included file, and prepare() looks them up by name in one flat dict.
    Sections that lost a duplicate are absent — that is the point.

    KNOWN LIMIT: this collapses namespaces. When the same plain name lives in two
    namespaces (measured: 182 such names in one real package), only the first
    survives here. The loader keeps both. prepare() only understands plain names,
    so this is a lossy compromise forced by the route, not a correct merge.
    If merged.collisions() is non-empty, check that the lost copies are not
    referenced by a namespaced `run = CommandList\\Name` you care about; if they
    are, this route is not safe for that package.
    """
    out: dict[str, str] = {}
    for _relative, sections in merged.documents():
        for name, section in sections.items():
            out.setdefault(name, section.body)
    return out


def build_package(source_dir: Path, output_bem: Path, character_id: str, package_name: str,
                  legacy_aliases: dict | None = None, clamp_draws: list | None = None,
                  root_offset_y: float | None = None, material_overrides: dict | None = None):
    src = Source(source_dir)
    merged = bem_merge.merge(source_dir)
    if not merged.entry:
        raise SystemExit("NO_ENTRY: bem_merge found no entry INI; run bem_triage.py for the routing")
    active_ini = merged.entry
    clean_sec = flatten_live_sections(merged)

    # 2. Scrub global weather/terrain textures that clash. Hashes come from the
    #    actual package — do not write them from memory.
    for section, body in list(clean_sec.items()):
        if section.startswith("TextureOverride_Texture") and value(body, "hash") in ("7d330dbd", "e69d7088"):
            del clean_sec[section]

    # 3. Match the character catalog (historical hash aliases may be injected)
    db = copy.deepcopy(component_auto.catalogs())
    cat = next((c for c in db if c["character_id"] == character_id), None)
    if cat is None:
        raise SystemExit(f"CATALOG_MISS: no {character_id} in catalog/; do not hand-edit the catalog")
    if legacy_aliases:
        cat["entries"].update(legacy_aliases)

    # 4. Strip the 3DMigoto runtime trigger (BEM has no such command)
    for key in list(clean_sec):
        if key.startswith("CommandList_Draw_Component"):
            clean_sec[key] = clean_sec[key].replace("run = CommandListTriggerResourceOverrides", "")
    clean_sec.pop("CommandListTriggerResourceOverrides", None)

    # 5. Repair the IB overrun: change the draw's declared count, not the buffer.
    #    Each clamp_draws entry must be (component_id, declared_count, safe_count, first),
    #    where safe_count comes from `bem_repair.py ib-audit`. Do not estimate it.
    if clamp_draws:
        for cid, declared, safe, first in clamp_draws:
            draw_key = f"CommandList_Draw_Component{cid}"
            body = clean_sec.get(draw_key)
            if body is None:
                raise SystemExit(f"DRAW_SECTION_MISSING: {draw_key}")
            old = f"drawindexed = {declared}, {first}, 0"
            new = f"drawindexed = {safe}, {first}, 0"
            if old not in body:
                raise SystemExit(f"DRAW_PATTERN_MISS: {old!r} not in {draw_key}; dump the body first")
            clean_sec[draw_key] = body.replace(old, new)

    # 6. Assemble in memory. prepare() already runs validate_manifest + check_geometry.
    overlay = Overlay(src, active_ini, clean_sec)
    builder, info = component_auto.prepare(overlay, active_ini, database=[cat])
    builder.m["name"] = package_name

    # 7. Inject BEM 1.5 features. The capability and the field must appear together
    #    or validation rejects the package.
    if root_offset_y is not None:
        caps = builder.m.setdefault("required_capabilities", [])
        if "root-offset-v1" not in caps:
            caps.append("root-offset-v1")
        builder.m["root_offset"] = {"path": "Bip001", "translation": [0.0, float(root_offset_y), 0.0]}

    if material_overrides:
        caps = builder.m.setdefault("required_capabilities", [])
        if "material-overrides-v1" not in caps:
            caps.append("material-overrides-v1")
        for mesh in builder.m.get("meshes", []):
            for draw in mesh.get("draws", []):
                draw.setdefault("material_overrides", {}).update(material_overrides)

    # 8. Write, then read the file back. Writing without reading back is not verification.
    output_bem.parent.mkdir(parents=True, exist_ok=True)
    builder.write(output_bem)
    m, p = validated(output_bem)       # genuinely re-reads the file and revalidates
    print(f"built & read back: {output_bem} ({output_bem.stat().st_size} bytes)")
    return m, p, info
```

**What this skeleton still cannot do — do not pretend otherwise**: `prepare` only handles the
"one contiguous index range per component" case (`component_auto.py:218-224` raises on `bindings`,
buffer switches, or a non-zero `base`). Multi-material-slot reordering, draws spanning buffers, or
packages needing a rebuilt vertex stream go through `bem_assemble.py` (repository only) or a direct
`Builder` call per §13.5. The skeleton deliberately does not call `bem_peer`: borrowing a donor is a
separate decision, see §13.3.

### 13.2 Pattern 1: Index Buffer Overrun (F3 self-healing)

When `bem_triage.py` reports `INDEX_BUFFER_OVERRUN`, start with:

```bash
python scripts/bem_repair.py ib-audit <package>/Meshes/Component0_IB.buf --first 0 --count 54018 --stride 2
```

It reports `available_from_first`, `short_by`, `overrun`, `fixable`, `clamped_count` and
`dropped_triangles`. **Read the numbers before acting** — "it overruns" is not a diagnosis.

**Key correction: what you change is the draw's declared `count`, not the index buffer.** Earlier
documentation offered a `clamp_index_buffer()` that truncated the `.buf` to `len // stride` and wrote
a new file. That approach has three fatal problems:

1. **It ignores `first` entirely.** The overrun condition is `first + count > total`, where `total` is
   the buffer length. The indices available after `first` number `total - first`, not `total`.
   Truncating to `total` fixes nothing when `first > 0`.
2. **It shortens the buffer.** The buffer is a source asset; shortening it changes the payload's byte
   identity, and one `.buf` is usually shared by several draws — truncating for one draw silently
   corrupts the others.
3. **It never reports how much geometry it destroyed.** Clamping always deletes geometry, and the
   amount must be stated or the delivery notes are false.

The correct move is to edit that one draw declaration's `count` so it lands inside
`[a multiple of 3, total - first]`, leaving the buffer byte-for-byte untouched (see step 5 of the
§13.1 skeleton):

```bash
# Compute the safe value first, then decide whether to accept it.
# Output goes to stdout; nothing is written.
python scripts/bem_repair.py ib-clamp <package>/Meshes/Component0_IB.buf \
    --first 0 --count 54018 --stride 2
# Without --accept-truncation it refuses with IB_TRUNCATION_NOT_ACCEPTED
```

`ib-clamp` only produces a clamped result when you explicitly pass `--accept-truncation`, and it
**never writes back to the `.buf`** — its output is the conclusion "this is what `count` should
become". Applying that conclusion to the INI is your script's job.

### 13.3 Pattern 2: Sibling DDS borrowing (F1 self-healing)

When `bem_triage.py` reports a missing DDS, **"a file with the same name exists" does not mean "this
is the same texture"**. Same-name-different-content DDS files are common in real packages (one
character's skins sharing a filename, an author reusing an older asset). Blind copying puts the wrong
normal map on a face and the validator will not complain — it only checks that the bytes are legal.

Two steps, with a judgement in between:

```bash
# Step 1: find candidates. Output has two tiers: strong (relative path matches)
# and weak (filename only). Results go to stdout, grouped by byte size; a
# same-name-different-size hit is flagged as a conflict.
python scripts/bem_repair.py donor-find "Body_Diffuse.dds" --root ..
```

`donor-find` **copies nothing**.

```bash
# Step 2: copy with evidence. --expect-size or --expect-sha256 is mandatory.
python scripts/bem_repair.py donor-copy "<one candidate path from donor-find output>" \
    -o ./payloads/ --expect-sha256 3f2a...
```

`donor-copy` takes **one candidate listed by `donor-find`** (not a free-form "missing name + source
dir" pair), with the output directory via `-o`. It refuses without `--expect-size` / `--expect-sha256`
(`DONOR_NO_EVIDENCE`) and refuses to overwrite an existing target (`DONOR_WOULD_OVERWRITE`, needs an
explicit `--force`). The constraint is deliberate: **the evidence level belongs in the command**, not
in a comment.

Where the evidence comes from, most reliable first:

| Evidence | Level | Notes |
| --- | --- | --- |
| Source `hash=` equals the donor package's `hash=` | exact | The hardest evidence: same hash is the same resource |
| Relative path identical including subdirectories | computed | What the `strong` tier means; the path is the author's identity claim |
| Filename matches and byte size matches | inferred | The `weak` tier. Acceptable, but state it in the delivery notes |
| Filename matches, size differs | unusable | This is a conflict, not a candidate |

When none of the four holds, the correct conclusion is **this slot has no donor**: fall back to the
native material slot (drop the slot). Do not fabricate a texture to fill it.

### 13.4 Pattern 3: Oversized textures (16K / >256 MiB) via mip dropping

When a single DDS exceeds 256 MiB the engine refuses to load it. Use the tool; do not hand-roll it:

```bash
# Inspect the structure first: dimensions, format, mip count, array size, cubemap flag
python scripts/bem_repair.py dds-audit <texture>.dds

# Drop mip 0 and rebuild into a new file (input untouched)
python scripts/bem_repair.py dds-mipdrop <texture>.dds -o <output>.dds
```

**Why you must not copy the old ~20-line `drop_mip0_dds()`**: it hardcodes
`mip0_bytes = blocks_w * blocks_h * 16`, but bytes per block vary with the format — 8 for BC1/BC4,
16 for BC5/BC6H/BC7, and uncompressed formats have no blocks at all. It also handles only one slice:
for `array_size > 1` or a cubemap (`misc & 0x4`) there is a mip 0 in **every** slice, so slicing only
the first leaves the file length inconsistent and the runtime either rejects it or reads misaligned
data.

`dds-mipdrop` gets these right: it looks up bytes per block from the DXGI format, rebuilds slice by
slice according to `array_size`, refuses volume textures (`DDS_VOLUME_UNSUPPORTED`), asserts the
rebuilt length equals `header_len + (per_slice - mip_sizes[0]) * array_size`
(`DDS_REBUILD_MISMATCH`), refuses single-mip input (`DDS_SINGLE_MIP`), and refuses when the result is
still over the limit (`DDS_STILL_OVER_LIMIT`, unless `--allow-still-over` is explicit).

Dropping mip 0 is **lossy**: the texture blurs at close range. State that in the delivery notes; it is
not a lossless repair.

### 13.5 Pattern 4: Option Groups Translation from State Probe

When `bem_state_probe.py` reveals reachable toggle keys, declare structured `option_groups` and
`component_rules` in `project.json`.

**Two structural details that are easy to get wrong** (both are rejected with a generic
`Invalid component rule` / `Invalid condition node`, which reads like a data problem):

1. `component_rules` is **one entry per component target, in target order**, and each entry is
   `{"target": N, "candidates": [...]}` — not `{"component": N, "rules": [...]}`. The validator
   requires `len(component_rules) == len(target.components)` and `rule['target'] == index`
   (`bem_v11.py:742-746`).
2. `when` is **not a string expression**. It is a structured node — `true`, `{"eq": [group, choice]}`,
   `{"all": [...]}`, `{"any": [...]}`, `{"not": ...}` — validated by `_condition()`
   (`bem_v11.py:123-142`). `"glasses == 'off'"` is an unsupported node and fails immediately.

```json
{
  "option_groups": [
    {
      "id": "glasses",
      "name": "Glasses Toggle",
      "default": "on",
      "choices": [
        {"id": "on", "name": "Equipped"},
        {"id": "off", "name": "Removed"}
      ]
    }
  ],
  "component_rules": [
    {"target": 0, "candidates": [{"when": true, "operation": "keep"}]},
    {"target": 1, "candidates": [{"when": true, "operation": "keep"}]},
    {"target": 2, "candidates": [{"when": true, "operation": "keep"}]},
    {
      "target": 3,
      "candidates": [
        {"when": {"eq": ["glasses", "off"]}, "operation": "hide"},
        {"when": {"eq": ["glasses", "on"]}, "operation": "replace", "mesh": 0}
      ]
    }
  ]
}
```

Note the `keep` stubs for targets 0–2: every component needs at least one candidate, and exactly one
candidate must hold in every legal combination (`BEM_FORMAT_SPEC` §7). Do not omit untouched
components.

Once written, self-check with `bem_repair.py option-audit work/project.json`: it runs every legal
combination and names the state that has zero or two candidates, plus the worst-case draw count.
Remember that **the 256 ceiling is judged per mesh, not as a sum over targets** (`bem_v11.py:392-394`
applies `logic.maximum` to each selected mesh independently), so adding the per-mesh draw counts
together is a false alarm. See §13.15.

### 13.6 Pattern 5: BEM 1.5 Shader Overrides & Posture Correction

Map source INI tints, cutoff, transparent depth writes, and heel clipping directly:

```python
# 1. Material and shader overrides
draw_entry["material_overrides"] = {
    "floats": {
        "_TransparentDepthWrite": 1.0,
        "_Cutoff": 0.5,
        "_Smoothness": 0.3
    },
    "colors": {
        "_BaseColor": [1.0, 1.0, 1.0, 1.0],
        "_EmissionColor": [0.2, 0.2, 0.2, 1.0]
    },
    "disable_keywords": ["_ANISOTROPY_SPECULAR_ON"],
    "enable_keywords": ["_ALPHAPREMULTIPLY_ON"]
}

# 2. Root offset (for high heels clipping into the ground)
manifest["required_capabilities"].append("root-offset-v1")
manifest["root_offset"] = {
    "path": "Bip001",
    "translation": [0.0, 0.04, 0.0]
}
```

### 13.7 Pattern 6: Normalising duplicate entry INI sections (`ini-audit` / `ini-normalize`)

`component_auto.prepare()` reads the entry INI with `efmi_source.sections()`, which raises
`duplicate INI section` on **any** repeated name (`efmi_source.py:20-21`). That is a purely syntactic
gate — it fires whether or not the duplicate is harmless.

The loader itself has two **opposite** rules for duplicates, and swapping them changes behaviour:

| Section class | Examples | What a repeat actually does |
| --- | --- | --- |
| Prefix-less "global" | `Constants`, `Present`, `Rendering` | **Appends** in source order; a later assignment to the same variable wins |
| Prefixed | `TextureOverride*`, `CommandList*`, `Resource*`, `Pool*`, `Key*` | **First occurrence only**; later copies are parsed and discarded |

So "keep the first one" is wrong for a global, and "merge everything" is wrong for a prefixed section.

```bash
python bem_repair.py ini-audit F:/zmd/伊冯/伊冯-围裙/mod.ini
python bem_repair.py ini-normalize F:/zmd/.../mod.ini -o work/mod.norm.ini
```

`ini-normalize` proves two things before writing, and refuses if either fails:

1. **Projection unchanged**: the source and the result are each projected to `(per-section active
   statement vectors, key lookup table)` and the two must be equal — first assignment wins for
   prefixed sections, last assignment wins for globals. Otherwise `INI_PROJECTION_CHANGED` names the
   differing sections.
2. **Idempotence**: a second normalisation pass over the result must be a no-op, else
   `INI_NOT_IDEMPOTENT`.

It also refuses two shapes it will not infer: `namespace =` (an explicitly empty namespace changes
the global append rule) and an indented header (`efmi_source.sections()` requires `[` at column zero
while a human reader counts the section — the two readers disagree about what the file contains, so
the tool does not pick a side).

**Measured** (`伊冯-沁夏霓光`, three live INIs):

| File | Source | After normalisation |
| --- | --- | --- |
| `065f9a34….ini` | `efmi_source.sections()` raises `duplicate INI section: Constants` | 202 sections, passes |
| `ce1deef2….ini` | 5 sections, passes | 5 sections, passes (unchanged) |
| `f38d483e….ini` | raises `duplicate INI section: Constants` | 6 sections, passes |

In `065f9a34` both `[Constants]×3` and `[Present]×3` are append-semantics, with active statement
counts of `[27, 7, 43]` and `[1, 9, 54]` — all three copies carry content, so dropping any one of
them changes the result.

### 13.8 Pattern 7: Collapsing conditional texture overrides (`texture-normalize`)

`component_auto.texture_table` accepts exactly one shape:

```ini
[TextureOverride_X]
hash = <hex>
if $object_detected
this = ResourceY
endif
```

Sources often write `if $swapvar_body==0 / this = ResourceA / else / this = ResourceB / endif`. That
is **not a defect**, it is a state choice. `texture-normalize` resolves the branch for a stated
state, rewrites it into the accepted shape, and then re-walks its own output to prove the selected
resource did not change (`TEXTURE_SELECTION_CHANGED` if it did).

```bash
python bem_repair.py texture-normalize work/mod.norm.ini -o work/mod.tex.ini
python bem_repair.py texture-normalize in.ini -o out.ini --set '$swapvar_body=1'
```

The order in which the environment is built matters, and getting it backwards silently drops every
override: `Constants` declarations → built-in assumptions (`$object_detected=1`, `$mod_enabled=1`,
`$lod_level=0`, `$lod_detected=0`) → `--set`. `Constants` declares the **pre-detection** values
(commonly `$object_detected = 0`) while a conversion is by definition the detected, mod-enabled
state, so the assumptions must override the declarations. Measured on `伊冯-围裙`: with the order
reversed all 21 overrides were dropped; corrected, all 21 are selected.

Per-draw `ps-tN =` overrides are left alone and reported under `left_untouched` — collapsing one
would change which texture a draw binds.

Two more traps make a condition look unresolvable when the package is fine:

- **Variable name case.** 3Dmigoto variable names are case-insensitive: `$SocksColor` in `Constants`
  and `$sockscolor` in a condition are the same variable. An exact-match environment reports every
  such condition as unknown. Measured on `伊冯-沁夏霓光`: 5 texture sections refused before the fix,
  all 22 selected after it.
- **Resale watermarks.** Same rule as above — strip them with `bem_merge`'s patterns before
  structural analysis, or the banner line is read as an unsupported statement.

### 13.9 Pattern 8: Entry program lowering and template drift (`entry-normalize`)

`component_auto.validate_entry` accepts exactly one dispatcher program (`component_auto.py:106-111`).
When it fails, decide which case you are in first; the three are handled completely differently:

| Symptom | Reading | Action |
| --- | --- | --- |
| Old inline `if $mod_enabled && DRAW_TYPE == 4 / handling = skip / run = CommandListX / endif` | older template | **Lowerable**: rewritten to a fixed LOD0 callback + synthesised dispatcher + `$component_count` |
| Dispatcher is **missing** template statements | package older than template | **Upgradable**: the whole body is replaced with the canonical nine lines, and the added lines are reported |
| Dispatcher has **extra** statements the template does not know | package **newer** than the converter | **Refuse** `ENTRY_TEMPLATE_STALE` with the extra list; the converter template is what needs fixing |
| Component declares `$\EFMIv1\gpu_posed = 0` | its draw stays in the original skeleton | **Refuse** `ENTRY_CPU_POSED_COMPONENT`; BEM cannot re-target it |

The third case is the most important finding of this round. `伊冯-围裙` is a package the complex set
marks as **successful**, yet 10 of its 11 entries are blocked by
`ENTRY_PROGRAM: 分发流程超出已支持的 ComponentN 模板`, and the real difference is four **added**
statements:

```
$\EFMIv1\bones_count = $bones_count
$\EFMIv1\instance_count = $max_instance_count
$\EFMIv1\custom_mesh_scale = 1.00
CommandList\EFMIv1\Callback_MergedSkeleton_ConnectComponent = ref CommandList_MergedSkeleton_ConnectComponent
```

Deleting those four to "fix" the package would also delete merged-skeleton wiring, instance counts
and custom mesh scale. The correct conclusion is that **the converter template lags the EFMIv1
runtime**, and the template is what must be extended — not the package. The eleventh entry
(`Component10`) declares `gpu_posed = 0`, a different class that also cannot be rewritten.

The opposite case is `伊冯-沁夏霓光`: all 10 entries are old inline form (`handling = skip` inside the
guard), and after lowering 10/10 pass `validate_entry`, with the dispatcher and `$component_count = 10`
filled in automatically. Four of those entries only suppressed and never drew, so an empty
`CommandListBEMReviewedHideN` callback is synthesised for each, matching the convention in
`standard_worker.normalize_entries`.

Two details that are easy to get wrong:

- Resale watermarks (`【防倒卖：…】_702728`) are spliced in after `endif`. They are not statements; strip
  them with `bem_merge`'s own rule (no `=` and a bracketed CJK banner) before structural analysis, or
  2 of 10 entries are misjudged.
- The rewritten file is re-parsed and every entry is re-run through the real
  `component_auto.validate_entry`; anything that still fails raises `ENTRY_STILL_INVALID`, so an
  unverified file is never written. That check needs `component_auto`, so in a shipped toolchain this
  one subcommand refuses (`ENTRY_TEMPLATE_UNAVAILABLE`); the other fourteen are unaffected.

### 13.10 Pattern 9: Skin palette and vertex compaction (`skin-audit` / `skin-fix` / `vertex-compact`)

These three only apply to an **unpacked project** (`project.json` + `payloads/`). A packed `.bem`
stores payloads compressed, so "fix one vertex" would mean rewriting the whole container.

The rules come from the validator, not from taste: palette indices are UINT8 with at most 256
entries; every weighted vertex's weights sum to 65535 (UNORM16) or 1.0 (float); an index may exceed
the palette **only when its weight is zero**.

```bash
python bem_repair.py skin-audit work/project.json
python bem_repair.py skin-fix  work/project.json -o work/fixed
python bem_repair.py vertex-compact work/project.json --mesh 0 -o work/compact
```

`skin-fix` zeroes only out-of-palette indices on **zero-weight** slots; a weighted overrun is refused
(`SKIN_BONE_OUT_OF_PALETTE`) because zeroing it drops that geometry onto the root bone. Only
`--zero-weight-policy first` touches vertices whose four weights are all zero (giving the whole weight
to bone 0), which is a visible change and therefore opt-in. When no vertex needs a change it writes
nothing and returns `written: false` — copying tens of megabytes to prove there was no work is not a
repair.

`vertex-compact` has three constraints; miss one and it produces a worse package:

1. **A shared buffer must be compacted as one group.** When several meshes read the same vertex
   buffer, compacting one leaves the old buffer in place and the package grows. Measured on
   `洁尔佩塔-侦探`: mesh 0 alone compacts to 66477/203284, apparently saving two thirds — but mesh 4
   shares the same streams, and compacting the group yields 203284/203284, i.e. not one vertex is
   spare. The tool therefore decides per group and refuses (`VERTEX_COMPACT_NOOP`) rather than hand
   back a larger package.
2. **Row-by-row verification before writing.** For every draw and every index, the bytes of the
   corresponding row in the new buffer must equal the old one. That is a far stronger proof than
   "the triangle count is unchanged" — any change in position, weight or bone is caught.
3. **Orphaned payloads are pruned.** Compaction adds a new vertex buffer; leaving the old one behind
   only makes the package bigger. `prune_payloads` walks the whole manifest collecting `payload` /
   `indices` / `preview` references (animation frames, audio and the preview image included) and
   renumbers what survives.

### 13.11 Pattern 10: Merged-skeleton skin rewrite (`skin-convert`)

The moment an EFMI export wires `Callback_MergedSkeleton_ConnectComponent`, its skin stream carries
**global** bone ids, and a BEM mesh may only reference its own palette. Two source layouts, both of
which must be converted to a mesh-local palette:

| Source stride | Form | Contents |
| --- | --- | --- |
| 8 | rigid | 4 × UINT16 global bone ids |
| 16 | weighted | 4 × UNORM16 weights (offset 0) + 4 × UINT16 global ids (offset 8) |

Only two target layouts exist, matching native meshes: stride 12 (2 × U16 weights + 4 × U8 indices)
and stride 32 (4 × F32 weights + 4 × U32 indices).

```bash
python bem_repair.py skin-convert work/project.json --mesh 0 -o work/native
python bem_repair.py skin-convert work/project.json --mesh 0 --target-stride 32 -o work/native
python bem_repair.py skin-convert work/project.json --mesh 0 --palette palette.json -o work/native
```

The palette is built in **first-use order** (not bone-table order), every `(weight, bone name)` pair
is verified before writing and re-read afterwards. Five refusal codes: `SKIN_MERGED_PALETTE_TOO_LARGE`
(>256 bones — the mesh has to be split at that point), `SKIN_BONE_OUT_OF_TABLE`,
`SKIN_ALREADY_NATIVE`, `SKIN_NO_BONE_TABLE` (no `--palette` and the target component has no bone
table either) and `SKIN_LAYOUT_UNKNOWN`. When several meshes share one skin buffer the group is
rewritten once, using the same `_stream_group` logic as `vertex-compact`.

### 13.12 Pattern 11: Texture override classification and duplicate identities (`texture-audit`)

`component_auto.texture_table` accepts only `hash =` + `if $object_detected` + `this = Resource`.
`texture-audit` sorts every `TextureOverride*` section into four kinds and **reproduces the exact
error the automatic path would raise**:

| kind | Meaning | What the auto path concludes |
| --- | --- | --- |
| `static` | the accepted shape above | convertible |
| `per_draw` | `ps-tN = ResourceX` binds one slot for one draw | `TEXTURE_STATE: 不支持的贴图语句` |
| `empty` | hash only, no statements | `TEXTURE_STATE: 不完整的贴图覆盖` |
| `unsupported` | anything else (e.g. a `$active = 1` marker section) | `TEXTURE_STATE: 不支持的贴图语句` |

Only a **duplicate hash is a real blocker** (`blockers`): two sections claiming one identity means
the source itself is ambiguous about which texture wins, and no rewrite can settle it — ask the user.
Everything else lands in `manual_review` and must be carried across verbatim rather than dropped.
`ps-tN = null` is collected separately in `cleared_slots`, because BEM 1.5 expresses "bind nothing in
this slot" with `texture_slots` — that is not the same as omitting the slot.

Measured: `伊冯-围裙` has 11 entries, 21 static overrides, 0 duplicates. `诀-庆典` (M0488) has two
`TextureOverride.Active.Mark` sections carrying `$active = 1`; they land in `manual_review` without
blocking.

### 13.13 Pattern 12: Locating a character-catalog gap (`catalog-gap`)

`component_auto.match_catalog` requires **every** `ComponentN` identity to appear in **one and the
same** character's `catalog/*.json`. One missing hash rejects the whole package, and the error does
not say which one. `catalog-gap` answers that:

```bash
python bem_repair.py catalog-gap work/mod.ini
python bem_repair.py catalog-gap work/mod.ini --catalog-dir <tool root>/catalog
```

Five verdicts, each with a different response:

- `matched`: exactly one profile covers everything — auto-conversion is possible.
- `ambiguous-profile`: several do — `match_catalog` rejects it as ambiguous; check for a duplicated
  profile.
- `mixed-characters`: the missing identities **belong to other characters** — the pack mixes parts
  from more than one character and no single profile can ever satisfy it. `identities_present_elsewhere`
  names the owners.
- `unknown-identity`: nobody knows them — the "native resource ↔ EFMI hash" join itself is missing.
- `partial`: both of the above.

It also recognises four entry families, which is the precondition for judging convertibility:

| Entry family | Shape | Has a reader |
| --- | --- | --- |
| `component-n` | `TextureOverride_(EntryPoint_)?ComponentN` | yes (`component_auto.BASE`) |
| `hash-lod` | `TextureOverride_(EntryPoint_)?LODn.<hash>_<count>_<first>` | yes (`efmi_lod_source.ENTRY`) |
| `bare-hash` | `TextureOverride_<hash>_<count>_<first>` | **no** |
| `ib-component` | `TextureOverride_IB_<hash>_<label>_ComponentN` | **no** (only `bem_triage` reads it as a CrossIB redirect) |

The last two are reported as `ENTRY_FAMILY` blockers: even with every identity resolved they cannot
be lowered.

**One easy mistake**: in `ib-component` the 8 hex digits in the section name are **not** the draw
identity — they are the resource-group tag the author exported the vertex/index buffers under. The
real identity is the body's `hash =`. Measured: `TextureOverride_IB_b21b22ac_衣服_Component1` has
`hash = 586b4574`, so reading the identity from the name yields a hash nobody knows and also trips a
bogus "name disagrees with body" error. The tool therefore records the name's hash separately as
`resource_tag`. Likewise the `ComponentN` in that family repeats once per part (cloth/body/skirt all
have a `Component1`), so it cannot be used for duplicate detection the way `component-n` is.

Hash-named entries (`bare-hash`) carry their own index count and never consult a catalog, so there
`catalog_required: false` and a gap is informational only.

Measured over the 15-package complex set: 3 `matched`, 10 `unknown-identity`, 2 `not-required`
(the entry family has no reader but also needs no catalog). In other words, "the catalog is missing
identities" accounts for two thirds of this corpus — it is not an occasional case.

### 13.14 Pattern 13: Body parameters and delta payloads (`deformation-audit`)

`bem_v13.validate_manifest` raises on the first violation, so one bad frame reports one problem.
`deformation-audit` walks all the rules and then **actually decodes every delta payload**:

```bash
python bem_repair.py deformation-audit work/project.json --minor 3
```

It checks the points in spec §10 that are invisible without reading the code:

- frame ticks aligned to `min` by `step`, strictly increasing, and **covering both** `min` and `max`;
- exactly one `{"value":neutral,"neutral":true}` neutral frame, and it carries no payload;
- a non-neutral frame's fields are exactly `value/payload/count/encoding`; `count` may be 0 but not
  exceed the vertex count;
- the payload length is **exactly** `4 + J + 16N`, `J` in 1..4096, the header JSON is
  `{"count":N,"encoding":"sparse-position-f32"}`, indices in range and unique within the frame,
  deltas finite;
- the deformed mesh must have a Float32 XYZ position attribute (semantic 0, format 0, dimension 3).

`DEFORM_PAYLOAD_SIZE` and `DEFORM_PAYLOAD_HEADER` usually appear together: a frame claiming `count=3`
while the payload holds 2 records breaks both — the typical trace of a hand-edited frame table.

### 13.15 Pattern 14: Option-space enumeration and the 256 ceiling (`option-audit`)

`bem_v11.selection_plan` proves "exactly one candidate per target" with a symbolic decision graph,
which needs the builder modules (and therefore zstandard). `option-audit` **enumerates** the legal
combinations instead; the advantage is that it can name **which state** breaks a rule:

```bash
python bem_repair.py option-audit work/project.json
```

- A group whose `available_when` is false **has no value** during enumeration, and any `eq` against
  it is false — so "the group is hidden" is itself a state and cannot be modelled as a plain
  Cartesian product.
- A combination is legal only when every `selection_constraints` entry holds; the default
  combination must be legal too (`OPTION_DEFAULT_ILLEGAL`).
- Every rule is evaluated per state: 0 candidates → `OPTION_RULE_UNCOVERED`, ≥2 →
  `OPTION_RULE_AMBIGUOUS`; both carry the first offending state.
- **The 256 ceiling is judged per mesh, not as a sum over targets.** `bem_v11` applies
  `logic.maximum(active, ...) <= 256` to each selected mesh independently. Two meshes with 200 draws
  each never coexist, so they are not over the limit; adding their draw counts would be a false alarm.
- Above 250000 combinations the report sets `truncated: true` and says so in `note` instead of
  pretending the enumeration was exhaustive.

### 13.16 Pattern 15: CrossIB geometry grouping (`ib-groups`)

Source-side CrossIB auditing is impossible: once a redirect shader appears, a draw's range no longer
bounds against the buffer its `ib = ref X` names (which is why `bem_triage` skips it, see
`CROSS_IB_SECTION_SKIPPED`). After conversion the question becomes answerable again: group the meshes
by the **bytes they actually index**.

```bash
python bem_repair.py ib-groups work/project.json
```

- `shared-topology` (same index bytes, different vertices) is the normal CrossIB outcome: one
  topology, several skins.
- `exact_duplicates` names byte-identical meshes; the packer dedups payloads anyway, so this is
  informational.
- `same_count_variants` (same length, different bytes) is a topology variant, not a defect.
- `index_size_conflicts` is the only real error: one index buffer cannot be read at two widths.

### 13.17 Pattern 16: Shape keys as a slider (`shape-params`)

A source package's ShapeKey data is not "one pose" but "a whole adjustable range". The
`ShapeKeyVertexOffsets` buffer holds the **weight-1 vertex delta** and Unity blends it linearly in
the weight; the value in `[Constants]` is only the **starting point** the source author picked. So
emit two frames covering 0..1000 instead of baking the source default into the vertices and shipping
a static mesh.

```bash
python bem_repair.py shape-params "mod/yvonne" work/project.json --output work/body-morphs.json
```

The result is the `kind: bem-position-morphs` spec that `deformation-audit` and the builder already
consume:

```json
{"schema":1,"kind":"bem-position-morphs",
 "parameters":[{"id":"shape_0_0","name":"Component 0 shape 0","min":0,"max":1000,
                "neutral":0,"default":500,"step":1}],
 "mesh_deformations":[{"mesh":0,"parameter":"shape_0_0","frames":[
    {"value":0,"neutral":true},
    {"value":1000,"efmi":{"source":"../../mod/yvonne","ini":"main.ini","component":0,
                          "shape_key":0,"vertex_order":"exported"}}]}]}
```

- Frames carry an `efmi` binding, never copied offsets: decoding happens at build time in
  `efmi_shapes.decode_binding`, so the deltas always follow the source buffer and cannot drift
  through transcription.
- `default` comes from the source constant, so the slider starts where the source sits; `neutral`
  is always 0.
- Component-to-mesh mapping goes through the **default appearance**:
  `appearance.components[].target` indexes `target.components[].id`, and `.mesh` is the target mesh
  index. Components the default appearance does not replace are skipped and listed under `skipped`.
- Refusals and blockers: `SHAPE_DATA_ABSENT` (no declarations and no setter call at all),
  `SHAPE_NOT_DRIVEN` (buffers declared and initialised but **no caller ever runs the setter**, so the
  source sliders are dead at weight 0 and there is no default to convert — M0265 is exactly this:
  `[Constants]` only runs `CommandListInitializeShapeKeys`, `$shapekey_id` / `$shapekey_value` are 0,
  and `CommandListSetShapeKey` appears only inside a comment), `SHAPE_SOURCE_ARCHIVE` (rar/7z need an
  external backend — unpack first), `SHAPE_RESOURCE_MISSING`, `SHAPE_VERTEX_MAP_REQUIRED` (source
  vertex ids past the exported mesh need an explicit `vertex_map`), `SHAPE_KEY_OUT_OF_RANGE` (key
  past `batch*127`), `SHAPE_POSITION_ATTRIBUTE` (no Float32 XYZ position attribute),
  `SHAPE_NO_DRIVEN_KEY` (every weight for that component is zero).
- Run `deformation-audit` after writing the spec and before building: `shape-params` only decides
  which parameters exist and what they bind to; it does not replace format validation.

### 13.18 Self-Debugging & Verification Loop

The Agent must test and validate its custom build script in the workspace before presenting results:
1. Run custom script: `python workspace/build_mod.py`
2. Run validation: `python tools/CustomModel/bem_tool.py validate output.bem --report report.json`
3. Inspect `report.json`:
   - If `success == true`: Package passes data validation.
   - If failed: Inspect `issues`, self-patch `build_mod.py`, and repeat until valid.

### 13.19 Novice Delivery Summary Format

Deliver the result using plain language, omitting raw hex hashes:

```markdown
### Conversion Complete

- **Output Package**: `F:\...\package.bem` (verified compatible with the current game version)
- **Appearance & Controls**:
  - [Default Look]: Base outfit and hair.
  - [Part Toggles]: Hotkey toggles configured (e.g. jacket on/off, hair length).
- **Automated Repairs**:
  - [Texture Complete]: Missing secondary texture borrowed from matching sibling package.
  - [Mesh Trim]: Truncated 6 out-of-bounds trailing index points cleanly with zero visual artifacts.
  - [Posture Adjustment]: Added 0.04m root offset to eliminate heel ground clipping.
  - [Safe Filtering]: Excluded author 2D mouse overlay menu scripts.
```


---

## Appendix A: source-side framework reference

What a source INI calls is not always defined inside the source package. Four origins must be
distinguished; the handling differs completely:

| Origin | Signature | How to read | Conclusion |
| --- | --- | --- | --- |
| **Inside the source package** | `[CommandList...]` defined in this package | Read as an ordinary command list | Readable to the end; `bem_merge.py` computes it |
| **Loader's own library** | `CommandList\EFMIv1\*`, `Resource\EFMIv1\*` | Read the originals in `references/efmi/core/` | Definite semantics; static evaluation per the definition |
| **Third-party framework, published** | undefined in the package, but the framework has its own release page | Read via the interface facts in A.3; the framework itself is **not bundled** | Slots and delegated draws expand; material semantics still per package |
| **Obtainable nowhere** | undefined in the package and no findable source | Unknowable | Per-draw values unreadable; must be disclosed |

`references/efmi/` holds an **unmodified copy** of the library shipped with the EFMI loader
(`namespace = EFMIv1`) plus the official templates, for offline checking of "what does this statement
actually do". It is reference material: it is not built, and it is never loaded.

### A.1 What has definite semantics here

| Question | File |
| --- | --- |
| How texture overriding is triggered | `core/API.ini` (`OverrideTextures` and the per-resource identity checks) |
| How instances are identified and configured individually | `core/SpatialIdentification.ini`, `shaders/MergedSkeleton_InstanceConfigOverrider_*` |
| How bone instances are merged and offset | `core/MergedSkeleton.ini`, `shaders/MergedSkeleton_BoneData*` |
| How a shape key value becomes a vertex displacement | `core/ShapeKeys.ini`, `shaders/ShapeKeyDequantizer.hlsl` |
| What a standard source INI looks like | `mod.ini.j2`, `per_component.ini.j2` |

**How to read it**: section name first, then which slot or buffer each command writes, then follow the
`run` chain to the draw it lands on. Never assume "the name sounds like it does this" —
`OverrideTextures` sounds like it builds materials, but it only triggers a series of checks by resource
identity and defines no material itself.

### A.2 Three source behaviours that cannot be read statically

None of the three has any BEM expression, and **no amount of source completeness makes them automatic**:

1. **Per-instance decomposition** — one mesh processed instance by instance, each instance with its own
   configuration. A BEM draw is one index stream; there is no "per instance" layer.
2. **Per-instance spatial identity** — the source distinguishes "player character" from "passer-by" by the
   instance's position in space and only alters those judged to be the player. BEM has no runtime
   judgement and no input for "is this instance the player".
3. **Runtime-probed instance configuration buffer** — the slot number is found by trial, not fixed.

On encountering these, record and stop (see §12), do not "find a way to simulate". A hard-simulated
result is wrong in other contexts, and wrong quietly.

### A.3 Third-party frameworks: RabbitFX as the worked example

`RabbitFX` is a third-party framework by caverabbit, released per loader; **the EFMI edition targets
Endfield**: <https://gamebanana.com/mods/651557>. The source package normally contains only the *calls*.
The framework itself is installed in the game's mod directory and shared by every package.

**It is not bundled because of its licence, not its size.** The framework totals about 47 KB
(`RabbitFX.ini` 43 KB, 2.3 KB of HLSL, 0.5 KB of `deltaTime.ini`), so size is irrelevant, but the release page declares
**CC BY-NC-ND 4.0**: ND forbids distributing derivative versions and NC forbids commercial use, while
this project is AGPL-3.0, whose §10 forbids additional restrictions. The two cannot coexist, so this
document gives the link and the facts, not the files. Fetch the original from the link when you need to
check something, **and record the version you used in the conversion record** — the interface changes
between versions.

Each item below changes what a line of source actually means. These are facts, not code.

**① The slot name is the property declaration, not a convention.** The framework's first line is
`namespace = RabbitFX`, so the empty placeholder sections `[ResourceDiffuse]`, `[ResourceGlowMap]` are
stored by the engine under `Resource\RabbitFX\Diffuse`, `Resource\RabbitFX\GlowMap`. The caller writes:

```
Resource\RabbitFX\Diffuse = ref Resource_Texture16
```

which fills that placeholder. **To decide "what is this image", read the slot name** — no guessing:

| Slot | Meaning | Register | Bound in |
| --- | --- | --- | --- |
| `Diffuse` | base colour | `ps-t70` | `CommandList\RabbitFX\SetTextures` |
| `Lightmap` | light map | `ps-t71` | same |
| `Normalmap` | normal map | `ps-t72` | same |
| `DiscardMap` | discard mask | `ps-t73` | same |
| `Rainmap` | rain mask | `ps-t74` | same |
| `GlowMap` | emission | `ps-t60` | `CommandList\RabbitFX\Run` |
| `FXMap` | effect mask | `ps-t61` | same |

Both groups are guarded by `ps == $RabbitFXMain || ps == $RabbitFXShadow` and `DRAW_TYPE == 4`.
`$RabbitFXMain = 1718.1`, `$RabbitFXShadow = 1718.2`, `$RabbitFXShadowFace = 1718.3`: **the pixel shader
version number is the channel switch**.

**② A delegated draw has its parameters in the source and its action in the framework; only both halves
together are the draw.**

```
source:      $\RabbitFX\MeshStartIndex = 9762
             $\RabbitFX\MeshDrawAmount = 8256
             run = CommandList\RabbitFX\DrawMesh
framework:   if $MeshStartIndex >= 0 && $MeshDrawAmount > 0
                 if vs == 1718.1
                     drawindexedinstanced = $MeshDrawAmount, INSTANCE_COUNT, $MeshStartIndex, 0, FIRST_INSTANCE
```

So `first = MeshStartIndex` and `count = MeshDrawAmount`; `vs == 1718.1` is the body pass, `1718.2` the
shadow pass (`DrawShadow`). The two sides spell it differently because the framework's own `namespace`
supplies the prefix — the source writes `$\RabbitFX\MeshStartIndex`, the framework writes
`$MeshStartIndex`, and it is one variable (variables follow the same rule as section names).
The framework resets both parameters to `-1` afterwards, **so one assignment covers only the single draw
that follows it**. Declaring "the draw is unknowable" on seeing `DrawMesh` is wrong: it expands, you just
have to read both sides. What is genuinely unknowable is a call whose framework cannot be obtained at all.

**③ The skeleton buffer is borrowed and returned, not a new skeleton.** The caller hands a buffer over
with `Resource\RabbitFX\T0 = ref ResourceT0`, the framework writes bones into it, and the caller takes it
back with `ResourceT0 = copy Resource\RabbitFX\T0`; `$componentOffset`, `$boneOffset`, `$totalBones` are
layout parameters the caller supplies. Conversion only needs the final buffer contents — reproducing the
framework's actions would hardcode guessed layout values.

**④ It fights `ShaderOverride` / `ShaderRegex`.** The framework's bulk is 17 `ShaderRegex` groups that
replace the game's shadow and emission shaders outright. When another installed package overrides the
same pass, they contend for the same shader and the package using the skeleton system turns entirely
invisible. That is a conflict in the machine's installed set, and the only conversion conclusion is: this
package cannot be handled automatically, record it per §12.

**⑤ The framework's values are a constant buffer, not a colour.** `Run` writes
`$h/$s/$v/$brightness/$interpolate` into the `x/y/z/w` components of constant buffer 217 (HSV plus
interpolation), on top of switch bits in 172. These are runtime parameters: BEM can only express them as
slider parameters, never as a texture and never as a fixed colour.

**⑥ Matching one spelling misses the rest.** The corpus writes the same namespace as both `RabbitFX` and
`RabbitFx`, and `Commandlist` also appears lower-cased. Match case-insensitively.

**How to use these facts**: is this image the base colour → the slot name (①); what index range does a
draw cover → combine the source parameters with the formula in ②; is this section a skeleton extension →
look for the `T0`/`CB1` round trip and a `componentOffset` assignment (③). None of the three requires the
framework itself.

---

## Appendix B: one-page flow card

```
-1 resolve effective sections (prerequisite for everything below)
   bem_merge.py <package>
   first section wins, include expansion, namespace rewriting, watermark stripping
   list discarded sections and why; skipping this duplicates counts everywhere downstream
⓪ mechanical triage (5 minutes, saves hours)
   bem_triage.py <package>
   missing resources in three tiers: draw-path=blocker / framework channel=warning / unused=harmless
   index overrun: report first+count vs capacity; skip CrossIB sections
   entry family: ComponentN / HASH_N_N / LODN.HASH_N_N; TextureN is not an entry
① read the entry
   outer directory → entry INI → reachable state list (including persistent key initial values)
   bem_state_probe.py <package>  what each state draws, which texture it uses
   framework-delegated draws (DrawMesh and friends): add source parameters to framework actions (A.3)
② establish identity
   catalog/alias library → byte comparison → geometry comparison → descriptor recompute → cross-package reuse
   bem_peer.py --target <package>   find same-byte donors when files are missing (candidate ≠ proof of identity)
   record L1/L2/L3 for each; L4 is not used
③ verify integrity
   IB/VB four criteria → repairable / unrepairable
   source's own errors (typos / segment counts / undeclared) recognised separately
④ align structure
   one-to-one / one-to-many (classify by triangle) / many-to-one (twin submeshes)
   split merged meshes + verify weights block by block
⑤ map skeleton
   palette ≤120 → group offsets → bone names → verify total weighted references
   generational shifts handled separately (top-down remap)
⑥ route materials
   check the target material's own table first → cost of a whole-material donor → bind per draw
   keep components: check consumers → suppress slot residue → infer ps-t per package
⑦ install states
   count reachable states correctly → fold where folding has comparison evidence → conditional draws
⑧ draw the exclusion line
   walk the decision tree; judge animation by "worth it", not "possible"
⑨ write the package + validate + disclose
   temp file → read back → byte-equal → atomic replace → read back again
   two-sided offline + grounds for every property + four required fields
   re-read the package at registration and recompute appearances; do not trust the report
   bem_audit.py  periodically cross-check ledger / report / artifact
```

**One last sentence**: no rule in this document replaces "go and find the consumer". Every decision
returns to the same question — **which draw, which material slot, which bone actually uses this data**.
Answer it and the work is doable; fail to answer it and record a failure, rather than fudging with similarity.

---

## Appendix C: toolchain

Six scripts under `scripts/`, standard library only, read-only, never writing to the source.
Run `python scripts/<name>.py <package dir>`.

| Script | Used at | Gives |
| --- | --- | --- |
| `bem_merge.py` | before ⓪ | The sections that **actually take effect** per loader rules (first occurrence of a name wins, include expansion, the file's own `namespace =` rewriting, watermark stripping), plus which sections were discarded and why. This is the step that turns a framework's slot names back into readable form, e.g. `Resource\RabbitFX\Diffuse` |
| `bem_triage.py` | ⓪ triage | Entry family, missing-payload tiers (a framework channel is graded by identity in the name → slot name → filename role word, and the report says which), index overrun, output-channel contamination, whether the framework definition is in the package, verdict and blockers |
| `bem_state_probe.py` | ① read entry | Key domain, reachable states, per-state texture resolution, zero-index draws, risks |
| `bem_peer.py` | ② when files are missing | Same-byte donors in sibling packages; same-name-different-size listed separately as a conflict |
| `bem_audit.py` | after ⑨ | Ledger / report / artifact inconsistency list |
| `bem_ini.py` | called by the above | INI parsing and condition evaluation; undefined identifiers return `None`, never 0 |

Only two hard rules of use:

- **Run `bem_merge` before the others.** Three of them read the effective sections from it; without it,
  same-named sections are counted more than once.
- **`bem_peer` candidates are not proof of identity.** It answers "does this byte string also exist in
  another package", nothing more; whether to substitute is decided by identity anchoring.

All six follow one principle: **report facts, do not decide for the executor**. Overruns report capacity
and requirement, repairability per the four criteria; identity reports zero hits, never falling back to
ordinals; missing payloads report where they are referenced, with blocker-versus-warning decided by the
referencing channel.
