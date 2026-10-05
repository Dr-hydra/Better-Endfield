# Character walk-loop inventory

Generated: `2026-08-04T01:42:00.834303+00:00`

The exact loop criterion is `m_MuscleClip.m_LoopTime == true` from exported 
Unity `AnimationClip` JSON. The usable count additionally excludes additive and 
`entry_pose` clips because those are not standalone full-body walk replacements.

| Character | Walk clips | Metadata checked | Loop walk clips | Usable loop walk clips | Neutral loop clips |
| --- | ---: | ---: | ---: | ---: | ---: |
| `chr_0002_endminm` | 15 | 15 | 4 | 4 | 1 |
| `chr_0003_endminf` | 15 | 15 | 4 | 4 | 1 |
| `chr_0004_pelica` | 9 | 9 | 3 | 3 | 1 |
| `chr_0005_chen` | 7 | 7 | 2 | 2 | 0 |
| `chr_0006_wolfgd` | 3 | 3 | 0 | 0 | 0 |
| `chr_0007_ikut` | 3 | 3 | 0 | 0 | 0 |
| `chr_0009_azrila` | 0 | 0 | 0 | 0 | 0 |
| `chr_0011_seraph` | 3 | 3 | 0 | 0 | 0 |
| `chr_0012_avywen` | 4 | 4 | 0 | 0 | 0 |
| `chr_0013_aglina` | 6 | 6 | 1 | 1 | 0 |
| `chr_0014_aurora` | 3 | 3 | 0 | 0 | 0 |
| `chr_0015_lifeng` | 6 | 6 | 1 | 1 | 0 |
| `chr_0016_laevat` | 7 | 7 | 2 | 1 | 0 |
| `chr_0017_yvonne` | 10 | 10 | 2 | 2 | 0 |
| `chr_0018_dapan` | 3 | 3 | 0 | 0 | 0 |
| `chr_0019_karin` | 0 | 0 | 0 | 0 | 0 |
| `chr_0020_meurs` | 0 | 0 | 0 | 0 | 0 |
| `chr_0021_whiten` | 0 | 0 | 0 | 0 | 0 |
| `chr_0022_bounda` | 0 | 0 | 0 | 0 | 0 |
| `chr_0023_antal` | 0 | 0 | 0 | 0 | 0 |
| `chr_0024_deepfin` | 0 | 0 | 0 | 0 | 0 |
| `chr_0025_ardelia` | 3 | 3 | 0 | 0 | 0 |
| `chr_0026_lastrite` | 0 | 0 | 0 | 0 | 0 |
| `chr_0027_tangtang` | 8 | 8 | 0 | 0 | 0 |
| `chr_0028_wulfa` | 8 | 8 | 1 | 1 | 0 |
| `chr_0029_pograni` | 0 | 0 | 0 | 0 | 0 |
| `chr_0030_zhuangfy` | 6 | 6 | 1 | 1 | 0 |
| `chr_0031_mifu` | 6 | 6 | 1 | 1 | 1 |
| `chr_0032_lizhiyan` | 4 | 4 | 1 | 0 | 0 |
| `chr_0033_camille` | 7 | 7 | 1 | 0 | 0 |
| `chr_0035_liino` | 6 | 6 | 0 | 0 | 0 |
| `chr_0036_jsspsi` | 6 | 6 | 0 | 0 | 0 |

Characters: **32**  
Walk clips: **148**  
Virtual single-walk clips: **69**, loop-enabled: **0**  
Walk clips with exact metadata: **148**  
Characters with any loop walk: **13**  
Characters with a usable non-additive loop walk: **11**
  
Characters with a conservatively named neutral walk loop: **4**

## Interpretation

