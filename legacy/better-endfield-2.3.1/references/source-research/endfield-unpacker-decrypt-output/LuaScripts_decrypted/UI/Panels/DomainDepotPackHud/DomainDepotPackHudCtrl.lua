local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotPackHud
local PackState = {
    Healthy = "Healthy",
    Warning = "Warning",
    Dangerous = "Dangerous",
}
local ReducePackageCompletenessReason = GEnums.ReducePackageCompletenessReason

DomainDepotPackHudCtrl = HL.Class('DomainDepotPackHudCtrl', uiCtrl.UICtrl)

local ReduceReasonHintNodeViewCfg = {
    [ReducePackageCompletenessReason.Hurt] = "hurtReasonText",
    [ReducePackageCompletenessReason.Teleport] = "teleportReasonText",
    [ReducePackageCompletenessReason.Jump] = "jumpReasonText",
}





DomainDepotPackHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_PACK_INTEGRITY_CHANGED] = '_RefreshIntegrityState',
    [MessageConst.ON_PHASE_LEVEL_ON_TOP] = '_OnPhaseLevelOnTop',
}

DomainDepotPackHudCtrl.m_lastIntegrity = HL.Field(HL.Number) << -1

DomainDepotPackHudCtrl.m_allInfoShowTimer = HL.Field(HL.Number) << -1

DomainDepotPackHudCtrl.m_attackedIntegrityTimeCache = HL.Field(HL.Table)

DomainDepotPackHudCtrl.m_attackedTween = HL.Field(HL.Userdata)

DomainDepotPackHudCtrl.m_needRefreshBackToPhaseLevel = HL.Field(HL.Boolean) << false


DomainDepotPackHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.reasonNode.gameObject:SetActive(false)
    self.view.integrityTextLayout.gameObject:SetActive(true)
    self.view.integrityProgressRedImg.gameObject:SetActive(false)
    self.view.integrityIconAnim:PlayWithTween("domainDepot_hud_pack_dangerous_out")
    self.view.integrityAnim:PlayWithTween("domainDepot_hud_pack_attacked_default")
    self.m_attackedIntegrityTimeCache = {
        {
            integrity = GameInstance.player.domainDepotSystem:GetDomainDepotDeliveringCargoIntegrity(),
            time = CS.Beyond.TimeManager.time
        }
    }

    self:_InitPackIntegrityReduceReasonNode()

    if arg.needShowAllInfo then
        self:_ShowAllIntegrityInfo()
    end
    self:_RefreshIntegrityState()
end

DomainDepotPackHudCtrl.OnClose = HL.Override() << function(self)
    if self.m_allInfoShowTimer > 0 then
        TimerManager:ClearTimer(self.m_allInfoShowTimer)
    end
    self.m_allInfoShowTimer = -1

    if self.m_attackedTween ~= nil then
        self.m_attackedTween:Kill(false)
        self.m_attackedTween = nil
    end
end

DomainDepotPackHudCtrl._OnPhaseLevelOnTop = HL.Method() << function(self)
    if not self.m_needRefreshBackToPhaseLevel then
        return
    end
    self.m_needRefreshBackToPhaseLevel = false
    self:_RefreshIntegrityState()
end

DomainDepotPackHudCtrl._InitPackIntegrityReduceReasonNode = HL.Method() << function(self)
    local domainDepotSystem = GameInstance.player.domainDepotSystem
    local deliverInfo = domainDepotSystem:GetDomainDepotDeliverInfoByInstId(domainDepotSystem.deliverInstId)
    if deliverInfo == nil then
        return
    end
    local reduceTypeList = DomainDepotUtils.GetDepotPackIntegrityReduceTypeList(deliverInfo.itemType)
    local needShowTypeList = {}

    for _, reduceType in ipairs(reduceTypeList) do
        needShowTypeList[reduceType] = true
    end

    for reduceType, viewName in pairs(ReduceReasonHintNodeViewCfg) do
        local reasonNode = self.view.reasonNode[viewName]
        if reasonNode ~= nil then
            reasonNode.gameObject:SetActive(needShowTypeList[reduceType:GetHashCode()] == true)
        end
    end
end

