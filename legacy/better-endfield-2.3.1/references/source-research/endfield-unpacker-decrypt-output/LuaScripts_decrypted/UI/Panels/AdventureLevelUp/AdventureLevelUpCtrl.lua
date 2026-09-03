local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureLevelUp
local MAIN_HUD_TOAST_TYPE = "AdventureLevelUp"

AdventureLevelUpCtrl = HL.Class('AdventureLevelUpCtrl', uiCtrl.UICtrl)






AdventureLevelUpCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}



AdventureLevelUpCtrl.m_genRecipeCells = HL.Field(HL.Forward("UIListCache"))

AdventureLevelUpCtrl.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

AdventureLevelUpCtrl.m_levelUpInfoQueue = HL.Field(HL.Forward("Queue"))

AdventureLevelUpCtrl.m_aniQueue = HL.Field(HL.Forward("Queue"))

AdventureLevelUpCtrl.m_aniPlayInfo = HL.Field(HL.Table)

AdventureLevelUpCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1

AdventureLevelUpCtrl.m_isWorldFreeze = HL.Field(HL.Boolean) << false

AdventureLevelUpCtrl.skipNextResume = HL.StaticField(HL.Boolean) << false

AdventureLevelUpCtrl.ResumePreLv = HL.StaticField(HL.Number) << 1

AdventureLevelUpCtrl.ResumePreExp = HL.StaticField(HL.Number) << 0

AdventureLevelUpCtrl.isLevelUpForForceSNS = HL.StaticField(HL.Boolean) << false

AdventureLevelUpCtrl.m_inputBindKeyExit = HL.Field(HL.Number) << -1




AdventureLevelUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.continueBtn.onClick:AddListener(function()
        self:_TryPlayUpgrade()
    end)

    self.m_genRecipeCells = UIUtils.genCellCache(self.view.rewardToast.recipeCell)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardToast.rewardCell)

    self.m_levelUpInfoQueue = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_aniQueue = require_ex("Common/Utils/DataStructure/Queue")()


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.rewardToast.allRewardNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

AdventureLevelUpCtrl.OnClose = HL.Override() << function(self)
    self:_SetFreezeWorld(false)
end

AdventureLevelUpCtrl.OnShowAdventureLevelUp = HL.StaticMethod(HL.Any) << function(arg)
    local preLv, preExp = unpack(arg)

    AdventureLevelUpCtrl.isLevelUpForForceSNS = preLv < GameInstance.player.adventure.adventureLevelData.lv
    if LuaSystemManager.mainHudActionQueue:HasRequestWaiting(MAIN_HUD_TOAST_TYPE) then


        return
    end
    AdventureLevelUpCtrl.ResumePreLv = preLv
    AdventureLevelUpCtrl.ResumePreExp = preExp

    AdventureLevelUpCtrl.skipNextResume = UIManager:IsShow(PANEL_ID)

    logger.info("AdventureLevelUpCtrl：OnShowAdventureLevelUp")
    LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_TOAST_TYPE, function(isResume)
        Notify(MessageConst.HIDE_ITEM_TIPS)
        if isResume then
            if AdventureLevelUpCtrl.skipNextResume then
                logger.info("AdventureLevelUpCtrl：OnShowAdventureLevelUp -> skipNextResume")
                AdventureLevelUpCtrl.skipNextResume = false
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
                return
            end
            local adventureData = GameInstance.player.adventure.adventureLevelData
            if AdventureLevelUpCtrl.ResumePreLv == adventureData.lv and AdventureLevelUpCtrl.ResumePreExp == adventureData.exp then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
                return
            end
            logger.info("AdventureLevelUpCtrl：OnShowAdventureLevelUp -> isResume")
            local self = UIManager:AutoOpen(PANEL_ID)
            self:_StartShow(AdventureLevelUpCtrl.ResumePreLv, AdventureLevelUpCtrl.ResumePreExp)
        else
            logger.info("AdventureLevelUpCtrl：OnShowAdventureLevelUp -> not isResume")
            local self = UIManager:AutoOpen(PANEL_ID)
            self:_StartShow(preLv, preExp)
        end
    end)

    if preLv < GameInstance.player.adventure.adventureLevelData.lv then
        AdventureLevelUpCtrl._ReportPlacementEvent()
    end
end

