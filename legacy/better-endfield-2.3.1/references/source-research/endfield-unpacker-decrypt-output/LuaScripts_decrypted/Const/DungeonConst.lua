





DUNGEON_CATEGORY = {
    Challenge = "dungeon_challenge",
    Resource = "dungeon_resource",
    RPG = "dungeon_rpg",
    Char = "dungeon_char",
    Train = "dungeon_train",
    BossRush = "dungeon_bossrush",
    Race = "dungeon_race",
    WeeklyRaid = "dungeon_weeklyraid",
    CharTrial = "dungeon_chartrial",
    CharTutorial = "dungeon_chartutorial",
    SS = "dungeon_ss",
    WorldLevel = "dungeon_worldlevel",
    HighDifficulty = "dungeon_highdifficulty",
    SeasonTower = "dungeon_seasontower",
    ActMonster = "dungeon_actmonster",
    ContingencyContract = "dungeon_contract",
    DoubleBattle = "dungeon_takestwo",
}

DUNGEON_REWARD_TAG_STATE = {
    First = "First",
    Extra = "Extra",
    Regular = "Regular",
    Random = "Random",
}

IGNORE_STAMINA_SHORT_HINT_FORMAT = "today_ignore_stamina_short_hint_%s"
SERIALIZED_CATEGORY = "Dungeon"

UI_RESTORE_DUNGEON_CATEGORY = {
    DUNGEON_CATEGORY.BossRush,
    DUNGEON_CATEGORY.Resource,
    DUNGEON_CATEGORY.SS,
    DUNGEON_CATEGORY.CharTutorial,
}

GameMechanicsTypeToFormationTitleIcon = {
    [GEnums.GameMechanicsType.DungeonContingencyContract] = "icon_formation_contingency",
    [GEnums.GameMechanicsType.DungeonRacing] = "icon_formation_racingdungeon",
}



DUNGEON_FIRST_ROW_REWARDS_TITLE = {
    [DUNGEON_CATEGORY.Resource] = "LUA_DUNGEON_REGULAR_REWARD_TITLE",
}