












local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaWeapon

local GachaStage = {
    UIStar = 1,
    CloseUp = 2,
    Reveal = 3,
}

local StarAnimations = {
    [4] = "gacha_char_start_4",
    [5] = "gacha_char_start_5",
    [6] = "gacha_char_start_6",
}

local StarAudios = {
    [4] = "Au_UI_Gacha_Star4_weapon",
    [5] = "Au_UI_Gacha_Star5_weapon",
    [6] = "Au_UI_Gacha_Star6_weapon",
}

local UIRarityColorConfigName = {
    [4] = "RARITY_COLOR_4",
    [5] = "RARITY_COLOR_5",
    [6] = "RARITY_COLOR_6",
}

local LoopAudios = {
    [4] = "Au_UI_Gacha_Weaponshow4",
    [5] = "Au_UI_Gacha_Weaponshow5",
    [6] = "Au_UI_Gacha_Weaponshow6",
}

local WeaponRootName = {
    [GEnums.WeaponType.Sword] = "swordRoot",
    [GEnums.WeaponType.Wand] = "wandRoot",
    [GEnums.WeaponType.Claymores] = "claymoresRoot",
    [GEnums.WeaponType.Lance] = "lanceRoot",
    [GEnums.WeaponType.Pistol] = "pistolRoot",
}


GachaWeaponCtrl = HL.Class('GachaWeaponCtrl', uiCtrl.UICtrl)




GachaWeaponCtrl.m_args = HL.Field(HL.Table)

GachaWeaponCtrl.m_curInfo = HL.Field(HL.Table)

GachaWeaponCtrl.m_curIndex = HL.Field(HL.Number) << -1

GachaWeaponCtrl.m_rarityEffect = HL.Field(HL.Table)

GachaWeaponCtrl.m_rarityEffectRoot = HL.Field(Transform)



GachaWeaponCtrl.m_isSkipped = HL.Field(HL.Boolean) << false

GachaWeaponCtrl.m_lastSkipTime = HL.Field(HL.Number) << 0

GachaWeaponCtrl.m_stage = HL.Field(HL.Number) << -1



GachaWeaponCtrl.m_weaponCount = HL.Field(HL.Number) << -1

GachaWeaponCtrl.m_curWeaponType = HL.Field(GEnums.WeaponType)

GachaWeaponCtrl.m_weaponRootCache = HL.Field(HL.Table)

GachaWeaponCtrl.m_curWeaponObjList = HL.Field(HL.Table)

GachaWeaponCtrl.m_modelRequestList = HL.Field(HL.Table)



GachaWeaponCtrl.m_startTimelineCor = HL.Field(HL.Thread)



GachaWeaponCtrl.m_weaponTlInfos = HL.Field(HL.Table)

GachaWeaponCtrl.m_curPlayTlInfo = HL.Field(HL.Table)

GachaWeaponCtrl.m_isInLoopTrack = HL.Field(HL.Boolean) << false

GachaWeaponCtrl.m_updateCheckTlKey = HL.Field(HL.Number) << -1





GachaWeaponCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaWeaponCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    self.view.fullScreenBtn.onClick:AddListener(function()
        self:_OnClickScreen()
    end)
    self.view.skipBtn.onClick:AddListener(function()
        self:_Skip()
    end)

    self.m_args = args
    self.m_weaponCount = #self.m_args.weapons
    self.m_modelRequestList = {}
    self.m_curWeaponObjList = {}

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaWeaponCtrl._PlayWeaponAt = HL.Method(HL.Number) << function(self, index)
    self.m_curIndex = index
    self.m_curInfo = self.m_args.weapons[index]
    self.m_lastSkipTime = 0
    logger.info("GachaWeaponCtrl._PlayWeaponAt", index, self.m_curInfo)
    self:_PlayStarAnimationStage()
end




