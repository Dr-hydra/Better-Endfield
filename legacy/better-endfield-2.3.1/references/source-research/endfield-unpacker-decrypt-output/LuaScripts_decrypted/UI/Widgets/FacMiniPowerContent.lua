local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacMiniPowerContent = HL.Class('FacMiniPowerContent', UIWidgetBase)

local XIRANITE_BUILDINGID = "xiranite_oven_1"
local BUILDING_ICON_MAP = {
    [GEnums.FacBuildingType.TravelPole] = "building_icon_travel_pole",
    [GEnums.FacBuildingType.Battle] = "building_icon_battle",
    [GEnums.FacBuildingType.Sign] = "building_icon_marker",
    [GEnums.FacBuildingType.BusFree] = "building_icon_bus_basaband",
    [GEnums.FacBuildingType.BusStart] = "building_icon_bus_sourcepillar",
    [GEnums.FacBuildingType.MachineCrafter] = "building_icon_xirong_machine",
    [GEnums.FacBuildingType.Decorate] = "building_icon_decorate",
}
local USE_DURATION = Tables.factoryConst.facBackUpPowerDuration
local RECOVERY_DURATION = Tables.factoryConst.facBackUpPowerCooldownTime

FacMiniPowerContent.isMonitorPower = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_chapterId = HL.Field(HL.Number) << -1

FacMiniPowerContent.m_powerInfo = HL.Field(HL.Userdata)

FacMiniPowerContent.m_buildingData = HL.Field(Cfg.Types.FactoryBuildingData)

FacMiniPowerContent.m_refreshCoroutine = HL.Field(HL.Thread)

FacMiniPowerContent.m_lastIsConsuming = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_lastPowerSavePercent = HL.Field(HL.Number) << -1

FacMiniPowerContent.m_lastPowerEnoughAnimName = HL.Field(HL.String) << ""

FacMiniPowerContent.m_lastPowerSaveAnimName = HL.Field(HL.String) << ""

FacMiniPowerContent.m_lackOfPowerAudioBaseTime = HL.Field(HL.Number) << -1

FacMiniPowerContent.m_isDirty = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_powerData = HL.Field(HL.Table)

FacMiniPowerContent.m_isSaveMaxBecameValid = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_showToastTimer = HL.Field(HL.Number) << -1

FacMiniPowerContent.shouldShowBackupPower = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_canShowBackupPower = HL.Field(HL.Boolean) << false

FacMiniPowerContent.m_backupUseEndTs = HL.Field(HL.Number) << -1

FacMiniPowerContent.m_backupRecoveryEndTs = HL.Field(HL.Number) << -1

FacMiniPowerContent._OnCreate = HL.Override() << function(self)
end

FacMiniPowerContent._OnEnable = HL.Override() << function(self)
    self:RefreshFacMiniPowerContent()
    self:_RefreshAnimState()
    self:ToggleCoroutine(true)
end

FacMiniPowerContent._OnDisable = HL.Override() << function(self)
    self:ToggleCoroutine(false)
end

FacMiniPowerContent._OnDestroy = HL.Override() << function(self)
    self:ToggleCoroutine(false)
    self:_ClearShowToastTimer()
    self.m_powerInfo = nil
end

FacMiniPowerContent.ToggleCoroutine = HL.Method(HL.Boolean) << function(self, active)
    if active then
        if not self.m_refreshCoroutine then
            self.m_refreshCoroutine = self:_StartCoroutine(function()
                while true do
                    coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
                    self:_UpdateAndRefreshAll(false)
                end
            end)
        end
    else
        self.m_refreshCoroutine = self:_ClearCoroutine(self.m_refreshCoroutine)
    end
end



FacMiniPowerContent._OnFirstTimeInit = HL.Override() << function(self)
end

FacMiniPowerContent._UpdateAndRefreshAll = HL.Method(HL.Boolean) << function(self, forceRefresh)
    self:_UpdateMiniPowerData()
    if forceRefresh or self.m_isDirty then
        self:RefreshFacMiniPowerContent()
        self:UpdateLackOfPowerAudio()
        self.m_isDirty = false
    end

    self:_UpdateBackupPowerState()
end

FacMiniPowerContent._ClearShowToastTimer = HL.Method() << function(self)
    if self.m_showToastTimer < 0 then
        return
    end
    self.m_showToastTimer = self:_ClearTimer(self.m_showToastTimer)
end

