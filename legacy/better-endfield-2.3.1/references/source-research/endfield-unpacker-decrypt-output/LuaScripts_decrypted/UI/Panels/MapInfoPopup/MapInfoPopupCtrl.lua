
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapInfoPopup

local TOTAL_COUNT_FORMAT = "/%d"

MapInfoPopupCtrl = HL.Class('MapInfoPopupCtrl', uiCtrl.UICtrl)






MapInfoPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapInfoPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.btnEmpty.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    local buildingInfo, collectionInfo = unpack(arg)
    self:_RefreshContent(buildingInfo, collectionInfo)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

MapInfoPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

MapInfoPopupCtrl._RefreshContent = HL.Method(HL.Table, HL.Table) << function(self, buildingInfo, collectionInfo)
    for buildingCfgId, buildingInfo in pairs(buildingInfo) do
        MapUtils.updateMapInfoViewNode(self.view[buildingCfgId], buildingInfo, true)
    end

    for collectionCfgId, collectionInfoUnit in pairs(collectionInfo) do
        MapUtils.updateMapInfoViewNode(self.view[collectionCfgId], collectionInfoUnit, false)
    end
end

HL.Commit(MapInfoPopupCtrl)
