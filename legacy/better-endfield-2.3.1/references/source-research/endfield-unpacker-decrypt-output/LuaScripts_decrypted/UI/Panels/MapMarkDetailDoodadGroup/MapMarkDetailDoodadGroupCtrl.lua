
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailDoodadGroup

MapMarkDetailDoodadGroupCtrl = HL.Class('MapMarkDetailDoodadGroupCtrl', uiCtrl.UICtrl)







MapMarkDetailDoodadGroupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailDoodadGroupCtrl.m_markInstId = HL.Field(HL.String) << ''

MapMarkDetailDoodadGroupCtrl.m_tickCoroutine = HL.Field(HL.Thread)


MapMarkDetailDoodadGroupCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local detail = markRuntimeData.detail
    if detail == nil then
        logger.error("采集组详情数据中没有detailData   " .. markInstId)
    else
        local itemId = markRuntimeData.detail.displayItemId
        local _, itemData = Tables.itemTable:TryGetValue(itemId)
        self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        self.view.tipsBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                itemId = itemId,
                transform = self.view.itemIcon.gameObject.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            })
        end)
        self:_RenderDoodadPart(markInstId)
    end

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = markInstId
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailDoodadGroupCtrl.OnClose = HL.Override() << function(self)
    self:_StopTickIfNecessary()
end

MapMarkDetailDoodadGroupCtrl._StopTickIfNecessary = HL.Method() << function(self)
    if self.m_tickCoroutine ~= nil then
        self:_ClearCoroutine(self.m_tickCoroutine)
        self.m_tickCoroutine = nil
    end
end

MapMarkDetailDoodadGroupCtrl._RenderDoodadPart = HL.Method(HL.String) << function(self, markInstId)
    self:_StopTickIfNecessary()
    if string.isEmpty(markInstId) then
        return
    end
    self.m_markInstId = markInstId
    self:_RefreshDoodadPart()
    self.m_tickCoroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_RefreshDoodadPart()
        end
    end)
end

MapMarkDetailDoodadGroupCtrl._RefreshDoodadPart = HL.Method() << function(self)
    local doodadSystem = GameInstance.player.doodadSystem
    local doodadGroupData = doodadSystem:GetDoodadSystemDataByMarkInst(self.m_markInstId)
    if doodadGroupData == nil then
        return
    end
    local doodadData = doodadGroupData.msgData
    if doodadData == nil then
        return
    end
    local isMax = doodadGroupData.isMax
    local curCount = doodadGroupData.curCount
    self.view.itemTopState:SetState(isMax and "Max" or "Normal")
    self.view.currentTxt.text = curCount
    self.view.upperLimitTxt.text = doodadGroupData.maxCount
    if isMax then
        return
    end
    self.view.current1Txt.text = curCount
    self.view.upperLimit1Txt.text = curCount + doodadGroupData.nextRefreshCount
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftRefreshTime = doodadGroupData.nextRefreshTime - curServerTime
    leftRefreshTime = math.max(leftRefreshTime, 0)
    self.view.refreshTimeTxt.text = UIUtils.getLeftTimeToSecond(leftRefreshTime)
end

HL.Commit(MapMarkDetailDoodadGroupCtrl)
