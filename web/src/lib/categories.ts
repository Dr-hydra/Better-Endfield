// Mirrors RdpsContributionKind in native/modules/combat_stats/module.cpp:
// one category per factor of the game's damage formula, attacker side first.
export interface ContributionCategory {
  id: string;
  name: string;
  short: string;
  color: string;
  side: "self" | "attacker" | "defender" | "other";
  hint: string;
}

export const CONTRIBUTION_CATEGORIES: readonly ContributionCategory[] = [
  { id: "direct", name: "直伤", short: "直伤", color: "#D3D8E1", side: "self", hint: "攻击者自身保留的伤害" },
  { id: "attack", name: "攻击力", short: "攻击", color: "#FFCE52", side: "attacker", hint: "ATK 属性加成" },
  { id: "damageIncrease", name: "增伤", short: "增伤", color: "#43C9FF", side: "attacker", hint: "伤害类型 / 技能类型 / 破韧增伤乘区" },
  { id: "abnormalIncrease", name: "异常增伤", short: "异常", color: "#FF6A3D", side: "attacker", hint: "异常爆发乘区与点燃系数" },
  { id: "amplification", name: "增幅", short: "增幅", color: "#C084FC", side: "attacker", hint: "增幅乘区" },
  { id: "critical", name: "暴击", short: "暴击", color: "#F06292", side: "attacker", hint: "暴击率（按期望）与暴击伤害" },
  { id: "independentZone", name: "独立乘区", short: "独立", color: "#2DD4BF", side: "attacker", hint: "不承载属性的乘区（乘法区 / 连携区 / 种族区）" },
  { id: "fragile", name: "脆弱", short: "脆弱", color: "#FF9148", side: "defender", hint: "脆弱乘区" },
  { id: "vulnerabilityTaken", name: "承伤", short: "承伤", color: "#A3E635", side: "defender", hint: "守方伤害类型乘区与承伤系数" },
  { id: "defense", name: "减防", short: "减防", color: "#57D99B", side: "defender", hint: "防御力削减" },
  { id: "resistance", name: "减抗", short: "减抗", color: "#3B82F6", side: "defender", hint: "元素抗性削减" },
  { id: "artsStrength", name: "法术强度", short: "法强", color: "#8B5CF6", side: "attacker", hint: "瞬时属性修饰" },
  { id: "other", name: "其他", short: "其他", color: "#8F98AA", side: "other", hint: "弱点 / 庇护等未归类项" },
];

export const CONTRIBUTION_COUNT = CONTRIBUTION_CATEGORIES.length;

const byId = new Map(CONTRIBUTION_CATEGORIES.map((category, index) => [category.id, index]));

export function contributionIndex(kind: string | number | undefined): number {
  if (typeof kind === "number") return kind >= 0 && kind < CONTRIBUTION_COUNT ? kind : CONTRIBUTION_COUNT - 1;
  if (kind && byId.has(kind)) return byId.get(kind)!;
  return CONTRIBUTION_COUNT - 1;
}

export function contributionCategory(kind: string | number | undefined): ContributionCategory {
  return CONTRIBUTION_CATEGORIES[contributionIndex(kind)];
}

export const SKILL_CATEGORIES = [
  { id: "attack", name: "普攻", color: "#8FA3B8" },
  { id: "skill", name: "战技", color: "#43C9FF" },
  { id: "ultimate", name: "终结技", color: "#FF6741" },
  { id: "combo", name: "连携技", color: "#AA82FF" },
  { id: "passive", name: "被动", color: "#5BD68B" },
  { id: "other", name: "其他", color: "#8F98AA" },
] as const;

export type SkillCategoryId = typeof SKILL_CATEGORIES[number]["id"];

export const DAMAGE_TYPE_NAMES = ["物理", "真实", "灼热", "电磁", "晶体", "生命汲取", "自然", "以太"];

export function damageTypeName(type: number | undefined): string {
  return type === undefined ? "—" : DAMAGE_TYPE_NAMES[type] ?? `类型 ${type}`;
}

// Beyond.Gameplay.AttributeType, the subset that shows up in ledgers.
export const ATTRIBUTE_NAMES: Record<number, string> = {
  2: "攻击力", 3: "防御力", 4: "物理承伤", 5: "灼热承伤", 6: "电磁承伤", 7: "晶体承伤",
  9: "暴击率", 10: "暴击伤害", 17: "普攻增伤", 28: "战技增伤", 30: "受治疗", 32: "终结技增伤", 33: "连携增伤",
  35: "灼热异常增伤", 36: "电磁异常增伤", 37: "晶体异常增伤", 38: "自然异常增伤", 44: "终结技充能",
  48: "自然承伤", 49: "点燃系数", 50: "物理增伤", 51: "灼热增伤", 52: "电磁增伤", 53: "晶体增伤", 54: "自然增伤", 55: "以太增伤",
  60: "以太承伤", 61: "破韧增伤", 62: "弱点", 63: "庇护", 64: "物理增幅", 65: "灼热增幅", 66: "电磁增幅", 67: "晶体增幅", 68: "自然增幅", 69: "以太增幅",
  70: "物理脆弱", 71: "灼热脆弱", 72: "电磁脆弱", 73: "晶体脆弱", 74: "自然脆弱", 75: "以太脆弱",
  94: "物理抗性", 95: "自然抗性", 96: "晶体抗性", 97: "电磁抗性", 98: "灼热抗性", 99: "以太抗性",
};

export function attributeName(type: number | undefined): string {
  if (type === undefined || type < 0) return "—";
  return ATTRIBUTE_NAMES[type] ?? `属性 ${type}`;
}

/**
 * Beyond.Gameplay.Core.BuffStackingSettings.StackingType, in declaration
 * order. `layered` marks the types the game itself counts as stacking
 * (get_isStackType); `instances` marks the ones that keep several Buff objects
 * alive instead of folding the layers into one.
 */
export const STACKING_TYPES = [
  { id: "unlimited", name: "无限叠加", layered: true, instances: true },
  { id: "highPriority", name: "取最高优先级", layered: false, instances: false },
  { id: "stack", name: "叠加", layered: true, instances: true },
  { id: "enhance", name: "叠层强化", layered: true, instances: false },
  { id: "refresh", name: "刷新持续时间", layered: false, instances: false },
  { id: "extend", name: "延长持续时间", layered: false, instances: false },
  { id: "modify", name: "改写", layered: false, instances: false },
  { id: "unique", name: "唯一", layered: false, instances: false },
  { id: "enhanceAndRefresh", name: "叠层强化并刷新", layered: true, instances: false },
  { id: "overwriteDuration", name: "覆盖持续时间", layered: false, instances: false },
  { id: "enhanceAndOverwriteDuration", name: "叠层强化并覆盖时长", layered: true, instances: false },
  { id: "highPriorityWithMaxStack", name: "优先级叠加", layered: true, instances: true },
  { id: "timedGrowingEnhance", name: "随时间增长叠层", layered: true, instances: false },
] as const;

export function stackingType(type: number | undefined) {
  return type === undefined ? undefined : STACKING_TYPES[type];
}

export function stackingTypeName(type: number | undefined): string {
  return stackingType(type)?.name ?? "未知";
}
