local DungeonCommonInfo = require_ex('UI/Widgets/DungeonCommonInfo')

DungeonSeasonTowerCommonInfo = HL.Class('DungeonSeasonTowerCommonInfo', DungeonCommonInfo)

DungeonSeasonTowerCommonInfo.m_charCellCache = HL.Field(HL.Forward("UIListCache"))


DungeonSeasonTowerCommonInfo._OnFirstTimeInit = HL.Override() << function(self)
    DungeonSeasonTowerCommonInfo.Super._OnFirstTimeInit(self)
    self.m_charCellCache = UIUtils.genCellCache(self.view.charCell)
end

DungeonSeasonTowerCommonInfo.RefreshDungeonInfo = HL.Method(HL.String) << function(self, dungeonId)
    DungeonSeasonTowerCommonInfo.Super.RefreshDungeonCommonInfo(self, dungeonId)
    local seasonTowerDungeonCfg = Tables.seasonTowerDungeonTable[dungeonId]
    if not string.isEmpty(seasonTowerDungeonCfg.specialBuffDesc) then
        self.view.specialBuffTxt:SetAndResolveTextStyle(
            CS.Beyond.Gameplay.FormatUtils.FormatBattleText(seasonTowerDungeonCfg.specialBuffDesc, self.m_paramBlackboardFormatData))
        self.view.specialNode.gameObject:SetActive(true)
    else
        self.view.specialNode.gameObject:SetActive(false)
    end

    local res, gameRecord = GameInstance.player.seasonTowerSystem.weekRecord.gameRecords:TryGetValue(dungeonId)

    local _,_,extraCount = GameWorld.subGameManager:TryGetSubGameTaskCount(dungeonId)
    if extraCount and extraCount > 0 then

        local _,result = GameWorld.subGameManager:TryGetExtraTaskExtraInfo(dungeonId, 0)
        if result then
            self.view.extraTask.gameObject:SetActive(true)
            if result.useSingleDescription then
                self.view.extraTaskTxt.text = result.singleDescription:GetText()
            else
                for _, aim in cs_pairs(result.trackingInfoDict) do
                    self.view.extraTaskTxt.text = aim.description:GetText()
                    break
                end
            end
            if res and gameRecord.completeTask then
                self.view.extraTaskState:SetState("Done")
            else
                self.view.extraTaskState:SetState("Nrl")
            end
        else
            self.view.extraTask.gameObject:SetActive(false)
        end
    else
        self.view.extraTask.gameObject:SetActive(false)
    end

    if res then
        self.view.roleNode:SetState("Passed")
        self:_RefreshFormation(gameRecord)
        local bestTimeSec = gameRecord.bestTime
        local min = math.floor(bestTimeSec / 60)
        local sec = bestTimeSec % 60
        self.view.bestTimeTxt.text = string.format(Language.LUA_SEASON_TOWER_BEST_TIME_FORMAT, min, sec)
    else
        self.view.roleNode:SetState("NotPassed")
        self.m_charCellCache:Refresh(4, function(cell, luaIndex)
            cell.stateController:SetState("Empty")
        end)
    end
end

DungeonSeasonTowerCommonInfo._RefreshFormation = HL.Method(CS.Beyond.Gameplay.SeasonTowerSystem.SeasonTowerGameRecord) << function(self, gameRecord)
    local charInfos = gameRecord.characterInfos
    local charCount = charInfos.Count
    self.m_charCellCache:Refresh(4, function(cell, luaIndex)
        if luaIndex <= charCount then
            local charInfo = charInfos[CSIndex(luaIndex)]
            local templateId = CSCharUtils.GetCharTemplateId(charInfo.templateId)
            cell.stateController:SetState("Nrl")

            cell.charHeadCell:InitCharFormationHeadCell({
                templateId = templateId,
                level = charInfo.level,
                potentialLevel = charInfo.potential,
                noHpBar = true,
                selectIndex = -1,
                slotIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM + 1,
            }, nil, true)
        else
            cell.stateController:SetState("Empty")
        end
    end)
end

DungeonSeasonTowerCommonInfo._OpenCharFormation = HL.Override() << function(self)
    if SeasonTowerUtils.getShouldRefresh() then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_SEASON_TOWER_UPDATE_NOTIFY,
            hideCancel = true,
            onConfirm = function()
                PhaseManager:GoToPhase(PhaseId.SeasonTowerMainHud)
            end})
        return
    end
    DungeonSeasonTowerCommonInfo.Super._OpenCharFormation(self)
end

HL.Commit(DungeonSeasonTowerCommonInfo)
return DungeonSeasonTowerCommonInfo

