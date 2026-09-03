















local GachaCharTLHelper = require_ex("UI/Panels/GachaChar/GachaCharTLHelper")

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaChar

local GachaState = {
    UIStar = 1,
    CharEnter = 2,
    CharLoop = 3,
    ShowResult = 4,
}

local StarAnimations = {
    [4] = "gacha_char_start_4",
    [5] = "gacha_char_start_5",
    [6] = "gacha_char_start_6",
}

local StarAudios = {
    [4] = "Au_UI_Gacha_Star4",
    [5] = "Au_UI_Gacha_Star5",
    [6] = "Au_UI_Gacha_Star6",
}
local LoopAudios = {
    [4] = "Au_UI_Gacha_Charshow4",
    [5] = "Au_UI_Gacha_Charshow5",
    [6] = "Au_UI_Gacha_Charshow6",
}


GachaCharCtrl = HL.Class('GachaCharCtrl', uiCtrl.UICtrl)






GachaCharCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GachaCharCtrl.m_args = HL.Field(HL.Table)

GachaCharCtrl.m_charCount = HL.Field(HL.Number) << -1

GachaCharCtrl.m_curInfo = HL.Field(HL.Table)

GachaCharCtrl.m_curIndex = HL.Field(HL.Number) << -1

GachaCharCtrl.m_state = HL.Field(HL.Number) << -1

GachaCharCtrl.m_startTimelineCor = HL.Field(HL.Thread)

GachaCharCtrl.m_curCharObj = HL.Field(CS.UnityEngine.GameObject)

GachaCharCtrl.m_curCharTLHelper = HL.Field(HL.Forward('GachaCharTLHelper'))

GachaCharCtrl.m_isSkipped = HL.Field(HL.Boolean) << false

GachaCharCtrl.m_startTimerId = HL.Field(HL.Number) << -1

GachaCharCtrl.m_start6StarNameUITimerId = HL.Field(HL.Number) << -1

GachaCharCtrl.m_introduceVoiceTimerId = HL.Field(HL.Number) << -1

GachaCharCtrl.m_sixStarUIBgLocalPos = HL.Field(Vector3)

GachaCharCtrl.m_isInWaitPlayCharTlTime = HL.Field(HL.Boolean) << false

GachaCharCtrl.m_canFastJumpToLoopCurChar = HL.Field(HL.Boolean) << false

GachaCharCtrl.m_curTriggerVoiceKey = HL.Field(HL.Number) << 0


GachaCharCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.fullScreenBtn.onClick:AddListener(function()
        self:_OnClickScreen()
    end)
    self.view.skipBtn.onClick:AddListener(function()
        self:_Skip()
    end)

    self.m_args = args
    self.m_charCount = #self.m_args.chars
    self.view.skipBtn.gameObject:SetActive(self.m_charCount > 1)


    local xRatio = UIManager.uiCanvasRect.rect.size.x / 1920
    local yRatio = UIManager.uiCanvasRect.rect.size.y / 1080
    self.view.transitionsCellMain.localScale = self.view.transitionsCellMain.localScale * math.max(xRatio, yRatio)
end

GachaCharCtrl.OnClose = HL.Override() << function(self)
    if self.m_curCharTLHelper then
        self.m_curCharTLHelper:OnDispose()
    end
end


GachaCharCtrl._PlayCharacterAt = HL.Method(HL.Number) << function(self, index)
    logger.info("################################ GachaTest: PlayCharacterAt ################################")
    self.m_curIndex = index
    self.m_curInfo = self.m_args.chars[index]
    self.m_lastSkipTime = 0
    self.m_startTimerId = self:_ClearTimer(self.m_startTimerId)
    self.m_start6StarNameUITimerId = self:_ClearTimer(self.m_start6StarNameUITimerId)
    logger.info("GachaCharCtrl._PlayCharacterAt", index, self.m_curInfo)
    if index == 1 then
        self.m_sixStarUIBgLocalPos = self.m_phase.m_roomObjItem.view.sixStarUIBg.transform.localPosition
    end
    self:_PlayStarAnimation()

    self.m_canFastJumpToLoopCurChar = not self.m_curInfo.isNew or self.view.config["NEW_CHAR_CAN_JUMP_LOOP_RARITY_" .. self.m_curInfo.rarity]
