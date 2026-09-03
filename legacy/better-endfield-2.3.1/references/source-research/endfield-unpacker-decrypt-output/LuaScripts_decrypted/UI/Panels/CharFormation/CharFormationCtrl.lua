local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharFormation

local BTN_ANIM_NAME = {
    BTN_FORMATION_MIXCONFIRM_IN = "btn_formation_mixconfirm_in",
    BTN_FORMATION_MIXCONFIRM_OUT = "btn_formation_mixconfirm_out",
    BTN_FORMATION_REMOV_IN = "btn_formation_remov_in",
    BTN_FORMATION_REMOV_OUT = "btn_formation_remov_out",
    BTN_EMPTY_IN = "btn_empty_in",
    BTN_EMPTY_OUT = "btn_empty_out",
}

local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget
local CHAR_FORMATION_BLOCK_OBTAIN_WAYS_JUMP = "CharFormationBlockObtainWaysJump"

CharFormationCtrl = HL.Class('CharFormationCtrl', uiCtrl.UICtrl)









CharFormationCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHANGE_ACTIVE_SQUAD] = '_OnActiveSquadChange',
}

CharFormationCtrl.m_args = HL.Field(HL.Table)

CharFormationCtrl.m_teamCells = HL.Field(HL.Forward("UIListCache"))

CharFormationCtrl.m_curTeamIndex = HL.Field(HL.Number) << -1

CharFormationCtrl.m_teamSet = HL.Field(HL.Number) << -1

CharFormationCtrl.preState = HL.Field(HL.Number) << -1

CharFormationCtrl.state = HL.Field(HL.Number) << -1

CharFormationCtrl.singleState = HL.Field(HL.Number) << -1

CharFormationCtrl.m_index2Char = HL.Field(HL.Table)

CharFormationCtrl.m_singleCharIndex = HL.Field(HL.Number) << -1

CharFormationCtrl.m_singleCharInfo = HL.Field(HL.Table)

CharFormationCtrl.m_genStars = HL.Field(HL.Forward('UIListCache'))

CharFormationCtrl.m_charTagCellCache = HL.Field(HL.Forward('UIListCache'))

CharFormationCtrl.m_empty = HL.Field(HL.Boolean) << false

CharFormationCtrl.m_dungeonId = HL.Field(HL.Any)

CharFormationCtrl.m_subGameData = HL.Field(HL.Userdata)

CharFormationCtrl.m_enterDungeonCallback = HL.Field(HL.Function)

CharFormationCtrl.m_enterConfirmCallback = HL.Field(HL.Function)

CharFormationCtrl.m_onCloseCallback = HL.Field(HL.Function)


CharFormationCtrl.m_weekRaidArg = HL.Field(HL.Table)

CharFormationCtrl.m_curSelectTakeItemCount = HL.Field(HL.Number) << 0






CharFormationCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self:_ProcessArgs(args)
    self:_InitAction()
    self:_Init()
    self:_InitController()
    self.view.bannerMidNode.gameObject:SetActive(false)
end

CharFormationCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, args)
    self:_ProcessArgs(args)
    self:_Init()
end

CharFormationCtrl.OnShow = HL.Override() << function(self)
    AudioAdapter.PostEvent("au_ui_menu_formation_open")

    if self.m_singleCharInfo then
        self:RefreshCharInformation(self.m_singleCharInfo)
    end

    if self.view.charList.gameObject.activeSelf then
        self.view.charList.view.naviGroup:NaviToThisGroup()
    end

    if self.m_isCharInfoNaviGroupFocused then
        self.view.charInfoNaviGroup:ManuallyFocus(false)
    end
end

CharFormationCtrl.OnHide = HL.Override() << function(self)
    AudioAdapter.PostEvent("au_ui_menu_formation_close")

    if self.m_isCharInfoNaviGroupFocused then
        self.view.charInfoNaviGroup:ManuallyStopFocus(false)
        Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
        Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)
        Notify(MessageConst.CHAR_INFO_CLOSE_INFO_TIP)
        InputManagerInst:ToggleBinding(self.m_equipTacticalItemBindingId, false)
        self.m_isCharInfoNaviGroupFocused = true
    end
end

CharFormationCtrl.OnClose = HL.Override() << function(self)
    AudioAdapter.PostEvent("au_ui_menu_formation_close")
    AudioAdapter.PostEvent("Au_UI_Menu_CharFormationPanel_Close")

    Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })

    if not string.isEmpty(self.m_dungeonId) or self.m_weekRaidArg then
        UIManager:ToggleBlockObtainWaysJump(CHAR_FORMATION_BLOCK_OBTAIN_WAYS_JUMP, false)
    end

    if self.m_onCloseCallback and not InputManagerInst.inChangingInputDevice then
        self.m_onCloseCallback()
        self.m_onCloseCallback = nil
    end
end





CharFormationCtrl._OnChangeTeamNameClicked = HL.Method() << function(self)
    local team = GameInstance.player.charBag.teamList[CSIndex(self.m_curTeamIndex)]
    local name = team.name
    if string.isEmpty(name) then
        name = Language[string.format("LUA_TEAM_NUM_%d", self.m_curTeamIndex)]
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_CHANGE_TEAM_NAME,
        input = true,
        checkInputValid = true,
        inputName = name,
        closeOnConfirm = false,
        onConfirm = function(changedName)
            self:_OnConfirmChangeTeamName(changedName)
        end
    })
end

CharFormationCtrl._OnConfirmChangeTeamName = HL.Method(HL.String) << function(self, changedName)
    if I18nUtils.GetTextRealLength(changedName) > UIConst.INPUT_FIELD_CHARACTER_LIMIT then
        Notify(MessageConst.SHOW_TOAST, Language.ui_char_common_popup_input_upper_limit)
        return
    end
    GameInstance.player.charBag:SetTeamName(CSIndex(self.m_curTeamIndex), changedName)
end

CharFormationCtrl._OnConfirmCharNotFullEnterDungeon = HL.Method() << function(self)
    local charInfos = {}
    for _, charInfo in ipairs(self.m_lockedTeamData.chars) do
        table.insert(charInfos, CharInfoUtils.getPlayerCharInfoByInstId(charInfo.charInstId))
    end
    self:_EnterDungeon(self.m_dungeonId, charInfos)
end

