local dialogCtrlBase = require_ex('UI/Panels/Dialog/DialogCtrlBase')
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Dialog
DialogCtrl = HL.Class('DialogCtrl', dialogCtrlBase.DialogCtrlBase)







DialogCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {

    [MessageConst.UI_DIALOG_TEXT_STOPPED] = 'OnDialogTextStopped',
    [MessageConst.ON_DIALOG_TRUNK_TEXT_FADE] = 'OnDialogTextFade',
    [MessageConst.ON_DIALOG_TRUNK_RADIO_FADE] = 'OnDialogRadioFade',
    [MessageConst.ON_DIALOG_CHANGE_CENTER_IMAGE] = 'OnDialogChangeCenterImage',
    [MessageConst.ON_DIALOG_DISABLE_CLICK_END] = 'OnDialogDisableClickEnd',
    [MessageConst.SWITCH_DIALOG_CAN_SKIP] = 'OnSwitchDialogCanSkip',
    [MessageConst.SWITCH_DIALOG_SHOW_LOG] = 'OnSwitchDialogShowLog',
    [MessageConst.DIALOG_SHOW_DEV_WATER_MARK] = 'ShowDevWaterMark',
    [MessageConst.DIALOG_REFRESH_AUTO_MODE] = 'OnRefreshAutoMode',
}

DialogCtrl.m_trunkNodeData = HL.Field(CS.Beyond.Gameplay.DTTrunkNodeData)

DialogCtrl.m_canSkip = HL.Field(HL.Boolean) << true

DialogCtrl.m_centerImageTween = HL.Field(HL.Userdata)

DialogCtrl.m_radioName = HL.Field(HL.String) << ""

DialogCtrl.m_curTrunkId = HL.Field(HL.String) << ""

DialogCtrl.m_hasShowedTrunkText = HL.Field(HL.Boolean) << false

DialogCtrl.m_clickCount = HL.Field(HL.Number) << 0

DialogCtrl.OnPreloadDialogPanel = HL.StaticMethod(HL.Opt(HL.Table)) << function(arg)
    local preloadFinishCallback = arg and unpack(arg)

    UIManager:PreloadPanelAsset(PANEL_ID, PhaseId.Dialog, function()
        if preloadFinishCallback ~= nil then
            preloadFinishCallback()
        end
    end)
end

DialogCtrl.OnShow = HL.Override() << function(self)
    self:OnDialogShow()
    self.view.debugNode.gameObject:SetActive(false)
end

DialogCtrl.RefreshDebugNode = HL.Method() << function(self)
    if NarrativeUtils.ShouldShowNarrativeDebugNode() then
        self.view.debugNode.gameObject:SetActive(true)
        local desc = "\nT2/T3"
        if GameWorld.dialogManager.dialogTree.dialogTreeData.qualityLevel == CS.Beyond.Gameplay.DialogEnums.DialogQualityLevel.High then
            desc = "\nTO/T1"
        end
        self.view.textDialogId.text = GameWorld.dialogManager.dialogId .. desc
    end
end


DialogCtrl.GetCurDialogId = HL.Override().Return(HL.String) << function(self)
    return GameWorld.dialogManager.dialogId or ""
end

DialogCtrl.OnDialogShow = HL.Override() << function(self)
    DialogCtrl.Super.OnDialogShow(self)
    self:_RefreshCanSkip()

    if self.m_curTrunkId == "" then
        self:OnDialogTextFade({ 0, 0 })
    end

    if not self.m_hasShowedTrunkText then
        self.view.bottomMask.gameObject:SetActive(false)
        self.view.buttonLog.gameObject:SetActive(false)
        self.view.buttonAuto.gameObject:SetActive(false)

        self.view.textAuto.gameObject:SetActive(false)
    end
end

DialogCtrl.OnBtnNextClick = HL.Override() << function(self)
    self.m_clickCount = self.m_clickCount + 1
    if self:CheckTextPlaying() then
        self.view.textTalk:SeekToEnd()
        self.view.textTalkCenter:SeekToEnd()
    else
        local dialogTimelineManager = GameWorld.dialogTimelineManager
        if dialogTimelineManager ~= nil and dialogTimelineManager.isDialogTimelinePlaying then
            return
        end
        self:_UpdateClickRecord()
        GameWorld.dialogManager:Next()
    end
