local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailRecycleBin

MapMarkDetailRecycleBinCtrl = HL.Class('MapMarkDetailRecycleBinCtrl', uiCtrl.UICtrl)

MapMarkDetailRecycleBinCtrl.m_rewardItemCache = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailRecycleBinCtrl.m_markInstId = HL.Field(HL.String) << ""

MapMarkDetailRecycleBinCtrl.m_recycleBinData = HL.Field(CS.Beyond.Gameplay.RecycleBinData)

MapMarkDetailRecycleBinCtrl.m_domainId = HL.Field(HL.String) << ""

MapMarkDetailRecycleBinCtrl.m_recyclingCor = HL.Field(HL.Thread)





MapMarkDetailRecycleBinCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_RECYCLE_BIN_REMOTE_COLLECTED] = '_UpdateCanPickUp',
}


MapMarkDetailRecycleBinCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_rewardItemCache = UIUtils.genCellCache(self.view.item)
    self.m_markInstId = arg.markInstId

    self:_InitRecycleBinInfo()
    self:_InitController()
end





MapMarkDetailRecycleBinCtrl.OnClose = HL.Override() << function(self)
    if self.m_recyclingCor then
        self.m_recyclingCor = self:_ClearCoroutine(self.m_recyclingCor)
    end
end

MapMarkDetailRecycleBinCtrl._InitRecycleBinInfo = HL.Method() << function(self)
    local markInstId = self.m_markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not getRuntimeDataSuccess then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    local detail = markRuntimeData.detail
    local recycleBinId = detail.systemInstId

    local recycleBinCfg = Tables.recycleBinTable[recycleBinId]
    local recycleBinSystem = GameInstance.player.recycleBinSystem
    local isUnlock, recycleBinData = recycleBinSystem.recycleBins:TryGetValue(recycleBinId)
    local curLv = isUnlock and recycleBinData.lv or 0
    local levelData = recycleBinCfg.levelData
    local descRawText = isUnlock and levelData[curLv].desc or recycleBinCfg.unlockDesc

    self.view.mapMarkDetailCommonStateController:SetState(isUnlock and "Unlocked" or "Locked")
    self.m_recycleBinData = recycleBinData
    self.m_domainId = recycleBinCfg.domainId

    if isUnlock then
        local isMaxLv = recycleBinData.isMaxLv
        self.view.lvStateNode:SetState(isMaxLv and "Max" or "Nrl")
        self.view.lvNumTxt.text = curLv

        local rewardId = levelData[curLv].rewardId
        local rewardBundles = UIUtils.getRewardItems(rewardId)

        self.m_rewardItemCache:Refresh(#rewardBundles, function(cell, luaIndex)
            self.view.mapMarkDetailCommon:InitDetailItem(cell, rewardBundles[luaIndex], {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = self.view.scrollView,
            })
        end)

        self:_UpdateCanPickUp()
        self.m_recyclingCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
                self:_UpdateCanPickUp()
            end
        end)
    end


    self.view.pickupNode.remoteCollectBtn.onClick:AddListener(function()

        local _, domainCfg = Tables.domainDataTable:TryGetValue(self.m_domainId)
        local domainName = domainCfg.domainName
        local availableCollectCount = recycleBinSystem:GetAllCanPickUpInstIdsByDomain(self.m_domainId)
        local remoteCollectLv = self.m_recycleBinData.remoteCollectLv
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(Language.LUA_RECYCLE_BIN_MAP_REMOTE_COLLECT_CONFIRM_POP_UP, domainName, availableCollectCount, remoteCollectLv, domainName),
            onConfirm = function()
                recycleBinSystem:RecycleBinRemoteCollect(self.m_domainId)
            end
        })
    end)

    local _, domainPOICfg = Tables.domainPoiTable:TryGetValue(GEnums.DomainPoiType.RecycleBin)
    local commonArgs = {}
    commonArgs.titleText = domainPOICfg.name .. "#" .. recycleBinCfg.serialId
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = self.m_markInstId
    commonArgs.descText = descRawText
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailRecycleBinCtrl._UpdateCanPickUp = HL.Method() << function(self)
    if not self.m_recycleBinData then
        return
    end

    local cd = self.m_recycleBinData:GetCoolDownBySeconds()
    local canPick = cd <= 0
    self.view.canPickNode.gameObject:SetActive(canPick)
    self.view.recyclingNode.gameObject:SetActive(not canPick)

    if not canPick then
        self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(cd)
    end


    if self.m_recycleBinData.lv == self.m_recycleBinData.remoteCollectLv then

        local recycleBinSystem = GameInstance.player.recycleBinSystem
        if recycleBinSystem:GetAllCanPickUpInstIdsByDomain(self.m_domainId)>0 then
            self.view.pickupNode.stateController:SetState("CanRemote")
        else
            self.view.pickupNode.stateController:SetState("NoRemote")
            self.view.pickupNode.noRemotePickupText.text = Language.LUA_RECYCLE_BIN_MAP_NOTHING_TO_REMOTE_COLLECT
        end
    else
        self.view.pickupNode.stateController:SetState("NoRemote")
        self.view.pickupNode.noRemotePickupText.text = string.format(Language.LUA_RECYCLE_BIN_MAP_NEED_UNLOCK_REMOTE_COLLECT, self.m_recycleBinData.remoteCollectLv)
    end
end

MapMarkDetailRecycleBinCtrl._InitController = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self.view.rewardList.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

HL.Commit(MapMarkDetailRecycleBinCtrl)
