// Hand-written Chinese for the things the client ships no text for.
//
// The client has no buff names at all -- BuffIconConfig carries a sprite path
// and nothing else, and BuffTable.json is empty -- so a status can never be
// looked up by name. What the exported buff table does carry is what each buff
// *does*: which attribute it moves, in which slot of the attribute formula, and
// which damage zone it writes. That is machine-readable, so most of the display
// text is generated rather than translated; only the vocabulary below is typed
// out by hand.
//
// The three enums come from Common.Beyond.dll (Beyond.GEnums) in the 1.4.4
// dump, in declaration order. Their ordinals are load-bearing: they are the
// same numbers the attribution ledger uses, and COMBAT_RUNTIME_CONTRACTS.md
// quotes them (2 Atk, 3 Def, 9/10 crit, 62 weakness, 63 shelter, 64-69
// enhanced, 70-75 vulnerable, 94-99 resistance). Do not renumber; on a game
// update, re-extract and append.

/** Beyond.GEnums.AttributeType. 80 of these 102 appear in the shipped tables. */
export const ATTRIBUTE_NAMES: readonly string[] = [
  "等级", "生命上限", "攻击力", "防御力",
  "物理承伤", "灼热承伤", "电磁承伤", "晶体承伤",
  "重量", "暴击率", "暴击伤害", "仇恨",
  "普攻范围", "移动速度", "转向速度", "攻击速度",
  "技能冷却", "普攻增伤", "每秒回复", "每秒回复（生命上限比例）",
  "韧性上限", "韧性恢复时间", "终结技能量上限", "连携技冷却",
  "韧性承伤", "物理侵蚀伤害", "韧性伤害", "破防承伤",
  "终结技增伤", "治疗量", "受治疗量", "韧性恢复速度",
  "战技增伤", "连携技增伤", "击倒时间",
  "灼热爆发增伤", "电磁爆发增伤", "晶体爆发增伤", "自然爆发增伤",
  "力量", "敏捷", "智慧", "意志",
  "生命汲取", "终结技能量获取", "ATB 消耗", "战技冷却", "连携技冷却系数",
  "自然承伤", "点燃伤害",
  "物理增伤", "灼热增伤", "电磁增伤", "晶体增伤", "自然增伤", "以太增伤",
  "灼热异常增伤", "电磁异常增伤", "晶体异常增伤", "自然异常增伤",
  "以太承伤", "破防目标增伤", "弱点伤害", "庇护减伤",
  "物理增幅", "灼热增幅", "电磁增幅", "晶体增幅", "自然增幅", "以太增幅",
  "物理脆弱", "灼热脆弱", "电磁脆弱", "晶体脆弱", "自然脆弱", "以太脆弱",
  "力量转攻击力", "敏捷转攻击力", "智慧转攻击力", "意志转攻击力",
  "物理抗性系数", "自然抗性系数", "晶体抗性系数", "电磁抗性系数",
  "灼热抗性系数", "以太抗性系数",
  "迟缓", "物理与法术侵蚀强化", "护盾量", "受护盾量",
  "普攻起手范围", "空中移动速度", "关键词加速", "连携技冷却回复",
  "物理抗性", "自然抗性", "晶体抗性", "电磁抗性", "灼热抗性", "以太抗性",
  "连携技冷却缩减", "",
];

/**
 * Beyond.GEnums.ModifierType, i.e. which slot of the attribute formula the
 * value lands in. base = (raw + [4]) x (1 + [5]) + [6]) x [7];
 * final = ((base + [0]) x (1 + [1]) + [2]) x [3].
 */
export const MODIFIER_SLOT_NAMES: readonly string[] = [
  "加算", "乘算", "最终加算", "最终乘算",
  "基础加算", "基础乘算", "基础最终加算", "基础最终乘算",
  "", "",
];

/** Slots whose value is a ratio no matter which attribute they apply to. */
const RATIO_SLOTS = new Set([1, 3, 5, 7]);

/**
 * Attributes measured as a ratio. Everything named *Scalar / *Rate /
 * *Increase / *Resistance is one; Atk, Def, MaxHp and the four stats are flat.
 */
const RATIO_ATTRIBUTES = new Set([
  4, 5, 6, 7, 9, 10, 13, 14, 15, 16, 17, 19, 24, 25, 26, 27, 28, 29, 30, 31,
  32, 33, 35, 36, 37, 38, 43, 44, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
  58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76,
  77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 91, 92, 93, 94, 95, 96,
  97, 98, 99, 100,
]);

/**
 * DamageScaleProcessorConfig zone names. The client assigns several roles to
 * one zone, so these are the zone's own identity, not a damage category --
 * NormalCalcZone carries damageType/skillType/ignite/brokenUnit at once.
 */
