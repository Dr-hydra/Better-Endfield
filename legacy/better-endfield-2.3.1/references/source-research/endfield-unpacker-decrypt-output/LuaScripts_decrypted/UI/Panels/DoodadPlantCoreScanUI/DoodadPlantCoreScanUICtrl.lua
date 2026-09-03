
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DoodadPlantCoreScanUI
DoodadPlantCoreScanUICtrl = HL.Class('DoodadPlantCoreScanUICtrl', uiCtrl.UICtrl)







DoodadPlantCoreScanUICtrl.s_messages = HL.StaticField(HL.Table) << {

}

DoodadPlantCoreScanUICtrl.m_doodadPlantCoreObjDict = HL.Field(HL.Table)

DoodadPlantCoreScanUICtrl.m_doodadPlantCoreObjPool = HL.Field(HL.Table)

DoodadPlantCoreScanUICtrl.m_doodadPlantCoreLogicIdDict = HL.Field(HL.Table)


DoodadPlantCoreScanUICtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_doodadPlantCoreObjDict = {}
    self.m_doodadPlantCoreObjPool = {}
    self.m_doodadPlantCoreLogicIdDict = {}
end





DoodadPlantCoreScanUICtrl.OnClose = HL.Override() << function(self)
    if self.m_doodadPlantCoreObjDict ~= nil then
        for _, v in pairs(self.m_doodadPlantCoreObjDict) do
            v.doodadPlantCore:Clear()
            GameObject.Destroy(v.doodadPlantCore.gameObject)
        end
        self.m_doodadPlantCoreObjDict = nil
    end

    if self.m_doodadPlantCoreObjPool ~= nil then
        for _, v in ipairs(self.m_doodadPlantCoreObjPool) do
            GameObject.Destroy(v.doodadPlantCore.gameObject)
        end
        self.m_doodadPlantCoreObjPool = nil
    end
end

DoodadPlantCoreScanUICtrl._OnAddDoodadPlantCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = DoodadPlantCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, coreName, size = unpack(args)
    ctrl:_AddDoodadPlantCore(entity, coreName, size)
end

DoodadPlantCoreScanUICtrl._AddDoodadPlantCore = HL.Method(HL.Any, HL.String, HL.String) << function(self, targetObject, coreName, size)
    if self.m_doodadPlantCoreObjDict[targetObject] == nil then
        self.m_doodadPlantCoreObjDict[targetObject] = self:_CreateDoodadPlantCore()
    end

    self.m_doodadPlantCoreObjDict[targetObject].rectTransform.gameObject:SetActive(true)
    local doodadPlantCore = self.m_doodadPlantCoreObjDict[targetObject].doodadPlantCore

    doodadPlantCore:SetTarget(targetObject)
    self:_InitDoodadPlantCore(self.m_doodadPlantCoreObjDict[targetObject], targetObject, coreName, size)
    self.m_doodadPlantCoreLogicIdDict[doodadPlantCore.entityLogicId] = doodadPlantCore
end

DoodadPlantCoreScanUICtrl._InitDoodadPlantCore = HL.Method(HL.Any, HL.Any, HL.String, HL.String) << function(self, doodadPlantCore, target, coreName, size)
    if not target or not doodadPlantCore then
        return
    end

    doodadPlantCore.doodadPlantCore:SetCoreInfo(coreName, size)
end

DoodadPlantCoreScanUICtrl._CreateDoodadPlantCore = HL.Method().Return(HL.Table) << function(self)
    if self.m_doodadPlantCoreObjPool ~= nil and #self.m_doodadPlantCoreObjPool > 0 then
        local result = self.m_doodadPlantCoreObjPool[#self.m_doodadPlantCoreObjPool]
        table.remove(self.m_doodadPlantCoreObjPool, #self.m_doodadPlantCoreObjPool)
        return result
    else
        local obj = self:_CreateWorldGameObject(self.view.config.CHAR_DOODAD_PLANT_CORE)
        local result = Utils.wrapLuaNode(obj)
        return result
    end
end

DoodadPlantCoreScanUICtrl._OnRemoveDoodadPlantCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local opened, ctrl = UIManager:IsOpen(PANEL_ID)
    if not opened then
        return
    end
    local ctrl = DoodadPlantCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    if ctrl.m_doodadPlantCoreObjDict[entity] ~= nil then
        local cell = ctrl.m_doodadPlantCoreObjDict[entity]
        cell.doodadPlantCore:Clear()
        cell.rectTransform.gameObject:SetActive(false)
        table.insert(ctrl.m_doodadPlantCoreObjPool, cell)
        ctrl.m_doodadPlantCoreObjDict[entity] = nil
    end
end

DoodadPlantCoreScanUICtrl._OnUpdateDoodadPlantCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = DoodadPlantCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, name, size = unpack(args)
    if ctrl.m_doodadPlantCoreObjDict[entity] ~= nil then
        local cell = ctrl.m_doodadPlantCoreObjDict[entity]
        cell.doodadPlantCore:SetCoreInfo(name, size)
    end
end

DoodadPlantCoreScanUICtrl._OnRefreshDoodadPlantCoreUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = DoodadPlantCoreScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, current, refreshNum, max, nextRefresh = unpack(args)
    if ctrl.m_doodadPlantCoreObjDict[entity] ~= nil then
        local cell = ctrl.m_doodadPlantCoreObjDict[entity]
        cell.doodadPlantCore:SetQuantityInfo(current, refreshNum, max)
        cell.doodadPlantCore:SetRefreshInfo(nextRefresh)
        cell.doodadPlantCore:RefreshTextNow()
    end
end

HL.Commit(DoodadPlantCoreScanUICtrl)
