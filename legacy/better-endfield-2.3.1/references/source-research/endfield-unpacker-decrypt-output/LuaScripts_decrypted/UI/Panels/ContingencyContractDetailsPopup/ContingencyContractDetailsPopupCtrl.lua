local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractDetailsPopup

ContingencyContractDetailsPopupCtrl = HL.Class('ContingencyContractDetailsPopupCtrl', uiCtrl.UICtrl)






ContingencyContractDetailsPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}



ContingencyContractDetailsPopupCtrl.m_gameId = HL.Field(HL.String) << ""

ContingencyContractDetailsPopupCtrl.m_tagIds = HL.Field(HL.Table)

ContingencyContractDetailsPopupCtrl.m_tagInfos = HL.Field(HL.Table)

ContingencyContractDetailsPopupCtrl.m_totalScore = HL.Field(HL.Number) << 0

ContingencyContractDetailsPopupCtrl.m_dungeonInfo = HL.Field(HL.Table)

ContingencyContractDetailsPopupCtrl.m_tagEffectCellCache = HL.Field(HL.Forward("UIListCache"))











ContingencyContractDetailsPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self:_InitUI(recoverState)
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()

    self:TryRecoverPopupState(recoverState and recoverState.popupState)
    if arg then
        arg.recoverState = nil
    end
    if #self.m_tagInfos > 0 then
        UIUtils.setAsNaviTarget(self.view.tagEffectListNaviDeco)
    else
        UIUtils.setAsNaviTarget(self.view.descListNaviDeco)
    end
end



ContingencyContractDetailsPopupCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_gameId = arg.gameId
    self.m_tagIds = arg.tagIds

    local dungeonCfg = Tables.dungeonTable[self.m_gameId]
    local dungeonTypeCfg = Tables.dungeonTypeTable[dungeonCfg.dungeonCategory]
    self.m_dungeonInfo = {
        enemyInfoTitle = dungeonTypeCfg.enemyInfoTitle,
        enemyIds = dungeonCfg.enemyIds,
        enemyLevels = dungeonCfg.enemyLevels,
        dungeonDesc = dungeonCfg.dungeonDesc,
        enemyFeatureDesc = dungeonCfg.featureDesc,
    }
end

ContingencyContractDetailsPopupCtrl._UpdateData = HL.Method() << function(self)
    self.m_tagInfos = {}
    self.m_totalScore = 0
    local infosTable = ContingencyContractUtils.GetTagParseInfos(self.m_gameId)
    local allTagInfosMap = infosTable.allTagInfosMap

    local ccSystem = GameInstance.player.contingencyContractSystem
    local ccData = ccSystem:GetCcGameData(self.m_gameId)
    if ccData then
        for _, tagId in pairs(ccData.receivedRewardTagList) do
            allTagInfosMap[tagId].hasFirstPassReward = false
        end
    end
    for i, tagId in pairs(self.m_tagIds) do
        local info = allTagInfosMap[tagId]
        if info ~= nil then
            table.insert(self.m_tagInfos, info)
            self.m_totalScore = self.m_totalScore + info.level
        end
    end
end



