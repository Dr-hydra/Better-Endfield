local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DomainDepotDeliveryCell = HL.Class('DomainDepotDeliveryCell', UIWidgetBase)

DomainDepotDeliveryCell.m_deliveryId = HL.Field(HL.String) << ""

DomainDepotDeliveryCell.m_roleId = HL.Field(HL.String) << ""

DomainDepotDeliveryCell.m_insId = HL.Field(HL.Number) << 0

DomainDepotDeliveryCell.m_canReceive = HL.Field(HL.Boolean) << false

DomainDepotDeliveryCell.m_deliveryOnClick = HL.Field(HL.Function)

DomainDepotDeliveryCell.m_domainDepotId = HL.Field(HL.String) << ""



DomainDepotDeliveryCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.positionBtn.onClick:RemoveAllListeners()
    self.view.positionBtn.onClick:AddListener(function()
        DomainDepotUtils.ShowDepotTargetMapPreview(self.m_domainDepotId, self.m_deliveryId)
    end)

    self.view.taskNode.onClick:RemoveAllListeners()
    self.view.taskNode.onClick:AddListener(function()
        if 0 == GameInstance.player.domainDepotSystem.deliverInstId then
            GameInstance.player.domainDepotSystem:SendDomainDepotDeliverAcceptMsg(self.m_insId)
        elseif GameInstance.player.domainDepotSystem.deliverInstId == self.m_insId then
            PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = Tables.domainDepotConst.depotDeliverMissionId })
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_DOMAIN_DEPOT_DELIVERY_TASK_NOT_ACCEPTED)
        end
    end)

    self.view.inputBindingGroupNaviDecorator.onIsNaviTargetChanged = function(isFocused)
        self.view.bgBtn.interactable = self.m_canReceive or (not isFocused)
    end

    self.view.bgBtn.onClick:RemoveAllListeners()
    self.view.bgBtn.onClick:AddListener(function()
        if not self.m_canReceive then
            return
        end
        if self.m_deliveryOnClick then
            self.m_deliveryOnClick(self.m_insId)
        end
        GameInstance.player.domainDepotSystem:SendDomainDepotCollectDelegateRewardReq({ self.m_insId })
    end)

end

DomainDepotDeliveryCell.InitDomainDepotDeliveryCell = HL.Method(HL.Userdata) << function(self, info)
    self:_FirstTimeInit()



    local timeStamp = info.delegateTimeStamp
    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local timeDiff = timeStamp - currentTime

    self.m_insId = info.insId
    self.m_domainDepotId = info.domainDepotId
    if timeDiff <= 0 then

        logger.error("InitRegionDeliveryCell: 送货已过期，deliveryId: " .. self.m_insId)
    else

        local daysLeft = UIUtils.getLeftTime(timeDiff)
        self.view.timeTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_DELIVERY_NOT_COMPLETED_TIME_LEFT, daysLeft)
    end

    self.view.stateController:SetState(GameInstance.player.domainDepotSystem.deliverInstId == info.insId and "accepted" or "canAccept")
    self.view.accessNode:SetState(GameInstance.player.domainDepotSystem.deliverInstId == info.insId and "recommend" or "Normal")

    self:_UpdateInfo(info)
    self.view.itemSmallCredit.gameObject:SetActiveIfNecessary(true)
    self.view.itemSmallCredit:InitItem({
        id = Tables.spaceshipConst.creditItemId,
        count = Tables.domainDepotConst.delegateExtraRewardCreditCount,
    }, true)
    self.view.itemSmallCredit:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
end