AdventureLevelUpCtrl.HaveAdventureLevelUpInQueue = HL.StaticMethod().Return(HL.Boolean) << function()
    return AdventureLevelUpCtrl.isLevelUpForForceSNS and (LuaSystemManager.mainHudActionQueue:HasRequest(MAIN_HUD_TOAST_TYPE) or UIManager:IsShow(PANEL_ID))
end

AdventureLevelUpCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self.animationWrapper:ClearTween(false)
    self:_ClearCache()
    self:Close()
end



AdventureLevelUpCtrl._UpdateData = HL.Method(HL.Number, HL.Number) << function(self, preLv, preExp)
    local adventureData = GameInstance.player.adventure.adventureLevelData
    local totalTargetLv = adventureData.lv
    local totalTargetExp = adventureData.exp
    local targetLvCfg = Tables.adventureLevelTable[totalTargetLv]
    local hasLevelUp = preLv < totalTargetLv
    logger.info("AdventureLevelUpCtrl：_UpdateData -> ResumePreLv " .. AdventureLevelUpCtrl.ResumePreLv)
    logger.info("AdventureLevelUpCtrl：_UpdateData -> ResumePreExp " .. AdventureLevelUpCtrl.ResumePreExp)

    if hasLevelUp then

        local raiseStamina = 0
        local recipeItemInfos = {}
        local rewardItemInfos = {}
        local rewardItemMap = {}
        for level = preLv + 1, totalTargetLv do
            local curLvCfg = Tables.adventureLevelTable[level]
            local rewardId = curLvCfg.rewardId
            local rewardsCfg = Tables.rewardTable[rewardId]
            raiseStamina = raiseStamina + curLvCfg.raiseMaxStamina
            for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                local itemCfg = Tables.itemTable[itemBundle.id]
                local count = itemBundle.count
                local rarity = itemCfg.rarity
                local type = itemCfg.type

                if type == GEnums.ItemType.EquipFormula then
                    local formulaInfo = {
                        id = itemBundle.id,
                        count = count,
                        rarity = rarity,
                        type = type,

                        sortId1 = itemCfg.sortId1,
                        sortId2 = itemCfg.sortId2,
                    }
                    table.insert(recipeItemInfos, formulaInfo)
                else
                    local itemInfo = rewardItemMap[itemBundle.id]
                    if itemInfo == nil then
                        itemInfo = {
                            id = itemBundle.id,
                            count = count,
                            rarity = rarity,
                            type = type,

                            sortId1 = itemCfg.sortId1,
                            sortId2 = itemCfg.sortId2,
                        }
                        rewardItemMap[itemBundle.id] = itemInfo
                    else
                        itemInfo.count = itemInfo.count + count
                    end
                end
            end
        end

        for _, info in pairs(rewardItemMap) do
            table.insert(rewardItemInfos, info)
        end

        table.sort(recipeItemInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS, true))
        table.sort(rewardItemInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS, true))

        local preLvCfg = Tables.adventureLevelTable[preLv]
        local maxExp = preLvCfg.nextLevelUpExp - preLvCfg.levelUpExp
        self.m_levelUpInfoQueue:Push({
            curLv = preLv,
            targetLv = totalTargetLv,
            curExp = preExp - preLvCfg.levelUpExp,
            targetExp = maxExp,
            maxExp = maxExp,
            totalAddExp = totalTargetExp - preExp,
            isLevelUp = true,
            isMaxLevel = false,

            raiseStamina = raiseStamina,
            recipeItemInfos = recipeItemInfos,
            rewardItemInfos = rewardItemInfos,

            curLvLevelUpExp = targetLvCfg.levelUpExp,
        })
        AdventureLevelUpCtrl.ResumePreLv = totalTargetLv
        AdventureLevelUpCtrl.ResumePreExp = targetLvCfg.levelUpExp
    else
        AdventureLevelUpCtrl.ResumePreLv = preLv
        AdventureLevelUpCtrl.ResumePreExp = preExp
    end

    local isMaxLevel = totalTargetLv == adventureData.maxLv
    local targetExp = totalTargetExp - targetLvCfg.levelUpExp
    local curExp = hasLevelUp and 0 or preExp - targetLvCfg.levelUpExp
    local curLvMaxExp = targetLvCfg.nextLevelUpExp - targetLvCfg.levelUpExp
    if isMaxLevel then
        curExp = targetLvCfg.levelUpExp
        targetExp = curExp
        curLvMaxExp = targetExp
    end
    self.m_levelUpInfoQueue:Push({
        curLv = totalTargetLv,
        targetLv = totalTargetLv,
        curExp = curExp,
        targetExp = targetExp,
        maxExp = curLvMaxExp,
        totalAddExp = hasLevelUp and -1 or totalTargetExp - preExp,
        isLevelUp = false,
        isMaxLevel = isMaxLevel,

        raiseStamina = 0,
        recipeItemInfos = nil,
        rewardItemInfos = nil,

        curLvLevelUpExp = targetLvCfg.levelUpExp,
    })
