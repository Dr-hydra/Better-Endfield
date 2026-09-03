
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaPool
local PHASE_ID = PhaseId.GachaPool

GachaPoolCtrl = HL.Class('GachaPoolCtrl', uiCtrl.UICtrl)






GachaPoolCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_GACHA_SUCC] = 'OnGachaSucc',
    [MessageConst.ON_GACHA_POOL_INFO_CHANGED] = 'OnGachaPoolInfoChanged',
    [MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED] = 'OnGachaPoolRoleDataChanged',
    [MessageConst.ON_WALLET_CHANGED] = 'OnWalletChanged',
    [MessageConst.ON_VALUABLE_DEPOT_CHANGED] = 'OnItemChanged',
    [MessageConst.GACHA_POOL_ADD_SHOW_REWARD] = 'AddQueueReward',
    [MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED] = 'OnOneQueueRewardFinished',
    [MessageConst.ON_SYSTEM_DISPLAY_SIZE_CHANGED] = '_OnDisplaySizeChanged',
    [MessageConst.ON_SYSTEM_SCREEN_SIZE_CHANGED] = '_OnDisplaySizeChanged',

    [MessageConst.ON_UI_PHASE_OPENED] = '_OnPhaseChange',
    [MessageConst.ON_UI_PHASE_EXITED] = '_OnPhaseChange',
}



GachaPoolCtrl.m_arg = HL.Field(HL.Table)

GachaPoolCtrl.m_getCell = HL.Field(HL.Function)

GachaPoolCtrl.m_curPoolId = HL.Field(HL.String) << ''

GachaPoolCtrl.m_pools = HL.Field(HL.Table)

GachaPoolCtrl.m_curIndex = HL.Field(HL.Number) << 1

GachaPoolCtrl.m_poolTabCache = HL.Field(HL.Forward('UIListCache'))

GachaPoolCtrl.m_showRewardFuncQueue = HL.Field(HL.Forward("Queue"))

GachaPoolCtrl.m_queueRewardConfigs = HL.Field(HL.Table)

GachaPoolCtrl.m_curIsShowReward = HL.Field(HL.Boolean) << false

GachaPoolCtrl.m_needTryPlayChangeTabInAni = HL.Field(HL.Boolean) << true

GachaPoolCtrl.m_isPlayingChangeTabInAni = HL.Field(HL.Boolean) << false

GachaPoolCtrl.m_onPlayChangeTabInAniComplete = HL.Field(HL.Function)

GachaPoolCtrl.m_jumpToPoolLuaIndex = HL.Field(HL.Number) << 0





GachaPoolCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_arg = arg
    self:_InitData(arg.poolId)

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            local cell = self.m_getCell(self.m_curIndex)
            if cell then
                self:_UpdateRemainingTime(cell.node, self.m_curIndex)
            end
        end
    end)
end

GachaPoolCtrl.OnShow = HL.Override() << function(self)

    local time = Time.unscaledTime
    self.loader:LoadGameObjectAsync("Assets/Beyond/DynamicAssets/Gameplay/Prefabs/Gacha/GachaOutside.prefab", function()
        logger.info("GachaOutside 预载完成", Time.unscaledTime - time)
    end)
    self.loader:LoadGameObjectAsync("Assets/Beyond/DynamicAssets/Gameplay/Prefabs/Gacha/GachaRoom.prefab", function()
        logger.info("GachaRoom 预载完成", Time.unscaledTime - time)
    end)
    logger.info("GachaPoolCtrl.OnShow")
    self:_OnTestimonialConvert()


    local count = self.view.poolList.count
    for i = 1, count do
        local cell = self.m_getCell(i)
        if cell then
            InputManagerInst:ToggleGroup(cell.node.inputGroup.groupId, self.m_curIndex == i)
        end
    end

    if self.m_arg.rewardQueueRestoreInfo then
        self:_RecoverRewardQueue(self.m_arg.rewardQueueRestoreInfo)
        self.m_arg.rewardQueueRestoreInfo = nil
    end

    if not self.m_isPlayingChangeTabInAni then
        self:_TryShowQueueReward()
    end

    local cell = self.m_poolTabCache:Get(self.m_curIndex)
    if cell then
        self:SetNaviTarget(cell.toggle)
    end


    self:HandleSubPanelArg()
