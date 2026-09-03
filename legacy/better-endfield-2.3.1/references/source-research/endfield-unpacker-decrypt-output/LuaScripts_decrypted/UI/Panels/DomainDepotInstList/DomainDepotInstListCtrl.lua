local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotInstList

DomainDepotInstListCtrl = HL.Class('DomainDepotInstListCtrl', uiCtrl.UICtrl)





DomainDepotInstListCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DomainDepotInstListCtrl.m_instCellGetFunc = HL.Field(HL.Function)

DomainDepotInstListCtrl.m_instIdList = HL.Field(HL.Table)

DomainDepotInstListCtrl.m_resumeState = HL.Field(HL.Table)

DomainDepotInstListCtrl.m_curNaviDepotId = HL.Field(HL.String) << ""


DomainDepotInstListCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.m_resumeState = arg.resumeState
    self.m_instCellGetFunc = UIUtils.genCachedCellFunction(self.view.instList)

    self.view.instList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateInstCell(self.m_instCellGetFunc(obj), LuaIndex(csIndex))
    end)

    self.view.domainMoneyDeco:LoadSprite(UIConst.UI_SPRITE_DOMAIN_DEPOT_BG_MONEY_ICON, Tables.domainDataTable[arg.domainId].domainGoldItemId)

    self.view.depotLimitNumTxt.text = tostring(Utils.getDepotItemStackLimitCount(arg.domainId))

    local domainDepotSystem = GameInstance.player.domainDepotSystem
    local allDepotIdList = domainDepotSystem:GetDomainDepotIdListByDomainId(arg.domainId)
    local depotIdDataList = {}
    for index = 0, allDepotIdList.Count - 1 do
        local depotId = allDepotIdList[index]
        local runtimeData = domainDepotSystem:GetDomainDepotDataById(depotId)
        if runtimeData.level > 0 then
            local tableConfig = Tables.domainDepotTable[depotId]
            table.insert(depotIdDataList, {
                depotId = depotId,
                sortId = tableConfig.sortId,
            })
        end
    end
    table.sort(depotIdDataList, Utils.genSortFunction({ "sortId" }, true))

    self.m_instIdList = {}
    for _, idData in ipairs(depotIdDataList) do
        table.insert(self.m_instIdList, idData.depotId)
    end

    self.m_curNaviDepotId = self.m_instIdList[1] or ""

    self.view.instList:UpdateCount(#self.m_instIdList, true)
end

DomainDepotInstListCtrl.OnShow = HL.Override() << function(self)
    self:_ApplyResumeState(self.m_resumeState)
    self.m_resumeState = nil
end


DomainDepotInstListCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        resumeState = {
            selectedDepotId = self.m_curNaviDepotId,
        }
    }
end


DomainDepotInstListCtrl.ResumeControllerNavi = HL.Method() << function(self)
    self:_ApplyResumeState({ selectedDepotId = self.m_curNaviDepotId })
end

DomainDepotInstListCtrl._OnUpdateInstCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    cell:InitDomainDepotInstCell(self.m_instIdList[index])



    cell:SetOnNaviTargetChanged(function(isNaviTarget)
        if isNaviTarget then
            self.m_curNaviDepotId = self.m_instIdList[index]
        end
    end)
end


DomainDepotInstListCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if #self.m_instIdList <= 0 then
        return
    end
    local targetDepotId = resumeState and resumeState.selectedDepotId or self.m_curNaviDepotId
    local targetIndex = 1
    for index, depotId in ipairs(self.m_instIdList) do
        if depotId == targetDepotId then
            targetIndex = index
            break
        end
    end
    self.m_curNaviDepotId = self.m_instIdList[targetIndex] or ""
    self.view.instList:ScrollToIndex(CSIndex(targetIndex), true)
    if not DeviceInfo.usingController then
        return
    end
    local targetCell = self.m_instCellGetFunc(targetIndex)
    if targetCell ~= nil then
        self:SetNaviTarget(targetCell.view.confirmBtn)
    else
        self.view.instList:ScrollToIndex(CSIndex(targetIndex), true)
    end
end

HL.Commit(DomainDepotInstListCtrl)
