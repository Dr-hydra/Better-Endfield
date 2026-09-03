
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassPlan

BattlePassPlanCtrl = HL.Class('BattlePassPlanCtrl', uiCtrl.UICtrl)






BattlePassPlanCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_PASS_LEVEL_UPDATE] = '_OnLevelUpdate',
    [MessageConst.ON_BATTLE_PASS_TRACK_UPDATE] = '_OnTrackUpdate',
    [MessageConst.ON_BATTLE_PASS_SHOW_REWARD] = '_OnRewardShow',
    [MessageConst.ON_BATTLE_PASS_ADVANCED_BUY_CLOSE] = '_TriggerBuyTrack'
}

BattlePassPlanCtrl.m_levelInfos = HL.Field(HL.Any)

BattlePassPlanCtrl.m_levelInfoMap = HL.Field(HL.Any)

BattlePassPlanCtrl.m_milestoneInfos = HL.Field(HL.Any)

BattlePassPlanCtrl.m_buyHintInfos = HL.Field(HL.Table)

BattlePassPlanCtrl.m_validTrack = HL.Field(HL.Table)

BattlePassPlanCtrl.m_allTrack = HL.Field(HL.Table)

BattlePassPlanCtrl.m_bannerInfos = HL.Field(HL.Any)

BattlePassPlanCtrl.m_contentCellFunc = HL.Field(HL.Function)

BattlePassPlanCtrl.m_focusIndex = HL.Field(HL.Number) << -1

BattlePassPlanCtrl.m_milestoneIndex = HL.Field(HL.Number) << -1

BattlePassPlanCtrl.m_isGainAll = HL.Field(HL.Boolean) << false

BattlePassPlanCtrl.m_isGainMilestone = HL.Field(HL.Boolean) << false

BattlePassPlanCtrl.m_buyHintType = HL.Field(HL.Any) << nil

BattlePassPlanCtrl.m_seasonData = HL.Field(HL.Any)

BattlePassPlanCtrl.m_naviTarget = HL.Field(HL.Any) << nil

BattlePassPlanCtrl.m_trackBuyFlags = HL.Field(HL.Table) << nil


BattlePassPlanCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitViews(arg)
    self:_LoadData()
    self:_RenderViews()
    self:_ScrollToDefault()
    self:_PlayTrackUnlockAnimOnCreate(arg)
    self:_TryRecoverSubPanel(arg and arg.panelArgs)
end

BattlePassPlanCtrl.OnShow = HL.Override() << function(self)
    self:_NaviResume()
    self.view.bannerNode:SetPause(false)
end

BattlePassPlanCtrl.OnHide = HL.Override() << function(self)
    self.view.bannerNode:SetPause(true)
end

BattlePassPlanCtrl.OnClose = HL.Override() << function(self)
    self.view.bannerNode:OnDestroy()
end

BattlePassPlanCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    self.view.bannerNode:SetPause(false)
end

BattlePassPlanCtrl._InitViews = HL.Method(HL.Any) << function(self, arg)
    local naviGroupIds = {}
    table.insert(naviGroupIds, self.view.inputGroup.groupId)
    if arg ~= nil and arg.baseNaviGroupId ~= nil then
        table.insert(naviGroupIds, arg.baseNaviGroupId)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(naviGroupIds)
    self.view.bannerNode:InitCommonBannerWidget({
        onUpdateCell = function(cell, luaIndex)
            self:_UpdateBannerCell(cell, luaIndex)
        end,
        onPageChange = function(oldIndex, newIndex)
            self:_OnBannerChange(oldIndex, newIndex)
        end,
        isWrappedLoop = true,
    })
    self.view.contentListScrollRect.onValueChanged:AddListener(function(normalizedPosition)
        self:_OnContentValueChanged()
    end)
    self.m_contentCellFunc = UIUtils.genCachedCellFunction(self.view.contentList)
    self.view.contentList.onUpdateCell:RemoveAllListeners()
    self.view.contentList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_UpdateContentCell(self.m_contentCellFunc(gameObject), LuaIndex(csIndex))
    end)

    local freeTrackData = self:_FindTrackDataByType(GEnums.BPTrackType.FREE)
    local orgTrackData = self:_FindTrackDataByType(GEnums.BPTrackType.ORIGINIUM)
    local payTrackData = self:_FindTrackDataByType(GEnums.BPTrackType.PAY)
    self.view.baseTitleText.text = freeTrackData ~= nil and freeTrackData.name or ''
    self.view.rationTitleText.text = orgTrackData ~= nil and orgTrackData.name or ''
    self.view.customizeTitleText.text = payTrackData ~= nil and payTrackData.name or ''

    self.view.receiveBtn.onClick:RemoveAllListeners()
    self.view.receiveBtn.onClick:AddListener(function()
        self:_OnTakeAllRewards()
    end)

    self.view.checkBtn.onClick:RemoveAllListeners()
    self.view.checkBtn.onClick:AddListener(function()
        self:_OnClickPlanBtn()
    end)
    self.view.planBtn.onClick:RemoveAllListeners()
    self.view.planBtn.onClick:AddListener(function()
        self:_OnClickPlanBtn()
    end)
    self.view.rationBtn.onClick:RemoveAllListeners()
    self.view.rationBtn.onClick:AddListener(function()
        self:_OnClickOrgPlan()
    end)
    self.view.customizeBtn.onClick:RemoveAllListeners()
    self.view.customizeBtn.onClick:AddListener(function()
        self:_OnClickPayPlan()
    end)

    self.view.contentNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self.m_naviTarget = nil
        end
    end)
    self.view.contentNaviGroup.onSetLayerSelectedTarget:AddListener(function(target)
        self.m_naviTarget = target
    end)

    self.m_allTrack = {}
    table.insert(self.m_allTrack, GEnums.BPTrackType.FREE)
    table.insert(self.m_allTrack, GEnums.BPTrackType.ORIGINIUM)
    table.insert(self.m_allTrack, GEnums.BPTrackType.PAY)