GachaWeaponCtrl._PlayStarAnimationStage = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._PlayStarAnimationStage")
    self.m_stage = GachaStage.UIStar

    self.view.contentNode.gameObject:SetActive(false)
    self.view.starNode.gameObject:SetActive(true)
    self.m_phase.m_displayObjItem.view.charInfo3DUI.gameObject:SetActive(false)
    self:_HideAllRarityEffect()

    local info = self.m_curInfo
    local weaponId = info.weaponId
    local weaponCfg = Utils.tryGetTableCfg(Tables.weaponBasicTable, weaponId)
    local weaponType = weaponCfg.weaponType
    self.m_curWeaponType = weaponType
    self:_LoadWeaponModelAsset(weaponId, weaponType)
    self:_LoadWeaponTimeline()

    local rarity = self.m_curInfo.rarity
    local ani = StarAnimations[rarity]
    self.view.starNode:Play(ani)

    local delayTime = rarity >= 6 and self.view.config["TIMELINE_DELAY_TIME_" .. rarity] or self.view.config["UI_DELAY_TIME_" .. rarity]
    local tween = self.view.starNode.curTween
    local clipLength = self.view.starNode:GetClipLength(ani)
    self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)
    self.m_startTimelineCor = self:_StartCoroutine(function()
        if rarity < 6 then

            local time = self.m_curPlayTlInfo.loopStartTime
            self:_SetTimeline(time, false)
        end
        while true do
            coroutine.step()
            local tweenProgress = tween:GetValue()
            if tweenProgress >= 1 or tweenProgress * clipLength >= delayTime then
                if PhaseManager:IsOpen(PhaseId.GachaWeapon) then
                    if rarity >= 6 then
                        self:_PlayWeaponCloseUpStage()
                    else
                        local time = self.m_curPlayTlInfo.loopStartTime
                        self:_SetTimeline(time, true)
                        self:_PlayRevealStage()
                    end
                end
                return
            end
        end
    end)

    AudioManager.PostEvent(StarAudios[info.rarity])
end

GachaWeaponCtrl._PlayWeaponCloseUpStage = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._PlayWeaponCloseUpStage")
    self.m_stage = GachaStage.CloseUp
    self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)

    self:_SetTimeline(0, true)

    self.m_updateCheckTlKey = LuaUpdate:Add("TailTick", function(deltaTime)
        self:_TailTickCheckTimelineStartLoop(function()
            self:_PlayRevealStage()
        end)
    end)
end

GachaWeaponCtrl._PlayRevealStage = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._PlayRevealStage")
    self.m_stage = GachaStage.Reveal
    self.m_startTimelineCor = self:_ClearCoroutine(self.m_startTimelineCor)

    local info = self.m_curInfo
    local weaponId = info.weaponId
    local weaponCfg = Utils.tryGetTableCfg(Tables.weaponBasicTable, weaponId)
    local weaponName = UIUtils.getItemName(weaponId)
    local weaponTypeName = UIUtils.getItemTypeName(weaponId)
    local weaponEngName = weaponCfg.engName
    local weaponType = weaponCfg.weaponType

    self.view.nameTxt.text = weaponName
    self.view.nameShadowTxt.text = weaponName
    self.view.professionIcon:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponType:ToInt())
    self.view.elementTxt.text = weaponTypeName
    self.view.newHintNode.gameObject:SetActive(info.isNew)
    self.view.starGroup:InitStarGroup(info.rarity)

    local extraRewardNode = self.view.extraRewardNode
    if info.items and next(info.items) then
        local extraCount = #info.items
        extraRewardNode.gameObject:SetActive(true)
        if not extraRewardNode.m_extraItemCells then
            extraRewardNode.m_extraItemCells = UIUtils.genCellCache(extraRewardNode.extraItemCell)
        end
        extraRewardNode.m_extraItemCells:Refresh(extraCount, function(cell, index)
            local bundle = info.items[index]
            self:_UpdateItemCell(cell, bundle.id, bundle.count)
        end)
    else
        extraRewardNode.gameObject:SetActive(false)
    end

    self.view.contentNode.gameObject:SetActive(true)

    local ui3d = self.m_phase.m_displayObjItem.view.charInfo3DUI
    if ui3d then
        ui3d.nameTxt.text = weaponEngName
        ui3d.gameObject:SetActive(true)
    end

    AudioManager.PostEvent(LoopAudios[info.rarity])

    local colorName = UIRarityColorConfigName[info.rarity]
    self.view.lightImg.color = self.view.config[colorName]

    self:_ShowRarityEffect(info.rarity)

    if not self.m_isSkipped then
        self.view.skipBtn.gameObject:SetActive(self.m_curIndex < self.m_weaponCount)
    end
