local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

local MissionState = CS.Beyond.Gameplay.MissionSystem.MissionState

AppStoreSystem = HL.Class('AppStoreSystem', LuaSystemBase.LuaSystemBase)

local RATING_POPUP_LAST_TIMESTAMP_KEY = "app_store_rating_popup_last_timestamp"
local RATING_POPUP_TIMES_KEY = "app_store_rating_popup_times"

local MAIN_HUD_QUEUE_ACTION_TYPE = "AppStoreRatingPopup"

local RatingPopupReason = {
    GachaCharPool = 1,
    UseItem = 2,
    CompleteMission = 3,
}

local PanelListenType = {
    Open = 1,
    Close = 2,
}


local DefaultUseItemPopupConfig = {
    panelName = "RewardsPopUpForSystem",
    listenType = PanelListenType.Open,
}

local UseItemPopupConfigs = {

    ["item_case_char_selfselect_standard"] = {
        panelName = "GachaChar",
        listenType = PanelListenType.Close,
    },
}

AppStoreSystem.m_registerKeys = HL.Field(HL.Table)

AppStoreSystem.m_listeningPanelName = HL.Field(HL.String) << ""

AppStoreSystem.AppStoreSystem = HL.Constructor() << function(self)
    local supportsRatingPopup = CS.Beyond.SDK.SDKUtils.SupportsAppStoreRatingPopup()
    if not supportsRatingPopup then
        return
    end

    self:_Initialize()
end

AppStoreSystem._Initialize = HL.Method() << function(self)
    self:RegisterMessage(MessageConst.ON_GACHA_SUCC, function(args)
        self:_OnGachaSucc(args)
    end)
    self:RegisterMessage(MessageConst.ON_USE_ITEM, function(args)
        self:_OnUseItem(args)
    end)
    self:RegisterMessage(MessageConst.ON_MISSION_STATE_CHANGE, function(args)
        self:_OnMissionStateChange(args)
    end)

    self.m_registerKeys = {}
end

AppStoreSystem._CustomRegisterMessage = HL.Method(HL.Number, HL.Function) << function(self, message, action)
    self:_CustomUnregisterMessage(message)
    self.m_registerKeys[message] = MessageManager:Register(message, action, self)
end

AppStoreSystem._CustomUnregisterMessage = HL.Method(HL.Number) << function(self, message)
    local registerKey = self.m_registerKeys[message]
    if registerKey then
        MessageManager:Unregister(registerKey)
        self.m_registerKeys[message] = nil
    end
end

AppStoreSystem._CanShowRatingPopup = HL.Method().Return(HL.Boolean) << function(self)
    if GameInstance.player.gameSettingSystem.forbiddenAppStoreRatingPopup then
        return false
    end


    local hasVar, timestamp = GameInstance.player.globalVar:TryGetClientVar(RATING_POPUP_LAST_TIMESTAMP_KEY)
    if hasVar then
        local currTimestamp = DateTimeUtils.GetCurrentTimestampBySeconds()
        local deltaSeconds = currTimestamp - timestamp
        if deltaSeconds <= Tables.globalConst.appStoreRatingCooldownSeconds then
            return false
        end
    end

    return true
end

AppStoreSystem._ShowRatingPopup = HL.Method(HL.Number, HL.Any) << function(self, reason, key)
    if reason == RatingPopupReason.GachaCharPool then

        self:_ShowRatingPopupOnPanelOpen(reason, "GachaCharResultBG")

    elseif reason == RatingPopupReason.UseItem then

        local config = UseItemPopupConfigs[key] or DefaultUseItemPopupConfig
        if config.listenType == PanelListenType.Open then
            self:_ShowRatingPopupOnPanelOpen(reason, config.panelName)
        else
            self:_ShowRatingPopupOnPanelClose(reason, config.panelName)
        end

    elseif reason == RatingPopupReason.CompleteMission then

        if LuaSystemManager.mainHudActionQueue:HasRequest(MAIN_HUD_QUEUE_ACTION_TYPE) then
            return
        end
        LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_QUEUE_ACTION_TYPE, function()
            self:_ShowRatingPopup_Internal(reason)
        end)
    end
end

AppStoreSystem._ShowRatingPopup_Internal = HL.Method(HL.Number) << function(self, reason)
    local success = CS.Beyond.SDK.SDKUtils.ShowAppStoreRatingPopup()
    if not success then
        return
    end


    local currTimestamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    GameInstance.player.globalVar:SetClientVar(RATING_POPUP_LAST_TIMESTAMP_KEY, currTimestamp)

    local hasVar, times = GameInstance.player.globalVar:TryGetClientVar(RATING_POPUP_TIMES_KEY)
    times = hasVar and (times + 1) or 1
    GameInstance.player.globalVar:SetClientVar(RATING_POPUP_TIMES_KEY, times)


    local reasonStr = lume.find(RatingPopupReason, reason)
    EventLogManagerInst:GameEvent_AppStoreRatingPopup(reasonStr)
end

