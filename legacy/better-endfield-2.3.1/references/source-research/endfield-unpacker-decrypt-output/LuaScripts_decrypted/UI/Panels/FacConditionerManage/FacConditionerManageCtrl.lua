local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacConditionerManage

FacConditionerManageCtrl = HL.Class('FacConditionerManageCtrl', uiCtrl.UICtrl)






FacConditionerManageCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacConditionerManageCtrl.m_data = HL.Field(HL.Any)

FacConditionerManageCtrl.m_getCell = HL.Field(HL.Function)

FacConditionerManageCtrl.m_nameCache = HL.Field(HL.Table)

FacConditionerManageCtrl.m_allCount = HL.Field(HL.Number) << 0

FacConditionerManageCtrl.m_maxCount = HL.Field(HL.Number) << 0

FacConditionerManageCtrl.m_waitingNaviFirst = HL.Field(HL.Boolean) << true


FacConditionerManageCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local onClose
    if arg ~= nil then
        onClose = arg.onClose
    end

    self.view.closeButton.onClick:AddListener(function()
        if onClose then
            onClose()
        end
        self:PlayAnimationOutAndClose()
    end)
    self.view.bgBlack.onClick:AddListener(function()
        if onClose then
            onClose()
        end
        self:PlayAnimationOutAndClose()
    end)

    self.m_nameCache = {}
    self.m_nameCache[GEnums.FCNodeType.BoxValve:GetHashCode()] = UIUtils.getItemName("item_log_conditioner")
    self.m_nameCache[GEnums.FCNodeType.FluidValve:GetHashCode()] = UIUtils.getItemName("item_log_pipe_conditioner")

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)

    self:_UpdateDataAndList()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacConditionerManageCtrl._UpdateDataAndList = HL.Method() << function(self)
    self.m_allCount, self.m_maxCount = FactoryUtils.getValveSpeedLimitedCount()
    self.view.valveNumTxt.text = self.m_allCount .. "/" .. self.m_maxCount
    self.m_data = FactoryUtils.getValveSpeedLimitedInfo()
    if self.m_allCount > 0 then
        self.view.main:SetState("List")
        self.view.scrollList:UpdateCount(#self.m_data)
    else
        self.view.main:SetState("Empty")
        if self.view.naviGroup.IsTopLayer then
            InputManagerInst.controllerNaviManager:TryRemoveLayer(self.naviGroup)
        end
    end
end

FacConditionerManageCtrl._OnValveClosed = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local cellData = self.m_data[index]
    self.m_allCount = self.m_allCount - cellData.count
    self.view.valveNumTxt.text = self.m_allCount .. "/" .. self.m_maxCount
    cellData.count = 0
    cell.limitNumTxt.text = cellData.count
    cell.stateController:SetState("Disable")
end

FacConditionerManageCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local cellData = self.m_data[index]

    local levelCfg = Tables.levelDescTable[cellData.levelId]
    cell.levelTxt.text = levelCfg.showName
    cell.typeTxt.text = self.m_nameCache[cellData.nodeType]
    cell.limitNumTxt.text = cellData.count
    cell.stateController:SetState(cellData.count > 0 and "Enable" or "Disable")

    if self.m_waitingNaviFirst and index == 1 then
        self.m_waitingNaviFirst = false
        self:SetNaviTarget(cell.naviDecorator)
    end

    cell.functionBtn.onClick:RemoveAllListeners()
    cell.functionBtn.onClick:AddListener(function()
        GameInstance.player.remoteFactory.core:Message_BatchCloseValveSpeedLimit(
            ScopeUtil.GetCurrentChapterId(),
            cellData.levelId,
            cellData.nodeType,
            function()
                self:_OnValveClosed(cell, index)
            end)
    end)
end

HL.Commit(FacConditionerManageCtrl)
