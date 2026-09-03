local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DomainTopMoneyTitle = HL.Class('DomainTopMoneyTitle', UIWidgetBase)


DomainTopMoneyTitle.m_domainId = HL.Field(HL.String) << ""





DomainTopMoneyTitle._OnFirstTimeInit = HL.Override() << function(self)
end

DomainTopMoneyTitle.InitDomainTopMoneyTitle = HL.Method(HL.String, HL.Opt(HL.Table)) << function(self, domainId, otherMoneyIds)
    self:_FirstTimeInit()

    self.m_domainId = domainId
    local moneyInfos = {}
    local hasData, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
    local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)

    if hasData and hasCfg then
        local _, moneyCfg = Tables.moneyConfigTable:TryGetValue(domainCfg.domainGoldItemId)
        local info = {
            moneyId = domainCfg.domainGoldItemId,
            showLimit = true,
            limitNumber = domainDevData.curLevelData.moneyLimit,
            clearRuleType = moneyCfg.clearRule,
            clearTipsTextKey = "ui_fac_domaindev_clear_info",
        }
        table.insert(moneyInfos, info)
    end

    if otherMoneyIds then
        for _, moneyId in pairs(otherMoneyIds) do
            local hasMoneyCfg, moneyCfg = Tables.moneyConfigTable:TryGetValue(moneyId)
            local moneyLimit
            if hasMoneyCfg then
                moneyLimit = moneyCfg.MoneyClearLimit
            end
            table.insert(moneyInfos, {
                moneyId = moneyId,
                showLimit = moneyLimit ~= nil,
                limitNumber = moneyLimit,
                clearRuleType = GEnums.MoneyClearRuleType.None,
            })
        end
    end

    self.view.walletBarPlaceholder:InitWalletBarPlaceholderDetailed(moneyInfos)
end



DomainTopMoneyTitle.SetTitleText = HL.Method(HL.String) << function(self, titleText)
    self.view.titleTxt.text = titleText
end


HL.Commit(DomainTopMoneyTitle)
return DomainTopMoneyTitle

