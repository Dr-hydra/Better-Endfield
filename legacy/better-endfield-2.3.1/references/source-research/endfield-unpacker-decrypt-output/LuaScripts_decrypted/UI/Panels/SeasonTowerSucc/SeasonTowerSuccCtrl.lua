local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerSucc
local PHASE_ID = PhaseId.SeasonTowerSucc

local EQUIP_INDEX_LIST = {
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.BODY,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_1,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.HAND,
    UIConst.CHAR_INFO_EQUIP_SLOT_MAP.EDC_2,
}


SeasonTowerSuccCtrl = HL.Class('SeasonTowerSuccCtrl', uiCtrl.UICtrl)

SeasonTowerSuccCtrl.m_system = HL.Field(CS.Beyond.Gameplay.SeasonTowerSystem)

SeasonTowerSuccCtrl.m_dungeonId = HL.Field(HL.String) << ""

SeasonTowerSuccCtrl.m_charInstIdList = HL.Field(HL.Table)

SeasonTowerSuccCtrl.m_charCellCache = HL.Field(HL.Forward("UIListCache"))

SeasonTowerSuccCtrl.m_isNewRecord = HL.Field(HL.Boolean) << false

SeasonTowerSuccCtrl.m_time = HL.Field(HL.Number) << 0

SeasonTowerSuccCtrl.m_finishTs = HL.Field(HL.Number) << 0

SeasonTowerSuccCtrl.m_completeExtraTask = HL.Field(HL.Boolean) << false

SeasonTowerSuccCtrl.m_isRankUp = HL.Field(HL.Boolean) << false

SeasonTowerSuccCtrl.m_rank = HL.Field(HL.Number) << 0

SeasonTowerSuccCtrl.s_pendingRankUp = HL.StaticField(HL.Number) << 0

SeasonTowerSuccCtrl.s_messages = HL.StaticField(HL.Table) << {
}

SeasonTowerSuccCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_system = GameInstance.player.seasonTowerSystem
    self.m_dungeonId = arg.dungeonId or ""
    self.m_time = arg.time or 0
    self.m_finishTs = arg.finishTs or 0
    self.m_isNewRecord = arg.isNewRecord or false
    self.m_completeExtraTask = arg.completeExtraTask or false

    local pendingRank = SeasonTowerSuccCtrl.s_pendingRankUp
    if pendingRank > 0 then
        self.m_isRankUp = true
        self.m_rank = pendingRank
        SeasonTowerSuccCtrl.s_pendingRankUp = 0
    end

    self:_BindUI()
    self:_CollectCharInstIds()
    self:_RefreshInfo()
    self:_RefreshFormation()

    if self.m_isRankUp then
        self:_RefreshRankUp()
        self.animationWrapper:ClearTween(false)
        self.animationWrapper:PlayWithTween("seasontowersucc_firstpopup_in", nil, CS.Beyond.UI.UIConst.AnimationState.In)
        AudioManager.PostEvent("Au_UI_Popup_WarEchoSword_PromotionCompleted")
    else
        AudioManager.PostEvent("Au_UI_Popup_WarEchoSword_Completed")
    end
end

SeasonTowerSuccCtrl.OnClose = HL.Override() << function(self)
    AudioManager.PostEvent("Au_UI_Menu_WarEchoUnmuteChar")
end

SeasonTowerSuccCtrl._BindUI = HL.Method() << function(self)
    self.view.shareBtn.onClick:AddListener(function()
        self:_OnShareBtnClick()
    end)
    self.view.againBtn.onClick:AddListener(function()
        self:_OnAgainBtnClick()
    end)
    self.view.doneBtn.onClick:AddListener(function()
        self:_OnDoneBtnClick()
    end)

    self.m_charCellCache = UIUtils.genCellCache(self.view.roleInfoCell)

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end
end

SeasonTowerSuccCtrl._CollectCharInstIds = HL.Method() << function(self)
    self.m_charInstIdList = {}
    local squadSlots = GameInstance.player.squadManager.curSquad.slots
    for i = 0, squadSlots.Count - 1 do
        local slot = squadSlots[i]
        if slot.charInstId and slot.charInstId > 0 then
            table.insert(self.m_charInstIdList, slot.charInstId)
        end
    end
