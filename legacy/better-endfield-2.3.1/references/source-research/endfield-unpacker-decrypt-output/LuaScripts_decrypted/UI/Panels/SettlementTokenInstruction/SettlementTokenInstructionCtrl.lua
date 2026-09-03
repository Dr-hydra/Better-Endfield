local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementTokenInstruction

local settlementSystem = GameInstance.player.settlementSystem

SettlementTokenInstructionCtrl = HL.Class('SettlementTokenInstructionCtrl', uiCtrl.UICtrl)






SettlementTokenInstructionCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SETTLEMENT_REMAIN_MONEY_MODIFY] = '_OnRemainMoneyModify',
}



SettlementTokenInstructionCtrl.m_stlId = HL.Field(HL.String) << ""

SettlementTokenInstructionCtrl.m_curLevel = HL.Field(HL.Number) << 0

SettlementTokenInstructionCtrl.m_info = HL.Field(HL.Table)

SettlementTokenInstructionCtrl.m_tickTimeKey = HL.Field(HL.Number) << -1

SettlementTokenInstructionCtrl.m_isFirstBuffNode = HL.Field(HL.Boolean) << true




SettlementTokenInstructionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    if arg == nil or type(arg) ~= "string" then
        logger.error(ELogChannel.UI, "据点代币说明界面参数错误")
        return
    end
    self:_InitData(arg)
    self:_RefreshAllUI()
end

SettlementTokenInstructionCtrl.OnShow = HL.Override() << function(self)
    self.m_tickTimeKey = LuaUpdate:Add("Tick", function(deltaTime)
        self:_RefreshTimeText()
    end)
    settlementSystem:AddSettlementSyncRequest(self.view.transform.name)
end

SettlementTokenInstructionCtrl.OnClose = HL.Override() << function(self)
    LuaUpdate:Remove(self.m_tickTimeKey)
    settlementSystem:RemoveSettlementSyncRequest(self.view.transform.name)
end



SettlementTokenInstructionCtrl._InitData = HL.Method(HL.Any) << function(self, arg)

    self.m_stlId = arg
    self.m_curLevel = settlementSystem:GetSettlementLevel(self.m_stlId)
    local stlData = Tables.settlementBasicDataTable[self.m_stlId]
    local stlLevelData = stlData.settlementLevelMap[self.m_curLevel]
    local domainCfg = Tables.domainDataTable[stlData.domainId]
    local moneyId = domainCfg.domainGoldItemId
    local moneyItemData = Tables.itemTable[moneyId]
    local basicProduceSpeed = stlLevelData.moneyPeriod * 3600 / Tables.settlementConst.produceMoneyDurationPerLoop


    local officerId = settlementSystem:GetSettlementOfficerId(self.m_stlId)
    local officerEnhanceRate = 0
    if not string.isEmpty(officerId) then
        for _, stlTagId in pairs(stlData.wantTagIdGroup) do
            if settlementSystem:IsCharMatchSettlementTag(officerId, stlTagId) then
                local stlTagData = Tables.settlementTagTable[stlTagId]
                officerEnhanceRate = officerEnhanceRate + stlTagData.enhanceMoneyProduceSpeedRate
            end
        end
    end

    self.m_info = {
        curMoney = settlementSystem:GetSettlementCurMoney(self.m_stlId),
        maxMoney = stlLevelData.moneyMax,
        moneyIcon = moneyItemData.iconId,
        filledMoneyTime = 0,
        basicProduceSpeed = basicProduceSpeed,
        totalProduceSpeed = math.floor(basicProduceSpeed * (100 + officerEnhanceRate) / 100),

        speedHasEnhance = false,
        totalEnhance = 0,

        officerEnhanceRate = officerEnhanceRate,

        hasTimeLimitTdGainEffect = false,
        timeLimitTdEffectLevelName = "",
        timeLimitTdGainEffect = 0,
        tdGainEffectExpirationTs = 0,

        tdGainEffectLevelName = "",
        tdGainEffect = 0,

        activityEnhanceRate = 0,
        activityName = "",
    }

    self:_UpdateDefenseGainEffect()

    local activityEnhanceRate, activityId = DomainPOIUtils.getSettlementMoneyActivityEffectInfo(self.m_stlId)
    local _, activityCfg = Tables.activityTable:TryGetValue(activityId)
    if activityCfg then
        self.m_info.activityEnhanceRate = activityEnhanceRate
        self.m_info.activityName = activityCfg.name
    end

    self.m_info.totalEnhance = self.m_info.officerEnhanceRate + self.m_info.tdGainEffect + self.m_info.timeLimitTdGainEffect + self.m_info.activityEnhanceRate
    self.m_info.speedHasEnhance = self.m_info.totalEnhance ~= 0
    self.m_info.totalProduceSpeed = math.floor(basicProduceSpeed * (100 + self.m_info.totalEnhance) / 100)
    self.m_info.filledMoneyTime = self:_GetFilledMoneyTime()

