
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonBlockMask

CommonBlockMaskCtrl = HL.Class('CommonBlockMaskCtrl', uiCtrl.UICtrl)






CommonBlockMaskCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.REMOVE_COMMON_BLOCK_MASK] = 'RemoveCommonBlockMask',
}


CommonBlockMaskCtrl.m_maskKeys = HL.Field(HL.Table)



CommonBlockMaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_maskKeys = {}
end

CommonBlockMaskCtrl.AddCommonBlockMask = HL.StaticMethod(HL.Any) << function(key)
    if type(key) == "table" then
        key = unpack(key)
    end

    local self = UIManager:AutoOpen(PANEL_ID)
    self:_AddCommonBlockMask(key)
end

CommonBlockMaskCtrl._AddCommonBlockMask = HL.Method(HL.String) << function(self, key)
    self.m_maskKeys[key] = true
    self:_UpdateMask()
end

CommonBlockMaskCtrl._UpdateMask = HL.Method() << function(self)
    if next(self.m_maskKeys) then
        return
    end
    self:Close()
end

CommonBlockMaskCtrl.RemoveCommonBlockMask = HL.Method(HL.Any) << function(self, key)
    if type(key) == "table" then
        key = unpack(key)
    end

    self.m_maskKeys[key] = nil
    self:_UpdateMask()
end

HL.Commit(CommonBlockMaskCtrl)