end

DialogCtrl.OnDialogTextStopped = HL.Override() << function(self)
    self.view.optionList.gameObject:SetActive(true)
    local showWait = self.m_optionCells:GetCount() <= 0
    self:_TrySetWaitNode(showWait)
end

DialogCtrl.OnDialogDisableClickEnd = HL.Method() << function(self)
    local showWait = self.m_optionCells:GetCount() <= 0
    self:_TrySetWaitNode(showWait)
end

DialogCtrl.OnDialogTextFade = HL.Method(HL.Table) << function(self, arg)
    local duration, alpha = unpack(arg)

    self:_UpdateClickRecord()
    self.m_clickCount = 0
    self.m_curTrunkId = GameWorld.dialogManager.trunkId
    self:SetGlossaryPopUpEnable(alpha > 0)

    self.view.bottomLayout:DOKill()
    if duration == 0 then
        self.view.bottomLayout.alpha = alpha
    else
        self.view.bottomLayout:DOFade(alpha, duration)
    end

    if self.view.textTalkCenterNode.gameObject.activeSelf then
        self.view.textTalkCenterNode:DOKill()
        if duration == 0 then
            self.view.textTalkCenterNode.alpha = alpha
        else
            self.view.textTalkCenterNode:DOFade(alpha, duration)
        end
    end
end

DialogCtrl.OnDialogRadioFade = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local radioNode = self.view.radioNode
    if radioNode.gameObject.activeSelf then
        radioNode:PlayOutAnimation(function()
            radioNode.gameObject:SetActive(false)
        end)
    end
end

DialogCtrl._ClearCenterImageTween = HL.Method() << function(self)
    if self.m_centerImageTween then
        self.m_centerImageTween:Kill()
        self.m_centerImageTween = nil
    end
end

DialogCtrl.OnDialogChangeCenterImage = HL.Method(HL.Table) << function(self, data)
    local enable, sprite = unpack(data)
    self:_ClearCenterImageTween()
    if enable then
        self.view.bgBlack.gameObject:SetActive(true)
        self.view.bgBlack.alpha = 0
        local finalSprite = self:_GetRealSprite("", sprite)
        self.view.itemImage:LoadSprite(finalSprite)
        self.m_centerImageTween = self.view.bgBlack:DOFade(1, 0.3)
        self.m_centerImageTween:OnComplete(function()
            self:_ClearCenterImageTween()
        end)
    else
        self.m_centerImageTween = self.view.bgBlack:DOFade(0, 0.3)
        self.m_centerImageTween:OnComplete(function()
            self:_ClearCenterImageTween()
            self.view.bgBlack.gameObject:SetActive(false)
        end)
    end
end

DialogCtrl.OnSwitchDialogCanSkip = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshCanSkip()
end

DialogCtrl.OnSwitchDialogShowLog = HL.Method(HL.Table) << function(self, arg)
    local show = unpack(arg)
    self.view.buttonLog.gameObject:SetActive(show)
end

DialogCtrl._RefreshCanSkip = HL.Override() << function(self)
    self.m_canSkip = GameWorld.dialogManager.canSkip
    self.view.buttonSkip.gameObject:SetActive(self.m_canSkip)
end

DialogCtrl._TrySetWaitNode = HL.Override(HL.Boolean) << function(self, active)
    if active then
        local disableClick = GameWorld.dialogManager.disableClick
        local playing = self.view.textTalk.playing and self.view.textTalkCenter.playing
        local canShowWait = not disableClick and not playing
        self.view.waitNode.gameObject:SetActive(canShowWait)
        self.view.centerWaitNode.gameObject:SetActive(canShowWait)
    else
        self.view.waitNode.gameObject:SetActive(active)
        self.view.centerWaitNode.gameObject:SetActive(active)
    end
end

DialogCtrl.OnOptionClick = HL.Override(HL.Number, HL.Any) << function(self, index, _)
    GameWorld.dialogManager:SelectIndex(CSIndex(index))
end

DialogCtrl.OnBtnSkipClick = HL.Override() << function(self)
    self:Notify(MessageConst.OPEN_DIALOG_SKIP_POP_UP)
end

DialogCtrl.OnBtnAutoClick = HL.Override() << function(self)
    local auto = not GameWorld.dialogManager.autoMode
    GameWorld.dialogManager:SetAutoMode(auto)
end

DialogCtrl._RefreshAutoMode = HL.Override(HL.Boolean) << function(self, autoMode)
    if not self.m_hasShowedTrunkText then
        return
    end

    DialogCtrl.Super._RefreshAutoMode(self, autoMode)
end

DialogCtrl.OnBtnBackClick = HL.Override() << function(self)
    self:Notify(MessageConst.ON_COMMON_BACK_CLICKED)
end

DialogCtrl.OnBtnStopClick = HL.Override() << function(self)
    self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FEATURE_NOT_AVAILABLE)
end

DialogCtrl._CloseAutoMode = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self.view.textAuto.gameObject:SetActive(false)
    GameWorld.dialogManager:SetAutoMode(false)
    self:_SwitchControllerAutoPlayHint()
end

DialogCtrl.SetTrunk = HL.Method(HL.Userdata, HL.Opt(HL.Boolean, HL.Any, HL.Any)) << function(self, trunkNodeData, fastMode,
                                                                                             npcId, npcGroupId)
    self.m_trunkNodeData = trunkNodeData
    local hideBg = trunkNodeData.hideBg
    local bgSprite = self:_GetRealSprite(UIConst.UI_SPRITE_DIALOG_BG, trunkNodeData.bgSprite or "")
    local trunkId = trunkNodeData.overrideTrunkId
    if string.isEmpty(trunkId) then
        trunkId = trunkNodeData.trunkId
    end

    self:_UpdateClickRecord()
    self.m_curTrunkId = trunkId
    local name = ""
    local dialogText = ""
    local res, trunkTbData = Tables.dialogTextTable:TryGetValue(trunkId)
    if res then
        if not string.isEmpty(trunkTbData.actorName) then
            name = trunkTbData.actorName
        end
        dialogText = trunkTbData.dialogText
    end

    if BEYOND_DEBUG then
        if trunkNodeData.mfTrunkActionData.useCustomText then
            dialogText = string.format("<color=red>[Dev Only] Debug Custom Text:</color> %s", trunkNodeData.mfTrunkActionData.customText)
        end
    end

    local text, entryLinks = UIUtils.resolveNarrativeTextWithEntryLinks(dialogText)
    local singleTrunk = string.isEmpty(name)
    self:SetCurEntryLinks(entryLinks)

    self.view.bottomLayout:DOKill()
    self.view.bottomLayout.alpha = 1
    self.view.textTalkCenterNode.alpha = 1

    self.view.textTalkCenterNode.gameObject:SetActive(singleTrunk)
    self.view.bottomLayout.gameObject:SetActive(not singleTrunk)
    self.view.imageBG.gameObject:SetActive(not hideBg)

    if not self.m_hasShowedTrunkText then
        self.m_hasShowedTrunkText = true
        self.view.buttonLog.gameObject:SetActive(true)
        self.view.buttonAuto.gameObject:SetActive(true)
        self.view.bottomMask.gameObject:SetActive(true)

        self:_RefreshAutoMode(GameWorld.dialogManager.autoMode)
        if not self:IsPlayingAnimationIn() then
            self:PlayAnimation("dialog_bottom_in")
        end
    end

    if singleTrunk then
        self.view.textTalkCenter:SetText(text)
        if fastMode then
            self.view.textTalkCenter:SeekToEnd()
        else
            self.view.textTalkCenter:Play()
        end

    else
        local richName = UIUtils.resolveTextCinematic(name)
        richName = UIUtils.removePattern(richName, UIConst.NARRATIVE_ANONYMITY_PATTERN)
        self.view.textName:SetAndResolveTextStyle(richName)
        self.view.textName.gameObject:SetActive(true)

        self.view.textTalk:SetText(text)
        if fastMode then
            self.view.textTalk:SeekToEnd()
        else
            self.view.textTalk:Play()
        end
    end

    if not string.isEmpty(bgSprite) then
        self.view.bgSprite:LoadSprite(UIConst.UI_SPRITE_DIALOG_BG, bgSprite)
        self.view.bgSprite.gameObject:SetActive(true)
    else
        self.view.bgSprite.gameObject:SetActive(false)
    end


    local useRadio = trunkNodeData.useRadio
    local radioNode = self.view.radioNode

    if useRadio then
        radioNode.gameObject:SetActive(useRadio)

        local charSpriteName = ""
        if not string.isEmpty(trunkNodeData.radioIcon) then
            charSpriteName = trunkNodeData.radioIcon
        else
            local entity = GameWorld.dialogManager:GetEntity(trunkNodeData.actorIndex)
            if entity and entity.templateData then
                local charId = entity.templateData.id
                charSpriteName = UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. charId
            end
        end

        if not string.isEmpty(charSpriteName) then
            self.view.charImage:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, charSpriteName)
            self.view.blueMask:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, charSpriteName)
        end

        local radioName
        if not string.isEmpty(trunkNodeData.radioNameId) then
            radioName = Language[trunkNodeData.radioNameId]
        else
            radioName = name
        end

        if radioName ~= self.m_radioName then
            radioNode:ClearTween(false)
            radioNode:PlayInAnimation()
        end

        self.m_radioName = radioName
    else
        if radioNode.gameObject.activeSelf then
            radioNode:ClearTween(false)
            radioNode:PlayOutAnimation(function()
                radioNode.gameObject:SetActive(useRadio)
            end)
        end
        self.m_radioName = ""
    end

    self.view.textDes.gameObject:SetActive(false)
    self:SetCtrlButtonVisible(true)
    self:_TrySetWaitNode(false)
    self:_RefreshCanSkip()
    self:RefreshDebugNode()