end

SettlementTokenInstructionCtrl._UpdateDefenseGainEffect = HL.Method() << function(self)
    local settlementData = settlementSystem:GetUnlockSettlementData(self.m_stlId)
    if settlementData == nil then
        return
    end
    if settlementData ~= nil and DateTimeUtils.GetCurrentTimestampBySeconds() < settlementData.tdGainEffectExpirationTs then
        self.m_info.hasTimeLimitTdGainEffect = true
        self.m_info.timeLimitTdEffectLevelName = self:_GetLevelGroupName(settlementData.timeLimitTdGainEffectByTdId)
        self.m_info.timeLimitTdGainEffect = settlementData.timeLimitTdGainEffect
        self.m_info.tdGainEffectExpirationTs = settlementData.tdGainEffectExpirationTs
    end

    if settlementData.tdGainEffect > 0 then
        self.m_info.tdGainEffect = settlementData.tdGainEffect
        self.m_info.tdGainEffectLevelName = self:_GetLevelGroupName(settlementData.tdGainEffectByTdId)
    end
end



SettlementTokenInstructionCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SettlementTokenInstructionCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.curOwnMoneyTxt.text = self.m_info.curMoney
    self.view.maxOwnMoneyTxt.text = "/" .. self.m_info.maxMoney
    self.view.moneyIconImg1:LoadSprite(UIConst.UI_SPRITE_WALLET, self.m_info.moneyIcon)
    self.view.moneyIconImg2:LoadSprite(UIConst.UI_SPRITE_WALLET, self.m_info.moneyIcon)

    self.view.moneyProduceSpeedTxt.text = self.m_info.totalProduceSpeed
    self.view.moneyProduceStateCtrl:SetState(self.m_info.speedHasEnhance and "HasEnhance" or "Normal")
    self.view.basicSpeedTxt.text = string.format(Language.LUA_SETTLEMENT_BASIC_MONEY_PRODUCE_SPEED, self.m_info.basicProduceSpeed)

    if self.m_info.speedHasEnhance then
        self.view.improveNode.gameObject:SetActive(true)
        local totalEnhance = math.floor(self.m_info.totalEnhance)
        self.view.improvePercentTxt.text = string.format("%d%%", totalEnhance)
        self.view.improveDescTxt:SetAndResolveTextStyle(string.format(Language.LUA_SETTLEMENT_MONEY_EXTRA_ENHANCE_TEXT, totalEnhance))

        if self.m_info.officerEnhanceRate ~= 0 then
            self.view.officerNode.gameObject:SetActive(true)
            self.view.officerNode.officerEffectNumTxt.text = string.format("%d%%", self.m_info.officerEnhanceRate)
            self:_TryHandleFirstBuffNode(self.view.officerNode)
        else
            self.view.officerNode.gameObject:SetActive(false)
        end

        self:_RefreshGainEffect()

        if self.m_info.activityEnhanceRate ~= 0 then
            self.view.activityNode.gameObject:SetActive(true)
            self.view.activityNode.activityTitleTxt.text = string.format(Language.LUA_SETTLEMENT_ACTIVITY_EFFECT_INSTRUCTION, self.m_info.activityName)
            self.view.activityNode.activityEffectNumTxt.text = string.format("%d%%", self.m_info.activityEnhanceRate)
            self:_TryHandleFirstBuffNode(self.view.activityNode)
        else
            self.view.activityNode.gameObject:SetActive(false)
        end
    else
        self.view.improveNode.gameObject:SetActive(false)
    end
    self:_RefreshTimeText()
