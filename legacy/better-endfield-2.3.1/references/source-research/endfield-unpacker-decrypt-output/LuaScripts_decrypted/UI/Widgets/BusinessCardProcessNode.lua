local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local CHAPTER_ICON_PATH = "Mission/ChapterIconNew"
local CHAPTER_BG_ICON_PATH = "Mission/ChapterBgIconNew"

BusinessCardProcessNode = HL.Class('BusinessCardProcessNode', UIWidgetBase)


BusinessCardProcessNode._OnFirstTimeInit = HL.Override() << function(self)

end

BusinessCardProcessNode.InitBusinessCardProcessNodeByRoleId = HL.Method(HL.Number) << function(self, roleId)
    self:_FirstTimeInit()
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if success and playerInfo then
        local info = {
            adventureLevel = playerInfo.adventureLevel,
            worldLevel = playerInfo.worldLevel,
            mainMissionId = playerInfo.mainMissionId,
            isSelf = roleId == GameInstance.player.roleId,
        }
        self:InitBusinessCardProcessNode(info)
    else
        logger.error("未找到角色信息，roleId: " .. roleId)
    end
end








BusinessCardProcessNode.InitBusinessCardProcessNode = HL.Method(HL.Table) << function(self, info)
    self:_FirstTimeInit()

    self.view.adventureLevelTxt.text = info.adventureLevel
    self.view.worldLevelTxt.text = info.worldLevel

    local currentChapter = nil
    if info.isSelf then
        local missionSystem = GameInstance.player.mission
        local missionId = missionSystem:GetLatestMainMissionId()
        if not string.isEmpty(missionId) then
            local chapterId = missionSystem:GetChapterIdByMissionId(missionId)
            currentChapter = missionSystem:GetChapterInfo(chapterId)
        end
    elseif info.mainMissionId and not string.isEmpty(info.mainMissionId) then

        local chapterId = GameInstance.player.mission:GetChapterIdByMissionId(info.mainMissionId);
        currentChapter = GameInstance.player.mission:GetChapterInfo(chapterId)
    end

    if currentChapter == nil then
        logger.error("未找到当前章节信息")
        return
    end

    self.view.mapNameTxt:SetAndResolveTextStyle(currentChapter.episodeName:GetText())
    local chapterNumTxt = currentChapter.chapterNum:GetText()
    local episodeNumTxt = currentChapter.episodeNum:GetText()
    local separator = ""
    if not string.isEmpty(chapterNumTxt) and not string.isEmpty(episodeNumTxt) then
        separator = " — "
    end
    self.view.mapDetailsTxt:SetAndResolveTextStyle(chapterNumTxt .. separator .. episodeNumTxt)

    local chapterConfig = UIConst.CHAPTER_ICON_CONFIGS[CS.Beyond.Gameplay.ChapterType.Main]

    if not string.isEmpty(currentChapter.icon) then
        self.view.iconExploreImg:LoadSprite(CHAPTER_ICON_PATH, currentChapter.icon)
    else
        self.view.iconExploreImg:LoadSprite(CHAPTER_ICON_PATH, chapterConfig.icon)
    end


    if not string.isEmpty(currentChapter.bgIcon) then
        self.view.bgExploreRightImg:LoadSprite(CHAPTER_BG_ICON_PATH, currentChapter.bgIcon)
    else
        self.view.bgExploreRightImg:LoadSprite(CHAPTER_BG_ICON_PATH, chapterConfig.bgIcon)
    end

end

HL.Commit(BusinessCardProcessNode)
return BusinessCardProcessNode