ContingencyContractDetailsPopupCtrl._InitUI = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.ContingencyContractDetailsPopup)
    end)

    self.view.enemyDetailBtn.onClick:AddListener(function()
        self:_OpenEnemyDetailsPopup()
    end)

    local SORT_OPTIONS = {
        {
            name = Language.LUA_CONTINGENCY_CONTRACT_SELECT_SORT_NAME_BY_LEVEL,
            keys = { "level", "groupId", "cellIndex"  }
        },
    }

    local sortSelectedIndex = recoverState and recoverState.sortSelectedIndex or 1
    sortSelectedIndex = lume.clamp(sortSelectedIndex, 1, #SORT_OPTIONS)
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental
    if sortIsIncremental == nil then
        sortIsIncremental = false
    end
    self.view.sortNode:InitSortNode(SORT_OPTIONS, function(data, isIncremental)
        self:_SortAndRefreshTagEffectList()
    end, CSIndex(sortSelectedIndex), sortIsIncremental, true)

    self.m_tagEffectCellCache = UIUtils.genCellCache(self.view.tagEffectCell)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ContingencyContractDetailsPopupCtrl._OpenEnemyDetailsPopup = HL.Method(HL.Opt(HL.Number)) << function(self, initSelectEnemyLuaIndex)
    local popupArg = {
        title = self.m_dungeonInfo.enemyInfoTitle,
        enemyIds = self.m_dungeonInfo.enemyIds,
        enemyLevels = self.m_dungeonInfo.enemyLevels,
        isBackBtnStyle = true,
    }
    if initSelectEnemyLuaIndex ~= nil then
        popupArg.initSelectEnemyLuaIndex = initSelectEnemyLuaIndex
    end
    UIManager:AutoOpen(PanelId.CommonEnemyPopup, popupArg)
end

ContingencyContractDetailsPopupCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, enemyPopupCtrl = UIManager:IsOpen(PanelId.CommonEnemyPopup)
    if not isOpen then
        return
    end
    local popupState = {
        popupType = "EnemyDetails",
    }

    if HL.TryGet(enemyPopupCtrl, "GetCurSelectEnemyLuaIndex") then
        local selectedEnemyLuaIndex = enemyPopupCtrl:GetCurSelectEnemyLuaIndex()
        if selectedEnemyLuaIndex > 0 then
            popupState.selectedEnemyLuaIndex = selectedEnemyLuaIndex
        end
    end
    return popupState
end

ContingencyContractDetailsPopupCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end
    if popupState.popupType == "EnemyDetails" then
        local isOpen = UIManager:IsOpen(PanelId.CommonEnemyPopup)
        if isOpen then
            return
        end
        self:_OpenEnemyDetailsPopup(popupState.selectedEnemyLuaIndex)
    end
end

ContingencyContractDetailsPopupCtrl.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex(),
        sortIsIncremental = self.view.sortNode.isIncremental,
        popupState = self:GetRecoverPopupStateArg(),
    }
end

ContingencyContractDetailsPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self.view.levelDetailTxt.text = self.m_dungeonInfo.dungeonDesc
    self.view.enemyDetailTxt.text = self.m_dungeonInfo.enemyFeatureDesc
    self.view.totalScoreTxt.text = self.m_totalScore
    self:_SortAndRefreshTagEffectList()
end

ContingencyContractDetailsPopupCtrl._SortAndRefreshTagEffectList = HL.Method() << function(self)
    local sortData = self.view.sortNode:GetCurSortData()
    local isIncremental = self.view.sortNode.isIncremental
    table.sort(self.m_tagInfos, Utils.genSortFunction(sortData.keys, isIncremental))
    local tagCount = #self.m_tagInfos
    self.m_tagEffectCellCache:Refresh(tagCount, function(cell, luaIndex)
        self:_RefreshTagEffectCell(cell, luaIndex)
    end)
    self.view.rightNode:SetState(tagCount <= 0 and "Empty" or "NotEmpty")
end

ContingencyContractDetailsPopupCtrl._RefreshTagEffectCell = HL.Method(HL.Any, HL.Number) << function(self, inCell, luaIndex)

    local cell = inCell
    local tagInfo = self.m_tagInfos[luaIndex]

    cell.levelTxt.text = tagInfo.level
    cell.tagNameTxt.text = tagInfo.tagFullName
    cell.descTxt.text = tagInfo.desc
    cell.stateController:SetState("Level" .. tagInfo.level)
    cell.rewardIcon.gameObject:SetActive(tagInfo.hasFirstPassReward)
    cell.tagIcon:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, tagInfo.icon)
end


HL.Commit(ContingencyContractDetailsPopupCtrl)
