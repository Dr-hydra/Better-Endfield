
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharacterFootBarUpgrade
CharacterFootBarUpgradeCtrl = HL.Class('CharacterFootBarUpgradeCtrl', uiCtrl.UICtrl)






CharacterFootBarUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}

CharacterFootBarUpgradeCtrl.m_oldCellNum = HL.Field(HL.Number) << 0

CharacterFootBarUpgradeCtrl.m_newCellNum = HL.Field(HL.Number) << 0

CharacterFootBarUpgradeCtrl.m_cells = HL.Field(HL.Table)

CharacterFootBarUpgradeCtrl.m_coroutine = HL.Field(HL.Thread)


CharacterFootBarUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local old, current = unpack(arg)
    self.m_oldCellNum = old
    self.m_newCellNum = current

    self.view.main:SetState(lume.round(old) .. "GridState")
    self.view.footBarNode:SampleClipAtPercent("characterfootbararcupgrad_in", 0)
    self.view.greenFootBar.fillAmount = 1
    self.view.whiteFootBar.fillAmount = 1

    self.m_cells = { self.view.splitLineCell.gameObject }
    local parent = self.view.splitLineCell.parent
    for i = 2, current do
        local cell = GameObject.Instantiate(self.view.splitLineCell.gameObject, parent)
        table.insert(self.m_cells, cell)
        cell:SetActive(i <= old)
    end
    self.view.splitLineCell.localScale = Vector3.zero
end

CharacterFootBarUpgradeCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_FORBID_CHAR_FOOT_BAR, {"footBarUpgradeCtrl", true} )
end

CharacterFootBarUpgradeCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self.m_coroutine = coroutine.create(function()
        self.view.footBarNode:PlayWithTween("characterfootbararcupgrad_in", function()
            self:_ResumeCoroutine()
        end)
        coroutine.yield()
        self.view.footBarNode:PlayWithTween("characterfootbararcupgrad_out", function()
            self:_ResumeCoroutine()
        end)
        coroutine.yield()
        self.view.main:SetState(lume.round(self.m_newCellNum) .. "GridState")
        for i, cell in ipairs(self.m_cells) do
            cell:SetActive(true)
        end
        self.view.footBarNode:PlayWithTween("characterfootbararcupgrad_upgrad", function()
            self:_ResumeCoroutine()
        end)
        self.view.greenFootBar.fillAmount = self.m_oldCellNum / self.m_newCellNum
        coroutine.yield()
        self.view.greenFootBar:DOFillAmount(1, self.view.config.FILL_AMOUNT_INCREASE_DURATION):OnComplete(function()
            self:_ResumeCoroutine()
        end)
        coroutine.yield()
        self.view.footBarNode:PlayWithTween("characterfootbararcupgrad_upgrad_glow", function()
            self:_ResumeCoroutine()
        end)
        coroutine.yield()
        self:PlayAnimationOutAndClose()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "DashBarUpgrade")
    end)
    coroutine.resume(self.m_coroutine)
end

CharacterFootBarUpgradeCtrl._ResumeCoroutine = HL.Method() << function(self)
    if self.m_coroutine ~= nil then
        coroutine.resume(self.m_coroutine)
    end
end

CharacterFootBarUpgradeCtrl.OnDashCountMaxChanged = HL.StaticMethod() << function()
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if isOpen then

        return
    end
    local old = GameInstance.playerController.lastShownMaxDashCount
    local current = GameInstance.playerController.maxDashCount
    if old >= current then
        return
    end
    LuaSystemManager.mainHudActionQueue:AddRequest("DashBarUpgrade", function()
        local old = GameInstance.playerController.lastShownMaxDashCount
        local current = GameInstance.playerController.maxDashCount
        GameInstance.playerController.lastShownMaxDashCount = current
        if old >= current then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "DashBarUpgrade")
            return
        end

        local maxDashCount = Tables.globalConst.maxDashEnergyLimit / Tables.globalConst.dashCostEnergyValue
        if old >= maxDashCount then
            Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "DashBarUpgrade")
            return
        end

        UIManager:Open(PANEL_ID, {old, current})
    end)
end

CharacterFootBarUpgradeCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self:Close()
end

CharacterFootBarUpgradeCtrl.OnClose = HL.Override() << function(self)
    self.m_coroutine = nil
    Notify(MessageConst.TOGGLE_FORBID_CHAR_FOOT_BAR, {"footBarUpgradeCtrl", false} )
end




HL.Commit(CharacterFootBarUpgradeCtrl)