end

GachaWeaponCtrl._JumpToRevealStage = HL.Method() << function(self)
    self.m_updateCheckTlKey = LuaUpdate:Remove(self.m_updateCheckTlKey)
    local time = self.m_curPlayTlInfo.loopStartTime
    self:_SetTimeline(time, true)
    self:_PlayRevealStage()
end

GachaWeaponCtrl._SetTimeline = HL.Method(HL.Number, HL.Boolean) << function(self, time, isPlay)
    local tlInfo = self.m_curPlayTlInfo
    local dir = tlInfo.mainDir
    dir.time = time
    if isPlay then
        dir:Play()
    else
        dir:Evaluate()
    end
end

GachaWeaponCtrl._ShowRarityEffect = HL.Method(HL.Number) << function(self, rarity)
    if self.m_rarityEffect == nil then
        self:_LoadRarityEffect()
    end

    for key, obj in pairs(self.m_rarityEffect) do
        if key == rarity then
            obj:SetActive(true)
        else
            obj:SetActive(false)
        end
    end
end

GachaWeaponCtrl._HideAllRarityEffect = HL.Method() << function(self)
    if self.m_rarityEffect == nil then
        self:_LoadRarityEffect()
    end

    for _, obj in pairs(self.m_rarityEffect) do
        obj:SetActive(false)
    end
end

GachaWeaponCtrl._UpdateItemCell = HL.Method(HL.Table, HL.String, HL.Number) << function(self, cell, itemId, count)
    local itemData = Tables.itemTable[itemId]
    cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    cell.countTxt.text = string.format("×%d", count)
    cell.rarityImg.color = UIUtils.getItemRarityColor(itemData.rarity)
end

GachaWeaponCtrl._Exit = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._Exit")

    local onComplete = self.m_args.onComplete
    PhaseManager:ExitPhaseFast(PhaseId.GachaWeapon)
    if onComplete then
        onComplete()
    end
end





GachaWeaponCtrl._InitAllWeaponRoot = HL.Method() << function(self)
    self.m_weaponRootCache = {}

    for weaponType, rootName in pairs(WeaponRootName) do
        local root = self.m_phase.m_displayObjItem.view.weaponRoot[rootName]
        if root then
            local rootList = self.m_weaponRootCache[weaponType]
            if not rootList then
                rootList = {}
                self.m_weaponRootCache[weaponType] = rootList
            end

            local subRootCount = root.childCount
            if subRootCount <= 0 then

                subRootCount = 1
                table.insert(rootList, root)
            else

                for i = 0, subRootCount - 1 do
                    local child = root:GetChild(i)
                    table.insert(rootList, child)
                end
            end
        else
            logger.error("Not found weapon root, name：" .. rootName)
        end
    end
end

