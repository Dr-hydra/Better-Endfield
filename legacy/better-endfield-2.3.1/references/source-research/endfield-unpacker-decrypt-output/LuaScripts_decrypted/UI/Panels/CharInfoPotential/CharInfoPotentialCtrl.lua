
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoPotential

CharInfoPotentialCtrl = HL.Class('CharInfoPotentialCtrl', uiCtrl.UICtrl)






CharInfoPotentialCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHAR_POTENTIAL_UNLOCK] = '_OnCharPotentialUnlock',
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = '_OnSelectCharChange',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = '_OnItemCountChange',
}

local MAIN_STATE_NAME =
{
    Normal = "Normal",
    LevelUp = "LevelUp",
    Photo = "Photo",
}

local MAX_SKILL_COUNT = 5

CharInfoPotentialCtrl.m_arg = HL.Field(HL.Table)

CharInfoPotentialCtrl.m_charTemplateId = HL.Field(HL.String) << ''

CharInfoPotentialCtrl.m_charInstId = HL.Field(HL.Number) << -1

CharInfoPotentialCtrl.m_potentialList= HL.Field(HL.Userdata)

CharInfoPotentialCtrl.m_isTrailChar = HL.Field(HL.Boolean) << false

CharInfoPotentialCtrl.m_isChangingChar = HL.Field(HL.Boolean) << false

CharInfoPotentialCtrl.m_isLevelUpActivated = HL.Field(HL.Boolean) << false


CharInfoPotentialCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_arg = args
    self.m_charInstId = args.initCharInfo.instId
    self.m_charTemplateId = args.initCharInfo.templateId

    self:_InitAction()
    self:_InitController()
    self:RefreshAll()
    self.m_phase:RefreshPotentialSceneDeco(self.m_charInstId)
    self.view.stateController:SetState(MAIN_STATE_NAME.Normal)
    self.view.maxAnim.gameObject:SetActive(false)
    self.view.unlockAnimMask.gameObject:SetActive(false)
    AudioAdapter.PostEvent("Au_UI_Menu_CharPotential_Open")

    self:_ProcessStateArg(self.m_arg.stateArg)
    self.m_arg.stateArg = nil
end

CharInfoPotentialCtrl.OnClose = HL.Override() << function(self)
    if self.m_isLevelUpActivated then
        self.view.rightNode.animWrapper.gameObject:SetActive(false)
        self:_ActiveLevelUp(false)
    end
    if self.isPhotoMode then
        self:_ActivePhotoMode(false)
        self:_ActiveLevelUp(false)
    end
end

CharInfoPotentialCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    local sceneDeco = self.m_phase:GetPotentialDecoView()
    sceneDeco.animationWrapper:ClearTween()
    sceneDeco.animationWrapper:PlayInAnimation()
end





CharInfoPotentialCtrl._OnCharPotentialUnlock = HL.Method(HL.Table) << function(self, args)
    local charInstId, level = unpack(args)
    if charInstId ~= self.m_charInstId then
        return
    end

    local function playUnlockAudio()
        local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInstId)
        local templateId = charInst.templateId
        if self.m_isPotentialMax then
            Utils.triggerVoice("chrup_telant_max", templateId)
            AudioAdapter.PostEvent("Au_UI_Event_CharPotentialLevelUpMax")
        else
            Utils.triggerVoice("chrup_telant_common", templateId)
            AudioAdapter.PostEvent("Au_UI_Event_CharPotentialLevelUp")
        end
    end

    self:_BlockUIInput(true)
    UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, false, function()
        self:_BlockUIInput(false)
        GameInstance.mobileMotionManager:PostEventCommonOperateSuccess()


        self:_StartCoroutine(function()
            self:_BlockUIInput(true)

            self:RefreshAll(level)
            self.m_phase:RefreshPotentialPhoto(self.m_charInstId, level)
            playUnlockAudio()

            local isMax = level == self.m_maxPotentialLevel
            if isMax then
                UIUtils.PlayAnimationAndToggleActive(self.view.maxAnim, true)
            end
            if lume.find(UIConst.CHAR_PHOTO_POTENTIAL_LEVELS, level) then
                coroutine.wait(3)
            else
                coroutine.wait(0.8)
            end
            if isMax then
                UIUtils.PlayAnimationAndToggleActive(self.view.maxAnim, false)
            end
            self:_RefreshRightNode(self.m_curShowPotentialLevel)
            UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, true)

            self:_BlockUIInput(false)
        end)

































    end)
end

CharInfoPotentialCtrl._OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_isChangingChar = true
    self.view.animWrapper:PlayOutAnimation(function()
        self.m_isChangingChar = false
        self.m_charInstId = charInfo.instId
        self.m_charTemplateId = charInfo.templateId
        self:RefreshAll()
        self.m_phase:RefreshPotentialSceneDeco(self.m_charInstId, true)
        self.view.animWrapper:PlayInAnimation()
    end)
end

CharInfoPotentialCtrl._OnItemCountChange = HL.Method(HL.Table) << function(self, arg)
    if self.view.rightNode.gameObject.activeSelf then
        self:_RefreshRightNode(self.m_curShowPotentialLevel)
    end
end





CharInfoPotentialCtrl._OnLevelUpClicked = HL.Method() << function(self)
    GameInstance.player.charBag:CharPotentialUnlock(self.m_charInstId, self.m_selectedItemId, self.m_potentialLevel + 1)
end





CharInfoPotentialCtrl.RefreshAll = HL.Method(HL.Opt(HL.Number)) << function(self, unlockedLv)
    local success, characterPotentialList = Tables.characterPotentialTable:TryGetValue(self.m_charTemplateId)
    if success then
        self.m_potentialList = characterPotentialList
        self.m_maxPotentialLevel = #self.m_potentialList.potentialUnlockBundle
    else
        logger.error("潜能数据不存在:"..self.m_charTemplateId)
    end

    self.m_isTrailChar = not CharInfoUtils.isCharDevAvailable(self.m_charInstId)

    self:_InitPotentialSkills()
    self:_RefreshPotentialData()
    self:_RefreshPotentialSkills(unlockedLv)
    self:_RefreshAllPotentialLevel()
    self:_InitControllerSideMenuBtn()

    if unlockedLv then
        self.m_phase:UnlockPotentialStar(unlockedLv, self.m_maxPotentialLevel)
    else
        self.m_phase:RefreshPotentialStar(self.m_potentialLevel, self.m_maxPotentialLevel)
    end

    if self.m_isTrailChar then
        self.view.currentPotentialNode.btnGoToLevelUp.gameObject:SetActive(false)
    end
end





CharInfoPotentialCtrl._InitAction = HL.Method() << function(self)
    self.view.currentPotentialNode.btnGoToLevelUp.onClick:AddListener(function()
        if self.m_isTrailChar then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_TALENT_UPGRADE_FORBID)
            return
        end
        self:_ActiveLevelUp(true, true)
    end)
    self.view.btnBack.onClick:AddListener(function()
        if self.isPhotoMode then
            self:_ActivePhotoMode(false)
        else
            self:_ActiveLevelUp(false)
        end
    end)
    self.view.rightNode.btnBack.onClick:AddListener(function()

        if self.view.rightNode.animWrapper.curState ~= CS.Beyond.UI.UIConst.AnimationState.Out then
            UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, false)
        end
        self:_ShowSkill(0)
    end)
    self.view.rightNode.leftBtn.onClick:AddListener(function()
        if self.m_curShowPotentialLevel > 1 then
            self:_ShowSkill(self.m_curShowPotentialLevel - 1)
        else
            self:_ShowSkill(self.m_maxPotentialLevel)
        end
    end)
    self.view.rightNode.rightBtn.onClick:AddListener(function()
        if self.m_curShowPotentialLevel < self.m_maxPotentialLevel then
            self:_ShowSkill(self.m_curShowPotentialLevel + 1)
        else
            self:_ShowSkill(1)
        end
    end)
    self.view.rightNode.btnLevelUp.onClick:AddListener(function()
        self:_OnLevelUpClicked()
    end)
    self.view.rightNode.needConditionBtn.onClick:AddListener(function()
        UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, false, function()
            self:_ShowSkill(self.m_potentialLevel + 1, true)
        end)
    end)
    self.view.rightNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_ShowSkill(0)
    end)
end

