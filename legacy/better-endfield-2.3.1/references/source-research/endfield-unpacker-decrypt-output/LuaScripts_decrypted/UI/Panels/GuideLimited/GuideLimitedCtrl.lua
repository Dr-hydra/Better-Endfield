local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local mainHudCtrl = require_ex('UI/Panels/MainHud/MainHudCtrl')
local PANEL_ID = PanelId.GuideLimited
local LimitedGuideType = CS.Beyond.Gameplay.LimitedGuideType
local LimitedGuideIconType = CS.Beyond.Gameplay.LimitedGuideIconType

GuideLimitedCtrl = HL.Class('GuideLimitedCtrl', uiCtrl.UICtrl)

local INITIAL_GUIDE_ID = 1
local NEXT_GUIDE_SHOW_DELAY = 1.0

local ICON_TYPE_TO_ICON_NAME = {
    [LimitedGuideIconType.Default] = "icon_limited_guide_default",
    [LimitedGuideIconType.System] = "icon_limited_guide_system",
    [LimitedGuideIconType.Explore] = "icon_limited_guide_explore",
    [LimitedGuideIconType.Factory] = "icon_limited_guide_factory",
    [LimitedGuideIconType.Battle] = "icon_limited_guide_battle",
}

GuideLimitedCtrl.m_showQueue = HL.Field(HL.Forward("Queue"))

GuideLimitedCtrl.m_guideInfoMap = HL.Field(HL.Table)

GuideLimitedCtrl.m_nextGuideId = HL.Field(HL.Number) << 1

GuideLimitedCtrl.m_showUpdate = HL.Field(HL.Number) << -1

GuideLimitedCtrl.m_updateValid = HL.Field(HL.Boolean) << true

GuideLimitedCtrl.m_delayShowTimer = HL.Field(HL.Number) << -1

GuideLimitedCtrl.m_progressWidth = HL.Field(HL.Number) << -1

GuideLimitedCtrl.m_isShowing = HL.Field(HL.Boolean) << false

GuideLimitedCtrl.m_isMainVisible = HL.Field(HL.Boolean) << false

GuideLimitedCtrl.m_controllerKeyCode = HL.Field(HL.Userdata)

GuideLimitedCtrl.s_waitReadGuideWikiEntry = HL.StaticField(HL.String) << ""

GuideLimitedCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CLEAR_LIMITED_GUIDE] = '_OnClearLimitedGuide',
    [MessageConst.ON_GUIDE_PREPARE_NARRATIVE] = '_RefreshMainVisibleState',
    [MessageConst.ON_GUIDE_LEAVE_NARRATIVE] = '_RefreshMainVisibleState',
    [MessageConst.ON_CLEAR_SCREEN_STATE_CHANGED] = '_RefreshMainVisibleState',
}


GuideLimitedCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_showQueue = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_nextGuideId = INITIAL_GUIDE_ID
    self.m_guideInfoMap = {}
    self.m_progressWidth = self.view.progressLine.rect.width

    self.view.button.onClick:AddListener(function()
        self:_OnClickButton()
    end)
    local actionInfo = InputManagerInst:GetPlayerActionInfo(self.view.button.onClick.playerActionId)
    if actionInfo ~= nil then
        self.m_controllerKeyCode = actionInfo.primaryGamepadInput.key
    end
end

GuideLimitedCtrl.OnClose = HL.Override() << function(self)
    self.m_showUpdate = LuaUpdate:Remove(self.m_showUpdate)
end

GuideLimitedCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, isActive)
    self.m_updateValid = isActive
end

GuideLimitedCtrl.OnWikiEntryRead = HL.StaticMethod(HL.Any) << function(arg)
    local wikiEntryId = unpack(arg)
    if wikiEntryId ~= GuideLimitedCtrl.s_waitReadGuideWikiEntry then
        return
    end
    GuideLimitedCtrl.s_waitReadGuideWikiEntry = ""
    Notify(MessageConst.ON_LIMITED_GUIDE_WIKI_ENTRY_READ_STATE_CHANGE)
end

GuideLimitedCtrl.OnShowLimitedGuide = HL.StaticMethod(HL.Any) << function(args)
    local guideInfo = unpack(args)
    if guideInfo == nil then
        return
    end

    local ctrl = UIManager:AutoOpen(PANEL_ID)
    ctrl:TryShowLimitedGuide(guideInfo)
end

GuideLimitedCtrl.TryShowLimitedGuide = HL.Method(HL.Any) << function(self, guideInfo)
    if self.m_isShowing and guideInfo.needIgnoreWhenConflict then
        return
    end

    local guideId = self.m_nextGuideId
    self.m_nextGuideId = self.m_nextGuideId + 1
    self.m_guideInfoMap[guideId] = guideInfo

    if self.m_showQueue:Empty() then

        self:_StartShowLimitedGuide(guideId)
    end

    self.m_showQueue:Push(guideId)
end

