
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractSettlement
local PHASE_ID = PhaseId.ContingencyContractSettlement


local LEVEL_TAG_STATE_NAME_LIST = {
    "Low",
    "Middle",
    "High",
}


local EQUIP_INDEX_LIST = {
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_1,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_2,
}

ContingencyContractSettlementCtrl = HL.Class('ContingencyContractSettlementCtrl', uiCtrl.UICtrl)

ContingencyContractSettlementCtrl.m_gameId = HL.Field(HL.Any)

ContingencyContractSettlementCtrl.m_activityId = HL.Field(HL.Any)

ContingencyContractSettlementCtrl.m_resultData = HL.Field(HL.Any)

ContingencyContractSettlementCtrl.m_charCells = HL.Field(HL.Forward("UIListCache"))

ContingencyContractSettlementCtrl.m_getTagCellFunc = HL.Field(HL.Function)

ContingencyContractSettlementCtrl.m_luaUpdateKey = HL.Field(HL.Number) << -1

ContingencyContractSettlementCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1

ContingencyContractSettlementCtrl.m_isWorldFreeze = HL.Field(HL.Boolean) << false

ContingencyContractSettlementCtrl.m_playVoiceTimerKey = HL.Field(HL.Number) << -1

ContingencyContractSettlementCtrl.m_recoverInputTimerKey = HL.Field(HL.Number) << -1





ContingencyContractSettlementCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONTINGENCY_CONTRACT_SETTLEMENT] = 'OnSettlement',
}




ContingencyContractSettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_gameId = arg and arg.gameId or nil
    local ccActivities = GameInstance.player.activitySystem:GetActivityOfCertainType(CS.Beyond.GEnums.ActivityType.ContingencyContract)
    if ccActivities.Count > 0 then
        self.m_activityId = ccActivities[0]
    else
        self.m_activityId = ""
    end
    self:_BindUI()
    self:_InitData()
    self:_SetupUI()

    self:_InternalHide(true)
    self.view.luaPanel:BlockAllInput()


    if self.m_resultData.isPass then
        self.view.animationWrapper:Play("contingencycontractsettlement_loadingclip", function()
            self:_StartPlayUIInAni()
        end)
    else
        self:_StartPlayUIInAni()
    end

    AudioManager.PostEvent("au_music_indie_ccdg01_cc_v1d3_b0_ingame_phase4_complete")

    AudioAdapter.PostEvent(self.m_resultData.isPass and "Au_UI_Popup_CCReignite_Completed" or "Au_UI_Popup_CCReignite_Failed")
end

ContingencyContractSettlementCtrl.OnClose = HL.Override() << function(self)
    self:_SetFreezeWorld(false)
    InputManagerInst:SetCursorShowRequest("AutoShowCursorContingencyContractSettlement", false)
    if self.m_clearScreenKey > 0 then
        self.m_clearScreenKey = UIManager:RecoverScreen(self.m_clearScreenKey)
    end
    if self.m_luaUpdateKey > 0 then
        self.m_luaUpdateKey = LuaUpdate:Remove(self.m_luaUpdateKey)
    end
    if self.m_playVoiceTimerKey > 0 then
        self.m_playVoiceTimerKey = self:_ClearTimer(self.m_playVoiceTimerKey)
    end
    if self.m_recoverInputTimerKey > 0 then
        self.m_recoverInputTimerKey = self:_ClearTimer(self.m_recoverInputTimerKey)
    end
    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "ContingencyContractSettlement", false)
    Utils.stopDefaultChannelVoice()
end