CharInfoPotentialCtrl._ActiveLevelUp = HL.Method(HL.Boolean, HL.Opt(HL.Boolean, HL.Number)) << function(self, active, selectCurrent, selectedIndex)
    self.m_isLevelUpActivated = active
    if active then
        self.view.animWrapper:PlayOutAnimation(function()
            self.view.stateController:SetState(MAIN_STATE_NAME.LevelUp)
            self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
            if selectCurrent then
                local index = selectedIndex or self.m_potentialLevel + 1
                self:_ShowSkill(index, true)
            else
                if DeviceInfo.usingController then
                    local naviIndex = self.m_potentialLevel + 1
                    if naviIndex > self.m_maxPotentialLevel then
                        naviIndex = 1
                    end
                    local skillNode = self.view[string.format("skill%02d", naviIndex)]
                    self:SetNaviTarget(skillNode.button)
                end
            end
        end)
    else
        UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, false, function()
            self:_ShowSkill(0)
            self.view.stateController:SetState(MAIN_STATE_NAME.Normal)
            self.view.potentialSkill.gameObject:SetActive(true)
            UIUtils.PlayAnimationAndToggleActive(self.view.potentialSkill, false)
            self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, false)
            self.view.animWrapper:PlayInAnimation()
            if DeviceInfo.usingController then
                InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.skillNaviGroup)
            end
        end)
        self.m_selectedSkillIndex = 0
    end
    self.m_phase:ActivePotentialFocusCamera(active)
    local decoView = self.m_phase:GetPotentialDecoView()
    if decoView and not IsNull(decoView.gameObject) then
        decoView.btnViewDetails.gameObject:SetActive(not active)
    end
    InputManagerInst:ToggleBinding(self.m_focusPhotoBindingId, active and self.m_potentialLevel > 1 and not self.m_isTrailChar)
    self.view.controllerSideMenuBtn.gameObject:SetActive(not active)
end

CharInfoPotentialCtrl.m_potentialLevel = HL.Field(HL.Number) << 0

CharInfoPotentialCtrl.m_isPotentialMax = HL.Field(HL.Boolean) << false

CharInfoPotentialCtrl.m_maxPotentialLevel = HL.Field(HL.Number) << 0

CharInfoPotentialCtrl._RefreshPotentialData = HL.Method() << function(self)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInstId)
    self.m_potentialLevel = charInfo.potentialLevel
    self.m_isPotentialMax = self.m_potentialLevel >= self.m_maxPotentialLevel
end

CharInfoPotentialCtrl._RefreshAllPotentialLevel = HL.Method() << function(self)
    self.view.detailPotentialNode.charPotential:InitCharPotential(self.m_potentialLevel)
    self.view.detailPotentialNode.stateController:SetState(self.m_isPotentialMax and "Max" or "Normal")
    self.view.detailPotentialNode.currentLevel.text = tostring(self.m_potentialLevel)
    self.view.detailPotentialNode.maxLevel.text = self.m_isPotentialMax and "MAX" or tostring(self.m_maxPotentialLevel)

    self.view.currentPotentialNode.stateController:SetState(self.m_isPotentialMax and "Max" or "Normal")
    self.view.currentPotentialNode.charPotential:InitCharPotential(self.m_potentialLevel)
    self.view.currentPotentialNode.currentLevel.text = tostring(self.m_potentialLevel)
    self.view.currentPotentialNode.maxLevel.text = self.m_isPotentialMax and "MAX" or tostring(self.m_maxPotentialLevel)
    self.view.currentPotentialNode.redDot:InitRedDot("CharInfoPotential", self.m_charInstId)

    self.view.glowHUD.level.text = string.format("%02d/", self.m_potentialLevel)
    self.view.glowHUD.maxLevel.text = self.m_isPotentialMax and "MAX" or string.format("%02d", self.m_maxPotentialLevel)
end

CharInfoPotentialCtrl._ShowSkill = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, playAnim)
    if index == self.m_selectedSkillIndex then
        return
    end
    if DeviceInfo.usingController then
        if index > 0 then
            InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.skillNaviGroup)
        else
            self:SetNaviTarget(self.view[string.format("skill%02d", self.m_selectedSkillIndex)].button)
        end
    end
    self:_SetSkillSelected(index)
    self:_RefreshRightNode(index)
    if playAnim then
        UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, true)
    end
    InputManagerInst:ToggleBinding(self.m_focusPhotoBindingId, index == 0 and self.m_potentialLevel > 1 and not self.m_isTrailChar)
end

CharInfoPotentialCtrl._BlockUIInput = HL.Method(HL.Boolean) << function(self, isBlock)
    if isBlock then
        self.view.luaPanel:BlockAllInput()
    else
        self.view.luaPanel:RecoverAllInput()
    end
    self.m_phase:GetPotentialDecoView().inputBindingGroup.enabled = not isBlock
    self.view.unlockAnimMask.gameObject:SetActive(isBlock)
