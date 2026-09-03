
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PresetTeamSwitch

PresetTeamSwitchCtrl = HL.Class('PresetTeamSwitchCtrl', uiCtrl.UICtrl)






PresetTeamSwitchCtrl.s_messages = HL.StaticField(HL.Table) << {

}

PresetTeamSwitchCtrl.m_charHeadCellCache = HL.Field(HL.Forward("UIListCache"))









PresetTeamSwitchCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.confirmButton.onClick:AddListener(function()
        if arg.onConfirm then
            arg.onConfirm()
        end
    end)
    self.view.cancelButton.onClick:AddListener(function()
        if arg.onCancel then
            arg.onCancel()
        end
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.titleTxt.text = arg.title
    local hasSubTitle = not string.isEmpty(arg.subTitle)
    local hasPresetTeam = not string.isEmpty(arg.presetTeamId)
    self.view.tipsMaxLayout.gameObject:SetActive(hasSubTitle and hasPresetTeam)
    if hasSubTitle then
        self.view.tipsTxt.text = arg.subTitle
    end
    self.view.charNode.gameObject:SetActive(hasPresetTeam)
    self.view.charNode.gameObject:SetActive(arg.hideTeam == nil or arg.hideTeam)
    if hasPresetTeam then
        local lockTeamData = CharInfoUtils.getLockedFormationData(arg.presetTeamId)
        if lockTeamData then
            self.m_charHeadCellCache = UIUtils.genCellCache(self.view.charHeadCellLongHpBar)

            self.m_charHeadCellCache:Refresh(#lockTeamData.chars, function(cell, index)
                cell:InitCharFormationHeadCell(lockTeamData.chars[index])
            end)
        else
            self.m_charHeadCellCache:Refresh(0)
        end
    end
end

HL.Commit(PresetTeamSwitchCtrl)
