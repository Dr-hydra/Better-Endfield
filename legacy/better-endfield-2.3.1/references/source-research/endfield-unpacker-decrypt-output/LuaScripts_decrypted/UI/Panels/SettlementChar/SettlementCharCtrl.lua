local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementChar
local PHASE_ID = PhaseId.SettlementChar

local settlementSystem = GameInstance.player.settlementSystem

SettlementCharCtrl = HL.Class('SettlementCharCtrl', uiCtrl.UICtrl)






SettlementCharCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SETTLEMENT_OFFICER_CHANGE] = '_OnSettlementOfficerChange',
}


SettlementCharCtrl.m_settlementId = HL.Field(HL.String) << ""

SettlementCharCtrl.m_curOfficerId = HL.Field(HL.Any) << nil

SettlementCharCtrl.m_charInfoList = HL.Field(HL.Table)

SettlementCharCtrl.m_stlWantTagInfoList = HL.Field(HL.Table)

SettlementCharCtrl.m_stlWantCharTagIdSet = HL.Field(HL.Table)

SettlementCharCtrl.m_curSelectedCharIndex = HL.Field(HL.Number) << 1

SettlementCharCtrl.m_getFeatureCellFunc = HL.Field(HL.Function)

SettlementCharCtrl.m_getCharCellFunc = HL.Field(HL.Function)

SettlementCharCtrl.m_firstTimePlayCurSettleTagAni = HL.Field(HL.Boolean) << false

SettlementCharCtrl.m_firstTimePlayOtherSettleTagAni = HL.Field(HL.Boolean) << false





SettlementCharCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_ApplyResumeState(type(arg) == "table" and arg.resumeState or nil)
    self:_RefreshAllUI(true)
end

SettlementCharCtrl.OnShow = HL.Override() << function(self)
end

SettlementCharCtrl.OnAnimationInFinished = HL.Override() << function(self)

    local selectedCell = self.m_getCharCellFunc(self.m_curSelectedCharIndex)
    if selectedCell then
        self:SetNaviTarget(selectedCell.btn)
    end
end



SettlementCharCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    if type(arg) == "table" then
        self.m_settlementId = arg.settlementId
    else
        self.m_settlementId = arg
    end
    self.m_curOfficerId = settlementSystem:GetSettlementOfficerId(self.m_settlementId)
    self.m_curSelectedCharIndex = 1
    self.m_stlWantTagInfoList = {}
    self.m_stlWantCharTagIdSet = {}
    local tagGroup = Tables.settlementBasicDataTable[self.m_settlementId].wantTagIdGroup
    for _, stlTagId in pairs(tagGroup) do
        local stlTagData = Tables.settlementTagTable[stlTagId]
        local info = {
            stlTagId = stlTagId,
            charTagIds = stlTagData.enhanceCharTagId,
            tagName = stlTagData.settlementTagName,
            tagIncludeEnhanceDesc = stlTagData.desc .. "\n" .. UIUtils.getSettlementEnhanceEffectDesc(
                stlTagData.enhanceMoneyProduceSpeedRate,
                stlTagData.enhanceMoneyProfitRate,
                stlTagData.enhanceExpProfitRate
            ),

            enhanceMoneyProduceSpeedRate = stlTagData.enhanceMoneyProduceSpeedRate,
            enhanceMoneyProfitRate = stlTagData.enhanceMoneyProfitRate,
            enhanceExpProfitRate = stlTagData.enhanceExpProfitRate,
        }
        table.insert(self.m_stlWantTagInfoList, info)

        for _, charTagId in pairs(info.charTagIds) do
            self.m_stlWantCharTagIdSet[charTagId] = info
        end
    end
end