end





CharInfoPotentialCtrl._InitPotentialSkills = HL.Method() << function(self)
    for i = 1, MAX_SKILL_COUNT do

        local skillNode = self.view[string.format("skill%02d", i)]
        if skillNode then
            local isShow = i <= self.m_maxPotentialLevel
            skillNode.gameObject:SetActive(isShow)
            if isShow then
                local potentialData = self.m_potentialList.potentialUnlockBundle[CSIndex(i)]
                skillNode.name.text = potentialData.name
                skillNode.number.text = string.format("%02d", i)
            end
            skillNode.button.onClick:RemoveAllListeners()
            skillNode.button.onClick:AddListener(function()
                if i == self.m_selectedSkillIndex then
                    return
                end
                UIUtils.PlayAnimationAndToggleActive(self.view.rightNode.animWrapper, false, function()
                    self:_ShowSkill(i, true)
                end)
            end)
            skillNode.redDot:InitRedDot("CharInfoPotentialSkill", {
                charInstId = self.m_charInstId,
                potentialLevel = i,
            })
        end
    end
end

CharInfoPotentialCtrl._RefreshPotentialSkills = HL.Method(HL.Opt(HL.Number)) << function(self, unlockedLv)
    for i = 1, self.m_maxPotentialLevel do

        local skillNode = self.view[string.format("skill%02d", i)]
        if skillNode then
            local isUnLocked = i <= self.m_potentialLevel
            local stateName = isUnLocked and "UnLocked" or "Locked"
            skillNode.stateController:SetState(stateName)
            local tagStateName = "None"
            local potentialData = self.m_potentialList.potentialUnlockBundle[CSIndex(i)]
            local hasPhoto = potentialData.unlockCharPictureItemList and potentialData.unlockCharPictureItemList.Count > 0
            local hasCard = not string.isEmpty(potentialData.unlockCardTopicItem)
            if hasCard and hasPhoto then
                tagStateName = "All"
            elseif hasPhoto then
                tagStateName = "Photo"
            elseif hasCard then
                tagStateName = "Card"
            end
            skillNode.tagStateController:SetState(tagStateName)
            skillNode.select.gameObject:SetActive(i == self.m_selectedSkillIndex)
            if unlockedLv and i == unlockedLv then
                skillNode.unLockedLayout:ClearTween()
                skillNode.unLockedLayout:PlayInAnimation()
            end
        end
    end
end

CharInfoPotentialCtrl.m_selectedSkillIndex = HL.Field(HL.Number) << 0

CharInfoPotentialCtrl._SetSkillSelected = HL.Method(HL.Number) << function(self, index)
    local lastSelectedNode = self.view[string.format("skill%02d", self.m_selectedSkillIndex)]
    if lastSelectedNode then
        lastSelectedNode.select.gameObject:SetActive(false)
    end

    local skillNode
    if index > 0 then
        skillNode = self.view[string.format("skill%02d", index)]
    end
    if skillNode then
        skillNode.select.gameObject:SetActive(true)
    end
    self.m_selectedSkillIndex = index
end





CharInfoPotentialCtrl.m_selectedItemId = HL.Field(HL.String) << ''

CharInfoPotentialCtrl.m_curShowPotentialLevel = HL.Field(HL.Number) << 0

