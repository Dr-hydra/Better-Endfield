
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHongsBusUpgradePopup

FacHongsBusUpgradePopupCtrl = HL.Class('FacHongsBusUpgradePopupCtrl', uiCtrl.UICtrl)

local BUS_FREE_ID = "log_hongs_bus"
local BUS_START_ID = "log_hongs_bus_source"





FacHongsBusUpgradePopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacHongsBusUpgradePopupCtrl.m_busFreeData = HL.Field(HL.Table)

FacHongsBusUpgradePopupCtrl.m_popItemList = HL.Field(HL.Table)

FacHongsBusUpgradePopupCtrl.m_popItemIndex = HL.Field(HL.Number) << 1


FacHongsBusUpgradePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_popItemList = arg.popItemList
    self.m_busFreeData = arg.busFreeData
    self.m_popItemIndex = 1
    local onClose = arg.onClose

    self.view.bgClick.onClick:AddListener(function()
        if self.m_popItemIndex < #self.m_popItemList then
            self.m_popItemIndex = self.m_popItemIndex + 1
            self:_UpdateContent()
        else
            if onClose ~= nil then
                onClose()
            end
            self:PlayAnimationOutAndClose()
        end
    end)

    self:_UpdateContent()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end








FacHongsBusUpgradePopupCtrl._UpdateContent = HL.Method() << function(self)
    local busInfo = self.m_popItemList[self.m_popItemIndex]
    if self.m_busFreeData ~= nil then
        local oldNum = self.m_busFreeData[busInfo.buildingId]
        self.view.mainController:SetState(oldNum == 0 and "unlock" or "uplevel")
        self.view.oldText.text = oldNum
        self.view.newText.text = oldNum + busInfo.count
        self.view.busFree.gameObject:SetActiveIfNecessary(busInfo.buildingId == BUS_FREE_ID)
        self.view.busStart.gameObject:SetActiveIfNecessary(busInfo.buildingId == BUS_START_ID)
        local success, buildingData = Tables.factoryBuildingTable:TryGetValue(busInfo.buildingId)
        if success then
            self.view.buildingName.text = buildingData.name
            local tips = oldNum == 0 and Language.LUA_HONGS_BUS_POPUP_UNLOCK_TIPS or Language.LUA_HONGS_BUS_POPUP_UPLEVEL_TIPS
            self.view.tipsText.text = string.format(tips, buildingData.name)
        end
        self:PlayAnimation(oldNum == 0 and "fachongsbusupgrade_unlockin" or "fachongsbusupgrade_upin")
        AudioAdapter.PostEvent("Au_UI_Popup_FacTechTreePopUpPanel_Open")
    end
end




HL.Commit(FacHongsBusUpgradePopupCtrl)
