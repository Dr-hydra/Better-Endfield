local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCharacterTrial
local activitySystem = GameInstance.player.activitySystem
local CharacterTrialStatus = CS.Beyond.Gameplay.ActivitySystem.CharacterTrialStatus

ActivityCharacterTrialCtrl = HL.Class('ActivityCharacterTrialCtrl', uiCtrl.UICtrl)






ActivityCharacterTrialCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHARACTER_TRIAL_INFO_CHANGE] = 'OnCharacterTrialInfoChange',
}

ActivityCharacterTrialCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCharacterTrialCtrl.m_csIndex2HeadCell = HL.Field(HL.Table)

ActivityCharacterTrialCtrl.m_csIndex2dungeonId = HL.Field(HL.Table)

ActivityCharacterTrialCtrl.m_trialDataList = HL.Field(HL.Table)

ActivityCharacterTrialCtrl.m_selectedCsIndex = HL.Field(HL.Number) << -1

ActivityCharacterTrialCtrl.m_headNum = HL.Field(HL.Number) << 0

ActivityCharacterTrialCtrl.m_dungeonCount = HL.Field(HL.Number) << 0

ActivityCharacterTrialCtrl.m_headCells = HL.Field(HL.Forward("UIListCache"))

ActivityCharacterTrialCtrl.m_equipCells = HL.Field(HL.Any)


local HEAD_ICON_SMALL = "_s"
local BG_IMAGE_FOLDER = "Activity"



ActivityCharacterTrialCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)

    self.m_equipCells = UIUtils.genCellCache(self.view.equipItem)
    self.view.btnSmallest.onClick:AddListener(function()
        ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "gotoGachaButton", "visit_gacha")
        if self.m_selectedCsIndex == -1 then
            return
        end

        local dungeonId = self.m_csIndex2dungeonId[self.m_selectedCsIndex]
        local charTrial = Tables.activityCharTrial[dungeonId]

        if Utils.canJumpToSystem(charTrial.jumpId)  then
            Utils.jumpToSystem(charTrial.jumpId)
        end
    end)

    self.view.btnGoto.onClick:AddListener(function()
        ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "gotoActivityHudButton", "visit_activity")
        self:ClickGotoBtn()
    end)

    self.view.btnReward.onClick:AddListener(function()
        ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "gotoActivityHudButton", "visit_activity")
        self:ClickGotoBtn()
    end)

    self.view.rewardsPreviewNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
        if not isFocus then
            self.view.scrollViewRewardsNaviGroup:ManuallyStopFocus()
        end
    end)
    self.view.scrollViewRewardsNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
        if not isFocus then
            self.view.rewardsPreviewNaviGroup:ManuallyStopFocus()
        end
    end)
    self.view.scrollViewEquipNaviGroup.onIsFocusedChange:AddListener(function(isFocus)
        if not isFocus then
            self.view.rewardsPreviewNaviGroup:ManuallyStopFocus()
            self.view.scrollViewRewardsNaviGroup:ManuallyStopFocus()
        end
    end)

    self.m_headCells = UIUtils.genCellCache(self.view.headCell)

    self.m_trialDataList = {}
    for dungeonId, trialData in pairs(Tables.activityCharTrial) do
        if trialData.activityId == self.m_activityId then
            local trialTable = {
                dungeonId = dungeonId,
                activityId = trialData.activityId,
                sortId = trialData.sortId,
            }
            table.insert(self.m_trialDataList, trialTable)
        end
    end

    table.sort(self.m_trialDataList, Utils.genSortFunction({ "sortId" }, true))

    self.m_csIndex2dungeonId = {}
    self.m_csIndex2HeadCell = {}

    local lastSelectedCsIndex = 0
    for luaIndex, trialData in pairs(self.m_trialDataList) do
        local dungeonId = trialData.dungeonId
        self.m_csIndex2dungeonId[CSIndex(luaIndex)] = dungeonId
        if args.dungeonId == dungeonId then
            lastSelectedCsIndex = CSIndex(luaIndex)
        end
    end

    self.m_dungeonCount = #self.m_trialDataList

    if self.m_dungeonCount > 0 then
        if self.m_dungeonCount > 1 then
            self.view.prevKeyHint.gameObject:SetActive(true)
            self.view.nextKeyHint.gameObject:SetActive(true)
        else
            self.view.prevKeyHint.gameObject:SetActive(false)
            self.view.nextKeyHint.gameObject:SetActive(false)
        end
        self.m_headCells:Refresh(self.m_dungeonCount, function(cell, luaIndex)
            self:_OnUpdateHeadCell(cell, luaIndex)
        end)
        self:_SelectHead(lastSelectedCsIndex)
    end

    self:BindInputPlayerAction("char_trial_select_prev", function()
        self:_SelectPrev()
    end)
    self:BindInputPlayerAction("char_trial_select_next", function()
        self:_SelectNext()
    end)
