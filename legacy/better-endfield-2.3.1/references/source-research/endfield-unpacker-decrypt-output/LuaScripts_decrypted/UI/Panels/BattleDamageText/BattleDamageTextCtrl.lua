local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleDamageText






BattleDamageTextCtrl = HL.Class('BattleDamageTextCtrl', uiCtrl.UICtrl)







BattleDamageTextCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.TOGGLE_DEBUG_DAMAGE_TEXT_MODE] = 'OnToggleDebugDamageTextMode',
}


BattleDamageTextCtrl.m_isGPUMode = HL.Field(HL.Boolean) << true
BattleDamageTextCtrl.m_isGPUModeInited = HL.Field(HL.Boolean) << false
BattleDamageTextCtrl.m_isCPUModeInited = HL.Field(HL.Boolean) << false


BattleDamageTextCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if self.m_isGPUMode then
        self.view.damageTextCtrlV2:OnCreate()
        self.m_isGPUModeInited = true
        self.view.content.gameObject:SetActive(false)
        self.view.prefabNode.gameObject:SetActive(false)
    else
        self.view.damageTextCtrl:OnCreate()
        self.m_isCPUModeInited = true
    end
end

BattleDamageTextCtrl.OnClose = HL.Override() << function(self)
    if self.m_isGPUModeInited then

        if not self.m_isGPUMode then
            self.view.damageTextCtrlV2:DebugChangeImpl(false)
        end
        self.view.damageTextCtrlV2:OnClose()
    end
    if self.m_isCPUModeInited then

        if self.m_isGPUMode then
            self.view.damageTextCtrl:DebugChangeImpl(false)
        end
        self.view.damageTextCtrl:OnClose()
    end
end

BattleDamageTextCtrl.OnShow = HL.Override() << function(self)
    if self.m_isGPUMode then
        self.view.damageTextCtrlV2:OnShow()
    else
        self.view.damageTextCtrl:OnShow()
    end
end

BattleDamageTextCtrl.OnHide = HL.Override() << function(self)
    if self.m_isGPUMode then
        self.view.damageTextCtrlV2:OnHide()
    else
        self.view.damageTextCtrl:OnHide()
    end
end


BattleDamageTextCtrl.OnToggleDebugDamageTextMode = HL.Method() << function(self)
    self.m_isGPUMode = not self.m_isGPUMode
    GameAction.ShowUIToast("已切换到" .. (self.m_isGPUMode and "GPU模式" or "CPU模式"))
    if self.m_isGPUModeInited == false and self.m_isGPUMode then
        self.m_isGPUModeInited = true
        self.view.damageTextCtrlV2:OnCreate()
        self.view.damageTextCtrlV2:OnShow()
        self.view.damageTextCtrl:DebugChangeImpl(self.m_isGPUMode)
        return
    end
    if self.m_isCPUModeInited == false and not self.m_isGPUMode then
        self.m_isCPUModeInited = true
        self.view.content.gameObject:SetActive(true)
        self.view.prefabNode.gameObject:SetActive(true)
        self.view.damageTextCtrl:OnCreate()
        self.view.damageTextCtrl:OnShow()
        self.view.damageTextCtrlV2:DebugChangeImpl(not self.m_isGPUMode)
        return
    end
    if self.m_isGPUModeInited then
        self.view.damageTextCtrlV2:DebugChangeImpl(not self.m_isGPUMode)
    end
    if self.m_isCPUModeInited then
        self.view.damageTextCtrl:DebugChangeImpl(self.m_isGPUMode)
    end
end

HL.Commit(BattleDamageTextCtrl)
