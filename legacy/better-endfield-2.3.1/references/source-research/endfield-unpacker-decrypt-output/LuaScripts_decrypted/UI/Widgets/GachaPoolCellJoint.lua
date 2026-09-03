local GachaPoolCellBase = require_ex('UI/Widgets/GachaPoolCellBase')

GachaPoolCellJoint = HL.Class('GachaPoolCellJoint', GachaPoolCellBase)


GachaPoolCellJoint.m_choicePackJumpArg = HL.Field(HL.Table)




GachaPoolCellJoint._OnFirstTimeInit = HL.Override() << function(self)
    GachaPoolCellJoint.Super._OnFirstTimeInit(self)
    self:_InitUI()
end

GachaPoolCellJoint._InnerInitGachaPoolCell = HL.Override() << function(self)
    logger.info("初始化 GachaPoolCellJoint")
    self.view.gachaTenBtn.redDot:InitRedDot("GachaCharTenLtTicket", self.m_poolId)
end

GachaPoolCellJoint._InnerUpdateGachaPoolCell = HL.Override() << function(self)
    logger.info("更新 GachaPoolCellJoint")
    self:_RefreshAllUI()
end



GachaPoolCellJoint._InitUI = HL.Method() << function(self)
    self.view.freeTenNode.gachaFreeBtn.button.onClick:AddListener(function()
        if not self:_CheckCanGacha() then
            return
        end
        GameInstance.player.gacha:GachaFreeTen(self.m_poolId)
    end)

    self.view.selCharTicRewardBtn.button.onClick:AddListener(function()
        local onceRewardInfo = self.m_baseInfo.onceAutoRewardItemInfo2
        local itemId = onceRewardInfo.itemId
        local itemName = Tables.itemTable[itemId].name
        local arg = {
            title = Language.LUA_GACHA_ITEM_INSTRUCTION_TITLE_SEL_CHAR_TIC,
            desc = string.format(Language.LUA_GACHA_ITEM_INSTRUCTION_DESC_SEL_CHAR_TIC, onceRewardInfo.needPullCount, itemName),
            tips = string.format(Language.LUA_GACHA_ITEM_INSTRUCTION_TIPS, onceRewardInfo.remainNeedPullCount, itemName),
            itemId = itemId,
        }
        UIManager:Open(PanelId.GachaItemInstructionPopup, arg)
    end)
    self.view.potentialRewardBtn.button.onClick:AddListener(function()
        local loopRewardInfo = self.m_baseInfo.loopCumulateRewardInfo
        local itemId = loopRewardInfo.rewardItemInfo[1].id
        local itemName = Tables.itemTable[itemId].name
        local arg = {
            title = Language.LUA_GACHA_ITEM_INSTRUCTION_TITLE_POTENTIAL_BOX,
            desc = string.format(Language.LUA_GACHA_ITEM_INSTRUCTION_DESC_POTENTIAL_BOX, loopRewardInfo.needPullCount, itemName),
            tips = string.format(Language.LUA_GACHA_ITEM_INSTRUCTION_TIPS, loopRewardInfo.remainNeedPullCount, itemName),
            itemId = itemId,
        }
        UIManager:Open(PanelId.GachaItemInstructionPopup, arg)
    end)
end

GachaPoolCellJoint._RefreshAllUI = HL.Method() << function(self)
    local baseInfo = self.m_baseInfo

    local freeTenInfo = baseInfo.cumulateFreeTenGachaInfo
    if freeTenInfo.curCanUseCount > 0 then
        self.view.freeTenNode.stateController:SetState("FreeTen")
        self.view.freeTenNode.stateController:SetState("HideFreeTenTip")
    else
        self.view.freeTenNode.stateController:SetState("Normal")

        if freeTenInfo.remainFreeCount > 0 then
            self.view.freeTenNode.stateController:SetState("ShowFreeTenTip")
            self.view.freeTenNumTxt.text = freeTenInfo.remainNeedPullCount
        else
            self.view.freeTenNode.stateController:SetState("HideFreeTenTip")
        end
    end

    local onceRewardInfo = baseInfo.onceAutoRewardItemInfo
    if string.isEmpty(onceRewardInfo.itemId) then
        logger.error("当前卡池的一次性自动发放奖励配置为空！卡池id：" .. self.m_poolId)
        self.view.selCharTicRewardBtn.gameObject:SetActive(false)
    else
        local canShowReward = freeTenInfo.curCanUseCount <= 0
            and freeTenInfo.remainFreeCount <= 0
            and onceRewardInfo.remainReceivedCount > 0
        if canShowReward then
            self.view.tenTicketTipsNode.gameObject:SetActive(true)
            self.view.tenTicketTipsNode.remainNeedPullCountTxt.text = onceRewardInfo.remainNeedPullCount
        else
            self.view.tenTicketTipsNode.gameObject:SetActive(false)
        end
    end

    local onceRewardInfo2 = baseInfo.onceAutoRewardItemInfo2
    if string.isEmpty(onceRewardInfo2.itemId) then
        logger.error("当前卡池的一次性自动发放奖励配置为空！卡池id：" .. self.m_poolId)
        self.view.selCharTicRewardBtn.gameObject:SetActive(false)
    else
        local canShowReward = freeTenInfo.curCanUseCount <= 0 and onceRewardInfo2.remainReceivedCount > 0
        if canShowReward then
            self.view.selCharTicRewardBtn.gameObject:SetActive(true)
            self.view.selCharTicRewardBtn.gachaItem.itemIcon:InitItemIcon(onceRewardInfo2.itemId)
            self.view.selCharTicRewardBtn.gachaItem.numText.text = onceRewardInfo2.itemCount
            self.view.selCharTicRewardBtn.remainNeedPullCountTxt.text = onceRewardInfo2.remainNeedPullCount
        else
            self.view.selCharTicRewardBtn.gameObject:SetActive(false)
        end
    end

    local loopRewardInfo = baseInfo.loopCumulateRewardInfo
    if #loopRewardInfo.rewardItemInfo <= 0 then
        logger.error("当前卡池的循环奖励配置为空！卡池id：" .. self.m_poolId)
        self.view.potentialRewardBtn.gameObject:SetActive(false)
    else
        local canShowLoopReward = freeTenInfo.curCanUseCount <= 0 and onceRewardInfo2.remainReceivedCount <= 0
        if canShowLoopReward then
            local itemInfo = loopRewardInfo.rewardItemInfo[1]
            self.view.potentialRewardBtn.gameObject:SetActive(true)
            self.view.potentialRewardBtn.gachaItem.itemIcon:InitItemIcon(itemInfo.id)
            self.view.potentialRewardBtn.gachaItem.numText.text = itemInfo.count
            self.view.potentialRewardBtn.remainNeedPullCountTxt.text = loopRewardInfo.remainNeedPullCount
        else
            self.view.potentialRewardBtn.gameObject:SetActive(false)
        end
    end