end

ActivityCharacterTrialCtrl._SelectPrev = HL.Method() << function(self)
    if self.m_selectedCsIndex == 0 then
        return
    end
    self:_SelectHead(self.m_selectedCsIndex - 1)
end

ActivityCharacterTrialCtrl._SelectNext = HL.Method() << function(self)
    if self.m_selectedCsIndex == self.m_dungeonCount - 1 then
        return
    end
    self:_SelectHead(self.m_selectedCsIndex + 1)
end

ActivityCharacterTrialCtrl._OnUpdateHeadCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local csIndex = CSIndex(luaIndex)
    local dungeonId = self.m_csIndex2dungeonId[csIndex]
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local charId = dungeonCfg.relatedCharId
    local success, charData = Tables.characterTable:TryGetValue(charId)
    if charData then
        local professionCfg = Tables.charProfessionTable[charData.profession]
        local iconName = professionCfg.iconId .. HEAD_ICON_SMALL
        cell.selectedIconImg:LoadSprite(UIConst.UI_SPRITE_HOR_CHAR_HEAD, charId)
        cell.normalIconImg:LoadSprite(UIConst.UI_SPRITE_HOR_CHAR_HEAD, charId)
        cell.charNameTxt.text = charData.name
        cell.professionIconImg:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, iconName)
    end

    cell.redDotNormal:InitRedDot("ActivityCharTrialGetReward", {activityId=self.m_activityId, dungeonId=dungeonId})
    cell.redDotSelected:InitRedDot("ActivityCharTrialGetReward", {activityId=self.m_activityId, dungeonId=dungeonId})

    self.m_csIndex2HeadCell[csIndex] = cell
    cell.headNormal.onClick:RemoveAllListeners()
    cell.headNormal.onClick:AddListener(function()
        self:_SelectHead(csIndex)
    end)

end

ActivityCharacterTrialCtrl.OnCharacterTrialInfoChange = HL.Method(HL.Any) << function(self, args)
    self:_UpdateHeadInfo(self.m_selectedCsIndex)
    local newRewardDungeonId = unpack(args)
    if not string.isEmpty(newRewardDungeonId) and #newRewardDungeonId > 0 then
        self:_UpdateHeadInfo(self.m_selectedCsIndex)








    end
end


ActivityCharacterTrialCtrl._SelectHead = HL.Method(HL.Number) << function(self, csIndex)
    if self.m_selectedCsIndex == csIndex then
        return
    end
    self:_UpdateHeadInfo(csIndex)
end

ActivityCharacterTrialCtrl._UpdateHeadInfo = HL.Method(HL.Number) << function(self, csIndex)
    for key, cell in pairs(self.m_csIndex2HeadCell) do
        if key == csIndex then
            cell.headNormal.gameObject:SetActive(false)
            cell.headSelected.gameObject:SetActive(true)

            local dungeonId = self.m_csIndex2dungeonId[csIndex]
            local trialStatus = GameInstance.player.activitySystem:CheckCharacterTrial(self.m_activityId, dungeonId)
            if trialStatus == CharacterTrialStatus.GotReward then
                cell.redDotNormalImage.gameObject:SetActive(true)
                cell.redDotSelectedImage.gameObject:SetActive(true)
            else
                cell.redDotNormalImage.gameObject:SetActive(false)
                cell.redDotSelectedImage.gameObject:SetActive(false)
            end

        else
            cell.headNormal.gameObject:SetActive(true)
            cell.headSelected.gameObject:SetActive(false)
        end
    end

    self.m_selectedCsIndex = csIndex
    local dungeonId = self.m_csIndex2dungeonId[csIndex]
    local charTrial = Tables.activityCharTrial[dungeonId]
    local trialStatus = GameInstance.player.activitySystem:CheckCharacterTrial(self.m_activityId, dungeonId)
    if trialStatus == CharacterTrialStatus.CanTrial then
        self.view.btnGoto.gameObject:SetActive(true)
        self.view.btnReward.gameObject:SetActive(false)
        self.view.activityCommonInfo:UpdateRewardInfo(charTrial.showRewardId)
        self:UpdateEquipReward(charTrial.equipRewardId)
    elseif trialStatus == CharacterTrialStatus.CanGetReward then
        self.view.btnGoto.gameObject:SetActive(false)
        self.view.btnReward.gameObject:SetActive(true)
        self.view.activityCommonInfo:UpdateRewardInfo(charTrial.showRewardId)
        self:UpdateEquipReward(charTrial.equipRewardId)
    elseif trialStatus == CharacterTrialStatus.GotReward then
        self.view.btnGoto.gameObject:SetActive(true)
        self.view.btnReward.gameObject:SetActive(false)
        self.view.activityCommonInfo:UpdateRewardInfo(charTrial.showRewardId)
        self:UpdateEquipReward(charTrial.equipRewardId)
    end

    self.view.activityCommonInfo:UpdateDescTxt(charTrial.desc)
    self.view.roleImg:LoadSprite(BG_IMAGE_FOLDER, charTrial.bgRolePath)
