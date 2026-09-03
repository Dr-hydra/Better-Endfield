
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityBenefits

ActivityBenefitsCtrl = HL.Class('ActivityBenefitsCtrl', uiCtrl.UICtrl)

ActivityBenefitsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET] = '_OnRefresh',
    [MessageConst.ON_BUY_ITEM_SUCC] = '_OnRefresh',
    [MessageConst.ON_ADVENTURE_BOOK_STAGE_MODIFY] = '_OnRefresh',
}

ActivityBenefitsCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityBenefitsCtrl.m_getCell = HL.Field(HL.Function)

ActivityBenefitsCtrl.m_benefits = HL.Field(HL.Table)

local GACHA_BIG_REWARD = "gachaBigReward"
local PREPARE_ACTIVITY_ID = "activity_newplayer_benefits_display"








local benefitConfigTable = {

    gachaPool = {
        activityId = Tables.activityConst.ActivityBenefitsGachaActivityId,
        getStageIdsFunc = function()
            local ids = {}
            for i = 1, Tables.activityLevelRewardsTable[Tables.activityConst.ActivityBenefitsGachaActivityId].stageList.Count do
                table.insert(ids, i)
            end

            table.insert(ids, GACHA_BIG_REWARD)
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(Tables.activityConst.ActivityBenefitsGachaActivityId)
            if not activity then
                return false
            end
            if id == GACHA_BIG_REWARD then

                local poolId = Tables.activityConst.ActivityBenefitsGachaId
                local hasInfo, poolInfo = GameInstance.player.gacha.poolInfos:TryGetValue(poolId)
                if not hasInfo then
                    return false
                end
                local poolRoleData = poolInfo.roleDataMsg
                if not poolRoleData then
                    return false
                end
                return poolRoleData.CumulativeRewardList.Count > 0
            end
            return activity.receiveStageList:Contains(id)
        end,
        getRewardBundleByStageIdFunc = function(id)
            if id == GACHA_BIG_REWARD then

                local poolId = Tables.charGachaConst.beginnerGachaActivityPoolId
                local poolCfg = Tables.gachaCharPoolTable[poolId]
                local cumulateRewardIds = poolCfg.cumulativeRewardIds
                local rewardItems = UIUtils.getRewardItems(cumulateRewardIds[0])
                return {{
                    id = rewardItems[1].id,
                    count = rewardItems[1].count,
                }}
            end
        end,
        getRewardIdByStageIdFunc = function(id)
            return Tables.activityLevelRewardsTable[Tables.activityConst.ActivityBenefitsGachaActivityId].stageList[CSIndex(id)].rewardId
        end,
    },

    checkIn = {
        activityId = Tables.activityConst.ActivityBenefitsCheckinActivityId,
        getStageIdsFunc = function()
            local ids = {}
            for i = 1, Tables.CheckInRewardTable[Tables.activityConst.ActivityBenefitsCheckinActivityId].stageList.Count do
                table.insert(ids, i)
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(Tables.activityConst.ActivityBenefitsCheckinActivityId)
            if not activity then
                return false
            end
            return activity.rewardDays:Contains(id)
        end,
        getRewardIdByStageIdFunc = function(id)
            return Tables.CheckInRewardTable[Tables.activityConst.ActivityBenefitsCheckinActivityId].stageList[CSIndex(id)].rewardId
        end,
    },

    levelRewards = {

        activityId = Tables.activityConst.ActivityBenefitsLevelRewardActivityId,
        getStageIdsFunc = function()
            local ids = {}
            for i = 1, Tables.activityLevelRewardsTable[Tables.activityConst.ActivityBenefitsLevelRewardActivityId].stageList.Count do
                table.insert(ids, i)
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(Tables.activityConst.ActivityBenefitsLevelRewardActivityId)
            if not activity then
                return false
            end
            return activity.receiveStageList:Contains(id)
        end,
        getRewardIdByStageIdFunc = function(id)
            return Tables.activityLevelRewardsTable[Tables.activityConst.ActivityBenefitsLevelRewardActivityId].stageList[CSIndex(id)].rewardId
        end,
    },

    mainline = {
        getStageIdsFunc = function()

            local ids = {}
            for i = 1, Tables.activityConst.ActivityBenefitsMainlineNormalMissionIds.Count do
                table.insert(ids, Tables.activityConst.ActivityBenefitsMainlineNormalMissionIds[CSIndex(i)])
            end

            for i = 1,#Tables.activityConst.ActivityBenefitsMainlineSpecialMissionIds do
                table.insert(ids, i)
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            if type(id) == "string" then

                return GameInstance.player.mission:IsMissionCompleted(id)
            else

                local missionId = Tables.activityConst.ActivityBenefitsMainlineSpecialMissionIds[CSIndex(id)]
                return GameInstance.player.mission:IsMissionCompleted(missionId)
            end
        end,
        getRewardBundleByStageIdFunc = function(id)
            if type(id) == "number" then

                return {{
                    id = Tables.activityConst.ActivityBenefitsMainlineSpecialMissionRewardIds[CSIndex(id)],
                    count = 1
                }}
            end
        end,
        getRewardIdByStageIdFunc = function(id)

            return GameInstance.player.mission:GetMissionMetaAsset(id).rewardId
        end,
    },

    adventureBook = {

        getStageIdsFunc = function()
            local ids = {}
            local i = 0
            for _ in pairs(Tables.adventureBookStageRewardTable) do
                i = i + 1
                table.insert(ids, i)
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)

            return GameInstance.player.adventure.adventureBookData.actualBookStage > id
        end,
        getRewardIdByStageIdFunc = function(id)
            return Tables.adventureBookStageRewardTable[id].rewardId
        end,
        unlockFunc = function()
            return Utils.isSystemUnlocked(GEnums.UnlockSystemType.AdventureBook)
        end,
        unlockHint = Language.LUA_ACTIVITY_BENEFITS_ADVENTURE_BOOK_LOCKED,
    },

    domainDevelopment = {
        getStageIdsFunc = function()
            local ids = {}

            local goodsIds = Tables.activityConst.ActivityBenefitsDomainShopGoodsIds
            for i = 1, goodsIds.Count do
                table.insert(ids, {
                    isShop = true,
                    goodsId = goodsIds[CSIndex(i)],
                })
            end

            local domainIds = {}
            for i = 1, Tables.activityConst.ActivityBenefitsDomainIds.Count do
                domainIds[Tables.activityConst.ActivityBenefitsDomainIds[CSIndex(i)]] = true
            end
            for domainId, domainInfo in pairs(Tables.domainDataTable) do
                if domainIds[domainId] and GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId) then
                    for _, levelInfo in pairs(domainInfo.domainDevelopmentLevel) do
                        if not string.isEmpty(levelInfo.rewardId) then
                            table.insert(ids, {
                                isShop = false,
                                domainId = domainId,
                                level = levelInfo.domainDevelopmentLevel,
                                rewardId = levelInfo.rewardId,
                            })
                        end
                    end
                end
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            if id.isShop then
                return GameInstance.player.shopSystem:GetBuyCountByGoodsId(id.goodsId) > 0
            else
                return GameInstance.player.domainDevelopmentSystem:IsLevelRewarded(id.domainId, id.level)
            end
        end,
        getRewardIdByStageIdFunc = function(id)
            if id.isShop then
                return Tables.shopGoodsTable[id.goodsId].rewardId
            else
                return id.rewardId
            end
        end,
        unlockFunc = function()
            return Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainDevelopment)
        end,
        unlockHint = Language.LUA_ACTIVITY_BENEFITS_DOMAIN_DEVELOPMENT_LOCKED,
    },

    levelRewardSystem = {
        getStageIdsFunc = function()
            local ids = {}

            local i = 0
            for id, info in pairs(Tables.adventureLevelTable) do
                if not string.isEmpty(info.rewardId) then
                    table.insert(ids, id)
                end
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)

            return GameInstance.player.adventure.adventureLevelData.lv >= id
        end,
        getRewardIdByStageIdFunc = function(id)
            local success, info = Tables.adventureLevelTable:TryGetValue(id)
            return success and info.rewardId or ""
        end,
    },

    newplayer_mission = {
        activityId = Tables.activityConst.ActivityNewplayerMissionRewardActivityId,
        getStageIdsFunc = function()
            local ids = {}
            for i = 1, Tables.activityLevelRewardsTable[Tables.activityConst.ActivityNewplayerMissionRewardActivityId].stageList.Count do
                table.insert(ids, i)
            end
            return ids
        end,
        checkRewardReceivedByStageIdFunc = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(Tables.activityConst.ActivityNewplayerMissionRewardActivityId)
            if not activity then
                return false
            end
            return activity.receiveStageList:Contains(id)
        end,
        getRewardIdByStageIdFunc = function(id)
            return Tables.ActivityLevelRewardsTable[Tables.activityConst.ActivityNewplayerMissionRewardActivityId].stageList[CSIndex(id)].rewardId
        end,
    },
}

ActivityBenefitsCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    ActivityBenefitsCtrl.PrepareBenefits()
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self:_RefreshInfo()
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(self.m_getCell(object), LuaIndex(csIndex))
    end)
    self.view.scrollList:UpdateCount(#self.m_benefits)

    local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
        self:_SetNaviTarget(1)
    end)

    self.view.scrollListSelectableNaviGroup.onIsTopLayerChanged:AddListener(function(active)
        InputManagerInst:ToggleBinding(viewBindingId, not active)
    end)
end

ActivityBenefitsCtrl.m_naviTarget = HL.Field(HL.Number) << -1

ActivityBenefitsCtrl._OnRefresh = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshInfo()
    if self.m_naviTarget > 0 then
        self.view.scrollList:UpdateCount(#self.m_benefits, self.m_naviTarget, false, false, true)
    else
        self.view.scrollList:UpdateCount(#self.m_benefits)
    end
    if UIManager:IsOpen(PanelId.ActivityBenefitsRegionPopup) then
        for _, info in ipairs(self.m_benefits) do
            if info.benefitId == "domainDevelopment" then
                Notify(MessageConst.ON_ACTIVITY_BENEFIT_REFRESH, info.rewardList)
                break
            end
        end
    end
end

ActivityBenefitsCtrl._SetNaviTarget = HL.Method(HL.Number) << function(self, index)
    if index == 0 or not DeviceInfo.usingController  then
        return
    end
    local cell = self:_GetCell(index)
    if cell then
        self:SetNaviTarget(cell.naviDecorator)
    end
end

ActivityBenefitsCtrl._GetCell = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    local oriCell = self.view.scrollList:Get(CSIndex(index))
    return oriCell and self.m_getCell(oriCell)
end

ActivityBenefitsCtrl._RefreshInfo = HL.Method() << function(self)
    local isPreparedActivity = self.m_activityId == PREPARE_ACTIVITY_ID
    self.m_benefits = {}
    local data = Tables.activityBenefitsTable[self.m_activityId].stageList
    for dataIndex = 1, data.Count do
        local info = data[CSIndex(dataIndex)]


        local config = benefitConfigTable[info.benefitId]


        local rewardDict = {}
        for rewardIndex = 1, info.rewardIdList.Count do
            local itemId = info.rewardIdList[CSIndex(rewardIndex)]
            rewardDict[itemId] = {
                sortId = rewardIndex,
                obtain = 0,
                total = 0,
            }
        end


        local isUnlocked = self:_IsBenefitUnlocked(info.benefitId)
        local isComplete = true
        if config then
            local ids
            if isPreparedActivity and config.preparedStageIds then

                ids = config.preparedStageIds
            else

                ids = config.getStageIdsFunc()
            end
            for _, id in ipairs(ids) do

                local rewardBundles
                if isPreparedActivity and config.rewardBundleDic and config.rewardBundleDic[id] then

                    rewardBundles = config.rewardBundleDic[id]
                else
                    if config.getRewardBundleByStageIdFunc then
                        rewardBundles = config.getRewardBundleByStageIdFunc(id)
                    end
                    if not rewardBundles then
                        local rewardId = config.getRewardIdByStageIdFunc(id)
                        if not string.isEmpty(rewardId) then
                            rewardBundles = UIUtils.getRewardItems(rewardId)
                        else
                            logger.error(info.benefitId .. "无奖励信息:" .. id)
                        end
                    end
                end


                if rewardBundles then
                    for _,reward in ipairs(rewardBundles) do
                        if rewardDict[reward.id] then
                            rewardDict[reward.id].total = rewardDict[reward.id].total + reward.count

                            local complete
                            if config.activityId and not GameInstance.player.activitySystem:GetActivity(config.activityId) then


                                complete = true
                            else
                                complete = config.checkRewardReceivedByStageIdFunc(id) and isUnlocked
                            end
                            if complete then
                                rewardDict[reward.id].obtain = rewardDict[reward.id].obtain + reward.count
                            end
                        end
                    end
                end
            end
        end


        local rewardList = {}
        for rewardId, dictItem in pairs(rewardDict) do
            dictItem.obtain = math.min(dictItem.obtain, dictItem.total)
            if dictItem.obtain < dictItem.total and rewardId ~= "item_adventureexp" then
                isComplete = false
            end
            table.insert(rewardList,{
                itemId = rewardId,
                obtain = dictItem.obtain,
                total = dictItem.total,
                sortId = dictItem.sortId,
            })
        end
        table.sort(rewardList, Utils.genSortFunction({"sortId"}, true))


        table.insert(self.m_benefits,{
            benefitId = info.benefitId,
            sortId = info.sortId,
            title = info.title,
            jumpId = info.jumpId,
            desc = info.desc,
            iconId = info.iconId,
            rewardList = rewardList,
            isComplete = isComplete,
            stateSortId = isComplete and 3 or (isUnlocked and 1 or 2),
            unlockHint = config.unlockHint,
            bigRewardId = info.bigRewardId,
            bigRewardStatement = info.bigRewardStatement,
        })
    end
    table.sort(self.m_benefits, Utils.genSortFunction({"stateSortId", "sortId"}, true))
end

ActivityBenefitsCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_benefits[index]
    cell.gameObject.name = "Cell" .. index


    cell.nameTxt.text = info.title
    cell.descTxt:SetAndResolveTextStyle(info.desc)
    cell.iconBig:LoadSprite(UIConst.UI_SPRITE_ACTIVITY_BENEFITS, info.iconId)
    cell.iconSmall:LoadSprite(UIConst.UI_SPRITE_ACTIVITY, info.iconId)


    cell.isFinished = info.isComplete
    cell.cache = cell.cache or UIUtils.genCellCache(cell.activityBenefitsRewardCell)
    cell.cache:Refresh(#info.rewardList, function(innerCell, innerIndex)
        innerCell.gameObject.name = "Reward-" .. tostring(index) .. "-" .. tostring(innerIndex)
        local rewardInfo = info.rewardList[innerIndex]
        rewardInfo.fromMain = true
        rewardInfo.itemExtraInfo = {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightTopOrLeftTop,
            tipsPosTransform = cell.rectTransform,
            isSideTips = true,
        }
        rewardInfo.CompleteAll = info.isComplete
        innerCell:InitActivityBenefitsRewardCell(rewardInfo)
    end)
    cell.keyHint:SetAsLastSibling()


    local jumpId = info.jumpId or ""
    local isDomain = info.benefitId == Tables.activityConst.ActivityBenefitsSpecialJumpBenefitId
    local canJump = isDomain or Tables.systemJumpTable:TryGetValue(jumpId)
    cell.jumpBtn.enabled = not cell.isFinished
    cell.jumpBtn.onClick:RemoveAllListeners()


    local isUnlocked = self:_IsBenefitUnlocked(info.benefitId)
    if not isUnlocked then

        cell.stateController:SetState("Lock")
        cell.stateController:SetState("NormalCell")
        if canJump then
            cell.jumpBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_TOAST, info.unlockHint)
            end)
        end
    elseif not cell.isFinished then

        cell.stateController:SetState("Goto")
        cell.stateController:SetState("NormalCell")
        if canJump then
            cell.jumpBtn.onClick:AddListener(function()
                if isDomain then

                    UIManager:Open(PanelId.ActivityBenefitsRegionPopup, info.rewardList)
                else
                    Utils.jumpToSystem(jumpId)
                end
            end)
        end
    else

        cell.stateController:SetState("Done")
        cell.stateController:SetState("GrayCell")
    end

    cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_naviTarget = index
        end
    end

    cell.itemIcon:InitItemIcon(info.bigRewardId, true)
    cell.bigRewardTxt:SetAndResolveTextStyle(info.bigRewardStatement)
