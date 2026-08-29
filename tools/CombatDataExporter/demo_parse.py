#!/usr/bin/env python3
# -*- coding: utf-8 -*-
import sys
import json
from pathlib import Path

if hasattr(sys.stdout, "reconfigure"):
    sys.stdout.reconfigure(encoding="utf-8")

dict_path = Path("manifests/combat/combat-dictionary.json")
if not dict_path.exists():
    print(f"Error: {dict_path} not found")
    sys.exit(1)

with open(dict_path, "r", encoding="utf-8") as f:
    d = json.load(f)

meta = d["metadata"]
print("=" * 66)
print("【BetterEndfield 战斗全量数据字典解析总览】")
print(f"• 导出时间: {meta['exportedAt']}")
print("• 抽取数据量统计:")
for k, v in meta["counts"].items():
    print(f"    - {k:15}: {v} 条")
print("=" * 66)

# 1. 角色与技能
print("\n【1. 干员与核心技能映射解析】")
for cid in ["chr_0004_pelica", "chr_0013_aglina", "chr_0002_endminm"]:
    c = d["characters"].get(cid, {})
    print(f"• [{cid}] => {c.get('name')} (稀有度: {c.get('rarity')}星, 职业类型: {c.get('profession')})")
    c_skills = [
        f"{sk_id}: {info['name']} [{info['category']}]"
        for sk_id, info in d["skills"].items()
        if info.get("characterId") == cid
    ]
    for sk in c_skills[:4]:
        print(f"    └─ {sk}")

# 2. 武器与专属被动
print("\n【2. 武器与专属技能/被动映射解析】")
for wid in ["wpn_claym_0004", "wpn_sword_0007", "wpn_funnel_0002", "wpn_claym_0006"]:
    w = d["weapons"].get(wid, {})
    pskill = w.get("potentialSkillName") or "通用被动"
    print(f"• [{wid}] => {w.get('name')} ({w.get('rarity')}星) | 专属被动: 【{pskill}】 ({w.get('potentialSkillId')})")

# 3. 套装效果
print("\n【3. 套装与3件套被动映射解析】")
for sid in ["suit_agi01", "suit_atk01", "suit_crit01", "suit_atb01"]:
    s = d["suits"].get(sid, {})
    print(f"• [{sid}] => {s.get('name')} | 3件套特效: {s.get('skillId')} | 图标: {s.get('logoName')}")

# 4. 副本关卡与系列
print("\n【4. 关卡与大活动系列映射解析】")
for did in ["dung01_bossrush01_01", "dung01_actmonster01", "indie_battletower006", "dung01_charbreak01"]:
    dung = d["dungeons"].get(did, {})
    print(f"• [{did}] => 关卡: 【{dung.get('name')}】 | 所属大活动系列: 【{dung.get('seriesName')}】")

# 5. 破韧与乘区语义
print("\n【5. 破韧与战斗乘区状态映射解析】")
for bid in ["buff_common_poise_break_damage_taken_scale", "buff_common_poise_can_be_breaking_attacked", "buff_common_mini_poise_break"]:
    b = d["buffs"].get(bid, {})
    print(f"• [{bid}] => 状态: {b.get('status')}, 乘区: {b.get('zone')}, 元素: {b.get('element')}")

print("\n" + "=" * 66)

# 6. 模拟解析一场完整的战斗记录 Session JSON
print("【6. 模拟实战 Session 记录完整反解析演示】")
sample_session = {
    "sessionId": 1724601234,
    "dungeonId": "dung01_bossrush01_01",
    "durationSeconds": 58.42,
    "squad": [
        {"charId": "chr_0004_pelica", "charInstId": 1001, "potential": 5, "skillGroupCount": 4, "equipSuitCount": 2},
        {"charId": "chr_0013_aglina", "charInstId": 1002, "potential": 3, "skillGroupCount": 4, "equipSuitCount": 2}
    ],
    "staggerIntervals": [
        {"targetId": "eny_0051_rodin", "start": 21.3, "end": 33.8, "buffId": "buff_common_poise_break_damage_taken_scale"}
    ]
}

d_info = d["dungeons"].get(sample_session["dungeonId"], {})
print(f"• 战斗关卡: 【{d_info.get('name', sample_session['dungeonId'])}】 (系列: {d_info.get('seriesName')})")
print(f"• 战斗时长: {sample_session['durationSeconds']} 秒")
print("• 出战队伍:")
for m in sample_session["squad"]:
    c_info = d["characters"].get(m["charId"], {})
    print(f"    - 干员: {c_info.get('name', m['charId'])} | 潜能: {m['potential']} 潜 | 技能组数: {m['skillGroupCount']}")
print("• Boss 虚弱/破韧时间段:")
for st in sample_session["staggerIntervals"]:
    b_info = d["buffs"].get(st["buffId"], {})
    duration = round(st["end"] - st["start"], 2)
    print(f"    - 目标: {st['targetId']} | 时间段: {st['start']}s ~ {st['end']}s (持续 {duration} 秒) | 状态: 破韧易伤 (乘区: {b_info.get('zone')})")
print("=" * 66)