end

ActivityCharacterTrialCtrl.UpdateEquipReward = HL.Method(HL.Any) << function(self, equipRewardId)
    if equipRewardId == nil or string.isEmpty(equipRewardId) then
        self.view.equipNode.gameObject:SetActive(false)
        self.view.arrowImgRectTransform.gameObject:SetActive(false)
        return
    end
    self.view.equipNode.gameObject:SetActive(true)
    self.view.arrowImgRectTransform.gameObject:SetActive(false)

    local _, rewardTableData = Tables.rewardTable:TryGetValue(equipRewardId)
    local rewardBundles = UIUtils.getRewardItems(equipRewardId)

    local showBundles = {}
    for i = 1, #rewardBundles do
        local b = rewardBundles[i]
        for j = 1, b.count do
            table.insert(showBundles, b)
        end
    end

    self.m_equipCells:Refresh(#showBundles, function(cell, index)

        cell:InitItem(showBundles[index], function()
            local itemBundle = GameInstance.player.charBag:CreateClientMaxEnhancedEquip(showBundles[index].id)
            cell.instId = itemBundle.instId
            cell:ShowTips(nil, function()
                GameInstance.player.charBag:RemoveClientItemBundle(cell.instId)
            end)
        end)
        cell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.activityCommonInfo.view.scrollViewRewards,
            isSideTips = true,
            onBeforeJump = function()
                if DeviceInfo.usingController then
                    self.view.scrollViewEquipNaviGroup:ManuallyStopFocus()
                    self.view.rewardsPreviewNaviGroup:ManuallyStopFocus()
                    self.view.scrollViewRewardsNaviGroup:ManuallyStopFocus()
                end
            end
        })
        cell.view.countNode.gameObject:SetActive(false)
        cell.view.equipEnhanceNode.gameObject:SetActive(true)
    end)
end

ActivityCharacterTrialCtrl.ClickGotoBtn = HL.Method() << function(self)
    if self.m_selectedCsIndex == -1 then
        return
    end
    local dungeonId = self.m_csIndex2dungeonId[self.m_selectedCsIndex]
    local trialStatus = GameInstance.player.activitySystem:CheckCharacterTrial(self.m_activityId, dungeonId)
    if trialStatus == CharacterTrialStatus.CanGetReward then
        GameInstance.player.activitySystem:GetCharTrialReward(self.m_activityId, dungeonId)
    else
        self:JumpToDungeon()
    end
end


ActivityCharacterTrialCtrl.JumpToDungeon = HL.Method() << function(self)
    if self.m_selectedCsIndex == -1 then
        return
    end
    local dungeonId = self.m_csIndex2dungeonId[self.m_selectedCsIndex]
    local hasSubGameInstData, subGameData = DataManager.subGameInstDataTable:TryGetValue(dungeonId)
    if not hasSubGameInstData then
        logger.error("JumpToDungeon失败,没有对应的subGameData,subGameId:", dungeonId)
        return
    end

    local teamId = subGameData.teamConfigId
    local lockedTeamData = CharInfoUtils.getLockedFormationData(teamId, true)

    local charInfos = {}
    for _, charInfo in ipairs(lockedTeamData.chars) do
        table.insert(charInfos, CharInfoUtils.getPlayerCharInfoByInstId(charInfo.charInstId))
    end

    if GameInstance.dungeonManager:TryReqEnterDungeon(dungeonId, charInfos) then
        LuaSystemManager.uiRestoreSystem:AddRequest(dungeonId)
        Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.DungeonBattleFirst)
    end
    GameInstance.player.charBag:ClearAllClientCharAndItemData()
end

HL.Commit(ActivityCharacterTrialCtrl)
