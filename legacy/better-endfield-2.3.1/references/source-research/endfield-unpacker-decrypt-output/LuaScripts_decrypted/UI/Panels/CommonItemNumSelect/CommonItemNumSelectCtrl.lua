
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonItemNumSelect
CommonItemNumSelectCtrl = HL.Class('CommonItemNumSelectCtrl', uiCtrl.UICtrl)





CommonItemNumSelectCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CommonItemNumSelectCtrl.m_itemId = HL.Field(HL.String) << ''

CommonItemNumSelectCtrl.m_count = HL.Field(HL.Number) << 1

CommonItemNumSelectCtrl.m_curCount = HL.Field(HL.Number) << 1

CommonItemNumSelectCtrl.m_onComplete = HL.Field(HL.Function)



CommonItemNumSelectCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.btnCancel.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.btnConfirm.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)

    self.m_itemId = args.id
    self.m_count = args.count
    self.m_onComplete = args.onComplete
    local useSlider = args.useSlider == true

    local maxCount = args.maxCount
    self.view.upLimit.gameObject:SetActiveIfNecessary(maxCount ~= nil)
    if maxCount then
        self.view.upLimitCarryTxt.text = maxCount
    end

    self.view.itemInfoBtn.gameObject:SetActiveIfNecessary(args.showItemInfoBtn)
    self.view.itemInfoBtn.onClick:RemoveAllListeners()
    self.view.itemInfoBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.itemInfoBtn.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
            itemId = self.m_itemId,
        })
    end)

    self.m_curCount = self.m_count
    if useSlider then
        self.view.numberSelector:InitNumberSelector(self.m_curCount, 0, maxCount, function(newNum)
            self:_OnNumChanged(newNum)
        end, true, Utils.getItemCount(self.m_itemId, args.forceIncludeCurDepot or false, true), nil, true)
    else
        self.view.numberSelector:InitNumberSelector(self.m_curCount, 0, maxCount, function(newNum)
            self:_OnNumChanged(newNum)
        end)
    end
    UIUtils.displayItemBasicInfos(self.view, self.loader, self.m_itemId)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

CommonItemNumSelectCtrl.OnClose = HL.Override() << function(self)
    if self.m_onComplete then
        self.m_onComplete()
    end
end

CommonItemNumSelectCtrl._OnNumChanged = HL.Method(HL.Number) << function(self, num)
    self.m_curCount = num
end

CommonItemNumSelectCtrl._OnClickConfirm = HL.Method() << function(self)
    self.m_onComplete(self.m_curCount)
    self:PlayAnimationOutAndClose()
end

HL.Commit(CommonItemNumSelectCtrl)
