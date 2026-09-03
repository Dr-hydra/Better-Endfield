local GachaPoolCellBase = require_ex('UI/Widgets/GachaPoolCellBase')

GachaPoolCellStandard = HL.Class('GachaPoolCellStandard', GachaPoolCellBase)


GachaPoolCellStandard.m_choicePackJumpArg = HL.Field(HL.Table)




GachaPoolCellStandard._OnFirstTimeInit = HL.Override() << function(self)
    GachaPoolCellStandard.Super._OnFirstTimeInit(self)
    self:_InitUI()
end

GachaPoolCellStandard._InnerInitGachaPoolCell = HL.Override() << function(self)
    logger.info("初始化 GachaPoolCellStandard")
    self:_InitData()
end

GachaPoolCellStandard._InnerUpdateGachaPoolCell = HL.Override() << function(self)
    logger.info("更新 GachaPoolCellStandard")
    self:_UpdateData()
    self:_RefreshAllUI()
end

GachaPoolCellStandard.UpdateMoneyNodeOnlyGachaTicket = HL.Override(HL.Any) << function(self, moneyNode)
    moneyNode.gachaItem1.view.gameObject:SetActiveIfNecessary(false)
    moneyNode.gachaItem2.view.gameObject:SetActiveIfNecessary(false)
    moneyNode.gachaItem3.view.gameObject:SetActiveIfNecessary(true)
    local singlePullItemId = self.m_baseInfo.gachaCostInfos.singlePullItemInfos[1].itemId
    moneyNode.gachaItem3:InitMoneyCell(singlePullItemId)
end



GachaPoolCellStandard._InitData = HL.Method() << function(self)
    self.m_choicePackJumpArg = {
        poolId = self.m_poolId,
        remainChoicePackProgress = self.m_baseInfo.cumulateChoicePackInfo.remainNeedPullCount,
        charIds = self.m_baseInfo.previewCharList,
    }
end

GachaPoolCellStandard._UpdateData = HL.Method() << function(self)
    self.m_choicePackJumpArg.remainChoicePackProgress = self.m_baseInfo.cumulateChoicePackInfo.remainNeedPullCount
end



GachaPoolCellStandard._InitUI = HL.Method() << function(self)
    self.view.charChoicePackNode.inviteBtn.onClick:AddListener(function()
        local packInfo = self.m_baseInfo.cumulateChoicePackInfo
        if packInfo.remainReceivedCount > 0 or packInfo.curCanUseCount > 0 then
            UIManager:Open(PanelId.GachaOptional, self.m_choicePackJumpArg)
        end
    end)

    self:RegisterMessage(MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED, function()
        GachaPoolCellStandard.Super._UpdateBaseData(self)
        self:_RefreshAllUI()
    end)
end

GachaPoolCellStandard._RefreshAllUI = HL.Method() << function(self)
    local choicePackInfo = self.m_baseInfo.cumulateChoicePackInfo
    local choicePackNode = self.view.charChoicePackNode

    if choicePackInfo.curCanUseCount > 0 then
        choicePackNode.gameObject:SetActive(true)
        choicePackNode.stateCtrl:SetState("Invitable")
    else
        if choicePackInfo.remainReceivedCount > 0 then
            choicePackNode.stateCtrl:SetState("NotInvitable")
            choicePackNode.remainProgressTxt.text = string.format(Language.LUA_GACHA_STANDARD_CHOICE_PACK_PROGRESS, choicePackInfo.remainNeedPullCount)
        else
            choicePackNode.gameObject:SetActive(false)
        end
    end
end



GachaPoolCellStandard.GetSubPanelArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.GachaOptional)
    if isOpen then
        local subPanelArg = ctrl:GetCurPhaseStateArg()
        if subPanelArg then
            subPanelArg.isGachaOptional = true
            return subPanelArg
        end
    end
    return GachaPoolCellStandard.Super.GetSubPanelArg(self)
end

GachaPoolCellStandard.HandleSubPanelArg = HL.Override(HL.Any) << function(self, subPanelArg)
    if subPanelArg.isGachaOptional then
        UIManager:Open(PanelId.GachaOptional, subPanelArg)
    else
        GachaPoolCellStandard.Super.HandleSubPanelArg(self, subPanelArg)
    end
end


HL.Commit(GachaPoolCellStandard)
return GachaPoolCellStandard