ContingencyContractSettlementCtrl._BindUI = HL.Method() << function(self)
    self.view.shareBtn.onClick:AddListener(function()
        self:_OnShareBtnClick()
    end)
    self.view.againBtn.onClick:AddListener(function()
        self:_OnAgainBtnClick()
    end)
    self.view.doneBtn.onClick:AddListener(function()
        self:_OnDoneBtnClick()
    end)
    self.view.leftNdoe.tipsNode.detailsBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "activity_contingency_contract_0_badge")
    end)
    self.m_charCells = UIUtils.genCellCache(self.view.rightNode.roleInfoCell)

    self.m_getTagCellFunc = UIUtils.genCachedCellFunction(self.view.leftNdoe.scrollSettlement)
    self.view.leftNdoe.scrollSettlement.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getTagCellFunc(obj)
        local tagId = self.m_resultData.tagList[LuaIndex(index)]
        local hasCfg, tagEffectCfg = Tables.ccTagTable:TryGetValue(tagId)
        if hasCfg then
            local iconName = tagEffectCfg.icon
            cell.icon:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, iconName)
        end
        AudioAdapter.PostEvent("Au_UI_Event_CCReignite_IconCount")
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ContingencyContractSettlementCtrl._InitData = HL.Method() << function(self)
    local serverResultData
    if self.m_gameId then
        local data = GameInstance.player.contingencyContractSystem:GetCcGameData(self.m_gameId)
        serverResultData = data.dungeonResultData
    end

    if serverResultData then
        self:_ConvertServerResultDataToLua(serverResultData)
    end


    if UNITY_EDITOR and serverResultData == nil then
        self:_ConstructResultData()
    end
end

ContingencyContractSettlementCtrl._ConvertServerResultDataToLua = HL.Method(HL.Any) << function(self, serverResultData)

    if serverResultData == nil then
        return
    end

    local data = {}
    data.isPass = serverResultData.isPass
    data.isBestScore = serverResultData.isBestScore
    data.score = serverResultData.score
    data.tagList = {}
    for i = 0, serverResultData.selectTagList.Count - 1 do
        local tagId = serverResultData.selectTagList[i]
        table.insert(data.tagList, tagId)
    end

    data.tagList = ContingencyContractUtils.SortTagIdsByGridPos(self.m_gameId, data.tagList)

    data.time = serverResultData.time
    data.charInstIdList = {}
    data.charIdList = {}
    for i = 0, serverResultData.charInstIdList.Count - 1 do
        local instId = serverResultData.charInstIdList[i]
        table.insert(data.charInstIdList, instId)
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
        if charInfo then
            table.insert(data.charIdList, charInfo.templateId)
        end
    end
    data.timeStamp = serverResultData.timeStamp

    if serverResultData.isPass then
        data.wave = GameInstance.dungeonManager.curDungeonLikeSubGame.maxStage
    else

        data.wave = GameInstance.dungeonManager.curDungeonLikeSubGame.stage - 1
    end

    data.isGameLock = serverResultData.isGameLock

    self.m_resultData = data
end

ContingencyContractSettlementCtrl._ConstructResultData = HL.Method() << function(self)
    local data = {}
    data.isFake = true
    data.isPass = true

    data.isBestScore = true
    data.score = 25
    data.tagList = { 100201, 100201, 100201, 100201, 100201, 100201 }

    data.time = 70
    data.timeStamp = DateTimeUtils.GetCurrentTimestampBySeconds()
    local charIdList = { "chr_0030_zhuangfy", "chr_0003_endminf", "chr_0013_aglina" }
    data.charIdList = charIdList
    data.charInstIdList = {}
    for _, charId in ipairs(charIdList) do
        local charInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charId, GEnums.CharType.Default)
        if charInfo then
            table.insert(data.charInstIdList, charInfo.instId)
        end
    end
    data.wave = 2

    self.m_resultData = data
end

ContingencyContractSettlementCtrl._SetupUI = HL.Method() << function(self)
    local success = self.m_resultData.isPass
    self.view.main:SetState(success and "Success" or "Fail")
    self.view.main:SetState("NoShare")
    self:_SetupLeftUI()

    self:_SetupRightUI()


    self.view.commonTopTitleNode.titleTxt.text = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_SETTLEMENT_TITLE



    self.view.aniNode.stateController:SetState(success and "Success" or "Fail")

    self.view.aniNode.newRecordNode.gameObject:SetActive(self.m_resultData.isBestScore)
end

