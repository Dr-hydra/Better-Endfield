local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local CULTIVATION_SUB_PREFAB_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Spaceship/Widgets/SSGrowCabinBoxCultivation.prefab"
local CULTURE_SUB_PREFAB_PATH = "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Spaceship/Widgets/SSGrowCabinBoxCulture.prefab"

SSGrowCabinBoxInfo = HL.Class('SSGrowCabinBoxInfo', UIWidgetBase)

SSGrowCabinBoxInfo.m_roomId = HL.Field(HL.String) << ""

SSGrowCabinBoxInfo.m_boxId = HL.Field(HL.Number) << -1

SSGrowCabinBoxInfo.m_ctrl = HL.Field(HL.Userdata) << nil

SSGrowCabinBoxInfo.m_onBtnAddClick = HL.Field(HL.Function)

SSGrowCabinBoxInfo.m_inputIds = HL.Field(HL.Table)

SSGrowCabinBoxInfo.m_cultivationNode = HL.Field(HL.Any)

SSGrowCabinBoxInfo.m_cultureNode = HL.Field(HL.Any)

SSGrowCabinBoxInfo.m_cultivationListenersInited = HL.Field(HL.Boolean) << false

SSGrowCabinBoxInfo.m_cultureListenersInited = HL.Field(HL.Boolean) << false



SSGrowCabinBoxInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btnAdd.onClick:AddListener(function()
        if self.m_onBtnAddClick then
            self.m_onBtnAddClick(self.m_boxId, false)
        end
    end)

    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        self:_SetKeyHintState(select)
        if select then
            self:_CreateInputBindings()
        else
            self:_ClearInputBindings()
        end
    end)
end

SSGrowCabinBoxInfo._SetKeyHintState = HL.Method(HL.Boolean) << function(self, select)
    if self.m_cultivationNode then
        self.m_cultivationNode.keyHintRemove.gameObject:SetActive(select)
        self.m_cultivationNode.keyHintCabinCollect.gameObject:SetActive(select)
        self.m_cultivationNode.keyHintItemDetail.gameObject:SetActive(select)
    end
    if self.m_cultureNode then
        self.m_cultureNode.keyHintCulture.gameObject:SetActive(select)
        self.m_cultureNode.keyHintCultureAgain.gameObject:SetActive(select)
    end
    self.view.keyHintBtnAdd.gameObject:SetActive(select)
end

SSGrowCabinBoxInfo._EnsureCultivationNode = HL.Method().Return(HL.Any) << function(self)
    if self.m_cultivationNode then
        return self.m_cultivationNode
    end
    local prefab = self.m_ctrl:LoadGameObject(CULTIVATION_SUB_PREFAB_PATH)
    local go = CSUtils.CreateObject(prefab, self.view.gameObject)
    go.name = "CultureCultivation"
    go.transform.localScale = Vector3.one
    self.m_cultivationNode = Utils.wrapLuaNode(go)
    self:_InitCultivationListeners()
    return self.m_cultivationNode
end

SSGrowCabinBoxInfo._EnsureCultureNode = HL.Method().Return(HL.Any) << function(self)
    if self.m_cultureNode then
        return self.m_cultureNode
    end
    local prefab = self.m_ctrl:LoadGameObject(CULTURE_SUB_PREFAB_PATH)
    local go = CSUtils.CreateObject(prefab, self.view.gameObject)
    go.name = "Culture"
    go.transform.localScale = Vector3.one
    self.m_cultureNode = Utils.wrapLuaNode(go)
    self:_InitCultureListeners()
    return self.m_cultureNode
end

SSGrowCabinBoxInfo._InitCultivationListeners = HL.Method() << function(self)
    if self.m_cultivationListenersInited then
        return
    end
    self.m_cultivationListenersInited = true
    local cultureCultivation = self.m_cultivationNode
    cultureCultivation.receiveBtn.onClick:AddListener(function()
        GameInstance.player.spaceship:GrowCabinHarvest(self.m_roomId, self.m_boxId)
    end)
    cultureCultivation.cancelBtn.onClick:AddListener(function()
        self:_OnCancelBtnClick()
    end)
end

SSGrowCabinBoxInfo._InitCultureListeners = HL.Method() << function(self)
    if self.m_cultureListenersInited then
        return
    end
    self.m_cultureListenersInited = true
    local culture = self.m_cultureNode
    culture.button.onClick:AddListener(function()
        if self.m_onBtnAddClick then
            self.m_onBtnAddClick(self.m_boxId, true)
        end
    end)
    culture.bubble.onClick:AddListener(function()
        self:_OnBubbleBtnClick()
    end)
    culture.cantBubble.onClick:AddListener(function()
        self:_OnCantBubbleBtnClick()
    end)