end

DialogCtrl._UpdateClickRecord = HL.Method() << function(self)
    GameWorld.dialogManager:UpdateClickRecord(self.m_curTrunkId, self.m_clickCount)
    self.m_clickCount = 0
end

DialogCtrl.RefreshTrunk = HL.Method() << function(self)
    self.view.textTalk:Play()
    self.view.textTalkCenter:Play()
end

DialogCtrl.SetTrunkOption = HL.Override(HL.Userdata) << function(self, optionData)
    DialogCtrl.Super.SetTrunkOption(self, optionData)

    local count = optionData.Count
    if count == 0 then
        self:_SwitchFriendshipShow(false)
    else
        local workFriendship = GameWorld.dialogManager.showSpaceshipCharFriendship
        self:_SwitchFriendshipShow(workFriendship)
    end
end

DialogCtrl.GetTouchPanel = HL.Method().Return(CS.Beyond.UI.UITouchPanel) << function(self)
    return self.view.touchPanel
end

DialogCtrl._GetRealSprite = HL.Method(HL.String, HL.String).Return(HL.String) << function(self, folder, spriteName)
    local gender = Utils.getPlayerGender()
    local finalSprite = spriteName
    if gender == CS.Proto.GENDER.GenMale then

        local subName = finalSprite
        if string.endWith(spriteName, UIConst.DIALOG_IMAGE_FEMALE_SUFFIX) then
            subName = string.sub(spriteName, 0, string.len(spriteName) - 2)
        end
        local maleSpriteName = subName .. UIConst.DIALOG_IMAGE_MALE_SUFFIX
        local maleSpritePath = maleSpriteName
        if not string.isEmpty(folder) then
            maleSpritePath = UIUtils.getSpritePath(folder, maleSpriteName)
        else
            maleSpritePath = UIUtils.getSpritePath(maleSpriteName)
        end
        local res = ResourceManager.CheckExists(maleSpritePath)
        if res then
            finalSprite = maleSpriteName
        end
    end
    return finalSprite
end