end

SeasonTowerSuccCtrl._RefreshInfo = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local gameCfg = Tables.gameMechanicTable[self.m_dungeonId]
    if dungeonCfg then
        self.view.nameText.text = dungeonCfg.dungeonName or ""
        local diffText = ""
        if gameCfg.difficulty == 1 then
            diffText = Language.ui_seasontowerdungeonentrypanel_level_1
        elseif gameCfg.difficulty == 2 then
            diffText = Language.ui_seasontowerdungeonentrypanel_level_2
        elseif gameCfg.difficulty == 3 then
            diffText = Language.ui_seasontowerdungeonentrypanel_level_3
        end
        self.view.difficultyTxt.text = diffText
        local levelCfg = Tables.seasonTowerGameGroupTable[gameCfg.gameGroupId]
        self.view.monsterImg:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, levelCfg.iconBanner)
    end

    local seasonCfg = nil
    if self.m_system.currentSeasonId > 0 and self.m_system.currentWeekId > 0 then
        local seasonData = Tables.seasonTowerTable[self.m_system.currentSeasonId]
        if seasonData then
            seasonCfg = seasonData.weeks[self.m_system.currentWeekId]
        end
    end
    self.view.weekNameText.text = seasonCfg and seasonCfg.weekShowName or ""

    local min = math.floor(self.m_time / 60)
    local sec = self.m_time % 60
    self.view.timeText.text = string.format(Language.LUA_SEASON_TOWER_BEST_TIME_FORMAT, min, sec)

    self.view.newRecordNode.gameObject:SetActive(self.m_isNewRecord)

    local _,_,extraCount = GameWorld.subGameManager:TryGetSubGameTaskCount(self.m_dungeonId)
    if extraCount and extraCount > 0 then
        self.view.extraNode.gameObject:SetActive(true)

        local _,result = GameWorld.subGameManager:TryGetExtraTaskExtraInfo(self.m_dungeonId, 0)
        if result.useSingleDescription then
            self.view.extraTaskText.text = result.singleDescription:GetText()
        else
            for _, aim in cs_pairs(result.trackingInfoDict) do
                self.view.extraTaskText.text = aim.description:GetText()
                break
            end
        end
        if self.m_completeExtraTask then
            self.view.extraNodeStateController:SetState("Complete")
        else
            self.view.extraNodeStateController:SetState("Unlock")
        end
    else
        self.view.extraNode.gameObject:SetActive(false)
    end
end

SeasonTowerSuccCtrl._RefreshFormation = HL.Method() << function(self)
    local charCount = #self.m_charInstIdList
    self.m_charCellCache:Refresh(Const.BATTLE_SQUAD_MAX_CHAR_NUM, function(cell, luaIndex)
        if luaIndex > charCount then
            cell.stateController:SetState("Empty")
            return
        end

        cell.stateController:SetState("Nrl")
        local charInstId = self.m_charInstIdList[luaIndex]
        self:_SetupCharCell(cell, charInstId)
    end)
end

SeasonTowerSuccCtrl._SetupCharCell = HL.Method(HL.Any, HL.Number) << function(self, cell, charInstId)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if not charInfo then
        cell.stateController:SetState("Empty")
        return
    end

    local templateId = charInfo.templateId
    cell.roleImg:LoadSprite(UIConst.UI_SPRITE_GACHA_CHAR, templateId)
    cell.roleLvTxt.text = tostring(charInfo.level)
    cell.potentialStar:InitCharPotentialStarByLevel(charInfo.potentialLevel)

    self:_SetupWeaponNode(cell.weapoNode, charInstId)
    self:_SetupEquipNode(cell.equipNode, charInfo)
end