end


GachaCharCtrl._PlayStarAnimation = HL.Method() << function(self)
    logger.info("GachaTest: PlayStarAnimation")

    self.m_state = GachaState.UIStar
    self:_SetContentVisible(false)
    self.view.starNode.gameObject:SetActive(true)
    self.m_phase.m_roomObjItem.view.charInfo3DUI.gameObject:SetActive(false)

    local rarity = self.m_curInfo.rarity

    local ani = StarAnimations[rarity]
    self.view.starNode:Play(ani)
    self.view.bottomMaskBeforePlayTL.gameObject:SetActive(true)
    self.m_phase.m_roomObjItem.view.sixStarUIBg.gameObject:SetActive(false)


    AudioManager.PostEvent(StarAudios[rarity])


    local charId = self.m_curInfo.charId
    local gachaCharData = Tables.gachaCharInfoTable[charId]
    local path = string.format(UIConst.GACHA_CHAR_TIMELINE_PATH, gachaCharData.timelineAssetName, gachaCharData.timelineAssetName)
    local time = Time.unscaledTime
    self.loader:LoadGameObjectAsync(path, function()
        logger.info("抽卡角色预载完成", Time.unscaledTime - time, charId)
    end)


    if rarity >= 6 then
        local charCfg = Tables.characterTable[charId]
        local name = charCfg.name
        local sixStarUIBg = self.m_phase.m_roomObjItem.view.sixStarUIBg
        sixStarUIBg.charNameTxt.text = name
        sixStarUIBg.professionIconImg:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, CharInfoUtils.getCharProfessionIconName(charCfg.profession))
        sixStarUIBg.charIonImg:LoadSprite(UIConst.UI_SPRITE_CHAR_INFO, UIConst.UI_CHAR_INFO_CHAR_BG_PREFIX .. charId)
        local color = UIUtils.getColorByString(gachaCharData.uiBgColor)
        sixStarUIBg.colorCanvas.color = color
    end



    self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)
    self.m_startTimelineCor = self:_StartCoroutine(function()
        local delayTime = self.view.config["TIMELINE_DELAY_TIME_" .. rarity]
        local tween = self.view.starNode.curTween
        local clipLength = self.view.starNode:GetClipLength(ani)
        while true do
            coroutine.step()
            local tweenValue = tween:GetValue()
            if tweenValue >= 1 or tweenValue * clipLength >= delayTime then
                logger.info("GachaTest: startTimelineCor playTimeline")
                self:_PlayTimeline(false)
                return
            end
        end
    end)


    self.m_phase.m_roomObjItem.view.sceneLight6Rarity.gameObject:SetActive(rarity >= 6)
    self.m_phase.m_roomObjItem.view.sceneLight5Rarity.gameObject:SetActive(rarity == 5)
    self.m_phase.m_roomObjItem.view.sceneLight4Rarity.gameObject:SetActive(rarity <= 4)

    self.m_phase.m_roomObjItem.view.sceneEffect6Rarity.gameObject:SetActive(rarity >= 6)
end

