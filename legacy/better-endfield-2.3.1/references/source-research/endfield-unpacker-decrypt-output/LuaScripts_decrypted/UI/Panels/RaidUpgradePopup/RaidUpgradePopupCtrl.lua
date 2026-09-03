local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RaidUpgradePopup

RaidUpgradePopupCtrl = HL.Class('RaidUpgradePopupCtrl', uiCtrl.UICtrl)






RaidUpgradePopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

RaidUpgradePopupCtrl.OnRaidTechModify = HL.StaticMethod(HL.Table) << function(args)
    local techId, beforeValue, afterValue = unpack(args)

    local self = UIManager:AutoOpen(PANEL_ID, {
        techId = techId,
        beforeValue = beforeValue,
        afterValue = afterValue,
    },false)
end


RaidUpgradePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.bgBtn.onClick:RemoveAllListeners()
    self.view.bgBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self:BindInputPlayerAction("common_pop_up_cancel", function()
        self:PlayAnimationOutAndClose()
    end)



    local techId = arg and arg.techId or 0


    local success, techCfg = Tables.weekRaidTechTable:TryGetValue(techId)
    if not success then
        logger.error("RaidUpgradePopupCtrl.OnCreate: Failed to get tech config for techId: " .. techId)
        return
    end

    local beforeValue = 0
    local afterValue = 0
    if arg.beforeValue and arg.afterValue then

        beforeValue = arg.beforeValue
        afterValue = arg.afterValue
    end


    self.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, techCfg.techTypeData.icon)
    self.view.nameText.text = techCfg.techTypeData.name

    self.view.bottomText.text = techCfg.techTypeData.desc

    if WeeklyRaidUtils.TechUseStrValue(techCfg) then

        self.view.capacityNode.gameObject:SetActive(false)
        self.view.unlockNode.gameObject:SetActive(true)

        self.view.unlockText.text = techCfg.techTypeData.normalDesc
    else

        self.view.capacityNode.gameObject:SetActive(true)
        self.view.unlockNode.gameObject:SetActive(false)

        self.view.capacityLeftText.text = beforeValue
        self.view.capacityRightText.text = afterValue
        self.view.capacityDescText.text = techCfg.techTypeData.normalDesc
    end
end











HL.Commit(RaidUpgradePopupCtrl)
