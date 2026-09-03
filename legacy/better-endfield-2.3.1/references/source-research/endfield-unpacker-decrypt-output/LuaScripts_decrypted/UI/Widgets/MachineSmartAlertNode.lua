local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MachineSmartAlertNode = HL.Class('MachineSmartAlertNode', UIWidgetBase)

MachineSmartAlertNode.m_SmartAlertStableTime = HL.Field(HL.Number) << 0

MachineSmartAlertNode.m_curCondition = HL.Field(GEnums.FacSmartAlertType)

MachineSmartAlertNode.m_cacheCondition = HL.Field(GEnums.FacSmartAlertType)

MachineSmartAlertNode.m_curCheck = HL.Field(HL.Any) << nil

MachineSmartAlertNode.m_curDefaultOpen = HL.Field(HL.Boolean) << false

MachineSmartAlertNode.m_targetTransform = HL.Field(HL.Userdata)

MachineSmartAlertNode.m_showDetailAnimState = HL.Field(HL.Boolean) << false

MachineSmartAlertNode.m_activeAnimState = HL.Field(HL.Boolean) << false

MachineSmartAlertNode.m_playingAnimation = HL.Field(HL.Boolean) << false

MachineSmartAlertNode.m_animationPostfix = HL.Field(HL.String) << ""

MachineSmartAlertNode.m_buildingData = HL.Field(HL.Table)


MachineSmartAlertNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.effectBtn.onClick:AddListener(function()
        if self.m_playingAnimation then
            return
        end
        self:_ShowDetailInfo(true)
    end)
    self.view.maskBtn.onClick:AddListener(function()
        if self.m_playingAnimation then
            return
        end
        self:_ShowDetailInfo(false)
    end)
end

MachineSmartAlertNode.InitMachineSmartAlertNode = HL.Method(HL.String, HL.Number) << function(self, buildingId, nodeId)
    self:_FirstTimeInit()

    self.m_curCondition = GEnums.FacSmartAlertType.DoNotShow
    self.m_cacheCondition = GEnums.FacSmartAlertType.DoNotShow
    self:_ShowDetailInfo(false)

    self.m_buildingData = {}
    self.m_buildingData.tid = buildingId
    self.m_buildingData.nid = nodeId
    self.m_buildingData.domain = Utils.getCurDomainId()
end

MachineSmartAlertNode._ShowDetailInfo = HL.Method(HL.Boolean, HL.Opt(HL.Function)) << function(self, show, onComplete)
    if self.m_showDetailAnimState ~= show then
        self.m_showDetailAnimState = show
        self.view.maskBtn.gameObject:SetActiveIfNecessary(show)
        self.m_playingAnimation = true
        self.view.animationWrapper:Play(show and ("machinesmartalert_in" .. self.m_animationPostfix) or ("machinesmartalert_out" .. self.m_animationPostfix), function()
            if onComplete then
                onComplete()
            end
            self.m_playingAnimation = false
        end)
        if DeviceInfo.usingController then
            AudioAdapter.PostEvent(show and "Au_UI_Toast_Common_Small_Open" or "Au_UI_Toast_Common_Small_Close")
        end
    end
end

MachineSmartAlertNode._CheckToRefreshState = HL.Method(HL.Table, HL.Number).Return(HL.Boolean, HL.Opt(HL.String)) << function(self, alertInfo, detlaTime)
    local newCondition = alertInfo.condition
    local newArgs = alertInfo.args
    local newCheck = alertInfo.checkRefresh
    local newDefaultOpen = alertInfo.defaultOpen or false
    if self.m_cacheCondition ~= newCondition then
        self.m_SmartAlertStableTime = 0
    elseif self.m_SmartAlertStableTime < Tables.factoryConst.smartAlertTriggerStableTime then
        self.m_SmartAlertStableTime = self.m_SmartAlertStableTime + detlaTime
    end
    self.m_cacheCondition = newCondition
    local refreshTag = false
    if self.m_curCondition ~= newCondition then

        if self.m_curCondition ~= GEnums.FacSmartAlertType.DoNotShow then
            self.m_curCondition = GEnums.FacSmartAlertType.DoNotShow
            refreshTag = true

        elseif self.m_SmartAlertStableTime >= Tables.factoryConst.smartAlertTriggerStableTime then
            self.m_curCondition = newCondition
            refreshTag = true
        end
    end
    if self.m_curCheck ~= newCheck then


        self.m_curCheck = newCheck
        refreshTag = true
    end
    if self.m_curDefaultOpen ~= newDefaultOpen then
        self.m_curDefaultOpen = newDefaultOpen
        refreshTag = true
    end

    local alertText
    if refreshTag then
        if self.m_curCondition ~= GEnums.FacSmartAlertType.DoNotShow then
            local alertData = Tables.factorySmartAlertTable:GetValue(self.m_curCondition)
            if DeviceInfo.usingController then
                alertText = alertData.alertText_ct
            elseif DeviceInfo.usingTouch then
                alertText = alertData.alertText_mb
            else
                alertText = alertData.alertText
            end
            if type(newArgs) == "table" and #newArgs > 0 then
                alertText = string.format(alertText, unpack(newArgs))
            end

            self.m_animationPostfix = FacConst.SMARTALERT_ANIMATION_NAME_POSTFIX[self.m_curCondition] or ""

            if self.m_buildingData ~= nil then
                EventLogManagerInst:GameEvent_FactorySmartAlertPopUp(
                    self.m_buildingData.tid,
                    self.m_buildingData.nid,
                    self.m_buildingData.domain,
                    self.m_curCondition:ToString()
                )
            end
        end
    end

    return refreshTag, alertText
