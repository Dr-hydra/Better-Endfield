
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RewardsPopUpForBlackBox

local RewardSourceType = CS.Beyond.GEnums.RewardSourceType
local SystemActionConflictId = "BlackboxObtainReward"
RewardsPopUpForBlackBoxCtrl = HL.Class('RewardsPopUpForBlackBoxCtrl', uiCtrl.UICtrl)

RewardsPopUpForBlackBoxCtrl.OnShowBlackboxResult = HL.StaticMethod(HL.Any) << function(args)
    UIManager:AutoOpen(PANEL_ID, args)

    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(SystemActionConflictId)
end







RewardsPopUpForBlackBoxCtrl.s_messages = HL.StaticField(HL.Table) << {

}

RewardsPopUpForBlackBoxCtrl.m_isAnimationIn = HL.Field(HL.Boolean) << false

RewardsPopUpForBlackBoxCtrl.m_leaveTick = HL.Field(HL.Number) << -1

RewardsPopUpForBlackBoxCtrl.m_dungeonId = HL.Field(HL.String) << ""

RewardsPopUpForBlackBoxCtrl.m_leaveTimestamp = HL.Field(HL.Number) << -1

RewardsPopUpForBlackBoxCtrl.m_isFail = HL.Field(HL.Boolean) << false

RewardsPopUpForBlackBoxCtrl.m_failReason = HL.Field(HL.String) << ""

RewardsPopUpForBlackBoxCtrl.m_items = HL.Field(HL.Table)

RewardsPopUpForBlackBoxCtrl.m_getItemCells = HL.Field(HL.Function)

RewardsPopUpForBlackBoxCtrl.m_failPointCells = HL.Field(HL.Forward("UIListCache"))


RewardsPopUpForBlackBoxCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_getItemCells = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
    self.m_failPointCells = UIUtils.genCellCache(self.view.pointCell)
    self.view.rewardsScrollList.onGraduallyShowFinish:AddListener(function()
        local firstItemGo = self.view.rewardsScrollList:Get(0)
        if firstItemGo then
            self.view.focusItemKeyHint.gameObject:SetActive(true)
            self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
            local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
            keyHintPos.x = keyHintPos.x - 50
            keyHintPos.y = keyHintPos.y - 45
            self.view.focusItemKeyHint.transform.localPosition = keyHintPos
        end
    end)

    self.view.rewardsScrollList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getItemCells(object)
        self:_OnUpdateCell(cell, LuaIndex(csIndex))
    end)

    self.view.restartDungeonBtn.onClick:AddListener(function()
        self:_OnClickRestartDungeonBtn()
    end)

    self.view.leaveDungeonBtn.onClick:AddListener(function()
        self:_OnLeaveDungeonBtnClick()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local dungeonId, levelTimestamp, isFail, failReason = unpack(arg)
    self.m_dungeonId = dungeonId
    self.m_leaveTimestamp = levelTimestamp
    self.m_isFail = isFail
    self.m_failReason = failReason or ""

    self.m_isAnimationIn = true

    self:UpdateContent()
end

RewardsPopUpForBlackBoxCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.ON_ENTER_BLOCKED_REWARD_POP_UP_PANEL)
    self.view.focusItemKeyHint.gameObject:SetActive(false)
end

RewardsPopUpForBlackBoxCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.ON_EXIT_BLOCKED_REWARD_POP_UP_PANEL)
end

RewardsPopUpForBlackBoxCtrl.OnClose = HL.Override() << function(self)
    if self.m_leaveTick then
        self.m_leaveTick = LuaUpdate:Remove(self.m_leaveTick)
    end

    self.animationWrapper:ClearTween(false)
    Notify(MessageConst.ON_EXIT_BLOCKED_REWARD_POP_UP_PANEL)
end

