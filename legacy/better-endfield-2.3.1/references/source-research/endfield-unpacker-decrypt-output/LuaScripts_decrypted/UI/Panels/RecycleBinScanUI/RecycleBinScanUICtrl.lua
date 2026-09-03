
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RecycleBinScanUI
RecycleBinScanUICtrl = HL.Class('RecycleBinScanUICtrl', uiCtrl.UICtrl)







RecycleBinScanUICtrl.s_messages = HL.StaticField(HL.Table) << {

}

RecycleBinScanUICtrl.m_recycleBinObjDict = HL.Field(HL.Table)

RecycleBinScanUICtrl.m_recycleBinObjPool = HL.Field(HL.Table)

RecycleBinScanUICtrl.m_recycleBinLogicIdDict = HL.Field(HL.Table)


RecycleBinScanUICtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_recycleBinObjDict = {}
    self.m_recycleBinObjPool = {}
    self.m_recycleBinLogicIdDict = {}
end





RecycleBinScanUICtrl.OnClose = HL.Override() << function(self)
    if self.m_recycleBinObjDict ~= nil then
        for _, v in pairs(self.m_recycleBinObjDict) do
            v.recycleBin:Clear()
            GameObject.Destroy(v.recycleBin.gameObject)
        end
        self.m_recycleBinObjDict = nil
    end

    if self.m_recycleBinObjPool ~= nil then
        for _, v in ipairs(self.m_recycleBinObjPool) do
            GameObject.Destroy(v.recycleBin.gameObject)
        end
        self.m_recycleBinObjPool = nil
    end
end

RecycleBinScanUICtrl._OnAddRecycleBinUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = RecycleBinScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, coreName, formName, nextRefresh, hpRatio = unpack(args)
    ctrl:_AddRecycleBin(entity, coreName, formName, nextRefresh, hpRatio)
end

RecycleBinScanUICtrl._AddRecycleBin = HL.Method(HL.Any, HL.String, HL.String, HL.Number, HL.Number) << function(self, targetObject, coreName, formName, nextRefresh, hpRatio)
    if self.m_recycleBinObjDict[targetObject] == nil then
        self.m_recycleBinObjDict[targetObject] = self:_CreateRecycleBin()
    end

    self.m_recycleBinObjDict[targetObject].rectTransform.gameObject:SetActive(true)
    local recycleBin = self.m_recycleBinObjDict[targetObject].recycleBin

    recycleBin:SetTarget(targetObject)
    recycleBin:SyncRefreshInfo(nextRefresh, hpRatio)
    self:_InitRecycleBin(self.m_recycleBinObjDict[targetObject], targetObject, coreName, formName)
    self.m_recycleBinLogicIdDict[recycleBin.entityLogicId] = recycleBin
end

RecycleBinScanUICtrl._InitRecycleBin = HL.Method(HL.Any, HL.Any, HL.String, HL.String) << function(self, recycleBin, target, coreName, formName)
    if not target or not recycleBin then
        return
    end

    recycleBin.doodadCoreName:SetText(coreName)
    recycleBin.typeName:SetText(formName)
end

RecycleBinScanUICtrl._CreateRecycleBin = HL.Method().Return(HL.Table) << function(self)
    if self.m_recycleBinObjPool ~= nil and #self.m_recycleBinObjPool > 0 then
        local result = self.m_recycleBinObjPool[#self.m_recycleBinObjPool]
        table.remove(self.m_recycleBinObjPool, #self.m_recycleBinObjPool)
        return result
    else
        local obj = self:_CreateWorldGameObject(self.view.config.CHAR_RECYCLE_BIN)
        local result = Utils.wrapLuaNode(obj)
        return result
    end
end

RecycleBinScanUICtrl._OnRemoveRecycleBinUI = HL.StaticMethod(HL.Any) << function(args)
    local opened, ctrl = UIManager:IsOpen(PANEL_ID)
    if not opened then
        return
    end
    local ctrl = RecycleBinScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    if ctrl.m_recycleBinObjDict[entity] ~= nil then
        local cell = ctrl.m_recycleBinObjDict[entity]
        cell.recycleBin:Clear()
        cell.rectTransform.gameObject:SetActive(false)
        table.insert(ctrl.m_recycleBinObjPool, cell)
        ctrl.m_recycleBinObjDict[entity] = nil
    end
end

RecycleBinScanUICtrl._OnUpdateRecycleBinUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = RecycleBinScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity, formName, nextRefresh, hpRatio = unpack(args)
    if ctrl.m_recycleBinObjDict[entity] ~= nil then
        local cell = ctrl.m_recycleBinObjDict[entity]
        cell.typeName:SetText(formName)
        cell.recycleBin:UpdateUI(nextRefresh, hpRatio)
    end
end

HL.Commit(RecycleBinScanUICtrl)
