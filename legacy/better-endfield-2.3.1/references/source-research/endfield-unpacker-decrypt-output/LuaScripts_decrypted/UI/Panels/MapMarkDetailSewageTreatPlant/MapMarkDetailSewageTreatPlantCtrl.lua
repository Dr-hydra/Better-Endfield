local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSewageTreatPlant

MapMarkDetailSewageTreatPlantCtrl = HL.Class('MapMarkDetailSewageTreatPlantCtrl', uiCtrl.UICtrl)






MapMarkDetailSewageTreatPlantCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailSewageTreatPlantCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local commonArgs = {
        markInstId = arg.markInstId,
        bigBtnActive = true
    }

    local _, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(commonArgs.markInstId)
    local plantId = markRuntimeData.detail.plantId
    local plantData = FactoryUtils.getSewageTreatPlantData(plantId)
    if plantData.currLevel > 0 then
        local isMaxLevel = plantData.currLevel == plantData.maxLevel and plantData.isFinalMaxLevel
        self.view.levelStateNode.stateController:SetState(isMaxLevel and "Max" or "Normal")
        self.view.levelStateNode.levelTxt.text = string.format("%d", plantData.currLevel)
        self.view.levelStateNode.gameObject:SetActive(true)
    else
        self.view.levelStateNode.gameObject:SetActive(false)
    end

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailSewageTreatPlantCtrl)
