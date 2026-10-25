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

### 9.3 Writing the package: atomic replace + byte-exact readback

Three actions must happen in sequence, or an interruption leaves a half-written artifact:

```
write temp file → read back → byte-equal to the in-memory model → atomic replace to the real path → read back again
```

```python
fd, temp = tempfile.mkstemp(prefix=output.stem + '.', suffix='.bem.tmp', dir=output.parent)
merged.write(temp)
m, p = read_package(temp)
assert m == merged.manifest and p == merged.payloads   # ROUNDTRIP
os.replace(temp, output)                                # atomic, never half a package
m2, p2 = read_package(output)
assert (m2, p2) == (m, p)                               # FINAL_READBACK
```

Why not "write in place, then check": if power fails halfway, the disk holds a file whose header
declares everything while payloads are missing — and every "file exists" check passes.
`os.replace` is atomic within one partition; readers see either the old file or the new one.

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

In the following cases, **continuing only produces something that looks legal and is actually wrong**.
Stop, organise the evidence, and hand it over. Do not fudge with similarity, and do not use "it passed
validation" as an excuse.

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
