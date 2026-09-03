
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BlueprintsUnlockHint
BlueprintsUnlockHintCtrl = HL.Class('BlueprintsUnlockHintCtrl', uiCtrl.UICtrl)





BlueprintsUnlockHintCtrl.s_messages = HL.StaticField(HL.Table) << {
}

BlueprintsUnlockHintCtrl.m_itemList = HL.Field(HL.Forward('UIListCache'))
BlueprintsUnlockHintCtrl.m_startIndex = HL.Field(HL.Number) << 1


BlueprintsUnlockHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_itemList = UIUtils.genCellCache(self.view.item)
end







BlueprintsUnlockHintCtrl.ShowBlueprintsUnlockHint = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = UIManager:AutoOpen(PANEL_ID, nil, true)
    ctrl.m_startIndex = 1
    ctrl:_ShowBlueprintsUnlockHint(args)
end

BlueprintsUnlockHintCtrl._ShowBlueprintsUnlockHint = HL.Method(HL.Table) << function(self, args)
    local count = #args.items
    local startIndex = self.m_startIndex
    local endIndex = math.min(count, startIndex + self.view.config.MAX_SHOW_COUNT - 1)
    self.m_itemList:Refresh(endIndex - startIndex + 1, function(cell, index)
        local itemId = args.items[index + startIndex - 1]
        local itemData = Tables.itemTable:GetValue(itemId)
        cell.image:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        cell.name.text = itemData.name
    end)
    self.view.animationWrapper:PlayInAnimation(function()
        if endIndex == count then
            self:Hide()
            if args.onComplete then
                args.onComplete()
            end
        else
            self.m_startIndex = endIndex + 1
            self:_ShowBlueprintsUnlockHint(args)
        end
    end)
end

HL.Commit(BlueprintsUnlockHintCtrl)
