local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimpleSystem
SimpleSystemCtrl = HL.Class('SimpleSystemCtrl', uiCtrl.UICtrl)







SimpleSystemCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SimpleSystemCtrl.m_selectIndex = HL.Field(HL.Number) << 1


SimpleSystemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.backBtn.button.onClick:AddListener(function()
        self:_OnClick(1)
    end)
    self:BindInputPlayerAction("common_back", function()
        self:_OnClick(1)
    end)

    self.view.settingBtn.button.onClick:AddListener(function()
        self:_OnClick(2)
    end)

    self.view.quitBtn.button.onClick:AddListener(function()
        self:_OnClick(3)
    end)

    self:_InitSimpleSystemController()
end

SimpleSystemCtrl._InitSimpleSystemController = HL.Method() << function(self)
    self.view.selectableNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        AudioManager.PostEvent("au_ui_btn_dlg_next")
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SimpleSystemCtrl._OnClick = HL.Method(HL.Number) << function(self, index)
    if index == 1 then
        PhaseManager:PopPhase(PhaseId.SimpleSystem)
    elseif index == 2 then
        PhaseManager:OpenPhase(PhaseId.GameSetting)
    elseif index == 3 then
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_EXIT_GAME_CONFIRM,
            hideBlur = true,
            onConfirm = function()
                logger.info("click quit btn on watch")
                CSUtils.ReturnToLogin()
            end,
        })
    end
end

HL.Commit(SimpleSystemCtrl)