DialogCtrl.SetFullBg = HL.Method(CS.Beyond.Gameplay.DialogFullBgActionData) << function(self, actionData)
    local bgSprite = self:_GetRealSprite(UIConst.UI_SPRITE_DIALOG_BG, actionData.bgSprite)
    local textId = actionData.textId
    local pos = actionData.pos
    local alpha = actionData.alpha
    local duration = actionData.duration
    local useCurve = actionData.useCurve
    local curve = actionData.curve
    local maskFadeTime = actionData.maskFadeTime
    local maskColor = actionData.maskColor
    local hideOtherUI = actionData.hideOtherUI

    self.view.bgSprite:DOKill()
    self.view.bgMask:DOKill()

    local function bgSpriteFade(onFadeComplete)
        if duration == 0 then
            UIUtils.changeAlpha(self.view.bgSprite, alpha)
            if onFadeComplete then
                onFadeComplete()
            end
            if alpha == 0 then
                self.view.bgSprite.gameObject:SetActive(false)
            end
        else
            local tween = self.view.bgSprite:DOFade(alpha, duration)
            tween:OnComplete(function()
                if onFadeComplete then
                    onFadeComplete()
                end

                if alpha == 0 then
                    self.view.bgSprite.gameObject:SetActive(false)
                end
            end)
            if useCurve and curve then
                tween:SetEase(curve)
            end
        end
    end

    local function maskFade(onFadeComplete)
        if maskFadeTime == 0 then
            UIUtils.changeAlpha(self.view.bgMask, alpha)
            if onFadeComplete then
                onFadeComplete()
            end
            if alpha == 0 then
                self.view.bgMask.gameObject:SetActive(false)
            end
        else
            local tween = self.view.bgMask:DOFade(alpha, maskFadeTime)
            tween:OnComplete(function()
                if onFadeComplete then
                    onFadeComplete()
                end
                if alpha == 0 then
                    self.view.bgMask.gameObject:SetActive(false)
                end
            end)
        end
    end

    if not string.isEmpty(bgSprite) then
        self.view.bgSprite:LoadSprite(UIConst.UI_SPRITE_DIALOG_BG, bgSprite)
        self.view.bgSprite.gameObject:SetActive(true)
        self.view.bgMask.gameObject:SetActive(true)
        self.view.bgMask.color = maskColor;

        UIUtils.changeAlpha(self.view.bgSprite, 1 - alpha)
        UIUtils.changeAlpha(self.view.bgMask, 1 - alpha)

        if alpha == 1 then
            maskFade(bgSpriteFade)
        else
            bgSpriteFade(maskFade)
        end
    else
        self.view.bgSprite.gameObject:SetActive(false)
    end

    if string.isEmpty(textId) then
        self.view.textDes.gameObject:SetActive(false)
    else
        self.view.textDes.gameObject:SetActive(true)
        self.view.textDes:SetAndResolveTextStyle(Language[textId])
        self.view.textDes.transform.localPosition = pos
        self.view.textDes.transform.localPosition = pos
    end

    if hideOtherUI then
        self.view.imageBG.gameObject:SetActive(false)
        self.view.optionList.gameObject:SetActive(false)
        self.view.bottomLayout.gameObject:SetActive(false)
        self.view.textTalkCenterNode.gameObject:SetActive(false)
        self.view.radioNode.gameObject:SetActive(false)
        self:SetCtrlButtonVisible(false)
    end
end

DialogCtrl.SetPostProcessEffect = HL.Method(CS.Beyond.Gameplay.DialogUIPostProcessDesc) << function(self, postProcessDesc)
    local effectTexture = postProcessDesc.textureAsset
    local color = postProcessDesc.color
    local alpha = postProcessDesc.alpha
    local fadeTime = postProcessDesc.fadeTime

    self.view.postProcessEffect:DOKill()
    self.view.postProcessEffect.gameObject:SetActive(true)
    self.view.postProcessEffect.color = color

    if effectTexture ~= nil then
        self.view.postProcessEffect.texture = effectTexture
    end

    if fadeTime == 0 then
        UIUtils.changeAlpha(self.view.postProcessEffect, alpha)
        if alpha == 0 then
            self.view.postProcessEffect.gameObject:SetActive(false)
        end
    else
        UIUtils.changeAlpha(self.view.postProcessEffect, 1 - alpha)
        local tween = self.view.postProcessEffect:DOFade(alpha, fadeTime)
        tween:OnComplete(function()
            if alpha == 0 then
                self.view.postProcessEffect.gameObject:SetActive(false)
            end
        end)
    end