end

SettlementTokenInstructionCtrl._RefreshTimeText = HL.Method() << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = self:_GetFilledMoneyTime() - curTime
    if leftSec <= 0 then
        self.view.timeNode.gameObject:SetActive(false)
        return
    end

    self.view.timeNode.gameObject:SetActive(true)
    local showTimeStr = UIUtils.getFullLeftTime(leftSec)
    self.view.timeTxt.text = string.format(Language.LUA_SETTLEMENT_FILLED_MONEY_TIME, showTimeStr)
end


SettlementTokenInstructionCtrl._GetFilledMoneyTime = HL.Method().Return(HL.Number) << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftMoney = self.m_info.maxMoney - self.m_info.curMoney
    if leftMoney <= 0 then
        return curTime
    end

    local leftTime = leftMoney * 3600 / self.m_info.totalProduceSpeed
    return curTime + leftTime
end

SettlementTokenInstructionCtrl._RefreshGainEffect = HL.Method() << function(self)
    self.view.manualDefenseNode.gameObject:SetActive(false)
    self.view.defensivePlanNode.gameObject:SetActive(false)

    if self.m_info.hasTimeLimitTdGainEffect then
        self.view.manualDefenseNode.gameObject:SetActive(true)
        self.view.manualDefenseNode.gainSpeedTxt.text = string.format(Language.LUA_TD_LEVEL_COMPLETED_FORMAT, self.m_info.timeLimitTdEffectLevelName)
        self.view.manualDefenseNode.buffManNumTxt.text = string.format("%d%%", math.floor(self.m_info.timeLimitTdGainEffect))
        self.view.manualDefenseNode.durationNode:InitCountDownText(self.m_info.tdGainEffectExpirationTs, function()
            self:_UpdateDefenseGainEffect()
            self:_RefreshGainEffect()
        end, UIUtils.getLeftTimeToSecond)
        self:_TryHandleFirstBuffNode(self.view.manualDefenseNode)
    end


    if self.m_info.tdGainEffect > 0 then
        self.view.defensivePlanNode.gameObject:SetActive(true)
        self.view.defensivePlanNode.defensivePlanTxt.text = string.format(Language.LUA_TD_LEVEL_COMPLETED_FORMAT, self.m_info.tdGainEffectLevelName)
        self.view.defensivePlanNode.buffNumTxt.text = string.format("%d%%", math.floor(self.m_info.tdGainEffect))
        self:_TryHandleFirstBuffNode(self.view.defensivePlanNode)
    end
end





SettlementTokenInstructionCtrl._OnRemainMoneyModify = HL.Method(HL.Any) << function(self, arg)
    local stlId, curMoney = unpack(arg)
    if self.m_stlId ~= stlId or self.m_info == nil then
        return
    end

    self.m_info.curMoney = curMoney
    self.m_info.filledMoneyTime = self:_GetFilledMoneyTime()
    self.view.curOwnMoneyTxt.text = curMoney
end

SettlementTokenInstructionCtrl._GetLevelGroupName = HL.Method(HL.String).Return(HL.String) << function(self, tdId)
    local levelName = ""
    local _, tdLevelData = Tables.towerDefenseTable:TryGetValue(tdId)
    if tdLevelData then
        local _, groupTableData = Tables.towerDefenseGroupTable:TryGetValue(tdLevelData.tdGroup)
        if groupTableData then
            levelName = groupTableData.name
        end
    end
    return levelName
end

SettlementTokenInstructionCtrl._TryHandleFirstBuffNode = HL.Method(HL.Any) << function(self, node)
    if not self.m_isFirstBuffNode then
        return
    end

    node.splitLine.gameObject:SetActive(false)
    self.m_isFirstBuffNode = false
end


HL.Commit(SettlementTokenInstructionCtrl)