DomainDepotDeliveryCell.InitSelfDomainDepotDeliveryCell = HL.Method(HL.Userdata, HL.Function) << function(self, info, onClick)
    self:_FirstTimeInit()

    self.m_deliveryOnClick = onClick
    self.m_canReceive = false


    local timeStamp = info.delegateTimeStamp
    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local timeDiff = timeStamp - currentTime

    self.m_insId = info.insId
    self.m_domainDepotId = info.domainDepotId

    local roleId = info.delegateRoleId


    local daysLeft = UIUtils.getLeftTime(timeDiff)
    self.view.expiredTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_DELIVERY_NOT_COMPLETED_TIME_LEFT, daysLeft)

    local showPlayerName = roleId ~= 0

    if timeDiff <= 0 or info.packageProgress == GEnums.DomainDepotPackageProgress.SendPackageTimeout then
        self.m_canReceive = true
        self.view.stateController:SetState("expired")
    elseif info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingRecvFinalPayment then
        self.m_canReceive = true
        self.view.stateController:SetState("completed")
        self.view.expiredTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_DELIVERY_COMPLETED_TIME_LEFT, daysLeft)
    elseif roleId == 0 then
        self.view.stateController:SetState("waiting")
    elseif info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingSendPackage or info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingRecvPackage then
        self.view.stateController:SetState("delivering")
    else
        logger.error("InitRegionSelfDeliveryCell: 未知的包裹进度 " .. info.packageProgress:ToString())
    end
    self.view.bgBtn.interactable = self.m_canReceive or (not self.view.inputBindingGroupNaviDecorator.isNaviTarget)
    self.view.redDot.gameObject:SetActive(self.m_canReceive)
    self:_UpdateInfo(info)
    self.view.commonPlayerHead.view.nameText.gameObject:SetActiveIfNecessary(showPlayerName)
    self.view.blankText.gameObject:SetActiveIfNecessary(not showPlayerName and self.view.blankText.gameObject.activeSelf)
    self.view.itemSmallCredit.gameObject:SetActiveIfNecessary(false)

    if DeviceInfo.usingController then
        self.view.rewardsNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            self.view.rewardsKeyHint.gameObject:SetActive(not isFocused)
        end)
    end
end

DomainDepotDeliveryCell._UpdateInfo = HL.Method(HL.Userdata) << function(self, info)
    self.m_deliveryId = info.buyerInfo.targetId
    local roleId = info.delegateRoleId
    self.view.commonPlayerHead:UpdateHideSignature(true)
    self.view.commonPlayerHead:InitCommonPlayerHeadByRoleId(roleId, true)




    local packConfig = Tables.domainDepotDeliverPackTypeTable:GetValue(info.deliverPackType)
    local itemConfig = Tables.domainDepotDeliverItemTypeTable:GetValue(info.itemType)

    local str = I18nUtils.CombineStringReverseForIndonesianAndVietnamese(packConfig.deliveryDesc, itemConfig.deliveryDesc)

    self.view.largeBagTxt.text = str

    self.view.cargoType:SetState(info.itemType:ToString())


    local deliverCfg = Tables.domainDepotDeliverTargetTable:GetValue(info.buyerInfo.targetId)
    local levelDesc = Tables.levelDescTable:GetValue(deliverCfg.level)
    local domainData = Tables.domainDataTable:GetValue(deliverCfg.domainId)

    self.view.positionTxt.text = string.format(Language.LUA_DOMAIN_DEPOT_TARGET_SPACE, levelDesc.showName)


    self.view.itemSmallBlack:InitItem({
        id = domainData.domainGoldItemId,
        count = info.domainGoldRewardCount,
    }, true)
    self.view.itemSmallBlack:SetExtraInfo({ isSideTips = DeviceInfo.usingController })

    if roleId ~= 0 then
        local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
        if success then
            if friendInfo.businessCardTopicId ~= nil then
                local topicSuccess, topicCfg = Tables.businessCardTopicTable:TryGetValue(friendInfo.businessCardTopicId)
                if topicSuccess then
                    self.view.themeBgImg:LoadSprite(UIConst.UI_BUSINESS_CARD_FRIEND_DOMAIN_DEPOT_ICON_PATH, topicCfg.id)
                else
                    logger.error("未找到名片主题配置 " .. friendInfo.businessCardTopicId)
                end
            else
                logger.error("好友名片主题ID为空 " .. friendInfo.roleId)
            end
        else
            logger.error("DomainDepotDeliveryCell: 未找到好友信息，roleId: " .. roleId)
        end
    end
end

HL.Commit(DomainDepotDeliveryCell)
return DomainDepotDeliveryCell