SeasonTowerSuccCtrl._SetupWeaponNode = HL.Method(HL.Any, HL.Number) << function(self, weapoNode, charInstId)
    local weaponData = CharInfoUtils.getCharCurWeapon(charInstId)
    if not weaponData then
        weapoNode.canvasGroup.alpha = 0
        return
    end
    weapoNode.canvasGroup.alpha = 1

    local weaponInst = weaponData.weaponInst
    weapoNode.weaponLvTxt.text = tostring(weaponInst.weaponLv)

    local weaponItemCfg = Tables.itemTable:GetValue(weaponData.weaponCfg.weaponId)
    weapoNode.weaponIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, weaponItemCfg.iconId)

    UIUtils.setItemRarityImage(weapoNode.weapoQualityImg, weaponItemCfg.rarity)

    weapoNode.potentialStar:InitCharPotentialStarByLevel(weaponInst.refineLv)


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
        local attrCell = weapoNode[attrCellName]
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

SeasonTowerSuccCtrl._SetupEquipNode = HL.Method(HL.Any, HL.Any) << function(self, equipNode, charInfo)
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
                local itemCfg = Tables.itemTable:GetValue(equipTemplateId)
                equipCell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
                equipCell.equipEnhanceLevelNode:InitEquipEnhanceLevelNode({
                    equipInstId = equipInstId,
                })
                UIUtils.setItemRarityImage(equipCell.qualityImg, itemCfg.rarity)
            end
        end
    end
end

SeasonTowerSuccCtrl._RefreshRankUp = HL.Method() << function(self)
    local rankStateName = SeasonTowerUtils.getRankStateName(self.m_rank)
    self.view.rankupPopup.rankTag:SetState(rankStateName)

    local res, rankData = Tables.seasonTowerRankTable:TryGetValue(self.m_rank)
    if res then
        self.view.rankupPopup.levelNameTxt.text = rankData.rankName
        self.view.rankupPopup.rankImage:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, rankData.pic)
    else
        self.view.rankupPopup.levelNameTxt.text = ""
    end
end

SeasonTowerSuccCtrl._OnShareBtnClick = HL.Method() << function(self)
    self.view.main:SetState("Share")

    Notify(MessageConst.SHOW_COMMON_SHARE_PANEL, {
        type = "EchoesOfWar",
        onCaptureEnd = function()
            self.view.main:SetState("NoShare")
        end,
        onClose = function()
        end,
        timeStamp = Utils.appendUTC(Utils.timestampToDateYMDHM(self.m_finishTs)),
    })
end

SeasonTowerSuccCtrl._OnAgainBtnClick = HL.Method() << function(self)
    if SeasonTowerUtils.getShouldRefresh() then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SEASON_TOWER_UPDATE_NOTIFY,
            hideCancel = true,
            onConfirm = function()
                GameInstance.dungeonManager:LeaveDungeon()
            end})
        return
    end
    PhaseManager:PopPhase(PHASE_ID)
    GameInstance.dungeonManager.curDungeonLikeSubGame:SendReStart(true)
end

SeasonTowerSuccCtrl._OnDoneBtnClick = HL.Method() << function(self)
    GameInstance.dungeonManager:LeaveDungeon()
end

SeasonTowerSuccCtrl.OnSettlement = HL.StaticMethod(HL.Any) << function(args)
    SeasonTowerSuccCtrl.ShowSettlement(args)
end

SeasonTowerSuccCtrl.ShowSettlement = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonSettlement", function()
        if not Utils.isInDungeon() then
            logger.error(ELogChannel.Dungeon, "error, try to open SeasonTowerSucc out of dungeon")
            return
        end

        PhaseManager:ExitPhaseFastTo(PhaseId.Level)

        local dungeonId, time, isNewRecord, completeExtraTask, finishTs = unpack(args)
        PhaseManager:OpenPhase(PHASE_ID, {
            dungeonId = dungeonId,
            time = time,
            isNewRecord = isNewRecord,
            completeExtraTask = completeExtraTask,
            finishTs = finishTs,
        })
    end, function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
end

SeasonTowerSuccCtrl.OnRankUpgraded = HL.StaticMethod(HL.Any) << function(arg)
    local newRank = unpack(arg)
    SeasonTowerSuccCtrl.s_pendingRankUp = newRank
end

HL.Commit(SeasonTowerSuccCtrl)