GachaWeaponCtrl._InitAllTimeline = HL.Method() << function(self)
    self.m_weaponTlInfos = {}

    for weaponType, rootName in pairs(WeaponRootName) do
        local cutscene = self.m_phase.m_displayObjItem.view.timelineRoot[rootName]
        if cutscene then

            local dirInfo = {
                tlCutscene = cutscene,
                camRoot = nil,
                mainDir = nil,
                dirList = {},

                actorDir = nil,
                loopStartTime = 0,
                loopEndTime = 0,
            }

            cutscene.gameObject:SetActive(false)
            local rootTrans = cutscene.transform
            dirInfo.mainDir = cutscene.director
            table.insert(dirInfo.dirList, dirInfo.mainDir)

            dirInfo.camRoot = rootTrans:Find("ExternalCamera")
            if not dirInfo.camRoot then
                logger.error("【武器抽卡】没找到timeline对应相机：", dirInfo.actorDir.transform:PathFromRoot())
            end

            for k = 0, rootTrans.childCount - 1 do
                local child = rootTrans:GetChild(k)
                if child.name == "Actor" then
                    local suc, actorDir = child:TryGetComponent(typeof(CS.UnityEngine.Playables.PlayableDirector))
                    if suc then
                        dirInfo.actorDir = actorDir
                        table.insert(dirInfo.dirList, actorDir)
                        break
                    else
                        logger.error("【武器抽卡】没有找到Actor Playable Director，武器类型：", weaponType)
                    end
                end
            end
            local suc, timeInfo = CS.Beyond.Gameplay.Core.TimelineUtils.GetClipTimeInfo(dirInfo.actorDir, "Loop Track", "LoopPlayableClip")
            if suc then
                dirInfo.loopStartTime = timeInfo.x
                dirInfo.loopEndTime = timeInfo.y
            else
                logger.error("【武器抽卡】没找到LoopTrack", dirInfo.actorDir.transform:PathFromRoot())
            end

            self.m_weaponTlInfos[weaponType] = dirInfo
        else
            logger.error("Not found weapon timeline root, name：" .. rootName)
        end
    end
end

GachaWeaponCtrl._LoadWeaponModelAsset = HL.Method(HL.String, GEnums.WeaponType) << function(self, weaponId, weaponType)
    logger.info("GachaWeaponCtrl._LoadWeaponModelAsset")

    local hasValue, modelPath = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponModelByTemplateId(weaponId, false)
    if not hasValue then
        logger.error("[武器抽卡] 没找到武器模型路径, id：" .. weaponId)
        return
    end

    if self.m_weaponRootCache == nil then
        self:_InitAllWeaponRoot()
    end
    local subRootList = self.m_weaponRootCache[weaponType]
    if subRootList == nil then
        logger.error("[武器抽卡] subRootList == nil, weaponType:" .. weaponType)
        return
    end
    local subRootCount = #subRootList
    for i = 1, subRootCount do
        local subRoot = subRootList[i]
        self:_LoadModelAsync(i, modelPath, function(modelGo)
            table.insert(self.m_curWeaponObjList, modelGo)
            modelGo:SetLayerRecursive(UIConst.GACHA_LAYER)


            local success, animator = modelGo:TryGetComponent(typeof(CS.UnityEngine.Animator))
            if success then
                animator.enabled = false
            end

            local transform = modelGo.transform
            transform:SetParent(subRoot)
            transform.localPosition = Vector3.zero
            transform.localEulerAngles = Vector3.zero
            transform.localScale = Vector3.one
        end)
    end
end

GachaWeaponCtrl._LoadModelAsync = HL.Method(HL.Number, HL.String, HL.Function) << function(self, loadKey, modelPath, callback)

    local modelManager = GameInstance.modelManager
    local hash = __beyond_calculate_ab_path_hash(modelPath)
    local pathHash = CS.Beyond.Resource.StringPathHash(hash)
    self.m_modelRequestList[loadKey] = modelManager:LoadAsync(pathHash, function(requestId, path, activeModelGo)
        self.m_modelRequestList[loadKey] = nil
        if activeModelGo then
            GameInstance.player.gacha:SetWeaponModelTextureMipmapLevel(activeModelGo, true)
            if callback then
                callback(activeModelGo)
            end
        end
    end)
end