CharInfoPotentialCtrl._RefreshRightNode = HL.Method(HL.Number) << function(self, potentialLevel)
    Notify(MessageConst.ON_STOP_HOVER_LINK)
    self.m_curShowPotentialLevel = potentialLevel
    local potentialDataCount = self.m_potentialList.potentialUnlockBundle.Count
    if potentialLevel < 1 or potentialLevel > potentialDataCount then
        return
    end
    local view = self.view.rightNode
    local potentialData = self.m_potentialList.potentialUnlockBundle[CSIndex(potentialLevel)]
    view.name.text = potentialData.name
    local potentialDesc = CS.Beyond.Gameplay.PotentialUtil.GetPotentialDescription(self.m_charTemplateId, potentialLevel, self.m_charInstId)
    view.textDesc:SetAndResolveTextStyle(potentialDesc)
    local itemId = potentialData.itemIds[0]
    local itemCount = Utils.getItemCount(itemId)
    local needCount = potentialData.itemCnts[0]
    local isLack = itemCount < needCount
    self.m_selectedItemId = itemId
    view.itemBigBlack:InitItem({id = itemId, count = needCount }, true)
    view.storageText.text = UIUtils.setCountColor(Language.ui_char_info_potential_mat_owned, isLack)
    view.storageCount.text = UIUtils.setCountColor(UIUtils.getNumString(itemCount), isLack)
    view.currentPotentialNumber.text = string.format("%02d", potentialLevel)

    local isUnlocked = potentialLevel <= self.m_potentialLevel
    local photoCount = potentialData.unlockCharPictureItemList and potentialData.unlockCharPictureItemList.Count or 0
    local hasCard = not string.isEmpty(potentialData.unlockCardTopicItem)
    local hasReward = (photoCount > 0 or hasCard) and not self.m_isTrailChar
    view.rewardItem.gameObject:SetActive(hasReward)
    view.scrollViewNaviGroup.enabled = hasReward
    if hasReward then
        view.photoNodeCache = view.photoNodeCache or UIUtils.genCellCache(view.rewardItem.photoNode)

        view.photoNodeCache:Refresh(photoCount, function(cell, index)
            local photoItemId = potentialData.unlockCharPictureItemList[CSIndex(index)]
            local _, itemData = Tables.itemTable:TryGetValue(photoItemId)
            if itemData then
                cell.txtPhotoName.text = itemData.name
            end
            cell.itemPhoto:InitItem({id = photoItemId}, true)
            cell.stateController:SetState(isUnlocked and "Normal" or "Locked")
            local _, pictureId = Tables.pictureItemTable:TryGetValue(photoItemId)
            cell.redDot:InitRedDot("CharInfoPotentialPicture", {
                charInstId = self.m_charInstId,
                potentialLevel = potentialLevel,
                pictureId = pictureId,
            })
            cell.btnCheckPicture.onClick:RemoveAllListeners()
            cell.btnCheckPicture.onClick:AddListener(function()
                if isUnlocked then
                    self.view.rightNode.autoCloseArea.enabled = false
                    self:ShowPhoto(pictureId, potentialLevel, function()
                        self.view.rightNode.autoCloseArea.enabled = true
                        local isChanged = self.m_phase:RefreshPotentialPhoto(self.m_charInstId, potentialLevel)
                        if isChanged then
                            self.view.rightNode.gameObject:SetActive(false)
                            self:_ShowSkill(0)
                        end
                    end)
                else
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_POTENTIAL_REWARD_LOCKED)
                end
            end)



        end)

        local cardNodeView = view.rewardItem.cardNode
        cardNodeView.gameObject:SetActive(hasCard)
        if hasCard then
            local _, itemData = Tables.itemTable:TryGetValue(potentialData.unlockCardTopicItem)
            if itemData then
                cardNodeView.txtCardName.text = itemData.name
            end
            local cardId
            for topicId , topicCfg in pairs(Tables.businessCardTopicTable) do
                if topicCfg.itemId == potentialData.unlockCardTopicItem then
                    cardId = topicId
                end
            end
            cardNodeView.itemCard:InitItem({id = potentialData.unlockCardTopicItem}, true)
            cardNodeView.stateController:SetState(isUnlocked and "Normal" or "Locked")
            cardNodeView.transform:SetAsLastSibling()
            cardNodeView.redDot:InitRedDot("NewBusinessCard", cardId)
            cardNodeView.btnCheckCard.onClick:RemoveAllListeners()
            cardNodeView.btnCheckCard.onClick:AddListener(function()
                if isUnlocked then
                    self.view.rightNode.autoCloseArea.enabled = false
                    PhaseManager:GoToPhase(PhaseId.FriendThemeChange, {
                        selectId = cardId,
                        onClose = function()
                            self.view.rightNode.autoCloseArea.enabled = true
                        end})
                else
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_POTENTIAL_REWARD_LOCKED)
                end
            end)



        end
    end
    view.scrollViewNaviGroup.getDefaultSelectableFunc = function()
        if hasReward then
            if photoCount > 0 then
                return view.photoNodeCache:Get(1).naviDeco
            elseif hasCard then
                return view.rewardItem.cardNode.naviDeco
            end
        end
        return nil
    end

    local stateName = ""
    if potentialLevel <= self.m_potentialLevel then
        stateName = "Unlocked"
    elseif potentialLevel == self.m_potentialLevel + 1 then
        if isLack then
            stateName = "Lack"
        else
            stateName = "Normal"
        end
    else
        stateName = "Locked"
    end
    view.stateController:SetState(stateName)

    if self.m_isTrailChar then
        view.stateController:SetState("Trail")
    end