CharFormationCtrl._OnCharInfoClicked = HL.Method(HL.Table) << function(self, charInfo)

    CharInfoUtils.openCharInfoBestWay({
        initCharInfoCreator = function(initCharTemplateId)
            local initCharInfo
            local _, ctrl = UIManager:IsOpen(PANEL_ID)
            if not ctrl then
                return nil
            end
            initCharInfo = ctrl.m_singleCharInfo
            local isShowFixed, isShowTrail = CharInfoUtils.getLockedFormationCharTipsShow(initCharInfo)
            return {
                instId = initCharInfo.charInstId,
                templateId = initCharInfo.charId,
                isSingleChar = true,
                isTrail = true,
                isShowFixed = isShowFixed,
                isShowTrail = isShowTrail,
            }
        end,
        forceSkipIn = true,
    })
end

CharFormationCtrl._OnBtnMixConfirmClicked = HL.Method() << function(self)
    self:Notify(MessageConst.ON_CHAR_FORMATION_LIST_CONFIRM, self.m_curTeamIndex)
end

CharFormationCtrl._OnBtnConfirmClicked = HL.Method() << function(self)
    if Utils.isCurSquadAllDead() then

        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH)
        return
    end

    if self.m_phase then
        local success, toast = self.m_phase:_CheckTeamCanFight()
        if not success then
            Notify(MessageConst.SHOW_TOAST, toast)
            return
        end
    end

    if self.state == UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet and self.m_customTeamIndex < 0 then
        self:Notify(MessageConst.ON_CHAR_FORMATION_TEAM_SET)
    end

    if self.m_dungeonId then
        local specialTeamType = DungeonUtils.getSpecialTeamType(self.m_dungeonId)
        if self.m_isFormationLocked and not specialTeamType then
            local charInfos = {}
            for _, charInfo in ipairs(self.m_lockedTeamData.chars) do
                table.insert(charInfos, CharInfoUtils.getPlayerCharInfoByInstId(charInfo.charInstId))
            end
            local selectedCharCount = #charInfos

            if selectedCharCount == 0 then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_TEAM_FORMATION_EMPTY_TIPS)
                return
            end

            local showConfirmTips = false
            if selectedCharCount < self.m_lockedTeamData.maxTeamMemberCount then
                local _, selectableCharCount = CharInfoUtils.getCharInfoListWithLockedTeamData(self.m_lockedTeamData)
                if selectableCharCount > selectedCharCount then
                    showConfirmTips = true
                end
            end

            if showConfirmTips then
                self:Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_TEAM_FORMATION_CHAR_NOT_FULL_TIPS,
                    onConfirm = function()
                        self:_OnConfirmCharNotFullEnterDungeon()
                    end
                })
            else
                self:_EnterDungeon(self.m_dungeonId, charInfos)
            end
        else
            self:_EnterDungeon(self.m_dungeonId)
        end
    end

    if self.m_weekRaidArg then
        local gameId = string.isEmpty(GameInstance.player.weekRaidSystem.guideGameId) and GameInstance.player.weekRaidSystem.gameId or GameInstance.player.weekRaidSystem.guideGameId
        if GameInstance.player.weekRaidSystem:ReqStartWeekRaid(gameId,
                                                               Tables.weekRaidConst.takeItemId,
                                                               self.m_curSelectTakeItemCount) then


            GameWorld.dialogManager:Next(0)
            if self.m_enterDungeonCallback then
                self.m_enterDungeonCallback()
            end
        end
    end

    if self.m_dungeonId then
        AudioAdapter.PostEvent("au_ui_btn_start_dungeon")
    else

        AudioAdapter.PostEvent("au_ui_g_confirm_button")
    end

    if self.m_args.startBtnCallback then
        self.m_args.startBtnCallback()
    end
end





CharFormationCtrl.InitSelectTeam = HL.Method(HL.Opt(HL.Number)) << function(self, selectedTeamIndex)
    self.m_teamSet = self:_GetCurTeamIndex()
    local index = self.m_teamSet
    if selectedTeamIndex then
        index = selectedTeamIndex
    end
    self:_SetTeamSelect(index)


    self.view.infoNoe.gameObject:SetActive(not self:_IsSpecialFormation())
end

CharFormationCtrl.UpdateTeamSet = HL.Method() << function(self)
    self.m_teamSet = LuaIndex(GameInstance.player.charBag.curTeamIndex)
    if self.m_curTeamIndex == self.m_teamSet then
        self:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet)
    else
        self:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet)
    end
end

CharFormationCtrl.SetSingleCharIndex = HL.Method(HL.Number) << function(self, index)
    self.m_singleCharIndex = index
    local charInfo = self.m_index2Char[index]
    self:RefreshCharInformation(charInfo)
end

CharFormationCtrl.RefreshTeamCharInfo = HL.Method(HL.Table) << function(self, team)
    local index = 1
    if team ~= nil then
        for _, teamChar in pairs(team.slots) do
            self:UpdateChar(index, teamChar)
            index = index + 1
        end
    end

    for i = index, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        self:UpdateChar(i, nil)
    end
    self:_UpdateSlotConfirmBinding()
end

CharFormationCtrl.UpdateChar = HL.Method(HL.Number, HL.Table) << function(self, index, charInfo)
    self:Notify(MessageConst.ON_CHAR_FORMATION_REFRESH_SLOT, { index, charInfo })
    self.m_index2Char[index] = charInfo
end

