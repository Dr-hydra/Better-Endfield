
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeekRaidLeaveConfirm

WeekRaidLeaveConfirmCtrl = HL.Class('WeekRaidLeaveConfirmCtrl', uiCtrl.UICtrl)






WeekRaidLeaveConfirmCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WeekRaidLeaveConfirmCtrl.ShowPopUp = HL.StaticMethod() << function()
    UIManager:Open(PANEL_ID)
end


WeekRaidLeaveConfirmCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.cancelButton.onClick:RemoveAllListeners()
    self.view.cancelButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.confirmButton.onClick:RemoveAllListeners()
    self.view.confirmButton.onClick:AddListener(function()
        GameInstance.player.weekRaidSystem:WeekRaidStartSettlement()
        self:PlayAnimationOutAndClose()
    end)

    self.view.disorderNumTxt.text = string.format("%d", GameInstance.player.weekRaidSystem.weekRaidGame.DangerMeter)

    local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())

    self.view.bagNumTxt.text = string.format("%d/%d", itemBag:GetUsedSlotCount(), itemBag.slotCount)
end











HL.Commit(WeekRaidLeaveConfirmCtrl)
