
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHongsBusSource

FacHongsBusSourceCtrl = HL.Class('FacHongsBusSourceCtrl', uiCtrl.UICtrl)

local FC_NODE_TYPE_BUSFREE = 40
local COLOR_TEXT_FORMAT = "<color=#FFF100>%s</color>/%s"

FacHongsBusSourceCtrl.m_uiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo)

FacHongsBusSourceCtrl.m_nodeId = HL.Field(HL.Any)





FacHongsBusSourceCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacHongsBusSourceCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    self.m_nodeId = self.m_uiInfo.nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo)

    local isBusFree = self.m_uiInfo.nodeHandler.nodeType == FC_NODE_TYPE_BUSFREE
    self.view.busFreeNode.gameObject:SetActiveIfNecessary(isBusFree)
    self.view.busStartNode.gameObject:SetActiveIfNecessary(not isBusFree)
    if isBusFree then
        local cur, max = GameInstance.remoteFactoryManager:GetFreeBusCountInfoInCoreZone(self.m_nodeId)
        self.view.numberTxt.text = string.format(COLOR_TEXT_FORMAT, cur, max)
        self.view.buildingCommon.view.invalidStateNode.gameObject:SetActiveIfNecessary(not self.m_uiInfo.busFree.enabled)
        self.view.buildingCommon.view.stateNode.gameObject:SetActiveIfNecessary(self.m_uiInfo.busFree.enabled)
    end

    local itemData = FactoryUtils.getBuildingItemData(self.m_uiInfo.buildingId)
    self.view.text.text = itemData.desc

    self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()
end











HL.Commit(FacHongsBusSourceCtrl)
