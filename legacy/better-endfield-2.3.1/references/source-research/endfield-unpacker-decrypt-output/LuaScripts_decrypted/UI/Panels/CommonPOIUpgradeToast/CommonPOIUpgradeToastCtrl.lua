local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonPOIUpgradeToast
local PANEL_STATE = {
    Unlock = "Unlock",
    LevelUp = "LevelUp"
}

local GetDescFunc = {
    [GEnums.DomainPoiType.RecycleBin] = "_GetDescRecycleBin",
    [GEnums.DomainPoiType.DomainShop] = "_GetDescDomainShop",
    [GEnums.DomainPoiType.KiteStation] = "_GetDescKiteStation",
    [GEnums.DomainPoiType.DomainDepot] = "_GetDescDomainDepot",
    [GEnums.DomainPoiType.SimulationTraining] = "_GetDescSimulationTraining",
    [GEnums.DomainPoiType.SewageTreatPlant] = "_GetDescSewageTreatPlant",
}


local GetArgsFunc = {
    [GEnums.DomainPoiType.RecycleBin] = function(state, arg)
        local args = {}
        if state == PANEL_STATE.Unlock then
            local _, _, _, _, callback = unpack(arg)
            args.toastFinishCallback = callback
        end
        return args
    end
}

CommonPOIUpgradeToastCtrl = HL.Class('CommonPOIUpgradeToastCtrl', uiCtrl.UICtrl)

local MAIN_HUD_TOAST_TYPE = "CommonPOIUpgradeToast"

CommonPOIUpgradeToastCtrl.m_args = HL.Field(HL.Table)

CommonPOIUpgradeToastCtrl.m_state = HL.Field(HL.String) << ""

CommonPOIUpgradeToastCtrl.m_domainPOIType = HL.Field(GEnums.DomainPoiType)

CommonPOIUpgradeToastCtrl.m_preLv = HL.Field(HL.Number) << 0

CommonPOIUpgradeToastCtrl.m_instId = HL.Field(HL.String) << ""

CommonPOIUpgradeToastCtrl.m_lv = HL.Field(HL.Number) << 0

CommonPOIUpgradeToastCtrl.m_descTxtCellCache = HL.Field(HL.Forward("UIListCache"))

CommonPOIUpgradeToastCtrl.m_showCor = HL.Field(HL.Thread)





CommonPOIUpgradeToastCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}

CommonPOIUpgradeToastCtrl.OnCommonPOIUnlocked = HL.StaticMethod(HL.Opt(HL.Table)) << function(arg)
    if arg == nil then
        return
    end

    local domainPOIType, instId = unpack(arg)
    LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_TOAST_TYPE, function()
        UIManager:Open(PANEL_ID, { domainPOIType, instId, 0, 1})
    end)
end

CommonPOIUpgradeToastCtrl.OnCommonPOILevelUp = HL.StaticMethod(HL.Opt(HL.Table)) << function(arg)
    if arg == nil then
        return
    end

    local domainPOIType, instId, preLv, lv = unpack(arg)
    LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_TOAST_TYPE, function()
        UIManager:Open(PANEL_ID, { domainPOIType, instId, preLv, lv})
    end)
end









CommonPOIUpgradeToastCtrl.OnSewageTreatPlantUnlock = HL.StaticMethod(HL.Table) << function(arg)
    local instId = unpack(arg)
    UIManager:Open(PANEL_ID, { GEnums.DomainPoiType.SewageTreatPlant, instId, 0, 1})
end

CommonPOIUpgradeToastCtrl.OnSewageTreatPlantLevelUp = HL.StaticMethod(HL.Table) << function(arg)
    local instId = unpack(arg)
    local currLevel = FactoryUtils.getSewageTreatPlantLevel(instId)
    UIManager:Open(PANEL_ID, { GEnums.DomainPoiType.SewageTreatPlant, instId, currLevel - 1, currLevel})
end




CommonPOIUpgradeToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_domainPOIType, self.m_instId, self.m_preLv, self.m_lv, self.m_args = unpack(arg)
    self.m_args = self.m_args or {}
    self.m_state = self:_IsStateUnlock() and PANEL_STATE.Unlock or PANEL_STATE.LevelUp
    self.m_descTxtCellCache = UIUtils.genCellCache(self.view.infoNode)

    self:_InitBaseInfo()
    self:_InitDesc()

    self.m_showCor = self:_StartCoroutine(function()
        self:_StartToastShowingProcess()
    end)

    AudioManager.PostEvent(self:_IsStateUnlock() and "Au_UI_Toast_CommonPOIUnlock_Open" or "Au_UI_Toast_CommonPOILevelUp_Open")
