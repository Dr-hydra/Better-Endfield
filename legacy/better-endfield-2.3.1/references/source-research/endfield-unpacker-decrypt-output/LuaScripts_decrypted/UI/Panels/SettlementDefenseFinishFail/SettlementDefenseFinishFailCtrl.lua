
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseFinishFail
local PHASE_ID = PhaseId.SettlementDefenseFinishFail
SettlementDefenseFinishFailCtrl = HL.Class('SettlementDefenseFinishFailCtrl', uiCtrl.UICtrl)

local ZERO_HP_TOLERANCE = 0.01

SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs = HL.StaticField(HL.Table)

SettlementDefenseFinishFailCtrl.m_coreHpCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseFinishFailCtrl.m_isConfirmed = HL.Field(HL.Boolean) << false

SettlementDefenseFinishFailCtrl.m_tdId = HL.Field(HL.String) << ''





SettlementDefenseFinishFailCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_LOADING_PANEL_CLOSED] = '_OnLoadingPanelClosed',
}


SettlementDefenseFinishFailCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_coreHpCells = UIUtils.genCellCache(self.view.coreHpCell)
    self.m_isConfirmed = false
    self.m_tdId = args.tdId
    GameInstance.player.towerDefenseSystem:RefreshDangerSettlementIds()

    local SettlementDefenseTerminalCtrl = require_ex('UI/Panels/SettlementDefenseTerminal/SettlementDefenseTerminalCtrl')
    SettlementDefenseTerminalCtrl.SettlementDefenseTerminalCtrl.s_lastFailedTdId = self.m_tdId

    self:_InitController()
    self:_InitAction()
    self:_RefreshName(args.tdId)
    self:_RefreshCoreHps(args.coreHpInfoList)
end

SettlementDefenseFinishFailCtrl._OnLoadingPanelClosed = HL.Method() << function(self)
    if PhaseManager:IsOpen(PHASE_ID) then
        PhaseManager:ExitPhaseFast(PHASE_ID)
    end
end

SettlementDefenseFinishFailCtrl._InitAction = HL.Method() << function(self)
    local function close(callback)
        if not self:IsPlayingAnimationIn() then
            if GameInstance.player.squadManager:IsAllDead() and not self.m_isConfirmed then
                GameInstance.gameplayNetwork:SendRevive(true)
                self.m_isConfirmed = true
            end
            PhaseManager:PopPhase(PHASE_ID, function()
                SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs = nil
                GameInstance.player.towerDefenseSystem.systemInDefense = false
                Notify(MessageConst.ON_TOWER_DEFENSE_LEVEL_REWARDS_FINISHED)
                if callback then
                    callback()
                end
            end)
        end
    end
    self.view.btnConfirm.onClick:AddListener(function()
        close()
    end)
    self.view.btnRestart.onClick:AddListener(function()
        close(function()
            GameInstance.player.towerDefenseSystem:EnterPreparingPhase(self.m_tdId)
        end)
    end)
    self.view.btnRestart.gameObject:SetActive(not GameInstance.player.squadManager:IsAllDead())
end

SettlementDefenseFinishFailCtrl._RefreshName = HL.Method(HL.String) << function(self, tdId)
    local name = ''
    local _, tdCfg = Tables.towerDefenseTable:TryGetValue(tdId)
    if tdCfg then
        local _, groupCfg = Tables.towerDefenseGroupTable:TryGetValue(tdCfg.tdGroup)
        if groupCfg then
            name = groupCfg.name
        end
    end
    self.view.nameText.text = name
end

SettlementDefenseFinishFailCtrl._RefreshCoreHps = HL.Method(HL.Table) << function(self, coreHpInfoList)
    local count = coreHpInfoList == nil and 0 or #coreHpInfoList
    self.m_coreHpCells:Refresh(count, function(cell, index)
        local hpInfo = coreHpInfoList[index]
        local amount = hpInfo.hp / hpInfo.maxHp
        cell.indexText.text = string.format("%d", index)
        cell.hpSlider.value = amount
        cell.hpText.text = string.format("%d%%", math.floor(amount * 100))
        local stateName = 'Normal'
        if hpInfo.hp - 0.0 <= ZERO_HP_TOLERANCE then
            stateName = 'Zero'
        elseif amount < self.view.config.HP_RED_THRESHOLD / 100 then
            stateName = 'Low'
        end
        cell.stateController:SetState(stateName)
    end)
end

SettlementDefenseFinishFailCtrl.OnTowerDefenseLevelFinished = HL.StaticMethod(HL.Any) << function(args)
    if not Utils.isInSettlementDefenseDefending() then
        return
    end

    if SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs ~= nil then
        return
    end

    local tdId, finishType = unpack(args)
    local coreHpInfoList = {}
    local towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    if towerDefenseGame ~= nil then
        local tdCoreAbilitySystems = towerDefenseGame.tdCoreAbilitySystems
        if tdCoreAbilitySystems ~= nil and tdCoreAbilitySystems.Count > 0 then
            for index = 0, tdCoreAbilitySystems.Count - 1 do
                local coreAbilitySystem = tdCoreAbilitySystems[index]
                if coreAbilitySystem ~= nil then
                    table.insert(coreHpInfoList, {
                        hp = coreAbilitySystem.hp,
                        maxHp = coreAbilitySystem.maxHp,
                    })
                end
            end
        end
    end

    SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs = {
        tdId = tdId,
        finishType = finishType,
        coreHpInfoList = coreHpInfoList,
    }
end

SettlementDefenseFinishFailCtrl.OnTowerDefenseLevelCleared = HL.StaticMethod() << function()
    if SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs == nil or
        SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs.finishType == CS.Beyond.Gameplay.Core.TowerDefenseGame.FinishType.Complete then
        SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs = nil
        return
    end
    PhaseManager:OpenPhase(PHASE_ID, SettlementDefenseFinishFailCtrl.s_cachedPhaseArgs)
end

SettlementDefenseFinishFailCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

HL.Commit(SettlementDefenseFinishFailCtrl)