GuideLimitedCtrl._StartShowLimitedGuide = HL.Method(HL.Number) << function(self, guideId)
    local guideInfo = self.m_guideInfoMap[guideId]
    if guideInfo == nil then
        return
    end

    self:_RefreshDisplayState(guideId)

    UIManager:SetTopOrder(PANEL_ID)

    local duration = guideInfo.duration
    local time = 0.0
    self:_RefreshProgressFillState(0)
    self.m_showUpdate = LuaUpdate:Add("Tick", function(deltaTime)
        if not self:IsShow() or not self.m_updateValid or not self.m_isMainVisible then
            return
        end

        if time >= duration then
            local stopValid = not DeviceInfo.usingController or
                not self.m_controllerKeyCode or
                not InputManagerInst:GetKey(self.m_controllerKeyCode)
            if stopValid then

                if guideInfo.type == LimitedGuideType.MediaGuide then
                    local success, wikiLimitedGuideData = Tables.wikiLimitedGuideTable:TryGetValue(guideInfo.mediaGuideGroupId)
                    if success then
                        if WikiUtils.isWikiEntryUnread(wikiLimitedGuideData.wikiEntryId) then
                            GuideLimitedCtrl.s_waitReadGuideWikiEntry = wikiLimitedGuideData.wikiEntryId
                            Notify(MessageConst.ON_LIMITED_GUIDE_WIKI_ENTRY_READ_STATE_CHANGE)
                        end
                    end
                end


                self:_StopShowLimitedGuide(guideId)
            end
        end

        self:_RefreshProgressFillState(time / duration)

        time = time + deltaTime
    end)

    self.m_isShowing = true
    GameInstance.player.guide.isLimitedGuideShowing = true
    self:_RefreshMainVisibleState()
end

GuideLimitedCtrl._StopShowLimitedGuide = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, guideId, forceStop)
    self:_RefreshProgressFillState(1)
    self.m_showUpdate = LuaUpdate:Remove(self.m_showUpdate)

    if self.m_showQueue:Front() ~= guideId then
        logger.error("Wrong sequence in limited guide show queue!")
        return
    end

    self.m_showQueue:Pop()
    self.m_guideInfoMap[guideId] = nil

    if forceStop then
        self.m_showQueue:Clear()
        self.m_guideInfoMap = {}
    end

    if not self.m_showQueue:Empty() then
        self.m_isShowing = false
        self:_RefreshMainVisibleState()
        self.m_delayShowTimer = self:_StartTimer(NEXT_GUIDE_SHOW_DELAY, function()
            self.m_delayShowTimer = self:_ClearTimer(self.m_delayShowTimer)
            self:_StartShowLimitedGuide(self.m_showQueue:Front())
        end)
    else
        self.m_isShowing = false
        GameInstance.player.guide.isLimitedGuideShowing = false
        self:_RefreshMainVisibleState()
    end
end

GuideLimitedCtrl._RefreshMainVisibleState = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    local isVisible = true

    if not self.m_isShowing then
        isVisible = false
    end

    if GameInstance.player.guide.isInterruptByNarrative then
        isVisible = false
    end


    if mainHudCtrl.MainHudCtrl.s_clearScreenId ~= nil and mainHudCtrl.MainHudCtrl.s_clearScreenId > 0 then
        isVisible = false
    end

    if mainHudCtrl.MainHudCtrl.s_clearScreenIdExceptSomePanel ~= nil and mainHudCtrl.MainHudCtrl.s_clearScreenIdExceptSomePanel > 0 then
        isVisible = false
    end

    UIUtils.PlayAnimationAndToggleActive(self.view.main, isVisible)
    if isVisible then
        AudioAdapter.PostEvent("Au_UI_Popup_GuideLimitedPanel_Open")
    end

    self.m_isMainVisible = isVisible
end

GuideLimitedCtrl._RefreshDisplayState = HL.Method(HL.Number) << function(self, guideId)
    local guideInfo = self.m_guideInfoMap[guideId]
    if guideInfo == nil then
        return
    end

    self.view.text.text = Utils.getGuideText(guideInfo.textId)

    local sprite = self:_GetIconSprite(guideInfo.iconType)
    if sprite ~= nil then
        self.view.icon.sprite = sprite
        self.view.iconShadow.sprite = sprite
    end
end

GuideLimitedCtrl._RefreshProgressFillState = HL.Method(HL.Number) << function(self, percent)
    if IsNull(self.view.progressLine) then
        return
    end

    local currentWidth = (1 - percent) * self.m_progressWidth
    currentWidth = math.max(currentWidth, 0)
    UIUtils.setSizeDeltaX(self.view.progressLine, currentWidth)
end

GuideLimitedCtrl._OnClickButton = HL.Method() << function(self)
    if self.m_showQueue:Empty() then
        return
    end

    local guideId = self.m_showQueue:Front()
    local guideInfo = self.m_guideInfoMap[guideId]
    if guideInfo == nil then
        return
    end

    if guideInfo.type == LimitedGuideType.MediaGuide then
        self:_StartMediaGuide(guideInfo.mediaGuideGroupId, guideInfo.needIgnoreGuideScope)
    elseif guideInfo.type == LimitedGuideType.Wiki then
        self:_ShowWikiEntry(guideInfo.wikiId)
    end

    self:_StopShowLimitedGuide(guideId)
end

GuideLimitedCtrl._OnClearLimitedGuide = HL.Method() << function(self)
    if self.m_showQueue:Empty() then
        return
    end

    if not self.m_isShowing then
        return
    end

    local guideId = self.m_showQueue:Front()
    self:_StopShowLimitedGuide(guideId, true)
end



GuideLimitedCtrl._ShowWikiEntry = HL.Method(HL.String) << function(self, wikiId)
    Notify(MessageConst.SHOW_WIKI_ENTRY, {
        wikiEntryId = wikiId,
    })
end

GuideLimitedCtrl._GetIconSprite = HL.Method(HL.Userdata).Return(HL.Userdata) << function(self, iconType)
    local iconName = ICON_TYPE_TO_ICON_NAME[iconType]
    return self:LoadSprite(UIConst.UI_SPRITE_LIMITED_GUIDE, iconName)
end






GuideLimitedCtrl._StartMediaGuide = HL.Method(HL.String, HL.Boolean) << function(self, mediaGuideGroupId, needIgnoreGuideScope)
    if string.isEmpty(mediaGuideGroupId) then
        return
    end

    GameInstance.player.guide:ManuallyStartGuideGroup(mediaGuideGroupId, needIgnoreGuideScope)
end



HL.Commit(GuideLimitedCtrl)
