#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
BetterEndfield Combat Data Exporter & Incremental Sync Tool
==========================================================
Extracts official Chinese names, identifiers, attributes, and semantic bindings
from raw dumped game tables and generates a unified, ultra-compact dictionary for
both the desktop client and web analyzer platform.

Features:
- Full extraction: Characters, Skills, Talents, Weapons, Suits, Dungeons, Series, Buff Semantics
- Incremental Diff & Changelog: Detects added/modified/removed items on game updates
- Multi-target output: Pretty JSON, Minified Web Bundle (.min.json), Diff Report
- Referenced icon extraction: game VFS -> optimized web PNGs with strict coverage
- Deterministic hashing & strict verification
"""

import argparse
import collections
import hashlib
import json
import os
import shutil
import subprocess
import sys
import tempfile
from datetime import datetime, timezone
from pathlib import Path
from typing import Any, Dict, List, Optional, Set, Tuple

if hasattr(sys.stdout, "reconfigure"):
    sys.stdout.reconfigure(encoding="utf-8")
if hasattr(sys.stderr, "reconfigure"):
    sys.stderr.reconfigure(encoding="utf-8")


def sha256_file(path: Path) -> str:
    h = hashlib.sha256()
    with open(path, "rb") as f:
        while chunk := f.read(65536):
            h.update(chunk)
    return h.hexdigest()


class GameTableReader:
    def __init__(self, table_dir: Path):
        self.table_dir = table_dir
        self._cache: Dict[str, Any] = {}
        self._i18n: Dict[str, str] = {}
        self._load_i18n()

    def _load_i18n(self) -> None:
        path = self.table_dir / "I18nTextTable_CN.json"
        if not path.exists():
            print(f"[!] Warning: {path} not found. Localized names will be empty.")
            return
        with open(path, "r", encoding="utf-8") as f:
            self._i18n = json.load(f)
        print(f"[*] Loaded {len(self._i18n):,} localized strings from I18nTextTable_CN")

    def get_text(self, id_dict_or_id: Any) -> str:
        if isinstance(id_dict_or_id, dict):
            tid = str(id_dict_or_id.get("id", ""))
        else:
            tid = str(id_dict_or_id)
        if not tid or tid == "0":
            return ""
        return self._i18n.get(tid, "").strip()

    def load_table(self, filename: str) -> Dict[str, Any]:
        if filename in self._cache:
            return self._cache[filename]
        path = self.table_dir / filename
        if not path.exists():
            print(f"[!] Warning: Table {filename} not found.")
            return {}
        with open(path, "r", encoding="utf-8") as f:
            data = json.load(f)
        self._cache[filename] = data
        return data


def extract_characters(reader: GameTableReader) -> Dict[str, Any]:
    chars = reader.load_table("CharacterTable.json")
    result = {}
    for cid, c in chars.items():
        if not isinstance(c, dict):
            continue
        name = reader.get_text(c.get("name"))
        if not name:
            continue
        result[cid] = {
            "name": name,
            "engName": reader.get_text(c.get("engName")),
            "rarity": c.get("rarity", 0),
            "profession": c.get("profession", 0),
            "weaponType": c.get("weaponType", 0),
            "charTypeId": c.get("charTypeId", ""),
            "department": c.get("department", ""),
            "defaultWeaponId": c.get("defaultWeaponId", ""),
            "iconAvatar": f"icon_head_{cid}",
        }
    return dict(sorted(result.items()))


def extract_skills_and_talents(reader: GameTableReader) -> Tuple[Dict[str, Any], Dict[str, Any]]:
    growth = reader.load_table("CharGrowthTable.json")
    patch = reader.load_table("SkillPatchTable.json")
    skills = {}
    talents = {}

    for cid, g in growth.items():
        if not isinstance(g, dict):
            continue
        # 1. Character Skills
        sg = g.get("skillGroupMap", {})
        for stype, sinfo in sg.items():
            if not isinstance(sinfo, dict):
                continue
            sname = reader.get_text(sinfo.get("name"))
            sdesc = reader.get_text(sinfo.get("desc"))
            if sname:
                category = "attack"
                if "comboskill" in stype.lower():
                    category = "combo"
                elif "ultimateskill" in stype.lower():
                    category = "ultimate"
                elif "normalskill" in stype.lower():
                    category = "skill"
                elif "normalattack" in stype.lower():
                    category = "attack"

                skills[stype] = {
                    "characterId": cid,
                    "name": sname,
                    "category": category,
                    "desc": sdesc,
                    "iconId": sinfo.get("icon", ""),
                    "skillIds": (
                        sinfo.get("skillIdList", [])
                        if isinstance(sinfo.get("skillIdList", []), list)
                        else [sinfo.get("skillIdList")]
                    ),
                }

        # 2. Character Talents
        tn = g.get("talentNodeMap", {})
        for tid, tinfo in tn.items():
            if not isinstance(tinfo, dict):
                continue
            pnode = tinfo.get("passiveSkillNodeInfo", {})
            tname = reader.get_text(pnode.get("name"))
            eff_id = pnode.get("talentEffectId", "")
            if tname:
                key = eff_id if eff_id else tid
                talents[key] = {
                    "characterId": cid,
                    "nodeId": tid,
                    "name": tname,
                    "iconId": pnode.get("iconId", ""),
                }

    # 3. Add standalone skill patches (e.g. wpn skills, general skills)
    for pkey, pval in patch.items():
        if pkey in skills or not isinstance(pval, dict):
            continue
        bundles = pval.get("SkillPatchDataBundle", [])
        if bundles and isinstance(bundles[0], dict):
            pname = reader.get_text(bundles[0].get("skillName"))
            pdesc = reader.get_text(bundles[0].get("skillDesc"))
            if pname:
                skills[pkey] = {
                    "name": pname,
                    "category": "passive_patch",
                    "desc": pdesc,
                }

    return dict(sorted(skills.items())), dict(sorted(talents.items()))


def extract_weapons(reader: GameTableReader) -> Dict[str, Any]:
    items = reader.load_table("ItemTable.json")
    weapons = reader.load_table("WeaponBasicTable.json")
    patch = reader.load_table("SkillPatchTable.json")
    result = {}

    for wid, w in weapons.items():
        if not isinstance(w, dict):
            continue
        item_info = items.get(wid, {})
        wname = reader.get_text(item_info.get("name"))
        pskill_id = w.get("weaponPotentialSkill", "")
        pskill_name = ""
        if pskill_id in patch:
            bundles = patch[pskill_id].get("SkillPatchDataBundle", [])
            if bundles and isinstance(bundles[0], dict):
                pskill_name = reader.get_text(bundles[0].get("skillName"))

        result[wid] = {
            "name": wname if wname else wid,
            "rarity": w.get("rarity", 0),
            "weaponType": w.get("weaponType", 0),
            "iconId": item_info.get("iconId", wid),
            "potentialSkillId": pskill_id,
            "potentialSkillName": pskill_name,
        }
    return dict(sorted(result.items()))


def extract_suits(reader: GameTableReader) -> Dict[str, Any]:
    suits = reader.load_table("EquipSuitTable.json")
    patch = reader.load_table("SkillPatchTable.json")
    result = {}

    for sid, s in suits.items():
        if not isinstance(s, dict):
            continue
        slist = s.get("list", [])
        if not slist or not isinstance(slist[0], dict):
            continue
        sitem = slist[0]
        sname = reader.get_text(sitem.get("suitName"))
        pskill = sitem.get("skillID", "")
        pskill_name = ""
        if pskill in patch:
            bundles = patch[pskill].get("SkillPatchDataBundle", [])
            if bundles and isinstance(bundles[0], dict):
                pskill_name = reader.get_text(bundles[0].get("skillName"))

        result[sid] = {
            "name": sname if sname else sid,
            "skillId": pskill,
            "skillName": pskill_name,
            "logoName": sitem.get("suitLogoName", ""),
            "bonuses": [
                {
                    "count": item.get("equipCnt", 0),
                    "skillId": item.get("skillID", ""),
                    "skillLevel": item.get("skillLv", 0),
                }
                for item in slist
                if isinstance(item, dict)
            ],
        }
    return dict(sorted(result.items()))


def extract_equipment(reader: GameTableReader) -> Dict[str, Any]:
    items = reader.load_table("ItemTable.json")
    equipment = reader.load_table("EquipTable.json")
    result = {}
    for equip_id, equip in equipment.items():
        if not isinstance(equip, dict):
            continue
        item = items.get(equip_id, {})
        modifiers = []
        base = equip.get("displayBaseAttrModifier")
        if isinstance(base, dict):
            modifiers.append({
                "index": base.get("attrIndex", 0),
                "type": base.get("attrType", 0),
                "value": base.get("attrValue", 0),
                "modifierType": base.get("modifierType", 0),
                "base": True,
            })
        for modifier in equip.get("displayAttrModifiers", []):
            if not isinstance(modifier, dict):
                continue
            modifiers.append({
                "index": modifier.get("attrIndex", 0),
                "type": modifier.get("attrType", 0),
                "value": modifier.get("attrValue", 0),
                "modifierType": modifier.get("modifierType", 0),
                "enhancedValues": modifier.get("enhancedAttrValues", []),
                "base": False,
            })
        result[equip_id] = {
            "name": reader.get_text(item.get("name")) or equip_id,
            "rarity": item.get("rarity", 0),
            "iconId": item.get("iconId", equip_id),
            "partType": equip.get("partType", 0),
            "suitId": equip.get("suitID", ""),
            "minWearLevel": equip.get("minWearLv", 0),
            "modifiers": modifiers,
        }
    return dict(sorted(result.items()))


def extract_dungeons_and_series(reader: GameTableReader) -> Tuple[Dict[str, Any], Dict[str, str]]:
    dungeons = reader.load_table("DungeonTable.json")
    series = reader.load_table("DungeonSeriesTable.json")
    cat2nd = reader.load_table("DungeonCategory2ndTable.json")

    series_dict = {}
    for srid, sr in series.items():
        if not isinstance(sr, dict):
            continue
        sname = reader.get_text(sr.get("name"))
        if sname:
            series_dict[srid] = sname

    category_tags = {}
    for cid, c in cat2nd.items():
        if isinstance(c, dict):
            cname = reader.get_text(c.get("name")) or reader.get_text(c.get("categoryName"))
            if cname:
                category_tags[str(cid)] = cname

    dung_dict = {}
    for did, d in dungeons.items():
        if not isinstance(d, dict):
            continue
        dname = reader.get_text(d.get("dungeonName"))
        subname = reader.get_text(d.get("dungeonSubName"))
        srid = d.get("dungeonSeriesId", "")
        dung_dict[did] = {
            "name": dname if dname else did,
            "subName": subname,
            "seriesId": srid,
            "seriesName": series_dict.get(srid, ""),
            "domainId": d.get("domainId", ""),
            "costStamina": d.get("costStamina", 0),
        }

    return dict(sorted(dung_dict.items())), dict(sorted(series_dict.items()))


def extract_buff_semantics(besem_path: Optional[Path]) -> Dict[str, Any]:
    result: Dict[str, Any] = {}
    exact_sources: Dict[str, Set[Tuple[str, str, str]]] = {}
    if besem_path and besem_path.exists():
        with open(besem_path, "r", encoding="utf-8") as f:
            for line in f:
                parts = line.strip().split("\t")
                if not parts:
                    continue
                if parts[0] == "E" and len(parts) >= 5:
                    buff_id = parts[1]
                    status = parts[2]
                    zone = parts[3]
                    element = parts[4]
                    rate = parts[6] if len(parts) > 6 else ""
                    source_kind = parts[8] if len(parts) > 8 else ""
                    source_id = parts[9] if len(parts) > 9 else ""
                    skill_id = parts[10] if len(parts) > 10 else ""
                    confidence = parts[11] if len(parts) > 11 else ""
                    result[buff_id] = {
                        "status": status,
                        "zone": zone,
                        "element": element,
                        "rate": rate,
                    }
                    if (source_kind and source_id and
                            (status in {"verified", "verified_alias"} or
                             confidence == "compiled_from_unpack")):
                        exact_sources.setdefault(buff_id, set()).add(
                            (source_kind, source_id, skill_id)
                        )
                elif parts[0] == "B" and len(parts) >= 3:
                    num_id = parts[1]
                    buff_id = parts[2]
                    if buff_id not in result:
                        result[buff_id] = {
                            "numericId": int(num_id) if num_id.isdigit() else 0,
                            "status": "numeric_mapped",
                            "zone": "other",
                            "element": "all",
                        }
                    else:
                        result[buff_id]["numericId"] = int(num_id) if num_id.isdigit() else 0

    for buff_id, entry in result.items():
        sources = exact_sources.get(buff_id, set())
        source_pairs = {(kind, source_id) for kind, source_id, _ in sources}
        if len(source_pairs) == 1:
            kind, source_id = next(iter(source_pairs))
            skills = {skill for _, _, skill in sources if skill}
            entry["sourceKind"] = kind
            entry["sourceId"] = source_id
            entry["skillId"] = next(iter(skills)) if len(skills) == 1 else ""
            entry["sourceAttribution"] = "dictionaryExact"
            if len(skills) > 1:
                entry["attributionReason"] = "conflicting_source_skill_ids"
        else:
            entry["sourceKind"] = ""
            entry["sourceId"] = ""
            entry["skillId"] = ""
            entry["sourceAttribution"] = "unknown"
            entry["attributionReason"] = (
                "conflicting_source_templates" if len(source_pairs) > 1
                else "no_exact_source_mapping"
            )

    # Poise Break default semantics
    poise_buffs = {
        "buff_common_poise_break_damage_taken_scale": {
            "status": "verified", "zone": "vuln_taken", "element": "all", "desc": "破韧失衡易伤增幅"
        },
        "buff_common_poise_can_be_breaking_attacked": {
            "status": "verified", "zone": "vuln_taken", "element": "all", "desc": "失衡可处决重击状态"
        },
        "buff_common_mini_poise_break": {
            "status": "verified", "zone": "vuln_taken", "element": "all", "desc": "微失衡/破韧节点"
        },
        "buff_common_temp_poise_break": {
            "status": "verified", "zone": "vuln_taken", "element": "all", "desc": "瞬时失衡"
        },
    }
    for p_id, p_info in poise_buffs.items():
        if p_id in result:
            result[p_id].update(p_info)
        else:
            result[p_id] = p_info

    return dict(sorted(result.items()))


def build_buff_source_index(
    reader: GameTableReader,
    json_data_dir: Path,
) -> Tuple[Dict[str, Set[Tuple[str, str, str, str]]], Dict[str, Any], Dict[str, Set[str]]]:
    """Build exact Buff ownership candidates from authoritative references."""
    skill_dir = json_data_dir / "SkillData"
    buff_dir = json_data_dir / "BuffData"
    if not skill_dir.is_dir() or not buff_dir.is_dir():
        raise FileNotFoundError(
            "JsonData SkillData/BuffData cache is incomplete: " +
            str(json_data_dir.resolve())
        )

    skill_files = {path.stem.lower(): path for path in skill_dir.glob("*.json")}
    buff_files = {path.stem.lower(): path for path in buff_dir.glob("*.json")}
    known_ids = set(skill_files) | set(buff_files)
    graph: Dict[str, Set[str]] = {}
    source_hash = hashlib.sha256()
    import re
    token_pattern = re.compile(rb"[A-Za-z0-9_]{4,}")
    for config_id, path in sorted(
        [*skill_files.items(), *buff_files.items()], key=lambda item: item[0]
    ):
        payload = path.read_bytes()
        source_hash.update(config_id.encode("utf-8"))
        source_hash.update(hashlib.sha256(payload).digest())
        tokens = {
            value.decode("ascii").lower()
            for value in token_pattern.findall(payload)
        }
        graph[config_id] = tokens & known_ids

    # kind, template id, source/display skill id, traversal root
    roots: Set[Tuple[str, str, str, str]] = set()
    growth = reader.load_table("CharGrowthTable.json")
    talent_owner: Dict[str, str] = {}
    for char_id, value in growth.items():
        if not isinstance(value, dict):
            continue
        for skill_group in value.get("skillGroupMap", {}).values():
            if not isinstance(skill_group, dict):
                continue
            skill_ids = skill_group.get("skillIdList", [])
            if not isinstance(skill_ids, list):
                skill_ids = [skill_ids]
            for skill_id in skill_ids:
                if skill_id:
                    root = str(skill_id).lower()
                    roots.add(("characterSkill", char_id, root, root))
        for node in value.get("talentNodeMap", {}).values():
            if not isinstance(node, dict):
                continue
            effect_id = node.get("passiveSkillNodeInfo", {}).get(
                "talentEffectId", ""
            )
            if effect_id:
                talent_owner[str(effect_id).lower()] = char_id

    potential_owner: Dict[str, str] = {}
    for char_id, value in reader.load_table("CharacterPotentialTable.json").items():
        if not isinstance(value, dict):
            continue
        for unlock in value.get("potentialUnlockBundle", []):
            if isinstance(unlock, dict) and unlock.get("potentialEffectId"):
                potential_owner[str(unlock["potentialEffectId"]).lower()] = char_id

    # Talents and potentials are table-driven. Only direct attachment links are
    # evidence. Blackboard changes to an active skill are not classified as a
    # passive source because that would be an inference rather than ownership.
    passive_effects = reader.load_table("PotentialTalentEffectTable.json")
    for effect_id, value in passive_effects.items():
        effect_key = str(effect_id).lower()
        char_id = talent_owner.get(effect_key) or potential_owner.get(effect_key)
        if not char_id or not isinstance(value, dict):
            continue
        for effect in value.get("dataList", []):
            if not isinstance(effect, dict):
                continue
            buff_id = str(effect.get("attachBuff", {}).get("buffId", "")).lower()
            skill_id = str(effect.get("attachSkill", {}).get("skillId", "")).lower()
            if buff_id:
                roots.add(("characterPassive", char_id, effect_key, buff_id))
            if skill_id:
                roots.add(("characterPassive", char_id, effect_key, skill_id))

    for weapon_id, value in reader.load_table("WeaponBasicTable.json").items():
        if not isinstance(value, dict):
            continue
        skill_ids: List[Any] = []
        for key in ("weaponPotentialSkill", "weaponSkillList"):
            item = value.get(key, [])
            skill_ids.extend(item if isinstance(item, list) else [item])
        for skill_id in skill_ids:
            if skill_id:
                root = str(skill_id).lower()
                roots.add(("weapon", weapon_id, root, root))

    for suit_id, value in reader.load_table("EquipSuitTable.json").items():
        if not isinstance(value, dict):
            continue
        for bonus in value.get("list", []):
            if not isinstance(bonus, dict) or not bonus.get("skillID"):
                continue
            root = str(bonus["skillID"]).lower()
            roots.add(("equipSuit", suit_id, root, root))

    # Direct SkillData -> BuffData references. At runtime the recorder knows
    # which origin skill created a Buff; the static config confirming that the
    # skill's own definition references the Buff makes the pair exact evidence
    # without any ownership-candidate table.
    skill_buff_edges: Dict[str, Set[str]] = {}
    for skill_id in skill_files:
        edges = {item for item in graph.get(skill_id, set()) if item in buff_files}
        if edges:
            skill_buff_edges[skill_id] = edges

    result: Dict[str, Set[Tuple[str, str, str, str]]] = collections.defaultdict(set)
    missing_roots: Set[str] = set()
    for kind, template_id, source_skill_id, root in sorted(roots):
        if root not in graph:
            missing_roots.add(root)
            continue
        # A SkillData string naming another SkillData can be a comparison, UI
        # reference, or orchestration link, so it is not safe evidence that all
        # of the other skill's Buffs share ownership. Start only from BuffData
        # directly referenced by the authoritative root, then follow Buff-to-
        # Buff links. A direct passive attachBuff is itself an accepted root.
        initial_buffs = ({root} if root in buff_files else {
            item for item in graph[root] if item in buff_files
        })
        queue = list(initial_buffs)
        visited = set(initial_buffs)
        for node in queue:
            for referenced in graph.get(node, set()):
                if referenced in buff_files and referenced not in visited:
                    visited.add(referenced)
                    queue.append(referenced)
        trigger_skill_id = root if root in skill_files else ""
        for node in visited:
            if node in buff_files:
                result[node].add(
                    (kind, template_id, source_skill_id, trigger_skill_id)
                )

    stats = {
        "schemaVersion": 2,
        "sourceSha256": source_hash.hexdigest(),
        "skillConfigs": len(skill_files),
        "buffConfigs": len(buff_files),
        "referenceEdges": sum(len(items) for items in graph.values()),
        "sourceRoots": len(roots),
        "missingRoots": len(missing_roots),
        "mappedBuffs": len(result),
        "candidateRows": sum(len(items) for items in result.values()),
        "ambiguousBuffs": sum(len(items) > 1 for items in result.values()),
        "skillBuffEdges": sum(len(items) for items in skill_buff_edges.values()),
    }
    return dict(sorted(result.items())), stats, dict(sorted(skill_buff_edges.items()))


def write_buff_source_map(
    output: Path,
    sources: Dict[str, Set[Tuple[str, str, str, str]]],
    stats: Dict[str, Any],
    skill_buff_edges: Dict[str, Set[str]],
) -> None:
    output.parent.mkdir(parents=True, exist_ok=True)
    with open(output, "w", encoding="utf-8", newline="\n") as handle:
        handle.write("BESOURCE\t2\n")
        handle.write(f"M\tsourceSha256\t{stats['sourceSha256']}\n")
        for key in ("skillConfigs", "buffConfigs", "referenceEdges",
                    "sourceRoots", "missingRoots", "mappedBuffs",
                    "candidateRows", "ambiguousBuffs", "skillBuffEdges"):
            handle.write(f"M\t{key}\t{stats[key]}\n")
        for buff_id, candidates in sources.items():
            for kind, template_id, source_skill_id, trigger_skill_id in sorted(candidates):
                handle.write(
                    "C\t" + "\t".join((buff_id, kind, template_id,
                                           source_skill_id, trigger_skill_id)) + "\n"
                )
        for skill_id, buff_ids in skill_buff_edges.items():
            for buff_id in sorted(buff_ids):
                handle.write(f"S\t{skill_id}\t{buff_id}\n")


def compute_incremental_diff(previous: Dict[str, Any], current: Dict[str, Any]) -> Dict[str, Any]:
    diff: Dict[str, Any] = {
        "timestamp": datetime.now(timezone.utc).isoformat(),
        "summary": {},
        "categories": {},
    }
    categories = ["characters", "skills", "talents", "weapons", "equipment", "suits", "dungeons", "dungeonSeries", "buffs"]
    total_added = 0
    total_modified = 0
    total_removed = 0

    for cat in categories:
        prev_cat = previous.get(cat, {})
        curr_cat = current.get(cat, {})
        prev_keys = set(prev_cat.keys())
        curr_keys = set(curr_cat.keys())

        added_keys = sorted(curr_keys - prev_keys)
        removed_keys = sorted(prev_keys - curr_keys)
        modified_keys = []

        for key in curr_keys & prev_keys:
            if curr_cat[key] != prev_cat[key]:
                modified_keys.append(key)
        modified_keys.sort()

        diff["categories"][cat] = {
            "addedCount": len(added_keys),
            "modifiedCount": len(modified_keys),
            "removedCount": len(removed_keys),
            "added": [k for k in added_keys[:50]],
            "modified": [k for k in modified_keys[:50]],
            "removed": removed_keys,
        }
        total_added += len(added_keys)
        total_modified += len(modified_keys)
        total_removed += len(removed_keys)

    diff["summary"] = {
        "totalAdded": total_added,
        "totalModified": total_modified,
        "totalRemoved": total_removed,
        "hasChanges": (total_added + total_modified + total_removed) > 0,
    }
    return diff


def create_minified_web_dict(full_dict: Dict[str, Any]) -> Dict[str, Any]:
    """Extract the compact records required by the schema-8 web analyzer."""
    min_chars = {
        k: {
            "n": v.get("name", k), "r": v.get("rarity", 0),
            "p": v.get("profession", 0), "wt": v.get("weaponType", 0),
            "i": f"icons/characters/{k}.png",
        }
        for k, v in full_dict.get("characters", {}).items()
    }
    min_skills = {
        k: {
            "n": v.get("name", k), "c": v.get("category", "other"),
            "i": v.get("iconId", ""), "ids": v.get("skillIds", []),
            "ch": v.get("characterId", ""),
        }
        for k, v in full_dict.get("skills", {}).items()
    }
    min_talents = {k: v["name"] for k, v in full_dict.get("talents", {}).items() if "name" in v}
    min_weapons = {
        k: {
            "n": v.get("name", k), "r": v.get("rarity", 0),
            "t": v.get("weaponType", 0), "i": v.get("iconId", k),
            "sid": v.get("potentialSkillId", ""),
            "sn": v.get("potentialSkillName", ""),
        }
        for k, v in full_dict.get("weapons", {}).items()
    }
    min_equipment = {
        k: {
            "n": v.get("name", k), "r": v.get("rarity", 0),
            "i": v.get("iconId", k), "p": v.get("partType", 0),
            "su": v.get("suitId", ""), "lv": v.get("minWearLevel", 0),
            "m": v.get("modifiers", []),
        }
        for k, v in full_dict.get("equipment", {}).items()
    }
    min_suits = {
        k: {
            "n": v.get("name", k), "i": v.get("logoName", ""),
            "sid": v.get("skillId", ""), "sn": v.get("skillName", ""),
            "b": v.get("bonuses", []),
        }
        for k, v in full_dict.get("suits", {}).items()
    }
    min_dungeons = {
        k: {
            "n": v.get("name", k), "sn": v.get("subName", ""),
            "s": v.get("seriesId", ""), "m": v.get("modeId", ""),
        }
        for k, v in full_dict.get("dungeons", {}).items()
    }
    min_series = full_dict.get("dungeonSeries", {})

    return {
        "schemaVersion": full_dict.get("metadata", {}).get("schemaVersion", 1),
        "exportedAt": full_dict.get("metadata", {}).get("exportedAt", ""),
        "c": min_chars,
        "sk": min_skills,
        "t": min_talents,
        "w": min_weapons,
        "eq": min_equipment,
        "su": min_suits,
        "d": min_dungeons,
        "ds": min_series,
        "zones": [
            "直伤", "攻击力", "增伤", "增幅", "脆弱",
            "承伤易伤", "减防/减抗", "法术强度", "其他"
        ],
    }


def export_web_icons(
    full_dict: Dict[str, Any],
    icon_source: Optional[Path],
    avatar_source: Path,
    output_dir: Path,
) -> Dict[str, Any]:
    """Copy only dictionary-referenced PNGs into the static web bundle.

    The icon source is the PNG tree produced for CombatDataExporter. Files may
    retain their game folders or be flattened; matching is done by icon stem.
    """
    try:
        from PIL import Image
    except ImportError as error:
        raise RuntimeError("Pillow is required for web icon conversion: pip install Pillow") from error

    def write_web_png(source: Path, destination: Path, max_edge: int) -> None:
        destination.parent.mkdir(parents=True, exist_ok=True)
        with Image.open(source) as original:
            image = original.convert("RGBA")
            image.thumbnail((max_edge, max_edge), Image.Resampling.LANCZOS)
            image.save(destination, format="PNG", optimize=True, compress_level=9)

    groups: Dict[str, Set[str]] = {
        "skills": {
            str(item.get("iconId", ""))
            for item in full_dict.get("skills", {}).values()
            if item.get("iconId")
        },
        "items": {
            str(item.get("iconId", ""))
            for category in ("weapons", "equipment")
            for item in full_dict.get(category, {}).values()
            if item.get("iconId")
        },
        "suits": {
            str(item.get("logoName", ""))
            for item in full_dict.get("suits", {}).values()
            if item.get("logoName")
        },
    }
    index: Dict[str, Path] = {}
    if icon_source and icon_source.exists():
        for path in icon_source.rglob("*.png"):
            index.setdefault(path.stem.lower(), path)

    copied: Dict[str, List[str]] = {name: [] for name in (*groups.keys(), "characters")}
    missing: Dict[str, List[str]] = {name: [] for name in groups}
    for group, icon_ids in groups.items():
        destination = output_dir / group
        destination.mkdir(parents=True, exist_ok=True)
        expected_names = {f"{icon_id}.png".lower() for icon_id in icon_ids}
        for stale in destination.glob("*.png"):
            if stale.name.lower() not in expected_names:
                stale.unlink()
        for icon_id in sorted(icon_ids):
            source = index.get(Path(icon_id).stem.lower())
            if source:
                write_web_png(source, destination / f"{icon_id}.png", 128)
                copied[group].append(icon_id)
            else:
                missing[group].append(icon_id)

    char_destination = output_dir / "characters"
    char_destination.mkdir(parents=True, exist_ok=True)
    expected_char_names = {
        f"{char_id}.png".lower() for char_id in full_dict.get("characters", {})
    }
    for stale in char_destination.glob("*.png"):
        if stale.name.lower() not in expected_char_names:
            stale.unlink()
    for char_id in sorted(full_dict.get("characters", {})):
        source = avatar_source / f"{char_id}.png"
        if source.exists():
            write_web_png(source, char_destination / source.name, 128)
            copied["characters"].append(char_id)

    manifest = {
        "generatedAt": datetime.now(timezone.utc).isoformat(),
        "source": str(icon_source.resolve()) if icon_source and icon_source.exists() else "",
        "copied": {key: len(value) for key, value in copied.items()},
        "missing": missing,
    }
    output_dir.mkdir(parents=True, exist_ok=True)
    with open(output_dir / "icon-manifest.json", "w", encoding="utf-8") as handle:
        json.dump(manifest, handle, ensure_ascii=False, indent=2)
    print("[+] Web icons: " + ", ".join(f"{key}={len(value)}" for key, value in copied.items()))
    missing_count = sum(len(value) for value in missing.values())
    if missing_count:
        print(f"[!] {missing_count} referenced icons are not present in --icon-source")
    return manifest


def default_vfs_path() -> Optional[Path]:
    configured = os.environ.get("ENDFIELD_VFS")
    candidates = [
        Path(configured) if configured else None,
        Path("E:/Endfield Game/Endfield_Data/StreamingAssets"),
    ]
    for candidate in candidates:
        if candidate and candidate.exists():
            return candidate
    return None


def default_game_path() -> Optional[Path]:
    configured = os.environ.get("ENDFIELD_GAME_PATH")
    candidates = [
        Path(configured) if configured else None,
        Path("E:/Endfield Game"),
    ]
    for candidate in candidates:
        if candidate and (candidate / "Endfield_Data/StreamingAssets/VFS").exists():
            return candidate
    return None


def refresh_game_tables(
    game_path: Path,
    extractor_path: Path,
    table_dir: Path,
    threads: int,
) -> Dict[str, Any]:
    """Atomically refresh Table JSON from the live base + hot-update VFS."""
    base_root = game_path / "Endfield_Data/StreamingAssets"
    persistent_root = game_path / "Endfield_Data/Persistent"
    if not (base_root / "VFS").is_dir():
        raise FileNotFoundError(f"Base game VFS not found: {base_root / 'VFS'}")
    if not extractor_path.is_file():
        raise FileNotFoundError(f"Endfield table extractor not found: {extractor_path}")

    table_dir.parent.mkdir(parents=True, exist_ok=True)
    staging_root = Path(tempfile.mkdtemp(
        prefix=".combat-table-refresh-", dir=str(table_dir.parent)
    ))
    extraction_root = staging_root / "extracted"
    try:
        command = [
            str(extractor_path), "dump",
            "--vfs", str(base_root),
            "--out", str(extraction_root),
            "--block", "Table",
            "--threads", str(max(1, threads)),
        ]
        source_kind = "base"
        if (persistent_root / "VFS").is_dir():
            command[2:4] = ["--vfs", str(persistent_root)]
            command[4:4] = ["--base-vfs", str(base_root)]
            source_kind = "persistent+base"
        print(f"[*] Refreshing game tables from {source_kind} VFS")
        subprocess.run(command, check=True)

        staged_table = extraction_root / "Table"
        required = [
            "CharacterTable.json", "CharGrowthTable.json",
            "I18nTextTable_CN.json", "ItemTable.json",
            "WeaponBasicTable.json", "EquipTable.json",
            "EquipSuitTable.json", "DungeonTable.json",
        ]
        missing = [name for name in required if not (staged_table / name).is_file()]
        if missing:
            raise RuntimeError("Refreshed Table block is incomplete: " + ", ".join(missing))

        backup = table_dir.parent / f".{table_dir.name}.backup-{staging_root.name}"
        replaced_existing = False
        try:
            if table_dir.exists():
                os.replace(table_dir, backup)
                replaced_existing = True
            os.replace(staged_table, table_dir)
        except Exception:
            if replaced_existing and backup.exists() and not table_dir.exists():
                os.replace(backup, table_dir)
            raise
        if backup.exists():
            shutil.rmtree(backup)

        source = {
            "kind": source_kind,
            "refreshedAt": datetime.now(timezone.utc).isoformat(),
            "gamePath": str(game_path.resolve()),
            "baseVfs": str(base_root.resolve()),
            "persistentVfs": str(persistent_root.resolve())
                if (persistent_root / "VFS").is_dir() else "",
            "tableCount": len(list(table_dir.glob("*.json"))),
            "requiredSha256": {
                name: sha256_file(table_dir / name) for name in required
            },
        }
        with open(table_dir.parent / "source-metadata.json", "w", encoding="utf-8") as f:
            json.dump(source, f, ensure_ascii=False, indent=2)
        return source
    finally:
        if staging_root.exists():
            shutil.rmtree(staging_root, ignore_errors=True)


def refresh_game_json_data(
    game_path: Path,
    extractor_path: Path,
    json_data_dir: Path,
    threads: int,
) -> None:
    """Atomically refresh JsonData used to build the exact Buff source graph."""
    base_root = game_path / "Endfield_Data/StreamingAssets"
    persistent_root = game_path / "Endfield_Data/Persistent"
    if not (base_root / "VFS").is_dir():
        raise FileNotFoundError(f"Base game VFS not found: {base_root / 'VFS'}")
    if not extractor_path.is_file():
        raise FileNotFoundError(f"Endfield JsonData extractor not found: {extractor_path}")

    cache_root = json_data_dir.parent.parent
    cache_root.mkdir(parents=True, exist_ok=True)
    staging_root = Path(tempfile.mkdtemp(
        prefix=".combat-jsondata-refresh-", dir=str(cache_root.parent)
    ))
    extraction_root = staging_root / "extracted"
    try:
        command = [
            str(extractor_path), "dump",
            "--vfs", str(base_root),
            "--out", str(extraction_root),
            "--block", "JsonData",
            "--threads", str(max(1, threads)),
        ]
        source_kind = "base"
        if (persistent_root / "VFS").is_dir():
            command[2:4] = ["--vfs", str(persistent_root)]
            command[4:4] = ["--base-vfs", str(base_root)]
            source_kind = "persistent+base"
        print(f"[*] Refreshing combat JsonData from {source_kind} VFS")
        subprocess.run(command, check=True)
        staged_json = extraction_root / "Data/Json"
        required = [staged_json / "SkillData", staged_json / "BuffData"]
        if not all(path.is_dir() for path in required):
            raise RuntimeError("Refreshed JsonData lacks SkillData or BuffData")

        backup = cache_root / ".Json.backup"
        if backup.exists():
            shutil.rmtree(backup)
        replaced_existing = False
        try:
            if json_data_dir.exists():
                os.replace(json_data_dir, backup)
                replaced_existing = True
            json_data_dir.parent.mkdir(parents=True, exist_ok=True)
            os.replace(staged_json, json_data_dir)
        except Exception:
            if replaced_existing and backup.exists() and not json_data_dir.exists():
                os.replace(backup, json_data_dir)
            raise
        if backup.exists():
            shutil.rmtree(backup)
    finally:
        if staging_root.exists():
            shutil.rmtree(staging_root, ignore_errors=True)


def load_table_source_metadata(table_dir: Path) -> Dict[str, Any]:
    path = table_dir.parent / "source-metadata.json"
    if not path.is_file():
        return {"kind": "offline-snapshot", "tablePath": str(table_dir.resolve())}
    try:
        with open(path, "r", encoding="utf-8") as f:
            value = json.load(f)
        return value if isinstance(value, dict) else {}
    except (OSError, json.JSONDecodeError):
        return {"kind": "unknown", "tablePath": str(table_dir.resolve())}


def extract_icon_pngs(
    full_dict: Dict[str, Any],
    vfs_path: Path,
    base_vfs_path: Optional[Path],
    extractor_path: Path,
    cache_dir: Path,
    threads: int,
) -> Path:
    """Extract the narrow icon families referenced by the combat dictionary.

    A compact family regex avoids Windows command-line length limits. The later
    copy step still whitelists exact dictionary IDs, so unrelated extracted PNGs
    never enter the website bundle.
    """
    if not vfs_path.exists():
        raise FileNotFoundError(f"Endfield StreamingAssets not found: {vfs_path}")
    if not extractor_path.exists():
        raise FileNotFoundError(f"Endfield image extractor not found: {extractor_path}")

    referenced = {
        str(item.get("iconId", ""))
        for category in ("skills", "weapons", "equipment")
        for item in full_dict.get(category, {}).values()
        if item.get("iconId")
    }
    referenced.update(
        str(item.get("logoName", ""))
        for item in full_dict.get("suits", {}).values()
        if item.get("logoName")
    )
    if not referenced:
        return cache_dir

    cache_dir.mkdir(parents=True, exist_ok=True)
    cached_stems = {path.stem.lower() for path in cache_dir.rglob("*.png")}
    missing_from_cache = {icon_id for icon_id in referenced if icon_id.lower() not in cached_stems}
    if not missing_from_cache:
        print(f"[*] Icon cache is complete ({len(referenced)} referenced PNGs); extraction skipped")
        return cache_dir
    print(f"[*] Icon cache needs {len(missing_from_cache)} of {len(referenced)} referenced PNGs")
    family_regex = (
        r"^(?:icon_combo_skill_|icon_skill_|icon_ultimate_skill_|icon_attack_"
        r"|item_equip_|wpn_|icon_pack_).*$"
    )
    command = [
        str(extractor_path),
        "extract",
        "--vfs", str(vfs_path),
        "--out", str(cache_dir),
        "--asset-name", family_regex,
        "--types", "Texture2D",
        "--threads", str(max(1, threads)),
        "--format", "png",
        "--png-compression", "fast",
        "--max-memory-gb", "16",
        "--exclude-material",
        "--skip-missing",
    ]
    if base_vfs_path:
        command[4:4] = ["--base-vfs", str(base_vfs_path)]
    print(f"[*] Extracting referenced icon families from: {vfs_path.resolve()}")
    print(f"[*] Icon cache: {cache_dir.resolve()}")
    subprocess.run(command, check=True)
    return cache_dir


def main():
    parser = argparse.ArgumentParser(
        description="BetterEndfield Combat Data Exporter & Incremental Sync Tool"
    )
    parser.add_argument(
        "--table-dir",
        type=Path,
        default=Path("research/table-dump/Table"),
        help="Path to dumped game tables directory",
    )
    parser.add_argument(
        "--game-path",
        type=Path,
        default=default_game_path(),
        help="Current Endfield installation root (or set ENDFIELD_GAME_PATH)",
    )
    parser.add_argument(
        "--refresh-tables",
        action=argparse.BooleanOptionalAction,
        default=True,
        help="Refresh Table JSON from current base + hot-update VFS before export",
    )
    parser.add_argument(
        "--table-extractor",
        type=Path,
        default=Path("tools/EndfieldStudio/AnimeStudio.Endfield.Cli/bin/Release/net9.0/endfield-dump.exe"),
        help="EndfieldStudio CLI with dump --base-vfs support",
    )
    parser.add_argument(
        "--table-threads",
        type=int,
        default=min(os.cpu_count() or 1, 16),
        help="Table extraction worker count",
    )
    parser.add_argument(
        "--besem",
        type=Path,
        default=Path("manifests/combat/combat-semantics.besem"),
        help="Path to combat-semantics.besem file",
    )
    parser.add_argument(
        "--json-data-dir",
        type=Path,
        default=Path("research/combat-jsondata/Data/Json"),
        help="Dumped JsonData/Data/Json containing SkillData and BuffData",
    )
    parser.add_argument(
        "--refresh-json-data",
        action=argparse.BooleanOptionalAction,
        default=True,
        help="Refresh SkillData/BuffData from current base + hot-update VFS",
    )
    parser.add_argument(
        "--buff-source-output",
        type=Path,
        default=Path("manifests/combat/buff-sources.bemap"),
        help="Exact Buff ownership reverse index used by the native recorder",
    )
    parser.add_argument(
        "--base",
        type=Path,
        default=None,
        help="Previous dictionary JSON file for incremental diff comparison",
    )
    parser.add_argument(
        "--output",
        type=Path,
        default=Path("manifests/combat/combat-dictionary.json"),
        help="Output full JSON dictionary path",
    )
    parser.add_argument(
        "--ui-output",
        type=Path,
        default=Path("ui/BetterEndfield.UI/Assets/combat/combat-dictionary.json"),
        help="Output desktop UI embedded JSON dictionary path",
    )
    parser.add_argument(
        "--min-output",
        type=Path,
        default=Path("web/src/data/combat-dict.min.json"),
        help="Output minified JSON dictionary path for web/mobile",
    )
    parser.add_argument(
        "--icon-source",
        type=Path,
        default=Path(os.environ["ENDFIELD_ICON_SOURCE"]) if os.environ.get("ENDFIELD_ICON_SOURCE") else None,
        help="PNG asset tree used by this exporter for skill/item/suit icons",
    )
    parser.add_argument(
        "--export-icons",
        action="store_true",
        help="Extract referenced PNGs directly from Endfield StreamingAssets",
    )
    parser.add_argument(
        "--vfs",
        type=Path,
        default=default_vfs_path(),
        help="Endfield_Data/StreamingAssets path (or set ENDFIELD_VFS)",
    )
    parser.add_argument(
        "--image-extractor",
        type=Path,
        default=Path("tools/EndfieldStudio/AnimeStudio.Endfield.Cli/bin/Release/net9.0/endfield-dump.exe"),
        help="Existing Endfield texture extraction CLI",
    )
    parser.add_argument(
        "--icon-cache",
        type=Path,
        default=Path("tools/CombatDataExporter/.icon-cache"),
        help="Persistent cache for extracted source PNGs",
    )
    parser.add_argument(
        "--icon-threads",
        type=int,
        default=min(os.cpu_count() or 1, 16),
        help="Texture extraction worker count",
    )
    parser.add_argument(
        "--avatar-source",
        type=Path,
        default=Path("native/modules/combat_stats/assets/avatars"),
        help="Existing combat character avatar directory",
    )
    parser.add_argument(
        "--icon-output",
        type=Path,
        default=Path("web/public/icons"),
        help="Static web icon output directory",
    )
    parser.add_argument(
        "--verify-icons",
        action="store_true",
        help="Fail if any referenced skill/item/suit PNG is missing",
    )
    parser.add_argument(
        "--report",
        type=Path,
        default=None,
        help="Optional path to output incremental diff report",
    )
    parser.add_argument(
        "--verify",
        action="store_true",
        help="Run integrity validation on the generated dictionary",
    )
    args = parser.parse_args()

    print("==========================================================")
    print("BetterEndfield Combat Data Exporter")
    print("==========================================================")
    if args.refresh_tables:
        if not args.game_path:
            print("[x] Error: current game installation not found; pass --game-path or use --no-refresh-tables")
            sys.exit(1)
        try:
            refresh_game_tables(
                args.game_path, args.table_extractor, args.table_dir,
                args.table_threads,
            )
        except (OSError, RuntimeError, subprocess.CalledProcessError) as error:
            print(f"[x] Error: table refresh failed: {error}")
            sys.exit(1)
    else:
        print("[!] Using offline Table snapshot (--no-refresh-tables)")

    if args.refresh_json_data:
        if not args.game_path:
            print("[x] Error: current game installation not found; pass --game-path or use --no-refresh-json-data")
            sys.exit(1)
        try:
            refresh_game_json_data(
                args.game_path, args.table_extractor, args.json_data_dir,
                args.table_threads,
            )
        except (OSError, RuntimeError, subprocess.CalledProcessError) as error:
            print(f"[x] Error: JsonData refresh failed: {error}")
            sys.exit(1)
    else:
        print("[!] Using offline combat JsonData snapshot (--no-refresh-json-data)")

    print(f"[*] Game Table Directory: {args.table_dir.resolve()}")

    if not args.table_dir.exists():
        print(f"[x] Error: Table directory {args.table_dir} does not exist.")
        sys.exit(1)

    reader = GameTableReader(args.table_dir)
    chars = extract_characters(reader)
    skills, talents = extract_skills_and_talents(reader)
    weapons = extract_weapons(reader)
    suits = extract_suits(reader)
    equipment = extract_equipment(reader)
    dungeons, series = extract_dungeons_and_series(reader)
    buffs = extract_buff_semantics(args.besem)
    try:
        buff_sources, buff_source_stats, skill_buff_edges = build_buff_source_index(
            reader, args.json_data_dir
        )
        write_buff_source_map(
            args.buff_source_output, buff_sources, buff_source_stats,
            skill_buff_edges
        )
        print(
            f"[+] Buff source map saved to: {args.buff_source_output} "
            f"({buff_source_stats['mappedBuffs']} buffs, "
            f"{buff_source_stats['candidateRows']} exact candidates, "
            f"{buff_source_stats['skillBuffEdges']} skill-buff edges)"
        )
    except (OSError, ValueError) as error:
        print(f"[x] Error: Buff source map generation failed: {error}")
        sys.exit(1)

    now_utc = datetime.now(timezone.utc).isoformat()
    metadata = {
        "schemaVersion": 1,
        "generator": "CombatDataExporter v1.0",
        "exportedAt": now_utc,
        "source": load_table_source_metadata(args.table_dir),
        "counts": {
            "characters": len(chars),
            "skills": len(skills),
            "talents": len(talents),
            "weapons": len(weapons),
            "suits": len(suits),
            "equipment": len(equipment),
            "dungeons": len(dungeons),
            "dungeonSeries": len(series),
            "buffs": len(buffs),
            "buffSourceMappings": buff_source_stats["mappedBuffs"],
            "buffSourceCandidates": buff_source_stats["candidateRows"],
        },
        "buffSourceIndex": buff_source_stats,
    }

    full_dict = {
        "metadata": metadata,
        "characters": chars,
        "skills": skills,
        "talents": talents,
        "weapons": weapons,
        "suits": suits,
        "equipment": equipment,
        "dungeons": dungeons,
        "dungeonSeries": series,
        "buffs": buffs,
    }

    # Incremental diff against previous dictionary only if report path is explicitly specified
    if args.report:
        base_file = args.base if args.base else (args.output if args.output.exists() else None)
        if base_file and base_file.exists():
            try:
                with open(base_file, "r", encoding="utf-8") as f:
                    prev_data = json.load(f)
                diff = compute_incremental_diff(prev_data, full_dict)
                args.report.parent.mkdir(parents=True, exist_ok=True)
                with open(args.report, "w", encoding="utf-8") as f:
                    json.dump(diff, f, ensure_ascii=False, indent=2)
                print(f"[+] Incremental Report saved to: {args.report}")
                print(f"    Added: {diff['summary']['totalAdded']}, Modified: {diff['summary']['totalModified']}, Removed: {diff['summary']['totalRemoved']}")
            except Exception as e:
                print(f"[!] Warning: Failed to compute incremental diff: {e}")

    # Write full JSON
    args.output.parent.mkdir(parents=True, exist_ok=True)
    with open(args.output, "w", encoding="utf-8") as f:
        json.dump(full_dict, f, ensure_ascii=False, indent=2)
    print(f"[+] Full Dictionary saved to: {args.output} ({args.output.stat().st_size / 1024:.1f} KB)")

    # Write UI Embedded JSON
    if args.ui_output:
        args.ui_output.parent.mkdir(parents=True, exist_ok=True)
        with open(args.ui_output, "w", encoding="utf-8") as f:
            json.dump(full_dict, f, ensure_ascii=False, indent=2)
        print(f"[+] UI Embedded Dictionary saved to: {args.ui_output} ({args.ui_output.stat().st_size / 1024:.1f} KB)")

    # Write minified JSON
    args.min_output.parent.mkdir(parents=True, exist_ok=True)
    min_dict = create_minified_web_dict(full_dict)
    with open(args.min_output, "w", encoding="utf-8") as f:
        json.dump(min_dict, f, ensure_ascii=False, separators=(",", ":"))
    print(f"[+] Minified Web Dictionary saved to: {args.min_output} ({args.min_output.stat().st_size / 1024:.1f} KB)")

    icon_source = args.icon_source
    if icon_source is None and args.icon_cache.exists():
        icon_source = args.icon_cache
    if args.export_icons:
        if not args.vfs:
            print("[x] Error: Endfield StreamingAssets not found; pass --vfs or set ENDFIELD_VFS")
            sys.exit(1)
        try:
            icon_vfs = args.vfs
            icon_base_vfs = None
            if args.game_path:
                base_root = args.game_path / "Endfield_Data/StreamingAssets"
                persistent_root = args.game_path / "Endfield_Data/Persistent"
                if (persistent_root / "VFS").is_dir():
                    icon_vfs = persistent_root
                    icon_base_vfs = base_root
            icon_source = extract_icon_pngs(
                full_dict,
                icon_vfs,
                icon_base_vfs,
                args.image_extractor,
                args.icon_cache,
                args.icon_threads,
            )
        except (OSError, subprocess.CalledProcessError) as error:
            print(f"[x] Error: icon extraction failed: {error}")
            sys.exit(1)
    icon_manifest = export_web_icons(
        full_dict, icon_source, args.avatar_source, args.icon_output
    )
    if args.verify_icons:
        missing_icons = sum(len(items) for items in icon_manifest["missing"].values())
        if missing_icons:
            print(f"[x] Error: {missing_icons} referenced icons could not be exported")
            sys.exit(1)

    if args.verify:
        print("[*] Verifying coverage...")
        assert len(chars) > 20, "Characters count too low"
        assert len(weapons) > 30, "Weapons count too low"
        assert len(dungeons) > 100, "Dungeons count too low"
        print("[OK] Verification passed successfully!")

    print("==========================================================")
    print("Export complete!")


if __name__ == "__main__":
    main()
