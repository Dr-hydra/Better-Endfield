
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailGasMinePointTeam

MapMarkDetailGasMinePointTeamCtrl = HL.Class('MapMarkDetailGasMinePointTeamCtrl', uiCtrl.UICtrl)






MapMarkDetailGasMinePointTeamCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailGasMinePointTeamCtrl.m_minePointList = HL.Field(HL.Forward("UIListCache"))

MapMarkDetailGasMinePointTeamCtrl.HIGH_PURITY = HL.Field(HL.Number) << 2

MapMarkDetailGasMinePointTeamCtrl.LOW_PURITY = HL.Field(HL.Number) << 1


MapMarkDetailGasMinePointTeamCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_minePointList = UIUtils.genCellCache(self.view.singleMinePoint)
    local markInstId = arg.markInstId

    local commonArgs = {}
    commonArgs.markInstId = markInstId
    commonArgs.descText = ""
    commonArgs.bigBtnActive = true
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)

    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local detail = markRuntimeData.detail
    if detail == nil then
        logger.error("集中矿点详情页没有detail" .. markInstId)
        return
    end

    local itemId = detail.displayItemId
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    self.view.itemDetailBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = itemId,
            transform = self.view.itemIcon.gameObject.transform,
            posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            notPenetrate = true,
        })
    end)

    self:_ProcessMinePoint(markRuntimeData, detail)
end

MapMarkDetailGasMinePointTeamCtrl._ProcessMinePoint = HL.Method(HL.Any, HL.Any) << function(self, markRuntimeData, detail)
    local minePointCount = detail.count
    local levelId = markRuntimeData.levelId
    local levelSuccess, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(levelId)
    if not levelSuccess then
        return
    end

    local developmentLv = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(levelBasicInfo.domainName)
    local infoTable = {}
    for i = 1, minePointCount do
        local index = CSIndex(i)
        local lowLevel = detail.lowPurityLevel[index]
        local highLevel = detail.highPurityLevel[index]
        local logicIdGlobal = detail.coreLogicId[index]
        local state

        if developmentLv < lowLevel then
            state = -lowLevel
        end
        if developmentLv >= lowLevel then
            state = self.LOW_PURITY
        end
        if developmentLv >= highLevel then
            state = self.HIGH_PURITY
        end

        table.insert(infoTable, {
            id = logicIdGlobal,
            state = state,
        })
    end
    table.sort(infoTable, Utils.genSortFunction({"state"}, false))
    local idList = {}
    for i = 1, minePointCount do
        table.insert(idList, infoTable[i].id)
    end
    local minerList = GameInstance.player.mapManager:GetGasMinerList(idList, levelId)

    self.m_minePointList:Refresh(minePointCount, function(minePoint, index)
        local state = infoTable[index].state
        local miner = minerList[CSIndex(index)]
        self:_FillSingleMinePoint(minePoint, state, miner, index)
    end)
end

MapMarkDetailGasMinePointTeamCtrl._FillSingleMinePoint = HL.Method(HL.Any, HL.Number, HL.Any, HL.Number) << function(self, minePoint, state, miner, indexNumber)
    minePoint.indexNumberText.text = indexNumber
    minePoint.lockedRoot.gameObject:SetActive(state < 0)
    minePoint.unlockedRoot.gameObject:SetActive(state > 0)
    if state < 0 then
        minePoint.lockText.text = string.format(Language.LUA_DETAIL_MINE_POINT_TEAM_DEV_UNLOCK_TEXT, -state)
    else
        minePoint.purityLow.gameObject:SetActive(state == self.LOW_PURITY)
        minePoint.purityHigh.gameObject:SetActive(state == self.HIGH_PURITY)
        minePoint.minerDeployed.gameObject:SetActive(miner ~= nil)
        if miner ~= nil then
            local minerState = GameInstance.remoteFactoryManager:QueryBuildingState(miner.belongChapter.chapterId, miner.nodeId, false)
            local activeState = minerState == GEnums.FacBuildingState.Normal or minerState == GEnums.FacBuildingState.Blocked
            minePoint.unlockedRoot:SetState(activeState and "Active" or "Inactive")
        end
    end
end

HL.Commit(MapMarkDetailGasMinePointTeamCtrl)
