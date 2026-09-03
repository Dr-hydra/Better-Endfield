
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MissionCompletePop
local ChapterType = CS.Beyond.Gameplay.ChapterType
local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState

local CHAPTER_ICON_PATH = "Mission/ChapterIconNew"
local CHAPTER_BG_ICON_PATH = "Mission/ChapterBgIconNew"

local ChapterConfig = {
    [ChapterType.Main] = {
        icon = "chapter_main_icon_01",
        bgIcon = "chapter_main_bg_icon_01",
        deco = "main_mission_icon_gray",
    },
    [ChapterType.Other] = {
        icon = "",
        bgIcon = "",
        deco = "",
    },
}

local STATE_CHAPTER_START = 0
local STATE_CHAPTER_FINISH = 1

local function _ToBoolean(value)
    if type(value) == "boolean" then
        return value
    end
    if type(value) ~= "string" then
        return false
    end
    return string.lower(value) == "true"
end
















MissionCompletePopCtrl = HL.Class('MissionCompletePopCtrl', uiCtrl.UICtrl)








MissionCompletePopCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MissionCompletePopCtrl.m_chapterEffectList = HL.Field(HL.Table)


MissionCompletePopCtrl.m_itemCells = HL.Field(HL.Forward("UIListCache"))





MissionCompletePopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    Notify(MessageConst.COMMON_START_BLOCK_MAIN_HUD_ACTION_QUEUE)

    self.m_chapterEffectList = {}


    local chapterId, state, tbc = unpack(arg)

    self:PushChapter(chapterId, state)


    self.view.main.gameObject:SetActive(false)

    self:_StartCoroutine(function()
        while #self.m_chapterEffectList > 0 do

            coroutine.step()
            while UIManager:IsOpen(PanelId.CommonTaskTrackToast) do
                coroutine.step()
            end
            local chapterEffect = table.remove(self.m_chapterEffectList,1)
            self.view.main.gameObject:SetActive(true)
            local length = self:_Refresh(chapterEffect.chapterId, chapterEffect.state, tbc)
            coroutine.wait(length)
            CS.Beyond.Gameplay.Actions.ShowChapterPanelWaitForFinish.OnChapterPanelFinish(chapterEffect.chapterId, chapterEffect.state)
            self.view.main.gameObject:SetActive(false)
        end
        self:Close()
    end)
end






MissionCompletePopCtrl._Refresh = HL.Method(HL.String, HL.Number, HL.Boolean).Return(HL.Number) << function(self, chapterId, state, tbc)
    local missionSystem = GameInstance.player.mission
    local chapterInfo = missionSystem:GetChapterInfo(chapterId)
    if chapterInfo then
        local length = 0
        if chapterInfo.type == ChapterType.Main then
            length = self:_RefreshMainChapter(chapterInfo, state, tbc)
        elseif chapterInfo.type == ChapterType.Other then
            length = self:_RefreshCharacterChapter(chapterInfo, state)
        end
        return length
    end
    return 0
end