SettlementCharCtrl._UpdateData = HL.Method() << function(self)

    self.m_charInfoList = {}
    for _, charInfo in pairs(GameInstance.player.charBag.charList) do
        local charId = charInfo.templateId
        if charId == Tables.globalConst.maleCharID or charId == Tables.globalConst.femaleCharID then
            goto continue
        end
        local charData = Tables.characterTable[charId]
        local hasData, charTagData = Tables.characterTagTable:TryGetValue(charId)
        if not hasData then
            logger.error("characterTagTable missing id : " .. charId)
            goto continue
        end
        local info = {

            templateId = charId,
            charName = charData.name,
            charIcon = UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. charId,
            charHeadIcon = UIConst.UI_CHAR_HEAD_PREFIX .. charId,

            settledId = settlementSystem:GetCharSettledId(charId),

            blocTagInfo = self:_WrapCharTagInfo(charTagData.blocTagId),
            expertTagInfoList = {},
            hobbyTagInfoList = {},

            enhanceMoneyProduceSpeedRate = 0,
            enhanceMoneyProfitRate = 0,
            enhanceExpProfitRate = 0,

            isCurOfficerOrder = charId == self.m_curOfficerId and 1 or 0,
            matchTagCount = 0,
            totalEnhanceRate = 0,
        }
        for _, tagId in pairs(charTagData.hobbyTagIds) do
            local tagInfo = self:_WrapCharTagInfo(tagId)
            table.insert(info.hobbyTagInfoList, tagInfo)
        end
        for _, tagId in pairs(charTagData.expertTagIds) do
            local tagInfo = self:_WrapCharTagInfo(tagId)
            table.insert(info.expertTagInfoList, tagInfo)
        end

        local matchTagCount = 0
        for _, stlTagInfo in pairs(self.m_stlWantTagInfoList) do
            if settlementSystem:IsCharMatchSettlementTag(charId, stlTagInfo.stlTagId) then
                matchTagCount = matchTagCount + 1
                info.enhanceMoneyProduceSpeedRate = info.enhanceMoneyProduceSpeedRate + stlTagInfo.enhanceMoneyProduceSpeedRate
                info.enhanceMoneyProfitRate = info.enhanceMoneyProfitRate + stlTagInfo.enhanceMoneyProfitRate
                info.enhanceExpProfitRate = info.enhanceExpProfitRate + stlTagInfo.enhanceExpProfitRate
            end
        end
        info.matchTagCount = matchTagCount
        info.totalEnhanceRate = info.enhanceMoneyProduceSpeedRate +
            info.enhanceMoneyProfitRate +
            info.enhanceExpProfitRate

        table.insert(self.m_charInfoList, info)
        :: continue ::
    end





    table.sort(self.m_charInfoList, Utils.genSortFunction({"isCurOfficerOrder", "matchTagCount", "totalEnhanceRate", "templateId"}))
end




SettlementCharCtrl._InitUI = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.removeOfficerBtn.onClick:AddListener(function()
        settlementSystem:SendSetOfficer(self.m_settlementId, nil)
    end)

    self.view.confirmOfficerBtn.onClick:AddListener(function()
        self:_OnClickConfirmOfficerBtn()
    end)


    self.m_getCharCellFunc = UIUtils.genCachedCellFunction(self.view.charScrollList)
    self.view.charScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RefreshCharCell(self.m_getCharCellFunc(obj), LuaIndex(csIndex), true)
    end)

    self.m_getFeatureCellFunc = UIUtils.genCachedCellFunction(self.view.featureScrollList)
    self.view.featureScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RefreshFeatureCell(self.m_getFeatureCellFunc(obj), LuaIndex(csIndex))
    end)
end

