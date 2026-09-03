
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.ShopMonthlyPassPopUp
PhaseShopMonthlyPassPopUp = HL.Class('PhaseShopMonthlyPassPopUp', phaseBase.PhaseBase)

PhaseShopMonthlyPassPopUp.m_showTimeStamps = HL.Field(HL.Table)

PhaseShopMonthlyPassPopUp.m_currShowTimeStampIndex = HL.Field(HL.Number) << 1

PhaseShopMonthlyPassPopUp.m_endCallback = HL.Field(HL.Any)

PhaseShopMonthlyPassPopUp.m_haveGotReward = HL.Field(HL.Boolean) << false





PhaseShopMonthlyPassPopUp.s_messages = HL.StaticField(HL.Table) << {

}



PhaseShopMonthlyPassPopUp._OnInit = HL.Override() << function(self)
    PhaseShopMonthlyPassPopUp.Super._OnInit(self)
end

PhaseShopMonthlyPassPopUp._InitAllPhaseItems = HL.Override() << function(self)
    if self.arg.ShowTimeStamps ~= nil then
        self.m_showTimeStamps = self.arg.ShowTimeStamps
    end

    if self.arg.EndCallback ~= nil then
        self.m_endCallback = self.arg.EndCallback
    end

    self:_CreatePanel()
end

PhaseShopMonthlyPassPopUp._CreatePanel = HL.Method() << function(self)
    local timestamp = self.m_showTimeStamps[self.m_currShowTimeStampIndex]
    local day = GameInstance.player.monthlyPassSystem:GetRemainValidDaysByTimeStamp(timestamp)

    self:CreatePhasePanelItem(PanelId.ShopMonthlyPassPopUp)
    self:CreatePhasePanelItem(PanelId.ShopMonthlyPass3D,
        {
            isDailyPopup = true,
            remainDayNumber = day,
        })
end

PhaseShopMonthlyPassPopUp.OnClickBg = HL.Method() << function(self)
    if self.m_haveGotReward == false then

        local currTs = self.m_showTimeStamps[self.m_currShowTimeStampIndex]
        if GameInstance.player.monthlyPassSystem:CheckIsValidShowTimeStamp(currTs) then
            GameInstance.player.monthlyPassSystem:SendConfirm(currTs)
        end
        self.m_haveGotReward = true
        self:_GetPanelPhaseItem(PanelId.ShopMonthlyPassPopUp).uiCtrl:RefreshUI()
        self:_GetPanelPhaseItem(PanelId.ShopMonthlyPass3D).uiCtrl:PlayGotDailyReward()

        local time = self:_GetPanelPhaseItem(PanelId.ShopMonthlyPassPopUp).uiCtrl.view.config.DELAY_CLOSE_TIME
        self:_StartCoroutine(function()
            logger.info(time .. "s后关闭PhaseId.ShopMonthlyPassPopUp")
            coroutine.wait(time)
            if #self.m_showTimeStamps > self.m_currShowTimeStampIndex then
                self:_PlayNext()
            else
                local endCallback = self.m_endCallback
                PhaseManager:ExitPhaseFast(PhaseId.ShopMonthlyPassPopUp)
                endCallback()
            end
        end)
    else
        return
    end
end

PhaseShopMonthlyPassPopUp._PlayNext = HL.Method() << function(self)

    self.m_currShowTimeStampIndex = self.m_currShowTimeStampIndex + 1

    self.m_haveGotReward = false

    self:RemovePhasePanelItem(self.m_panel2Item[PanelId.ShopMonthlyPassPopUp])
    self:RemovePhasePanelItem(self.m_panel2Item[PanelId.ShopMonthlyPass3D])

    self:_CreatePanel()
end




PhaseShopMonthlyPassPopUp.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseShopMonthlyPassPopUp._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseShopMonthlyPassPopUp._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseShopMonthlyPassPopUp._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseShopMonthlyPassPopUp._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseShopMonthlyPassPopUp._OnActivated = HL.Override() << function(self)
end

PhaseShopMonthlyPassPopUp._OnDeActivated = HL.Override() << function(self)
end

PhaseShopMonthlyPassPopUp._OnDestroy = HL.Override() << function(self)
    PhaseShopMonthlyPassPopUp.Super._OnDestroy(self)
end





HL.Commit(PhaseShopMonthlyPassPopUp)