CharFormationCtrl.SetState = HL.Method(HL.Number) << function(self, state)
    local needRefresh = self.state ~= state

    local isTeamFullLocked = self.m_lockedTeamData and
                             not self.m_lockedTeamData.hasReplaceable and
                             self.m_lockedTeamData.lockedTeamMemberCount == self.m_lockedTeamData.maxTeamMemberCount
    local showFormation = state == UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet or
                          state == UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet
    self.view.btnFormation.gameObject:SetActive(showFormation and not isTeamFullLocked)

    local showMixConfirm = state == UIConst.UI_CHAR_FORMATION_STATE.CharChange
    self.view.btnMixConfirm.gameObject:SetActive(showMixConfirm)


    local isDungeon = self.m_dungeonId or self.m_weekRaidArg
    self.view.btnMixInFight.gameObject:SetActive(showMixConfirm and self.m_curTeamIndex ~= self.m_teamSet and not isDungeon)


    if self.m_dungeonId then
        self.view.btnConfirm.text = DungeonUtils.getEntryText(self.m_dungeonId)
        self.view.btnConfirm.interactable = true
    elseif self.m_weekRaidArg then
        self.view.btnConfirm.text = Language.LUA_CHAR_FORMATION_ENTER_DUNGEON
        self.view.btnConfirm.interactable = true
    else
        if self.m_curTeamIndex ~= self.m_teamSet then
            self.view.btnConfirm.text = Language.LUA_CHAR_FORMATION_TEAM_CONFIRM
            self.view.btnConfirm.interactable = true
        else
            self.view.btnConfirm.text = Language.LUA_CHAR_FORMATION_TEAM_CONFIRMED
            self.view.btnConfirm.interactable = false
        end
    end
    self.view.btnConfirm.gameObject:SetActive(showFormation)

    if self.m_args.startBtnCallback then
        self.view.btnConfirm.interactable = true
    end
    if self.m_args.startBtnText then
        self.view.btnConfirm.text = self.m_args.startBtnText
    end


    local showSingle = state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar
    self.view.btnSoloConfirm.gameObject:SetActive(showSingle)
    local singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.None
    if needRefresh then
        if showSingle then
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.Current
            if self.m_isFormationLocked and self.m_singleCharIndex <= self.m_lockedTeamData.lockedTeamMemberCount then
                singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.CurrentLocked
            end
        else
            singleState = UIConst.UI_CHAR_FORMATION_SINGLE_STATE.None
        end
    end

    self:_PlayBtnMultiAnim(state)
    self:RefreshSingleBtns(singleState, self.m_singleCharInfo)

    local leftRightVisible = state == UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet or state == UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet
    self:_RefreshLeftRightBtns(leftRightVisible and not self:_IsSpecialFormation())


    local back = state == UIConst.UI_CHAR_FORMATION_STATE.CharChange or state == UIConst.UI_CHAR_FORMATION_STATE.SingleChar
    self.view.btnClose.gameObject:SetActive(not back)
    self.view.btnBack.gameObject:SetActive(back)


    self.view.btnBackTouch.gameObject:SetActive(showMixConfirm)

    local showCustomNode = state == UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet or
                           state == UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet
    self.view.customNode.gameObject:SetActive(showCustomNode)

    self.preState = self.state
    self.state = state
end

CharFormationCtrl.RefreshSingleBtns = HL.Method(HL.Number, HL.Table) << function(self, singleState, charInfo)
    self:_PlayBtnSingleAnim(singleState)

    self.singleState = singleState

    if singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.None then
        self.view.btnSoloConfirm.gameObject:SetActive(false)
        self.view.btnRemove.gameObject:SetActive(false)
        self.view.btnCannotReplace.gameObject:SetActive(false)
        return
    end

    local showRemove = singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.Current
    local otherDead = singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherDead
    local currentLocked = singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.CurrentLocked
    local interactable = singleState ~= UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherInTeam and
                         singleState ~= UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherInTeamLocked and
                         singleState ~= UIConst.UI_CHAR_FORMATION_SINGLE_STATE.OtherUnavailable and
                         not otherDead
    local text
    if otherDead then
        text = Language.ui_bat_action_exit
    elseif interactable then
        text = Language.LUA_CHAR_FORMATION_SINGLE_CONFIRM
    else
        text = Language.LUA_CHAR_FORMATION_SINGLE_CONFIRMED
    end
    self.view.btnSoloConfirm.interactable = interactable
    self.view.btnSoloConfirm.text = text
    self.view.btnSoloConfirm.gameObject:SetActive(not showRemove and not currentLocked)
    self.view.btnRemove.gameObject:SetActive(showRemove)
    self.view.btnCannotReplace.gameObject:SetActive(currentLocked)
    if currentLocked then
        local isFixed = charInfo and charInfo.isLocked and not charInfo.isReplaceable
        self.view.txtCannotReplace.text = isFixed and Language.LUA_TEAM_FORMATION_CHAR_CANNOT_CHANGE or Language.LUA_TEAM_FORMATION_CHAR_CANNOT_LEAVE
    end
end

CharFormationCtrl.RefreshEmpty = HL.Method(HL.Boolean) << function(self, empty)
    if self.m_empty == empty then
        return
    end

    self.view.btnFomationNode.gameObject:SetActive(not empty)
    self.view.btnMixConfirmNode.gameObject:SetActive(not empty)
    self.view.btnRemovNode.gameObject:SetActive(not empty)
    self.view.btnNode:ClearTween()
    if empty then
        self.view.emptyNode.gameObject:SetActive(empty)
        self.view.btnNode:PlayWithTween(BTN_ANIM_NAME.BTN_EMPTY_IN)
    else
        self.view.btnNode:PlayWithTween(BTN_ANIM_NAME.BTN_EMPTY_OUT, function()
            self.view.emptyNode.gameObject:SetActive(empty)
        end)
    end

    self.m_empty = empty
end

CharFormationCtrl.RefreshTeamName = HL.Method(HL.Opt(HL.String)) << function(self, name)
    if self:_IsSpecialFormation() then
        return
    end
    if string.isEmpty(name) then
        local team = GameInstance.player.charBag.teamList[CSIndex(self.m_curTeamIndex)]
        name = team.name
    end

    if string.isEmpty(name) then
        name = Language[string.format("LUA_TEAM_NUM_%d", self.m_curTeamIndex)]
    end

    self.view.textName.text = name
end