end

AdventureLevelUpCtrl._GetLevelUpReward = HL.Method(HL.String).Return(HL.Table, HL.Table) << function(self, rewardId)
    local recipeItemInfos = {}
    local rewardItemInfos = {}
    local rewardsCfg = Tables.rewardTable[rewardId]
    for _, itemBundle in pairs(rewardsCfg.itemBundles) do
        local itemCfg = Tables.itemTable[itemBundle.id]
        local count = itemBundle.count
        local rarity = itemCfg.rarity
        local type = itemCfg.type

        local info = {
            id = itemBundle.id,
            count = count,
            rarity = rarity,
            type = type,

            sortId1 = itemCfg.sortId1,
            sortId2 = itemCfg.sortId2,
        }

        if type == GEnums.ItemType.EquipFormula then
            table.insert(recipeItemInfos, info)
        else
            table.insert(rewardItemInfos, info)
        end
    end

    table.sort(recipeItemInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS, true))
    table.sort(rewardItemInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS, true))
    return recipeItemInfos, rewardItemInfos
end

AdventureLevelUpCtrl._ClearCache = HL.Method() << function(self)
    if self.m_aniPlayInfo and self.m_aniPlayInfo.updateKey > 0 then
        LuaUpdate:Remove(self.m_aniPlayInfo.updateKey)
    end

    self:_ClearOrRecoverScreen(false)
    self:_SetFreezeWorld(false)

    self.m_aniPlayInfo = nil
    self.m_levelUpInfoQueue:Clear()
    self.m_aniQueue:Clear()
end





AdventureLevelUpCtrl._StartShow = HL.Method(HL.Number, HL.Number) << function(self, preLv, preExp)
    logger.info("AdventureLevelUpCtrl：_StartShow -> preLv " .. preLv)
    logger.info("AdventureLevelUpCtrl：_StartShow -> preExp " .. preExp)
    self:_UpdateData(preLv, preExp)
    self:_TryPlayUpgrade()
end

AdventureLevelUpCtrl._TryPlayUpgrade = HL.Method() << function(self)

    if self.m_levelUpInfoQueue:Count() <= 0 then
        self:_CompleteCloseSelf(true)
        return
    end
    local lvUpInfo = self.m_levelUpInfoQueue:Pop()

    self.m_aniPlayInfo = {
        basicInfo = lvUpInfo,

        updateKey = -1,
        curLvGainExp = lvUpInfo.targetExp - lvUpInfo.curExp,
        curTickTime = 0,
        totalTime = self.view.config.PROGRESS_INCREASE_ANI_DURATION,
    }

    AdventureLevelUpCtrl.ResumePreLv = lvUpInfo.curLv
    AdventureLevelUpCtrl.ResumePreExp = lvUpInfo.curLvLevelUpExp + lvUpInfo.curExp
    if lvUpInfo.isLevelUp then
        if BEYOND_DEBUG_COMMAND then
            self.m_inputBindKeyExit = self:BindInputPlayerAction("common_cancel_no_hint", function()
                self:_CompleteCloseSelf(true)
            end)
        end
        self:_ClearOrRecoverScreen(true)
        self:_StartPlayLevelUpAni()

        GameInstance.player.guide:OnAdventureLevelUpShow()
    else
        self:_StartPlayNotLevelUpAni()
    end
end

