local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalReconnect
local PHASE_ID = PhaseId.ReflowFormalReconnect

ReflowFormalReconnectCtrl = HL.Class('ReflowFormalReconnectCtrl', uiCtrl.UICtrl)






ReflowFormalReconnectCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ReflowFormalReconnectCtrl.m_activityId = HL.Field(HL.String) << ''

ReflowFormalReconnectCtrl.m_instructionId = HL.Field(HL.String) << ''

ReflowFormalReconnectCtrl.m_endTime = HL.Field(HL.Number) << -1

ReflowFormalReconnectCtrl.m_curPanelId = HL.Field(HL.Number) << -1

ReflowFormalReconnectCtrl.m_tabInfo = HL.Field(HL.Table)

ReflowFormalReconnectCtrl.m_tabCellCache = HL.Field(HL.Any)


ReflowFormalReconnectCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs()
end

ReflowFormalReconnectCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_curPanelId = arg.panelId
    local _, activityCfg = Tables.activityTable:TryGetValue(self.m_activityId)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_instructionId = activityCfg.instructionId
    self.m_endTime = activityData.endTime

    self.m_tabInfo = {
        {
            panelId = PanelId.ActivityReflowSignin,
            icon = "icon_reflow_formal_signin",
            title = Language["reflow_checkin"],
            redDot = "ActivityReflowSignin",
        },
        {
            panelId = PanelId.ReflowFormalReconnectTask,
            icon = "icon_reflow_formal_task",
            title = Language["reflow_mission"],
            redDot = "ActivityReflowTask",
        },
    }
end

ReflowFormalReconnectCtrl._InitUI = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.helpBtn.onClick:AddListener(function()
        self.m_phase:ShowInstruction(self.m_instructionId)
    end)
    self.m_tabCellCache = UIUtils.genCellCache(self.view.tab)
    self.view.countDownText:InitCountDownText(self.m_endTime)
end

ReflowFormalReconnectCtrl._RefreshAllUIs = HL.Method() << function(self)
    self.m_tabCellCache:Refresh(#self.m_tabInfo, function(cell,luaIndex)
        local tabInfo = self.m_tabInfo[luaIndex]
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_REFLOW, tabInfo.icon)
        cell.nameTxt.text = tabInfo.title
        cell.redDot:InitRedDot(tabInfo.redDot, self.m_activityId)
        cell.toggle.isOn = tabInfo.panelId == self.m_curPanelId
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnTabClicked(tabInfo.panelId)
            end
        end)
    end)
    self:_OnTabClicked(self.m_curPanelId)
end

ReflowFormalReconnectCtrl._OnTabClicked = HL.Method(HL.Number) << function(self, panelId)
    self.m_curPanelId = panelId
    self.m_phase:OnTabChange({panelId = panelId})
end



HL.Commit(ReflowFormalReconnectCtrl)
