BATTLE_SQUAD_MAX_CHAR_NUM = GlobalConsts.CHAR_SQUAD_SLOT_NUM
CHAR_SKILL_NUM = 3

ObjectType = CS.Beyond.Gameplay.Core.ObjectType

SkillTypeEnum = GEnums.SkillType

CharHeightEnum = CS.Beyond.Gameplay.CharacterHeight

GUIDE_MANUALLY_TRIGGER_ID = "p_manually"

GUIDE_USE_INFO_NAME_CAST_SKILL_RECORD = "CastSkillRecord"

MAX_MAIL_COUNT = 200
MAX_LOST_AND_FOUND_COUNT = 500

SEC_PER_MIN = 60
MIN_PER_HOUR = 60
HOUR_PER_DAY = 24
SEC_PER_HOUR = SEC_PER_MIN * MIN_PER_HOUR
SEC_PER_DAY = HOUR_PER_DAY * SEC_PER_HOUR

USE_ITEM_SLOT_COUNT = 3

MAX_ITEM_RARITY = 6

ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY = {
    [GEnums.AttributeType.Level] = "level",
    [GEnums.AttributeType.MaxHp] = "maxHp",
    [GEnums.AttributeType.Atk] = "atk",
    [GEnums.AttributeType.Def] = "def",
    [GEnums.AttributeType.PhysicalDamageTakenScalar] = "physicalDamageTakenScalar",
    [GEnums.AttributeType.FireDamageTakenScalar] = "fireDamageTakenScalar",
    [GEnums.AttributeType.PulseDamageTakenScalar] = "pulseDamageTakenScalar",
    [GEnums.AttributeType.CrystDamageTakenScalar] = "crystDamageTakenScalar",
    [GEnums.AttributeType.Weight] = "weight",
    [GEnums.AttributeType.CriticalRate] = "criticalRate",
    [GEnums.AttributeType.CriticalDamageIncrease] = "criticalDamageIncrease",
    [GEnums.AttributeType.Hatred] = "hatred",
    [GEnums.AttributeType.NormalAttackRange] = "normalAttackRange",
    [GEnums.AttributeType.MoveSpeedScalar] = "moveSpeedScalar",
    [GEnums.AttributeType.TurnRateScalar] = "turnRateScalar",
    [GEnums.AttributeType.AttackRate] = "attackRate",
    [GEnums.AttributeType.HpRecoveryPerSec] = "hpRecoveryPerSec",
    [GEnums.AttributeType.HpRecoveryPerSecByMaxHpRatio] = "hpRecoveryPerSecByMaxHpRatio",
    [GEnums.AttributeType.MaxPoise] = "maxPoise",
    [GEnums.AttributeType.PoiseRecTime] = "poiseRecTime",
    [GEnums.AttributeType.MaxUltimateSp] = "maxUltimateSp",
    [GEnums.AttributeType.PoiseDamageTakenScalar] = "poiseDamageTakenScalar",
    [GEnums.AttributeType.PoiseDamageOutputScalar] = "poiseDamageOutputScalar",
    [GEnums.AttributeType.BreakingAttackDamageTakenScalar] = "breakingAttackDamageTakenScalar",
    [GEnums.AttributeType.HealOutputIncrease] = "healOutputIncrease",
    [GEnums.AttributeType.HealTakenIncrease] = "healTakenIncrease",
    [GEnums.AttributeType.PoiseRecTimeScalar] = "poiseRecTimeScalar",
    [GEnums.AttributeType.KnockDownTimeAddition] = "knockDownTimeAddition",
    [GEnums.AttributeType.Str] = "str",
    [GEnums.AttributeType.Agi] = "agi",
    [GEnums.AttributeType.Wisd] = "wisd",
    [GEnums.AttributeType.Will] = "will",
    [GEnums.AttributeType.LifeSteal] = "lifeSteal",
    [GEnums.AttributeType.UltimateSpGainScalar] = "ultimateSpGainScalar",
    [GEnums.AttributeType.AtbCostAddition] = "atbCostAddition",
    [GEnums.AttributeType.ComboSkillCooldownScalar] = "comboSkillCooldownScalar",
    [GEnums.AttributeType.NaturalDamageTakenScalar] = "naturalResistance",
    [GEnums.AttributeType.IgniteDamageScalar] = "igniteDamageScalar",
    [GEnums.AttributeType.PhysicalDamageIncrease] = "physicalDamageIncrease",
    [GEnums.AttributeType.FireDamageIncrease] = "fireDamageIncrease",
    [GEnums.AttributeType.PulseDamageIncrease] = "pulseDamageIncrease",
    [GEnums.AttributeType.CrystDamageIncrease] = "crystDamageIncrease",
    [GEnums.AttributeType.NaturalDamageIncrease] = "naturalDamageIncrease",
    [GEnums.AttributeType.EtherDamageIncrease] = "etherDamageIncrease",
}

LEVEL_MAP_MAX_LENGTH = 10000

CutsceneSkipType = GEnums.CutsceneSkipType

DialogType = CS.Beyond.Gameplay.DialogEnums.DialogType

GACHA_RATE_TOTAL_VALUE = 1000000

PhaseState = {
    Idle = 1,
    Push = 2,
    Pop = 3,
}

CinematicQueueItemTypeEnum = CS.Beyond.Gameplay.CinematicEnums.CinematicQueueItemType
CinematicQueueType = "Cinematic"

LevelScriptClearScreenQueueType = "LoginCheck_PreventedClearScreenForLevelScripts"

PhasePushSystemActionConflictName = "LUA_PHASE_PUSH"
FacBuildSystemActionConflictName = "FAC_BUILD"
PowerPoleFastTravelActionConflictName = "POWER_POLE_FAST_TRAVEL"
GeneralAbilityConflictName = "GENERAL_ABILITY"
FacDestroySystemActionConflictName = "FAC_DESTROY"
InteractOptionSystemActionConflictName = "INTERACT_OPTION"
TowerDefenseSystemActionConflictName = "TOWER_DEFENSE"
FacTopViewSystemActionConflictName = "FAC_TOP_VIEW"
QuickMenuSystemActionConflictName = "QUICK_MENU"

POPULAR_EXPIRE_WARNING_TIME = 3600 * 24 * 7