end

DialogCtrl.SetLeftSubtitle = HL.Method(CS.Beyond.Gameplay.DialogLeftSubtitleActionData) << function(self, actionData)
    self.view.leftSubtitlePanel:SetLeftSubTitle(
        (not actionData.text1.isEmpty) and actionData.text1:GetText() or nil,
        (not actionData.text2.isEmpty) and actionData.text2:GetText() or nil,
        (not actionData.text3.isEmpty) and actionData.text3:GetText() or nil,
        (not actionData.text4.isEmpty) and actionData.text4:GetText() or nil
    )
    self.view.leftSubtitlePanel:StartAutoPlay(actionData.textStayTime)
end

DialogCtrl.ExitLeftSubtitle = HL.Method() << function(self)
    self.view.leftSubtitlePanel:Exit()
end

DialogCtrl.SetCtrlButtonVisible = HL.Method(HL.Boolean) << function(self, visible)
    self.view.topRight.gameObject:SetActive(visible)
    self.view.topLeft.gameObject:SetActive(visible)
    self.view.top.gameObject:SetActive(visible)
end



DialogCtrl._SwitchFriendshipShow = HL.Method(HL.Boolean) << function(self, visible)
    local charId = GameWorld.dialogManager.spaceshipCharId
    local realVisible = visible and not string.isEmpty(charId)
    local cellGO = self.view.friendshipRight.reliabilityCell.gameObject
    local lastVisible = cellGO.activeSelf
    if realVisible then
        self.view.friendshipRight.gameObject:SetActive(realVisible)
        if not lastVisible then
            cellGO:SetActive(realVisible)
            AudioAdapter.PostEvent("Au_UI_Popup_ReliabilithCell_Open")
            self.view.friendshipRight.animationWrapper:PlayInAnimation()
            self.view.friendshipRight.reliabilityCell:InitReliabilityCell(charId)
        end
    else
        self.view.friendshipRight.animationWrapper:PlayOutAnimation(function()
            cellGO:SetActive(realVisible)
            if lastVisible then
                AudioAdapter.PostEvent("Au_UI_Popup_ReliabilithCell_Close")
            end
        end)
    end
end

DialogCtrl.ShowPresentSuccess = HL.Method(HL.Boolean, HL.Number, HL.Table) << function(self, levelChanged, deltaFav, selectedItems)
    self.view.friendshipRight.reliabilityCell:ShowPresentSuccessTips(levelChanged, deltaFav, selectedItems)
    local workFriendship = GameWorld.dialogManager.showSpaceshipCharFriendship
    self:_SwitchFriendshipShow(workFriendship)
end

DialogCtrl._RefreshRestCell = HL.Method(HL.String) << function(self, charId)
    local restCell = self.view.friendshipRight.restCell
    local roomId, isWorking = CSPlayerDataUtil.GetCharRoomId(charId)
    local maxStamina = Tables.spaceshipConst.maxPhysicalStrength
    local curStamina = CSPlayerDataUtil.GetCharCharCurStamina(charId)
    local percent = curStamina / maxStamina
    if isWorking then

        restCell.textRest.text = Language.LUA_SPACESHIP_CHAR_WORKING
    elseif string.isEmpty(roomId) then

        if percent >= 1 then

            restCell.textRest.text = Language.LUA_SPACESHIP_CHAR_HANGING
        else
            restCell.textRest.text = Language.LUA_SPACESHIP_CHAR_RESTING
        end
    else
        restCell.textRest.text = Language.LUA_SPACESHIP_CHAR_RESTING
    end

    restCell.slider.value = percent
    restCell.textStamina.text = tostring(lume.round(percent * 100)) .. "%"

    if string.isEmpty(roomId) then
        restCell.textRoom.text = Language.LUA_SPACESHIP_HALL_NAME
    else
        local res, data = GameInstance.player.spaceship:TryGetRoom(roomId)
        restCell.textRoom.text = data.name
    end

end



HL.Commit(DialogCtrl)