end

GachaPoolCtrl.OnHide = HL.Override() << function(self)
    self.view.moneyNode.naviGroup:ManuallyStopFocus()
end

GachaPoolCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    GachaPoolCtrl.Super._OnPlayAnimationOut(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()

    local cell = self.m_getCell(self.m_curIndex)
    if cell then
        cell.cellWidget:PlayGachaOutAni()
    end
end




GachaPoolCtrl._InitData = HL.Method(HL.Opt(HL.String)) << function(self, poolId)
    self:_InitRewardQueueConfigs()
    self.m_curIndex = 1
    self.m_jumpToPoolLuaIndex = 1
    self.m_pools = {}

    local csGacha = GameInstance.player.gacha
    for id, csInfo in pairs(csGacha.poolInfos) do
        if csInfo.isChar and csInfo.isOpenValid then
            local info = {
                id = id,
                csInfo = csInfo,
                data = csInfo.data,
                sortId = csInfo.data.sortId,
            }
            table.insert(self.m_pools, info)
        end
    end
    table.sort(self.m_pools, Utils.genSortFunction({ "sortId" }, true))
    local count = #self.m_pools
    self.m_poolTabCache:Refresh(count, function(cell, index)
        if poolId and self.m_pools[index].id == poolId then
            self.m_jumpToPoolLuaIndex = index
        end
        self:_UpdateTabCell(cell, index)
    end)
    self.view.poolList:UpdateCount(count)
    if self.m_jumpToPoolLuaIndex ~= 1 then
        self.m_needTryPlayChangeTabInAni = true
        self.view.poolList:ScrollToIndex(CSIndex(self.m_jumpToPoolLuaIndex), true)
    end
    self.m_jumpToPoolLuaIndex = -1
end





GachaPoolCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.GachaPool)
    end)
    self:BindInputPlayerAction("common_open_gacha", function()
        PhaseManager:PopPhase(PhaseId.GachaPool)
    end, self.view.closeBtn.groupId)

    self.view.weaponShopBtn.onClick:AddListener(function()
        Utils.jumpToSystem("jump_payshop_weapon")
    end)

    self.m_onPlayChangeTabInAniComplete = function(sucPlayAni)
        InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, true)

        local aniWrapper = self.animationWrapper
        if sucPlayAni then
            aniWrapper:PlayInAnimation()
        else
            aniWrapper:SampleClipAtPercent(aniWrapper.animationIn.name, 1)
        end
        self.m_isPlayingChangeTabInAni = false
        self:_TryShowQueueReward()
    end

    self.m_poolTabCache = UIUtils.genCellCache(self.view.poolTabCell)
    self.m_getCell = UIUtils.genCachedCellFunction(self.view.poolList)
    self.view.poolList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.view.poolList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnCenterIndexChanged(LuaIndex(newIndex))
    end)

    self:_InitMoneyNode()
    self.m_showRewardFuncQueue = require_ex("Common/Utils/DataStructure/Queue")()

    self.view.moneyNode.naviGroup.getDefaultSelectableFunc = function()
        local cell = self.view.moneyNode.diamond
        return cell.view.button
    end
end



GachaPoolCtrl._UpdateTabCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_pools[index]
    local poolTypeData = Tables.gachaCharPoolTypeTable[info.data.type]
    cell.nameTxt.text = poolTypeData.tagName
    cell.nameTxt.color = UIUtils.getColorByString(info.data.textColor)
    cell.nameBG.color = UIUtils.getColorByString(info.data.color)
    cell.shadowImg.color = UIUtils.getColorByString(info.data.tabGradientColor, cell.shadowImg.color.a * 255)
    cell.selectDeco.color = UIUtils.getColorByString(info.data.tabGradientColor)
    cell.bannerImg:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, info.data.tabImage)
    cell.selectStateCtrl:SetState("Unselect")
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if self.m_curIndex == index then
            return
        end
        if isOn then
            self.m_needTryPlayChangeTabInAni = true
            self.view.poolList:ScrollToIndex(CSIndex(index), true)
        end
    end)

    cell.gameObject.name = info.data.type:ToString()

    cell.redDot:InitRedDot("GachaSinglePool", info.id)
