
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitDocument
WeaponExhibitDocumentCtrl = HL.Class('WeaponExhibitDocumentCtrl', uiCtrl.UICtrl)







WeaponExhibitDocumentCtrl.m_weaponInfo = HL.Field(HL.Table)

WeaponExhibitDocumentCtrl.s_messages = HL.StaticField(HL.Table) << {}


WeaponExhibitDocumentCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local weaponInfo = arg.weaponInfo

    self.m_weaponInfo = weaponInfo

    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    self.view.title.text = string.format(Language.LUA_WEAPON_EXHIBIT_DOCUMENT_TITLE, weaponExhibitInfo.itemCfg.name)

    self:_InitActionEvent()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

WeaponExhibitDocumentCtrl.OnShow = HL.Override() << function(self)
    local weaponInfo = self.m_weaponInfo

    self:_RefreshDocumentPanel(weaponInfo)
end


WeaponExhibitDocumentCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnBack.onClick:AddListener(function()
        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW,
        })
    end)
end

WeaponExhibitDocumentCtrl._RefreshDocumentPanel = HL.Method(HL.Table) << function(self, weaponInfo)
    local exhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    local weaponCfg = exhibitInfo.weaponCfg
    self.view.descContent.text = weaponCfg.weaponDesc
end

HL.Commit(WeaponExhibitDocumentCtrl)
