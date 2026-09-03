
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMarker

FacMarkerCtrl = HL.Class('FacMarkerCtrl', uiCtrl.UICtrl)






FacMarkerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacMarkerCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacMarkerCtrl.m_nodeId = HL.Field(HL.Number) << -1

FacMarkerCtrl.m_selectedIconKey = HL.Field(HL.Table)

FacMarkerCtrl.m_selectedIcon = HL.Field(HL.Table)


FacMarkerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    self.m_nodeId = arg.uiInfo.nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {})

    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(self.m_nodeId)
    self.view.numberInfoNode.gameObject:SetActiveIfNecessary(not isOthersSocialBuilding)
    self.view.markerManageBtn.gameObject:SetActiveIfNecessary(not isOthersSocialBuilding)
    self.view.deco.gameObject:SetActiveIfNecessary(isOthersSocialBuilding)
    if not isOthersSocialBuilding then
        self.view.markerManageBtn.onClick:AddListener(function()
            UIManager:AutoOpen(PanelId.FacMarkerManagePopup, {
                nodeId = self.m_nodeId,
                onDelBuilding = function(count)
                    self.view.currentNumberTxt.text = count
                end
            })
        end)
    end

    self.m_selectedIcon = {}
    for i = 1, FacConst.SOCIAL_ICON_MAX_COUNT do
        self.m_selectedIcon[i] = self.view["markerIcon" .. i]
    end

    self:_UpdateSelectedIcon()
    self:_UpdateSignBuildingCount()

    self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()
end








FacMarkerCtrl._UpdateSelectedIcon = HL.Method() << function(self)
    self.m_selectedIconKey = {}
    local icons = GameInstance.remoteFactoryManager:GetSignBuildingIcons(self.m_nodeId)
    for i = 0, icons.Length - 1 do
        if icons[i] ~= 0 then
            table.insert(self.m_selectedIconKey, icons[i])
        end
    end
    local combineText = ""
    for i = 1, FacConst.SOCIAL_ICON_MAX_COUNT do
        local iconKey = self.m_selectedIconKey[i]
        self.m_selectedIcon[i].gameObject:SetActiveIfNecessary(iconKey ~= nil)
        if iconKey ~= nil then
            local iconData = Tables.socialBuildingSignTable:GetValue(iconKey)
            self.m_selectedIcon[i]:LoadSprite(UIConst.UI_SPRITE_FAC_MARKER_SETTING_ICON, iconData.uiIconKey)
            if i == 1 then
                combineText = iconData.text
            else
                combineText = I18nUtils.CombineStringWithLanguageSpilt(combineText, iconData.text)
            end
        end
    end
    self.view.tipsTxt.text = combineText
end

FacMarkerCtrl._UpdateSignBuildingCount = HL.Method() << function(self)
    local count = FactoryUtils.getPlayerAllMarkerBuildingNodeInfo()
    self.view.currentNumberTxt.text = count
    self.view.maxNumberTxt.text = Tables.factoryConst.signNodeCountLimit
end




HL.Commit(FacMarkerCtrl)
