
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ItemDragHelper

ItemDragHelperCtrl = HL.Class('ItemDragHelperCtrl', uiCtrl.UICtrl)






ItemDragHelperCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_ITEM_DRAG_HELPER] = 'HideItemDragHelper',
}












ItemDragHelperCtrl.ShowItemDragHelper = HL.StaticMethod(HL.Table) << function(args)

    local isOpen, self = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        local wrapper = self.animationWrapper
        if wrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then
            wrapper:ClearTween(false)
        end
        self:Show()
    else
        self = UIManager:Open(PANEL_ID)
    end
    self.m_isLeft = args.isLeft
    self.m_actions = args.actions
    self:Refresh()
end



ItemDragHelperCtrl.m_isLeft = HL.Field(HL.Boolean) << false

ItemDragHelperCtrl.m_actions = HL.Field(HL.Table)


ItemDragHelperCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    for _, node in ipairs({ self.view.leftNode, self.view.rightNode }) do
        for k = 1, self.view.config.MAX_CELL_COUNT do
            local cell = node["cell" .. k]
            cell.button.onClick:AddListener(function()
                self:_OnClickCell(k)
            end)
        end
    end
end



ItemDragHelperCtrl.HideItemDragHelper = HL.Method() << function(self)
    if not self:IsShow() then
        return
    end
    self.m_actions = nil
    self:PlayAnimationOutAndHide()
end

ItemDragHelperCtrl.Refresh = HL.Method() << function(self)
    self.view.leftNode.gameObject:SetActive(self.m_isLeft)
    self.view.rightNode.gameObject:SetActive(not self.m_isLeft)
    local node = self.m_isLeft and self.view.leftNode or self.view.rightNode
    for k = 1, self.view.config.MAX_CELL_COUNT do
        local cell = node["cell" .. k]
        local info = self.m_actions[k]
        if info then
            cell.gameObject:SetActive(true)
            cell.text.text = info.text
            cell.icon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)
            cell.iconShadow:LoadSprite(UIConst.UI_SPRITE_INVENTORY, info.icon)
        else
            cell.gameObject:SetActive(false)
        end
    end
end

ItemDragHelperCtrl._OnClickCell = HL.Method(HL.Number) << function(self, index)
    local info = self.m_actions[index]
    self:HideItemDragHelper()
    info.action()
end

HL.Commit(ItemDragHelperCtrl)