AppStoreSystem._ShowRatingPopupOnPanelOpen = HL.Method(HL.Number, HL.String) << function(self, reason, targetPanelName)
    if UIManager:IsShow(PanelId[targetPanelName]) then

        self:_ShowRatingPopup_Internal(reason)
    else

        self.m_listeningPanelName = targetPanelName

        local onPanelOpen = function(panelName)
            self:_OnPanelOpen(reason, panelName)
        end
        self:_CustomRegisterMessage(MessageConst.ON_UI_PANEL_START_OPEN, onPanelOpen)
        self:_CustomRegisterMessage(MessageConst.ON_UI_PANEL_SHOW, onPanelOpen)
    end
end

AppStoreSystem._OnPanelOpen = HL.Method(HL.Number, HL.String) << function(self, reason, panelName)
    if panelName ~= self.m_listeningPanelName then
        return
    end


    self:_CustomUnregisterMessage(MessageConst.ON_UI_PANEL_START_OPEN)
    self:_CustomUnregisterMessage(MessageConst.ON_UI_PANEL_SHOW)

    self:_ShowRatingPopup_Internal(reason)
end

AppStoreSystem._ShowRatingPopupOnPanelClose = HL.Method(HL.Number, HL.String) << function(self, reason, targetPanelName)

    self.m_listeningPanelName = targetPanelName

    local onPanelClose = function(panelName)
        self:_OnPanelClose(reason, panelName)
    end
    self:_CustomRegisterMessage(MessageConst.ON_UI_PANEL_HIDE, onPanelClose)
    self:_CustomRegisterMessage(MessageConst.ON_UI_PANEL_CLOSED, onPanelClose)
end

AppStoreSystem._OnPanelClose = HL.Method(HL.Number, HL.String) << function(self, reason, panelName)
    if panelName ~= self.m_listeningPanelName then
        return
    end


    self:_CustomUnregisterMessage(MessageConst.ON_UI_PANEL_HIDE)
    self:_CustomUnregisterMessage(MessageConst.ON_UI_PANEL_CLOSED)

    self:_ShowRatingPopup_Internal(reason)
end

AppStoreSystem._OnGachaSucc = HL.Method(HL.Table) << function(self, arg)


    if not self:_CanShowRatingPopup() then
        return
    end

    local msg = unpack(arg)
    local poolId = msg.GachaPoolId
    local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
    if not hasInfo then
        logger.error("Gacha character pool info not found, id: " .. tostring(poolId))
        return
    end
    local hasData, poolData = Tables.gachaCharPoolTable:TryGetValue(poolId)
    if not hasData then
        logger.error("Gacha character pool data not found, id: " .. tostring(poolId))
        return
    end

    if not poolInfo.isChar or poolInfo.type ~= GEnums.CharacterGachaPoolType.Special then
        return
    end

    local currProgress = poolInfo.hardGuaranteeProgress
    local maxProgress = Tables.globalConst.appStoreRatingConditionGachaCharPoolCount
    if poolInfo.upGotCount > 0 or currProgress >= maxProgress then
        return
    end

    local showRatingPopup = false

    local gotCharIds = msg.OriResultIds
    local upCharIds = poolData.upCharIds
    for i = 0, gotCharIds.Count - 1 do
        local gotCharId = gotCharIds[i]

        for j = 0, upCharIds.Count - 1 do
            if gotCharId == upCharIds[j] then
                showRatingPopup = true
                break
            end
        end

        currProgress = currProgress + 1
        if showRatingPopup or currProgress > maxProgress then
            break
        end
    end

    if showRatingPopup then
        self:_ShowRatingPopup(RatingPopupReason.GachaCharPool, poolId)
    end
end

AppStoreSystem._OnUseItem = HL.Method(HL.Table) << function(self, args)


    if not self:_CanShowRatingPopup() then
        return
    end

    local showRatingPopup = false

    local itemId = unpack(args)
    local targetItemIds = Tables.globalConst.appStoreRatingConditionUseItemIds
    for i = 0, targetItemIds.Count - 1 do
        if itemId == targetItemIds[i] then
            showRatingPopup = true
            break
        end
    end

    if showRatingPopup then
        self:_ShowRatingPopup(RatingPopupReason.UseItem, itemId)
    end
end

AppStoreSystem._OnMissionStateChange = HL.Method(HL.Table) << function(self, args)


    if not self:_CanShowRatingPopup() then
        return
    end

    local missionId, missionState = unpack(args)
    if missionState ~= MissionState.Completed then
        return
    end

    local showRatingPopup = false

    local targetMissionIds = Tables.globalConst.appStoreRatingConditionCompleteMissionIds
    for i = 0, targetMissionIds.Count - 1 do
        if missionId == targetMissionIds[i] then
            showRatingPopup = true
        end
    end

    if showRatingPopup then
        self:_ShowRatingPopup(RatingPopupReason.CompleteMission, missionId)
    end
end

HL.Commit(AppStoreSystem)
return AppStoreSystem