end

GachaPoolCtrl._OnCenterIndexChanged = HL.Method(HL.Number) << function(self, index)
    logger.info("GachaPoolCtrl._OnCenterIndexChanged", index)

    local info = self.m_pools[index]
    local preIndex = self.m_curIndex
    self.m_curIndex = index
    self.m_curPoolId = info.id




    local preTabCell = self.m_poolTabCache:Get(preIndex)
    local nowTabCell = self.m_poolTabCache:Get(index)
    if preTabCell then
        preTabCell.selectStateCtrl:SetState("Unselect")
    end
    if nowTabCell then
        nowTabCell.selectStateCtrl:SetState("Select")
        nowTabCell.toggle:SetIsOnWithoutNotify(true)
    end

    local prePoolCell = self.m_getCell(preIndex)
    if prePoolCell then
        InputManagerInst:ToggleGroup(prePoolCell.node.inputGroup.groupId, false)
        prePoolCell.cellWidget:PlayGachaScrollOutAni()
    end
    local cell = self.m_getCell(self.m_curIndex)
    if cell then
        self:_OnUpdateCell(cell, self.m_curIndex)
        cell.cellWidget:UpdateMoneyNode(self.view.moneyNode)
        RedDotUtils.setGachaSinglePoolRead(self.m_curPoolId)
        if self.m_needTryPlayChangeTabInAni then
            if self.m_jumpToPoolLuaIndex <= 0 or self.m_jumpToPoolLuaIndex == index then
                self:_PlayChangeTabInAni(cell.cellWidget)
            end
            self.m_needTryPlayChangeTabInAni = false
        else
            cell.cellWidget:PlayGachaScrollInAni()
            if PhaseManager:GetTopPhaseId() == PHASE_ID and (self.m_jumpToPoolLuaIndex <= 0 or self.m_jumpToPoolLuaIndex == index) then
                self:_TryShowQueueReward()
            end
        end
        InputManagerInst:ToggleGroup(cell.node.inputGroup.groupId, true)

        local naviGroup = cell.node.jumpBtnNaviGroup
        if naviGroup and naviGroup.relatedInputBindingGroups.Count > 0 then
            local cellInputGroup = naviGroup.relatedInputBindingGroups[0]
            self.view.controllerHintBar:InitControllerHintPlaceholder({ self.view.inputGroup.groupId, cellInputGroup.groupId })
        else
            self.view.controllerHintBar:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
        end
    end
    if self.view.poolList.centerIndex ~= CSIndex(index) then
        self.view.poolList:ScrollToIndex(CSIndex(index))
    end
end

GachaPoolCtrl._PlayChangeTabInAni = HL.Method(HL.Any) << function(self, poolCellWidget)
    if poolCellWidget:CheckCanPlayChangeTabInAni() then

        local aniWrapper = self.animationWrapper
        aniWrapper:SampleClipAtPercent(aniWrapper.animationIn.name, 0)
        InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, false)
        self.m_isPlayingChangeTabInAni = true
        poolCellWidget:PlayGachaChangeTabInAni(self.m_onPlayChangeTabInAniComplete)
    else
        poolCellWidget:PlayGachaScrollInAni()
        if PhaseManager:GetTopPhaseId() == PHASE_ID then
            self:_TryShowQueueReward()
        end
    end
end



GachaPoolCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)


    if cell.m_lastUpdateFrame and cell.m_lastUpdateFrame == Time.frameCount then
        return
    end
    cell.m_lastUpdateFrame = Time.frameCount
    logger.info("GachaPoolCtrl._OnUpdateCell", index)

    local poolInfo = self.m_pools[index]
    local poolData = Tables.gachaCharPoolTable[poolInfo.id]

    local uiPrefabName = poolData.uiPrefab
    if cell.uiPrefabName ~= uiPrefabName then
        if cell.node then
            GameObject.Destroy(cell.node.gameObject)
        end
        local path = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Gacha/Widgets/%s.prefab", uiPrefabName)
        local prefab = self.m_phase.m_resourceLoader:LoadGameObject(path)
        local obj = CSUtils.CreateObject(prefab, cell.transform)


        obj.name = poolData.type:ToString()

        cell.uiPrefabName = uiPrefabName

        local poolCellWidget = Utils.wrapLuaNode(obj)
        poolCellWidget:InitGachaPoolCell(poolInfo.id)
        cell.cellWidget = poolCellWidget
        cell.node = poolCellWidget.view
        InputManagerInst:ToggleGroup(cell.node.inputGroup.groupId, false)
    end
    local node = cell.node
    cell.cellWidget:UpdateGachaPoolCell()
    if self.m_curIndex == index then
        cell.cellWidget:CheckAndShowSpecialRewardPopup()
    end

    if node.nameMainImg then
        node.nameMainImg:LoadSprite(UIConst.UI_SPRITE_GACHA_POOL, poolData.nameImage)
    end


    node.detailNode.gameObject:SetActive(not GameInstance.player.gameSettingSystem.forbiddenWebView)
    node.detailBtn.onClick:RemoveAllListeners()
    node.detailBtn.onClick:AddListener(function()
        self:_ShowDetailPanel()
    end)


    local endTime = poolInfo.csInfo.closeTime
    if node.endTimeTxt then
        node.endTimeTxt.text = Utils.appendUTC(Utils.timestampToDateMDHM(endTime))
    end
    self:_UpdateRemainingTime(node, index)
end

GachaPoolCtrl._UpdateRemainingTime = HL.Method(HL.Table, HL.Number) << function(self, node, index)
    if node.remainingTimeTxt then
        local poolInfo = self.m_pools[index]
        local endTime = poolInfo.csInfo.closeTime
        local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        local diffTime = math.max(0, endTime - curServerTime)
        node.remainingTimeTxt.text = string.format(Language.LUA_GACHA_REMAINING_TIME, UIUtils.getShortLeftTime(diffTime))
    end
end

GachaPoolCtrl._InitMoneyNode = HL.Method() << function(self)
    local moneyNode = self.view.moneyNode
    local originiumItemCfg = Tables.itemTable:GetValue(Tables.globalConst.originiumItemId)
    local diamondItemCfg = Tables.itemTable:GetValue(Tables.globalConst.diamondItemId)

    moneyNode.diamond:InitMoneyCell(Tables.globalConst.diamondItemId)
    moneyNode.diamond.view.addBtn.onClick:AddListener(function()
        moneyNode.naviGroup:ManuallyStopFocus()
    end)

    moneyNode.originiumConvertedDiamond.icon:LoadSprite(UIConst.UI_SPRITE_WALLET, originiumItemCfg.iconId)
    moneyNode.originiumConvertedDiamond.icon2:LoadSprite(UIConst.UI_SPRITE_WALLET, diamondItemCfg.iconId)
    moneyNode.originiumConvertedDiamond.button.onClick:AddListener(function()
        local curIsShow = moneyNode.originiumConvertedDiamond.selected.gameObject.activeSelf
        if curIsShow then
            if not DeviceInfo.usingController then
                moneyNode.originiumConvertedDiamond.selected.gameObject:SetActive(false)
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
            return
        end
        moneyNode.originiumConvertedDiamond.selected.gameObject:SetActive(true)
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = moneyNode.originiumConvertedDiamond.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.MidBottom,
            itemId = Tables.globalConst.originiumItemId,
            isSideTips = true,
            padding = { top = 100 },
            onClose = function()
                if IsNull(moneyNode) or IsNull(moneyNode.originiumConvertedDiamond) then
                    return
                end
                moneyNode.originiumConvertedDiamond.selected.gameObject:SetActive(false)
            end
        })
    end)
end





GachaPoolCtrl.OnWalletChanged = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local cell = self.m_getCell(self.m_curIndex)
    if not cell or string.isEmpty(self.m_curPoolId) then
        return
    end
    cell.cellWidget:UpdateMoneyNodeOnlyMoney(self.view.moneyNode)
    cell.cellWidget:UpdateGachaBtnCost()