DomainDepotPackHudCtrl._ShowAllIntegrityInfo = HL.Method() << function(self)
    self.view.reasonNode.gameObject:SetActive(true)
    self.view.integrityTextLayout.gameObject:SetActive(false)
    self.m_allInfoShowTimer = TimerManager:StartTimer(self.view.config.ALL_INFO_SHOW_DURATION, function()
        self.view.reasonNode.gameObject:SetActive(false)
        self.view.integrityTextLayout.gameObject:SetActive(true)
        TimerManager:ClearTimer(self.m_allInfoShowTimer)
        self.m_allInfoShowTimer = -1
    end)
end

DomainDepotPackHudCtrl._RefreshPackDisplayStateByIntegrity = HL.Method(HL.Number) << function(self, integrity)
    local state
    if integrity <= self.view.config.DANGER_PERCENTAGE then
        state = PackState.Dangerous
    elseif integrity <= self.view.config.WARNING_PERCENTAGE then
        state = PackState.Warning
    else
        state = PackState.Healthy
    end
    self.view.integrityIconNode:SetState(state)
end

DomainDepotPackHudCtrl._RefreshIntegrityState = HL.Method() << function(self)
     if PhaseManager:GetTopPhaseId() ~= PhaseId.Level then
        self.m_needRefreshBackToPhaseLevel = true
        return
    end

    local integrity = GameInstance.player.domainDepotSystem:GetDomainDepotDeliveringCargoIntegrity()

    if self.m_lastIntegrity <= 0 then

        self:_RefreshPackDisplayStateByIntegrity(integrity)
    else
        if (self.m_lastIntegrity > self.view.config.WARNING_PERCENTAGE and integrity <= self.view.config.WARNING_PERCENTAGE) or
            (self.m_lastIntegrity > self.view.config.DANGER_PERCENTAGE and integrity <= self.view.config.DANGER_PERCENTAGE) then
            self:_RefreshPackDisplayStateByIntegrity(integrity)
        end
    end

    if self.m_lastIntegrity > integrity then
        self.view.integrityAnim:ClearTween()
        self.view.integrityAnim:PlayWithTween("domainDepot_hud_pack_attacked_in_integrity", function()
            self.view.integrityAnim:PlayWithTween("domainDepot_hud_pack_attacked_out_integrity")
        end)

        local needPlayDangerousAnim = false
        local currTime = CS.Beyond.TimeManager.time
        if self.m_lastIntegrity - integrity >= self.view.config.SEVERE_ATTACKED_INTEGRITY then
            needPlayDangerousAnim = true
        else
            for index = #self.m_attackedIntegrityTimeCache, 1, -1 do
                local cacheData = self.m_attackedIntegrityTimeCache[index]
                if cacheData.integrity - integrity >= self.view.config.SEVERE_ATTACKED_INTEGRITY and
                    currTime - cacheData.time < self.view.config.SEVERE_ATTACKED_DURATION then
                    needPlayDangerousAnim = true
                end
            end
        end
        if needPlayDangerousAnim then
            self.view.integrityIconAnim:ClearTween()
            self.view.integrityIconAnim:PlayWithTween("domainDepot_hud_pack_dangerous_in")
        end

        table.insert(self.m_attackedIntegrityTimeCache, {
            integrity = integrity,
            time = currTime,
        })

        self.view.integrityProgressRedImg.gameObject:SetActive(true)
        if self.m_attackedTween ~= nil then
            self.m_attackedTween:Kill(false)
        end
        self.m_attackedTween = DOTween.To(function()
            return self.view.integrityProgressRedImg.fillAmount
        end, function(amount)
            self.view.integrityProgressRedImg.fillAmount = amount
        end, integrity / 100, self.view.config.ATTACKED_PROGRESS_TWEEN_DURATION):OnComplete(function()
            self.m_attackedTween = nil
            self.view.integrityProgressRedImg.gameObject:SetActive(false)
        end)

        AudioAdapter.PostEvent("Au_UI_Event_CompletenessReduce")
    end

    self.view.integrityPercentageTxt.text = string.format("%d", integrity)
    self.view.reasonNode.reasonIntegrityTxt.text = string.format("%d", integrity)
    self.view.integrityProgressImg.fillAmount = integrity / 100

    self.m_lastIntegrity = integrity
end

HL.Commit(DomainDepotPackHudCtrl)
