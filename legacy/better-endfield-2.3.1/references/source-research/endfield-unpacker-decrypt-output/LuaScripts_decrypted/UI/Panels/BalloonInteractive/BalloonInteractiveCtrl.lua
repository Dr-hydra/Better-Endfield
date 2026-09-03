
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BalloonInteractive
local PHASE_ID = PhaseId.BalloonInteractive
BalloonInteractiveCtrl = HL.Class('BalloonInteractiveCtrl', uiCtrl.UICtrl)

BalloonInteractiveCtrl.m_mainMiniGameId = HL.Field(HL.String) << ""

BalloonInteractiveCtrl.m_miniGameData = HL.Field(HL.Userdata)

BalloonInteractiveCtrl.m_miniGameSystem = HL.Field(HL.Userdata)

BalloonInteractiveCtrl.m_isOpenGame = HL.Field(HL.Boolean) << false

BalloonInteractiveCtrl.m_unEquipItem = HL.Field(HL.Boolean) << false

BalloonInteractiveCtrl.m_donNotHaveItem = HL.Field(HL.Boolean) << false

BalloonInteractiveCtrl.m_recipeNotUnlocked = HL.Field(HL.Boolean) << false

BalloonInteractiveCtrl.m_jumpCraftId = HL.Field(HL.String) << ""






BalloonInteractiveCtrl.s_messages = HL.StaticField(HL.Table) << {

}


BalloonInteractiveCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.playBtn.onClick:AddListener(function()

        local isOpen, phase = PhaseManager:IsOpen(PHASE_ID)
        if not isOpen or not phase or phase.state ~= PhaseConst.EPhaseState.Activated then
            return
        end
        if self.m_unEquipItem then
            PhaseManager:OpenPhase(PhaseId.Inventory)
            return
        end
        if self.m_donNotHaveItem then
            if not self.m_recipeNotUnlocked then
                PhaseManager:OpenPhase(PhaseId.ManualCraft, {jumpId = self.m_jumpCraftId})
            end
            return
        end
        self.m_isOpenGame = true
        PhaseManager:ExitPhaseFast(PHASE_ID)
        PhaseManager:OpenPhase(PhaseId.Balloon)
    end)

    self.view.wikiButton.onClick:AddListener(function()
        Notify(MessageConst.SHOW_WIKI_ENTRY, self.view.config.WIKI_ID)
    end)
    self.m_miniGameSystem = GameInstance.player.miniGame
    self.m_miniGameData = self.m_miniGameSystem.balloonGame

    if arg and arg.isOpenGame ~= nil then
        self.m_isOpenGame = arg.isOpenGame
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

BalloonInteractiveCtrl.InitData = HL.Method() << function(self)
    if not self.m_miniGameData then
        PhaseManager:ExitPhaseFast(PhaseId.BalloonInteractive)
        return
    end
    local rawData = self.m_miniGameData.firstMiniGameData.rawData
    if rawData then
        self.view.descText.text = Language[self.m_miniGameData.curBalloonIntData.panelDesc]
        self.view.machineName.text = Language[rawData.levelTextId]
    end
    local needItemIds = rawData.propLevelItem
    local isEquip = false

    local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
    self.m_unEquipItem = false
    for i = CSIndex(1), CSIndex(needItemIds.Count) do
        local itemId = needItemIds[i]
        local succ, itemData = Tables.itemTable:TryGetValue(itemId)
        if not succ then
            logger.critical("BalloonInteractive：气球关卡所需要的item不存在item id:", itemId)
            return
        end
        for k = 0, itemBag.coloredSlotNum - 1 do
            local item = itemBag.slots[k]
            if Utils.getItemCount(itemId) > 0 then
                self.m_unEquipItem = true
            end
            if itemId == item.id  then
                isEquip = true
                break
            end
        end
    end
    local needItemId
    if needItemIds and needItemIds.Count > 0 then
        needItemId = needItemIds[CSIndex(1)]
    else

        needItemId = "item_device_balloon_recycle_1"
        isEquip = true
    end
    self.view.itemBlack:InitItem({ id = needItemId }, true)
    local propLevel = rawData.propLevel or 1
    local tipsKey = "ui_msc_balloon_tips_item_" .. tostring(propLevel)
    self.view.itemInfotxt.text = Language[tipsKey]
    if isEquip then
        self.m_unEquipItem = false
    end
    self.m_donNotHaveItem = false
    self.m_recipeNotUnlocked = false
    self.m_jumpCraftId = ""
    if isEquip then
        self.view.content:SetState("CanPlay")
        InputManagerInst:SetBindingText(self.view.playBtn.onClick.bindingId, Language.ui_msc_balloon_button_startgame)
    elseif self.m_unEquipItem then
        self.view.content:SetState("UnEquip")
        InputManagerInst:SetBindingText(self.view.playBtn.onClick.bindingId, Language.ui_msc_balloon_button_goequipment)
    else
        self.m_donNotHaveItem = true
        self.view.content:SetState("NoHaving")
        InputManagerInst:SetBindingText(self.view.playBtn.onClick.bindingId, Language.ui_msc_balloon_button_gomake)
        local hasCraft, craftIds = Tables.factoryItemAsManualCraftOutcomeTable:TryGetValue(needItemId)
        if hasCraft then
            local unlocked = false
            for _, craftId in pairs(craftIds.list) do
                if GameInstance.player.facManualCraft:IsCraftUnlocked(craftId) then
                    unlocked = true
                    self.m_jumpCraftId = craftId
                    break
                end
            end
            self.m_recipeNotUnlocked = not unlocked
        end
    end

    if self.m_recipeNotUnlocked then
        self.view.bottomNode:SetState("Locked")
    else
        self.view.bottomNode:SetState("Play")
    end
end

BalloonInteractiveCtrl.OnShow = HL.Override() << function(self)
    self:InitData()
end
BalloonInteractiveCtrl.OnHide = HL.Override() << function(self)

end
BalloonInteractiveCtrl.OnClose = HL.Override() << function(self)
    if InputManagerInst.inChangingInputDevice then
        return
    end
    if not self.m_isOpenGame then
        GameInstance.player.miniGame:FinishBalloonGame()
    end
end

BalloonInteractiveCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        isOpenGame = self.m_isOpenGame,
    }
end

BalloonInteractiveCtrl.ShowBalloonInteractive = HL.StaticMethod(HL.Opt(HL.Any)) << function(args)
    PhaseManager:OpenPhase(PHASE_ID, args)
end

HL.Commit(BalloonInteractiveCtrl)