RewardsPopUpForBlackBoxCtrl.UpdateContent = HL.Method() << function(self)
    self.view.restartDungeonBtn.gameObject:SetActive(self.m_isFail)

    if self.m_isFail then


        self:_StartCoroutine(function()
            local seconds = math.floor(self.m_leaveTimestamp - CS.Beyond.DateTimeUtils.GetCurrentTimestampBySeconds())
            while seconds > 0 do

                local leaveTxt = tostring(seconds) .. Language.LUA_LEAVE_DUNGEON_TEXT
                self.view.leaveTxt.text = leaveTxt

                coroutine.wait(1)
                seconds = seconds - 1
            end
            self:_OnLeaveDungeonBtnClick()
        end)
    else
        self.m_leaveTick = DungeonUtils.startSubGameLeaveTick( function(leftTime)
            local leaveTxt = tostring(leftTime) .. Language.LUA_LEAVE_DUNGEON_TEXT
            self.view.leaveTxt.text = leaveTxt
        end)
    end


    local animWrapper = self.animationWrapper
    if self.m_isFail then
        local success, dungeonData = Tables.dungeonTable:TryGetValue(self.m_dungeonId)
        local featureDesc = dungeonData and dungeonData.featureDesc or ""
        local contentTxt = string.isEmpty(featureDesc) and {} or string.split(featureDesc, "\n")
        self.m_failPointCells:Refresh(#contentTxt, function(cell, index)
            cell.label:SetAndResolveTextStyle(contentTxt[index])
        end)

        self.view.failReasonTxt:SetAndResolveTextStyle(self.m_failReason)

        animWrapper:Play("rewardspopupforblackbox_fail", function()
            self.m_isAnimationIn = false
            animWrapper:Play("rewardspopupforblackbox_failloop")
        end)
    else

        local firstPassRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonFirstPass)
        local needTriggerSysBlueprint = false
        local items = {}
        if firstPassRewardPack and firstPassRewardPack.rewardSourceType == RewardSourceType.DungeonFirstPass then
            for _, itemBundle in pairs(firstPassRewardPack.itemBundleList) do
                local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
                if itemData then
                    table.insert(items, { id = itemBundle.id,
                                          count = itemBundle.count,
                                          typeId = 2,
                                          typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.First,
                                          sortId1 = itemData.sortId1,
                                          sortId2 = itemData.sortId2 })
                    if itemData.type == GEnums.ItemType.SysBluePrint then
                        needTriggerSysBlueprint = true
                    end
                end
            end

        end

        local extraRewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(RewardSourceType.DungeonExtraReward)
        if extraRewardPack and extraRewardPack.rewardSourceType == RewardSourceType.DungeonExtraReward then
            for _, itemBundle in pairs(extraRewardPack.itemBundleList) do
                local _, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
                if itemCfg then
                    table.insert(items, {id = itemBundle.id,
                                         count = itemBundle.count,
                                         typeId = 1,
                                         typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Extra,
                                         sortId1 = itemCfg.sortId1,
                                         sortId2 = itemCfg.sortId2,})
                end
            end
        end

        local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
        table.insert(sortKeys, 1, "typeId")
        table.sort(items, Utils.genSortFunction(sortKeys))
        local count = #items
        self.m_items = items

        self.view.rewardsScrollList.gameObject:SetActiveIfNecessary(false)
        self.view.rewardsScrollList:UpdateCount(count, true)
        self.view.rewardsList.gameObject:SetActiveIfNecessary(count > 0)

        animWrapper:Play("rewardspopupforblackbox_in", function()
            self.m_isAnimationIn = false
            animWrapper:Play("rewardspopupforblackbox_inloop")

            if needTriggerSysBlueprint then
                CS.Beyond.Gameplay.Conditions.OnBlackboxSettlementRewardsHasBlueprint.Trigger()
            end
        end)
    end
end

RewardsPopUpForBlackBoxCtrl._OnUpdateCell = HL.Method(HL.Forward("Item"), HL.Number) << function(self, cell, index)
    local itemBundle = self.m_items[index]
    cell.gameObject.name = itemBundle.id
    cell:InitItem(itemBundle, true)
    cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController, })
    UIUtils.setRewardItemRarityGlow(cell, UIUtils.getItemRarity(itemBundle.id))
end


RewardsPopUpForBlackBoxCtrl._OnClickRestartDungeonBtn = HL.Method() << function(self)
    self:PlayAnimationOutWithCallback(function()
        GameInstance.dungeonManager.curDungeonLikeSubGame:SendReStart(true)
        self:Close()
    end)
end

RewardsPopUpForBlackBoxCtrl._OnLeaveDungeonBtnClick = HL.Method() << function(self)


    if self.m_isAnimationIn then
        return
    end

    self:Notify(MessageConst.HIDE_ITEM_TIPS)



    GameInstance.dungeonManager:LeaveDungeon()
end

HL.Commit(RewardsPopUpForBlackBoxCtrl)
