local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacDecorate

FacDecorateCtrl = HL.Class('FacDecorateCtrl', uiCtrl.UICtrl)

FacDecorateCtrl.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Decorate)





FacDecorateCtrl.s_messages = HL.StaticField(HL.Table) << {

}


FacDecorateCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    self.view.buildingCommon:InitBuildingCommon(self.m_buildingInfo)

    local numberInfoNode = self.view.numberInfoNode
    local maxNumber = GameInstance.remoteFactoryManager.currentChapterInfo.data.domainDecorateCount
    numberInfoNode.maxNumberTxt.text = string.format("%d", maxNumber)
    local currNumber = CSFactoryUtil.GetChapterBuildingCountByType(
        GameInstance.remoteFactoryManager.currentChapterInfo,
        GEnums.FCNodeType.Decorate:GetHashCode()
    )
    numberInfoNode.currentNumberTxt.text = string.format("%d", currNumber)
    numberInfoNode.currentNumberTxt.color = currNumber == maxNumber and self.view.config.MAX_COLOR or self.view.config.NORMAL_COLOR

    self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()
end

HL.Commit(FacDecorateCtrl)