end






CommonPOIUpgradeToastCtrl.OnClose = HL.Override() << function(self)
    if self.m_showCor then
        self.m_showCor = self:_ClearCoroutine(self.m_showCor)
    end
end

CommonPOIUpgradeToastCtrl._IsStateUnlock = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_preLv == 0 and self.m_lv == 1
end

CommonPOIUpgradeToastCtrl._StartToastShowingProcess = HL.Method() << function(self)
    local animWrapper = self.animationWrapper


    local inAnimName = string.format("common_poi_%s_in", string.lower(self.m_state))
    local inAnimDuration = animWrapper:GetClipLength(inAnimName)
    animWrapper:Play(inAnimName)
    coroutine.wait(inAnimDuration)


    coroutine.wait(self.view.config.STAY_DURATION)


    local outAnimDuration = self:GetAnimationOutDuration()
    self:PlayAnimationOutWithCallback()
    coroutine.wait(outAnimDuration)

    if self.m_args.toastFinishCallback then
        self.m_args.toastFinishCallback()
    end
    self:Close()

    if LuaSystemManager.mainHudActionQueue:HasRequest(MAIN_HUD_TOAST_TYPE) then
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
    end
end

CommonPOIUpgradeToastCtrl._InitBaseInfo = HL.Method() << function(self)
    self.view.content:SetState(self.m_state)

    local domainPOITypeCfg = Tables.domainPoiTable[self.m_domainPOIType]
    if self.m_state == PANEL_STATE.Unlock then
        self.view.nameTxt.text = domainPOITypeCfg.unlockToastTitle
    elseif self.m_state == PANEL_STATE.LevelUp then
        self.view.nameTxt.text = domainPOITypeCfg.upgradeToastTitle

        self.view.preLvTxt.text = self.m_preLv
        self.view.curLvTxt.text = self.m_lv
    end

    if not string.isEmpty(domainPOITypeCfg.upgradeToastIcon) then
        self.view.icon:LoadSprite(UIConst.UI_SPRITE_COMMON_POI_UPGRADE_TOAST, domainPOITypeCfg.upgradeToastIcon)
    end
end

CommonPOIUpgradeToastCtrl._InitDesc = HL.Method() << function(self)
    local funcName = GetDescFunc[self.m_domainPOIType]
    if not funcName then
        logger.error("[CommonPOIUpgradeToastCtrl] GetDescFunc定义缺失，类型为：", self.m_domainPOIType)
        return
    end

    local descList = self[funcName](self)
    self.m_descTxtCellCache:RefreshCoroutine(#descList, 0.1, function(cell, index)
        if type(descList[index])=="table" then
            cell.infoTxt.text = descList[index][1]
            cell.stateController:SetState(descList[index][2])
        else
            cell.infoTxt.text = descList[index]
            cell.stateController:SetState("Normal")
        end
    end)
end

CommonPOIUpgradeToastCtrl._GetDescRecycleBin = HL.Method().Return(HL.Table) << function(self)
    local descList = {}
    if self.m_state == PANEL_STATE.Unlock then
        table.insert(descList, Language["ui_recycling_upgradtoast_unlock_effect"])
    elseif self.m_state == PANEL_STATE.LevelUp then

        local _, recycleBinData = GameInstance.player.recycleBinSystem.recycleBins:TryGetValue(self.m_instId)
        if self.m_lv == recycleBinData.remoteCollectLv then
            table.insert(descList, {Language.LUA_RECYCLE_BIN_POI_REMOTE_COLLECT_UPGRADE_TOAST, "Special"})
        end
        table.insert(descList, Language["ui_recycling_upgradtoast_levelup_effect"])
    end

    return descList
end

CommonPOIUpgradeToastCtrl._GetDescDomainShop = HL.Method().Return(HL.Table)  << function(self)
    local descList = {}

    if self.m_state == PANEL_STATE.Unlock then
        table.insert(descList, Language.LUA_DOMAIN_SHOP_UPGRADE_TOAST_UNLOCK)
    else
        table.insert(descList, Language.LUA_DOMAIN_SHOP_UPGRADE_TOAST_LEVEL_UP)
    end

    return descList
end

