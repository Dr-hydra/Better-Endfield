local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPipe
FacPipeCtrl = HL.Class('FacPipeCtrl', uiCtrl.UICtrl)

local PIPE_SPEED_OVERRIDE = 1000.0





FacPipeCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPipeCtrl.m_buildingInfo = HL.Field(HL.Userdata)

FacPipeCtrl.m_index = HL.Field(HL.Number) << -1

FacPipeCtrl.m_updateThread = HL.Field(HL.Thread)

FacPipeCtrl.m_currentItemId = HL.Field(HL.String) << ""

FacPipeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_buildingInfo = arg.uiInfo
    self.m_index = arg.index

    local pipeData = Tables.factoryLiquidPipeTable:GetValue(self.m_buildingInfo.nodeHandler.templateId).pipeData
    local buildingData = { nodeId = arg.uiInfo.nodeId }
    setmetatable(buildingData, { __index = pipeData })

    self.view.buildingCommon:InitBuildingCommon(nil, {
        data = buildingData,
        customLeftButtonOnClicked = function()
            self:_OnReversePipeButtonClicked()
        end,
        customRightButtonOnClicked = function()
            self:_OnDeletePipeButtonClicked()
        end,
    })
    self.view.delAllButton.onClick:AddListener(function()
        self:_OnDeletePipeButtonClicked(true)
    end)
    self.view.buildingCommon:ChangeBuildingStateDisplay(GEnums.FacBuildingState.Normal)
    self.view.buildingCommon.view.liquidBg:InitFacLiquidBg()

    self:_InitPipeUpdateThread()
end

FacPipeCtrl.OnClose = HL.Override() << function(self)
    self.m_updateThread = self:_ClearCoroutine(self.m_updateThread)
end

FacPipeCtrl._InitPipeUpdateThread = HL.Method() << function(self)

    self:_RefreshPipeBasicDisplayContent()
    self:_RefreshPipeItem(true)
    self:_RefreshLiquidBg()
    self.m_updateThread = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshPipeBasicDisplayContent()
            self:_RefreshPipeItem()
            self:_RefreshLiquidBg()
        end
    end)
end

FacPipeCtrl._RefreshPipeBasicDisplayContent = HL.Method() << function(self)
    local buildingCommonView = self.view.buildingCommon.view


    buildingCommonView.lengthText.text = string.format("%d", self.m_buildingInfo.pipeLength)


    buildingCommonView.currentText.text = string.format("%d", self.m_buildingInfo.currentVolume)
    buildingCommonView.totalText.text = string.format("/%d", self.m_buildingInfo.totalVolume)


    buildingCommonView.speedText.text = string.format("%d", math.floor(1000 * self.m_buildingInfo.speed))
    buildingCommonView.stopLine.gameObject:SetActive(self.m_buildingInfo.speed <= 0)
    buildingCommonView.normalLine.gameObject:SetActive(self.m_buildingInfo.speed > 0)
end

FacPipeCtrl._RefreshPipeItem = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceRefresh)
    local itemId = self.m_buildingInfo.pipeComponentPayload.itemId
    if itemId == self.m_currentItemId and not forceRefresh then
        if not string.isEmpty(self.m_currentItemId) then
            self.view.pipeItem:UpdateCountSimple(self.m_buildingInfo.currentVolume)
        end
        return
    end

    if not string.isEmpty(itemId) then
        self.view.pipeItem:InitItem({
            id = itemId,
            count = self.m_buildingInfo.currentVolume,
        }, true)
        self.view.pipeItem.gameObject:SetActiveIfNecessary(true)

        if self.view.inputGroup.internalEnabled then
            self.view.pipeItem:SetAsNaviTarget()
        end
    else
        self.view.pipeItem.gameObject:SetActiveIfNecessary(false)
        if self.view.inputGroup.internalEnabled then
            self:SetNaviTarget(self.view.emptyItem)
        end
    end

    local itemIcon = self.view.buildingCommon.view.itemIcon
    local success, itemData = Tables.itemTable:TryGetValue(itemId)
    if success then
        local iconId = itemData.iconId
        local sprite = self:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)
        if sprite ~= nil then
            itemIcon.sprite = sprite
            itemIcon.gameObject:SetActiveIfNecessary(true)
        end
    else
        itemIcon.gameObject:SetActiveIfNecessary(false)
    end

    self.m_currentItemId = itemId
end

FacPipeCtrl._RefreshLiquidBg = HL.Method() << function(self)
    local height = 0
    if self.m_buildingInfo.totalVolume > 0 then
        height = self.m_buildingInfo.currentVolume / self.m_buildingInfo.totalVolume
    end
    self.view.buildingCommon.view.liquidBg:RefreshLiquidHeight(height)
end

FacPipeCtrl._OnDeletePipeButtonClicked = HL.Method(HL.Opt(HL.Boolean)) << function(self, isAll)
    if not FactoryUtils.canDelBuilding(self.m_buildingInfo.nodeId, true) then
        return
    end
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    if isAll then
        GameInstance.player.remoteFactory.core:Message_OpDismantle(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
    else
        GameInstance.remoteFactoryManager:DismantleUnitFromConveyor(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId, self.m_index)
    end
end

FacPipeCtrl._OnReversePipeButtonClicked = HL.Method() << function(self)
    PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    GameInstance.player.remoteFactory.core:Message_OpReverseFluidConveyorDirection(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end

HL.Commit(FacPipeCtrl)
