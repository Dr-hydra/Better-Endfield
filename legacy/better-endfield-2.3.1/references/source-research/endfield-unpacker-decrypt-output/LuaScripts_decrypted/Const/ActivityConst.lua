local ActivityType = GEnums.ActivityType
ACTIVITY_TABLE = {
    [ActivityType.Basic] = {
        redDot = "ActivityBasic",
    },
    [ActivityType.LevelRewards] = {
        redDot = "ActivityBaseMultiStage",
    },
    [ActivityType.NewbieBonus] = {
        redDot = "ActivityBaseMultiStage",
	},
    [ActivityType.BeginnerGachaPool] = {
        redDot = "ActivityGachaBeginner",
    },
    [ActivityType.Checkin] = {
        redDot = "ActivityCheckIn",
    },
    [ActivityType.DailyTask] = {
        redDot = "ActivityGlobalEffect",
    },
    [ActivityType.GlobalEffect] = {
        redDot = "ActivityGlobalEffect",
    },
    [ActivityType.NormalChallenge] = {
        redDot = "ActivityNormalChallenge",
    },
    [ActivityType.CharGrowthRebate] = {
        redDot = "ActivityCharGrowthRebate",
    },
    [ActivityType.CharacterTrial] = {
        redDot = "ActivityCharTrial",
    },
    [ActivityType.HighDifficultyChallenge] = {
        redDot = "ActivityHighDifficulty",
    },
    [ActivityType.PhotoTaking] = {
        redDot = "ActivityConditionalMultiStage",
    },
    [ActivityType.CharacterGuideLine] = {
        redDot = "ActivityCharacterGuideLine",
    },
    [ActivityType.CharacterGuideLine] = {
        redDot = "ActivityCharacterGuideLine",
    },
    [ActivityType.ItemSubmission] = {
        redDot = "ActivityItemSubmission",
    },
    [ActivityType.VersionGuide] = {
        redDot = "ActivityVersionGuide",
    },
    [ActivityType.ScratchOffLottery] = {
        redDot = "ActivityScratchOffLottery",
    },
    [ActivityType.RewardOverview] = {
        redDot = "ActivityRewardOverview",
    },
    [ActivityType.WeeklyTask] = {
        redDot = "ActivityWeeklyTask",
    },
    [ActivityType.GraffitiCleaning] = {
        redDot = "ActivityGraffitiCleaning",
    },
    [ActivityType.DungeonFighting] = {
        redDot = "ActivityDungeon",
	},
    [ActivityType.LimitedFormula] = {
        redDot = "ActivityLimitedFormula",
    },
    [ActivityType.ArknightsXEndfieldLightWeight] = {
        redDot = "ActivityArknightsBirth",
	},
    [ActivityType.SeasonTower] = {
        redDot = "ActivitySeasonTower",
    },
    [ActivityType.ContingencyContract] = {
        redDot = "ActivityContingencyContract",
    },
    [ActivityType.CalendarCheckin] = {
        redDot = "ActivityCalendarCheckin",
    },
    [ActivityType.GamblingBattle] = {
        redDot = "ActivitySimulationTrainingTask",
    },
    [ActivityType.DoubleBattle] = {
        redDot = "ActivityDoubleBattle",
    },
    [ActivityType.SkipChapter] = {
        redDot = "ActivitySkipChapter",
    },
    [ActivityType.RacingDungeon] = {
        redDot = "ActivityRacingDungeon",
    },
    [ActivityType.ReflowNew] = {
        redDot = "ActivityReflowNew",
    },
    [ActivityType.InviteBack] = {
        redDot = "ActivityBasic",
    },

}
ACTIVITY_COMMON_SONS = {
    ActivityBasic = false,
    ActivityBaseMultiStage = false,
    ActivityDaily = false,
    ActivityGachaBeginner = false,
    ActivityCheckIn = false,
    ActivityGlobalEffect = false,
    ActivityNormalChallenge = false,
    ActivityHighDifficulty = false,
    ActivityConditionalMultiStage = false,
    ActivityCharacterGuideLine = false,
    ActivityItemSubmission = false,
    ActivityVersionGuide = false,
    ActivityScratchOffLottery = false,
    ActivityRewardOverview = false,
    ActivityWeeklyTask = false,
    ActivityGraffitiCleaning = false,
    ActivityDungeon = false,
    ActivityDoubleBattle = false,
    ActivityLimitedFormula = false,
    ActivityArknightsBirth = false,
    ActivityCalendarCheckin = false,
    ActivityContingencyContract = false,
    ActivitySimulationTrainingTask = false,
    ActivitySeasonTower = false,
    ActivitySkipChapter = false,
    ActivityRacingDungeon = false,
    ActivityReflowNew = false,
    ActivityWEB = false,

}
ACTIVITY_REMINDER_DRAW_MODE = {
    All = 1,
    NoComplete = 2,
}
