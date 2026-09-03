local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DomainDepotPriceListCell = HL.Class('DomainDepotPriceListCell', UIWidgetBase)

DomainDepotPriceListCell.m_onClick = HL.Field(HL.Function)

DomainDepotPriceListCell.m_info = HL.Field(HL.Table)

DomainDepotPriceListCell.m_domainDepotId = HL.Field(HL.String) << ""


DomainDepotPriceListCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if self.m_onClick then
            self.m_onClick(self.m_info)
        end
    end)

    self.view.posBtn.onClick:RemoveAllListeners()
    self.view.posBtn.onClick:AddListener(function()
        DomainDepotUtils.ShowDepotTargetMapPreview(self.m_domainDepotId, self.m_info.targetId)
    end)

    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        if select and self.m_onClick then
            self.m_onClick(self.m_info)
        end
    end)
end

DomainDepotPriceListCell.SetCellState = HL.Method(HL.Any) << function(self, select)
    if select == true then
        self.view.stateController:SetState('Sel')
    elseif select == false then
        self.view.stateController:SetState('Nrl')
    else
        self.view.stateController:SetState('Empty')
    end
end

DomainDepotPriceListCell.InitDomainDepotPriceListCell = HL.Method(HL.String, HL.Table, HL.Function) << function(self, domainDepotId, info, onClick)
    self:_FirstTimeInit()

    if info == nil then
        self.view.inputBindingGroupMonoTarget.enabled = false
        self.view.inputBindingGroupNaviDecorator.interactable = false
        self:SetCellState(nil)
        return
    end

    self.m_onClick = onClick
    self.m_info = info
    self.m_domainDepotId = domainDepotId

    self.view.nameTxt.text = info.name
    self.view.commonPlayerHead.view.playerHead:LoadSprite(UIConst.UI_SPRITE_HEAD, info.headIcon)
    self.view.infoTxt.text = info.desc
    self.view.itemSmall:InitItem(info.reward, true)
    self.view.iconSurprise.gameObject:SetActiveIfNecessary(info.isCritical)
end

HL.Commit(DomainDepotPriceListCell)
return DomainDepotPriceListCell