end

BattlePassPlanCtrl._ScrollToDefault = HL.Method() << function(self)

    local hasAvail, firstAvailLevel = BattlePassUtils.CheckHasAvailBpPlanReward()
    local focusLevel = firstAvailLevel
    if hasAvail and firstAvailLevel > 0 then
        focusLevel = firstAvailLevel
    else
        local hasLast, lastLevel = BattlePassUtils.GetLastGainBpPlanLevel()
        if hasLast then
            focusLevel = lastLevel
        else
            focusLevel = 1
        end
    end
    self.view.contentList:ScrollToIndex(CSIndex(focusLevel), true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)


    local defaultIndex= 1
    for index, bannerInfo in ipairs(self.m_bannerInfos) do
        if bannerInfo.isGain ~= true then
            defaultIndex = index
            break
        end
    end
    self.view.bannerNode:ScrollToIndex(defaultIndex)


    if DeviceInfo.usingController then
        self.view.contentList:UpdateShowingCells(function(csIndex, obj)
            local luaIndex = LuaIndex(csIndex)
            local cell = self.m_contentCellFunc(obj)
            local levelInfo = self.m_levelInfos[luaIndex]
            local isLoop = levelInfo.isLoop == true
            self:_UpdateContentCell(cell, luaIndex)
            if luaIndex == focusLevel then
                if isLoop then
                    cell.battlePassPlanLoopCell:SetAsNaviFocusCell()
                else
                    cell.battlePassPlanCell:SetAsNaviFocusCell()
                end
            end
        end)
    end
end

BattlePassPlanCtrl._PlayTrackUnlockAnimOnCreate = HL.Method(HL.Table) << function(self, arg)
    if arg == nil then
        return
    end
    if arg.panelArgs == nil or arg.panelArgs.showTrackUnlockType == nil then
        return
    end
    if arg.popupPlanBuy == true then

        self.m_trackBuyFlags[arg.panelArgs.showTrackUnlockType] = true
    else

        if arg.panelArgs.showTrackUnlockType == GEnums.BPTrackType.ORIGINIUM then
            self:_PlayBuyTrackAnim(true, false)
        elseif arg.panelArgs.showTrackUnlockType == GEnums.BPTrackType.PAY then
            self:_PlayBuyTrackAnim(false, true)
        end
    end
end

BattlePassPlanCtrl._NaviResume = HL.Method() << function(self)
    local lastNaviTarget = self.m_naviTarget
    if lastNaviTarget ~= nil then
        self:SetNaviTarget(lastNaviTarget)
    end
end

BattlePassPlanCtrl._FindTrackDataByType = HL.Method(HL.Any).Return(HL.Any) << function(self, trackType)
    for _, trackData in pairs(Tables.battlePassTrackTable) do
        if trackData.trackType == trackType then
            return trackData
        end
    end
    return nil
end

BattlePassPlanCtrl._LoadData = HL.Method() << function(self)
    self.m_trackBuyFlags = {}
    local bpSystem = GameInstance.player.battlePassSystem
    if string.isEmpty(bpSystem.seasonData.seasonId) then
        return
    end
    local hasSeason, seasonData = Tables.battlePassSeasonTable:TryGetValue(bpSystem.seasonData.seasonId)
    if not hasSeason then
        return
    end
    self.m_seasonData = seasonData
    self:_LoadLevelData()
    self:_LoadBannerData()
end