GachaCharCtrl._PlayTimeline = HL.Method(HL.Boolean) << function(self, jumpToLoop)
    logger.info("GachaTest: playTimeline")

    self.view.bottomMaskBeforePlayTL.gameObject:SetActive(false)

    self.m_state = GachaState.CharEnter


    local charId = self.m_curInfo.charId
    local gachaCharData = Tables.gachaCharInfoTable[charId]
    local path = string.format(UIConst.GACHA_CHAR_TIMELINE_PATH, gachaCharData.timelineAssetName, gachaCharData.timelineAssetName)
    local prefab = self.loader:LoadGameObject(path)
    local charObj = CSUtils.CreateObject(prefab, self.m_phase.m_roomObjItem.view.timelineRoot)
    self.m_curCharObj = charObj
    self.m_curCharTLHelper = GachaCharTLHelper(charObj.transform, {
        onLoopChanged = function(isLoop)
            if isLoop and self.m_state ~= GachaState.CharLoop then
                self:_ShowContent()
            end
        end,
    })
    if self.m_curCharTLHelper.showContentTime > 0 then
        self.m_curCharTLHelper:SetTriggerOnceTimer(self.m_curCharTLHelper.showContentTime, function()
            if self.m_state ~= GachaState.CharLoop then
                self:_ShowContent()
            end
        end)
    end


    local lightPrefab = self.loader:LoadGameObject(string.format("Assets/Beyond/DynamicAssets/Gameplay/Prefabs/CharInfo/AdditionalLights/light_%s.prefab", charId))
    local lightObj = CSUtils.CreateObject(lightPrefab, charObj.transform)
    local uiModelMono = charObj.gameObject:GetComponentInChildren(typeof(CS.Beyond.Gameplay.View.CharUIModelMono), true)
    lightObj.gameObject:SetActive(true)
    lightObj.transform:DoActionOnChildren(function(childTrans)
        local isTarget = childTrans.name == "light_overview"
        childTrans.gameObject:SetActive(isTarget)
        if isTarget then
            uiModelMono:InitLightFollower(childTrans)
        end
    end)
    local volumePrefab = self.loader:LoadGameObject(string.format("Assets/Beyond/DynamicAssets/Gameplay/Prefabs/CharInfo/CameraTracks/track_%s.prefab", charId))
    local volumeObj = CSUtils.CreateObject(volumePrefab, charObj.transform)
    volumeObj.gameObject:SetActive(true)
    volumeObj.transform:DoActionOnChildren(function(childTrans)
        local isVolume = childTrans.name == "VolumeModifiers"
        childTrans.gameObject:SetActive(isVolume)
        if isVolume then
            childTrans:DoActionOnChildren(function(volumeTrans)
                local isTargetVolume = volumeTrans.name == "volume_overview"
                volumeTrans.gameObject:SetActive(isTargetVolume)
                if isTargetVolume then
                    local mod = volumeTrans:GetComponent(typeof(CS.Beyond.Gameplay.CharInfoVolumeCameraBiasModifier))
                    if mod then
                        local globalVolume = self.m_phase.m_roomObjItem.view.charOverrideVolume
                        mod:UseDataOnVolume(globalVolume)
                    end
                end
            end)
        end
    end)

    charObj:SetLayerRecursive(UIConst.GACHA_LAYER)
    if self.m_curCharTLHelper.m_exCamera ~= nil then
        local uiBgTrans = self.m_phase.m_roomObjItem.view.sixStarUIBg.transform
        uiBgTrans:SetParent(self.m_curCharTLHelper.m_exCamera)
        uiBgTrans.localPosition = self.m_sixStarUIBgLocalPos
        uiBgTrans.localEulerAngles = Vector3.zero
    end

    self.m_startTimerId = self:_ClearTimer(self.m_startTimerId)
    self.m_start6StarNameUITimerId = self:_ClearTimer(self.m_start6StarNameUITimerId)
    self.m_isInWaitPlayCharTlTime = true
    if jumpToLoop then
        logger.info("GachaTest: playTimeline jumpToLoop")
        self.m_curCharTLHelper:JumpToLoopSection(0.5)
        self.m_isInWaitPlayCharTlTime = false
    else
        logger.info("GachaTest: playTimeline SampleToBeginning")
        self.m_curCharTLHelper:SampleToBeginning()

        self.m_startTimerId = self:_StartTimer(self.view.config.WAIT_BLACK_SCREEN_FADE_PLAY_TL_TIME, function()
            if self.m_curCharTLHelper then
                self.m_curCharTLHelper:PlayFromStart()
                self.m_isInWaitPlayCharTlTime = false
                if self.m_curInfo.rarity >= 6 then
                    local uiDelayTime = self.view.config.SIX_STAR_UI_BG_DELAY_TIME
                    if self.m_curCharTLHelper.showNameTime > 0 then
                        uiDelayTime = self.m_curCharTLHelper.showNameTime
                    end
                    self.m_start6StarNameUITimerId = self:_StartTimer(uiDelayTime, function()
                        self:_Show6StarNameUI()
                    end)
                end
            end
        end)
    end

    if not jumpToLoop then
        self.m_curTriggerVoiceKey = Utils.triggerVoice("introduce", self.m_curInfo.charId)
        logger.info("Gacha introduce 1", self.m_curInfo.charId)
    end
