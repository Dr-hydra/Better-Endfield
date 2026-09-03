TriggerConfig ={
    ["btnItem01"] = "001",
    ["btnItem02"] = "002",
    ["btnItem03"] = "003",
    ["btnItem04"] = "004",
    ["btnItem05"] = "005",
    ["btnItem06"] = "006",
    ["btnItem07"] = "Ice",
}

PhotoEffectConfig = {
    [1] = {
        mountPoint = "b_photo_01",
        effectNames = {"P_fxui_prop_charinfo_zmdlife_01", "P_fxui_prop_charinfo_zmdlife_02","P_fxui_prop_charinfo_zmdlife_10"},
        vfxNames = {"P_fxui_prop_charinfo_zmdlife_07"},
        shadowEffectName = "P_fxui_prop_charinfo_zmdlife_10_loop",
    },
    [3] = {
        mountPoint = "b_photo_03",
        effectNames = {"P_fxui_prop_charinfo_zmdlife_05", "P_fxui_prop_charinfo_zmdlife_06", "P_fxui_prop_charinfo_zmdlife_12"},
        vfxNames = {"P_fxui_prop_charinfo_zmdlife_09"},
        shadowEffectName = "P_fxui_prop_charinfo_zmdlife_12_loop",
    },
    [5] = {
        mountPoint = "b_photo_02",
        effectNames = {"P_fxui_prop_charinfo_zmdlife_03", "P_fxui_prop_charinfo_zmdlife_04", "P_fxui_prop_charinfo_zmdlife_11"},
        vfxNames = {"P_fxui_prop_charinfo_zmdlife_08"},
        shadowEffectName = "P_fxui_prop_charinfo_zmdlife_11_loop",
    },
}

StarEffectConfig = {
    [1] = {
        mountPoint = "bone_potential_001",
        unlockEffectName = "P_fxui_prop_charinfo_zmdlife_originium_01",
        unlockMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_01",
        loopEffectName = "P_fxui_prop_charinfo_zmdlife_originium_01_loop",
        loopMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_01_loop"
    },
    [2] = {
        mountPoint = "bone_potential_002",
        unlockEffectName = "P_fxui_prop_charinfo_zmdlife_originium_02",
        unlockMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_02",
        loopEffectName = "P_fxui_prop_charinfo_zmdlife_originium_02_loop",
        loopMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_02_loop"
    },
    [3] = {
        mountPoint = "bone_potential_003",
        unlockEffectName = "P_fxui_prop_charinfo_zmdlife_originium_03",
        unlockMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_03",
        loopEffectName = "P_fxui_prop_charinfo_zmdlife_originium_03_loop",
        loopMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_03_loop"
    },
    [4] = {
        mountPoint = "bone_potential_004",
        unlockEffectName = "P_fxui_prop_charinfo_zmdlife_originium_04",
        unlockMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_04",
        loopEffectName = "P_fxui_prop_charinfo_zmdlife_originium_04_loop",
        loopMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_04_loop"
    },
    [5] = {
        mountPoint = "bone_potential_005",
        unlockEffectName = "",
        unlockMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_05",
        loopEffectName = "",
        loopMaxEffectName = "P_fxui_prop_charinfo_zmdlife_originium_2_05_loop"
    },
    Max = {
        {
            mountPoint = "root",
            effectName = "P_fxui_prop_charinfo_zmdlife_originium_2_06",
        },
        {
            mountPoint = "bone_halo_001",
            effectName = "P_fxui_prop_charinfo_zmdlife_originium_2_08",
        },
        {
            mountPoint = "Camera",
            effectName = "P_fxui_prop_charinfo_zmdlife_originium_2_07",
        }
    },
    MaxLoop = {
        {
            mountPoint = "root",
            effectName = "P_fxui_prop_charinfo_zmdlife_originium_2_06_loop",
        },
        {
            mountPoint = "Camera",
            effectName = "P_fxui_prop_charinfo_zmdlife_originium_2_07_loop",
        }
    }
}

DecoItemPathFormat = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/CharInfo/PotentialDeco/%s.prefab"
DecoACPathFormat = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/CharInfo/PotentialDeco/%s.overrideController"
DecoItemReplaceCfg = {
    {
        cfgName = "rabbitModelId",
        decoName = "P_prop_charinfo_zmdlife_01",
        ignoreRotate = true,
    },
    {
        cfgName = "carModelId",
        decoName = "P_prop_charinfo_zmdlife_07",
        posOffset = {
            ["P_prop_charinfo_zmdlife_11"] = Vector3(-0.005, 0, 0),
        }
    }
}