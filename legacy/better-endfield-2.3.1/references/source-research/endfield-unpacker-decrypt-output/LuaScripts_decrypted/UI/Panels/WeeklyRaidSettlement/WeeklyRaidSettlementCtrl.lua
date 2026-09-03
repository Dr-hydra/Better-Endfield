local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeeklyRaidSettlement

WeeklyRaidSettlementCtrl = HL.Class('WeeklyRaidSettlementCtrl', uiCtrl.UICtrl)

WeeklyRaidSettlementCtrl.m_genCharCells = HL.Field(HL.Forward("UIListCache"))

WeeklyRaidSettlementCtrl.m_genValuableDepotCells = HL.Field(HL.Forward("UIListCache"))

WeeklyRaidSettlementCtrl.m_genMoneyCells = HL.Field(HL.Forward("UIListCache"))





WeeklyRaidSettlementCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WeeklyRaidSettlementCtrl.OnWeekRaidSettlement = HL.StaticMethod(HL.Any) << function(arg)
    PhaseManager:ExitPhaseFastTo(PhaseId.Level)
    UIManager:Open(PANEL_ID, arg)
end


WeeklyRaidSettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "weeklyRaidSettlement", isInMainHud = false })

    local data = unpack(arg)

    self.view.infoBtn.onClick:RemoveAllListeners()
    self.view.infoBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "week_raid_settlement")
    end)

    self.view.getMaterialsScrollView.controllerScrollEnabled = true
    self.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.getMaterialsScrollView.controllerScrollEnabled = not isFocused
    end)

    self.view.battlePassInfoBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "week_raid_battle_pass")
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.finishBtn.onClick:RemoveAllListeners()
    self.view.finishBtn.onClick:AddListener(function()
        GameInstance.dungeonManager:LeaveDungeon()
    end)

    self.view.timeNumTxt.text = UIUtils.getLeftTimeToSecond(data.TotalPlaytime / 1000)
    self.view.disorderNumTxt.text = string.format("%d/%d", data.DangerMeter, GameInstance.player.weekRaidSystem.weekRaidGame.MaxDangerMeter)
    self.view.slider.value = data.DangerMeter / GameInstance.player.weekRaidSystem.weekRaidGame.MaxDangerMeter

    self.view.integralTxt.text = string.format("%d/%d", math.min(data.BpScore, GameInstance.player.weekRaidSystem.battlePassMaxScore), GameInstance.player.weekRaidSystem.battlePassMaxScore)
    self.view.countDownText:InitCountDownText(Utils.getNextWeeklyServerRefreshTime())

    self.m_genCharCells = UIUtils.genCellCache(self.view.charCell)
    self.m_genValuableDepotCells = UIUtils.genCellCache(self.view.itemBlack)
    self.m_genMoneyCells = UIUtils.genCellCache(self.view.iconCountItem)

    self.view.rightNode:SetState(data.LootItems.Count == 0 and data.ConvertItems.Count == 0 and 'Empty' or 'Normal')

    local lookItem = {}
    local convertItem = {}
    local sum = 0
    for i = 0, data.ConvertItems.Count - 1 do
        local item = data.ConvertItems[i]
        sum = sum + item.Count
        table.insert(convertItem, item)
    end
    table.sort(convertItem, function(a, b)
        if Tables.itemTable[a.LootItem.Id].rarity ~= Tables.itemTable[b.LootItem.Id].rarity then
            return Tables.itemTable[a.LootItem.Id].rarity > Tables.itemTable[b.LootItem.Id].rarity
        end
        if Tables.itemTable[a.LootItem.Id].sortId1 ~= Tables.itemTable[b.LootItem.Id].sortId1 then
            return Tables.itemTable[a.LootItem.Id].sortId1 > Tables.itemTable[b.LootItem.Id].sortId1
        end
        if Tables.itemTable[a.LootItem.Id].sortId2 ~= Tables.itemTable[b.LootItem.Id].sortId2 then
            return Tables.itemTable[a.LootItem.Id].sortId2 > Tables.itemTable[b.LootItem.Id].sortId2
        end
        return a.LootItem.Id < b.LootItem.Id
    end)

    for i = 0, data.LootItems.Count - 1 do
        local item = data.LootItems[i]
        table.insert(lookItem, item)
    end

    table.sort(lookItem, function(a, b)
        if Tables.itemTable[a.Id].rarity ~= Tables.itemTable[b.Id].rarity then
            return Tables.itemTable[a.Id].rarity > Tables.itemTable[b.Id].rarity
        end
        if Tables.itemTable[a.Id].sortId1 ~= Tables.itemTable[b.Id].sortId1 then
            return Tables.itemTable[a.Id].sortId1 > Tables.itemTable[b.Id].sortId1
        end
        if Tables.itemTable[a.Id].sortId2 ~= Tables.itemTable[b.Id].sortId2 then
            return Tables.itemTable[a.Id].sortId2 > Tables.itemTable[b.Id].sortId2
        end
        return a.Id < b.Id
    end)

    self.view.addNumTxt.text = sum

    self.animationWrapper:PlayInAnimation(function()

        local squadSlots = GameInstance.player.squadManager.curSquad.slots
        self.m_genCharCells:Refresh(Const.BATTLE_SQUAD_MAX_CHAR_NUM, function(cell, luaIndex)
            if CSIndex(luaIndex) < squadSlots.Count then
                cell.charHeadCellLongHpBar.gameObject:SetActive(true)
                local slot = squadSlots[CSIndex(luaIndex)]
                cell.charHeadCellLongHpBar:InitCharFormationHeadCell({
                    instId = slot.charInstId,
                    templateId = slot.charId,
                    noHpBar = false,
                })
                local isAlive = slot.character ~= nil and slot.character.abilityCom.alive
                if isAlive then
                    cell.charHeadCellLongHpBar.view.charHeadBar.gameObject:SetActive(true)
                    cell.charHeadCellLongHpBar.view.addHpFill.gameObject:SetActive(false)
                    cell.charHeadCellLongHpBar.view.totalAddHpFill.gameObject:SetActive(false)
                    cell.charHeadCellLongHpBar.view.curHpFill.gameObject:SetActive(true)
                    local abilityCom = slot.character.abilityCom
                    cell.charHeadCellLongHpBar.view.curHpFill.fillAmount = abilityCom.hp / abilityCom.maxHp
                end
            else
                cell.charHeadCellLongHpBar.gameObject:SetActive(false)
            end
        end)

        self.view.rateNumTxt.text = string.format("%d%%", data.ConvertRate * 100)

        if #lookItem + #convertItem > 0 then
            AudioAdapter.PostEvent("Au_UI_Event_RewardsPopUpForWeekDungeon")
        end

        self.m_genValuableDepotCells:GraduallyRefresh(#lookItem, 0.1, function(cell, luaIndex)
            local item = lookItem[luaIndex]
            cell:InitItem({
                id = item.Id,
                count = item.Count,
            }, true)
            AudioAdapter.PostEvent("Au_UI_Event_RewardsPopUpForWeekDungeonItem")
        end)


        local moneyId = Tables.weekRaidTable[data.GameId].moneyId
        self.m_genMoneyCells:GraduallyRefresh(#convertItem, 0.1, function(cell, luaIndex)
            local item = convertItem[luaIndex]
            cell.itemBlack:InitItem({
                id = item.LootItem.Id,
            }, true)
            cell.text.text = item.Count
            local moneyItemData = Tables.itemTable[item.ConvertGold]
            cell.image:LoadSprite(UIConst.UI_SPRITE_ITEM, moneyItemData.iconId)
            AudioAdapter.PostEvent("Au_UI_Event_RewardsPopUpForWeekDungeonItem")
        end)
        self.view.iconImage:LoadSprite(UIConst.UI_SPRITE_ITEM, Tables.itemTable[moneyId].iconId)
        self.view.goldNumText.text = sum
        self.view.goldLayout:PlayInAnimation()
    end)

end






WeeklyRaidSettlementCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "weeklyRaidSettlement", isInMainHud = true })
end




HL.Commit(WeeklyRaidSettlementCtrl)