GachaWeaponCtrl._LoadWeaponTimeline = HL.Method() << function(self)
    if self.m_weaponTlInfos == nil then
        self:_InitAllTimeline()
    end
    if self.m_curPlayTlInfo then
        self.m_curPlayTlInfo.tlCutscene.gameObject:SetActive(false)
        self.m_curPlayTlInfo = nil
    end
    local weaponType = self.m_curWeaponType
    local tlInfo = self.m_weaponTlInfos[weaponType]
    tlInfo.tlCutscene.gameObject:SetActive(true)
    self.m_curPlayTlInfo = tlInfo

    local transform = self.m_rarityEffectRoot
    transform:SetParent(tlInfo.camRoot)
    transform.localPosition = Vector3.zero
    transform.localEulerAngles = Vector3.zero
    transform.localScale = Vector3.one

    self:_SetTimeline(0, false)
end

GachaWeaponCtrl._LoadRarityEffect = HL.Method() << function(self)
    local displayView = self.m_phase.m_displayObjItem.view
    self.m_rarityEffectRoot = displayView.rarityEffectRoot
    self.m_rarityEffect = {
        [4] = displayView.rarityEffect4.gameObject,
        [5] = displayView.rarityEffect5.gameObject,
        [6] = displayView.rarityEffect6.gameObject,
    }
end

GachaWeaponCtrl._ClearCurAsset = HL.Method() << function(self)

    local modelManager = GameInstance.modelManager

    for key, obj in pairs(self.m_curWeaponObjList) do
        GameInstance.player.gacha:SetWeaponModelTextureMipmapLevel(obj, false)
        modelManager:Unload(obj)
        self.m_curWeaponObjList[key] = nil
    end

    for key, requestId in pairs(self.m_modelRequestList) do
        modelManager:Cancel(requestId)
        self.m_modelRequestList[key] = nil
    end
end




GachaWeaponCtrl._GoToNext = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._GoToNext")
    self:_ClearCurAsset()

    local newIndex
    for k = self.m_curIndex + 1, self.m_weaponCount do
        local info = self.m_args.weapons[k]
        if not self.m_isSkipped or info.isNew or info.rarity == UIConst.WEAPON_MAX_RARITY then
            newIndex = k
            break
        end
    end
    if newIndex then
        self:_PlayWeaponAt(newIndex)
    else
        self:_Exit()
    end
end

GachaWeaponCtrl._Skip = HL.Method() << function(self)
    logger.info("GachaWeaponCtrl._Skip")

    self.m_isSkipped = true
    self.view.skipBtn.gameObject:SetActive(false)

    if self.m_curInfo.isNew or self.m_curInfo.rarity == UIConst.WEAPON_MAX_RARITY then
        self:_OnClickScreen()
    else
        self:_GoToNext()
    end
end

GachaWeaponCtrl._OnClickScreen = HL.Method() << function(self)
    if Time.unscaledTime < self.m_lastSkipTime + self.view.config.SKIP_CD then
        return
    end
    logger.info("GachaWeaponCtrl._OnClickScreen", self.m_stage)

    if self.m_stage == GachaStage.UIStar then
        self.m_lastSkipTime = Time.unscaledTime
        self.view.starNode:ClearTween(false)
        self.view.starNode.gameObject:SetActive(false)
        self:_JumpToRevealStage()
    elseif self.m_stage == GachaStage.CloseUp then
        self.m_lastSkipTime = Time.unscaledTime
        self:_JumpToRevealStage()
    elseif self.m_stage == GachaStage.Reveal then
        self.m_lastSkipTime = Time.unscaledTime
        self:_GoToNext()
    end
end



GachaWeaponCtrl._TailTickCheckTimelineStartLoop = HL.Method(HL.Function) << function(self, onBecomeLoop)
    if self.m_isInLoopTrack then
        return
    end
    local tlInfo = self.m_curPlayTlInfo
    if tlInfo.actorDir.time >= tlInfo.loopStartTime then
        onBecomeLoop()
        self.m_updateCheckTlKey = LuaUpdate:Remove(self.m_updateCheckTlKey)
    end
end


HL.Commit(GachaWeaponCtrl)
