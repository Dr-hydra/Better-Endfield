
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitBreakPopup
WeaponExhibitBreakPopupCtrl = HL.Class('WeaponExhibitBreakPopupCtrl', uiCtrl.UICtrl)







WeaponExhibitBreakPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WeaponExhibitBreakPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local weaponInfo = arg.weaponInfo
    local fromBreakthroughLevel = arg.fromLevel
    local toBreakthroughLevel = arg.toLevel

    self:_InitActionEvent()
    local fromBreakthroughCfg = CharInfoUtils.getWeaponBreakthroughInfo(weaponInfo.weaponInstId, fromBreakthroughLevel)
    local toBreakthroughCfg = CharInfoUtils.getWeaponBreakthroughInfo(weaponInfo.weaponInstId, toBreakthroughLevel)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)

    self.view.fromLevel.text = string.format(Language.LUA_WEAPON_EXHIBIT_BREAK_FROM_LEVEL, weaponExhibitInfo.curLv, fromBreakthroughCfg.breakthroughLv)
    self.view.toLevel.text = string.format(Language.LUA_WEAPON_EXHIBIT_BREAK_TO_LEVEL, weaponExhibitInfo.curLv, toBreakthroughCfg.breakthroughLv)

    self.view.levelBreakNode:InitLevelBreakNode(fromBreakthroughLevel)
    self.view.weaponUpgradeAttributeNode:InitWeaponUpgradeAttributeNode({
        fromLv = weaponExhibitInfo.curLv,
        fromBreakthroughLv = fromBreakthroughLevel,
        toLv = weaponExhibitInfo.curLv,
        toBreakthroughLv = toBreakthroughLevel,
        weaponInstId = weaponInfo.weaponInstId,
    })
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

WeaponExhibitBreakPopupCtrl._InitActionEvent = HL.Method() << function(self, arg)
    self.view.continueButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)
end

HL.Commit(WeaponExhibitBreakPopupCtrl)