AdventureLevelUpCtrl._StartPlayLevelUpAni = HL.Method() << function(self)
    if self.m_aniPlayInfo == nil or self.m_aniPlayInfo.basicInfo == nil then
        self:_CompleteCloseSelf()
        return
    end
    local lvUpInfo = self.m_aniPlayInfo.basicInfo
    local viewRef = self.view
    local upgradeView = self.view.upgradeToast

    viewRef.upgradeToast.gameObject:SetActiveIfNecessary(true)
    viewRef.rewardToast.gameObject:SetActiveIfNecessary(false)
    viewRef.blurWithUI.gameObject:SetActiveIfNecessary(true)

    self:ChangePanelCfg("blockKeyboardEvent", true)
    self:ChangePanelCfg("realMouseMode", Types.EPanelMouseMode.NeedShow)
    self:_SetFreezeWorld(true)

    self:_UpdateUIProgress(upgradeView.progressNode, lvUpInfo.curExp, lvUpInfo.targetExp, lvUpInfo.maxExp)

    upgradeView.curLvTxt.text = lvUpInfo.curLv
    upgradeView.targetLvTxt.text = lvUpInfo.targetLv

    local showTotalAddExp = lvUpInfo.totalAddExp > 0
    if showTotalAddExp then
        upgradeView.diffExpTxt.text = "+" .. lvUpInfo.totalAddExp
        upgradeView.diffExpNode.gameObject:SetActiveIfNecessary(true)
    else
        upgradeView.diffExpNode.gameObject:SetActiveIfNecessary(false)
    end

    AudioManager.PostEvent("Au_UI_Menu_AdventureLevelUpPanel_B_Open")
    local aniWrapper = self.animationWrapper
    aniWrapper:PlayWithTween("adventurelevelup_upgradetoast_levelup_init", function()
        AudioManager.PostEvent("Au_UI_Event_Count")
        if showTotalAddExp then
            upgradeView.aniWrapper:PlayWithTween("adventurelevelup_diffexptext_out")
        end
        if self.m_aniPlayInfo then
            self.m_aniPlayInfo.updateKey = LuaUpdate:Add("Tick", function(deltaTime)
                self:_OnUpdateLevelUpProgressAni(deltaTime)
            end)
        end
    end)
end

AdventureLevelUpCtrl._OnUpdateLevelUpProgressAni = HL.Method(HL.Number) << function(self, deltaTime)
    local playInfo = self.m_aniPlayInfo
    local lvUpInfo = playInfo.basicInfo
    playInfo.curTickTime = playInfo.curTickTime + deltaTime
    if playInfo.curTickTime >= playInfo.totalTime then
        self:_OnEndLevelUpProgressAni()
        return
    end

    local progressNode = self.view.upgradeToast.progressNode
    local timeProg = self.view.config.PROGRESSIVE_CURVE:Evaluate(playInfo.curTickTime / playInfo.totalTime)
    local curTickExp = timeProg * playInfo.curLvGainExp + lvUpInfo.curExp
    AdventureLevelUpCtrl.ResumePreExp = lume.round(lvUpInfo.curLvLevelUpExp + curTickExp)
    self:_UpdateUIProgress(progressNode, math.floor(curTickExp), lvUpInfo.targetExp, lvUpInfo.maxExp)
end

AdventureLevelUpCtrl._OnEndLevelUpProgressAni = HL.Method() << function(self)
    if self.m_aniPlayInfo == nil or self.m_aniPlayInfo.basicInfo == nil then
        self:_CompleteCloseSelf()
        return
    end
    local aniWrapper = self.animationWrapper
    local targetExp = self.m_aniPlayInfo.basicInfo.targetExp
    self:_UpdateUIProgress(self.view.upgradeToast.progressNode, targetExp, targetExp, self.m_aniPlayInfo.basicInfo.maxExp)
    LuaUpdate:Remove(self.m_aniPlayInfo.updateKey)
    self.m_aniPlayInfo.updateKey = -1
    logger.info("LevelUp Start Play Levelup")
    AudioManager.PostEvent("Au_UI_Event_AdventureLevelUp")
    aniWrapper:PlayWithTween("adventurelevelup_upgradetoast_levelup", function()
        logger.info("LevelUp Start Play Reward")
        self.view.rewardToast.progressNode.progressStaticFillOffset.fillAmount = 0
        self:_PlayReward()
    end)
end

