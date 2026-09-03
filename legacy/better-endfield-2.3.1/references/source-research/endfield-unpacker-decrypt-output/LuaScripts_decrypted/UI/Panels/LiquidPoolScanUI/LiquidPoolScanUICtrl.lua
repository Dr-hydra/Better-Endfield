
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.LiquidPoolScanUI
LiquidPoolScanUICtrl = HL.Class('LiquidPoolScanUICtrl', uiCtrl.UICtrl)







LiquidPoolScanUICtrl.s_messages = HL.StaticField(HL.Table) << {

}

LiquidPoolScanUICtrl.m_liquidPoolObjDict = HL.Field(HL.Table)

LiquidPoolScanUICtrl.m_liquidPoolObjPool = HL.Field(HL.Table)



LiquidPoolScanUICtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_liquidPoolObjDict = {}
    self.m_liquidPoolObjPool = {}
end





LiquidPoolScanUICtrl.OnClose = HL.Override() << function(self)
    if self.m_liquidPoolObjDict ~= nil then
        for _, v in pairs(self.m_liquidPoolObjDict) do
            v.liquidPool:Clear()
            GameObject.Destroy(v.liquidPool.gameObject)
        end
        self.m_liquidPoolObjDict = nil
    end

    if self.m_liquidPoolObjPool ~= nil then
        for _, v in ipairs(self.m_liquidPoolObjPool) do
            GameObject.Destroy(v.liquidPool.gameObject)
        end
        self.m_liquidPoolObjPool = nil
    end
end

LiquidPoolScanUICtrl._OnAddLiquidPoolUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = LiquidPoolScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local waterVolumeId, x, y, z = unpack(args)
    ctrl:_AddLiquidPool(waterVolumeId, x, y, z)
end

LiquidPoolScanUICtrl._AddLiquidPool = HL.Method(HL.Number, HL.Number, HL.Number, HL.Number) << function(self, waterVolumeId, x, y, z)
    if self.m_liquidPoolObjDict[waterVolumeId] == nil then
        self.m_liquidPoolObjDict[waterVolumeId] = self:_CreateLiquidPool()
    end

    self.m_liquidPoolObjDict[waterVolumeId].rectTransform.gameObject:SetActive(true)
    local liquidPool = self.m_liquidPoolObjDict[waterVolumeId].liquidPool

    liquidPool:SetWaterInfo(waterVolumeId, x, y, z)
end

LiquidPoolScanUICtrl._CreateLiquidPool = HL.Method().Return(HL.Table) << function(self)
    if self.m_liquidPoolObjPool ~= nil and #self.m_liquidPoolObjPool > 0 then
        local result = self.m_liquidPoolObjPool[#self.m_liquidPoolObjPool]
        table.remove(self.m_liquidPoolObjPool, #self.m_liquidPoolObjPool)
        return result
    else
        local obj = self:_CreateWorldGameObject(self.view.config.LIQUID_POOL_ITEM)
        local result = Utils.wrapLuaNode(obj)
        return result
    end
end

LiquidPoolScanUICtrl._OnRemoveLiquidPoolUI = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = LiquidPoolScanUICtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    if ctrl.m_liquidPoolObjDict[entity] ~= nil then
        local cell = ctrl.m_liquidPoolObjDict[entity]
        cell.liquidPool:Clear()
        cell.rectTransform.gameObject:SetActive(false)
        table.insert(ctrl.m_liquidPoolObjPool, cell)
        ctrl.m_liquidPoolObjDict[entity] = nil
    end
end

LiquidPoolScanUICtrl._OnUpdateLiquidPoolUI = HL.StaticMethod(HL.Any) << function(args)






end

HL.Commit(LiquidPoolScanUICtrl)