export const ZONE_NAMES: Readonly<Record<string, string>> = {
  NormalCalcZone: "常规乘区",
  ProdCalcZone: "独立乘区",
  ComboCalcZone: "连携乘区",
  RaceCalcZone: "竞速乘区",
  EnergyTypeCalcZone: "能量类型乘区",
};

/**
 * What a buff id says about itself once the source has been stripped, e.g. the
 * `talent_0` of buff_chr_0004_pelica_talent_0.
 *
 * These are prefix rules, not a dictionary, because the tail space is a long
 * tail: 568 distinct tails across 683 character buffs, where the 30 most common
 * cover only 20%. What *is* regular is the leading segment -- it names the
 * mechanism, and an optional index follows it. Eleven rules cover 71% of
 * character buffs; everything past the mechanism (`_vfx`, `_listener`,
 * `_hitstop`) is engine detail.
 *
 * `utimate` is not a typo here: the game data has it.
 */
const MECHANISM_RULES: ReadonlyArray<readonly [RegExp, string]> = [
  [/^talent_?(\d*)/, "天赋"],
  [/^potential_?(\d*)/, "潜能"],
  [/^passive_?(\d*)/, "被动"],
  [/^(?:ultimate|utimate)_?skill_?(\d*)/, "终结技"],
  [/^ult_?(\d*)/, "终结技"],
  [/^normal_?skill_?(\d*)/, "战技"],
  [/^combo_?skill_?(\d*)/, "连携技"],
  [/^normal_?attack_?(\d*)/, "普攻"],
  [/^extra_?attack_?(\d*)/, "额外攻击"],
  [/^dodge_?(\d*)/, "闪避"],
  [/^attack(\d*)/, "普攻"],
  [/^showhide_?/, "显隐"],
];

/** Qualifiers allowed to follow a mechanism. Anything else aborts the rewrite. */
const SUFFIX_TERMS: Readonly<Record<string, string>> = {
  effect: "效果",
  effectbuff: "效果",
  buff: "增益",
  debuff: "减益",
  vfx: "特效",
  fx: "特效",
  cd: "冷却",
  marker: "标记",
  listener: "监听",
  atkup: "攻击提升",
  up: "提升",
  down: "降低",
  weak: "弱化",
  remain: "残留",
  stack: "叠加",
  add: "追加",
};

/** Whole-tail names for the shared engine states, which follow no pattern. */
const COMMON_TAILS: Readonly<Record<string, string>> = {
  born: "出生",
  dash: "冲刺",
  dash_immune: "冲刺免疫",
  dash_succeed_immune: "完美闪避免疫",
  damage_immune_medium: "伤害免疫",
  damage_immune_ult_skill: "终结技伤害免疫",
  full_immune_weak: "完全免疫",
  channeling_casting: "引导施法",
  combo_attack_fx: "连携攻击特效",
  char_wallhack: "角色穿模",
  eny_wallhack: "敌人穿模",
  maxhpup: "生命上限提升",
  obtain_ultimate_sp: "获得终结技能量",
  immobilized_interrupt_action: "定身打断",
  mutant_enemy_superarmor: "变异敌人霸体",
  lower_superarmor_when_interrupted: "被打断时降低霸体",
  enemy_patrolregen_delay: "巡逻回复延迟",
  enemy_poiseresist_normal: "敌人韧性抗性",
  atkup: "攻击提升",
  atkup_owner: "攻击提升·自身",
  spellenhance: "法术增幅",
  poiseresist_normal: "韧性抗性",
};

export function attributeName(attributeType: number): string {
  return ATTRIBUTE_NAMES[attributeType] || `属性 ${attributeType}`;
}

export function zoneName(zone: string): string {
  return ZONE_NAMES[zone] || zone;
}

/**
 * Chinese for a buff id tail, or "" when nothing in the vocabulary matches, in
 * which case the caller keeps the raw id.
 *
 * Translating token by token was the first attempt and it was wrong twice over:
 * it produced `lower·霸体·when·interrupted`, and it mistranslated, because
 * `normal` is 普攻 in `normal_skill` but 普通 in `enemy_poiseresist_normal`.
 * Matching a mechanism prefix and leaving any unrecognised remainder intact
 * avoids both -- no token is guessed at, and no identifier is chopped up.
 */
