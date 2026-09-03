
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureActivity

AdventureActivityCtrl = HL.Class('AdventureActivityCtrl', uiCtrl.UICtrl)

AdventureActivityCtrl.m_dataList = HL.Field(HL.Table)

AdventureActivityCtrl.m_getCellFunc = HL.Field(HL.Function)


AdventureActivityCtrl.m_cellRewardCellsDict = HL.Field(HL.Table)





AdventureActivityCtrl.s_messages = HL.StaticField(HL.Table) << {

}


AdventureActivityCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData()
    self:_BindUI()
    self:_RefreshUI()
end

AdventureActivityCtrl.OnShow = HL.Override() << function(self)
    local firstCell = self.m_getCellFunc(self.view.itemScrollList:Get(0))
    if firstCell then
        self:SetNaviTarget(firstCell.normalNode)
    end


    self.view.itemScrollList:UpdateShowingCells(function(index, obj)
        local cell = self.m_getCellFunc(obj)
        local animationWrapper = cell.animationWrapper
        animationWrapper:PlayInAnimation()
    end)
end








AdventureActivityCtrl._InitData = HL.Method() << function(self)
    self.m_dataList = AdventureBookUtils.InitActivityDataList()
end

AdventureActivityCtrl._BindUI = HL.Method() << function(self)
    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.itemScrollList)
    self.view.itemScrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        self:_SetupCellUI(cell, LuaIndex(index))
    end)

    self.m_cellRewardCellsDict = {}
end

AdventureActivityCtrl._RefreshUI = HL.Method() << function(self)
    self.view.itemScrollList:UpdateCount(#self.m_dataList)
end

AdventureActivityCtrl._SetupCellUI = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local data = self.m_dataList[luaIndex]
    cell.nodeState:SetState(data.nodeStateName)
    if data.setUI == false then
        return
    end
    cell.titleTxt.text = data.name

    cell.bgNode.color = UIUtils.getColorByString(data.bgNodeColor)

    cell.titleImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, data.titleImg)

    cell.decoImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, data.decoImg)

    cell.bgImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, data.bgImg)

    local rewardCells = self.m_cellRewardCellsDict[luaIndex]
    if rewardCells == nil then
        rewardCells = UIUtils.genCellCache(cell.itemSmallRewardBlack)
        self.m_cellRewardCellsDict[luaIndex] = rewardCells
    end
    local rewardInfos = data.rewardInfos
    if rewardInfos and #rewardInfos > 0 then
        cell.titleNode2.gameObject:SetActive(true)
        rewardCells:Refresh(#rewardInfos, function(rewardCell, rewardIndex)
            local rewardInfo = rewardInfos[rewardIndex]
            self:SetRewardUI(rewardCell, rewardInfo)
        end)
    else
        cell.titleNode2.gameObject:SetActive(false)
        rewardCells:Refresh(0, nil)
    end

    local redDotName = data.redDotName
    if not string.isEmpty(redDotName) then
        cell.redDot:InitRedDot(redDotName)
    else
        cell.redDot.gameObject:SetActive(false)
    end

    cell.clickBtn.onClick:RemoveAllListeners()
    cell.clickBtn.onClick:AddListener(data.ClickFunc)
end

AdventureActivityCtrl.SetRewardUI = HL.Method(HL.Any, HL.Any) << function(self, cell, rewardInfo)
    cell:InitItem(rewardInfo, function()
        UIUtils.showItemSideTips(cell)
    end)
    cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
end

HL.Commit(AdventureActivityCtrl)