ContingencyContractSettlementCtrl._StartScoreAnim = HL.Method() << function(self)
    local curve = self.view.config.SCORE_CURVE
    local start_frame = self.view.config.SCORE_START_FRAME
    local max_frame = self.view.config.SCORE_MAX_FRAME
    local animFps = self.animationWrapper.animationIn.frameRate
    local start_time = start_frame / animFps
    local max_time = max_frame / animFps
    local timeCount = 0
    local score = self.m_resultData.score

    local tag_start_time = self.view.config.TAG_FADE_START / animFps
    local char_start_time = self.view.config.CHAR_FADE_START / animFps
    local tag_start_flag = false
    local char_start_flag = false
    local lastScoreValue = 0


    self.view.aniNode.recordNumTxt.text = "0"

    self.m_luaUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        timeCount = timeCount + deltaTime
        local currentScoreValue = 0
        if timeCount < start_time then
            currentScoreValue = 0
        elseif timeCount > max_time then
            currentScoreValue = score
        else

            local normalizedX = (timeCount - start_time) / (max_time - start_time)
            local normalizedY = curve:Evaluate(normalizedX)
            currentScoreValue = lume.round(score * normalizedY)
        end
        self.view.aniNode.recordNumTxt.text = tostring(currentScoreValue)
        if currentScoreValue ~= lastScoreValue then
            lastScoreValue = currentScoreValue
            AudioAdapter.PostEvent("Au_UI_Event_CCReignite_Count")
        end

        if not tag_start_flag and timeCount > tag_start_time then
            tag_start_flag = true
            self.view.leftNdoe.scrollSettlement:UpdateCount(#self.m_resultData.tagList)
        end

        if not char_start_flag and timeCount > char_start_time then
            char_start_flag = true

            self:_FadeRightUI(self.view.config.CHAR_FADE_TIME)
        end

    end)
end

ContingencyContractSettlementCtrl._SetupLeftUI = HL.Method() << function(self)

    local leftNode = self.view.leftNdoe

    leftNode.titleText.text = self.m_resultData.isPass
        and Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_RESULT_TITLE_SUCCESS
        or Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_RESULT_TITLE_FAIL

    local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
    if activityData then
        leftNode.shareTitleTxt.text = activityData.name
    end


    local levelLayout = leftNode.levelLayout
    for i = 1, 4 do
        local name = "contingencyContractLevelCell0" .. i
        local stateCtrl = levelLayout[name]
        if stateCtrl then
            if i <= self.m_resultData.wave then
                stateCtrl:SetState("done")
            else
                stateCtrl:SetState("Normal")
            end
        end
    end

    leftNode.totalNumTxt.text = self.m_resultData.score
    leftNode.newScoreNode.gameObject:SetActive(self.m_resultData.isBestScore)

    local seconds = self.m_resultData.time
    if seconds >= 3600 then
        leftNode.timeNumTxt.text = UIUtils.getRemainingText(self.m_resultData.time)
    else
        leftNode.timeNumTxt.text = UIUtils.getRemainingTextToMinute(self.m_resultData.time)
    end



    local showSocialTips = self.m_resultData.isBestScore and self.m_resultData.score > 0
    leftNode.tipsNode.gameObject:SetActive(showSocialTips)
    if showSocialTips then
        leftNode.tipsNode.levelTxt.text = self.m_resultData.score

        local stateCtrl = leftNode.tipsNode.levelTagNode
        local stateLevel = 1
        local currentActivityId = self.m_activityId
        local rangeArray = Tables.activityContingencyContractTable:GetValue(currentActivityId).rangeArray
        for i = 1, #rangeArray do
            local range = rangeArray[CSIndex(i)]
            if self.m_resultData.score >= range then
                stateLevel = i + 1
            end
        end
        stateCtrl:SetState(LEVEL_TAG_STATE_NAME_LIST[stateLevel])
    end
end

ContingencyContractSettlementCtrl._SetupRightUI = HL.Method() << function(self)
    local charCount = #self.m_resultData.charInstIdList
    self.m_charCells:GraduallyRefresh(4, 0.1, function(cell, index)
        cell.gameObject:SetActive(false)

        if index > charCount then

            cell.stateController:SetState("Empty")
        else
            cell.stateController:SetState("Nrl")
            local charInstId = self.m_resultData.charInstIdList[index]
            self:_SetupCharCell(cell, charInstId)
        end
    end)
end

ContingencyContractSettlementCtrl._FadeRightUI = HL.Method(HL.Any) << function(self, time)
    local curr = 1
    self:_StartCoroutine(function()
        for curr = 1, 4 do
            local cell = self.m_charCells:GetItem(curr)
            if cell then
                cell.gameObject:SetActive(true)
            end
            coroutine.wait(time)
        end
    end)
