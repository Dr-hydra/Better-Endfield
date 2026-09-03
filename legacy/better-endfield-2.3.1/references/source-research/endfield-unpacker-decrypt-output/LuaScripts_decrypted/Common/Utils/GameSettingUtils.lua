local GameSetting = CS.Beyond.GameSetting
local SettingItemType = GEnums.SettingItemType
local LanguageAudio = CS.Beyond.GameSetting.GameSettingLanguageAudio

local VFSBlockType = CS.Beyond.VFS.EVFSBlockType


local GameSettingUtils = setmetatable({}, {
    __index = GameSetting,
})

function GameSettingUtils.GetSettingValue(settingId)
    local hasItem, itemData = GameSetting.TryGetSettingItemData(settingId)
    if not hasItem then
        return nil
    end

    local settingItemType = itemData.settingItemType
    if settingItemType == SettingItemType.Dropdown then
        return GameSetting.GetSettingValueInt(settingId)
    elseif settingItemType == SettingItemType.Slider then
        return GameSetting.GetSettingValueFloat(settingId)
    elseif settingItemType == SettingItemType.Toggle then
        return GameSetting.GetSettingValueBool(settingId)
    end

    logger.error(ELogChannel.GameSetting, "Setting item type '" .. tostring(settingItemType) .. "' has no value")
    return nil
end

function GameSettingUtils.GetSettingDefaultValue(settingId)
    local hasItem, itemData = GameSetting.TryGetSettingItemData(settingId)
    if not hasItem then
        return nil
    end

    local settingItemType = itemData.settingItemType
    if settingItemType == SettingItemType.Dropdown then
        return GameSetting.GetSettingDefaultValueInt(settingId)
    elseif settingItemType == SettingItemType.Slider then
        return GameSetting.GetSettingDefaultValueFloat(settingId)
    elseif settingItemType == SettingItemType.Toggle then
        return GameSetting.GetSettingDefaultValueBool(settingId)
    end

    logger.error(ELogChannel.GameSetting, "Setting item type '" .. tostring(settingItemType) .. "' has no value")
    return nil
end

function GameSettingUtils.ToVFSBlockType(languageAudio)
    if languageAudio == LanguageAudio.Chinese then
        return VFSBlockType.AudioChinese
    elseif languageAudio == LanguageAudio.English then
        return VFSBlockType.AudioEnglish
    elseif languageAudio == LanguageAudio.Japanese then
        return VFSBlockType.AudioJapanese
    elseif languageAudio == LanguageAudio.Korean then
        return VFSBlockType.AudioKorean
    end
end


_G.GameSettingUtils = GameSettingUtils
return GameSettingUtils
