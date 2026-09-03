local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.KiteStationCollectionReward

KiteStationCollectionRewardCtrl = HL.Class('KiteStationCollectionRewardCtrl', uiCtrl.UICtrl)

KiteStationCollectionRewardCtrl.m_insId = HL.Field(HL.String) << ""

KiteStationCollectionRewardCtrl.m_getCellFunc = HL.Field(HL.Function)

KiteStationCollectionRewardCtrl.m_collectionCount = HL.Field(HL.Number) << 0

KiteStationCollectionRewardCtrl.m_collectionReward = HL.Field(HL.Number) << 0





KiteStationCollectionRewardCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_KITE_STATION_COLLECTION_REWARD] = '_OnCollectionReward'

}

local function _HasCollectionFlag(mask, flag)
    return (mask & flag) == flag
end

local function _GetRewardFlag(rewardIndex)
    return 1 << rewardIndex
end

KiteStationCollectionRewardCtrl._RefreshCollectionData = HL.Method() << function(self)
    self.m_collectionCount = GameInstance.player.kiteStationSystem:GetKiteStationCollectionCount(self.m_insId)
    self.m_collectionReward = GameInstance.player.kiteStationSystem:GetKiteStationCollectionReward(self.m_insId)
end

KiteStationCollectionRewardCtrl._HasAnyRewardCanReceive = HL.Method(HL.Any).Return(HL.Boolean) << function(self, cfg)
    for rewardIndex = 0, cfg.rewardList.Count - 1 do
        local data = cfg.rewardList[rewardIndex]
        local rewardFlag = _GetRewardFlag(rewardIndex)
        local isAlreadyReceived = _HasCollectionFlag(self.m_collectionReward, rewardFlag)
        local canReceiveReward = self.m_collectionCount >= data.collectionCnt
        if not isAlreadyReceived and canReceiveReward then
            return true
        end
    end
    return false
end


KiteStationCollectionRewardCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self.m_phase:CloseCollectionReward()
    end)

    if arg and arg.insId then
        self.m_insId = arg.insId
    else
        logger.error("KiteStationCollectionRewardCtrl.OnCreate: insId is required in arg")
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self:_RefreshCollectionData()

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)


    local levelCfg = Tables.kiteStationLevelTable:GetValue(self.m_insId)
    self.view.nameTxt.text = levelCfg.list[1].name

    local success, cfg = Tables.kiteStationRewardTable:TryGetValue(self.m_insId)

    local lastCell = nil
    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        local data = cfg.rewardList[csIndex]
        local cell = self.m_getCellFunc(object)
        local rewardFlag = _GetRewardFlag(csIndex)
        local canReceiveReward = self.m_collectionCount >= data.collectionCnt
        local isAlreadyReceived = _HasCollectionFlag(self.m_collectionReward, rewardFlag)


        if LuaIndex(csIndex) == cfg.rewardList.Count then
            cell.bgImage.gameObject:SetActiveIfNecessary(false)
            cell.sliderImage.gameObject:SetActiveIfNecessary(false)
            if lastCell and canReceiveReward then
                lastCell.sliderImage.fillAmount = 1.0
            end
        else
            cell.bgImage.gameObject:SetActiveIfNecessary(true)
            cell.sliderImage.gameObject:SetActiveIfNecessary(true)


            if data.collectionCnt > self.m_collectionCount then
                cell.sliderImage.fillAmount = 0.0
            elseif data.collectionCnt == self.m_collectionCount then
                cell.sliderImage.fillAmount = 0.0
                if lastCell then
                    lastCell.sliderImage.fillAmount = 1.0
                end
            else
                cell.sliderImage.fillAmount = 0.5
                if lastCell then
                    lastCell.sliderImage.fillAmount = 1.0
                end
            end
            lastCell = cell
        end

        local stateName = isAlreadyReceived and "AlreadyReceived" or (canReceiveReward and "CanReceive" or "NotAvailable")
        cell.stateController:SetState(stateName)
        cell.numberTxt.text = data.collectionCnt

        local rewardSuccess, rewardCfg = Tables.rewardTable:TryGetValue(data.rewardId)

        if rewardSuccess and rewardCfg then

            cell.rewardItemBlack:InitItem(rewardCfg.itemBundles[0], true)
        else
            logger.error("KiteStationCollectionRewardCtrl.OnCreate: Invalid rewardId " .. data.rewardId)
        end

        cell.receivedBtn.onClick:RemoveAllListeners()
        cell.receivedBtn.onClick:AddListener(function()
            GameInstance.player.kiteStationSystem:SendKiteStationCollectReward(self.m_insId, {[1] = data.collectionCnt})
        end)
    end)
    local naviGroup = self.view.scrollList:GetComponent("UISelectableNaviGroup")
    self.view.scrollList:UpdateCount(cfg.rewardList.Count)
    naviGroup:NaviToThisGroup()
    self.view.numberTxt.text = self.m_collectionCount

    local canReward = self:_HasAnyRewardCanReceive(cfg)
    self.view.confirmBtn.gameObject:SetActiveIfNecessary(canReward)

    self.view.confirmBtn.onClick:RemoveAllListeners()
    self.view.confirmBtn.onClick:AddListener(function()
        local cnts = {}
        for rewardIndex = 0, cfg.rewardList.Count - 1 do
            local data = cfg.rewardList[rewardIndex]
            local rewardFlag = _GetRewardFlag(rewardIndex)
            local canReceiveReward = self.m_collectionCount >= data.collectionCnt
            local isAlreadyReceived = _HasCollectionFlag(self.m_collectionReward, rewardFlag)
            if not isAlreadyReceived and canReceiveReward then
                table.insert(cnts, data.collectionCnt)
            end
        end
        GameInstance.player.kiteStationSystem:SendKiteStationCollectReward(self.m_insId, cnts)
    end)
end

KiteStationCollectionRewardCtrl._OnCollectionReward = HL.Method(HL.Any) << function(self, pack)
    local rewardPack = unpack(pack)
    local success, cfg = Tables.kiteStationRewardTable:TryGetValue(self.m_insId)
    if not success then
        logger.error("KiteStationCollectionRewardCtrl._OnCollectionReward: Invalid insId " .. self.m_insId)
        return
    end

    self:_RefreshCollectionData()
    self.view.scrollList:UpdateCount(cfg.rewardList.Count)
    self.view.numberTxt.text = self.m_collectionCount
    local canReward = self:_HasAnyRewardCanReceive(cfg)
    self.view.confirmBtn.gameObject:SetActiveIfNecessary(canReward)

    if rewardPack == nil or rewardPack.itemBundleList == nil or rewardPack.itemBundleList.Count == 0 then
        return
    end

    Notify(MessageConst.SHOW_SYSTEM_REWARDS,{
        items = rewardPack.itemBundleList,
        chars = rewardPack.chars,
    })
end











HL.Commit(KiteStationCollectionRewardCtrl)