end

ContingencyContractSettlementCtrl._SetupCharCell = HL.Method(HL.Any, HL.Any) << function(self, cell, charInstId)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local templateId = charInfo.templateId
    cell.roleImg:LoadSprite(UIConst.UI_SPRITE_GACHA_CHAR, templateId)


    local potentialLevel = charInfo.potentialLevel
    cell.potentialStar:InitCharPotentialStarByLevel(potentialLevel)


    local charLevel = charInfo.level
    cell.roleLvTxt.text = tostring(charLevel)


    local weaponData = CharInfoUtils.getCharCurWeapon(charInstId)
    if weaponData then

        local weaponLevel = weaponData.weaponInst.weaponLv
        cell.weapoNode.weaponLvTxt.text = tostring(weaponLevel)

        local weaponItemCfg = Tables.itemTable:GetValue(weaponData.weaponCfg.weaponId)
        local weaponIcon = weaponItemCfg.iconId
        cell.weapoNode.weaponIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, weaponIcon)

        local rarity = weaponItemCfg.rarity
        UIUtils.setItemRarityImage(cell.weapoNode.weapoQualityImg, rarity)

        local refineLv = weaponData.weaponInst.refineLv
        cell.weapoNode.potentialStar:InitCharPotentialStarByLevel(refineLv)

        local attrInfoList = {}
        local weaponInst = weaponData.weaponInst
        local hasAttachedGem = weaponInst.attachedGemInstId and weaponInst.attachedGemInstId > 0
        local _, toSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(
            Utils.getCurrentScope(),
            weaponData.weaponInstId,
            weaponInst.attachedGemInstId,
            weaponInst.breakthroughLv,
            weaponInst.refineLv)

        local fromSkillList
        if toSkillList and hasAttachedGem then
            local _, tmpList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(
                Utils.getCurrentScope(),
                weaponData.weaponInstId,
                nil,
                weaponInst.breakthroughLv,
                weaponInst.refineLv)
            fromSkillList = tmpList
        end
        if toSkillList then
            for i = 0, toSkillList.Count - 1 do
                local toAttr = toSkillList[i]
                local hasGemAddOn = false
                if fromSkillList and i < fromSkillList.Count then
                    hasGemAddOn = toAttr.level > fromSkillList[i].level
                end
                table.insert(attrInfoList, { value = toAttr.level, hasGemAddOn = hasGemAddOn })
            end
        end
        for i = 1, 3 do
            local attrCellName = "lvDotCell0" .. tostring(i)
            local attrCell = cell.weapoNode[attrCellName]
            if attrCell then
                if i <= #attrInfoList then
                    local info = attrInfoList[i]
                    attrCell.gameObject:SetActive(true)
                    attrCell.dotNumTxt.text = tostring(info.value)
                    local color = info.hasGemAddOn and self.view.config.GEM_EFFECT_COLOR or Color.white
                    attrCell.dotNumTxt.color = color
                    attrCell.dotImg.color = color
                else
                    attrCell.gameObject:SetActive(false)
                end
            end
        end
    end



    local equipNode = cell.equipNode
    local equips = charInfo.equipCol
    for i = 1, 4 do
        local slotIndex = EQUIP_INDEX_LIST[i]
        local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
        local hasValue, equipInstId = equips:TryGetValue(equipIndex)
        local cellName = "equipCell0" .. i

        local equipCell = equipNode[cellName]
        if equipCell then
            equipCell.stateController:SetState(hasValue and "Nrl" or "Empty")
            if hasValue and equipInstId > 0 then

                local equipInst = CharInfoUtils.getEquipByInstId(equipInstId)
                local equipTemplateId = equipInst.templateId
                equipCell.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
                    equipInstId = equipInstId,

                })
                local itemCfg = Tables.itemTable:GetValue(equipTemplateId)
                equipCell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
                UIUtils.setItemRarityImage(equipCell.qualityImg, itemCfg.rarity)
            end
        end
    end
end