end

GachaCharCtrl._ShowContent = HL.Method() << function(self)
    logger.info("GachaTest: showContent")

    self.m_startTimerId = self:_ClearTimer(self.m_startTimerId)
    self.m_start6StarNameUITimerId = self:_ClearTimer(self.m_start6StarNameUITimerId)

    self.m_state = GachaState.CharLoop

    local info = self.m_curInfo
    local charId = info.charId
    local charCfg = Tables.characterTable[charId]

    self.view.nameTxt:SetPhoneticText(GEnums.PhoneticType.CharNamePhonetic, charId)
    self.view.nameTxtShadow.text = charCfg.name
    self.view.professionIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, CharInfoUtils.getCharProfessionIconName(charCfg.profession))
    self.view.charElementIcon:InitCharTypeIcon(charCfg.charTypeId)
    self.view.elementTxt.text = Tables.CharTypeTable[charCfg.charTypeId].name
    self.view.newHintNode.gameObject:SetActive(info.isNew)
    self.view.starGroup:InitStarGroup(self.m_curInfo.rarity)
    self.view.dialogTxt.gameObject:SetActive(false)


    AudioManager.PostEvent(LoopAudios[self.m_curInfo.rarity])

    local firstItemBundle = info.items[1]
    if firstItemBundle then

        self:_UpdateItemCell(self.view.wpnRewardNode, firstItemBundle.id, firstItemBundle.count)

        local extraCount = #info.items - 1
        local extraRewardNode = self.view.extraRewardNode
        if extraCount > 0 then

            extraRewardNode.gameObject:SetActive(true)
            extraRewardNode.titleRewardTxt.text = Language.ui_GachaCharPanel_repeat_reward
            if not extraRewardNode.m_extraItemCells then
                extraRewardNode.m_extraItemCells = UIUtils.genCellCache(extraRewardNode.extraItemCell)
            end
            extraRewardNode.m_extraItemCells:Refresh(extraCount, function(cell, index)
                local bundle = info.items[index + 1]
                self:_UpdateItemCell(cell, bundle.id, bundle.count)
            end)
        elseif info.firstGetExtraItem then

            extraRewardNode.gameObject:SetActive(true)
            extraRewardNode.titleRewardTxt.text = Language.LUA_GACHA_CHAR_EXTRA_REWARD_FIRST_GET_TITLE
            if not extraRewardNode.m_extraItemCells then
                extraRewardNode.m_extraItemCells = UIUtils.genCellCache(extraRewardNode.extraItemCell)
            end
            extraRewardNode.m_extraItemCells:Refresh(1, function(cell, index)
                self:_UpdateItemCell(cell, info.firstGetExtraItem.id, info.firstGetExtraItem.count)
            end)
        else
            extraRewardNode.gameObject:SetActive(false)
        end
    else
        self.view.wpnRewardNode.gameObject:SetActive(false)
        self.view.extraRewardNode.gameObject:SetActive(false)
    end

    self:_SetContentVisible(true)

    local ui3d = self.m_phase.m_roomObjItem.view.charInfo3DUI
    ui3d.nameTxt.text = charCfg.engName
    ui3d.gameObject:SetActive(true)
    local isPadScreen = Screen.width / Screen.height < CS.Beyond.UI.UIConst.STANDARD_RATIO - 0.1
    local uiPosNode = self.m_curCharObj.transform:Find(isPadScreen and "UIPosNodePad" or "UIPosNode")
    if not uiPosNode and isPadScreen then
        logger.error("抽卡角色展示没有为Pad设置的名字位置", charId)
        uiPosNode = self.m_curCharObj.transform:Find("UIPosNode")
    end
    if uiPosNode then
        ui3d.gameObject.transform.position = uiPosNode.transform.position
    else
        logger.error("No UIPosNode In", self.m_curCharObj.transform:PathFromRoot())
    end

    if not self.m_isSkipped then
        self.view.skipBtn.gameObject:SetActive(self.m_curIndex < self.m_charCount)
    end