FacMiniPowerContent._GetPowerInfo = HL.Method().Return(HL.Opt(HL.Userdata)) << function(self)
    if self.m_chapterId < 0 then
        return FactoryUtils.getCurRegionPowerInfo()
    else
        return FactoryUtils.getRegionPowerInfoByChapterId(self.m_chapterId)
    end
end

FacMiniPowerContent._UpdateMiniPowerData = HL.Method() << function(self)
    if not self.m_powerInfo then
        self.m_powerData = {}
        self.m_powerInfo = self:_GetPowerInfo()
    end

    if self.m_powerInfo ~= nil then
        local powerInfo = self.m_powerInfo


        if self.m_powerData.powerCost == nil or self.m_powerData.powerCost ~= powerInfo.powerCost then
            self.m_powerData.powerCost = powerInfo.powerCost
            self.m_isDirty = true
        end


        if self.m_powerData.powerGen == nil or self.m_powerData.powerGen ~= powerInfo.powerGen then
            self.m_powerData.powerGen = powerInfo.powerGen
            self.m_isDirty = true
        end


        if self.m_powerData.powerSaveCurrent == nil or self.m_powerData.powerSaveCurrent ~= powerInfo.powerSaveCurrent then
            self.m_powerData.powerSaveCurrent = powerInfo.powerSaveCurrent
            self.m_isDirty = true
        end


        if self.m_powerData.powerSaveMax == nil or self.m_powerData.powerSaveMax ~= powerInfo.powerSaveMax then
            if self.m_powerData.powerSaveMax ~= nil and self.m_powerData.powerSaveMax <= 0 and powerInfo.powerSaveMax > 0 then
                self.m_isSaveMaxBecameValid = true
            end
            self.m_powerData.powerSaveMax = powerInfo.powerSaveMax
            self.m_isDirty = true
        end


        if self.m_powerData.backupPowerLastStartTs == nil or self.m_powerData.backupPowerLastStartTs ~= powerInfo.backupLastStartTs then
            self.m_powerData.backupPowerLastStartTs = powerInfo.backupLastStartTs
            self.m_isDirty = true
        end
        if self.m_powerData.backupLastStopTs == nil or self.m_powerData.backupLastStopTs ~= powerInfo.backupLastStopTs then
            self.m_powerData.backupLastStopTs = powerInfo.backupLastStopTs
            self.m_isDirty = true
        end
    end
end

