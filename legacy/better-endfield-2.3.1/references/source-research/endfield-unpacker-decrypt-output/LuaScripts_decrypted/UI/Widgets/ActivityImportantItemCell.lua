local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityImportantItemCell = HL.Class('ActivityImportantItemCell', UIWidgetBase)

ActivityImportantItemCell.m_itemId = HL.Field(HL.String) << ""

ActivityImportantItemCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.selectBtn.onClick:AddListener(function()
        self:_OnClickShowItemTips()
    end)
end

ActivityImportantItemCell.InitItem = HL.Method(HL.Any, HL.Opt(HL.Boolean)) << function(self, item, showEnd)
    self:_FirstTimeInit()

    self.m_itemId = tostring(item.id)
    local success, itemData = Tables.itemTable:TryGetValue(item.id)
    if success then
        self.view.itemIcon.sprite = self:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    end

    self.view.itemNumTxt.text = item.count
    self.view.itemNumTxtShadow.text = item.count

    if not showEnd then
        self.view.stateController:SetState("AcquireBefore")
    else
        self.view.stateController:SetState("AcquireAfter")
    end
end

ActivityImportantItemCell.SetDoneState = HL.Method(HL.Opt(HL.Function)) << function(self, callback)
    self.view.stateController:SetState("AcquireAfter")
    self.view.animationWrapper:PlayInAnimation(function()
        if callback then
            callback()
        end
    end)
end

ActivityImportantItemCell._OnClickShowItemTips = HL.Method() << function(self)
    if string.isEmpty(self.m_itemId) then
        return
    end
    self.view.selectedBG.gameObject:SetActive(true)
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = self.transform,
        itemId = self.m_itemId,
        isSideTips = true,
        onClose = function()
            self.view.selectedBG.gameObject:SetActive(false)
        end,
    })
end

HL.Commit(ActivityImportantItemCell)
return ActivityImportantItemCell