SettlementCharCtrl._RefreshAllUI = HL.Method(HL.Boolean) << function(self, isInit)

    local needRecover = self.m_curSelectedCharIndex <= 0 or self.m_curSelectedCharIndex > #self.m_charInfoList
    if isInit and needRecover then
        self.m_curSelectedCharIndex = 1
        self.view.charScrollList:UpdateCount(#self.m_charInfoList, isInit)
    else
        self.view.charScrollList:UpdateCount(#self.m_charInfoList, self.m_curSelectedCharIndex)
    end
    self:_RefreshFeatureInfo()
    self:_RefreshOfficerInfo()
end

SettlementCharCtrl._RefreshFeatureInfo = HL.Method() << function(self)
    self.view.featureScrollList:UpdateCount(#self.m_stlWantTagInfoList)
    self.view.featureListAniWrapper:Play("settlementchar_change")
end

SettlementCharCtrl._RefreshOfficerInfo = HL.Method() << function(self)
    if self.m_curSelectedCharIndex <= 0 or self.m_curSelectedCharIndex > #self.m_charInfoList then
        logger.error("index out of range, index: " .. self.m_curSelectedCharIndex)
        return
    end

    local charInfo = self.m_charInfoList[self.m_curSelectedCharIndex]

    self.view.officerNameText.text = charInfo.charName
    self.view.officerHeadIcon.spriteName = charInfo.charHeadIcon

    local effectStr = UIUtils.getSettlementEnhanceEffectDesc(charInfo.enhanceMoneyProduceSpeedRate, charInfo.enhanceMoneyProfitRate, charInfo.enhanceExpProfitRate)
    if string.isEmpty(effectStr) then
        self.view.officerEffectText.text = Language.LUA_SETTLEMENT_CHARACTER_NO_EFFECT
    else
        self.view.officerEffectText:SetAndResolveTextStyle(effectStr)
    end

    local isCurOfficer = charInfo.templateId == self.m_curOfficerId
    self.view.removeOfficerBtn.gameObject:SetActiveIfNecessary(isCurOfficer)
    self.view.confirmOfficerBtn.gameObject:SetActiveIfNecessary(not isCurOfficer)

end

SettlementCharCtrl._RefreshCharCell = HL.Method(HL.Table, HL.Number, HL.Boolean) << function(self, cell, luaIndex, isInit)

    local charInfo = self.m_charInfoList[luaIndex]
    local isCurSelected = self.m_curSelectedCharIndex == luaIndex

    cell.gameObject.name = charInfo.templateId
    cell.charIconImg.spriteName = charInfo.charIcon
    cell.nameTxt.text = charInfo.charName

    local showTagCount = 1

    cell.blocTagTxt.text = charInfo.blocTagInfo.name
    local matchColor
    local normalColor
    if isCurSelected then
        matchColor = self.view.config.CHAR_TAG_MATCH_SELECT_COLOR
        normalColor = self.view.config.CHAR_TAG_NORMAL_SELECT_COLOR
    else
        matchColor = self.view.config.CHAR_TAG_MATCH_UNSELECT_COLOR
        normalColor = self.view.config.CHAR_TAG_NORMAL_UNSELECT_COLOR
    end
    cell.blocTagTxt.color = charInfo.blocTagInfo.isMatch and matchColor or normalColor

    local hobbyTagTexts = {
        [1] = cell.hobbyTag1Txt,
        [2] = cell.hobbyTag2Txt,
    }
    for i, txt in ipairs(hobbyTagTexts) do
        if i > #charInfo.hobbyTagInfoList then
            txt.gameObject:SetActiveIfNecessary(false)
        else
            local tagInfo = charInfo.hobbyTagInfoList[i]
            txt.gameObject:SetActiveIfNecessary(true)
            txt.text = tagInfo.name
            txt.color = tagInfo.isMatch and matchColor or normalColor
            showTagCount = showTagCount + 1
        end
    end

    local expertTagTexts = {
        [1] = cell.expertTag1Txt,
        [2] = cell.expertTag2Txt,
    }
    for i, txt in ipairs(expertTagTexts) do
        if i > #charInfo.expertTagInfoList then
            txt.gameObject:SetActiveIfNecessary(false)
        else
            local tagInfo = charInfo.expertTagInfoList[i]
            txt.gameObject:SetActiveIfNecessary(true)
            txt.text = tagInfo.name
            txt.color = tagInfo.isMatch and matchColor or normalColor
            showTagCount = showTagCount + 1
        end
    end

    cell.decoLineImg.gameObject:SetActive(showTagCount < 5)


    if string.isEmpty(charInfo.settledId) then
        cell.officerStateCtrl:SetState("NormalState")
    elseif charInfo.settledId == self.m_settlementId then
        cell.officerStateCtrl:SetState("CurSettleState")
        if not self.m_firstTimePlayCurSettleTagAni then
            self.m_firstTimePlayCurSettleTagAni = true
            cell.curSettleAniWrapper:Play("settlementcharyellownode_in")
        end
    else
        cell.officerStateCtrl:SetState("OtherSettleState")
        if not self.m_firstTimePlayOtherSettleTagAni then
            self.m_firstTimePlayOtherSettleTagAni = true
            cell.otherSettleAniWrapper:Play("settlementcharnormalnode_in")
        end
    end

    cell.selectedStateCtrl:SetState(isCurSelected and "SelectState" or "NormalState")

    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onClick:AddListener(function()
        self:_OnSelectCharChange(luaIndex)
    end)
    cell.btn.onIsNaviTargetChanged = function(isNaviTarget)
        if DeviceInfo.usingController and isNaviTarget then
            self:_OnSelectCharChange(luaIndex)
        end
    end
end

SettlementCharCtrl._RefreshFeatureCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)

    if self.m_curSelectedCharIndex <= 0 or self.m_curSelectedCharIndex > #self.m_charInfoList then
        logger.error("index out of range, index: " .. self.m_curSelectedCharIndex)
        return
    end
    local officerId = self.m_charInfoList[self.m_curSelectedCharIndex].templateId
    local wantTagInfo = self.m_stlWantTagInfoList[luaIndex]
    local isTagMatched = settlementSystem:IsCharMatchSettlementTag(officerId, wantTagInfo.stlTagId)

    cell.gameObject.name = wantTagInfo.stlTagId
    cell.titleTxt.text = wantTagInfo.tagName
    cell.lockStateCtrl:SetState(isTagMatched and "UnlockState" or "LockState")
    cell.contentTxt:SetAndResolveTextStyle(wantTagInfo.tagIncludeEnhanceDesc)
end




SettlementCharCtrl._OnSelectCharChange = HL.Method(HL.Number) << function(self, newIndex)
    if newIndex == self.m_curSelectedCharIndex then
        return
    end
    local oldIndex = self.m_curSelectedCharIndex
    local oldCell = self.m_getCharCellFunc(oldIndex)
    local newCell = self.m_getCharCellFunc(newIndex)
    self.m_curSelectedCharIndex = newIndex
    if oldCell then
        self:_RefreshCharCell(oldCell, oldIndex, false)
    end
    if newCell then
        self:_RefreshCharCell(newCell, newIndex, false)
    end
    self:_RefreshOfficerInfo()
    self:_RefreshFeatureInfo()
end

SettlementCharCtrl._OnClickConfirmOfficerBtn = HL.Method() << function(self)
    local charInfo = self.m_charInfoList[self.m_curSelectedCharIndex]
    local charId = charInfo.templateId
    local charData = Tables.characterTable[charId]
    local charSettledId = charInfo.settledId

    if charSettledId ~= nil then

        local settlementData = Tables.settlementBasicDataTable[charSettledId]
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(Language.LUA_SETTLEMENT_CHARACTER_SWITCH_CONFIRM, charData.name, settlementData.settlementName),
            charIcons = { charId },
            hideBlur = true,
            onConfirm = function()
                settlementSystem:SendSetOfficer(self.m_settlementId, charId)
            end })
    else
        settlementSystem:SendSetOfficer(self.m_settlementId, charId)
    end