end

GachaCharCtrl._SetContentVisible = HL.Method(HL.Boolean) << function(self, isShow)

    self.view.contentNodeCanvasGroup.alpha = isShow and 1 or 0
    self.view.contentNodeAnimationWrapper:ClearTween()

    if isShow then
        self.view.contentNodeAnimationWrapper:PlayInAnimation()
    end
end

GachaCharCtrl._Show6StarNameUI = HL.Method() << function(self)
    local sixStarUIBg = self.m_phase.m_roomObjItem.view.sixStarUIBg
    sixStarUIBg.gameObject:SetActive(true)

    local worldParams = CS.Beyond.UI.UICanvasScaleHelper.CalcWorldCanvasParams(CameraManager.mainCamera, sixStarUIBg.transform, true)
    sixStarUIBg.transform.localScale = worldParams.uiRootScale;
    sixStarUIBg.transform.sizeDelta = worldParams.uiRootSize;

    sixStarUIBg.animationWrapper:Play("gacha_3d_char_in_bg", function()
        self.m_phase.m_roomObjItem.view.sixStarUIBg.gameObject:SetActive(false)
    end)
end

GachaCharCtrl._UpdateItemCell = HL.Method(HL.Table, HL.String, HL.Number) << function(self, cell, itemId, count)
    local itemData = Tables.itemTable[itemId]
    cell.itemIcon:InitItemIcon(itemId)
    cell.countTxt.text = string.format("×%d", count)
    cell.rarityImg.color = UIUtils.getItemRarityColor(itemData.rarity)
end

GachaCharCtrl.m_lastSkipTime = HL.Field(HL.Number) << 0

