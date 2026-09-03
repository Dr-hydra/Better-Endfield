
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitUpgradePopup
WeaponExhibitUpgradePopupCtrl = HL.Class('WeaponExhibitUpgradePopupCtrl', uiCtrl.UICtrl)

WeaponExhibitUpgradePopupCtrl.s_messages = HL.StaticField(HL.Table) << {}







WeaponExhibitUpgradePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local weaponInfo = arg.weaponInfo
    local fromLevel = arg.fromLevel
    local toLevel = arg.toLevel

    self:_InitActionEvent()

    local basicInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)

    self.view.fromLevel.text = string.format(Language.LUA_WEAPON_EXHIBIT_BREAK_FROM_LEVEL, fromLevel, basicInfo.stageLv)
    self.view.toLevel.text = string.format(Language.LUA_WEAPON_EXHIBIT_BREAK_FROM_LEVEL, toLevel, basicInfo.stageLv)
    self.view.weaponUpgradeAttributeNode:InitWeaponUpgradeAttributeNode({
        fromLv = fromLevel,
        fromBreakthroughLv = basicInfo.curBreakthroughLv,
        toLv = toLevel,
        toBreakthroughLv = basicInfo.curBreakthroughLv,
        weaponInstId = weaponInfo.weaponInstId,
    })
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

WeaponExhibitUpgradePopupCtrl._InitActionEvent = HL.Method() << function(self, arg)
    self.view.continueButton.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)
end

HL.Commit(WeaponExhibitUpgradePopupCtrl)
