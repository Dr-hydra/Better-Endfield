local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local Style = {
    DefaultMode = "DefaultMode",
    HunterMode = "HunterMode",
    HardMode = "HardMode",

    Lock = "Lock",
    Unlock = "Unlock",

    DefaultPassState = "DefaultPassState",
    Pass = "Pass",
    PerfectPass = "PerfectPass",
}

DungeonCustomInfo = HL.Class('DungeonCustomInfo', UIWidgetBase)

DungeonCustomInfo.m_arg = HL.Field(HL.Table)

DungeonCustomInfo.m_dungeonGoalCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonCustomInfo.m_charAttributeCellCache = HL.Field(HL.Forward("UIListCache"))


DungeonCustomInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_dungeonGoalCellCache = UIUtils.genCellCache(self.view.dungeonGoalCell)
    self.m_charAttributeCellCache = UIUtils.genCellCache(self.view.attriNode)

    self.view.tipsBtn.onClick:AddListener(function()
        if self.m_arg and self.m_arg.onTipsBtnClick then
            self.m_arg.onTipsBtnClick()
        end
    end)

    self.view.btnEnemyDetails.onClick:AddListener(function()
        if self.m_arg and self.m_arg.onEnemyDetailsClick then
            self.m_arg.onEnemyDetailsClick()
        end
    end)

    self.view.btnRewardDetails.onClick:AddListener(function()
        if self.m_arg and self.m_arg.onRewardDetailsClick then
            self.m_arg.onRewardDetailsClick()
        end
    end)

    self.view.btnDungeonEntry.onClick:AddListener(function()
        if self.m_arg and self.m_arg.onEntryClick then
            self.m_arg.onEntryClick()
        end
    end)

    self.view.btnUnlockMultiCondition.onClick:AddListener(function()
        if self.m_arg and self.m_arg.onUnlockConditionClick then
            self.m_arg.onUnlockConditionClick()
        end
    end)
end

DungeonCustomInfo.InitDungeonCustomInfo = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()
    self.m_arg = arg

    if arg.walletBarItemIds and #arg.walletBarItemIds > 0 then
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder(arg.walletBarItemIds)
    end

    self:_RefreshUI(arg)
end

DungeonCustomInfo._RefreshUI = HL.Method(HL.Table) << function(self, arg)
    local isUnlock = arg.isUnlock ~= false


    local styleMode = arg.styleMode or Style.DefaultMode
    self.view.rightNode:SetState(styleMode)


    self.view.rightNode:SetState(isUnlock and Style.Unlock or Style.Lock)


    self.view.dungeonTitleTxt.text = arg.title or ""


    local showTimeInfo = arg.timeRecordText ~= nil
    self.view.timeRecordNode.gameObject:SetActive(showTimeInfo)
    if showTimeInfo then
        self.view.timeTxt.text = arg.timeRecordText
    end


    local hasLocation = not string.isEmpty(arg.locationText)
    if hasLocation then
        self.view.locationTxt.text = arg.locationText
    end
    self.view.locationNode.gameObject:SetActive(hasLocation)


    local hasRecommendLv = arg.recommendLv and arg.recommendLv > 0
    if hasRecommendLv then
        self.view.recommendLvTxt.text = string.format("LV.%d", arg.recommendLv)
    end
    self.view.recommendLv.gameObject:SetActive(hasRecommendLv == true)


    self.view.dungeonDescTxt:SetAndResolveTextStyle(arg.desc or "")


    local hasFeature = not string.isEmpty(arg.featureDesc)
    if hasFeature then
        self.view.featureTxt:SetAndResolveTextStyle(arg.featureDesc)
    end
    self.view.feature.gameObject:SetActive(hasFeature)


    local hasChar = not string.isEmpty(arg.charTemplateId)
    if hasChar then
        self.view.charImg:LoadSprite(UIConst.UI_SPRITE_HOR_CHAR_HEAD, arg.charTemplateId)
        local tagNames = arg.charBattleTagNames or {}
        self.m_charAttributeCellCache:Refresh(#tagNames, function(cell, luaIndex)
            cell.attriTxt.text = tagNames[luaIndex]
        end)
    end
    self.view.charInfo.gameObject:SetActive(hasChar)


    local isTrain = arg.isTrain or false
    local goalTexts = arg.goalTexts or {}
    local goalComplete = arg.goalComplete or false
    if #goalTexts > 0 then
        self.m_dungeonGoalCellCache:Refresh(#goalTexts, function(cell, luaIndex)
            cell.goalTxt:SetAndResolveTextStyle(goalTexts[luaIndex])
            cell.normalIcon.gameObject:SetActive(not goalComplete)
            cell.finishedIcon.gameObject:SetActive(goalComplete)
        end)
    end
    self.view.dungeonGoalInfoNode.gameObject:SetActive(#goalTexts > 0)
    self.view.awardsTxt.gameObject:SetActive(not isTrain)
    self.view.trainTxt.gameObject:SetActive(isTrain)


    local hasChest = arg.chestMax and arg.chestMax > 0
    if hasChest then
        self.view.rewardChestNode.chestTxt.text = string.format("%d/%d", arg.chestGained or 0, arg.chestMax)
    end
    self.view.rewardChestNode.gameObject:SetActive(hasChest == true)


    local passState = arg.passState or Style.DefaultPassState
    self.view.rightNode:SetState(passState)


    self.view.enemyNode.gameObject:SetActive(arg.showEnemy or false)


    self.view.rewardNode.gameObject:SetActive(false)
    self.view.selectNode.gameObject:SetActive(false)
    self.view.materialDecoImage.gameObject:SetActive(false)


    if isUnlock then
        local showCostStamina = arg.costStamina and arg.costStamina > 0
        self.view.staminaNode.gameObject:SetActive(showCostStamina or false)
        if showCostStamina then
            UIUtils.updateStaminaNode(self.view.staminaNode, {
                costStamina = arg.costStamina,
                descStamina = Language["ui_dungeon_details_ap_reuse"],
            })
        end
        self.view.hunterModeLockNode.gameObject:SetActive(arg.showHunterModeLock or false)

        self.view.btnDungeonEntry.text = arg.entryButtonText or ""
        self.view.btnDungeonEntry.gameObject:SetActive(arg.showEntryButton ~= false)

        self.view.lockedNode.gameObject:SetActive(false)
        self.view.lockedSpNode.gameObject:SetActive(false)
    else
        local lockUseSpStyle = arg.lockUseSpStyle or false
        self.view.lockedNode.gameObject:SetActive(not lockUseSpStyle)
        self.view.lockedSpNode.gameObject:SetActive(lockUseSpStyle)
        if lockUseSpStyle then
            self.view.lockedText.text = arg.lockText or ""
        else
            self.view.lockedTxt.text = arg.lockText or ""
        end
    end
    self.view.unlockedNode.gameObject:SetActive(isUnlock)
    self.view.staminaLaveNode.gameObject:SetActive(false)


    if arg.haveHardMode then
        self.view.hardTogStateController:SetState(arg.isRaid and "On" or "Off")
    else
        self.view.hardModeNode.gameObject:SetActive(false)
    end


    self.view.walletBarPlaceholder.gameObject:SetActive(arg.showWalletBar or false)


    self.view.animation:ClearTween()
    self.view.animation:PlayInAnimation()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.container)
end

HL.Commit(DungeonCustomInfo)
return DungeonCustomInfo