end

GachaPoolCtrl.OnItemChanged = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local cell = self.m_getCell(self.m_curIndex)
    if not cell or string.isEmpty(self.m_curPoolId) then
        return
    end
    cell.cellWidget:UpdateMoneyNodeOnlyGachaTicket(self.view.moneyNode)
    cell.cellWidget:UpdateGachaBtnCost()
end

GachaPoolCtrl.OnGachaPoolInfoChanged = HL.Method() << function(self)
end

GachaPoolCtrl.OnGachaPoolRoleDataChanged = HL.Method() << function(self)
end

GachaPoolCtrl._ShowDetailPanel = HL.Method() << function(self)
    CS.Beyond.SDK.SDKUtils.OpenHGWebPortalSDK("gacha_char", string.format("{\"pool_id\":\"%s\"}",self.m_curPoolId), nil)
end

GachaPoolCtrl._OnTestimonialConvert = HL.Method() << function(self)

    local csGachaSystem = GameInstance.player.gacha
    local count = csGachaSystem.testimonialConvertNtfs.Count
    if count > 0 then

        local ntf = csGachaSystem.testimonialConvertNtfs[0]
        local name1 = Tables.itemTable[ntf.TestimonialItemId].name
        local name2 = Tables.itemTable[ntf.ConvertToItemId].name
        local tips = string.format(Language.LUA_GACHA_ITEM_CONVERT_TIP, name1, string.format(Language.LUA_COMMON_NAME_X_COUNT, name2, ntf.ConvertCount))
        local poolId = ntf.CharGachaPoolId

        local hasData, poolData = csGachaSystem.poolInfos:TryGetValue(poolId)
        local arg
        if not hasData or not poolData.isOpenValid then

            arg = {
                queueRewardType = "TestimonialConvert",
                showRewardFunc = function()
                    Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_GACHA_TESTIMONIAL_CONVERT_BUT_EXPIRE_TOAST, name2))
                    csGachaSystem:SetTestimonialConvertNtfIsCheck(0)
                    Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
                end
            }
        else

            arg = {
                queueRewardType = "TestimonialConvert",
                showRewardFunc = function()
                    UIManager:Open(PanelId.GachaItemConvert, {
                        title = Language.LUA_GACHA_ITEM_CONVERT_TITLE_TESTIMONIAL,
                        tipsText = tips,
                        originalItemId = ntf.TestimonialItemId,
                        convertItemId = ntf.ConvertToItemId,
                        convertItemBundle = {
                            id = ntf.ConvertToItemId,
                            count = ntf.ConvertCount,
                        },
                        onComplete = function()
                            csGachaSystem:SetTestimonialConvertNtfIsCheck(0)
                            Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
                        end
                    })
                end
            }
        end
        Notify(MessageConst.GACHA_POOL_ADD_SHOW_REWARD, arg)
    end
end

GachaPoolCtrl._OnDisplaySizeChanged = HL.Method(HL.Opt(HL.Any, HL.Any)) << function(self, _, _)
    self.view.poolList:TryRecalculateSize()
    self.view.poolList:ScrollToIndex(CSIndex(self.m_curIndex))
end

GachaPoolCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    if not arg or not arg.poolId then
        return
    end
    local count = #self.m_pools
    for index = 1, count do
        if self.m_pools[index].id == arg.poolId then
            self.view.poolList:ScrollToIndex(CSIndex(index), true)
            return
        end
    end
end

GachaPoolCtrl._OnPhaseChange = HL.Method(HL.Opt(HL.Any)) << function(self)


    if not PhaseManager:IsOpenAndValid(PhaseId.GachaPool) then
        return
    end
    local needShowParticle = PhaseId.GachaPool == PhaseManager:GetTopPhaseId()
    local cell = self.m_getCell(self.m_curIndex)
    if cell and cell.cellWidget.view.particleEffectNode then
        if cell.cellWidget.view.particleEffectNode.gameObject.activeSelf ~= needShowParticle then
            cell.cellWidget.view.particleEffectNode.gameObject:SetActive(needShowParticle)
        end
    end
end