end

MachineSmartAlertNode.UpdateSmartAlertState = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, detlaTime, alertInfo)
    if alertInfo == nil then
        if self.m_SmartAlertStableTime < Tables.factoryConst.smartAlertTriggerStableTime then
            self.m_SmartAlertStableTime = self.m_SmartAlertStableTime + detlaTime
        end
        return
    end

    local refreshTag, alertText = self:_CheckToRefreshState(alertInfo, detlaTime)
    if refreshTag then
        if alertText then
            self.view.tipsTxt.text = alertText
            self.gameObject:SetActiveIfNecessary(true)
            if self.m_activeAnimState then
                self:_ShowDetailInfo(self.m_curDefaultOpen)
            else
                self.m_playingAnimation = true
                AudioAdapter.PostEvent("Au_UI_Toast_FactoryTips_Open")
                self.view.animationWrapper:Play("machinesmartalert_frist_in" .. self.m_animationPostfix, function()
                    self:_ShowDetailInfo(self.m_curDefaultOpen)
                    self.m_playingAnimation = false
                end)
                self.m_activeAnimState = true
            end

            self.m_targetTransform = alertInfo.targetTransform
            LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.layoutNode.transform)
            UIUtils.updateTipsPosition(self.view.mainRect, self.m_targetTransform, self.view.rectTransform, UIManager.uiCamera, UIConst.UI_TIPS_POS_TYPE.FacSmartAlertTop)
            local padding = FacConst.SMARTALERT_TRASNFORM_OFFSET[self.m_curCondition]
            local pos = self.view.mainRect.anchoredPosition
            pos.x = pos.x + padding.x
            pos.y = pos.y + padding.y
            self.view.mainRect.anchoredPosition = pos
        else
            if self.m_showDetailAnimState then
                self:_ShowDetailInfo(false, function()
                    AudioAdapter.PostEvent("Au_UI_Toast_FactoryTips_Close")
                    self.view.animationWrapper:Play("machinesmartalert_frist_out" .. self.m_animationPostfix, function()
                        self.gameObject:SetActiveIfNecessary(false)
                    end)
                    self.m_activeAnimState = false
                end)
            else
                self.m_playingAnimation = true
                AudioAdapter.PostEvent("Au_UI_Toast_FactoryTips_Close")
                self.view.animationWrapper:Play("machinesmartalert_frist_out" .. self.m_animationPostfix, function()
                    self.gameObject:SetActiveIfNecessary(false)
                    self.m_playingAnimation = false
                end)
                self.m_activeAnimState = false
            end
        end
    end
end

MachineSmartAlertNode.RestoreAlertState = HL.Method() << function(self)
    self.m_curCondition = GEnums.FacSmartAlertType.DoNotShow
    self.m_cacheCondition = GEnums.FacSmartAlertType.DoNotShow
    self.m_SmartAlertStableTime = 0
    self.m_curCheck = nil
    self.m_curDefaultOpen = false
    self.m_showDetailAnimState = false
    self.m_activeAnimState = false
end

MachineSmartAlertNode.ForceUpdateAlertPosition = HL.Method() << function(self)
    if self.m_targetTransform == nil or
        self.m_curCondition == nil or
        self.m_curCondition == GEnums.FacSmartAlertType.DoNotShow then
        return
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.layoutNode.transform)
    UIUtils.updateTipsPosition(self.view.mainRect, self.m_targetTransform, self.view.rectTransform, UIManager.uiCamera, UIConst.UI_TIPS_POS_TYPE.FacSmartAlertTop)
    local padding = FacConst.SMARTALERT_TRASNFORM_OFFSET[self.m_curCondition]
    local pos = self.view.mainRect.anchoredPosition
    pos.x = pos.x + padding.x
    pos.y = pos.y + padding.y
    self.view.mainRect.anchoredPosition = pos
    self:_ShowDetailInfo(self.m_curDefaultOpen)
end

HL.Commit(MachineSmartAlertNode)
return MachineSmartAlertNode