end

SettlementCharCtrl._OnSettlementOfficerChange = HL.Method(HL.Any) << function(self, arg)
    local stlId, officerId = unpack(arg)
    if self.m_settlementId ~= stlId then
        return
    end
    if string.isEmpty(officerId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SETTLEMENT_REMOVE_CHARACTER_SUCC)
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SETTLEMENT_SWITCH_CHARACTER_SUCC)
    end
    self.m_curOfficerId = officerId
    Utils.triggerVoice("sim_assign_work", officerId)
    self.m_firstTimePlayCurSettleTagAni = false
    self.m_firstTimePlayOtherSettleTagAni = false
    for index, charInfo in pairs(self.m_charInfoList) do

        local newSettledId = settlementSystem:GetCharSettledId(charInfo.templateId)
        if charInfo.settledId ~= newSettledId then
            charInfo.settledId = newSettledId
            local cell = self.m_getCharCellFunc(index)
            if cell then
                self:_RefreshCharCell(cell, index, false)
            end
        end
    end
    self:_RefreshOfficerInfo()
end

SettlementCharCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end

    if resumeState.selectedCharId ~= nil then
        for luaIndex, charInfo in ipairs(self.m_charInfoList) do
            if charInfo.templateId == resumeState.selectedCharId then
                self.m_curSelectedCharIndex = luaIndex
                return
            end
        end
    end

    local selectedIndex = resumeState.selectedIndex
    if selectedIndex and selectedIndex > 0 and selectedIndex <= #self.m_charInfoList then
        self.m_curSelectedCharIndex = selectedIndex
    end
end

SettlementCharCtrl._WrapCharTagInfo = HL.Method(HL.String).Return(HL.Table) << function(self, charTagId)
    local matchStlTagInfo = self.m_stlWantCharTagIdSet[charTagId]
    local hasData, tagData = Tables.tagDataTable:TryGetValue(charTagId)
    local tagName = hasData and tagData.tagName or ("error tag:"..charTagId)
    local tagInfo = {
        id = charTagId,
        name = tagName,
        matchStlTagInfo = matchStlTagInfo,
        isMatch = matchStlTagInfo ~= nil,
    }
    return tagInfo
end

SettlementCharCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local curSelectedChar = self.m_charInfoList and self.m_charInfoList[self.m_curSelectedCharIndex] or nil
    return {
        settlementId = self.m_settlementId,
        resumeState = {
            selectedIndex = self.m_curSelectedCharIndex,
            selectedCharId = curSelectedChar and curSelectedChar.templateId or nil,
        }
    }
end


HL.Commit(SettlementCharCtrl)