GachaPoolCtrl.GetSubPanelArg = HL.Method().Return(HL.Any) << function(self)

    local poolCell = self.m_getCell(self.m_curIndex).cellWidget
    local subPanelArg = poolCell:GetSubPanelArg()
    return subPanelArg
end

GachaPoolCtrl.HandleSubPanelArg = HL.Method() << function(self)
    local subPanelArg = self.m_phase.arg.subPanelArg
    if not subPanelArg then
        return
    end

    self.m_phase.arg.subPanelArg = nil

    local poolCell = self.m_getCell(self.m_curIndex).cellWidget
    poolCell:HandleSubPanelArg(subPanelArg)
end



GachaPoolCtrl.OnGachaSucc = HL.Method(HL.Table) << function(self, arg)

    local msg = unpack(arg)
    if msg.GachaPoolId ~= self.m_curPoolId then
        return
    end

    local maxRarity = 4
    local chars = {}
    for k = 0, msg.FinalResults.Count - 1 do

        local v = msg.FinalResults[k]
        local charId = msg.OriResultIds[k]
        local isNew = not string.isEmpty(v.ItemId)
        local items = {}
        for kk = 0, v.RewardIds.Count - 1 do
            local rewardId = v.RewardIds[kk]
            UIUtils.getRewardItems(rewardId, items)
        end

        if not string.isEmpty(v.RewardItemId) then
            table.insert(items, 2, { id = v.RewardItemId, count = 1 })
        end
        local rarity = Tables.characterTable[charId].rarity
        maxRarity = math.max(maxRarity, rarity)

        local firstGetExtraItem
        if not string.isEmpty(v.FirstGotRewardId) then
            firstGetExtraItem = UIUtils.getRewardFirstItem(v.FirstGotRewardId)
        end

        table.insert(chars, {
            charId = charId,
            isNew = isNew,
            items = items,
            firstGetExtraItem = firstGetExtraItem,
            rarity = rarity,
        })
    end
    logger.info("OnGachaSucc", chars)
    self:_ReportPlacementEvent(msg)



    local csGachaSystem = GameInstance.player.gacha
    csGachaSystem.hasSuperSurprise = false
    if maxRarity == 6 then
        local hasInfo, poolInfo = csGachaSystem.poolInfos:TryGetValue(self.m_curPoolId)
        local poolCfg = Tables.gachaCharPoolTable[self.m_curPoolId]
        local poolTypeCfg = Tables.gachaCharPoolTypeTable[poolCfg.type]
        local gachaCount = #chars


        local maxHardGuarantee = poolTypeCfg.hardGuarantee > 0 and 1 or 0
        local remainHardGuaranteeCount = maxHardGuarantee - poolInfo.upGotCount
        local isHardGuarantee = remainHardGuaranteeCount > 0 and poolInfo.hardGuaranteeProgress + gachaCount >= poolTypeCfg.hardGuarantee

        local remainSoftGuaranteeCount = poolTypeCfg.maxSoftGuaranteeCount == 0 and 1 or poolTypeCfg.maxSoftGuaranteeCount - poolInfo.star6GotCount
        local isSoftGuarantee = remainSoftGuaranteeCount > 0 and poolInfo.softGuaranteeProgress + gachaCount >= poolTypeCfg.softGuarantee

        local curIsGuarantee = isHardGuarantee or isSoftGuarantee
        if not curIsGuarantee then
            local randomValue = math.random()
            local superSurpriseProbability = self.view.config.SUPER_SURPRISE_PROBABILITY / 1000
            csGachaSystem.hasSuperSurprise = randomValue < superSurpriseProbability
        end
    end


    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
    local isOpen = PhaseManager:OpenPhaseFast(PhaseId.GachaLauncher, {
        chars = chars,
        onComplete = function(isAllowSkip)
            local openGachaCharArg = {
                fromGacha = true,
                poolId = self.m_curPoolId,
                chars = chars,
                onComplete = function()
                    if self.m_pools[self.m_curIndex].csInfo.isClosed then
                        self:_InitData()
                    else
                        if not IsNull(self.view.poolList) then
                            local cell = self.m_getCell(self.m_curIndex)
                            if cell then
                                self:_OnUpdateCell(cell, self.m_curIndex)
                            end
                        end
                    end
                end
            }
            if isAllowSkip then
                PhaseManager:OpenPhaseFast(PhaseId.GachaChar, openGachaCharArg)
            else
                PhaseManager:OpenPhaseFast(PhaseId.GachaDropBin, {
                    chars = chars,
                    onComplete = function()
                        PhaseManager:OpenPhaseFast(PhaseId.GachaChar, openGachaCharArg)
                    end
                })
            end
        end
    })
    if isOpen then

        Notify(MessageConst.ON_DISABLE_ACHIEVEMENT_TOAST, UIConst.ACHIEVEMENT_TOAST_DISABLE_KEY.GachaChar)
    end