CharFormationCtrl.RefreshCharInformation = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, charInfo, selectTable)
    local needRefresh = false
    if self.m_singleCharInfo and charInfo and self.m_singleCharInfo.charInstId ~= charInfo.charInstId then
        needRefresh = true
    end

    self.m_singleCharInfo = charInfo
    if charInfo then
        self:_SetSlotCharInfo(self.view.charInformation, charInfo)

        self.view.charInformation.charSkillNode:InitCharSkillNodeNew({
            charInstId = charInfo.charInstId,
            isSingleChar = true,
            hideBtnUpgrade = true,
            tipsNode = self.view.charInformation.skillTipsNode,
            tipPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        })
        self.view.charInformation.charPassiveSkillNode:InitCharPassiveSkillNode({
            charInstId = charInfo.charInstId,
            isSingleChar = true,
            hideBtnUpgrade = true,
            tipsNode = self.view.charInformation.passiveSkillTipsNode,
            tipPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        })


        local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInfo.charInstId)
        local dungeonId = self.m_dungeonId


        local tacticalItemArgs = {
            itemId = charInst.tacticalItemId,
            isLocked = charInfo.isTrail,
            isForbidden = not string.isEmpty(dungeonId) and
                UIUtils.isItemForbidden(dungeonId, charInst.tacticalItemId),
            isClickable = true,
            charTemplateId = charInfo.charId,
            charInstId = charInfo.charInstId,
            tipNode = self.view.charInformation.tacticalItemTipsNode,
            tipPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        }
        self.view.charInformation.charFormationTacticalItem:InitCharFormationTacticalItem(tacticalItemArgs)
        local canChangeItem = self.view.charInformation.charFormationTacticalItem:CanChangeItem()
        InputManagerInst:ToggleBinding(self.m_equipTacticalItemBindingId, canChangeItem)

        self.view.charInformation.gameObject:SetActive(true)
        if needRefresh then
            self.view.charInformation.animationWrapper:PlayInAnimation()
        end
        self.view.charInfoNaviGroup.getDefaultSelectableFunc = function()
            return self.view.charInformation.charSkillNode.m_skillCells:GetItem(1).view.button
        end
    else
        if self.view.charInformation.gameObject.activeSelf then
            self.view.charInformation.animationWrapper:PlayOutAnimation(function()
                self.view.charInformation.gameObject:SetActive(false)
            end)
        end
        InputManagerInst:ToggleBinding(self.m_equipTacticalItemBindingId, false)
    end

    local starNum = 0
    local tagNum = 0
    local charCfg
    if charInfo then
        local characterTable = Tables.characterTable
        charCfg = characterTable:GetValue(charInfo.charId)
        starNum = charCfg.rarity
        tagNum = charCfg.charBattleTagIds and #charCfg.charBattleTagIds or 0
    end
    self.m_genStars:Refresh(starNum)
    self.m_charTagCellCache:Refresh(tagNum, function(cell, index)
        local _, tagName = Tables.charBattleTagTable:TryGetValue(charCfg.charBattleTagIds[CSIndex(index)])
        if tagName then
            cell.tagTxt.text = tagName
        end
    end)
end





CharFormationCtrl._GetCurTeamIndex = HL.Method().Return(HL.Number) << function(self)
    if self.m_customTeamIndex > 0 then
        return self.m_customTeamIndex
    end
    return LuaIndex(GameInstance.player.charBag.curTeamIndex)
end

CharFormationCtrl._ProcessArgs = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self.m_customTeamIndex = args.customTeamIndex or -1
    self.m_dungeonId = args.dungeonId
    self.m_weekRaidArg = args.weekRaidArg
    self.m_isFormationLocked = args.lockedTeamData ~= nil
    self.m_lockedTeamData = args.lockedTeamData
    self.m_enterDungeonCallback = args.enterDungeonCallback
    self.m_enterConfirmCallback = args.enterConfirmCallback
    self.m_onCloseCallback = args.onCloseCallback
    self.m_index2Char = {}
    self.m_teamSet = self:_GetCurTeamIndex()
    if not string.isEmpty(self.m_dungeonId) then
        local _, subGameData = DataManager.subGameInstDataTable:TryGetValue(self.m_dungeonId )
        self.m_subGameData = subGameData
    else
        self.m_subGameData = nil
    end
end

CharFormationCtrl._Init = HL.Method() << function(self)
    if self.m_dungeonId then
        local _, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_dungeonId)
        if dungeonCfg then
            if not string.isEmpty(dungeonCfg.dungeonName) then
                self.view.titleTxt.text = string.format("//%s", dungeonCfg.dungeonName)
            end
            local featureInfos = DungeonUtils.getListByStr(dungeonCfg.featureDesc)
            local hasFeature = #featureInfos > 0
            self.view.dungeonInfoBtn.gameObject:SetActive(hasFeature)
        else
            self.view.dungeonInfoBtn.gameObject:SetActive(false)
            self.view.titleTxt.gameObject:SetActive(false)
        end
        if self.m_subGameData then
            local iconId = DungeonConst.GameMechanicsTypeToFormationTitleIcon[self.m_subGameData.gameMechanicsType]
            if not string.isEmpty(iconId) then
                self.view.titleIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_FORMATION_ICON, iconId)
            end
        end
    else
        self.view.dungeonInfoBtn.gameObject:SetActive(false)
    end

    self.view.infoBtn.gameObject:SetActive(self.m_args.infoBtnCallback ~= nil)
    if self.m_args.infoBtnCallback ~= nil then
        self.view.infoBtn.onClick:AddListener(self.m_args.infoBtnCallback)
    end

    if not string.isEmpty(self.m_args.customTitle) then
        self.view.titleTxt.text = string.format("//%s", self.m_args.customTitle)
    end

    if self.m_args.infoBtnCallback then
        self.view.dungeonInfoBtn.gameObject:SetActive(true)
        self.view.dungeonInfoBtn.onClick:RemoveAllListeners()
        self.view.dungeonInfoBtn.onClick:AddListener(self.m_args.infoBtnCallback)
    end
    if self.m_args.closeInfoBtn then
        self.view.dungeonInfoBtn.gameObject:SetActive(false)
    end

    self.m_teamCells = self.m_teamCells or UIUtils.genCellCache(self.view.team)
    local totalSquadNum = Tables.globalConst.totalSquadNum
    self.m_teamCells:Refresh(totalSquadNum, function(cell, luaIndex)
        local data = {
            index = luaIndex
        }
        cell:InitTeamCell(data, function()
            self:_SetTeamSelect(luaIndex)
        end)
        cell.gameObject:SetActive(true)
    end)

    self:BindInputPlayerAction("common_open_team_panel", function()
        PhaseManager:PopPhase(PhaseId.CharFormation)
    end)

    self.m_genStars = self.m_genStars or UIUtils.genCellCache(self.view.charInformation.starIcon)
    self.m_charTagCellCache = self.m_charTagCellCache or UIUtils.genCellCache(self.view.charInformation.tagCell)

    self.view.charInformation.gameObject:SetActive(false)
    self.view.emptyNode.gameObject:SetActive(false)
    self.view.btnCannotReplace.gameObject:SetActive(false)
    self.view.trialOperators.gameObject:SetActive(self.m_lockedTeamData and self.m_lockedTeamData.shouldShowTrailTips)

    if not string.isEmpty(self.m_dungeonId) or self.m_weekRaidArg or self:_IsSpecialFormation() then
        UIManager:ToggleBlockObtainWaysJump(CHAR_FORMATION_BLOCK_OBTAIN_WAYS_JUMP, true)
    end

    self.view.customNode.gameObject:SetAllChildrenActiveIfNecessary(false)
    self:_UpdateWeekRaid()
    self:_UpdateContingencyContract()
