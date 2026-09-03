
local commonMaskCtrl = require_ex('UI/Panels/CommonMask/CommonMaskCtrl')
local PANEL_ID = PanelId.CommonMaskLower
CommonMaskLowerCtrl = HL.Class('CommonMaskLowerCtrl', commonMaskCtrl.CommonMaskCtrl)







CommonMaskLowerCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_COMMON_MASK_LOW_SHUTDOWN] = 'OnCommonMaskShutDown',
}

CommonMaskLowerCtrl.OnCommonMaskLowerStart = HL.StaticMethod(HL.Table) << function(arg)
    local ctrl = CommonMaskLowerCtrl.AutoOpen(PANEL_ID, {}, true)
    local commonMaskData = unpack(arg) or arg
    ctrl:TryStartCommonMask(commonMaskData)
end

CommonMaskLowerCtrl.OnCommonMaskLowerEnd = HL.StaticMethod(HL.Table) << function(arg)
    local ctrl = CommonMaskLowerCtrl.AutoOpen(PANEL_ID, {}, true)
    local commonMaskData = unpack(arg) or arg
    ctrl:TryStartCommonMask(commonMaskData)
end

CommonMaskLowerCtrl._UpdatePlayerState = HL.Override(HL.Boolean) << function(self, inBlackScreen)
end












HL.Commit(CommonMaskLowerCtrl)
