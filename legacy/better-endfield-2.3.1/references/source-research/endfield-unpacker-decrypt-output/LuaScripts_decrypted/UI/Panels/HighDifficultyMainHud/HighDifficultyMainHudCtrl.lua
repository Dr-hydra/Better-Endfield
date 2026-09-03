local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HighDifficultyMainHud
local PHASE_ID = PhaseId.HighDifficultyMainHud

HighDifficultyMainHudCtrl = HL.Class('HighDifficultyMainHudCtrl', uiCtrl.UICtrl)






HighDifficultyMainHudCtrl.s_messages = HL.StaticField(HL.Table) << {

}

HighDifficultyMainHudCtrl.m_listCells = HL.Field(HL.Any)

HighDifficultyMainHudCtrl.m_seriesCount = HL.Field(HL.Number) << 0

HighDifficultyMainHudCtrl.m_allSeries = HL.Field(HL.Table)

HighDifficultyMainHudCtrl.m_firstCell = HL.Field(HL.Any)

HighDifficultyMainHudCtrl.m_focusCell = HL.Field(HL.Any)

HighDifficultyMainHudCtrl.m_initSeriesId = HL.Field(HL.String) << ""

HighDifficultyMainHudCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

HighDifficultyMainHudCtrl.m_latestSeries = HL.Field(HL.Table)

local MAX_SHOW_COUNT = 3

local CELL_GRADUALLY_SHOW_TIME = 0.1


HighDifficultyMainHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    args = args or {}
    self.m_fromDialog = args.fromDialog or false


    local activityId = HighDifficultyUtils.getHighDifficultyActivityId()
    local activityExist = false
    if activityId and GameInstance.player.activitySystem:GetActivity(activityId) then
        activityExist = true
        self.view.goBtn.onClick:AddListener(function()
            PhaseManager:GoToPhase(PhaseId.ActivityCenter,  { activityId = activityId, gotoCenter = true })
        end)
        self.view.redDot:InitRedDot("HighDifficultyMainHudGotoBtn")
    else
        self.view.goBtn.gameObject:SetActive(false)
    end


    self.m_latestSeries = {}
    if activityExist then
        local seriesIds = HighDifficultyUtils.GetLatestSeriesIds()
        for _, seriesId in ipairs(seriesIds) do
            self.m_latestSeries[seriesId] = true
        end
    end


    self.m_initSeriesId = args.seriesId or ""
    self.m_seriesCount = 0
    self.m_allSeries = {}
    local ids = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
    self.m_seriesCount = ids.Count
    for i = 1,ids.Count do
        local seriesId = ids[CSIndex(i)]
        table.insert(self.m_allSeries,{
            seriesId = seriesId,
            sortId = Tables.HighDifficultySeriesTable[seriesId].sortId,
        })
    end
    table.sort(self.m_allSeries, Utils.genSortFunction({"sortId"}, false))


    if self.m_seriesCount > MAX_SHOW_COUNT then
        self.view.main:SetState("Multiple")
    else
        self.view.main:SetState("Single")
    end


    self.m_listCells = UIUtils.genCellCache(self.view.cellNode)
    self.m_listCells:Refresh(math.max(MAX_SHOW_COUNT, self.m_seriesCount), function(cellNode, index)

        cellNode.itemChallengeCell.gameObject:SetActive(false)
        self:_StartCoroutine(function()
            coroutine.wait(CELL_GRADUALLY_SHOW_TIME * (index - 1))
            cellNode.itemChallengeCell.gameObject:SetActive(true)
            self:_UpdateCell(cellNode.itemChallengeCell, index)
        end)
    end)
    self.view.levelsScrollList.horizontalNormalizedPosition = 0


    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end
end


HighDifficultyMainHudCtrl._UpdateCell = HL.Method(HL.Any,HL.Number) << function(self,cell,index)

    if string.isEmpty(self.m_initSeriesId) and index == 1 and not self.m_firstCell then
        self.m_firstCell = cell
        self:SetNaviTarget(cell.cellNaviDeco)
    end
    cell.cellNaviDeco.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_focusCell = cell
        end
    end


    if index > self.m_seriesCount then
        cell.nodeState:SetState("EmptyNode")
        return
    end


    local seriesId = self.m_allSeries[index].seriesId
    if seriesId == self.m_initSeriesId then
        self.m_firstCell = cell
        self:SetNaviTarget(cell.cellNaviDeco)
    end
    local unlocked = GameInstance.player.highDifficultySystem:IsHighDiffilcultySeriesUnlock(seriesId)
    if unlocked and index <= self.m_seriesCount then
        cell.nodeState:SetState("NormalNode")
    else
        cell.nodeState:SetState("EmptyNode")
        return
    end


    local _, seriesCfg = Tables.HighDifficultySeriesTable:TryGetValue(seriesId)
    cell.nameTxt.text = seriesCfg.name
    local path = UIConst.UI_SPRITE_ACTIVITY
    local name = seriesCfg.bgImg
    cell.bgImg:LoadSprite(path,name)
    cell.redDot:InitRedDot("HighDifficultyMainHudCell", seriesId)


    local achievementId = seriesCfg.achieveId
    cell.dungeonMedalCell:InitCommonMedalNode(achievementId)


    if DeviceInfo.usingTouch then
        cell.normalNode:SetState("Mobile")
    else
        cell.normalNode:SetState("Standatone")
    end


    local dungeonInfo = HighDifficultyUtils.GetSeriesInfo(seriesId)
    cell.levelCountNode:SetState("TotalLevel_" .. tostring(#dungeonInfo))
    local allPassed = true
    for i = 1,#dungeonInfo do
        local raidUnlocked = dungeonInfo[i].raidUnlocked
        local raidPassed = dungeonInfo[i].raidPassed
        if not raidUnlocked or not raidPassed then
            allPassed = false
        end
        local stateController = "levelsState"..tostring(i)
        cell[stateController]:SetState(not raidUnlocked and "Normal" or raidPassed and "Raid" or "Ordinary" )
    end
    if allPassed then
        cell.sliderState:SetState("FinishNode")
    else
        cell.sliderState:SetState("ConductNode")
    end


    cell.clickBtn.onClick:RemoveAllListeners()
    cell.clickBtn.onClick:AddListener(function()
        local dungeonSeriesId = seriesId
        local enterDungeonCallback
        enterDungeonCallback = function(enterDungeonId)
            LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
        end
        Notify(MessageConst.ON_OPEN_DUNGEON_ENTRY_PANEL, {
            dungeonSeriesId,
            enterDungeonCallback,
        })
        HighDifficultyUtils.setFalseNewHighDifficultySeries(seriesId)
    end)


    cell.eventOpen.gameObject:SetActive(self.m_latestSeries[seriesId] or false)
end

HighDifficultyMainHudCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if active and DeviceInfo.usingController then
        self:SetNaviTarget(self.m_focusCell and self.m_focusCell.cellNaviDeco or self.m_firstCell.cellNaviDeco)
    end
end

HL.Commit(HighDifficultyMainHudCtrl)
