local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotDelivery

DomainDepotDeliveryCtrl = HL.Class('DomainDepotDeliveryCtrl', uiCtrl.UICtrl)

DomainDepotDeliveryCtrl.m_getCellFunc = HL.Field(HL.Function)

DomainDepotDeliveryCtrl.m_filterDomainIdList = HL.Field(HL.String) << ""

DomainDepotDeliveryCtrl.m_domainDropDownInfo = HL.Field(HL.Table)

DomainDepotDeliveryCtrl.m_curDomainIndex = HL.Field(HL.Number) << 1

DomainDepotDeliveryCtrl.m_resumeState = HL.Field(HL.Table)





DomainDepotDeliveryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_DOMAIN_DEPOT_DELIVERY_SYNC] = 'OnSync',
}


DomainDepotDeliveryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local inited = false
    self.m_resumeState = arg and arg.resumeState or nil
    GameInstance.player.domainDepotSystem.remoteDelegateDeliverList:Clear()
    self.view.refreshBtn.onClick:RemoveAllListeners()
    self.view.refreshBtn.onClick:AddListener(function()
        self:_OnRefreshBtnClick()
    end)


    if self.m_resumeState and self.m_resumeState.filterDomainId ~= nil then
        self.m_filterDomainIdList = self.m_resumeState.filterDomainId
    end



    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(object, index)
        local cell = self.m_getCellFunc(object)
        cell:InitDomainDepotDeliveryCell(GameInstance.player.domainDepotSystem.remoteDelegateDeliverList[index])
    end)


    self.view.times1Txt.text = GameInstance.player.domainDepotSystem.dailyTakeDelegateCount
    self.view.times1Txt.color = GameInstance.player.domainDepotSystem.dailyTakeDelegateCount == 3 and self.view.config.FULL_COLOR or self.view.config.NORMAL_COLOR
    self.view.times2Txt.text = Tables.domainDepotConst.dailyTakeDelegateCount
    for i = 1, Tables.domainDepotConst.dailyTakeDelegateCount do
        self.view['state' .. i]:SetState(GameInstance.player.domainDepotSystem.dailyTakeDelegateCount == 3 and "red" or i <= GameInstance.player.domainDepotSystem.dailyTakeDelegateCount and "have" or "use")
    end

    self.view.tipsButton.onClick:RemoveAllListeners()
    self.view.tipsButton.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "domain_depot_delivery")
    end)

    self.m_domainDropDownInfo = {
        [1] = {
            domainId = "",
            name = Language.LUA_DOMAIN_DEPOT_DELIVERY_FILTER_ALL,
            icon = "domain_all",
        }
    }
    local curDomainId = Utils.getCurDomainId()
    for domainId, domainData in pairs(Tables.domainDataTable) do
        if GameInstance.player.domainDevelopmentSystem.domainDevDataDic:ContainsKey(domainId) then
            table.insert(self.m_domainDropDownInfo, {
                domainId = domainId,
                name = domainData.domainName,
                icon = domainData.domainIcon,
            })
            if domainId == curDomainId then
                self.m_curDomainIndex = #self.m_domainDropDownInfo
                self.m_filterDomainIdList = domainId
            end
        end

    end

    if self.m_resumeState and self.m_resumeState.filterDomainId ~= nil then
        for index, info in ipairs(self.m_domainDropDownInfo) do
            if info.domainId == self.m_filterDomainIdList then
                self.m_curDomainIndex = index
                break
            end
        end

    end

    self.view.dropDownListUp:Init(function(index, option, isSelected)
        local info = self.m_domainDropDownInfo[LuaIndex(index)]
        option:SetText(info.name)
        option.icon:LoadSprite(UIConst.UI_SPRITE_DOMAIN_ICON, "icon_depot_filter_" .. info.icon)
    end, function(index)
        if inited then
            self.m_filterDomainIdList = self.m_domainDropDownInfo[LuaIndex(index)].domainId
            GameInstance.player.domainDepotSystem:SendSyncDomainDepotDeliverDelegate(self.m_filterDomainIdList)
        end
    end)
    self.view.dropDownListUp:Refresh(#self.m_domainDropDownInfo, CSIndex(self.m_curDomainIndex))

    self.view.dropDownListUp.captionIcon:LoadSprite(UIConst.UI_SPRITE_DOMAIN_ICON, "icon_depot_filter_" .. self.m_domainDropDownInfo[self.m_curDomainIndex].icon)

    GameInstance.player.domainDepotSystem:SendSyncDomainDepotDeliverDelegate(self.m_filterDomainIdList)

    inited = true
end


DomainDepotDeliveryCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        resumeState = {
            filterDomainId = self.m_filterDomainIdList,
        }
    }
end

DomainDepotDeliveryCtrl._OnRefreshBtnClick = HL.Method() << function(self)
    GameInstance.player.domainDepotSystem:SendSyncDomainDepotDeliverDelegate(self.m_filterDomainIdList)
    self.view.refreshBtn.gameObject:SetActive(false)
    self.view.countTimeNode.gameObject:SetActive(true)
    self.view.countDownText:InitCountDownText(5 + DateTimeUtils.GetCurrentTimestampBySeconds(), function()
        self.view.refreshBtn.gameObject:SetActive(true)
        self.view.countTimeNode.gameObject:SetActive(false)
    end, function(sec)
        return UIUtils.getSecondsLeftTime(sec) .. Language.LUA_DOMAIN_DEPOT_DELIVERY_REFRESH_COUNTDOWN
    end)
end

DomainDepotDeliveryCtrl.OnSync = HL.Method() << function(self)
    self.view.times1Txt.text = GameInstance.player.domainDepotSystem.dailyTakeDelegateCount
    self.view.scrollList:UpdateCount(GameInstance.player.domainDepotSystem.remoteDelegateDeliverList.Count)
    self.view.selectableNaviGroup:NaviToThisGroup()
    self.view.emptyNode.gameObject:SetActive(GameInstance.player.domainDepotSystem.remoteDelegateDeliverList.Count == 0)
end

DomainDepotDeliveryCtrl.OnShow = HL.Override() << function(self)
    if GameInstance.player.domainDepotSystem.remoteDelegateDeliverList.Count ~= 0 then
        self:OnSync()
    end
end


DomainDepotDeliveryCtrl.OnHide = HL.Override() << function(self)
    self.view.scrollList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getCellFunc(obj)
        cell.view.animationWrapper:PlayOutAnimation()
    end)
end




HL.Commit(DomainDepotDeliveryCtrl)