AdventureLevelUpCtrl._StartPlayNotLevelUpAni = HL.Method() << function(self)
    if BEYOND_DEBUG_COMMAND then
        if self.m_inputBindKeyExit >= 0 then
            self:DeleteInputBinding(self.m_inputBindKeyExit)
            self.m_inputBindKeyExit = -1
        end
    end
    if self.m_aniPlayInfo == nil or self.m_aniPlayInfo.basicInfo == nil then
        self:_CompleteCloseSelf()
        return
    end
    local lvUpInfo = self.m_aniPlayInfo.basicInfo
    local viewRef = self.view
    local rewardToast = self.view.rewardToast

    viewRef.upgradeToast.gameObject:SetActiveIfNecessary(false)
    viewRef.continueBtnNode.gameObject:SetActiveIfNecessary(false)
    viewRef.blurWithUI.gameObject:SetActiveIfNecessary(false)
    rewardToast.gameObject:SetActiveIfNecessary(true)
    rewardToast.state:SetState("ShowToast")

    self:_ClearOrRecoverScreen(false)
    self:ChangePanelCfg("blockKeyboardEvent", false)
    self:ChangePanelCfg("realMouseMode", Types.EPanelMouseMode.NotNeedShow)
    self:_SetFreezeWorld(false)

    self:_UpdateUIProgress(rewardToast.progressNode, lvUpInfo.curExp, lvUpInfo.targetExp, lvUpInfo.maxExp)

    rewardToast.curLvTxt.text = lvUpInfo.curLv
    rewardToast.maxLevelText.gameObject:SetActiveIfNecessary(lvUpInfo.isMaxLevel)
    rewardToast.numProgressNode.gameObject:SetActiveIfNecessary(not lvUpInfo.isMaxLevel)

    local showTotalAddExp = lvUpInfo.totalAddExp > 0
    if showTotalAddExp then
        rewardToast.diffExpTxt.text = "+" .. lvUpInfo.totalAddExp
        rewardToast.diffExpNode.gameObject:SetActiveIfNecessary(true)
    else
        rewardToast.diffExpNode.gameObject:SetActiveIfNecessary(false)
    end

    AudioManager.PostEvent("Au_UI_Menu_AdventureLevelUpPanel_A_Open")
    local aniWrapper = self.animationWrapper
    aniWrapper:PlayWithTween("adventurelevelup_exptoast_init", function()
        AudioManager.PostEvent("Au_UI_Event_Count")
        if showTotalAddExp then
            rewardToast.aniWrapper:PlayWithTween("adventurelevelup_exptoast_diffexptext_out")
        end
        if self.m_aniPlayInfo then
            self.m_aniPlayInfo.updateKey = LuaUpdate:Add("Tick", function(deltaTime)
                self:_OnUpdateNotLevelUpProgressAni(deltaTime)
            end)
        end
    end)
end

AdventureLevelUpCtrl._OnUpdateNotLevelUpProgressAni = HL.Method(HL.Number) << function(self, deltaTime)
    local playInfo = self.m_aniPlayInfo
    local lvUpInfo = playInfo.basicInfo
    playInfo.curTickTime = playInfo.curTickTime + deltaTime
    if playInfo.curTickTime >= playInfo.totalTime then
        self:_OnEndNotLevelUpProgressAni()
        return
    end

    local progressNode = self.view.rewardToast.progressNode
    local timeProg = self.view.config.PROGRESSIVE_CURVE:Evaluate(playInfo.curTickTime / playInfo.totalTime)
    local curTickExp = timeProg * playInfo.curLvGainExp + lvUpInfo.curExp
    AdventureLevelUpCtrl.ResumePreExp = lume.round(lvUpInfo.curLvLevelUpExp + curTickExp)
    self:_UpdateUIProgress(progressNode, math.floor(curTickExp), lvUpInfo.targetExp, lvUpInfo.maxExp)
end

AdventureLevelUpCtrl._OnEndNotLevelUpProgressAni = HL.Method() << function(self)
    if self.m_aniPlayInfo == nil or self.m_aniPlayInfo.basicInfo == nil then
        self:_CompleteCloseSelf()
        return
    end
    local aniWrapper = self.animationWrapper
    local lvUpInfo = self.m_aniPlayInfo.basicInfo
    local targetExp = lvUpInfo.targetExp
    self:_UpdateUIProgress(self.view.rewardToast.progressNode, targetExp, targetExp, lvUpInfo.maxExp)
    self.view.rewardToast.maxLevelText.gameObject:SetActiveIfNecessary(lvUpInfo.isMaxLevel)
    LuaUpdate:Remove(self.m_aniPlayInfo.updateKey)
    self.m_aniPlayInfo.updateKey = -1
    aniWrapper:PlayWithTween("adventurelevelup_exptoast_out", function()
        self:_TryPlayUpgrade()
    end)