end

SSGrowCabinBoxInfo._ShowPopUp = HL.Method(HL.Table) << function(self, args)
    Notify(MessageConst.SHOW_POP_UP, {
        content = args.content,
        subContent = args.subContent,
        items = args.items,
        onConfirm = args.onConfirm,
        onCancel = args.onCancel,
    })
end

SSGrowCabinBoxInfo._OnCancelBtnClick = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local boxes = spaceship:GetGrowCabinBoxes(self.m_roomId)

    local succ, box = boxes:TryGetValue(self.m_boxId)
    if succ then
        local formula = Tables.spaceshipGrowCabinFormulaTable[box.scdMsg.RecipeId]
        local itemData = Tables.itemTable[formula.outcomeItemId]
        local args = {
            content = string.format(Language.LUA_SPACESHIP_ROOM_GROW_CABIN_BOX_CANCEL_SOW_CONFIRM_FORMAT,
                                    itemData.name),
            subContent = Language.LUA_SPACESHIP_ROOM_GROW_CABIN_BOX_CANCEL_SOW_CONFIRM_SUB_DESC,
            onConfirm = function()
                GameInstance.player.spaceship:GrowCabinCancel(self.m_roomId, self.m_boxId)
            end,
        }
        self:_ShowPopUp(args)
    end
end

SSGrowCabinBoxInfo._OnBubbleBtnClick = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local boxes = spaceship:GetGrowCabinBoxes(self.m_roomId)

    local succ, box = boxes:TryGetValue(self.m_boxId)
    if succ then
        local formula = Tables.spaceshipGrowCabinFormulaTable[box.scdMsg.PreviewRecipeId]
        local seedItemData = Tables.itemTable[formula.seedItemId]
        local outcomeItemData = Tables.itemTable[formula.outcomeItemId]
        local args = {
            content = string.format(Language.LUA_SPACESHIP_ROOM_GROW_CABIN_CONTINUE_SOW_CONFIRM_FORMAT,
                                    outcomeItemData.name),
            subContent = string.format(Language.LUA_SPACESHIP_ROOM_GROW_CABIN_CONTINUE_SOW_CONFIRM_SUB_FORMAT,
                                       seedItemData.name, formula.seedItemCount),
            items = { { id = seedItemData.id, needCount = formula.seedItemCount, count = Utils.getItemCount(seedItemData.id) } },
            onConfirm = function()
                if not string.isEmpty(box.previewRecipeId) then
                    GameInstance.player.spaceship:GrowCabinSow(self.m_roomId, self.m_boxId, box.previewRecipeId)
                end
            end,
        }
        GameInstance.player.spaceship:GrowCabinClearPreviewRecipe(self.m_roomId, self.m_boxId)
        self:_ShowPopUp(args)
    end
end

SSGrowCabinBoxInfo._OnCantBubbleBtnClick = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local boxes = spaceship:GetGrowCabinBoxes(self.m_roomId)

    local succ, box = boxes:TryGetValue(self.m_boxId)
    if succ then
        local formulaId = box.scdMsg.PreviewRecipeId
        local formula = Tables.spaceshipGrowCabinFormulaTable[formulaId]
        local seedItemData = Tables.itemTable[formula.seedItemId]
        local args = {
            content = string.format(Language.LUA_SPACESHIP_ROOM_GROW_CABIN_CONTINUE_SOW_TO_BREED_CONFIRM_FORMAT,
                                    seedItemData.name),
            items = { { id = seedItemData.id, needCount = formula.seedItemCount, count = Utils.getItemCount(seedItemData.id) } },
            onConfirm = function()
                self.m_ctrl:JumpToBreed(formulaId)
            end,
        }
        GameInstance.player.spaceship:GrowCabinClearPreviewRecipe(self.m_roomId, self.m_boxId)
        self:_ShowPopUp(args)
    end
end

SSGrowCabinBoxInfo.InitSSGrowCabinBoxInfo = HL.Method(HL.Userdata, HL.String, HL.Number, HL.Any, HL.Function)
        << function(self, ctrl, roomId, boxId, lineCell, onBtnAddClick)
    self:_FirstTimeInit()

    local rt = self.view.rectTransform
    rt.anchorMin = Vector2.zero
    rt.anchorMax = Vector2.one
    rt.offsetMin = Vector2.zero
    rt.offsetMax = Vector2.zero
    rt.localScale = Vector3.one

    self.m_roomId = roomId
    self.m_boxId = boxId
    self.m_onBtnAddClick = onBtnAddClick
    self.m_ctrl = ctrl

    self.view.locked.gameObject:SetActiveIfNecessary(false)
    self.view.btnAdd.gameObject:SetActiveIfNecessary(false)
    self:Refresh(lineCell)