end




GachaPoolCtrl._InitRewardQueueConfigs = HL.Method() << function(self)
    self.m_queueRewardConfigs = {
        GachaResultReward = {
            order = -100,
        },
        PotentialReward = {
            order = 0,
        },
        SelCharTicReward = {
            order = 1,
        },
        TestimonialReward = {
            order = 10,
        },
        TestimonialConvert = {
            order = 11,
        },
    }
end

GachaPoolCtrl.AddQueueReward = HL.Method(HL.Table) << function(self, arg)
    logger.info("GachaPoolCtrl.AddQueueReward：" .. arg.queueRewardType)
    self.m_showRewardFuncQueue:Push({
        queueRewardType = arg.queueRewardType,
        order = self.m_queueRewardConfigs[arg.queueRewardType].order,
        showRewardFunc = arg.showRewardFunc
    })
    self.m_showRewardFuncQueue:Sort(function(x, y)
        return x.order < y.order
    end)
end

GachaPoolCtrl._TryShowQueueReward = HL.Method() << function(self)
    if self.m_showRewardFuncQueue:Count() > 0 and not self.m_curIsShowReward then
        self.m_curIsShowReward = true
        local queueData = self.m_showRewardFuncQueue:Pop()
        queueData.showRewardFunc()
    elseif not self.m_curIsShowReward then
        Notify(MessageConst.ON_GACHA_POOL_ALL_REWARDS_SHOWN)
    end
end

GachaPoolCtrl.OnOneQueueRewardFinished = HL.Method() << function(self)
    self.m_curIsShowReward = false
    self:_TryShowQueueReward()
end



GachaPoolCtrl._ReportPlacementEvent = HL.Method(HL.Any) << function(self, msg)
    local curCount = msg.FinalResults.Count
    if curCount == 10 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Gacha10xFirst)
    end
    local csGacha = GameInstance.player.gacha
    local totalPrevGachaCharCount = 0
    for _, csInfo in pairs(csGacha.poolInfos) do
        if csInfo.isChar then
            totalPrevGachaCharCount = totalPrevGachaCharCount + csInfo.totalPullCountNoShare
        end
    end
    if totalPrevGachaCharCount < 30 and (totalPrevGachaCharCount + curCount) >= 30 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Gacha30xFirst)
    end
end



GachaPoolCtrl._RecoverRewardQueue = HL.Method(HL.Table) << function(self, restoreInfo)
    if not restoreInfo or not restoreInfo.pendingItems then
        return
    end
    for _, item in ipairs(restoreInfo.pendingItems) do
        self.m_showRewardFuncQueue:Push(item)
    end
    self.m_curIsShowReward = restoreInfo.curIsShowReward
end

GachaPoolCtrl.GetRewardQueueRestoreInfo = HL.Method().Return(HL.Table) << function(self)
    local pendingItems = {}
    local count = self.m_showRewardFuncQueue:Count()
    for i = 1, count do
        local item = self.m_showRewardFuncQueue:AtIndex(i)
        if item then
            table.insert(pendingItems, item)
        end
    end
    return {
        curIsShowReward = self.m_curIsShowReward,
        pendingItems = pendingItems,
    }
end

GachaPoolCtrl.GetIsPlayingTabInAnimation = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isPlayingChangeTabInAni
end


HL.Commit(GachaPoolCtrl)
