local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BattlePassPlanCell = HL.Class('BattlePassPlanCell', UIWidgetBase)

BattlePassPlanCell.m_itemCellCache = HL.Field(HL.Forward("UIListCache"))


BattlePassPlanCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemCellCache = UIUtils.genCellCache(self.view.itemNode)
end

BattlePassPlanCell.InitBattlePassPlanCell = HL.Method(HL.Any, HL.Opt(HL.Any, HL.Function)) << function(self, itemBundles, levelInfo, onTakeReward)
















    self:_FirstTimeInit()

    if not self.view.config.LOOP_REWARD and levelInfo ~= nil and self.view.lvTxt ~= nil then
        self.view.lvTxt.text = levelInfo.level
    end

    self.m_itemCellCache:Refresh(#itemBundles, function(cell, luaIndex)
        local itemInfo = itemBundles[luaIndex]
        if itemInfo ~= nil and not string.isEmpty(itemInfo.id) then
            local isObtained = itemInfo.obtained == true
            local canObtain = itemInfo.canObtain == true
            local isUnlocked = itemInfo.isUnlocked == true
            if not isObtained and canObtain and not string.isEmpty(itemInfo.trackId) and onTakeReward ~= nil then
                cell.itemBlack:InitItem(itemInfo, function(itemBundle)
                    onTakeReward(itemInfo.trackId, levelInfo.level)
                end)
                cell.itemBlack.view.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.PressConfirmTriggerOnClick)
            else
                cell.itemBlack:InitItem(itemInfo, true)
                cell.itemBlack.view.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
            end
            if DeviceInfo.usingController then
                cell.itemBlack.view.button.customBindingViewLabelText = (not isObtained and canObtain)
                    and I18nUtils.GetText("key_hint_bp_plan_item_reward")
                    or I18nUtils.GetText("key_hint_bp_plan_item_details")
            end
            cell.isEmpty = false
            cell.canObtain = not isObtained and canObtain
            cell.isObtained = isObtained
            cell.stateController:SetState(isObtained and "Obtained" or (canObtain and "CanObtain" or "Normal"))
            cell.stateController:SetState(isUnlocked and "Unlocked" or "Lock")
        else
            cell.isEmpty = true
            cell.canObtain = false
            cell.isObtained = false
            cell.stateController:SetState("Empty")
        end
    end)
end

BattlePassPlanCell.SetAsNaviFocusCell = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    for _, itemCell in pairs(self.m_itemCellCache:GetItems()) do
        if itemCell.isEmpty ~= true and itemCell.canObtain then
            self:SetNaviTarget(itemCell.itemBlack.view.button)
            return
        end
    end

    for _, itemCell in pairs(self.m_itemCellCache:GetItems()) do
        if itemCell.isEmpty ~= true and itemCell.isObtained then
            self:SetNaviTarget(itemCell.itemBlack.view.button)
            return
        end
    end

    for _, itemCell in pairs(self.m_itemCellCache:GetItems()) do
        if itemCell.isEmpty ~= true then
            self:SetNaviTarget(itemCell.itemBlack.view.button)
            return
        end
    end
end

HL.Commit(BattlePassPlanCell)
return BattlePassPlanCell

