
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DoodadMineCoreScanUI
DoodadMineCoreScanUICtrl = HL.Class('DoodadMineCoreScanUICtrl', uiCtrl.UICtrl)







DoodadMineCoreScanUICtrl.s_messages = HL.StaticField(HL.Table) << {

}

DoodadMineCoreScanUICtrl.m_doodadMineCoreObjDict = HL.Field(HL.Table)

DoodadMineCoreScanUICtrl.m_doodadMineCoreObjPool = HL.Field(HL.Table)

DoodadMineCoreScanUICtrl.m_doodadMineCoreLogicIdDict = HL.Field(HL.Table)


DoodadMineCoreScanUICtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_doodadMineCoreObjDict = {}
    self.m_doodadMineCoreObjPool = {}
    self.m_doodadMineCoreLogicIdDict = {}
end





DoodadMineCoreScanUICtrl.OnClose = HL.Override() << function(self)
    if self.m_doodadMineCoreObjDict ~= nil then
        for _, v in pairs(self.m_doodadMineCoreObjDict) do
            v.doodadMineCore:Clear()
            GameObject.Destroy(v.doodadMineCore.gameObject)
        end
        self.m_doodadMineCoreObjDict = nil
    end

    if self.m_doodadMineCoreObjPool ~= nil then
        for _, v in ipairs(self.m_doodadMineCoreObjPool) do
            GameObject.Destroy(v.doodadMineCore.gameObject)
        end
        self.m_doodadMineCoreObjPool = nil
    end
end

DoodadMineCoreScanUICtrl._OnAddDoodadMineCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = DoodadMineCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity,coreName, forming, condition = unpack(args)
    ctrl:_AddDoodadMineCore(entity, coreName, forming, condition)
end

DoodadMineCoreScanUICtrl._AddDoodadMineCore = HL.Method(HL.Any, HL.String, HL.Any, HL.String) << function(self, targetObject, coreName, forming, condition)
    if self.m_doodadMineCoreObjDict[targetObject] == nil then
        self.m_doodadMineCoreObjDict[targetObject] = self:_CreateDoodadMineCore()
    end

    self.m_doodadMineCoreObjDict[targetObject].rectTransform.gameObject:SetActive(true)
    local doodadMineCore = self.m_doodadMineCoreObjDict[targetObject].doodadMineCore

    doodadMineCore:SetTarget(targetObject)
    doodadMineCore:SyncConditonInfo(forming, condition)
    self:_InitDoodadMineCore(self.m_doodadMineCoreObjDict[targetObject], targetObject, coreName)
    self.m_doodadMineCoreLogicIdDict[doodadMineCore.entityLogicId] = doodadMineCore
end

DoodadMineCoreScanUICtrl._InitDoodadMineCore = HL.Method(HL.Any, HL.Any, HL.String) << function(self, doodadMineCore, target, coreName)
    if not target or not doodadMineCore then
        return
    end

    doodadMineCore.doodadCoreName:SetText(coreName)
end

DoodadMineCoreScanUICtrl._CreateDoodadMineCore = HL.Method().Return(HL.Table) << function(self)
    if self.m_doodadMineCoreObjPool ~= nil and #self.m_doodadMineCoreObjPool > 0 then
        local result = self.m_doodadMineCoreObjPool[#self.m_doodadMineCoreObjPool]
        table.remove(self.m_doodadMineCoreObjPool, #self.m_doodadMineCoreObjPool)
        return result
    else
        local obj = self:_CreateWorldGameObject(self.view.config.CHAR_DOODAD_MINE_CORE)
        local result = Utils.wrapLuaNode(obj)
        return result
    end
end

DoodadMineCoreScanUICtrl._OnRemoveDoodadMineCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local opened, ctrl = UIManager:IsOpen(PANEL_ID)
    if not opened then
        return
    end
    local ctrl = DoodadMineCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    if ctrl.m_doodadMineCoreObjDict[entity] ~= nil then
        local cell = ctrl.m_doodadMineCoreObjDict[entity]
        cell.doodadMineCore:Clear()
        cell.rectTransform.gameObject:SetActive(false)
        table.insert(ctrl.m_doodadMineCoreObjPool, cell)
        ctrl.m_doodadMineCoreObjDict[entity] = nil
    end
end

DoodadMineCoreScanUICtrl._OnUpdateDoodadMineCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = DoodadMineCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, forming, condition = unpack(args)
    if ctrl.m_doodadMineCoreObjDict[entity] ~= nil then
        local cell = ctrl.m_doodadMineCoreObjDict[entity]
        cell.doodadMineCore:UpdateUI(forming, condition)
    end
end

HL.Commit(DoodadMineCoreScanUICtrl)