MissionCompletePopCtrl._RefreshMainChapter = HL.Method(HL.Any, HL.Number, HL.Boolean).Return(HL.Number) << function(self, chapterInfo, state, tbc)

    if state == STATE_CHAPTER_START then
        local audioKey = chapterInfo.beginAudioKey
        AudioManager.PostEvent(audioKey)
    else
        local audioKey = chapterInfo.endAudioKey
        AudioManager.PostEvent(audioKey)
    end

    local chapterMain = self.view.chapterMain
    local chapterCharacter = self.view.chapterCharacter
    chapterMain.gameObject:SetActive(true)
    chapterCharacter.gameObject:SetActive(false)
    chapterMain.episodeName:SetAndResolveTextStyle(chapterInfo.episodeName:GetText())
    local chapterNumTxt = chapterInfo.chapterNum:GetText()
    local episodeNumTxt = chapterInfo.episodeNum:GetText()
    local separator = ""
    if not string.isEmpty(chapterNumTxt) and not string.isEmpty(episodeNumTxt) then
        separator = " - "
    end
    chapterMain.chapterNumAndEpisodeNum:SetAndResolveTextStyle(chapterNumTxt .. separator .. episodeNumTxt)

    local chapterConfig = ChapterConfig[chapterInfo.type]

    if not string.isEmpty(chapterInfo.icon) then
        chapterMain.icon.gameObject:SetActive(true)
        chapterMain.icon:LoadSprite(CHAPTER_ICON_PATH, chapterInfo.icon .. "_long")
    elseif not string.isEmpty(chapterConfig.icon) then
        chapterMain.icon.gameObject:SetActive(true)
        chapterMain.icon:LoadSprite(CHAPTER_ICON_PATH, chapterConfig.icon .. "_long")
    else
        chapterMain.icon.gameObject:SetActive(false)
        chapterMain.icon.sprite = nil
    end

    if not string.isEmpty(chapterInfo.bgIcon) then
        chapterMain.bgIcon.gameObject:SetActive(true)
        chapterMain.bgIcon:LoadSprite(CHAPTER_BG_ICON_PATH, chapterInfo.bgIcon .. "_long")
    elseif not string.isEmpty(chapterConfig.icon) then
        chapterMain.bgIcon.gameObject:SetActive(true)
        chapterMain.bgIcon:LoadSprite(CHAPTER_BG_ICON_PATH, chapterConfig.bgIcon .. "_long")
    else
        chapterMain.bgIcon.gameObject:SetActive(false)
        chapterMain.bgIcon.sprite = nil
    end

    if not string.isEmpty(chapterConfig.deco) then
        chapterMain.deco.gameObject:SetActive(true)
        chapterMain.deco:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, chapterConfig.deco)
    else
        chapterMain.deco.gameObject:SetActive(false)
        chapterMain.deco.sprite = nil
    end

    chapterMain.chapterStartLabel.gameObject:SetActive(state == STATE_CHAPTER_START)
    chapterMain.chapterCompleteLabel.gameObject:SetActive(state == STATE_CHAPTER_FINISH)
    chapterMain.finishTitle.gameObject:SetActive(state == STATE_CHAPTER_FINISH)

    if tbc then
        self.view.tbcRoot.gameObject:SetActive(true)
        self.view.tbcText1:SetAndResolveTextStyle(chapterNumTxt .. separator .. episodeNumTxt)
        self.view.tbcText2:SetAndResolveTextStyle(chapterInfo.episodeName:GetText())
        self.view.tbcIcon:LoadSprite(CHAPTER_ICON_PATH, chapterInfo.icon .. "_long")
        chapterMain.gameObject:SetActive(false)

        local animWrapper = self.animationWrapper
        animWrapper:PlayWithTween(self.view.config.CHAPTER_COMPLETE_TBC_ANIM)
        local length = animWrapper:GetClipLength(self.view.config.CHAPTER_COMPLETE_TBC_ANIM)
        return length

    else
        self.view.tbcRoot.gameObject:SetActive(false)
    end

    local animWrapper = self.animationWrapper
    if state == STATE_CHAPTER_START then
        animWrapper:PlayWithTween(self.view.config.CHAPTER_MAIN_START_ANIM)
        local length = animWrapper:GetClipLength(self.view.config.CHAPTER_MAIN_START_ANIM)
        return length
    else
        animWrapper:PlayWithTween(self.view.config.CHAPTER_MAIN_FINISH_ANIM)
        local length = animWrapper:GetClipLength(self.view.config.CHAPTER_MAIN_FINISH_ANIM)
        return length
    end

end