end

SSGrowCabinBoxInfo.Refresh = HL.Method(HL.Any) << function(self, lineCell)
    local spaceship = GameInstance.player.spaceship
    local boxes = spaceship:GetGrowCabinBoxes(self.m_roomId)
    local boxProducing = spaceship:IsGrowCabinBoxStateProducing(self.m_roomId, self.m_boxId)

    lineCell.gameObject:SetActiveIfNecessary(boxProducing)


    if not self.m_ctrl.m_trainAudioPlayed and boxProducing then
        AudioManager.PostEvent("Au_UI_Event_GrowCabin_Train")
        self.m_ctrl.m_trainAudioPlayed = true
    end

    local succ, box = boxes:TryGetValue(self.m_boxId)
    if succ then

        local hasFormula = box.hasFormula
        local sustainable = box.sustainable
        self.view.btnAdd.gameObject:SetActiveIfNecessary(not hasFormula and not sustainable)

        if hasFormula then
            local cultureCultivation = self:_EnsureCultivationNode()
            cultureCultivation.gameObject:SetActiveIfNecessary(true)
            local canReceive = box.scdMsg.IsReady
            cultureCultivation.pauseNode.gameObject:SetActiveIfNecessary(not boxProducing)
            cultureCultivation.schedule.gameObject:SetActiveIfNecessary(not canReceive)
            cultureCultivation.timeNode.gameObject:SetActiveIfNecessary(not canReceive)
            cultureCultivation.cancelBtn.gameObject:SetActiveIfNecessary(not canReceive)
            cultureCultivation.canBeClaimed.gameObject:SetActiveIfNecessary(canReceive)
            cultureCultivation.deco.gameObject:SetActiveIfNecessary(canReceive)
            cultureCultivation.bgFrame.gameObject:SetActiveIfNecessary(canReceive)
            cultureCultivation.bgFrameGlow.gameObject:SetActiveIfNecessary(canReceive)

            local formula = Tables.spaceshipGrowCabinFormulaTable[box.scdMsg.RecipeId]
            local itemId = formula.outcomeItemId
            cultureCultivation.ccItem:InitItem({ id = itemId }, true)

            local haveCharSkill = spaceship:IsRoomAttrHaveCharSkill(self.m_roomId, formula.roomAttrType, false)
            cultureCultivation.accNode.gameObject:SetActiveIfNecessary(haveCharSkill)

            self:RefreshTimeSchedule()
        else
            if self.m_cultivationNode then
                self.m_cultivationNode.gameObject:SetActiveIfNecessary(false)
            end
        end

        if sustainable then
            local culture = self:_EnsureCultureNode()
            culture.gameObject:SetActiveIfNecessary(true)
            local formula = Tables.spaceshipGrowCabinFormulaTable[box.scdMsg.PreviewRecipeId]
            local seedItemData = Tables.itemTable[formula.seedItemId]
            local outcomeItemData = Tables.itemTable[formula.outcomeItemId]
            local canBubble = Utils.getItemCount(formula.seedItemId) >= formula.seedItemCount

            culture.cItem:InitItem({id = outcomeItemData.id, count = formula.outcomeItemCount})
            culture.bubble.gameObject:SetActiveIfNecessary(canBubble)
            culture.cantBubble.gameObject:SetActiveIfNecessary(not canBubble)
            culture.canIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, seedItemData.iconId)
            culture.cantIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, seedItemData.iconId)
        else
            if self.m_cultureNode then
                self.m_cultureNode.gameObject:SetActiveIfNecessary(false)
            end
        end
    else

        if self.m_cultivationNode then
            self.m_cultivationNode.gameObject:SetActiveIfNecessary(false)
        end
        if self.m_cultureNode then
            self.m_cultureNode.gameObject:SetActiveIfNecessary(false)
        end
        local unlockLevel = Tables.spaceshipGrowCabinBoxIdToUnlockLevelTable[self.m_boxId]
        self.view.unlockTxt.text = string.format(Language.LUA_SPACESHIP_ROOM_GROW_CABIN_BOX_UNLOCK_CONDITION_FORMAT,
                                                 unlockLevel)
    end
    local isHelped = GameInstance.player.spaceship:IsGrowCabinBoxHelped(self.m_roomId, self.m_boxId)
    self.view.friendBoostTips.gameObject:SetActiveIfNecessary(isHelped)
    self.view.locked.gameObject:SetActiveIfNecessary(not succ)
    self.view.unlock.gameObject:SetActiveIfNecessary(succ)

    self:_ClearInputBindings()
