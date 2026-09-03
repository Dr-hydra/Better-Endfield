local phaseBase = require_ex('Phase/Core/PhaseBase')

PhaseShopTrade = HL.Class('PhaseShopTrade', phaseBase.PhaseBase)

PhaseShopTrade.s_messages = HL.StaticField(HL.Table) << {

}

PhaseShopTrade.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local panelItem = self.m_panel2Item[PanelId.ShopTrade]
    if panelItem then
        local overrideArg = panelItem.uiCtrl:GetCurPhaseStateArg()
        if overrideArg ~= nil then
            return lume.deepCopy(overrideArg)
        end
    end
end

HL.Commit(PhaseShopTrade)