GachaCharCtrl._OnClickScreen = HL.Method() << function(self)

    if Time.unscaledTime < self.m_lastSkipTime + self.view.config.SKIP_CD then
        return
    end

    if self.m_state ~= GachaState.CharLoop and not self.m_canFastJumpToLoopCurChar then
        return
    end

    if self.m_state == GachaState.UIStar then
        logger.info("GachaTest: click state == UIStar")
        self.m_lastSkipTime = Time.unscaledTime
        self.view.starNode.gameObject:SetActive(false)
        self:_PlayTimeline(true)
        self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)
        local curIndex = self.m_curIndex
        self.m_introduceVoiceTimerId = self:_ClearTimer(self.m_introduceVoiceTimerId)
        self.m_introduceVoiceTimerId = self:_StartTimer(self.view.config.SKIP_CD + 0.1, function()
            if curIndex == self.m_curIndex then
                self.m_curTriggerVoiceKey = Utils.triggerVoice("introduce", self.m_curInfo.charId)
                logger.info("[GachaCharCtrl]audioEvent: char audio")
            end
        end)
        self.m_phase.m_roomObjItem.view.sixStarUIBg.animationWrapper:ClearTween(true)
    elseif self.m_state == GachaState.CharEnter then
        logger.info("GachaTest: click state == CharEnter")
        self.m_lastSkipTime = Time.unscaledTime
        local needTransitionGuaranteeTime = self.m_curInfo.rarity <= 4 and not self.m_isInWaitPlayCharTlTime
        if needTransitionGuaranteeTime and self.view.config.TRANSITION_GUARANTEE_TIME_RARITY_4 > (self.m_curCharTLHelper.m_loopStartTime - self.m_curCharTLHelper:GetTime()) then
            logger.info("GachaTest: click and protect time")
            self:_ShowContent()
        else
            logger.info("GachaTest: click and jump")
            self.m_curCharTLHelper:JumpToLoopSection(0.5)
        end
        self.m_phase.m_roomObjItem.view.sixStarUIBg.animationWrapper:ClearTween(true)
    elseif self.m_state == GachaState.CharLoop then
        logger.info("GachaTest: click state == CharLoop")
        if self.view.starNode.curState == CS.Beyond.UI.UIConst.AnimationState.Stop then
            self.m_lastSkipTime = Time.unscaledTime
            self:_GoToNext()
        end
    end
end


GachaCharCtrl._GoToNext = HL.Method() << function(self)
    logger.info("GachaCharCtrl._GoToNext")




    self:_ClearCurAsset()

    VoiceManager:StopResponse(self.m_curTriggerVoiceKey)
    logger.info("[GachaCharCtrl]audioEvent: _GoToNext stop pre trigger voice")


    local newIndex
    for k = self.m_curIndex + 1, self.m_charCount do
        local info = self.m_args.chars[k]
        if not self.m_isSkipped or info.isNew or info.rarity == UIConst.CHAR_MAX_RARITY then
            newIndex = k
            break
        end
    end
    if newIndex then
        self:_PlayCharacterAt(newIndex)
    else
        self:_ShowResult()
    end
end


GachaCharCtrl._ClearCurAsset = HL.Method() << function(self)
    if not self.m_curCharObj then
        return
    end

    local uiBgTrans = self.m_phase.m_roomObjItem.view.sixStarUIBg.transform
    uiBgTrans:SetParent(self.m_phase.m_roomObjItem.view.transform)

    self.m_startTimerId = self:_ClearTimer(self.m_startTimerId)
    self.m_start6StarNameUITimerId = self:_ClearTimer(self.m_start6StarNameUITimerId)
    self.m_curCharTLHelper:OnDispose()
    self.m_curCharTLHelper = nil
    GameObject.Destroy(self.m_curCharObj)
    self.m_curCharObj = nil
end


GachaCharCtrl._Skip = HL.Method() << function(self)
    logger.info("GachaCharCtrl._Skip")

    self.m_isSkipped = true
    self.view.skipBtn.gameObject:SetActive(false)

    if self.m_curInfo.isNew or self.m_curInfo.rarity == UIConst.CHAR_MAX_RARITY then
        self:_OnClickScreen()
    else
        self:_GoToNext()
    end
end

GachaCharCtrl._ShowResult = HL.Method() << function(self)
    logger.info("GachaCharCtrl._ShowResult")
    self.m_introduceVoiceTimerId = self:_ClearTimer(self.m_introduceVoiceTimerId)
    VoiceManager:StopResponse(self.m_curTriggerVoiceKey)
    logger.info("[GachaCharCtrl] audioEvent: _ShowResult stop pre trigger voice")
    self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)

    Notify(MessageConst.ON_ENABLE_ACHIEVEMENT_TOAST, UIConst.ACHIEVEMENT_TOAST_DISABLE_KEY.GachaChar)

    if #self.m_args.chars == 10 then

        UIManager:Open(PanelId.GachaCharResultBG)
        local resultCtrl = UIManager:Open(PanelId.GachaCharResult, {
            chars = self.m_args.chars,
        })
        UIManager:Open(PanelId.GachaCharResultTop, {
            chars = self.m_args.chars,
            onComplete = function()
                UIManager:Close(PanelId.GachaCharResult)
                UIManager:Close(PanelId.GachaCharResultBG)
                self:_ShowRewardsAndExit()
            end
        })
        if resultCtrl then
            resultCtrl:InitControllerHintBar()
        end
    else
        self:_ShowRewardsAndExit()
    end