end

AdventureLevelUpCtrl._PlayReward = HL.Method() << function(self)
    if self.m_aniPlayInfo == nil or self.m_aniPlayInfo.basicInfo == nil then
        self:_CompleteCloseSelf()
        return
    end
    local levelUpInfo = self.m_aniPlayInfo.basicInfo
    local upgradeView = self.view.upgradeToast
    local rewardView = self.view.rewardToast

    upgradeView.gameObject:SetActiveIfNecessary(false)
    rewardView.gameObject:SetActiveIfNecessary(true)
    rewardView.state:SetState("ShowReward")
    rewardView.diffExpNode.gameObject:SetActiveIfNecessary(false)
    rewardView.numProgressNode.gameObject:SetActiveIfNecessary(false)
    self.view.continueBtnNode.gameObject:SetActiveIfNecessary(false)
    self.m_aniQueue:Push({ aniName = "adventurelevelup_rewardtoast_in" })

    rewardView.curLvTxt.text = levelUpInfo.targetLv

    rewardView.staminaRewardNode.gameObject:SetActiveIfNecessary(false)
    if levelUpInfo.raiseStamina > 0 then
        local curStamina = GameInstance.player.adventure:GetAdventureLevelStaminaLimit(levelUpInfo.curLv)
        local targetStamina = curStamina + levelUpInfo.raiseStamina
        rewardView.curStaminaTxt.text = curStamina
        rewardView.targetStaminaTxt.text = targetStamina
        self.m_aniQueue:Push({
            targetObj = rewardView.staminaRewardNode.gameObject,
            aniName = "adventurelevelup_rewardtoast_stamina_in",
            voice = "Au_UI_Event_AdventureReward",
        })
    end

    local recipeCount = #levelUpInfo.recipeItemInfos
    rewardView.recipeRewardNode.gameObject:SetActiveIfNecessary(false)
    if recipeCount > 0 then
        self.m_genRecipeCells:Refresh(recipeCount, function(cell, luaIndex)
            local info = levelUpInfo.recipeItemInfos[luaIndex]
            cell:InitItem(info, function()
                UIUtils.showItemSideTips(cell)
            end)
            cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
            if DeviceInfo.usingController then
                cell:SetEnableHoverTips(false)
            else
                cell:SetEnableHoverTips(true)
            end
        end)

        self.m_aniQueue:Push({
            targetObj = rewardView.recipeRewardNode.gameObject,
            aniName = "adventurelevelup_rewardtoast_recipe_in",
            voice = "Au_UI_Event_AdventureReward",
        })
    end

    local rewardCount = #levelUpInfo.rewardItemInfos
    rewardView.rewardNode.gameObject:SetActiveIfNecessary(false)
    if rewardCount > 0 then
        self.m_genRewardCells:Refresh(rewardCount, function(cell, luaIndex)
            local info = levelUpInfo.rewardItemInfos[luaIndex]
            cell:InitItem(info, function()
                UIUtils.showItemSideTips(cell)
            end)
            cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
            if DeviceInfo.usingController then
                cell:SetEnableHoverTips(false)
            else
                cell:SetEnableHoverTips(true)
            end
        end)

        self.m_aniQueue:Push({
            targetObj = rewardView.rewardNode.gameObject,
            aniName = "adventurelevelup_rewardtoast_reward_in",
            voice = "Au_UI_Event_AdventureReward",
        })
    end

    if recipeCount <= 0 and rewardCount <= 0 then
        self.view.rewardToast.focusKeyHintRoot.gameObject:SetActive(false)
    elseif recipeCount > 0 then
        self.view.rewardToast.focusKeyHintRoot.gameObject:SetActive(true)

        local firstCell = self.m_genRecipeCells:Get(1)
        self.view.rewardToast.focusKeyHintRoot:SetParent(firstCell.view.transform)
        self.view.rewardToast.focusKeyHintRoot.anchoredPosition = Vector2(-74, 0)
    else
        self.view.rewardToast.focusKeyHintRoot.gameObject:SetActive(true)

        local firstCell = self.m_genRewardCells:Get(1)
        self.view.rewardToast.focusKeyHintRoot:SetParent(firstCell.view.transform)
        self.view.rewardToast.focusKeyHintRoot.anchoredPosition = Vector2(-74, 0)
    end

    self.m_aniQueue:Push({ aniName = "adventurelevelup_rewardtoast_continueBtn_in" })
    self:_NestedPlayAniQueue()