end

ActivityBenefitsCtrl._IsBenefitUnlocked = HL.Method(HL.String).Return(HL.Boolean) << function(self, benefitId)
    local config = benefitConfigTable[benefitId]
    if not config then
        return false
    end
    local unlockFunc = config.unlockFunc

    if not unlockFunc then
        return true
    end
    return unlockFunc()
end

ActivityBenefitsCtrl.PrepareBenefits = HL.StaticMethod() << function()

    if not Tables.activityBenefitsTable:ContainsKey(PREPARE_ACTIVITY_ID) or benefitConfigTable.isPrepared then
        return
    end
    local data = Tables.activityBenefitsTable[PREPARE_ACTIVITY_ID].stageList
    for dataIndex = 1, data.Count do
        local info = data[CSIndex(dataIndex)]
        local config = benefitConfigTable[info.benefitId]
        if config then
            config.rewardBundleDic = {}
            local ids = config.getStageIdsFunc()
            config.preparedStageIds = ids
            for _, id in ipairs(ids) do

                local rewardBundles
                if config.getRewardBundleByStageIdFunc then
                    rewardBundles = config.getRewardBundleByStageIdFunc(id)
                end
                if not rewardBundles then
                    local rewardId = config.getRewardIdByStageIdFunc(id)
                    if not string.isEmpty(rewardId) then
                        rewardBundles = UIUtils.getRewardItems(rewardId)
                    else
                        logger.error(info.benefitId .. "无奖励信息:" .. id)
                    end
                end
                if rewardBundles then
                    config.rewardBundleDic[id] = rewardBundles
                end
            end
        end
    end
    benefitConfigTable.isPrepared = true
end

HL.Commit(ActivityBenefitsCtrl)