ContingencyContractSettlementCtrl._OnShareBtnClick = HL.Method() << function(self)
    self.view.main:SetState("Share")

    self.view.leftNdoe.tipsNode.gameObject:SetActive(false)

    local codeId = ""
    local showCodeId = ""
    if not self.m_resultData.isFake then
        _, codeId = ContingencyContractUtils.GenShareText(
            self.m_gameId,
            self.m_resultData.tagList,
            self.m_resultData.score,
            self.m_resultData.isPass and ContingencyContractUtils.GenShareCodeSourceType.DungeonClear
                or ContingencyContractUtils.GenShareCodeSourceType.DungeonFail
        )
        _, showCodeId = ContingencyContractUtils.GenShareCode(self.m_gameId, self.m_resultData.tagList)
    end
    Notify(MessageConst.SHOW_COMMON_SHARE_PANEL, {
        type = "ContingencyContractSettlement",
        codeId = codeId,
        showCodeId = showCodeId,
        showPlayerInfo = true,
        showPlayerInfoToggle = true,
        success = false,
        onCaptureEnd = function()
            Unity.GUIUtility.systemCopyBuffer = codeId
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_RESULT_SHARE_COPY_TOAST)

            self.view.main:SetState("NoShare")


            local showSocialTips = self.m_resultData.isBestScore and self.m_resultData.score > 0
            self.view.leftNdoe.tipsNode.gameObject:SetActive(showSocialTips)
        end,
        onClose = function()
        end,
        needEdge = true,
        timeStamp = Utils.appendUTC(Utils.timestampToDateYMDHM(self.m_resultData.timeStamp)),
    })
end

ContingencyContractSettlementCtrl._OnAgainBtnClick = HL.Method() << function(self)
    local activityIsClosed = self:_ActivityIsClosed()
    if activityIsClosed then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_SETTLEMENT_RETRY_ON_CLOSE)
        return
    else
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_SETTLEMENT_RETRY_POPUP,
            onConfirm = function()

                self:_GetRewardItemList()
                self:Close()
                GameInstance.dungeonManager.curDungeonLikeSubGame:SendReStart()
            end,
            onCancel = function()
            end,
        })
    end
end

ContingencyContractSettlementCtrl._OnDoneBtnClick = HL.Method() << function(self)
    if self.m_resultData and self.m_resultData.isFake then
        self:PlayAnimationOut()
        self:_TestShowRewardPanel()
    else

        local items = self:_GetRewardItemList()
        if #items == 0 then
            self:_LeaveDungeonAfterSettlement()
        else
            local rewardPanelArgs = {}
            rewardPanelArgs.items = items
            rewardPanelArgs.onComplete = function()
                self:_LeaveDungeonAfterSettlement()
            end
            Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArgs)
        end
    end
end

ContingencyContractSettlementCtrl._StartPlayUIInAni = HL.Method() << function(self)
    self.m_clearScreenKey = UIManager:ClearScreen({ PanelId.ContingencyContractSettlement })
    if not DeviceInfo.usingController then
        InputManagerInst:SetCursorShowRequest("AutoShowCursorContingencyContractSettlement", true)
    end
    self:_SetFreezeWorld(true)
    self:_InternalHide(false)
    self.view.animationWrapper:PlayInAnimation()
    self.m_playVoiceTimerKey = self:_StartTimer(self.view.config.WAIT_TIME_PLAY_VOICE, function()
        self:_StartPlayCharVoice()
    end, false)
    self.m_recoverInputTimerKey = self:_StartTimer(self.view.config.WAIT_TIME_RECOVER_INPUT, function()
        self.view.luaPanel:RecoverAllInput()
    end, false)
    self:_StartScoreAnim()
    if self:_ActivityIsClosed() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_CONTINGENCY_CONTRACT_SETTLEMENT_SHOW_PANEL_ON_CLOSE)
    end
end