end





CharInfoPotentialCtrl.ShowPhotoByLevel = HL.Method(HL.Number) << function(self, potentialLevel)
    if self.m_isChangingChar then
        return
    end
    if potentialLevel <= 0 and potentialLevel > #self.m_potentialList.potentialUnlockBundle then
        return
    end
    self:_StartCoroutine(function()
        local isLevelUp = self.view.stateController.currentStateName == MAIN_STATE_NAME.LevelUp
        if isLevelUp then
            UIUtils.PlayAnimationAndToggleActive(self.view.potentialSkill, false)
        else
            self.view.animWrapper:PlayOutAnimation()
            self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
        end
        self.m_phase:ActivePotentialPhotoCamera(potentialLevel, true)
        local sceneDecoView = self.m_phase:GetPotentialDecoView()
        local photoNode = sceneDecoView.viewPhotoNode
        photoNode.gameObject:SetActive(false)
        self:_BlockUIInput(true)
        coroutine.wait(0.6)
        self:_BlockUIInput(false)
        self:ShowPhoto("", potentialLevel, function()
            self:_StartCoroutine(function()
                photoNode.gameObject:SetActive(true)
                self.m_phase:ActivePotentialPhotoCamera(potentialLevel, false)
                self.m_phase:RefreshPotentialPhoto(self.m_charInstId, potentialLevel)
                self:_BlockUIInput(true)
                coroutine.wait(0.6)
                self:_BlockUIInput(false)
                if isLevelUp then
                    UIUtils.PlayAnimationAndToggleActive(self.view.potentialSkill, true)
                else
                    self.view.animWrapper:PlayInAnimation()
                    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, false)
                end
            end)
        end)
    end)
end

CharInfoPotentialCtrl.ShowPhoto = HL.Method(HL.String, HL.Number, HL.Opt(HL.Function)) << function(self, pictureId, potentialLevel, onClose)
    UIManager:Open(PanelId.CharInfoPhoto, {
        charInstId = self.m_charInstId,
        pictureId = pictureId,
        potentialLevel = potentialLevel,
        onClose = onClose,
    })
end





CharInfoPotentialCtrl.m_focusPhotoBindingId = HL.Field(HL.Number) << 0

CharInfoPotentialCtrl._InitController = HL.Method() << function(self)
    local charInfoPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfo)
    if charInfoPanelPhaseItem then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
            {self.view.inputGroup.groupId, charInfoPanelPhaseItem.uiCtrl.view.inputGroup.groupId,
            self.m_phase:GetPotentialDecoView().inputBindingGroup.groupId})
    else
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end
    self:BindInputPlayerAction("char_potential_view_details", function()
        self:_ActiveLevelUp(true)
    end, self.view.currentBindingGroup.groupId)
    self:BindInputPlayerAction("char_potential_view_photo", function()
        if self.m_potentialLevel < UIConst.CHAR_PHOTO_POTENTIAL_LEVELS[1] then
            return
        end
        self:_ActivePhotoMode(true, true)
    end, self.view.currentBindingGroup.groupId)
    self.m_focusPhotoBindingId = self:BindInputPlayerAction("char_potential_focus_photo", function()
        self:_ActivePhotoMode(true)
    end)
    InputManagerInst:ToggleBinding(self.m_focusPhotoBindingId, false)
    for i = 1, MAX_SKILL_COUNT do
        local skillNode = self.view[string.format("skill%02d", i)]
        if skillNode then
            skillNode.keyHint.gameObject:SetActive(false)
            skillNode.button.onIsNaviTargetChanged = function(isTarget)
                skillNode.keyHint.gameObject:SetActive(isTarget)
                if lume.find(UIConst.CHAR_PHOTO_POTENTIAL_LEVELS, i) == nil then
                    return
                end
                local sceneDecoView = self.m_phase:GetPotentialDecoView()
                if sceneDecoView == nil then
                    return
                end
                local photoNode = sceneDecoView[string.format("photoNode%d", i)]
                if photoNode then
                    photoNode.btnView.gameObject:SetActive(isTarget)
                end
            end
        end
    end
    UIUtils.bindHyperlinkPopup(self, "CharInfoPotential", self.view.inputGroup.groupId)