FacMiniPowerContent._RefreshContent = HL.Method() << function(self)
    if not self.m_powerInfo then
        return
    end
    local data = self.m_powerData
    local node = self.view
    local isMoving = FactoryUtils.isMovingBuilding()

    local powerCost = data.powerCost + ((self.m_buildingData and not isMoving) and self.m_buildingData.powerConsume or 0)
    local powerGen = data.powerGen
    node.costPowerTxt.text = UIUtils.getNumString(powerCost)
    node.genPowerTxt.text = string.format("/%s", UIUtils.getNumString(powerGen))
    local isConsuming = powerGen < powerCost
    node.costPowerTxt.color = self.m_buildingData and self.view.config.COUNT_ENOUGH_COLOR or self.view.config.COUNT_NORMAL_COLOR

    if self.m_buildingData then
        node.costPowerTxt.color = isConsuming and self.view.config.COUNT_NOT_ENOUGH_COLOR or self.view.config.COUNT_ENOUGH_COLOR
        return
    else
        node.costPowerTxt.color = self.view.config.COUNT_NORMAL_COLOR
    end

    local restPower = data.powerSaveCurrent
    local restPercent = restPower / (data.powerSaveMax > 0 and data.powerSaveMax or 1)
    local emptyRestPower = restPercent <= self.view.config.POWER_SAVE_EMPTY_VALUE
    node.powerSavePercentTxt.text = math.floor(restPercent * 100)
    node.powerSaveBar.fillAmount = restPercent
    local powerSaveColor
    if emptyRestPower then
        powerSaveColor = self.view.config.NO_POWER_COLOR
    else
        powerSaveColor = restPercent <= self.view.config.POWER_SAVE_OUT_VALUE and self.view.config.COST_SAVE_POWER_COLOR or self.view.config.NORMAL_COLOR
    end
    node.powerSaveBar.color = powerSaveColor
    node.powerSaveIcon.color = powerSaveColor

    local powerEnoughColor = isConsuming and self.view.config.NO_POWER_COLOR or self.view.config.COUNT_NORMAL_COLOR
    node.powerEnoughImg.color = powerEnoughColor
    node.costPowerTxt.color = powerEnoughColor

    if isConsuming then
        if self.m_lastPowerEnoughAnimName ~= self.view.config.POWER_ENOUGH_LOOP_ANIM_NAME then
            self.m_lastPowerEnoughAnimName = self.view.config.POWER_ENOUGH_LOOP_ANIM_NAME
            node.powerEnoughAnim:PlayWithTween(self.m_lastPowerEnoughAnimName)
            self.m_lackOfPowerAudioBaseTime = CS.UnityEngine.Time.unscaledTime
        end
    else
        if self.m_lastPowerEnoughAnimName ~= self.view.config.POWER_ENOUGH_NORMAL_ANIM_NAME then
            self.m_lastPowerEnoughAnimName = self.view.config.POWER_ENOUGH_NORMAL_ANIM_NAME
            node.powerEnoughAnim:PlayWithTween(self.m_lastPowerEnoughAnimName)
            self.m_lackOfPowerAudioBaseTime = -1
        end
    end

    local powerSaveOutValue = self.view.config.POWER_SAVE_OUT_VALUE
    if restPercent <= powerSaveOutValue then
        if self.m_lastPowerSaveAnimName ~= self.view.config.POWER_SAVE_LOOP_ANIM_NAME then
            self.m_lastPowerSaveAnimName = self.view.config.POWER_SAVE_LOOP_ANIM_NAME
            node.powerSaveAnim:PlayWithTween(self.m_lastPowerSaveAnimName)
        end
    else
        if self.m_lastPowerSaveAnimName ~= self.view.config.POWER_SAVE_NORMAL_ANIM_NAME then
            self.m_lastPowerSaveAnimName = self.view.config.POWER_SAVE_NORMAL_ANIM_NAME
            node.powerSaveAnim:PlayWithTween(self.m_lastPowerSaveAnimName)
        end
    end


    if self.shouldShowBackupPower then
        local chapterId = self.m_chapterId < 0 and Utils.getCurrentChapterId() or self.m_chapterId
        local reserveNode = node.reserveNode
        local lastStartUseTs = data.backupPowerLastStartTs
        local currentTimeTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        self.m_backupUseEndTs = lastStartUseTs + USE_DURATION
        self.m_backupRecoveryEndTs = self.m_backupUseEndTs + RECOVERY_DURATION

        local inUse = currentTimeTs < self.m_backupUseEndTs

        local canShowPre = self.m_canShowBackupPower
        local canShowNow = (inUse or (isConsuming and restPower == 0)) and not Utils.isInBlackbox()
        self.m_canShowBackupPower = canShowNow



        if not canShowPre and canShowNow then
            reserveNode.button.onClick:RemoveAllListeners()
            reserveNode.button.onClick:AddListener(function()
                PhaseManager:OpenPhase(PhaseId.FacReservePowerPopup, {
                    chapterId = chapterId,
                    useEndTime = self.m_backupUseEndTs,
                    recoveryEndTime = self.m_backupRecoveryEndTs,
                    isRealPowerEnough = not isConsuming,
                    isAutoUse = GameInstance.player.remoteFactory.core.IsBackPowerAutoStartupEnabled,
                })
            end)
        end

        if canShowPre and not canShowNow then
            self:_HideBackupPower()
        end
    end

end

FacMiniPowerContent.ToggleBackupPowerShow = HL.Method(HL.Boolean) << function(self, shouldShow)
    self.shouldShowBackupPower = shouldShow
    if not shouldShow then
        self:_HideBackupPower()
    else
        self.m_isDirty = true
    end
end

FacMiniPowerContent._HideBackupPower = HL.Method() << function(self)
    local reserveNode = self.view.reserveNode
    reserveNode.stateController:SetState("Hide")
    reserveNode.gameObject:SetActive(false)
end