ContingencyContractSettlementCtrl._StartPlayCharVoice = HL.Method() << function(self)
    if not self.m_resultData.isPass then
        return
    end

    local charIdList = self.m_resultData.charIdList
    if not charIdList or #charIdList == 0 then
        return
    end
    local charId = charIdList[math.random(1, #charIdList)]

end

ContingencyContractSettlementCtrl._SetFreezeWorld = HL.Method(HL.Boolean) << function(self, isFreeze)
    if self.m_isWorldFreeze == isFreeze then
        return
    end

    self.m_isWorldFreeze = isFreeze
    if isFreeze then
        Notify(MessageConst.OPEN_FREEZE_WORLD_PANEL, "ContingencyContractSettlementCtrl")
    else
        Notify(MessageConst.CLOSE_FREEZE_WORLD_PANEL, "ContingencyContractSettlementCtrl")
    end
end

ContingencyContractSettlementCtrl._InternalHide = HL.Method(HL.Boolean) << function(self, isHide)
    if isHide then
        self.view.main.gameObject:SetLayerRecursive(UIConst.HIDE_LAYER)
        self.view.mainCanvasGroup.alpha = 0
        self.view.animationWrapper:SampleToOutAnimationEnd()
    else
        self.view.mainCanvasGroup.alpha = 1
        self.view.main.gameObject:SetLayerRecursive(UIConst.UI_LAYER)
    end

end


ContingencyContractSettlementCtrl._TestShowRewardPanel = HL.Method() << function(self)
    local testReward = true
    if testReward then
        local items = {}
        local testItemIdList = { "item_ap_supply_lt_n" }
        for _, itemId in ipairs(testItemIdList) do
            local _, itemData = Tables.itemTable:TryGetValue(itemId)
            if itemData then
                table.insert(items, {id = itemId,
                                     type = itemData.type:ToInt()})
            end
        end
        local rewardPanelArgs = {}
        rewardPanelArgs.items = items
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArgs)
    end
end

ContingencyContractSettlementCtrl._GetRewardItemList = HL.Method().Return(HL.Table) << function(self)
    local rewardSourceTypeList = {
        CS.Beyond.GEnums.RewardSourceType.DungeonFirstPass,
        CS.Beyond.GEnums.RewardSourceType.Dungeon,
    }
    local items = {}
    for _, sourceType in ipairs(rewardSourceTypeList) do
        local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(sourceType)
        if rewardPack and rewardPack.rewardSourceType == sourceType then
            for _, itemBundle in pairs(rewardPack.itemBundleList) do
                local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
                if itemData then
                    local found = lume.match(items, function(item) return item.id == itemBundle.id end)
                    if found then
                        found.count = found + itemBundle.count
                    else
                        table.insert(items, {id = itemBundle.id,
                                             count = itemBundle.count,
                                             instData = itemBundle.instData,
                                             instId = itemBundle.instId,
                                             rarity = itemData.rarity,
                                             type = itemData.type:ToInt()})
                    end
                end
            end
        end
    end
    table.sort(items, Utils.genSortFunction({"rarity", "type", "id"}, false))
    return items
end

ContingencyContractSettlementCtrl._LeaveDungeonAfterSettlement = HL.Method() << function(self)
    if self:_ActivityIsClosed() and self.m_gameId then
        LuaSystemManager.uiRestoreSystem:RemoveRequest(self.m_gameId)
    end
    GameInstance.dungeonManager:LeaveDungeon()
end

ContingencyContractSettlementCtrl._ActivityIsClosed = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_resultData.isGameLock then
        return true
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activityData == nil then
        return true
    end

    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local isGameplayEnd = activityData.gameplayEndTime - currentTime < 0
    return isGameplayEnd
end






ContingencyContractSettlementCtrl.OnSettlement = HL.StaticMethod(HL.Any) << function(args)

    local isAllDead = Utils.isCurSquadAllDead()
    if isAllDead then
        logger.info("ContingencyContractSettlementCtrl.OnSettlement: 等待角色死亡动画播完后再弹")
        return
    end

    GameInstance.player.contingencyContractSystem.latestSettlementGameId = ""
    ContingencyContractSettlementCtrl.ShowSettlement(args)
end

ContingencyContractSettlementCtrl.ShowSettlement = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonSettlement", function()
        if not Utils.isInDungeon() then


            logger.error(ELogChannel.Dungeon, "error, try to open ContingencyContractSettlement out of dungeon")
            return
        end


        PhaseManager:ExitPhaseFastTo(PhaseId.Level)

        local gameId = unpack(args)

        local ctrl = UIManager:AutoOpen(PANEL_ID, {
            gameId = gameId,
        })
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end



HL.Commit(ContingencyContractSettlementCtrl)
