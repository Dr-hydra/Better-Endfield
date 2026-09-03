local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WirelessModeNode = HL.Class('WirelessModeNode', UIWidgetBase)

local INVALID_PERCENT_VALUE = -1

WirelessModeNode.m_buildingInfo = HL.Field(HL.Userdata)

WirelessModeNode.m_wirelessModeContent = HL.Field(HL.Table)

WirelessModeNode.m_contentUpdateThread = HL.Field(HL.Thread)

WirelessModeNode.m_wirelessPercent = HL.Field(HL.Number) << 0

WirelessModeNode.m_isPaused = HL.Field(HL.Boolean) << false

WirelessModeNode.m_isBlocked = HL.Field(HL.Boolean) << false

WirelessModeNode.m_onComplete = HL.Field(HL.Function)


WirelessModeNode._OnFirstTimeInit = HL.Override() << function(self)
end

WirelessModeNode._OnEnable = HL.Override() << function(self)
    self:_RestartWirelessUpdateThread()
end

WirelessModeNode._OnDisable = HL.Override() << function(self)
    self:_ClearWirelessUpdateThread()
end

WirelessModeNode._OnDestroy = HL.Override() << function(self)
    self:_ClearWirelessUpdateThread()
end

WirelessModeNode.InitWirelessModeNode = HL.Method(HL.Userdata, HL.Opt(HL.Function)) << function(self, buildingInfo, onComplete)
    self:_FirstTimeInit()

    if buildingInfo == nil then
        return
    end

    self.m_buildingInfo = buildingInfo
    self.m_onComplete = onComplete ~= nil and onComplete or function()end
    self.m_wirelessModeContent = self.view.wirelessModeContent

    self:_InitWirelessMode()
end

WirelessModeNode._InitWirelessMode = HL.Method() << function(self)
    local isWirelessMode = self.m_buildingInfo.cacheTransport.inUse
    self.view.wirelessModeToggle.isOn = isWirelessMode
    self.view.wirelessModeToggle.onValueChanged:AddListener(function(isOn)
        self:_ChangeWirelessMode(isOn)
    end)
    self.view.wirelessModeContent.gameObject:SetActiveIfNecessary(isWirelessMode)
    self:_RefreshDomainInfo()
    self:_RestartWirelessUpdateThread()
end

WirelessModeNode._ChangeWirelessMode = HL.Method(HL.Boolean) << function(self, isWirelessMode)
    self.m_buildingInfo.sender:Message_OpCacheTransportEnable(Utils.getCurrentChapterId(), self.m_buildingInfo.cacheTransport.componentId, isWirelessMode, function()
        if not NotNull(self.view.gameObject) then
            return
        end

        self.m_buildingInfo:Update()
        if self.m_buildingInfo.cacheTransport.inUse then
            self:_StartContentUpdateThread()
        else
            self:_ClearWirelessUpdateThread()
        end

        self:_RefreshModeDisplayAfterChanged(self.m_buildingInfo.cacheTransport.inUse)
    end)
end

WirelessModeNode._RefreshModeDisplayAfterChanged = HL.Method(HL.Boolean) << function(self, isWirelessMode)
    self.view.wirelessModeContent.gameObject:SetActiveIfNecessary(isWirelessMode)

    local toggleAnim = isWirelessMode and "belttoggle_right" or "belttoggle_left"
    self.view.wirelessModeToggleAnimationWrapper:PlayWithTween(toggleAnim)

    local contentAnim = isWirelessMode and "fac_wireless_complete_in" or "fac_wireless_complete_out"
    self.view.wirelessModeContent.animationWrapper:PlayWithTween(contentAnim, function()
        self.view.wirelessModeContent.animationWrapper:PlayWithTween("fac_wireless_doneloop")
    end)

    local iconAnim = self.m_isPaused and "facminerwireless_defalut" or "facminerwireless_loop"
    self.m_wirelessModeContent.iconAnimationWrapper:PlayWithTween(iconAnim)
end



WirelessModeNode._StartContentUpdateThread = HL.Method() << function(self)
    self.m_wirelessPercent = INVALID_PERCENT_VALUE
    self:_UpdateWirelessModeContent(true)

    self.m_contentUpdateThread = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_UpdateWirelessModeContent()
        end
    end)
end

WirelessModeNode._RestartWirelessUpdateThread = HL.Method() << function(self)
    if self.m_buildingInfo == nil then
        return
    end

    if not self.m_buildingInfo.cacheTransport.inUse then
        return
    end

    if self.m_contentUpdateThread ~= nil then
        return
    end

    self:_StartContentUpdateThread()
end

WirelessModeNode._ClearWirelessUpdateThread = HL.Method() << function(self)
    if self.m_contentUpdateThread ~= nil then
        self.m_contentUpdateThread = self:_ClearCoroutine(self.m_contentUpdateThread)
    end
end