BattlePassPlanCtrl._LoadLevelData = HL.Method() << function(self)
    self.m_milestoneInfos = {}
    self.m_levelInfos = {}
    self.m_levelInfoMap = {}
    self.m_buyHintInfos = {}
    local hasGroup, levelGroup = Tables.battlePassLevelTable:TryGetValue(self.m_seasonData.levelGroupId)
    if not hasGroup then
        return
    end

    local hasOverrideGroup, overrideGroup = Tables.battlePassOverrideLevelTable:TryGetValue(self.m_seasonData.ovrLvRewardGroupId)

    for level, levelData in pairs(levelGroup.levelInfos) do
        local levelInfo = {}
        levelInfo.level = level
        levelInfo.isLoop = levelData.isRecurring
        levelInfo.isMilestone = levelData.isMilestone
        levelInfo.toNextExp = levelData.levelExp
        local freeRewardId = levelData.freeRewardId
        local originiumRewardId = levelData.originiumRewardId
        local payRewardId = levelData.payRewardId
        if hasOverrideGroup then
            local hasOverrideLevel, overrideLevelData = overrideGroup.levelInfos:TryGetValue(level)
            if hasOverrideLevel then
                freeRewardId = overrideLevelData.freeRewardId
                originiumRewardId = overrideLevelData.originiumRewardId
                payRewardId = overrideLevelData.payRewardId
            end
        end
        levelInfo.itemBundles = {}
        freeRewardId = freeRewardId == nil and '' or freeRewardId
        local hasFree, freeReward = Tables.rewardTable:TryGetValue(freeRewardId)
        if hasFree and freeReward.itemBundles.Count > 0 then
            levelInfo.itemBundles[GEnums.BPTrackType.FREE] = freeReward.itemBundles[0]
        end
        originiumRewardId = originiumRewardId == nil and '' or originiumRewardId
        local hasOrg, orgReward = Tables.rewardTable:TryGetValue(originiumRewardId)
        if hasOrg and orgReward.itemBundles.Count > 0 then
            levelInfo.itemBundles[GEnums.BPTrackType.ORIGINIUM] = orgReward.itemBundles[0]
        end
        payRewardId = payRewardId == nil and '' or payRewardId
        local hasPay, payReward = Tables.rewardTable:TryGetValue(payRewardId)
        if hasPay and payReward.itemBundles.Count > 0 then
            levelInfo.itemBundles[GEnums.BPTrackType.PAY] = payReward.itemBundles[0]
        end
        if levelData.isMilestone then
            table.insert(self.m_milestoneInfos, levelInfo)
        end
        if levelData.buyHintType ~= GEnums.BPBuyHintType.None then
            self.m_buyHintInfos[level] = levelData.buyHintType
        end
        table.insert(self.m_levelInfos, levelInfo)
        self.m_levelInfoMap[level] = levelInfo
    end
    table.sort(self.m_milestoneInfos, Utils.genSortFunction({"level"}, true))
    table.sort(self.m_levelInfos, Utils.genSortFunction({"level"}, true))
    self:_RefreshLevelGain(true)
end

