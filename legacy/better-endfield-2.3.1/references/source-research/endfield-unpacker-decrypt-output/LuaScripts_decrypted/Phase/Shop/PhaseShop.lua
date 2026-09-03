local phaseBase = require_ex('Phase/Core/PhaseBase')

PhaseShop = HL.Class('PhaseShop', phaseBase.PhaseBase)

PhaseShop.s_messages = HL.StaticField(HL.Table) << {

}

PhaseShop.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local panelItem = self.m_panel2Item[PanelId.Shop]
    if panelItem then
        local overrideArg = panelItem.uiCtrl:GetCurPhaseStateArg()
        if overrideArg ~= nil then
            return lume.deepCopy(overrideArg)
        end
    end
end

HL.Commit(PhaseShop)