WirelessModeNode._UpdateWirelessModeContent = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceUpdate)
    if self.m_isPaused and not forceUpdate then
        return
    end

    local cacheTransport = self.m_buildingInfo.cacheTransport

    if cacheTransport.inUse then
        local cdTime = 0
        local currentProgress, totalProgress = cacheTransport.currentProgress, cacheTransport.totalProgress
        if cacheTransport.progressIncreaseMS > 0 then
            cdTime = (totalProgress - currentProgress) / cacheTransport.progressIncreaseMS / 1000
        end
        local percent = currentProgress / totalProgress

        if self.m_wirelessPercent == INVALID_PERCENT_VALUE then
            self.m_wirelessPercent = percent
        end
        if percent < self.m_wirelessPercent then
            self:_SwitchCompletedState(function()
                self:_SwitchNormalState()
            end)
        end
        self.m_wirelessPercent = percent

        self:_RefreshTimeInfo(cdTime, percent)
    end
end

WirelessModeNode._RefreshLineColor = HL.Method(HL.Any) << function(self, color)
    self.m_wirelessModeContent.lineGroup.color = color
    self.m_wirelessModeContent.timeText.color = color
    self.m_wirelessModeContent.fillBG.color = color
    self.m_wirelessModeContent.bgDeco.color = color
    self.m_wirelessModeContent.infoTextGroup.color = color
end

WirelessModeNode._SwitchNormalState = HL.Method() << function(self)
    self:_RefreshLineColor(self.config.COLOR_LINE_NORMAL)
    self.m_wirelessModeContent.infoText.text = Language.LUA_FAC_MINER_WIRELESS_MODE_NORMAL
    self.m_wirelessModeContent.iconAnimationWrapper:PlayWithTween("facminerwireless_loop")
end

WirelessModeNode._SwitchPausedState = HL.Method() << function(self)
    self:_RefreshLineColor(self.config.COLOR_LINE_PAUSED)
    self.m_wirelessModeContent.infoText.text = Language.LUA_FAC_MINER_WIRELESS_MODE_PAUSED
    self.m_wirelessModeContent.iconAnimationWrapper:PlayWithTween("facminerwireless_defalut")
    self.m_wirelessPercent = INVALID_PERCENT_VALUE
end

WirelessModeNode._SwitchCompletedState = HL.Method(HL.Opt(HL.Function)) << function(self, animationCallback)
    if not self.m_isBlocked then
        self:_RefreshLineColor(self.config.COLOR_LINE_COMPLETED)
        self.m_wirelessModeContent.infoText.text = Language.LUA_FAC_MINER_WIRELESS_MODE_COMPLETED
        self.m_wirelessModeContent.animationWrapper:PlayWithTween("fac_wireless_done", function()
            self.m_wirelessModeContent.animationWrapper:PlayWithTween("fac_wireless_doneloop")
            if animationCallback ~= nil then
                animationCallback()
            end
        end)
    end

    if self.m_onComplete ~= nil then
        self.m_onComplete()
    end
end

WirelessModeNode._RefreshTimeInfo = HL.Method(HL.Number, HL.Number) << function(self, timeRemain, fillPercent)
    if fillPercent <= 0 then
        timeRemain = 0
    end
    local min = math.floor(timeRemain / 60)
    local second = math.floor(timeRemain % 60)
    self.m_wirelessModeContent.timeText.text = string.format("%02d:%02d", min, second)
    self.m_wirelessModeContent.fillBG.fillAmount = fillPercent
end

WirelessModeNode._RefreshDomainInfo = HL.Method() << function(self)
    self.m_wirelessModeContent.titleTxt.gameObject:SetActive(false)
    local curLevelId = GameWorld.worldInfo.curLevelId
    local levelSuccess, levelInfo = DataManager.levelBasicInfoTable:TryGetValue(curLevelId)
    if not levelSuccess then
        return
    end

    local domainId = levelInfo.domainName
    local domainSuccess, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if not domainSuccess then
        return
    end

    self.m_wirelessModeContent.titleTxt.text = domainData.storageName
    self.m_wirelessModeContent.titleTxt.gameObject:SetActive(true)
end





WirelessModeNode.RefreshPausedState = HL.Method(HL.Boolean) << function(self, isPaused)
    if not isPaused then
        self:_SwitchNormalState()
    else
        self:_SwitchPausedState()
    end
    self.m_isPaused = isPaused
end

WirelessModeNode.RefreshBlockedState = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isBlocked, forceRefresh)
    if isBlocked == self.m_isBlocked and not forceRefresh then
        return
    end

    self:_RefreshLineColor(self.config.COLOR_LINE_NORMAL)
    self.m_wirelessModeContent.normalNode.gameObject:SetActiveIfNecessary(not isBlocked)
    self.m_wirelessModeContent.blockedNode.gameObject:SetActiveIfNecessary(isBlocked)
    self.m_wirelessModeContent.leftNode.gameObject:SetActiveIfNecessary(not isBlocked)
    if self.m_isBlocked ~= isBlocked then
        local animName = isBlocked and "fac_wireless_blockednode_in" or "fac_wireless_blockednode_out"
        self.view.wirelessModeBlockAnimationWrapper:PlayWithTween(animName)
    end
    self.m_isBlocked = isBlocked
end

WirelessModeNode.RefreshSwitchValidState = HL.Method(HL.Boolean) << function(self, isValid)
    self.view.wirelessModeToggle.interactable = isValid
    self.view.invalidIcon.gameObject:SetActive(not isValid)
end





HL.Commit(WirelessModeNode)
return WirelessModeNode