BattlePassPlanCtrl._RefreshLevelGain = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local bpSystem = GameInstance.player.battlePassSystem
    local trackTable = Tables.battlePassTrackTable
    local prevBuy = {}
    if isInit ~= true then
        for _, trackType in pairs(self.m_allTrack) do
            if trackType ~= GEnums.BPTrackType.FREE then
                prevBuy[trackType] = self.m_validTrack ~= nil and self.m_validTrack[trackType] ~= nil
            end
        end
    end
    self.m_validTrack = {}
    for trackId, playerTrack in pairs(bpSystem.trackData.trackRewards) do
        local hasTrack, trackData = trackTable:TryGetValue(trackId)
        if hasTrack then
            self.m_validTrack[trackData.trackType] = playerTrack
        end
    end
    if isInit ~= true then

        for _, trackType in pairs(self.m_allTrack) do
            if trackType ~= GEnums.BPTrackType.FREE then
                local prevNotBuy = prevBuy[trackType] ~= true
                local currBuy = self.m_validTrack[trackType] ~= nil
                if prevNotBuy and currBuy then
                    self.m_trackBuyFlags[trackType] = true
                end
            end
        end
    end
    for _, levelInfo in ipairs(self.m_levelInfos) do
        levelInfo.gainInfo = {}
        levelInfo.canObtainInfo = {}
        levelInfo.overrideCount = {}
        levelInfo.activeInfo = {}
        if levelInfo.isLoop then
            local isMaxLevel = bpSystem.levelData.currLevel >= self.m_seasonData.maxLevel
            local recruitAllTime = (isMaxLevel and levelInfo.toNextExp > 0) and (bpSystem.levelData.currExp // levelInfo.toNextExp) or 0
            for _, trackType in pairs(self.m_allTrack) do
                levelInfo.overrideCount[trackType] = recruitAllTime > 0 and recruitAllTime or 0
                local playerTrack = self.m_validTrack[trackType]
                local isActive = playerTrack ~= nil
                local recruitTime = 0
                if isActive then
                    recruitTime = playerTrack.recurringTimes
                    local canObtain = recruitAllTime > recruitTime
                    local isGain = recruitAllTime <= recruitTime
                    levelInfo.gainInfo[trackType] = isGain
                    levelInfo.canObtainInfo[trackType] = canObtain and isActive
                else
                    levelInfo.gainInfo[trackType] = true
                    levelInfo.canObtainInfo[trackType] = false
                end
                levelInfo.activeInfo[trackType] = isActive and isMaxLevel
                levelInfo.overrideCount[trackType] = levelInfo.overrideCount[trackType] - recruitTime
            end
        else
            local canObtain = bpSystem.levelData.currLevel >= levelInfo.level
            for trackType, playerTrack in pairs(self.m_validTrack) do
                local isGain = playerTrack.rewardGainedLevel:Contains(levelInfo.level)
                levelInfo.activeInfo[trackType] = canObtain
                levelInfo.gainInfo[trackType] = isGain
                levelInfo.canObtainInfo[trackType] = canObtain
            end
        end
    end
end

BattlePassPlanCtrl._LoadBannerData = HL.Method() << function(self)
    self.m_bannerInfos = {}
    local bpSystem = GameInstance.player.battlePassSystem
    if string.isEmpty(bpSystem.seasonData.seasonId) then
        return
    end
    local hasBanner, bannerGroup = Tables.battlePassBannerTable:TryGetValue(self.m_seasonData.bannerPresetId)
    if not hasBanner then
        return
    end
    for _, bannerData in pairs(bannerGroup.bannerInfos) do
        local trackType = GEnums.BPTrackType.FREE
        local succ, trackData = Tables.battlePassTrackTable:TryGetValue(bannerData.trackId)
        if succ then
            trackType = trackData.trackType
        end
        local blocked = trackType == GEnums.BPTrackType.PAY and BattlePassUtils.CheckBattlePassPurchaseBlock()
        if not blocked then
            local bannerInfo = {
                iconId = bannerData.iconId,
                labelTip = bannerData.labelTip,
                desc = bannerData.desc,
                itemId = bannerData.itemId,
                sortId = bannerData.sortId,
                trackId = bannerData.trackId,
                trackType = trackType,
            }
            table.insert(self.m_bannerInfos, bannerInfo)
        end
    end
    table.sort(self.m_bannerInfos, Utils.genSortFunction({"sortId"}, true))
    local hasGroup, levelGroup = Tables.battlePassLevelTable:TryGetValue(self.m_seasonData.levelGroupId)
    if not hasGroup then
        return
    end
    local rewardItemMap = {}
    for _, trackType in ipairs(self.m_allTrack) do
        rewardItemMap[trackType] = {}
    end

    for _, levelInfo in ipairs(self.m_levelInfos) do
        for trackType, itemBundle in pairs(levelInfo.itemBundles) do
            local trackMap = rewardItemMap[trackType]
            if not string.isEmpty(itemBundle.id) and trackMap[itemBundle.id] == nil then
                trackMap[itemBundle.id] = levelInfo.level
            end
        end
    end
    for _, bannerInfo in ipairs(self.m_bannerInfos) do
        local trackMap = rewardItemMap[bannerInfo.trackType]
        if not string.isEmpty(bannerInfo.itemId) and bannerInfo.level == nil and trackMap[bannerInfo.itemId] ~= nil then
            bannerInfo.level = trackMap[bannerInfo.itemId]
        end
    end
    self:_RefreshBannerGain()
end

BattlePassPlanCtrl._RefreshBannerGain = HL.Method() << function(self)
    local bpSystem = GameInstance.player.battlePassSystem
    for _, bannerInfo in ipairs(self.m_bannerInfos) do
        if bannerInfo.level ~= nil then
            local succ, playerTrack = bpSystem.trackData.trackRewards:TryGetValue(bannerInfo.trackId)
            if succ then
                bannerInfo.isGain = playerTrack.rewardGainedLevel:Contains(bannerInfo.level)
            end
        else
            local succ, playerTrack = bpSystem.trackData.trackRewards:TryGetValue(bannerInfo.trackId)
            bannerInfo.isGain = succ
        end
    end
end

BattlePassPlanCtrl._RenderViews = HL.Method(HL.Opt(HL.Boolean)) << function(self, isRefresh)
    if isRefresh then
        self.view.bannerNode:Refresh()
        self.view.contentList:UpdateShowingCells(function(csIndex, obj)
            self:_UpdateContentCell(self.m_contentCellFunc(obj), LuaIndex(csIndex))
        end)
    else
        self.view.bannerNode:UpdateCount(#self.m_bannerInfos)
        self.view.contentList:UpdateCount(#self.m_levelInfos)
    end
    local hasAvailReward = BattlePassUtils.CheckHasAvailBpPlanReward()
    self:_OnContentValueChanged(true)
    self.view.receiveBtn.gameObject:SetActive(hasAvailReward)
    self.view.rationLockNode.gameObject:SetActive(self.m_validTrack[GEnums.BPTrackType.ORIGINIUM] == nil)
    self.view.customizeLockNode.gameObject:SetActive(self.m_validTrack[GEnums.BPTrackType.PAY] == nil)

    local hasOrg = self.m_validTrack[GEnums.BPTrackType.ORIGINIUM] ~= nil
    local hasPay = self.m_validTrack[GEnums.BPTrackType.PAY] ~= nil
    local showCheck = hasOrg and (hasPay or BattlePassUtils.CheckBattlePassPurchaseBlock())
    self.view.planBtn.gameObject:SetActive(not showCheck)
    self.view.checkBtn.gameObject:SetActive(showCheck)
    if not showCheck then
        local btnState = "Both"
        if hasPay and not hasOrg then
            btnState = "Originium"
        elseif hasOrg and not hasPay and not BattlePassUtils.CheckBattlePassPurchaseBlock() then
            btnState = "Pay"
        end
        self.view.planBtnStateController:SetState(btnState)
    end
end

BattlePassPlanCtrl._UpdateBannerCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local bannerInfo = self.m_bannerInfos[luaIndex]
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        if bannerInfo.level == nil then
            if bannerInfo.trackType == GEnums.BPTrackType.ORIGINIUM then
                self:_OnClickOrgPlan()
            elseif bannerInfo.trackType == GEnums.BPTrackType.PAY then
                self:_OnClickPayPlan()
            else
                self:_OnClickPlanBtn()
            end
        else
            self.view.contentList:ScrollToIndex(bannerInfo.level - 1, false, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
        end
    end)
end

BattlePassPlanCtrl._UpdateContentCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local levelInfo = self.m_levelInfos[luaIndex]
    local itemBundles = self:_ConstructItemBundles(levelInfo)
    local isLoop = levelInfo.isLoop == true
    cell.stateController:SetState(isLoop and "Loop" or "Normal")
    if isLoop then
        cell.battlePassPlanLoopCell:InitBattlePassPlanCell(itemBundles, levelInfo, function(trackId, level)
            self:_OnTakeReward(trackId, level)
        end)
    else
        cell.battlePassPlanCell:InitBattlePassPlanCell(itemBundles, levelInfo, function(trackId, level)
            self:_OnTakeReward(trackId, level)
        end)
    end
end

BattlePassPlanCtrl._ConstructItemBundles = HL.Method(HL.Any).Return(HL.Any) << function(self, levelInfo)
    local itemBundles = {}
    for _, trackType in pairs(self.m_allTrack) do
        local item = levelInfo.itemBundles[trackType]
        local hasItem = item ~= nil
        local trackData = self:_FindTrackDataByType(trackType)
        local isOverrideCount = levelInfo.overrideCount[trackType] ~= nil
        local overrideCount = isOverrideCount and levelInfo.overrideCount[trackType] or 0
        local count = isOverrideCount and overrideCount or (hasItem and item.count or 0)
        local itemBundle = {
            id = hasItem and item.id or '',
            obtained = levelInfo.gainInfo[trackType] == true,
            canObtain = levelInfo.canObtainInfo[trackType] == true,
            isUnlocked = levelInfo.activeInfo[trackType] == true,
            trackId = trackData ~= nil and trackData.trackId or nil,
        }
        if count > 0 then
            itemBundle.count = count
        end
        table.insert(itemBundles, itemBundle)
    end
    return itemBundles
end

BattlePassPlanCtrl._OnContentValueChanged = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceRefresh)
    local selectIndex = self.view.contentList:GetCenterIndex()
    local floorIndex = math.floor(selectIndex - 0.65)
    local luaIndex = LuaIndex(floorIndex)

    if luaIndex ~= self.m_focusIndex or forceRefresh then
        self.m_focusIndex = luaIndex
        if self.m_levelInfos == nil then
            return
        end
        local levelInfo = self.m_levelInfos[luaIndex]
        if levelInfo == nil then
            return
        end
        local mileStoneIndex = -1
        for index, milestoneInfo in ipairs(self.m_milestoneInfos) do
            if levelInfo.level < milestoneInfo.level then
                mileStoneIndex = index
                break
            end
        end
        if mileStoneIndex > 0 then
            if mileStoneIndex ~= self.m_milestoneIndex or forceRefresh then
                self.m_milestoneIndex = mileStoneIndex
                local milestoneInfo = self.m_milestoneInfos[mileStoneIndex]
                local itemBundles = self:_ConstructItemBundles(milestoneInfo)
                self.view.leftNode:SetState("Exists")
                self.view.bigRewardCell:InitBattlePassPlanCell(itemBundles, milestoneInfo, function(trackId, level)
                    self:_OnTakeReward(trackId, level)
                end)
            end
            return
        end
        self.m_milestoneIndex = -1
        self.view.leftNode:SetState("DoesNotExist")
    end
end

BattlePassPlanCtrl._OnBannerChange = HL.Method(HL.Number, HL.Number) << function(self, oldIndex, newIndex)
    if self.m_bannerInfos == nil then
        return
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local bannerInfo = self.m_bannerInfos[newIndex]
    if bannerInfo == nil then
        return
    end

    if oldIndex ~= newIndex then
        self.view.bannerNodeAnimationWrapper:ClearTween()
        self.view.bannerNodeAnimationWrapper:PlayWithTween("bp_banner_switch")
        local isFrontPanel = self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        if isFrontPanel then
            AudioAdapter.PostEvent("Au_UI_Event_BP_Glitch")
        end
    end

    local prevBannerPicId = bannerInfo.iconId
    local prevBannerInfo = self.m_bannerInfos[oldIndex]
    if prevBannerInfo ~= nil then
        prevBannerPicId = prevBannerInfo.iconId
    end
    self.view.icon01:LoadSprite(UIConst.UI_SPRITE_BATTLE_PASS_PLAN, prevBannerPicId)
    self.view.icon02:LoadSprite(UIConst.UI_SPRITE_BATTLE_PASS_PLAN, bannerInfo.iconId)
    local isItem = not string.isEmpty(bannerInfo.itemId)
    local isPay = bannerInfo.trackType == GEnums.BPTrackType.PAY
    local isObtained = false
    local isPurchased = bpSystem.trackData.trackRewards:ContainsKey(bannerInfo.trackId)
    if bannerInfo.level ~= nil then
        isObtained = isPurchased and (bannerInfo.isGain == true)
        self.view.lvTxt.text = string.format(Language.LUA_BATTLEPASS_PLAN_BANNER_LEVEL_FORMAT, bannerInfo.level)
    else
        isObtained = isPurchased
        self.view.lvTxt.text = ''
    end
    if isItem then
        local hasItem, itemData = Tables.itemTable:TryGetValue(bannerInfo.itemId)
        if hasItem then
            self.view.itemNameTxt.text = itemData.name
        end
        self.view.exclusiveText.text = bannerInfo.labelTip
    else
        local hasTrack, trackData = Tables.battlePassTrackTable:TryGetValue(bannerInfo.trackId)
        if hasTrack then
            self.view.nameText.text = trackData.name
        end
        self.view.recommendText.text = bannerInfo.labelTip
        self.view.decoTxt.text = bannerInfo.desc
    end
    self.view.bannerNodeStateController:SetState(isPay and "Customize" or "Ration")
    self.view.bannerNodeStateController:SetState(isObtained and "Obtained" or "NotObtained")
    self.view.bannerNodeStateController:SetState(isItem and "StageRewards" or "Style")

    self.view.priceTxt.text = ''
    if isPay then
        self.view.priceTxt.text = CashShopUtils.getGoodsPriceText(Tables.battlePassConst.buyPayTrackCashGoodsId)
    else
        self.view.priceTxt.text = Tables.battlePassConst.buyOriginiumTrackMoneyCnt
    end
end

BattlePassPlanCtrl._OnClickPlanBtn = HL.Method() << function(self)
    if PhaseManager:CheckIsInTransition() then
        return
    end
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        PhaseManager:GoToPhase(PhaseId.BattlePassBuyPlan,{
            type = "Ori"
        })
        return
    end
    self:_OnOpenBuyPlan()
end

BattlePassPlanCtrl._OnClickOrgPlan = HL.Method() << function(self)
    if PhaseManager:CheckIsInTransition() then
        return
    end
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        PhaseManager:GoToPhase(PhaseId.BattlePassBuyPlan,{
            type = "Ori"
        })
        return
    end
    self:_OnOpenBuyPlan()
end

BattlePassPlanCtrl._OnClickPayPlan = HL.Method() << function(self)
    if BattlePassUtils.CheckBattlePassPurchaseBlock() then
        return
    end
    self:_OnOpenBuyPlan()
end

BattlePassPlanCtrl._OnOpenBuyPlan = HL.Method() << function(self)
    if PhaseManager:CheckIsInTransition() then
        return
    end
    PhaseManager:GoToPhase(PhaseId.BattlePassAdvancedPlanBuy)
end

BattlePassPlanCtrl._OnTakeReward = HL.Method(HL.String, HL.Number) << function(self, trackId, level)
    if string.isEmpty(trackId) then
        return
    end
    local bpSystem = GameInstance.player.battlePassSystem
    local hasTrack, trackData = Tables.battlePassTrackTable:TryGetValue(trackId)
    local levelInfo = self.m_levelInfoMap[level]
    if levelInfo == nil then
        return
    end
    if level > bpSystem.levelData.currLevel or not hasTrack or (levelInfo.gainInfo[trackData.trackType] == true)
        or not (levelInfo.canObtainInfo[trackData.trackType] == true) then
        return
    end
    local msgData = {}
    local subMsgData = {}
    msgData[trackId] = subMsgData
    table.insert(subMsgData, level)
    self.m_isGainAll = false
    self.m_isGainMilestone = false
    self.m_buyHintType = GEnums.BPBuyHintType.None
    bpSystem:SendTakeRewards(msgData)
end

BattlePassPlanCtrl._OnTakeAllRewards = HL.Method() << function(self)
    local bpSystem = GameInstance.player.battlePassSystem
    local bpLevel = bpSystem.levelData.currLevel
    local msgData = {}
    local rewardCount = 0
    local hasMilestone = false
    local hintPriority = {
        [GEnums.BPBuyHintType.None] = 0,
        [GEnums.BPBuyHintType.Originium] = 1,
        [GEnums.BPBuyHintType.Pay] = 2,
    }
    local hintTrackMap = {
        [GEnums.BPBuyHintType.Originium] = GEnums.BPTrackType.ORIGINIUM,
        [GEnums.BPBuyHintType.Pay] = GEnums.BPTrackType.PAY,
    }
    local hintType = GEnums.BPBuyHintType.None
    for trackId, trackReward in pairs(bpSystem.trackData.trackRewards) do
        local subMsgData = {}
        msgData[trackId] = subMsgData
        local hasTrack, trackData = Tables.battlePassTrackTable:TryGetValue(trackId)
        if hasTrack then
            for i = bpSystem.firstCanObtainedLevel, bpLevel do
                local levelInfo = self.m_levelInfoMap[i]
                if levelInfo ~= nil and (levelInfo.gainInfo[trackData.trackType] ~= true)
                        and (levelInfo.canObtainInfo[trackData.trackType] == true) then
                    local itemBundle = levelInfo.itemBundles[trackData.trackType]
                    if itemBundle ~= nil then
                        table.insert(subMsgData, i)
                        rewardCount = rewardCount + 1
                    end
                    hasMilestone = hasMilestone or levelInfo.isMilestone
                    if self.m_buyHintInfos[i] ~= nil then
                        local buyHintType = self.m_buyHintInfos[i]
                        local hintTrack = hintTrackMap[buyHintType]
                        local isActive = self.m_validTrack[hintTrack] ~= nil
                        if not isActive and hintPriority[buyHintType] > hintPriority[hintType] then
                            hintType = buyHintType
                        end
                    end
                end
            end
        end
    end
    if rewardCount <= 0 then
        return
    end
    self.m_isGainAll = true
    self.m_isGainMilestone = hasMilestone
    self.m_buyHintType = hintType
    bpSystem:SendTakeRewards(msgData)
end

BattlePassPlanCtrl._OnLevelUpdate = HL.Method() << function(self)
    self:_RefreshLevelGain()
    self:_RefreshBannerGain()
    self:_RenderViews(true)
end

BattlePassPlanCtrl._OnTrackUpdate = HL.Method() << function(self)
    if UIManager:IsOpen(PanelId.BattlePassAdvancedPlanBuy) then
        return
    end
    self:_TriggerBuyTrack()
end

BattlePassPlanCtrl._TriggerBuyTrack = HL.Method() << function(self)
    self:_RefreshLevelGain()
    self:_RefreshBannerGain()
    self:_RenderViews(true)
    local isOrgBuy = self.m_trackBuyFlags[GEnums.BPTrackType.ORIGINIUM] == true
    if isOrgBuy then
        self.m_trackBuyFlags[GEnums.BPTrackType.ORIGINIUM] = nil
    end
    local isPayBuy = self.m_trackBuyFlags[GEnums.BPTrackType.PAY] == true
    if isPayBuy then
        self.m_trackBuyFlags[GEnums.BPTrackType.PAY] = nil
    end
    self:_PlayBuyTrackAnim(isOrgBuy, isPayBuy)
end

BattlePassPlanCtrl._PlayBuyTrackAnim = HL.Method(HL.Boolean, HL.Boolean) << function(self, isOrgBuy, isPayBuy)
    if not isOrgBuy and not isPayBuy then
        return
    end
    self.view.leftNodeAnimationWrapper:ClearTween()
    if isOrgBuy and isPayBuy then
        self.view.leftNodeAnimationWrapper:PlayWithTween(self.view.config.ANI_BOTH_UNLOCK_NAME)
    elseif isOrgBuy then
        self.view.leftNodeAnimationWrapper:PlayWithTween(self.view.config.ANI_ORG_UNLOCK_NAME)
    elseif isPayBuy then
        self.view.leftNodeAnimationWrapper:PlayWithTween(self.view.config.ANI_PAY_UNLOCK_NAME)
    end
    AudioAdapter.PostEvent("Au_UI_Event_BPUnlockMotion")
end

BattlePassPlanCtrl._OnRewardShow = HL.Method(HL.Any) << function(self, args)
    local bundles = unpack(args)
    self:_ProcessRewardGain(bundles)
end

BattlePassPlanCtrl._ProcessRewardGain = HL.Method(HL.Any) << function(self, itemBundles)
    local weaponBoxId = self.m_seasonData.weaponBoxId
    local hasWeaponBox = false
    local merged = {}
    local mergedItems = {}
    for _, itemBundle in pairs(itemBundles) do
        if itemBundle ~= nil and not string.isEmpty(itemBundle.id) then
            if itemBundle.id == weaponBoxId then
                hasWeaponBox = true
            end
            local existing = merged[itemBundle.id]
            if existing then
                existing.count = existing.count + (itemBundle.count or 0)
            else
                local newItem = { id = itemBundle.id, count = itemBundle.count or 0 }
                merged[itemBundle.id] = newItem
                table.insert(mergedItems, newItem)
            end
        end
    end
    if #mergedItems <= 0 then
        return
    end
    local rewardPanelArg = {
        items = mergedItems,
    }
    if self.m_isGainAll and self.m_isGainMilestone then
        local buyHintType = self.m_buyHintType
        rewardPanelArg.onComplete = function()
            if hasWeaponBox then

                self:_OpenWeaponBox(weaponBoxId, false)
            else

                self:_OpenRecommend(buyHintType)
            end
        end
    end
    self.m_isGainAll = false
    self.m_isGainMilestone = false
    self.m_buyHintType = GEnums.BPBuyHintType.None
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArg)
end

BattlePassPlanCtrl._OpenRecommend = HL.Method(HL.Any) << function(self, buyHintType)
    local rewardId = ''
    local recommendPlanName = ''
    if buyHintType == GEnums.BPBuyHintType.Originium then
        rewardId = self.m_seasonData.originiumHintRewardId
        local trackData = self:_FindTrackDataByType(GEnums.BPTrackType.ORIGINIUM)
        if trackData ~= nil then
            recommendPlanName = trackData.name
        end
    elseif buyHintType == GEnums.BPBuyHintType.Pay then
        rewardId = self.m_seasonData.payHintRewardId
        local trackData = self:_FindTrackDataByType(GEnums.BPTrackType.PAY)
        if trackData ~= nil then
            recommendPlanName = trackData.name
        end
    end
    if not string.isEmpty(rewardId) then
        local bpSystem = GameInstance.player.battlePassSystem
        local bpLevel = bpSystem.levelData.currLevel
        UIManager:Open(PanelId.BattlePassRecommend, {
            rewardId = rewardId,
            desc = string.format(Language.LUA_BATTLEPASS_PLAN_RECOMMEND_HINT_TIP_FORMAT, bpLevel, recommendPlanName)
        })
    end
end

BattlePassPlanCtrl._OpenWeaponBox = HL.Method(HL.String, HL.Boolean) << function(self, itemId, isPreview)
    UIManager:Open(PanelId.BattlePassWeaponCase, {
        isAutoOpenWhenGet = true,
        isPreview = isPreview,
        itemId = itemId,
    })
end

BattlePassPlanCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.BattlePass then
        return
    end
    local isOpen, ctrl = UIManager:IsOpen(PanelId.BattlePassWeaponCase)
    if not isOpen or ctrl == nil then
        return
    end
    local subPanelArg = ctrl:GetCurPhaseStateArg()
    if not subPanelArg then
        return
    end
    subPanelArg.isBattlePassWeaponCase = true
    return {
        subPanelArg = subPanelArg,
    }
end

BattlePassPlanCtrl._TryRecoverSubPanel = HL.Method(HL.Opt(HL.Any)) << function(self, panelArgs)
    local subPanelArg = panelArgs and panelArgs.subPanelArg
    if not subPanelArg or subPanelArg.isBattlePassWeaponCase ~= true then
        return
    end
    UIManager:Open(PanelId.BattlePassWeaponCase, subPanelArg)
end

HL.Commit(BattlePassPlanCtrl)