CommonPOIUpgradeToastCtrl._GetDescKiteStation = HL.Method().Return(HL.Table) << function(self)
    local descList = {}
    if self.m_state == PANEL_STATE.Unlock then
        table.insert(descList, Language.LUA_KITE_STATION_UNLOCKED_TIP)
    elseif self.m_state == PANEL_STATE.LevelUp then

        local preSlotCount = Tables.kiteStationLevelTable[self.m_instId].list[self.m_preLv].entrustSlotCnt
        local curSlotCount = Tables.kiteStationLevelTable[self.m_instId].list[self.m_lv].entrustSlotCnt

        if curSlotCount == preSlotCount then
            table.insert(descList, Language.LUA_KITE_STATION_LEVEL_TIP)
        else
            table.insert(descList, Language.LUA_KITE_STATION_LEVEL_TIP_SLOT_CHANGE)
        end

    end

    return descList
end

CommonPOIUpgradeToastCtrl._GetDescDomainDepot = HL.Method().Return(HL.Table) << function(self)
    local descList = {}
    local domainDepotId = self.m_instId
    local domainDepotData = GameInstance.player.domainDepotSystem:GetDomainDepotDataById(domainDepotId)
    local levelSuccess, domainDepotLevelList = Tables.domainDepotLevelTable:TryGetValue(domainDepotId)
    if not levelSuccess or domainDepotData == nil then
        return descList
    end

    domainDepotLevelList = domainDepotLevelList.levelList
    local currLevel = domainDepotData.level
    local currLevelConfig = domainDepotLevelList[currLevel]
    local currExtraDepotLimit = currLevelConfig.extraDepotLimit
    local currDeliverItemTypeCount = currLevelConfig.deliverItemTypeList.Count
    local currDeliverPackTypeCount = currLevelConfig.deliverPackTypeList.Count
    local lastExtraDepotLimit, lastDeliverItemTypeCount, lastDeliverPackTypeCount

    if self.m_state == PANEL_STATE.Unlock then
        lastExtraDepotLimit, lastDeliverItemTypeCount, lastDeliverPackTypeCount = 0, 0, 0
    else
        local lastLevel = currLevel - 1
        local lastLevelConfig = domainDepotLevelList[lastLevel]
        lastExtraDepotLimit = lastLevelConfig.extraDepotLimit
        lastDeliverItemTypeCount = lastLevelConfig.deliverItemTypeList.Count
        lastDeliverPackTypeCount = lastLevelConfig.deliverPackTypeList.Count
    end

    if lastDeliverItemTypeCount == 0 and currDeliverItemTypeCount > 0 then
        table.insert(descList, Language.LUA_DOMAIN_DEPOT_TOAST_DELIVER_UNLOCK)
    end

    if lastExtraDepotLimit < currExtraDepotLimit then
        table.insert(descList, Language.LUA_DOMAIN_DEPOT_TOAST_EXTRA_DEPOT_LIMIT)
    end

    if lastDeliverItemTypeCount < currDeliverItemTypeCount then
        table.insert(descList, Language.LUA_DOMAIN_DEPOT_TOAST_DELIVER_ITEM_TYPE)
    end

    if lastDeliverPackTypeCount < currDeliverPackTypeCount then
        table.insert(descList, Language.LUA_DOMAIN_DEPOT_TOAST_DELIVER_PACK_TYPE)
    end

    return descList
end


CommonPOIUpgradeToastCtrl._GetDescSimulationTraining = HL.Method().Return(HL.Table) << function(self)
    local descList = {}

    local simulationTrainingSystem = GameInstance.player.simulationTrainingSystem
    local curLevel = simulationTrainingSystem.curLevel

    if curLevel == 1 then
        table.insert(descList, Language.ui_simulationtraining_unlock_mainhud_toast)
    else
        table.insert(descList, Language.ui_simulationtraining_upgrade_mainhud_toast)
    end

    return descList
end
CommonPOIUpgradeToastCtrl._GetDescSewageTreatPlant = HL.Method().Return(HL.Table) << function(self)
    local descList = {}

    local plantCfg = Tables.factorySewageTreatPlantStoreTable[self.m_instId]
    for index = 0, plantCfg.levelList.Count - 1 do
        if plantCfg.levelList[index].level == self.m_lv then
            table.insert(descList, plantCfg.levelList[index].levelDesc)
            break
        end
    end

    return descList
end

CommonPOIUpgradeToastCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self:Close()
end
HL.Commit(CommonPOIUpgradeToastCtrl)