end




GachaPoolCellJoint.CheckAndShowSpecialRewardPopup = HL.Override() << function(self)

    local csGachaSystem = GameInstance.player.gacha
    local baseInfo = self.m_baseInfo

    local onceRewardInfo = baseInfo.onceAutoRewardItemInfo
    if not onceRewardInfo.isCheck then
        local poolId = self.m_poolId
        local arg = {
            queueRewardType = "TestimonialReward",
            showRewardFunc = function()
                local itemName = UIUtils.getItemName(onceRewardInfo.itemId)
                local formatName = string.format(Language.LUA_COMMON_NAME_X_COUNT, itemName, onceRewardInfo.itemCount)
                UIManager:AutoOpen(PanelId.GachaImportantRewardPopup, {
                    itemId = onceRewardInfo.itemId,
                    itemCount = onceRewardInfo.itemCount,
                    desc = string.format(Language.LUA_GACHA_GOT_TEN_TICKET_IMPORTANT_DESC, formatName),
                    onComplete = function()
                        csGachaSystem:SendConfirmRewardReq(poolId, CS.Proto.GACHA_CONFIRM_REWARD_TYPE.GcrtOnceReward, {
                            CS.Proto.CHAR_GACHA_ONCE_AUTO_REWARD_INDEX.CgoarIndexOnceRewardId:GetHashCode()
                        })
                        Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
                    end,
                })
            end
        }
        Notify(MessageConst.GACHA_POOL_ADD_SHOW_REWARD, arg)
    end

    local onceRewardInfo2 = baseInfo.onceAutoRewardItemInfo2
    if not onceRewardInfo2.isCheck then
        local itemId = onceRewardInfo2.itemId
        local poolId = self.m_poolId
        local arg = {
            queueRewardType = "SelCharTicReward",
            showRewardFunc = function()
                UIManager:AutoOpen(PanelId.GachaSelCharTicPopup, {
                    itemId = itemId,
                    onComplete = function()
                        csGachaSystem:SendConfirmRewardReq(poolId, CS.Proto.GACHA_CONFIRM_REWARD_TYPE.GcrtOnceReward, {
                            CS.Proto.CHAR_GACHA_ONCE_AUTO_REWARD_INDEX.CgoarIndexOnceRewardId2:GetHashCode()
                        })
                        Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
                    end,
                })
            end
        }
        Notify(MessageConst.GACHA_POOL_ADD_SHOW_REWARD, arg)
    end

    local loopRewardInfo = baseInfo.loopCumulateRewardInfo
    if not loopRewardInfo.allIsCheck then

        local suc, poolData = csGachaSystem.poolInfos:TryGetValue(self.m_poolId)
        if suc then
            local poolId = self.m_poolId
            local poolCfg = Tables.gachaCharPoolTable[poolId]
            for loopRound, isCheck in pairs(poolData.roleDataMsg.IntervalAutoRewardCheckMap) do
                if not isCheck then
                    local arg = {
                        queueRewardType = "PotentialReward",
                        showRewardFunc = function()
                            UIManager:AutoOpen(PanelId.GachaPotentialPopup, {
                                charId = poolCfg.upCharIds[0],
                                potentialItemId = loopRewardInfo.rewardItemInfo[1].id,
                                isPotentialBox = true,
                                onComplete = function()
                                    csGachaSystem:SendConfirmRewardReq(poolId, CS.Proto.GACHA_CONFIRM_REWARD_TYPE.GcrtIntervalReward, {
                                        loopRound
                                    })
                                    Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
                                end,
                            })
                        end
                    }
                    Notify(MessageConst.GACHA_POOL_ADD_SHOW_REWARD, arg)
                end
            end
        end
    end
end


HL.Commit(GachaPoolCellJoint)
return GachaPoolCellJoint

