local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefensePrepareHud
SettlementDefensePrepareHudCtrl = HL.Class('SettlementDefensePrepareHudCtrl', uiCtrl.UICtrl)

local LEAVE_AREA_TOAST_TEXT_ID = "ui_fac_settlement_defence_prepare_stage_quit"
local WAIT_ANIMATION_PLAY_COUNT = 2

SettlementDefensePrepareHudCtrl.m_levelId = HL.Field(HL.String) << ""

SettlementDefensePrepareHudCtrl.m_updateTick = HL.Field(HL.Number) << -1

SettlementDefensePrepareHudCtrl.m_taskTrackCtrl = HL.Field(HL.Forward("UICtrl"))

SettlementDefensePrepareHudCtrl.m_outAnimPlayCount = HL.Field(HL.Number) << 0

SettlementDefensePrepareHudCtrl.m_btnLock = HL.Field(HL.Boolean) << false






SettlementDefensePrepareHudCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SettlementDefensePrepareHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local towerDefenseSystem = GameInstance.player.towerDefenseSystem
    self.m_btnLock = false

    self.m_levelId = towerDefenseSystem.activeTdId
    if string.isEmpty(self.m_levelId) then
        return
    end

    self.view.startButton.onClick:AddListener(function()
        if self.m_btnLock then
            return
        end
        if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.TowerDefenseSystemActionConflictName) then
            return
        end
        self:_StartCoroutine(function()
            coroutine.step()
            coroutine.step()


            GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.TowerDefenseSystemActionConflictName)
        end)
        towerDefenseSystem:EnterDefendingPhase()
        self.m_btnLock = true
    end)

    self.view.btnStop.onClick:AddListener(function()
        if self.m_btnLock then
            return
        end
        towerDefenseSystem:LeavePreparingPhase()
        self.m_btnLock = true
    end)



    if not DeviceInfo.usingTouch then
        self.m_updateTick = LuaUpdate:Add("LateTick", function(deltaTime)
            self:_RefreshPcNodePosition()
        end)
    end
    self.view.prepareToast:SetState("prepare")
end

SettlementDefensePrepareHudCtrl.OnClose = HL.Override() << function(self)
    self.m_updateTick = LuaUpdate:Remove(self.m_updateTick)
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.TowerDefenseSystemActionConflictName)
end

SettlementDefensePrepareHudCtrl._RefreshPcNodePosition = HL.Method() << function(self)
    if not self.view.pcNode.gameObject.activeInHierarchy then
        return
    end

    if self.m_taskTrackCtrl == nil then
        local success, taskTrackCtrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
        if not success then
            return
        end
        self.m_taskTrackCtrl = taskTrackCtrl
    end

    local followNode = self.m_taskTrackCtrl:GetContentBottomFollowNode()
    if NotNull(followNode) then
        self.view.pcNode.position = followNode.position
    end
end

SettlementDefensePrepareHudCtrl._TryInvokeCloseCallback = HL.Method() << function(self)
    self.m_outAnimPlayCount = self.m_outAnimPlayCount - 1
    if self.m_outAnimPlayCount > 0 then
        return
    end

    self:Close()
end

SettlementDefensePrepareHudCtrl._PlayBtnGroupAnimOut = HL.Method() << function(self)
    self.view.btnGroupAnim:PlayOutAnimation(function()
        self.view.btnGroupAnim.gameObject:SetActive(false)
        self:_TryInvokeCloseCallback()
    end)
end

SettlementDefensePrepareHudCtrl._PlayTitleAnimOut = HL.Method() << function(self)
    self.view.mainTitle:PlayOutAnimation(function()
        self:_TryInvokeCloseCallback()
    end)
end

SettlementDefensePrepareHudCtrl.CloseDefensePrepareHud = HL.Method(HL.Boolean, HL.Boolean) << function(self, needAreaLeave, closeDirectly)
    if closeDirectly then
        self:Close()
    else
        self.m_updateTick = LuaUpdate:Remove(self.m_updateTick)
        self.m_outAnimPlayCount = WAIT_ANIMATION_PLAY_COUNT

        if needAreaLeave then
            self.view.prepareToast:SetState("leave")
            self:_PlayBtnGroupAnimOut()
            TimerManager:StartTimer(self.view.config.LEAVE_AREA_DELAY, function()
                self:_PlayTitleAnimOut()
            end)
        else
            self:_PlayBtnGroupAnimOut()
            self:_PlayTitleAnimOut()
        end
    end
end

HL.Commit(SettlementDefensePrepareHudCtrl)