FacMiniPowerContent._UpdateBackupPowerState = HL.Method() << function(self)
    if not (self.shouldShowBackupPower and self.m_canShowBackupPower) then
        return
    end

    local reserveNode = self.view.reserveNode
    local currentTimeTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    local inUse = currentTimeTs < self.m_backupUseEndTs
    local underRecovery = not inUse and currentTimeTs < self.m_backupRecoveryEndTs
    local isAvailable = not inUse and not underRecovery
    local lastState = reserveNode.stateController.currentStateName
    reserveNode.stateController:SetState(inUse and "InUse" or (underRecovery and "UnderRecovery" or "Available"))
    local currentState = reserveNode.stateController.currentStateName
    local chapterId = self.m_chapterId < 0 and Utils.getCurrentChapterId() or self.m_chapterId

    local domainId = ScopeUtil.ChapterIdInt2Str(chapterId)
    local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    local domainName = domainCfg.domainName


    if not reserveNode.gameObject.activeSelf or lastState ~= currentState then
        local isPopup = not reserveNode.gameObject.activeSelf
        reserveNode.gameObject:SetActive(true)
        self.view.reserveNode.redDot:InitRedDot("FacBackupPower", {
            chapterId = chapterId,
        })
        CS.Beyond.Gameplay.Conditions.OnFacShowBackupPower.Trigger()

        if not isPopup then
            if lastState == "InUse" and currentState == "UnderRecovery" then
                reserveNode.animationWrapper:Play(self.view.config.BACKUP_CHANGE_FROM_USE_TO_RECOVERY)
                AudioAdapter.PostEvent("Au_UI_Event_TempowerSupplyShortage")
            elseif lastState == "UnderRecovery" and currentState ~= "UnderRecovery" then
                reserveNode.animationWrapper:Play(self.view.config.BACKUP_CHANGE_FROM_RECOVERY_TO_AVAILABLE)
            end
        end

        if not underRecovery then
            reserveNode.animationWrapper:Play(self.view.config.BACKUP_COMMON_IN_USE)
        end

        if isAvailable then
            reserveNode.fgBar.fillAmount = 1
            reserveNode.iconAnimationWrapper:Play(self.view.config.BACKUP_AVAILABLE)
            return
        end
        reserveNode.iconAnimationWrapper:Play(self.view.config.BACKUP_DEFAULT)

        if inUse then
            reserveNode.lineAnimationWrapper:Play(self.view.config.BACKUP_IN_USE)
        end
    end


    local leftTime = 0
    if inUse then
        leftTime = self.m_backupUseEndTs - currentTimeTs
        reserveNode.fgBar.fillAmount = leftTime / USE_DURATION
    elseif underRecovery then
        leftTime = self.m_backupRecoveryEndTs - currentTimeTs
        reserveNode.fgBar.fillAmount = (RECOVERY_DURATION - leftTime) / RECOVERY_DURATION
    end

    reserveNode.timeTxt:SetAndResolveTextStyle(leftTime < 3600 and UIUtils.getLeftTimeToSecond(leftTime) or UIUtils.getShortLeftTime(leftTime))

end

FacMiniPowerContent._MonitorPower = HL.Method() << function(self)
    local data = self.m_powerData
    if data.powerSaveMax <= 0 then
        return
    end

    local powerCost = data.powerCost
    local powerGen = data.powerGen
    local isConsuming = powerGen < powerCost
    local restPower = data.powerSaveCurrent
    local restPercent = restPower / (data.powerSaveMax > 0 and data.powerSaveMax or 1)
    local powerSaveOutValue = self.view.config.POWER_SAVE_OUT_VALUE
    local powerSaveEmptyValue = self.view.config.POWER_SAVE_EMPTY_VALUE

    local showText
    if self.m_lastPowerSavePercent < 0 then
        if not self.m_isSaveMaxBecameValid then

            if restPercent <= powerSaveEmptyValue then
                showText = Language.LUA_FAC_POWER_SAVE_EMPTY
            else
                if restPercent <= powerSaveOutValue then
                    showText = Language.LUA_FAC_POWER_SAVE_OUT
                else
                    if isConsuming then
                        showText = Language.LUA_FAC_POWER_CONSUMING
                    end
                end
            end
        else
            self.m_isSaveMaxBecameValid = false
        end
    else
        if isConsuming and not self.m_lastIsConsuming then
            showText = Language.LUA_FAC_POWER_CONSUMING
        end
        if restPercent <= powerSaveOutValue and self.m_lastPowerSavePercent > powerSaveOutValue then
            showText = Language.LUA_FAC_POWER_SAVE_OUT
        end
        if restPercent <= powerSaveEmptyValue and self.m_lastPowerSavePercent > powerSaveEmptyValue then
            showText = Language.LUA_FAC_POWER_SAVE_EMPTY
        end
    end

    if not string.isEmpty(showText) then
        if not Utils.isInDungeon() and not Utils.isForbidden(ForbidType.FacPowerToast) and self.m_showToastTimer < 0 then

            Notify(MessageConst.SHOW_SPECIAL_TOAST, showText)
            self.m_showToastTimer = self:_StartTimer(self.view.config.WARNING_TOAST_INTERVAL, function()
                self:_ClearShowToastTimer()
            end)
        end
    end

    self.m_lastIsConsuming = isConsuming
    self.m_lastPowerSavePercent = restPercent