export function localizeIdTail(tail: string): string {
  const cleaned = tail.replace(/^[_-]+/, "").replace(/[_-]+$/, "").toLowerCase();
  if (!cleaned) return "";
  const common = COMMON_TAILS[cleaned];
  if (common) return common;
  for (const [pattern, name] of MECHANISM_RULES) {
    const match = pattern.exec(cleaned);
    if (!match) continue;
    const label = match[1] ? `${name} ${match[1]}` : name;
    const rest = cleaned.slice(match[0].length).replace(/^[_-]+/, "");
    if (!rest) return label;
    const pieces = rest.split(/[_-]+/).filter(Boolean);
    // A trailing index belongs to the mechanism. Known qualifiers translate;
    // an unknown one is kept whole rather than chopped up, because it is an
    // engine identifier (`seal`, `precheck`, `abilityentity`) and guessing at
    // its parts is how `lower·霸体·when·interrupted` happened.
    const qualifiers: string[] = [];
    const unknown: string[] = [];
    for (const piece of pieces) {
      if (/^\d+$/.test(piece)) continue;
      const term = SUFFIX_TERMS[piece];
      if (term && !unknown.length) qualifiers.push(term);
      else unknown.push(piece);
    }
    const suffix = [qualifiers.join(""), unknown.join("_")].filter(Boolean).join("·");
    return suffix ? `${label}·${suffix}` : label;
  }
  return "";
}

function formatAmount(value: number, ratio: boolean): string {
  if (ratio) {
    const percent = Math.round(value * 1000) / 10;
    return `${percent > 0 ? "+" : ""}${percent}%`;
  }
  const rounded = Math.round(value * 100) / 100;
  return `${rounded > 0 ? "+" : ""}${rounded}`;
}

/**
 * How a slot's number reads. Slots 3 and 7 multiply the running total, so they
 * are written x1.25; slots 1 and 5 sit inside a (1 + v) and read +25%; the four
 * additive slots take their unit from the attribute.
 */
function formatSlotAmount(slot: number, attributeType: number, value: number): string {
  if (slot === 3 || slot === 7) {
    return `×${Math.round(value * 1000) / 1000}`;
  }
  return formatAmount(value, RATIO_SLOTS.has(slot) || RATIO_ATTRIBUTES.has(attributeType));
}

/** What a buff configures for one attribute slot, as the recorder measured it. */
export interface NominalEffectLike {
  attributeType: number;
  slot: number;
  min: number;
  max: number;
}

/**
 * "+25%", or "+18%~25%" when the buff scaled with something while it was live
 * -- layers, an affix roll, how many enemies the hit caught.
 */
export function nominalAmount(effect: NominalEffectLike): string {
  const low = formatSlotAmount(effect.slot, effect.attributeType, effect.min);
  if (Math.abs(effect.max - effect.min) <= 1e-6) return low;
  const high = formatSlotAmount(effect.slot, effect.attributeType, effect.max);
  // The sign and unit are already in `low`; repeating them reads as noise.
  return `${low}~${high.replace(/^[+×]/, "")}`;
}

/** The same with the attribute it moves: "自然增幅 +25%". */
export function describeNominalEffect(effect: NominalEffectLike): string {
  return `${attributeName(effect.attributeType)} ${nominalAmount(effect)}`;
}

/**
 * The buff's headline effect. Buffs that write several slots are led by the
 * largest one, because that is the number a player would quote.
 */
export function primaryNominalEffect<T extends NominalEffectLike>(
  effects: readonly T[] | undefined,
): T | undefined {
  if (!effects?.length) return undefined;
  return effects.reduce((best, effect) =>
    Math.abs(effect.max) > Math.abs(best.max) ? effect : best);
}

export function describeNominalEffects(
  effects: readonly NominalEffectLike[] | undefined, limit = 3,
): string {
  if (!effects?.length) return "";
  const ordered = [...effects].sort((a, b) => Math.abs(b.max) - Math.abs(a.max));
  const shown = ordered.slice(0, limit).map(describeNominalEffect);
  return ordered.length > limit
    ? `${shown.join("、")} 等 ${ordered.length} 项`
    : shown.join("、");
}

/** One attribute modifier row from the buff table: [mt, at, fi, param]. */
export type AttributeModifier = [number, number, number, number | { k: string }];

/**
 * "攻击力 +20%" instead of a name nobody has. A modifier whose value comes from
 * a blackboard key has no static number, so it degrades to the attribute alone
 * rather than inventing one.
 */
export function describeModifier(modifier: AttributeModifier): string {
  const [, attributeType, slot, param] = modifier;
  const name = attributeName(attributeType);
  if (typeof param !== "number") return name;
  return `${name} ${formatSlotAmount(slot, attributeType, param)}`;
}

/** Joins a buff's modifiers into one line, keeping it short enough for a track. */
export function describeModifiers(modifiers: readonly AttributeModifier[], limit = 3): string {
  if (!modifiers.length) return "";
  const shown = modifiers.slice(0, limit).map(describeModifier);
  return modifiers.length > limit
    ? `${shown.join("、")} 等 ${modifiers.length} 项`
    : shown.join("、");
}