end

CharInfoPotentialCtrl._InitControllerSideMenuBtn = HL.Method() << function(self)
    local extraBtnInfos = {
        {
            button = self.m_phase:GetPotentialDecoView().btnViewDetails,
            textId = "ui_char_info_potential_view_details",
        },
    }
    if self.m_potentialLevel >= UIConst.CHAR_PHOTO_POTENTIAL_LEVELS[1] and not self.m_isTrailChar then
        table.insert(extraBtnInfos, {
            button = self.m_phase:GetPotentialDecoView().btnViewPhoto,
            textId = "ui_char_info_potential_view_photo",
            action = function()
                self:_ActivePhotoMode(true, true)
            end
        })
    end
    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = extraBtnInfos,
    })
end

CharInfoPotentialCtrl.isPhotoMode = HL.Field(HL.Boolean) << false

CharInfoPotentialCtrl._ActivePhotoMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isActive, playAnim)
    self.isPhotoMode = isActive
    if playAnim and isActive then
        self.view.animWrapper:PlayOutAnimation(function()
            self.view.stateController:SetState(MAIN_STATE_NAME.LevelUp)
            self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
            self:SetNaviTarget(self.view.skill01.button)
            self:_ActiveAllSkillNode(false)
            self.m_phase:NaviToPotentialPhoto(1)
        end)
    else
        self:_ActiveAllSkillNode(not isActive)
        if isActive then
            self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, true)
            self.m_phase:NaviToPotentialPhoto(1)
        end
    end

    self.m_phase:ActivePotentialFocusCamera(true)
    if not isActive then
        self.m_phase:StopNaviPotentialPhoto()
    end
    InputManagerInst:ToggleBinding(self.m_focusPhotoBindingId, not isActive and self.m_potentialLevel > 1 and not self.m_isTrailChar)
    self.m_phase:RefreshFocusPhotoBtn()
end

CharInfoPotentialCtrl._ActiveAllSkillNode = HL.Method(HL.Boolean) << function(self, isActive)
    for i = 1, MAX_SKILL_COUNT do
        local skillNode = self.view[string.format("skill%02d", i)]
        if skillNode then
            skillNode.gameObject:SetActive(isActive)
        end
    end
end

CharInfoPotentialCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg =
    {
        isLevelUpActivated = self.m_isLevelUpActivated,
        selectedSkillIndex = self.m_selectedSkillIndex,
    }
    local isPhotoOpen, photoCtrl = UIManager:IsOpen(PanelId.CharInfoPhoto)
    if isPhotoOpen then
        arg.photoPanelArg = photoCtrl:GetCurStateArg()
    end
    local isThemOpen, themCtrl = UIManager:IsOpen(PanelId.FriendThemeChange)
    if isThemOpen then
        arg.themePanelArg = {
            selectId = themCtrl.m_selectId,
        }
    end
    return arg
end

CharInfoPotentialCtrl._ProcessStateArg = HL.Method(HL.Table) << function(self, arg)
    if arg == nil then
        return
    end
    if arg.isLevelUpActivated then
        if arg.selectedSkillIndex and arg.selectedSkillIndex > 0 then
            self:_ActiveLevelUp(true, true, arg.selectedSkillIndex)
        else
            self:_ActiveLevelUp(true)
        end
        self.view.animWrapper:ClearTween(true)
    end
    if arg.photoPanelArg then
        arg.photoPanelArg.onClose = function()
            self.view.rightNode.autoCloseArea.enabled = true
            if self.view.rightNode.gameObject.activeSelf then
                local isChanged = self.m_phase:RefreshPotentialPhoto(self.m_charInstId, arg.photoPanelArg.potentialLevel)
                if isChanged then
                    self.view.rightNode.gameObject:SetActive(false)
                    self:_ShowSkill(0)
                end
            end
        end
        self.view.rightNode.autoCloseArea.enabled = false
        UIManager:Open(PanelId.CharInfoPhoto, arg.photoPanelArg)
    end
    if arg.themePanelArg then
        arg.themePanelArg.onClose = function()
            self.view.rightNode.autoCloseArea.enabled = true
        end
        self.view.rightNode.autoCloseArea.enabled = false
        UIManager:Open(PanelId.FriendThemeChange, arg.themePanelArg)
    end
end



HL.Commit(CharInfoPotentialCtrl)