end

CharFormationCtrl._InitAction = HL.Method() << function(self)

    self.view.btnClose.onClick:AddListener(function()
        self.m_phase:OnCommonBackClicked()
    end)

    self.view.btnBack.onClick:AddListener(function()
        self.m_phase:OnCommonBackClicked()
    end)


    self.view.btnRename.onClick:AddListener(function()
        self:_OnChangeTeamNameClicked()
    end)


    self.view.btnFormation.onClick:AddListener(function()
        self:_OnEnterMultiSelect()
    end)


    self.view.btnMixConfirm.onClick:AddListener(function()
        self:_OnBtnMixConfirmClicked()
    end)


    self.view.btnMixInFight.onClick:AddListener(function()
        self.m_phase:SetMemberAndActiveTeam()
    end)


    self.view.btnConfirm.onClick:AddListener(function()
        self:_OnBtnConfirmClicked()
    end)


    self.view.btnSoloConfirm.onClick:AddListener(function()
        self:Notify(MessageConst.ON_CHAR_FORMATION_CONFIRM_SINGLE_CHAR, self.m_curTeamIndex)
    end)


    self.view.btnRemove.onClick:AddListener(function()
        self:Notify(MessageConst.ON_CHAR_FORMATION_UNEQUIP_INDEX)
    end)

    self.view.buttonRight.onClick:AddListener(function()
        self:_SetTeamSelect(self:_WrapTeamIndex(self.m_curTeamIndex + 1))
    end)

    self.view.buttonLeft.onClick:AddListener(function()
        self:_SetTeamSelect(self:_WrapTeamIndex(self.m_curTeamIndex - 1))
    end)

    self.view.charInformation.btnCultivation.onClick:AddListener(function()
        self:_OnCharInfoClicked(self.m_singleCharInfo)
    end)

    self.view.dungeonInfoBtn.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.DungeonInfoPopup, { dungeonId = self.m_dungeonId })
    end)
end

CharFormationCtrl._SetTeamSelect = HL.Method(HL.Number) << function(self, index)
    if self.m_curTeamIndex ~= index then
        local oldCell = self.m_teamCells:GetItem(self.m_curTeamIndex)
        if oldCell then
            oldCell:SetSelect(false)
        end
        self.m_curTeamIndex = index
        local newCell = self.m_teamCells:GetItem(self.m_curTeamIndex)
        if newCell then
            newCell:SetSelect(true)
        end
        self:Notify(MessageConst.ON_CHAR_FORMATION_SELECT_TEAM_CHANGE, self.m_curTeamIndex)
    end

    if self.m_curTeamIndex == self.m_teamSet then
        self:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamHasSet)
    else
        self:SetState(UIConst.UI_CHAR_FORMATION_STATE.TeamWaitSet)
    end

    self:RefreshTeamName()
end

CharFormationCtrl._WrapTeamIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local totalSquadNum = Tables.globalConst.totalSquadNum
    if index > totalSquadNum then
        index = index - totalSquadNum
    elseif index <= 0 then
        index = index + totalSquadNum
    end
    return index
end

CharFormationCtrl._SetSlotCharInfo = HL.Method(HL.Table, HL.Table) << function(self, slot, info)
    local characterTable = Tables.characterTable
    local data = characterTable:GetValue(info.charId)
    local instId = info.charInstId
    local charInfo = nil

    if instId and instId > 0 then
        charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    end

    if charInfo then
        slot.textLv.text = string.format("%02d", charInfo.level)
    end

    slot.textName.text = data.name

    slot.charElementIcon:InitCharTypeIcon(data.charTypeId)

    local proSpriteName = CharInfoUtils.getCharProfessionIconName(data.profession, true)
    slot.imagePro:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)

    local isFixed, isTrail = CharInfoUtils.getLockedFormationCharTipsShow(info)
    slot.tryoutTips.gameObject:SetActive(isTrail)
    slot.fixedTips.gameObject:SetActive(isFixed)
end

CharFormationCtrl._PlayBtnSingleAnim = HL.Method(HL.Number) << function(self, singleState)
    if singleState == self.singleState then
        return
    end

    local name

    local charInfo = self.m_index2Char[self.m_singleCharIndex]

    if self.singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.None then

        if singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.Current and charInfo then
            name = BTN_ANIM_NAME.BTN_FORMATION_REMOV_IN

        else
            name = BTN_ANIM_NAME.BTN_EMPTY_IN
        end
    else
        if singleState == UIConst.UI_CHAR_FORMATION_SINGLE_STATE.Current then
            name = BTN_ANIM_NAME.BTN_FORMATION_REMOV_IN
        else
            name = BTN_ANIM_NAME.BTN_EMPTY_OUT
        end
    end

    if not string.isEmpty(name) then
        self.view.btnNode:PlayWithTween(name)
    end

end

CharFormationCtrl._RefreshLeftRightBtns = HL.Method(HL.Boolean) << function(self, visible)
    self.view.midNodeAnim:ClearTween(false)
    if visible then
        self.view.buttonLeft.gameObject:SetActive(true)
        self.view.buttonRight.gameObject:SetActive(true)
        self.view.buttonLeft.interactable = true
        self.view.buttonRight.interactable = true
        self.view.midNodeAnim:PlayInAnimation()
    else
        self.view.buttonLeft.interactable = false
        self.view.buttonRight.interactable = false
        self.view.midNodeAnim:PlayOutAnimation(function()
            self.view.buttonLeft.gameObject:SetActive(false)
            self.view.buttonRight.gameObject:SetActive(false)
        end)
    end

end