end

FacMiniPowerContent._RefreshAnimState = HL.Method() << function(self)
    if not string.isEmpty(self.m_lastPowerEnoughAnimName) then
        self.view.powerEnoughAnim:PlayWithTween(self.m_lastPowerEnoughAnimName)
    end
    if not string.isEmpty(self.m_lastPowerSaveAnimName) then
        self.view.powerSaveAnim:PlayWithTween(self.m_lastPowerSaveAnimName)
    end
end

FacMiniPowerContent.InitFacMiniPowerContent = HL.Method(HL.Opt(HL.Number)) << function(self, chapterId)
    self.m_chapterId = chapterId ~= nil and chapterId or -1
    self.m_powerInfo = nil

    self.view.powerNode.gameObject:SetActive(true)
    self.view.powerSaveNode.gameObject:SetActive(true)
    self.view.bandwidthNode.gameObject:SetActive(false)
    self.view.buildingCountNode.gameObject:SetActive(false)
    self:ToggleBackupPowerShow(self.shouldShowBackupPower)

    self:_UpdateAndRefreshAll(true)

    self:_FirstTimeInit()
end

FacMiniPowerContent.SwitchFacMiniPowerContent = HL.Method(HL.String) << function(self, buildingItemId)
    local data = FactoryUtils.getItemBuildingData(buildingItemId)
    self.m_buildingData = data

    local inBuildingMode = FactoryUtils.isInBuildMode()
    local node = self.view

    node.powerSaveNode.gameObject:SetActive(not inBuildingMode)
    if not inBuildingMode or not data then
        node.powerNode.gameObject:SetActive(true)
        node.buildingCountNode.gameObject:SetActive(false)
        node.bandwidthNode.gameObject:SetActive(false)
        self:RefreshFacMiniPowerContent()
        self:_RefreshAnimState()
        if self.view.reserveNode.gameObject.activeSelf then
            CS.Beyond.Gameplay.Conditions.OnFacShowBackupPower.Trigger()
        end
        return
    end

    local sceneMsg = FactoryUtils.getCurSceneHandler()
    local buildingMode = GameInstance.remoteFactoryManager.interact.currentBuildingMode
    local inFacMain = false
    local bandwidth = nil
    if buildingMode then
        inFacMain = buildingMode.inMainRegion
        local settlementId = buildingMode.settlementId
        if settlementId ~= nil then
            if settlementId == "" then
                bandwidth = buildingMode.bandwidth
            else
                bandwidth = sceneMsg:GetSettlementBandwidth(settlementId)
            end
        end
    end

    local buildingCountNodeActive = false
    local max = 0
    local cur = 0
    local delta = 0
    if data.type == GEnums.FacBuildingType.TravelPole and not inFacMain then
        buildingCountNodeActive = true
        if bandwidth then
            max = bandwidth.travelPoleMax
            cur = bandwidth.travelPoleCurrent
        end
        if buildingMode then
            delta = buildingMode.deltaTravelPoleCount
        end
    elseif data.type == GEnums.FacBuildingType.Battle and not inFacMain then
        buildingCountNodeActive = true
        if bandwidth then
            max = bandwidth.battleMax
            cur = bandwidth.battleCurrent
        end
        if buildingMode then
            delta = buildingMode.deltaBattleCount
        end
    elseif data.type == GEnums.FacBuildingType.Sign and not inFacMain then
        buildingCountNodeActive = true
        max = Tables.factoryConst.signNodeCountLimit
        cur = FactoryUtils.getPlayerAllMarkerBuildingNodeInfo()
        if buildingMode then
            delta = buildingMode.isMoving and 0 or 1
        else
            delta = 0
        end

    elseif data.type == GEnums.FacBuildingType.BusFree then
        buildingCountNodeActive = true
        local bus, source = GameInstance.remoteFactoryManager:GetFreeBusLimitsInfoInCoreZone()
        max = bus
        cur = GameInstance.remoteFactoryManager:GetBuildingCountInCurCoreZone(data.id)
        if buildingMode then
            delta = buildingMode.isMoving and 0 or 1
        else
            delta = 0
        end
    elseif data.type == GEnums.FacBuildingType.BusStart then
        buildingCountNodeActive = true
        local bus, source = GameInstance.remoteFactoryManager:GetFreeBusLimitsInfoInCoreZone()
        max = source
        cur = GameInstance.remoteFactoryManager:GetBuildingCountInCurCoreZone(data.id)
        if buildingMode then
            delta = buildingMode.isMoving and 0 or 1
        else
            delta = 0
        end
    elseif data.type == GEnums.FacBuildingType.Decorate then
        buildingCountNodeActive = true
        max = GameInstance.remoteFactoryManager.currentChapterInfo.data.domainDecorateCount
        cur = buildingMode.currentDecorateCount
        delta = buildingMode.isMoving and 0 or 1
    elseif data.type == GEnums.FacBuildingType.MachineCrafter and data.id == XIRANITE_BUILDINGID then
        max = GameInstance.remoteFactoryManager:GetBuildingLimitsInChapter(data.id)
        if max < 0 then
            local domainId = FactoryUtils.getCurAndAutoTransferBlackBoxToDomainId()
            for i = 0, data.placeDomains.Count - 1 do
                if data.placeDomains[i] == domainId and data.placeDomainLimitCnt.Count > i then
                    max = data.placeDomainLimitCnt[i]
                end
            end
        end
        if max > 0 then
            buildingCountNodeActive = true
            cur = GameInstance.remoteFactoryManager:GetBuildingCountInCurMap(data.id)
            if buildingMode then
                delta = buildingMode.isMoving and 0 or 1
            else
                delta = 0
            end
        end
    end
    local newCur = cur + delta
    node.curBuildingCountTxt.text = newCur
    node.curBuildingCountTxt.color = newCur > max and self.view.config.COUNT_NOT_ENOUGH_COLOR or self.view.config.COUNT_ENOUGH_COLOR
    node.maxBuildingCountTxt.text = string.format("/%d", max)
    if buildingCountNodeActive then
        self.view.buildingIcon:LoadSprite(UIConst.UI_SPRITE_MINI_POWER, BUILDING_ICON_MAP[data.type])
    end
    node.buildingCountNode.gameObject:SetActive(buildingCountNodeActive)

    if not inFacMain and data.bandwidth > 0 then
        node.bandwidthNode.gameObject:SetActive(true)
        local max = 0
        local cur = 0
        local delta = 0
        if bandwidth then
            max = bandwidth.max
            cur = bandwidth.current
        end
        if buildingMode then
            delta = buildingMode.deltaBandwidth
        end
        local new = cur + delta
        node.useBandwidthTxt.text = new
        node.useBandwidthTxt.color = new > max and self.view.config.COUNT_NOT_ENOUGH_COLOR or self.view.config.COUNT_ENOUGH_COLOR
        node.maxBandwidthTxt.text = string.format("/%d", max)
    else
        node.bandwidthNode.gameObject:SetActive(false)
    end

    node.powerNode.gameObject:SetActive(data.powerConsume > 0 and not GameInstance.remoteFactoryManager.system.core.progressStatus:IsBuildingPowerCostNoNeed(data.id))
    self:RefreshFacMiniPowerContent()
    self:_RefreshAnimState()
