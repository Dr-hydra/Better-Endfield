local uiCtrl = require_ex('UI/Panels/Base/UICtrl')

AutoCalcOrderUICtrl = HL.Class('AutoCalcOrderUICtrl', uiCtrl.UICtrl)


AutoCalcOrderUICtrl.m_curArgs = HL.Field(HL.Table)

AutoCalcOrderUICtrl.m_attachedPanels = HL.Field(HL.Table)

AutoCalcOrderUICtrl.m_tailUpdateTickKey = HL.Field(HL.Number) << -1

AutoCalcOrderUICtrl.m_calcOrderDirty = HL.Field(HL.Boolean) << false

AutoCalcOrderUICtrl.CheckCalcOrderDirty = HL.Method() << function(self)
    if self.m_calcOrderDirty and not self:IsPlayingAnimationOut() then
        self:CalcPanelOrder()
    end
end

AutoCalcOrderUICtrl.OnShow = HL.Override() << function(self)
    self.m_tailUpdateTickKey = LuaUpdate:Add("TailTick", function()
        self:CheckCalcOrderDirty()
    end)
end

AutoCalcOrderUICtrl.OnClose = HL.Override() << function(self)
    self.m_tailUpdateTickKey = LuaUpdate:Remove(self.m_tailUpdateTickKey)
end

AutoCalcOrderUICtrl.OnHide = HL.Override() << function(self)
    self.m_tailUpdateTickKey = LuaUpdate:Remove(self.m_tailUpdateTickKey)
end


AutoCalcOrderUICtrl._CustomHide = HL.Virtual(HL.Number) << function(self, panelId)
    if not self.m_attachedPanels[panelId] then
        return
    end
    self.m_attachedPanels[panelId] = nil
    self:CalcPanelOrder()
end

AutoCalcOrderUICtrl._AttachToPanel = HL.Virtual(HL.Table) << function(self, args)
    self.m_attachedPanels[args.panelId] = args
    self:CalcPanelOrder()
end

AutoCalcOrderUICtrl.PanelOrderChanged = HL.Method(HL.Opt(HL.Any)) << function(self)
    self.m_calcOrderDirty = true
end

AutoCalcOrderUICtrl.CalcPanelOrder = HL.Virtual(HL.Opt(HL.Boolean)) << function(self, fromOutAni)
    self.m_calcOrderDirty = false

    local maxOrder, curArgs
    for panelId, args in pairs(self.m_attachedPanels) do
        local isOpen, panel = UIManager:IsOpen(panelId)
        if isOpen and panel:IsShow() and not panel:IsPlayingAnimationOut() then
            local order = panel:GetSortingOrder() + args.offset
            if not maxOrder or order > maxOrder then
                maxOrder = order
                curArgs = args
            end
        end
    end

    if maxOrder then
        self:CustomSetPanelOrder(maxOrder, curArgs)
        if fromOutAni then
            self:PlayAnimationIn()
        end
    else
        self.m_curArgs = nil
        self:Hide()
    end
end

AutoCalcOrderUICtrl.CustomSetPanelOrder = HL.Virtual(HL.Opt(HL.Number, HL.Table)) << function(self, maxOrder, args)
end

AutoCalcOrderUICtrl.PlayOutAnim = HL.Method(HL.Number) << function(self, panelId)
    if self:IsHide() or self:IsPlayingAnimationOut() then
        return
    end
    if not self.m_curArgs or self.m_curArgs.panelId ~= panelId then
        return
    end
    self:PlayAnimationOutWithCallback(function()
        self:CalcPanelOrder(true)
    end)
end

HL.Commit(AutoCalcOrderUICtrl)