CharFormationCtrl._PlayBtnMultiAnim = HL.Method(HL.Number) << function(self, state)
    if state == self.state then
        return
    end

    local name


    if state == UIConst.UI_CHAR_FORMATION_STATE.CharChange then
        name = BTN_ANIM_NAME.BTN_FORMATION_MIXCONFIRM_IN

    elseif self.state == UIConst.UI_CHAR_FORMATION_STATE.CharChange then
        name = BTN_ANIM_NAME.BTN_FORMATION_MIXCONFIRM_OUT
    end

    if not string.isEmpty(name) then
        self.view.btnNode:PlayWithTween(name)
    end
end

CharFormationCtrl._OnActiveSquadChange = HL.Method(HL.Table) << function(self, args)
    local curTeamIndex = unpack(args)
    self:UpdateTeamSet()
    if not string.isEmpty(self.m_dungeonId) or self.m_weekRaidArg then
        return
    end
    self:_StartCoroutine(function()
        self.view.bannerMidNode.gameObject:SetActive(true)
        self.view.bannerMidNode.numTxt.text = string.format("0%d", LuaIndex(curTeamIndex))
        coroutine.wait(self.view.config.SQUAD_CHANGED_TIPS_TIME)
        self.view.bannerMidNode.gameObject:SetActive(false)
    end)
    FilterUtils.setCharInstIdIndexDirty()
end

CharFormationCtrl._EnterDungeon = HL.Method(HL.String, HL.Opt(HL.Table)) << function(self, dungeonId, charInfos)
    local dungeonMrg = GameInstance.dungeonManager
    local entered = dungeonMrg:CheckCanEnterDungeonAndToast(dungeonId)
    if not entered then
        return
    end

    if charInfos then
        for _, charInfo in ipairs(charInfos) do
            if charInfo and charInfo.isDead then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_FORMATION_DEAD_TOAST)
                return
            end
        end
    end

    if self.m_enterConfirmCallback and self.m_enterConfirmCallback(dungeonId) == false then
        return
    end

    GameWorld.dialogManager:Next(0)
    if charInfos then
        GameInstance.dungeonManager:TryReqEnterDungeon(dungeonId, charInfos, self.m_args.presetTeamId or "")
    else
       GameInstance.dungeonManager:TryReqEnterDungeon(dungeonId, self.m_args.presetTeamId or "")
    end
    if self.m_enterDungeonCallback then
        self.m_enterDungeonCallback(dungeonId)
    end
    Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.DungeonBattleFirst)
end





CharFormationCtrl.OpenCharList = HL.Method(HL.Number, HL.Opt(HL.Int)) << function(self, mode, charInstId)
    if self.state == UIConst.UI_CHAR_FORMATION_STATE.CharChange then
        return
    end

    self.m_charListSingleCharInstId = charInstId or self.m_charListSingleCharInstId
    self.m_charListMode = mode

    local maxCharTeamMemberCount = self.m_lockedTeamData and
                                   self.m_lockedTeamData.maxTeamMemberCount or
                                   GameInstance.player.charBag.maxCharTeamMemberCount

    local info = {
        selectNum = math.min(Const.BATTLE_SQUAD_MAX_CHAR_NUM, maxCharTeamMemberCount),
        mode = self.m_charListMode,
        selectedCharInfo = self.m_singleCharInfo,
        lockedTeamData = self.m_lockedTeamData,
    }
    UIUtils.PlayAnimationAndToggleActive(self.view.charList.view.animationWrapper, true)
    self.view.charList:InitCharFormationList(info, function(charList)
        self.m_tmpCharItems = charList
    end)
    self.view.charList:SetUpdateCellFunc(nil, function(select, cellIndex, charItem, charItemList, charInfoList)
        self:_CharListChangeSelectIndex(select, cellIndex, charItem, charItemList, charInfoList)
    end)
    self:_RefreshCharList()
    self.view.charList:ShowSelectChars(self:_GetShowSelectChars())
    self:_ActiveTeamInfo(false)
end

CharFormationCtrl.CloseCharList = HL.Method() << function(self)

    Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })
    UIUtils.PlayAnimationAndToggleActive(self.view.charList.view.animationWrapper, false)
    self:_ActiveTeamInfo(true)
    InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.charList.view.naviGroup)
end

CharFormationCtrl._ActiveTeamInfo = HL.Method(HL.Boolean) << function(self, active)
    self.view.charTittleNode.gameObject:SetActive(active)
    self.view.infoNoe.gameObject:SetActive(active and not self:_IsSpecialFormation())
end

CharFormationCtrl.SetCharListMode = HL.Method(HL.Number, HL.Opt(HL.Number)) << function(self, mode, charInstId)
    self.view.charList:SetMode(mode, charInstId)
    self.m_charListMode = mode
end

CharFormationCtrl.GetCharListEmpty = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.charList:GetEmpty()
end

CharFormationCtrl.GetCurCharList = HL.Method().Return(HL.Table) << function(self)

    local charList = {}
    local index2Id = {}
    for cellIndex, index in pairs(self.view.charList.cell2Select) do
        index2Id[index] = self.m_tmpCharItems[cellIndex]
    end
    local realIndex = 1
    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        if index2Id[i] then
            charList[realIndex] = index2Id[i]
            realIndex = realIndex + 1
        end
    end
    return charList
end

CharFormationCtrl.GetCharListItemByInstId = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) << function(self, charInstId)
    for _, charItem in ipairs(self.m_tmpCharItems or {}) do
        if charItem.instId == charInstId then
            return charItem
        end
    end
    return nil
end

CharFormationCtrl.RecoverMultiCharListSelection = HL.Method(HL.Table).Return(HL.Table, HL.Table) << function(self, charInstIds)
    local selectedCharItems = {}
    for _, charInstId in ipairs(charInstIds) do
        local charItem = self:GetCharListItemByInstId(charInstId)
        if charItem then
            table.insert(selectedCharItems, charItem)
        end
    end
    self.view.charList:ShowSelectChars(selectedCharItems, false)

    local charItemList = self:GetCurCharList()
    local charInfoList = {}
    for _, charItem in ipairs(charItemList) do
        table.insert(charInfoList, {
            charId = charItem.templateId,
            charInstId = charItem.instId,
            isLocked = charItem.isLocked,
            isTrail = charItem.isTrail,
            isReplaceable = charItem.isReplaceable,
        })
    end
    return charItemList, charInfoList
end