- All **69** character-owned `virtual_single_walk_[s/m/l]` clips have `m_LoopTime == false`. This includes all three Li Zhiyan clips, so the current final animation has no native loop flag to preserve.
- **13 / 32** characters own at least one loop-enabled walk-named clip, but only **11** remain after excluding additive and entry-pose clips. Most are scenario-specific poses such as carrying a plate, shy walking, hands-behind-back, or walk-and-sit.
- Only **4 / 32** have the conservative plain `dialog_state_walk_loop` form: Endministrator male, Endministrator female, Perlica, and Mifu. A neutral character-owned loop is therefore a special case in this asset set, not the default.
- Li Zhiyan's only loop-enabled walk-named clip is `dialog_single_walk_loop_hurt_additive`; it is an additive hurt overlay, not a standalone replacement for the final full-body walk.
- **9** characters have no character-owned walk clip under this folder rule. They may rely on shared rig-family locomotion, so this catalog does not claim those characters have no walk animation anywhere in the game.

## Exact loop-walk clips

- `chr_0002_endminm` — `a_actor_endminm_dialog_state_walk_loop` (`walk_loop_named`, 2.0833335 s)
- `chr_0002_endminm` — `a_actor_endminm_dialog_state_walk_longpaopao_loop` (`walk_loop_named`, 1.0666667 s)
- `chr_0002_endminm` — `a_actor_endminm_dialog_state_walksit_loop` (`walk_loop_named`, 6.5 s)
- `chr_0002_endminm` — `a_actor_endminm_dialog_state_walksit_derived_getdocument_loop` (`walk_loop_named`, 3.666668 s)
- `chr_0003_endminf` — `a_actor_endminf_dialog_state_walk_loop` (`walk_loop_named`, 2.0833335 s)
- `chr_0003_endminf` — `a_actor_endminf_dialog_state_walk_longpaopao_loop` (`walk_loop_named`, 1.0666667 s)
- `chr_0003_endminf` — `a_actor_endminf_dialog_state_walksit_loop` (`walk_loop_named`, 6.5 s)
- `chr_0003_endminf` — `a_actor_endminf_dialog_state_walksit_derived_getdocument_loop` (`walk_loop_named`, 3.666668 s)
- `chr_0004_pelica` — `a_actor_pelica_dialog_state_holdwalk_loop` (`walk_loop_named`, 10.000001 s)
- `chr_0004_pelica` — `a_actor_pelica_dialog_state_walk_loop` (`walk_loop_named`, 2.0833335 s)
- `chr_0004_pelica` — `a_actor_pelica_dialog_state_walk_b` (`walk_other`, 6.066667 s)
- `chr_0005_chen` — `a_actor_chen_dialog_state_platewalk_loop` (`walk_loop_named`, 10.000001 s)
- `chr_0005_chen` — `a_actor_chen_dialog_state_scratchheadwalk_loop` (`walk_loop_named`, 1.0666667 s)
- `chr_0013_aglina` — `a_actor_aglina_dialog_state_shy2_walk_loop` (`walk_loop_named`, 8.150001 s)
- `chr_0015_lifeng` — `a_actor_lifeng_dialog_state_walkthink_loop` (`walk_loop_named`, 10.250001 s)
- `chr_0016_laevat` — `a_actor_laevat_walk_loop_entry` (`walk_loop_named`, 1.0666667 s)
- `chr_0016_laevat` — `a_actor_laevat_walk_loop_entry_pose` (`walk_loop_named`, 1.0666667 s)
- `chr_0017_yvonne` — `a_actor_yvonne_dialog_state_walkin_loop` (`walk_loop_named`, 6.2500005 s)
- `chr_0017_yvonne` — `a_actor_yvonne_dialog_state_walkinsilent_loop` (`walk_loop_named`, 3.583334 s)
- `chr_0028_wulfa` — `a_actor_wulfa_dialog_state_akimbo2_shield_followme_walk_loop` (`walk_loop_named`, 4.333335 s)
- `chr_0030_zhuangfy` — `a_actor_zhuangfy_dialog_state_handbackwalk_loop` (`walk_loop_named`, 1.9000001 s)
- `chr_0031_mifu` — `a_actor_mifu_dialog_state_walk_loop` (`walk_loop_named`, 2.0833335 s)
- `chr_0032_lizhiyan` — `a_actor_lizhiyan_dialog_single_walk_loop_hurt_additive` (`walk_additive`, 1.0666667 s)
- `chr_0033_camille` — `a_actor_camille_dialog_single_walk_loop_careful_additive` (`walk_additive`, 1.0666667 s)
