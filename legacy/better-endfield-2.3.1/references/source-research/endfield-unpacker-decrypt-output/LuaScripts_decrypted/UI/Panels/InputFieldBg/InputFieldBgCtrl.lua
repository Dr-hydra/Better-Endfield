
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.InputFieldBg
local PHASE_ID = PhaseId.InputFieldBg
InputFieldBgCtrl = HL.Class('InputFieldBgCtrl', uiCtrl.UICtrl)






InputFieldBgCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_INPUT_FIELD_BG_HEIGHT_CHANGE] = 'OnBgHeightChange',
    [MessageConst.EXIT_ALL_PHASE] = 'CloseSelf',
}


InputFieldBgCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

InputFieldBgCtrl.OnShow = HL.Override() << function(self)
    self.view.imgBack.gameObject:SetActive(true)
    UIUtils.setSizeDeltaY(self.view.imgBack.transform, 0)
end

InputFieldBgCtrl.OnInputFieldBgInit = HL.StaticMethod() << function()
    UIManager:AutoOpen(PANEL_ID)
end

InputFieldBgCtrl.CloseSelf = HL.Method(HL.Table) << function(self, arg)
    self:Close()
end

InputFieldBgCtrl.OnBgHeightChange = HL.Method(HL.Table) << function(self, arg)
    local height = arg[1]
    local isActive = arg[2] or false
    local alpha = arg[3] or 1
    if isActive then
        local uiPos, isInside = UIUtils.screenPointToUI(Vector2(0, height), self.uiCamera, self.view.transform)
        UIUtils.setSizeDeltaY(self.view.imgBack.transform, UIManager.uiCanvasRect.rect.height / 2 + uiPos.y)
        UIUtils.changeAlpha(self.view.imgBack, alpha)
    end
    if not isActive then
        UIUtils.setSizeDeltaY(self.view.imgBack.transform, 0)
        UIManager:Hide(self.panelId)
    else
        UIManager:Show(self.panelId)
    end
end

HL.Commit(InputFieldBgCtrl)
