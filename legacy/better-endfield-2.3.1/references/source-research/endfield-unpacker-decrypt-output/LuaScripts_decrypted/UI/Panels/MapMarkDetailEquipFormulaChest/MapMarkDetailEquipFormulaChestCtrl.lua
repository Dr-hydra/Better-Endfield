
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailEquipFormulaChest

MapMarkDetailEquipFormulaChestCtrl = HL.Class('MapMarkDetailEquipFormulaChestCtrl', uiCtrl.UICtrl)






MapMarkDetailEquipFormulaChestCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailEquipFormulaChestCtrl.m_rewardItemCache = HL.Field(HL.Forward("UIListCache"))

MapMarkDetailEquipFormulaChestCtrl.m_markInstId = HL.Field(HL.String) << ""


MapMarkDetailEquipFormulaChestCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitController()

    self.m_rewardItemCache = UIUtils.genCellCache(self.view.item)
    self.m_markInstId = arg.markInstId

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = self.m_markInstId
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)

    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(self.m_markInstId)

    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end
    local rewardId = markRuntimeData.detail.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)

    self.m_rewardItemCache:Refresh(#rewardBundles, function(cell, luaIndex)
        self.view.detailCommon:InitDetailItem(cell, rewardBundles[luaIndex], {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.scrollView,
        })
    end)
end

MapMarkDetailEquipFormulaChestCtrl._InitController = HL.Method() << function(self)
    self.view.itemGridNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

HL.Commit(MapMarkDetailEquipFormulaChestCtrl)
