
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EquipFormulaRewardPopup

EquipFormulaRewardPopupCtrl = HL.Class('EquipFormulaRewardPopupCtrl', uiCtrl.UICtrl)

EquipFormulaRewardPopupCtrl.m_itemListCache = HL.Field(HL.Forward("UIListCache"))

EquipFormulaRewardPopupCtrl.m_toastTimerId = HL.Field(HL.Number) << -1

EquipFormulaRewardPopupCtrl.m_firstFormulaId = HL.Field(HL.String) << ""

EquipFormulaRewardPopupCtrl.m_isInterrupted = HL.Field(HL.Boolean) << false





EquipFormulaRewardPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}

EquipFormulaRewardPopupCtrl.ShowRewardEquipFormula = HL.StaticMethod(HL.Any) << function(arg)
    LuaSystemManager.mainHudActionQueue:AddRequest("EquipFormulaRewardPopup", function()
        local ctrl = UIManager:AutoOpen(PANEL_ID)
        ctrl:ShowReward(arg)
    end)
end


EquipFormulaRewardPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_itemListCache = UIUtils.genCellCache(self.view.itemIcon)









end

EquipFormulaRewardPopupCtrl.ShowReward = HL.Method(HL.Any) << function(self, args)
    local oriItems = unpack(args)

    local items = {}
    for _, item in pairs(oriItems) do
        table.insert(items, { id = item.id, count = item.count })
    end


    for k = 1, #items do
        local v = items[k]
        if type(v) ~= "table" then
            v = { id = v.id, count = v.count }
        end
        local iData = Tables.itemTable[v.id]
        v.sortId1 = iData.sortId1
        v.sortId2 = iData.sortId2
        v.rarity = iData.rarity
    end
    table.sort(items, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

    self.m_firstFormulaId = ""
    self.m_itemListCache:Refresh(#items, function(cell, index)
        local itemInfo = items[index]
        cell:InitItemIcon(itemInfo.id)
        if string.isEmpty(self.m_firstFormulaId) then
            self.m_firstFormulaId = itemInfo.id
        end
    end)

    self.m_toastTimerId = self:_StartTimer(self.view.config.SHOW_TOAST_DURATION, function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if not self.m_isInterrupted then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "EquipFormulaRewardPopup")
            end
        end)
    end)
end

EquipFormulaRewardPopupCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self.m_toastTimerId = self:_ClearTimer(self.m_toastTimerId)
    self.m_isInterrupted = true
    self:Close()
end

HL.Commit(EquipFormulaRewardPopupCtrl)