end

GachaCharCtrl._ShowRewardsAndExit = HL.Method() << function(self)
    logger.info("GachaCharCtrl._ShowRewardsAndExit")

    if not self.m_args.fromGacha then
        self:_Exit()
        return
    end

    local itemMap = {}
    local firstGetCharReward
    for _, v in ipairs(self.m_args.chars) do
        for __, bundle in ipairs(v.items) do
            if itemMap[bundle.id] then
                itemMap[bundle.id] = itemMap[bundle.id] + bundle.count
            else
                itemMap[bundle.id] = bundle.count
            end
        end

        if v.firstGetExtraItem then
            if not firstGetCharReward then
                firstGetCharReward = {
                    id = v.firstGetExtraItem.id,
                    count = v.firstGetExtraItem.count,
                }
            else
                firstGetCharReward.count = firstGetCharReward.count + v.firstGetExtraItem.count
            end
        end
    end
    local itemList = {}
    for id, count in pairs(itemMap) do
        local info = {id = id, count = count}
        local itemData = Tables.itemTable[id]
        info.sortId1 = itemData.sortId1
        info.sortId2 = itemData.sortId2
        info.rarity = itemData.rarity
        table.insert(itemList, info)
    end
    table.sort(itemList, Utils.genSortFunction("sortId1", "sortId2", "rarity"))

    self:_Exit()

    if next(itemList) then
        local rewardArg = {
            items = itemList,
            onComplete = function()
                Notify(MessageConst.ON_ONE_GACHA_POOL_REWARD_FINISHED)
            end,
        }

        local extraItems = {}

        if not string.isEmpty(self.m_args.poolId) then
            local poolCfg = Tables.gachaCharPoolTable[self.m_args.poolId]
            if not string.isEmpty(poolCfg.bonusRewardIdEveryPull) then
                local rewardEveryPull = UIUtils.getRewardItems(poolCfg.bonusRewardIdEveryPull)
                table.insert(extraItems, {
                    extraTitle = Language.LUA_GACHA_JOINT_POOL_EVERY_PULL_REWARD_TOAST_TITLE,
                    titleColorState = "Red",
                    item = {
                        id = rewardEveryPull[1].id,
                        count = rewardEveryPull[1].count * self.m_charCount,
                    },
                })
            end
        end

        if firstGetCharReward then
            table.insert(extraItems, {
                extraTitle = Language.LUA_GACHA_FIRST_GET_CHAR_REWARD_TOAST_TITLE,
                titleColorState = "Yellow",
                item = firstGetCharReward,
            })
        end

        if #extraItems > 0 then
            rewardArg.extraItems = extraItems
        end


        Notify(MessageConst.GACHA_POOL_ADD_SHOW_REWARD, {
            queueRewardType = "GachaResultReward",
            showRewardFunc = function()
                Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardArg)
            end,
        })
    end
end



GachaCharCtrl._Exit = HL.Method() << function(self)
    logger.info("GachaCharCtrl._Exit")

    local onComplete = self.m_args.onComplete
    if self.m_args.seamlessExit then


        if onComplete then
            onComplete()
        end
        PhaseManager:ExitPhaseFast(PhaseId.GachaChar)
    else
        PhaseManager:PopPhase(PhaseId.GachaChar)
        if onComplete then
            onComplete()
        end
    end
end


HL.Commit(GachaCharCtrl)
