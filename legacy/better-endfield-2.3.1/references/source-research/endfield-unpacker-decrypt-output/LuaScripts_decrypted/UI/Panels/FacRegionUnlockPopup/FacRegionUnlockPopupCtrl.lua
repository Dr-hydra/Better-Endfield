
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacRegionUnlockPopup
FacRegionUnlockPopupCtrl = HL.Class('FacRegionUnlockPopupCtrl', uiCtrl.UICtrl)







FacRegionUnlockPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
}


FacRegionUnlockPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, areaId)
    local data = Tables.sceneAreaTable[areaId]
    local sceneMsg = FactoryUtils.getCurSceneHandler()

    self:_UpdateContent(self.view.bandwidthNode, sceneMsg.bandwidth.max, data.bandwidth)
    self:_UpdateContent(self.view.buildingCountNode, sceneMsg.bandwidth.spMax, data.spBuildingCnt)

    self:_StartTimer(self.view.config.AUTO_CLOSE_TIME, function()
        self:Close()
    end)
    self:_StartTimer(self.view.config.SFX_TIME, function()
        AudioManager.PostEvent("au_ui_fac_unlock")
    end)
end

FacRegionUnlockPopupCtrl._UpdateContent = HL.Method(HL.Table, HL.Number, HL.Number) << function(self, node, curMax, addedNum)
    node.beforeTxt.text = curMax - addedNum
    node.afterTxt.text = curMax
end

FacRegionUnlockPopupCtrl.OnRegionUnlocked = HL.StaticMethod(HL.Table) << function(arg)
    local areaId = unpack(arg)
    UIManager:AutoOpen(PANEL_ID, areaId)
end

HL.Commit(FacRegionUnlockPopupCtrl)
