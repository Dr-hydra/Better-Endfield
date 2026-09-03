local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseMainMap
local PHASE_ID = PhaseId.SettlementDefenseMainMap
SettlementDefenseMainMapCtrl = HL.Class('SettlementDefenseMainMapCtrl', uiCtrl.UICtrl)

local ENEMY_COUNT_FORMAT = "%d/%d"
local CORE_NUMBER_TEXT_FORMAT = "%d"
local CORE_HP_TEXT_FORMAT = "%d%%"

local TOP_CORE_ATTACKED_IN_ANIMATION_NAME = "defense_main_map_top_core_attacked_in"

SettlementDefenseMainMapCtrl.m_towerDefenseGame = HL.Field(HL.Userdata)

SettlementDefenseMainMapCtrl.m_coreInfoCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseMainMapCtrl.m_enemyCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseMainMapCtrl.m_enemyInfoList = HL.Field(HL.Table)

SettlementDefenseMainMapCtrl.m_enemyAbilityCells = HL.Field(HL.Forward("UIListCache"))

SettlementDefenseMainMapCtrl.m_coreDataList = HL.Field(HL.Table)

SettlementDefenseMainMapCtrl.m_selectEnemyIndex = HL.Field(HL.Number) << -1

SettlementDefenseMainMapCtrl.m_hpChangeCallbackList = HL.Field(HL.Table)





SettlementDefenseMainMapCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TOWER_DEFENSE_DEFENDING_ENEMY_KILLED] = '_RefreshEnemyCount',
}


SettlementDefenseMainMapCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    self.m_coreInfoCells = UIUtils.genCellCache(self.view.coreInfoCell)
    self.m_enemyCells = UIUtils.genCellCache(self.view.enemyCell)
    self.m_enemyAbilityCells = UIUtils.genCellCache(self.view.enemyTipsNode.skillCell)
    self.m_hpChangeCallbackList = {}

    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self:BindInputPlayerAction("map_close", function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.enemyTipsNode.fullScreenBtn.onClick:AddListener(function()
        self:_HideEnemyTips()
    end)

    self.view.settlementDefenseMapRoot.gameObject:SetActive(true)
    self.view.settlementDefenseMapRoot:InitSettlementDefenseMapRoot()
    self.view.mapImage.sprite = self.view.settlementDefenseMapRoot.view.map.sprite
    self.view.mapImage:SetNativeSize()

    self:_InitController()
    self:_RefreshEnemyCount()
    self:_RefreshEnemyList()
    self:_InitCoreInfos()
end

SettlementDefenseMainMapCtrl.OnClose = HL.Override() << function(self)
    if self.m_towerDefenseGame ~= nil then
        local coreAbilitySystems = self.m_towerDefenseGame.tdCoreAbilitySystems
        for coreAbilityIndex = 0, coreAbilitySystems.Count - 1 do
            local coreAbilitySystem = coreAbilitySystems[coreAbilityIndex]
            coreAbilitySystem.onHpChange:Remove(self.m_hpChangeCallbackList[coreAbilityIndex])
        end
    end
end




SettlementDefenseMainMapCtrl._InitCoreInfos = HL.Method() << function(self)
    local tdCoreAbilitySystems = self.m_towerDefenseGame.tdCoreAbilitySystems
    if tdCoreAbilitySystems == nil or tdCoreAbilitySystems.Count == 0 then
        return
    end

    self.m_coreDataList = {}
    for index = 0, tdCoreAbilitySystems.Count - 1 do
        local luaIndex = LuaIndex(index)
        local coreAbilitySystem = tdCoreAbilitySystems[index]
        if coreAbilitySystem ~= nil then
            self.m_coreDataList[luaIndex] = {
                hp = coreAbilitySystem.hp,
                maxHp = coreAbilitySystem.maxHp,
            }

            local callback = function(entity, changedHp, isMaxHpChanged)
                self:_OnCoreHpChanged(luaIndex, changedHp)
            end
            self.m_hpChangeCallbackList[index] = callback
            coreAbilitySystem.onHpChange:Add(callback)
        end
    end

    self.m_coreInfoCells:Refresh(tdCoreAbilitySystems.Count, function(cell, index)
        local coreData = self.m_coreDataList[index]
        cell.coreNumber.text = string.format(CORE_NUMBER_TEXT_FORMAT, index)
        cell.hpText.text = string.format(CORE_HP_TEXT_FORMAT, math.floor(coreData.hp / coreData.maxHp * 100))
        if index == tdCoreAbilitySystems.Count then
            cell.endLine.gameObject:SetActive(false)
        end
    end)
end

SettlementDefenseMainMapCtrl._OnCoreHpChanged = HL.Method(HL.Number, HL.Number) << function(self, index, changedHp)
    local coreData = self.m_coreDataList[index]
    local cell = self.m_coreInfoCells:Get(index)
    if coreData == nil or cell == nil then
        return
    end

    local hp = coreData.hp + changedHp
    local percent = hp / coreData.maxHp * 100
    coreData.hp = hp

    cell.hpText.text = string.format(CORE_HP_TEXT_FORMAT, math.floor(percent))

    cell.animationWrapper:ClearTween()
    cell.animationWrapper:PlayWithTween(TOP_CORE_ATTACKED_IN_ANIMATION_NAME)
end






SettlementDefenseMainMapCtrl._RefreshEnemyCount = HL.Method() << function(self)
    self.view.enemyCount.text = string.format(
        ENEMY_COUNT_FORMAT,
        self.m_towerDefenseGame.killedEnemyCount,
        self.m_towerDefenseGame.totalEnemyCount
    )
end

SettlementDefenseMainMapCtrl._RefreshEnemyList = HL.Method() << function(self)
    local tdId = GameInstance.player.towerDefenseSystem.activeTdId
    if string.isEmpty(tdId) then
        return
    end

    local success, tdTableData = Tables.towerDefenseTable:TryGetValue(tdId)
    if not success then
        return
    end

    self.m_enemyInfoList = {}
    local enemyIds = tdTableData.enemyIds
    local enemyLevels = tdTableData.enemyLevels
    for csIndex = 0, enemyIds.Count - 1 do
        local id = enemyIds[csIndex]
        local level = enemyLevels.Count >= enemyIds.Count and enemyLevels[csIndex] or 1
        local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(id, level)
        if enemyInfo ~= nil then
            table.insert(self.m_enemyInfoList, UIUtils.getEnemyInfoByIdAndLevel(id, level))
        end
    end

    self.m_enemyCells:Refresh(#self.m_enemyInfoList, function(cell, index)
        local info = self.m_enemyInfoList[index]
        cell:InitEnemyCell(info, function()
            self:_OnEnemyCellClick(index)
        end)
        cell:SetSelected(false)
    end)
end

SettlementDefenseMainMapCtrl._OnEnemyCellClick = HL.Method(HL.Number) << function(self, index)
    local lastCell = self.m_enemyCells:GetItem(self.m_selectEnemyIndex)
    local currCell = self.m_enemyCells:GetItem(index)
    if lastCell ~= nil then
        lastCell:SetSelected(false)
    end
    if currCell ~= nil then
        currCell:SetSelected(true)
    end
    self.m_selectEnemyIndex = index
    self:_ShowAndRefreshEnemyTips(index)
end

SettlementDefenseMainMapCtrl._ShowAndRefreshEnemyTips = HL.Method(HL.Number) << function(self, index)
    local enemyInfo = self.m_enemyInfoList[index]
    if enemyInfo == nil then
        return
    end

    local enemyTipsNode = self.view.enemyTipsNode
    enemyTipsNode.nameTxt.text = enemyInfo.name
    enemyTipsNode.levelTxt.text = enemyInfo.level or "-"
    enemyTipsNode.enemyImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON, enemyInfo.templateId)
    enemyTipsNode.descTxt:SetAndResolveTextStyle(enemyInfo.desc)

    local abilityList = enemyInfo.ability
    if #abilityList > 0 then
        self.m_enemyAbilityCells:Refresh(#abilityList, function(cell, abilityIndex)
            cell.descTxt:SetAndResolveTextStyle(abilityList[abilityIndex].description)
        end)
        enemyTipsNode.skillNode.gameObject:SetActive(true)
    else
        enemyTipsNode.skillNode.gameObject:SetActive(false)
    end

    enemyTipsNode.animationWrapper:ClearTween(false)
    UIUtils.PlayAnimationAndToggleActive(enemyTipsNode.animationWrapper, true)
end

SettlementDefenseMainMapCtrl._HideEnemyTips = HL.Method() << function(self)
    local currCell = self.m_enemyCells:GetItem(self.m_selectEnemyIndex)
    if currCell ~= nil then
        currCell:SetSelected(false)
    end

    UIUtils.PlayAnimationAndToggleActive(self.view.enemyTipsNode.animationWrapper, false)
end





SettlementDefenseMainMapCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            self:_HideEnemyTips()
        end
    end)
end



HL.Commit(SettlementDefenseMainMapCtrl)
