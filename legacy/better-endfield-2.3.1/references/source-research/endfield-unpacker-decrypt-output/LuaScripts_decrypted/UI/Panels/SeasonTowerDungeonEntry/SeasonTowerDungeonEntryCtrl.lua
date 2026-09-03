local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerDungeonEntry
local PHASE_ID = PhaseId.SeasonTowerDungeonEntry
local INSTRUCTION_BOOK_ID = "seasontower_level"

local INSTRUCTION_BOOK_POPUP_TYPE = "SeasonTowerInstructionBook"

SeasonTowerDungeonEntryCtrl = HL.Class('SeasonTowerDungeonEntryCtrl', uiCtrl.UICtrl)

SeasonTowerDungeonEntryCtrl.m_system = HL.Field(CS.Beyond.Gameplay.SeasonTowerSystem)

SeasonTowerDungeonEntryCtrl.m_gameGroupId = HL.Field(HL.String) << ""

SeasonTowerDungeonEntryCtrl.m_gameIds = HL.Field(HL.Table)

SeasonTowerDungeonEntryCtrl.m_selectedIndex = HL.Field(HL.Number) << 0
SeasonTowerDungeonEntryCtrl.m_starNum = HL.Field(HL.Number) << 0

SeasonTowerDungeonEntryCtrl.m_enterDungeonCallback = HL.Field(HL.Function)

SeasonTowerDungeonEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
}

SeasonTowerDungeonEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_system = GameInstance.player.seasonTowerSystem
    if arg.dungeonId then
        self.m_gameGroupId = Tables.gameMechanicTable[arg.dungeonId].gameGroupId
    else
        self.m_gameGroupId = arg.levelId
    end

    local groupData = Tables.gameMechanicGroupTable[self.m_gameGroupId]
    self.m_gameIds = {}
    for i = 0, groupData.includeGameMechanicIds.Count - 1 do
        table.insert(self.m_gameIds, groupData.includeGameMechanicIds[i])
    end

    local levelCfg = Tables.seasonTowerGameGroupTable[self.m_gameGroupId]
    self.view.monsterImg:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, levelCfg.iconBanner)

    self.view.nameTxt.text = groupData.gameGroupName
    self.m_enterDungeonCallback = function(enterDungeonId)
        LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
    end
    self.view.seasonTowerDungeonEntryInfo:InitDungeonCommonInfo({
        enterDungeonCallback = self.m_enterDungeonCallback,
        enterConfirmCallback = function()
            if SeasonTowerUtils.getShouldRefresh() then
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_SEASON_TOWER_UPDATE_NOTIFY,
                    hideCancel = true,
                    onConfirm = function()
                        PhaseManager:GoToPhase(PhaseId.SeasonTowerMainHud)
                    end})
                return false
            end
            return true
        end,
    })

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.tipsButton.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, INSTRUCTION_BOOK_ID)
    end)

    self.view.selectBtn1.onClick:AddListener(function() self:_OnSelectBtn(1) end)
    self.view.selectBtn2.onClick:AddListener(function() self:_OnSelectBtn(2) end)
    self.view.selectBtn3.onClick:AddListener(function() self:_OnSelectBtn(3) end)

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end

    local defaultIndex = 1
    if arg.dungeonId and not string.isEmpty(arg.dungeonId) then
        for i, gameId in ipairs(self.m_gameIds) do
            if gameId == arg.dungeonId then
                defaultIndex = i
                break
            end
        end
    else
        for i, gameId in ipairs(self.m_gameIds) do
            if not DungeonUtils.isDungeonUnlock(gameId) then
                break
            end
            defaultIndex = i
        end
    end

    local res, levelData = self.m_system.weekRecord.levelRecords:TryGetValue(self.m_gameGroupId)
    self.m_starNum = levelData and levelData.starNum or 0
    self.view.starList:InitStarGroupWithState(3, self.m_starNum,
        levelData.completeTask and "03" or "02", "01")

    self:_SelectGame(defaultIndex)
    if DeviceInfo.usingController then
        self:SetNaviTarget(self.view[("selectBtn" .. self.m_selectedIndex)])
    end

    UIUtils.bindHyperlinkPopup(self, "DungeonEntry", self.view.inputGroup.groupId)


    self:_TryRecoverPopupState(arg and arg.popupState or nil)
end

SeasonTowerDungeonEntryCtrl._OnSelectBtn = HL.Method(HL.Number) << function(self, index)
    if index == self.m_selectedIndex then
        return
    end
    if index > #self.m_gameIds then
        return
    end
    self:_SelectGame(index)
end

SeasonTowerDungeonEntryCtrl._SelectGame = HL.Method(HL.Number) << function(self, index)
    if index < 1 or index > #self.m_gameIds then
        return
    end

    self.m_selectedIndex = index
    self:_RefreshSelectBtns()

    local gameId = self.m_gameIds[index]
    self.view.seasonTowerDungeonEntryInfo:RefreshDungeonInfo(gameId)
end

SeasonTowerDungeonEntryCtrl._RefreshSelectBtns = HL.Method() << function(self)
    local btnList = { self.view.selectBtn1, self.view.selectBtn2, self.view.selectBtn3 }
    local stateList = { self.view.selectBtnState1, self.view.selectBtnState2, self.view.selectBtnState3 }

    for i = 1, 3 do
        local gameId = self.m_gameIds[i]
        if gameId then
            btnList[i].gameObject:SetActive(true)
            local isUnlock = DungeonUtils.isDungeonUnlock(gameId)
            local isPassed = (i <= self.m_starNum)
            if not isUnlock then
                stateList[i]:SetState("Lock")
            elseif isPassed then
                stateList[i]:SetState("Done")
            else
                stateList[i]:SetState("Nrl")
            end
            if i == self.m_selectedIndex then
                stateList[i]:SetState("Select")
            else

            end
        else
            btnList[i].gameObject:SetActive(false)
        end
    end
end

SeasonTowerDungeonEntryCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Any) << function(self)
    return {
        levelId = self.m_gameGroupId,
        dungeonId = self.m_gameIds[self.m_selectedIndex],
        popupState = self:_GetRecoverPopupStateArg(),
    }
end


SeasonTowerDungeonEntryCtrl._GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end

    local commonInfoPopupState = self.view.seasonTowerDungeonEntryInfo:GetRecoverPopupStateArg()
    if commonInfoPopupState ~= nil then
        return commonInfoPopupState
    end

    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl.id == INSTRUCTION_BOOK_ID then
        return {
            popupType = INSTRUCTION_BOOK_POPUP_TYPE,
        }
    end
end

SeasonTowerDungeonEntryCtrl._TryRecoverPopupState = HL.Method(HL.Opt(HL.Any)) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end
    if popupState.popupType == INSTRUCTION_BOOK_POPUP_TYPE then
        local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isOpen and instructionCtrl.id == INSTRUCTION_BOOK_ID then
            return
        end
        UIManager:Open(PanelId.InstructionBook, INSTRUCTION_BOOK_ID)
        return
    end

    self.view.seasonTowerDungeonEntryInfo:TryRecoverPopupState(popupState)
end

HL.Commit(SeasonTowerDungeonEntryCtrl)