MissionCompletePopCtrl._RefreshCharacterChapter = HL.Method(HL.Any, HL.Number).Return(HL.Number) << function(self, chapterInfo, state)

    if state == STATE_CHAPTER_START then
        local audioKey = chapterInfo.beginAudioKey
        AudioManager.PostEvent(audioKey)
    else
        local audioKey = chapterInfo.endAudioKey
        AudioManager.PostEvent(audioKey)
    end

    local chapterMain = self.view.chapterMain
    local chapterCharacter = self.view.chapterCharacter
    chapterMain.gameObject:SetActive(false)
    chapterCharacter.gameObject:SetActive(true)

    chapterCharacter.episodeName:SetAndResolveTextStyle(chapterInfo.episodeName:GetText())
    local chapterNumTxt = chapterInfo.chapterNum:GetText()
    local episodeNumTxt = chapterInfo.episodeNum:GetText()
    local separator = ""
    if not string.isEmpty(chapterNumTxt) and not string.isEmpty(episodeNumTxt) then
        separator = " - "
    end
    chapterCharacter.chapterNumAndEpisodeNum:SetAndResolveTextStyle(chapterNumTxt .. separator .. episodeNumTxt)


    if not string.isEmpty(chapterInfo.icon) then
        chapterCharacter.charIcon.gameObject:SetActive(true)
        chapterCharacter.charIcon:LoadSprite("CharInfo", chapterInfo.icon)
    else
        chapterCharacter.charIcon.gameObject:SetActive(false)
        chapterCharacter.charIcon.sprite = nil
    end

    chapterCharacter.startText.gameObject:SetActive(state == STATE_CHAPTER_START)
    chapterCharacter.finishText.gameObject:SetActive(state == STATE_CHAPTER_FINISH)
    chapterCharacter.finishDeco.gameObject:SetActive(state == STATE_CHAPTER_FINISH)

    local animWrapper = self.animationWrapper
    if state == STATE_CHAPTER_START then
        animWrapper:PlayWithTween(self.view.config.CHAPTER_CHARACTER_START_ANIM)
        local length = animWrapper:GetClipLength(self.view.config.CHAPTER_CHARACTER_START_ANIM)
        return length
    else
        animWrapper:PlayWithTween(self.view.config.CHAPTER_CHARACTER_FINISH_ANIM)
        local length = animWrapper:GetClipLength(self.view.config.CHAPTER_CHARACTER_FINISH_ANIM)
        return length
    end

end






MissionCompletePopCtrl.PushChapter = HL.Method(HL.String, HL.Number, HL.boolean) << function(self, chapterId, state, tbc)

    table.insert(self.m_chapterEffectList, {state = state, chapterId = chapterId, tbc = tbc})
end


MissionCompletePopCtrl.IsOpen = HL.StaticMethod().Return(HL.Boolean) << function()
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    return isOpen
end



MissionCompletePopCtrl.OnChapterStart = HL.StaticMethod(HL.Table) << function(arg)
    local chapterId = unpack(arg)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl:PushChapter(chapterId, STATE_CHAPTER_START, false)
    else
        UIManager:Open(PANEL_ID, {chapterId, STATE_CHAPTER_START, false})
    end
end



MissionCompletePopCtrl.OnChapterCompleted = HL.StaticMethod(HL.Table) << function(arg)
    local chapterId, tbc = unpack(arg)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        ctrl:PushChapter(chapterId, STATE_CHAPTER_FINISH, tbc)
    else
        UIManager:Open(PANEL_ID, {chapterId, STATE_CHAPTER_FINISH, tbc})
    end
end



MissionCompletePopCtrl.OnShowPanelDirectly = HL.StaticMethod(HL.Table) << function(arg)
    local chapterId, effectType, tbc = unpack(arg)

    LuaSystemManager.mainHudActionQueue:AddRequest("ChapterPanelWithoutMissionHud", function()
        if effectType == STATE_CHAPTER_START then
            UIManager:Open(PANEL_ID, {chapterId, STATE_CHAPTER_START, false})
        end
        if effectType == STATE_CHAPTER_FINISH then
            UIManager:Open(PANEL_ID, {chapterId, STATE_CHAPTER_FINISH, tbc})
        end
    end)
end



MissionCompletePopCtrl.OnShowPanelDirectlyStringParam = HL.StaticMethod(HL.Table) << function(arg)
    local chapterId, isComplete, needTbc, tbcVersion = unpack(arg)
    local state = _ToBoolean(isComplete) and STATE_CHAPTER_FINISH or STATE_CHAPTER_START
    local tbc = false
    if state == STATE_CHAPTER_FINISH and _ToBoolean(needTbc) and not string.isEmpty(tbcVersion) then
        local branchVersion = CS.Beyond.Gameplay.GameInstance.player.playerInfoSystem.branchVersion
        tbc = CS.Beyond.GlobalOptions.CheckVersionBefore(branchVersion, tbcVersion) or
            CS.Beyond.GlobalOptions.CheckVersionEqual(branchVersion, tbcVersion)
    end
    UIManager:Open(PANEL_ID, {chapterId, state, tbc})
end








MissionCompletePopCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.COMMON_END_BLOCK_MAIN_HUD_ACTION_QUEUE)
end

HL.Commit(MissionCompletePopCtrl)