end

AdventureLevelUpCtrl._NestedPlayAniQueue = HL.Method() << function(self)
    if self.m_aniQueue:Count() > 0 then
        local aniWrapper = self.animationWrapper
        local aniBundle = self.m_aniQueue:Pop()
        if aniBundle.targetObj then
            aniBundle.targetObj:SetActiveIfNecessary(true)
        end
        if aniBundle.voice then
            AudioManager.PostEvent(aniBundle.voice)
        end
        aniWrapper:PlayWithTween(aniBundle.aniName, function()
            self:_NestedPlayAniQueue()
        end)
    end
end

AdventureLevelUpCtrl._CompleteCloseSelf = HL.Method(HL.Opt(HL.Boolean)) << function(self, fastMode)
    if self:IsPlayingAnimationOut() then
        return
    end
    if fastMode then
        self:_ClearCache()
        self:Close()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
        return
    end
    self:PlayAnimationOutWithCallback(function()
        if self:IsPlayingAnimationOut() then
            return
        end
        self:_ClearCache()
        self:Close()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
    end)
end

AdventureLevelUpCtrl._UpdateUIProgress = HL.Method(HL.Any, HL.Number, HL.Number, HL.Number)
    << function(self, viewRef, curValue, targetGrowthValue, totalValue)

    local fillOffset = self.view.config.PROGRESS_STATIC_FILLAMOUNT_OFFSET
    local realFillPercent = 1 - 2 * fillOffset
    local fillAmount = curValue / totalValue
    local realFill = fillAmount * realFillPercent + fillOffset

    viewRef.curExpTxt.text = curValue
    viewRef.targetExpTxt.text = totalValue
    viewRef.progressFillOffset.fillAmount = realFill

    if curValue < totalValue then
        viewRef.progressStaticFillOffset.gameObject:SetActiveIfNecessary(true)


        local pStaticRectTransform = viewRef.progressStaticRectTransform

        local pRectTransform = viewRef.progressiveRectTransform

        local posXOffset = self.view.config.PROGRESS_STATIC_POS_X_OFFSET
        local staticFillAmount = (targetGrowthValue - curValue) / totalValue
        local realStaticFill = staticFillAmount * realFillPercent + fillOffset

        viewRef.progressStaticFillOffset.fillAmount = realStaticFill
        local posX = pRectTransform.rect.width * realFill + posXOffset
        pStaticRectTransform.anchoredPosition = Vector2(posX, 0)
    else
        viewRef.progressStaticFillOffset.gameObject:SetActiveIfNecessary(false)
    end
end

AdventureLevelUpCtrl._ClearOrRecoverScreen = HL.Method(HL.Boolean) << function(self, isClear)
    if isClear then
        if self.m_clearScreenKey <= 0 then
            self.m_clearScreenKey = UIManager:ClearScreen({ PanelId.AdventureLevelUp })
        end
    else
        if self.m_clearScreenKey > 0 then
            UIManager:RecoverScreen(self.m_clearScreenKey)
            self.m_clearScreenKey = -1
        end
    end
end


AdventureLevelUpCtrl._SetFreezeWorld = HL.Method(HL.Boolean) << function(self, isFreeze)
    if self.m_isWorldFreeze == isFreeze then
        return
    end

    self.m_isWorldFreeze = isFreeze
    if isFreeze then
        Notify(MessageConst.OPEN_FREEZE_WORLD_PANEL, "AdventureLevelUpCtrl")
    else
        Notify(MessageConst.CLOSE_FREEZE_WORLD_PANEL, "AdventureLevelUpCtrl")
    end
end




AdventureLevelUpCtrl._ReportPlacementEvent = HL.StaticMethod() << function()
    local curLevel = GameInstance.player.adventure.adventureLevelData.lv
    if curLevel == 5 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level5)
    elseif curLevel == 10 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level10)
    elseif curLevel == 18 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level18)
    elseif curLevel == 2 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level2)
    elseif curLevel == 3 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level3)
    elseif curLevel == 15 then
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.Level15)
    end
end


HL.Commit(AdventureLevelUpCtrl)