end

FacMiniPowerContent.RefreshFacMiniPowerContent = HL.Method() << function(self)
    if not self.m_powerData or not self.m_powerInfo then
        return
    end

    if self.view.gameObject.activeSelf then
        self:_RefreshContent()
    end

    if self.isMonitorPower then
        self:_MonitorPower()
    end
end
FacMiniPowerContent.ClearMemorizedPowerInfo = HL.Method() << function(self)
    self.m_powerInfo = nil
end

FacMiniPowerContent.UpdateLackOfPowerAudio = HL.Method() << function(self)
    if self.m_lackOfPowerAudioBaseTime < -0.01 then
        return
    end
    local animLength = self.view.powerEnoughAnim:GetClipLength(self.m_lastPowerEnoughAnimName)
    if animLength == 0 then
        return
    end
    local curTime = CS.UnityEngine.Time.unscaledTime
    local elapseTime = curTime - self.m_lackOfPowerAudioBaseTime
    local normalizedElapseTime = elapseTime / animLength
    if normalizedElapseTime > 1 then
        if self.gameObject and self.gameObject.activeInHierarchy and self.m_powerInfo and GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegion() then
            AudioAdapter.PostEvent(self.m_powerInfo.powerSaveCurrent == 0 and "Au_UI_HUD_PowerShortage_NoPower" or "Au_UI_HUD_PowerShortage_LowPower")
        end
    end
    self.m_lackOfPowerAudioBaseTime = self.m_lackOfPowerAudioBaseTime + math.floor(normalizedElapseTime) * animLength
end

HL.Commit(FacMiniPowerContent)
return FacMiniPowerContent