end

SSGrowCabinBoxInfo._CreateInputBindings = HL.Method() << function(self)
    self:_ClearInputBindings()
    local culture = self.m_cultureNode
    local cultureCultivation = self.m_cultivationNode
    if (culture and culture.button.gameObject.activeInHierarchy) or
        self.view.btnAdd.gameObject.activeInHierarchy then
        local id = InputManagerInst:CreateBindingByActionId("ss_cabin_cultivate", function()
            if self.m_onBtnAddClick then
                if culture and culture.button.gameObject.activeInHierarchy then
                    self.m_onBtnAddClick(self.m_boxId, true)
                else
                    self.m_onBtnAddClick(self.m_boxId, false)
                end
            end
            self.m_ctrl:_DeleteDetailNaviBinding()
        end, self.view.inputBindingGroupMonoTarget.groupId)
        table.insert(self.m_inputIds, id)
    end
    if cultureCultivation and cultureCultivation.receiveBtn.gameObject.activeInHierarchy then
        local id = InputManagerInst:CreateBindingByActionId("ss_cabin_collect", function()
            GameInstance.player.spaceship:GrowCabinHarvest(self.m_roomId, self.m_boxId)
        end, self.view.inputBindingGroupMonoTarget.groupId)
        table.insert(self.m_inputIds, id)
    end

    if cultureCultivation and cultureCultivation.gameObject.activeInHierarchy then
        local id = InputManagerInst:CreateBindingByActionId("ss_item_detail", function()
            self.m_cultivationNode.ccItem:ShowTips()
        end, self.view.inputBindingGroupMonoTarget.groupId)
        table.insert(self.m_inputIds, id)
    end

    if cultureCultivation and cultureCultivation.cancelBtn.gameObject.activeInHierarchy then
        local id = InputManagerInst:CreateBindingByActionId("ss_cabin_item_remove", function()
            self:_OnCancelBtnClick()
        end, self.view.inputBindingGroupMonoTarget.groupId)
        table.insert(self.m_inputIds, id)
    end
    if culture and (culture.bubble.gameObject.activeInHierarchy or
        culture.cantBubble.gameObject.activeInHierarchy) then
        local id = InputManagerInst:CreateBindingByActionId("ss_cabin_cultivate_again", function()
            if culture.bubble.gameObject.activeInHierarchy then
                self:_OnBubbleBtnClick()
                return
            end
            if culture.cantBubble.gameObject.activeInHierarchy then
                self:_OnCantBubbleBtnClick()
                return
            end
        end, self.view.inputBindingGroupMonoTarget.groupId)
        table.insert(self.m_inputIds, id)
    end
end

SSGrowCabinBoxInfo._ClearInputBindings = HL.Method() << function(self)
    self.m_inputIds = self.m_inputIds or {}
    for _, id in ipairs(self.m_inputIds) do
        InputManagerInst:DeleteBinding(id)
    end
    self.m_inputIds = {}
end

SSGrowCabinBoxInfo.RefreshTimeSchedule = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local boxes = spaceship:GetGrowCabinBoxes(self.m_roomId)
    local succ, box = boxes:TryGetValue(self.m_boxId)

    if not succ or string.isEmpty(box.scdMsg.RecipeId) or box.scdMsg.IsReady then
        return
    end
    if not self.m_cultivationNode then
        return
    end
    local boxProducing = spaceship:IsGrowCabinBoxStateProducing(self.m_roomId, self.m_boxId)

    local cultureCultivation = self.m_cultivationNode
    local diffTime = boxProducing and DateTimeUtils.GetCurrentTimestampBySeconds() - box.lastSyncTime or 0
    local formula = Tables.spaceshipGrowCabinFormulaTable[box.scdMsg.RecipeId]
    local produceRate = spaceship:GetRoomProduceRate(self.m_roomId, formula.roomAttrType)
    local totalProgress = formula.totalProgress
    local curProgress = box.scdMsg.Progress + produceRate * diffTime

    cultureCultivation.schedule.fillAmount = curProgress / totalProgress
    cultureCultivation.timeTxt.text = UIUtils.getLeftTimeToSecond(math.max(totalProgress - curProgress,
                                                                               0) / produceRate)


    cultureCultivation.pauseNode.gameObject:SetActive(spaceship:IsGrowCabinStateShutDown(self.m_roomId))
end

HL.Commit(SSGrowCabinBoxInfo)
return SSGrowCabinBoxInfo
