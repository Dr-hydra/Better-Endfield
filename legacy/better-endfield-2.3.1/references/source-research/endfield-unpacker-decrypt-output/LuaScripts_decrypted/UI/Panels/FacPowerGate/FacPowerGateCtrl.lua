
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerGate
FacPowerGateCtrl = HL.Class('FacPowerGateCtrl', uiCtrl.UICtrl)







FacPowerGateCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPowerGateCtrl.m_toMapId = HL.Field(HL.String) << ""

FacPowerGateCtrl.m_toIconId = HL.Field(HL.String) << ""


FacPowerGateCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local nodeId = arg.uiInfo.nodeId
    local uiInfo = arg.uiInfo

    self.view.buildingCommon:InitBuildingCommon(uiInfo)

    self:_RefreshPowerInfo()
    self:_RefreshPowerGate(nodeId)
    self:_InitActionEvent()
end

FacPowerGateCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.offlineNode.rightInfo.btnMap.onClick:AddListener(function()
        self:_ShowToMap()
    end)
    self.view.onlineNode.rightInfo.btnMap.onClick:AddListener(function()
        self:_ShowToMap()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacPowerGateCtrl._RefreshPowerGate = HL.Method(HL.Number) << function(self, fromNodeId)
    local _, fromInstKey = GameAction.Factory.TryGetDpInstKey(fromNodeId)
    if not fromInstKey then
        logger.error(string.format("FacPowerGateCtrl->Can't find dpNodeId, fromNodeId: %s", fromNodeId))
        return
    end

    local _, fromPowerGateMapCfg =  GameWorld.worldInfo.curLevel.levelData.factoryPredefineData.powerGateTable:TryGetValue(fromInstKey)
    if not fromPowerGateMapCfg then
        logger.error(string.format("FacPowerGateCtrl->Can't find cfg in powerGateTable, fromInstKey: %s", fromInstKey))
        return
    end

    local toDPNodeId = fromPowerGateMapCfg.toInstKey
    local _, fromGateCfg = Tables.factorySpecialPowerPoleTable:TryGetValue(fromInstKey)
    local _, toGateCfg = Tables.factorySpecialPowerPoleTable:TryGetValue(toDPNodeId)
    if (not fromGateCfg) or (not toGateCfg) then
        logger.error(string.format("FacPowerGateCtrl->Can't find cfg in specialPowerPole, fromInstKey: %s, toDPNodeId: %s", fromInstKey, toDPNodeId))
        return
    end

    self:_RefreshPositionInfo(self.view.offlineNode.leftInfo, fromGateCfg)
    self:_RefreshPositionInfo(self.view.onlineNode.leftInfo, fromGateCfg)
    self:_RefreshPositionInfo(self.view.offlineNode.rightInfo, toGateCfg)
    self:_RefreshPositionInfo(self.view.onlineNode.rightInfo, toGateCfg)

    local toNodeId = CSFactoryUtil.GetConnectedPowerGateNodeId(fromNodeId)
    local currentChapterId = Utils.getCurrentChapterId()
    self.m_toMapId = toGateCfg.mapId
    self.m_toIconId = GameInstance.player.mapManagerOdd:GetFactoryMarkInsId(currentChapterId, toNodeId)

    self.view.buildingCommon.view.machineName.text = fromGateCfg.buildingName

    local state = FactoryUtils.getBuildingStateType(fromNodeId)
    local isNormal = state == GEnums.FacBuildingState.Normal
    self.view.onlineNode.gameObject:SetActive(isNormal)
    self.view.offlineNode.gameObject:SetActive(not isNormal)
end

FacPowerGateCtrl._RefreshPositionInfo = HL.Method(HL.Table, HL.Userdata) << function(self, entranceCell, positionInfo)
    entranceCell.entranceNameTxt.text = positionInfo.positionDesc
    entranceCell.mapNameTxt.text = positionInfo.mapName
end

FacPowerGateCtrl._RefreshPowerInfo = HL.Method() << function(self)
    local powerInfo = FactoryUtils.getCurRegionPowerInfo()
    local powerStorageCapacity = powerInfo.powerSaveMax
    local restPower = powerInfo.powerSaveCurrent

    self.view.maxRestPowerText.text = string.format("/%s", UIUtils.getNumString(powerStorageCapacity))
    self.view.providePowerText.text = string.format("/%s", UIUtils.getNumString(powerInfo.powerGen))
    self.view.currentPowerText.text = UIUtils.getNumString(powerInfo.powerCost)
    self.view.restPowerText.color = restPower <= 0 and self.view.config.COLOR_POWER_SHORTAGE or self.view.config.COLOR_POWER_ENOUGH
    self.view.restPowerText.text = UIUtils.getNumString(restPower)
end

FacPowerGateCtrl._ShowToMap = HL.Method() << function(self)
    local isMapUnlock = self.m_toIconId and self.m_toMapId and
        GameInstance.player.mapManagerOdd:CheckLevelUnlock(self.m_toMapId) and
        GameInstance.player.mapManagerOdd:HasMarkIcon(self.m_toIconId)
    if not isMapUnlock then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_POWER_GATE_ON_MAP_LOCKED)
        return
    end
    MapUtils.openMap(self.m_toIconId)
end

HL.Commit(FacPowerGateCtrl)