CharFormationCtrl.CollectCharListSortFilterState = HL.Method().Return(HL.Table) << function(self)
    local recoverState = {}
    local charListView = self.view and self.view.charList and self.view.charList.view or nil
    local sortNode = charListView and charListView.sortNode or nil
    if sortNode then
        recoverState.sortSelectedIndex = sortNode:GetCurSelectedIndex()
        recoverState.sortIsIncremental = sortNode.isIncremental
        if sortNode.m_filterBtn and sortNode.m_filterBtn.m_args then
            recoverState.filterTags = lume.deepCopy(sortNode.m_filterBtn.m_args.selectedTags)
        end
    end
    return recoverState
end

CharFormationCtrl.ApplyCharListSortFilterState = HL.Method(HL.Table) << function(self, recoverState)
    if not recoverState then
        return
    end
    local charListView = self.view and self.view.charList and self.view.charList.view or nil
    local sortNode = charListView and charListView.sortNode or nil
    if not sortNode then
        return
    end

    local sortSelectedIndex = recoverState.sortSelectedIndex
    if sortSelectedIndex and sortNode.m_sortOptions and sortNode.view and sortNode.view.mobilePCNode and sortNode.view.mobilePCNode.dropDown then
        local optionCount = #sortNode.m_sortOptions
        if optionCount > 0 then
            sortSelectedIndex = math.max(1, math.min(sortSelectedIndex, optionCount))
            sortNode.isIncremental = recoverState.sortIsIncremental == true
            sortNode:RefreshIncremental()
            sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(sortSelectedIndex), true, false)
        end
    end

    local filterTags = recoverState.filterTags and lume.deepCopy(recoverState.filterTags) or {}
    if sortNode.m_filterBtn and sortNode.m_filterBtn.m_args then
        sortNode.m_filterBtn.m_args.selectedTags = lume.deepCopy(filterTags)
        if sortNode.m_filterBtn.m_args.onConfirm then
            sortNode.m_filterBtn.m_args.onConfirm(filterTags)
        else
            sortNode:OnSortChanged()
        end
    else
        sortNode:OnSortChanged()
    end
    sortNode:UpdateDeviceState()
end

CharFormationCtrl.m_charListMode = HL.Field(HL.Number) << 0

CharFormationCtrl.m_charListSingleCharInstId = HL.Field(HL.Int) << 0

CharFormationCtrl.m_tmpCharItems = HL.Field(HL.Table)

CharFormationCtrl._OnEnterMultiSelect = HL.Method() << function(self)
    self:OpenCharList(UIConst.CharListMode.MultiSelect)
    self:SetState(UIConst.UI_CHAR_FORMATION_STATE.CharChange)
    self:Notify(MessageConst.ON_CHAR_FORMATION_ENTER_MULTI_SELECT)
end

CharFormationCtrl._RefreshCharList = HL.Method() << function(self)
    if self.m_lockedTeamData then
        self.m_tmpCharItems = CharInfoUtils.getCharInfoListWithLockedTeamData(self.m_lockedTeamData)
    else
        self.m_tmpCharItems = CharInfoUtils.getCharInfoList(CSIndex(self.m_curTeamIndex), true)
    end
    self.view.charList:UpdateCharItems(self.m_tmpCharItems)
end

CharFormationCtrl._CharListChangeSelectIndex = HL.Method(HL.Boolean, HL.Number, HL.Table, HL.Table, HL.Table) << function
(self, select, cellIndex, charItem, charItemList, charInfoList)
    if self.m_charListMode == UIConst.UIConst.CharListMode.MultiSelect then
        self:Notify(MessageConst.ON_CHAR_FORMATION_LIST_MULTI_SELECT, { charItemList, charInfoList })
    else
        self:Notify(MessageConst.ON_CHAR_FORMATION_LIST_SINGLE_SELECT, { cellIndex, charItem })
    end
end

CharFormationCtrl._GetShowSelectChars = HL.Method().Return(HL.Table) << function(self)
    local showSelectChars = {}
    for i = 1, #self.m_tmpCharItems do
        local charItem = self.m_tmpCharItems[i]
        if charItem.slotIndex and charItem.slotIndex <= Const.BATTLE_SQUAD_MAX_CHAR_NUM then
            table.insert(showSelectChars, charItem)
        end
    end

    table.sort(showSelectChars, Utils.genSortFunction({ "slotIndex" }, true))
    return showSelectChars
end






CharFormationCtrl.m_isFormationLocked = HL.Field(HL.Boolean) << false

CharFormationCtrl.m_lockedTeamData = HL.Field(HL.Table)


CharFormationCtrl.m_customTeamIndex = HL.Field(HL.Number) << -1

CharFormationCtrl._IsSpecialFormation = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_lockedTeamData ~= nil or self.m_customTeamIndex > 0
end





CharFormationCtrl.m_equipTacticalItemBindingId = HL.Field(HL.Number) << -1

CharFormationCtrl.m_isCharInfoNaviGroupFocused = HL.Field(HL.Boolean) << false

CharFormationCtrl.m_slotConfirmBindingId = HL.Field(HL.Number) << -1

CharFormationCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self:BindInputPlayerAction("char_formation_slot_next", function()
        self:Notify(MessageConst.ON_CHAR_FORMATION_CHANGE_HOVER_INDEX, true)
        self:_UpdateSlotConfirmBinding()
    end, self.view.slotInputBindingGroup.groupId)
    self:BindInputPlayerAction("char_formation_slot_previous", function()
        self:Notify(MessageConst.ON_CHAR_FORMATION_CHANGE_HOVER_INDEX, false)
        self:_UpdateSlotConfirmBinding()
    end, self.view.slotInputBindingGroup.groupId)
    self.m_slotConfirmBindingId = self:BindInputPlayerAction("char_formation_slot_confirm", function()
        self:Notify(MessageConst.ON_CHAR_FORMATION_CONFIRM_HOVER)
    end, self.view.slotInputBindingGroup.groupId)
    self.m_equipTacticalItemBindingId = self:BindInputPlayerAction("char_formation_equip_tactical_item", function()
        self.view.charInformation.charFormationTacticalItem:GoToCharInfoEquipPage()
    end)
    InputManagerInst:ToggleBinding(self.m_equipTacticalItemBindingId, false)
    self.view.charInfoNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.m_isCharInfoNaviGroupFocused = isFocused
        if not isFocused then
            Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
            Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)
            Notify(MessageConst.CHAR_INFO_CLOSE_INFO_TIP)
        end
        self.view.charInformation.controllerFocusHintNode.gameObject:SetActive(not isFocused)
    end)
