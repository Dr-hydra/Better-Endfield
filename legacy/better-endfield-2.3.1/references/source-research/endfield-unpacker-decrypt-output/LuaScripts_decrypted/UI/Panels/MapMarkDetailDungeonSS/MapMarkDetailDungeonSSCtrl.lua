
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailDungeonSS

MapMarkDetailDungeonSSCtrl = HL.Class('MapMarkDetailDungeonSSCtrl', uiCtrl.UICtrl)

MapMarkDetailDungeonSSCtrl.m_firstPassRewardItemCache = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailDungeonSSCtrl.m_hunterModeRewardItemCache = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailDungeonSSCtrl.m_markInstId = HL.Field(HL.String) << ""

MapMarkDetailDungeonSSCtrl.m_dungeonSeriesId = HL.Field(HL.String) << ""





MapMarkDetailDungeonSSCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailDungeonSSCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_firstPassRewardItemCache = UIUtils.genCellCache(self.view.firstPassRewardItem)
    self.m_hunterModeRewardItemCache = UIUtils.genCellCache(self.view.hunterModeRewardItem)

    self.m_markInstId = arg.markInstId
    self:_InitDungeonInfo()
    self:_InitController()
end








MapMarkDetailDungeonSSCtrl._InitDungeonInfo = HL.Method() << function(self)
    local markInstId = self.m_markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not getRuntimeDataSuccess then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end

    local detail = markRuntimeData.detail
    local dungeonSeriesId = detail.systemInstId

    self.m_dungeonSeriesId = dungeonSeriesId


    local dungeonId = Tables.dungeonSeriesTable[dungeonSeriesId].includeDungeonIds[0]

    local firstPassRewards = DungeonUtils.genFirstPartRewardsInfo(dungeonId)

    self.m_firstPassRewardItemCache:Refresh(#firstPassRewards, function(item, luaIndex)
        local reward = firstPassRewards[luaIndex]
        self.view.mapMarkDetailCommon:InitDetailItem(item, reward, {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.rewardsNodeRectTransform,
        })
        item.view.rewardedCover.gameObject:SetActive(reward.gained == true)
    end)


    local hunterModeRewards = DungeonUtils.genSecondPartRewardsInfo(dungeonId)
    self.m_hunterModeRewardItemCache:Refresh(#hunterModeRewards, function(item, luaIndex)
        local reward = hunterModeRewards[luaIndex]
        self.view.mapMarkDetailCommon:InitDetailItem(item, reward, {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.rewardsNodeRectTransform,
        })
        item.view.rewardedCover.gameObject:SetActive(reward.gained == true)
    end)

    self:_InitMapMarkDetailCommon(markRuntimeData.isActive)
end

MapMarkDetailDungeonSSCtrl._InitMapMarkDetailCommon = HL.Method(HL.Boolean) << function(self, mapMarkActive)

    local dungeonId = Tables.dungeonSeriesTable[self.m_dungeonSeriesId].includeDungeonIds[0]
    local dungeonCfg = Tables.dungeonTable[dungeonId]

    local commonArgs = {}
    commonArgs.markInstId = self.m_markInstId
    commonArgs.titleText = dungeonCfg.dungeonName
    commonArgs.descText = dungeonCfg.dungeonDesc

    if mapMarkActive then
        commonArgs.leftBtnActive = true
        commonArgs.leftBtnText = Language["ui_mapmarkdetail_button_enter"]
        commonArgs.leftBtnCallback = function()
            PhaseManager:GoToPhase(PhaseId.DungeonEntry, {dungeonSeriesId = self.m_dungeonSeriesId})
        end
        commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.FAST_ENTER

        commonArgs.rightBtnActive = true
    else
        commonArgs.bigBtnActive = true
    end

    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailDungeonSSCtrl._InitController = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self.view.rewardsNode.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

HL.Commit(MapMarkDetailDungeonSSCtrl)
