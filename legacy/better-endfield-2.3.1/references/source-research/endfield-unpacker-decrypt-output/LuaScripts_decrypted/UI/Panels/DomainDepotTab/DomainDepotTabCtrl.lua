local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotTab

local tabConfig = {
    [1] = {
        panelId = PanelId.DomainDepotInstList,
        redDotName = "DomainDepotInstList",
        useDomainIdAsRedDotArg = true,
    },
    [2] = {
        panelId = PanelId.DomainDepotDelivery,
        redDotName = "",
        useDomainIdAsRedDotArg = false,
        hideIfDeliverLocked = true,
    },
    [3] = {
        panelId = PanelId.DomainDepotMyOrder,
        redDotName = "DomainDepotMyOrder",
        useDomainIdAsRedDotArg = false,
        hideIfDeliverLocked = true,
    },
}
DomainDepotTabCtrl = HL.Class('DomainDepotTabCtrl', uiCtrl.UICtrl)

DomainDepotTabCtrl.m_index = HL.Field(HL.Number) << 0

DomainDepotTabCtrl.m_domainId = HL.Field(HL.String) << ''





DomainDepotTabCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHANGE_DOMAIN_DEPOT_TAB] = 'ChangeTab',
}









DomainDepotTabCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg == nil then
        logger.error('DomainDepotTabCtrl.OnCreate: arg is nil')
        return
    end

    if arg.domainId == nil then
        logger.error('DomainDepotTabCtrl.OnCreate: arg.domainId is nil')
        return
    end

    self.m_domainId = arg.domainId
    DomainDepotUtils.SetDomainColorToDepotNodes(self.m_domainId, {
        self.view.dyeAreaNode,
        self.view.marketTabListShadow,
        self.view.decoExImg,
    })

    local isDeliverUnlocked = DomainDepotUtils.IsDeliverUnlocked(self.m_domainId)
    for i = 1, #tabConfig do
        local tabInfo = tabConfig[i]
        local tabNode = self.view['tab'..i]
        if isDeliverUnlocked or not tabInfo.hideIfDeliverLocked then
            tabNode.toggle.onValueChanged:RemoveAllListeners()
            tabNode.toggle.onValueChanged:AddListener(function(isOn)
                if isOn then
                    self:_OnTabChange(i)
                end
            end)

            if not string.isEmpty(tabInfo.redDotName) then
                if tabInfo.useDomainIdAsRedDotArg then
                    tabNode.redDot:InitRedDot(tabInfo.redDotName, self.m_domainId)
                else
                    tabNode.redDot:InitRedDot(tabInfo.redDotName)
                end
                tabNode.redDot.gameObject:SetActive(true)
            else
                tabNode.redDot.gameObject:SetActive(false)
            end
            tabNode.gameObject:SetActive(true)
        else
            tabNode.gameObject:SetActive(false)
        end
    end

    local dataSucc, domainDevData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(self.m_domainId)
    if not dataSucc then
        logger.error("DomainGradeCtrl._UpdateMoneyCell cant find domainDevData: ", self.m_domainId)
        return
    end

    self.view.tabToggleGroup.enabled = isDeliverUnlocked

    CS.Beyond.Gameplay.Conditions.OnDomainDepotMainPanelOpen.Trigger(self.m_domainId, false)
end

DomainDepotTabCtrl.OnAnimationInFinished = HL.Override() << function(self)
    CS.Beyond.Gameplay.Conditions.OnDomainDepotMainPanelOpen.Trigger(self.m_domainId, true)
end

DomainDepotTabCtrl.ChangeTab = HL.Method(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end

    if arg.index == nil then
        arg.index = 1
    end

    if arg.index < 1 or arg.index > #tabConfig then
        logger.error('DomainDepotTabCtrl.ChangeTab: index out of range')
        return
    end

    self:_OnTabChange(arg.index)
end

DomainDepotTabCtrl._OnTabChange = HL.Method(HL.Number) << function(self, index)
    if index < 1 or index > #tabConfig then
        logger.error('DomainDepotTabCtrl._OnTabChange: index out of range')
        return
    end

    if self.m_index == index then
        return
    end

    if self.m_index <= 0 then
        self.view['tab'..index].toggle:SetIsOnWithoutNotify(true)
    end

    self.m_index = index
    local tabInfo = tabConfig[index]
    self.m_phase:OnTabChange(tabInfo.panelId)
end

DomainDepotTabCtrl.ForceResetTab = HL.Method() << function(self)
    self.m_index = 1
    self.view['tab1'].toggle:SetIsOnWithoutNotify(true)
    local tabInfo = tabConfig[1]
    self.m_phase:OnTabChange(tabInfo.panelId)
end

HL.Commit(DomainDepotTabCtrl)