end

CharFormationCtrl._UpdateSlotConfirmBinding = HL.Method() << function(self)
    local isLocked = false
    local isEmpty = false
    if self.m_phase then
        local slot = self.m_phase:GetCurNaviSlot()
        if slot then
            isLocked = slot.isLocked
            isEmpty = slot.empty
        end
    end
    InputManagerInst:ForceBindingKeyhintToGray(self.m_slotConfirmBindingId, isLocked)
    InputManagerInst:SetBindingText(self.m_slotConfirmBindingId, (isEmpty or isLocked) and
         Language.key_hint_char_formation_slot_confirm_no_char or Language.key_hint_char_formation_slot_confirm)
end





CharFormationCtrl._UpdateWeekRaid = HL.Method() << function(self)
    local isWeekRaid = self.m_weekRaidArg ~= nil


    local success, maxCount = GameInstance.player.weekRaidSystem.techTypeValue:TryGetValue(GEnums.WeekRaidTechType.BombLimit)
    self.view.carryingExplosives.gameObject:SetActive(isWeekRaid and success and maxCount > 0)

    if isWeekRaid then
        self.m_curSelectTakeItemCount = GameInstance.player.weekRaidSystem.lastBombCount
        local lastCount = GameInstance.player.weekRaidSystem.lastBombCount
        local allCount = Utils.getItemCount(Tables.weekRaidConst.takeItemId, true, true)
        self.m_curSelectTakeItemCount = math.min(lastCount, allCount)
        if self.m_curSelectTakeItemCount ~= GameInstance.player.weekRaidSystem.lastBombCount then
            GameInstance.player.weekRaidSystem:SetBombCount(self.m_curSelectTakeItemCount)
        end
        local cfg = Tables.weekRaidTable:GetValue(GameInstance.player.weekRaidSystem.gameId)
        self.view.titleTxt.text = string.format("//%s", cfg.raidTopic)
        self.view.titleIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_FORMATION_ICON, cfg.icon)
        self.view.dungeonInfoBtn.gameObject:SetActive(false)

        CS.Beyond.Gameplay.Conditions.OnWeekRaidIntroCharFormationOpen.Trigger()
        self.view.carryingExplosives.addBtn.onClick:RemoveAllListeners()
        self.view.carryingExplosives.addBtn.onClick:AddListener(function()
            self:_AdjustSelectTakeItemCount()
        end)
        self:_UpdateTakeItemInfo()
    end
end

CharFormationCtrl._AdjustSelectTakeItemCount = HL.Method() << function(self)
    local maxCount = GameInstance.player.weekRaidSystem.techTypeValue[GEnums.WeekRaidTechType.BombLimit]
    UIManager:Open(PanelId.CommonItemNumSelect,{
        id = Tables.weekRaidConst.takeItemId,
        count = self.m_curSelectTakeItemCount,
        maxCount = math.min(Utils.getItemCount(Tables.weekRaidConst.takeItemId, true, true), maxCount),
        showItemInfoBtn = true,
        useSlider = true,
        forceIncludeCurDepot = true,
        onComplete = function(num)
            if num ~= nil then
                self.m_curSelectTakeItemCount = num
                GameInstance.player.weekRaidSystem:SetBombCount(num)
            end
            self:_UpdateTakeItemInfo()
        end
    })
end

CharFormationCtrl._UpdateTakeItemInfo = HL.Method() << function(self)
    if self.m_isClosed then
        return
    end

    if self.m_curSelectTakeItemCount <= 0 then
        self.view.carryingExplosives.itemBigBlack.gameObject:SetActiveIfNecessary(false)
    else
        self.view.carryingExplosives.itemBigBlack.gameObject:SetActiveIfNecessary(true)
        self.view.carryingExplosives.itemBigBlack:InitItem({
            id = Tables.weekRaidConst.takeItemId,
            count = self.m_curSelectTakeItemCount,
        },function()
            self:_AdjustSelectTakeItemCount()
        end)
    end
end





CharFormationCtrl.m_isContingencyContract = HL.Field(HL.Boolean) << false

CharFormationCtrl._UpdateContingencyContract = HL.Method() << function(self)
    self.m_isContingencyContract = false
    if self.m_subGameData and self.m_subGameData.gameMechanicsType == GEnums.GameMechanicsType.DungeonContingencyContract then
        self.m_isContingencyContract = true
    end
    self.view.contingencyTips.gameObject:SetActive(false)

    if not self.m_isContingencyContract then
        return
    end


    local ccSystem = GameInstance.player.contingencyContractSystem
    local _, gameData = ccSystem.ccDataDict:TryGetValue(self.m_dungeonId)
    local tagIds = {}
    local tempTipData
    if gameData then
        for _, tagId in pairs(gameData.curSelectTagList) do
            if not tempTipData then
                local _, tipData = Tables.CcTagTipTable:TryGetValue(tagId)
                if tipData and not string.isEmpty(tipData.formationTip) then
                    tempTipData = tipData
                    self.view.contingencyTips.gameObject:SetActive(true)
                    self.view.contingencyTips.tipsTxt.text = tipData.formationTip
                end
            end
            table.insert(tagIds, tagId)
        end
    end

    self.view.dungeonInfoBtn.gameObject:SetActive(true)
    self.view.dungeonInfoBtn.onClick:RemoveAllListeners()
    self.view.dungeonInfoBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.ContingencyContractDetailsPopup, {
            gameId = self.m_dungeonId,
            tagIds = tagIds,
        })
    end)
end






CharFormationCtrl._TryRecoverCommonPopUp = HL.Method(HL.Table) << function(self, commonPopUpArg)
    if commonPopUpArg == nil or type(commonPopUpArg) ~= "table" or string.isEmpty(commonPopUpArg.content) then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and commonPopUpCtrl:IsShow() then
        return
    end
    if commonPopUpArg.content == Language.LUA_CHANGE_TEAM_NAME then
        commonPopUpArg.onConfirm = function(changedName)
            self:_OnConfirmChangeTeamName(changedName)
        end
        Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
    elseif commonPopUpArg.content == Language.LUA_TEAM_FORMATION_CHAR_NOT_FULL_TIPS then
        commonPopUpArg.onConfirm = function()
            self:_OnConfirmCharNotFullEnterDungeon()
        end
        self:Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
    end
end



HL.Commit(CharFormationCtrl)
