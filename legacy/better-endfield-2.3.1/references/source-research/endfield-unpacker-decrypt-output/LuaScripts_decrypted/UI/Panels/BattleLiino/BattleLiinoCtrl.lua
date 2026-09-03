local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleLiino

BattleLiinoCtrl = HL.Class('BattleLiinoCtrl', uiCtrl.UICtrl)






BattleLiinoCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_SQUAD_CHANGED] = 'OnBattleTeamChanged',
}

BattleLiinoCtrl.m_likeAnims = HL.Field(HL.Table)

BattleLiinoCtrl.m_curLikeAnimIndex = HL.Field(HL.Number) << 1

BattleLiinoCtrl.m_bottomEffTimerId = HL.Field(HL.Number) << -1


BattleLiinoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_likeAnims = {}
    self.m_curLikeAnimIndex = 1

    for i = 1, 3 do
        local anim = self.view['likeAnim' .. i]
        table.insert(self.m_likeAnims, anim)
        anim.gameObject:SetActive(false)
    end

    self.view.liinoBottomEff.gameObject:SetActive(false)
    self.view.randomLikeEff:OnHide()
end

BattleLiinoCtrl.OnShow = HL.Override() << function(self)
    self:OnBattleTeamChanged()
end

BattleLiinoCtrl.OnHide = HL.Override() << function(self)
    self.view.randomLikeEff:OnHide()
    if self.m_bottomEffTimerId >= 0 then
        self.m_bottomEffTimerId = self:_ClearTimer(self.m_bottomEffTimerId)
    end

    self.view.liinoBottomEff:ClearTween(false)
    self.view.liinoBottomEff.gameObject:SetActive(false)
end

BattleLiinoCtrl.OnBattleTeamChanged = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    if not self.isPCPanel then
        return
    end
    local squadSlots = GameInstance.player.squadManager.curSquad.slots
    for i = 1, squadSlots.Count do
        local charId = squadSlots[CSIndex(i)].charId
        if charId == "chr_0035_liino" then
            local isOpen, battleActionCtrl = UIManager:IsOpen(PanelId.BattleAction)
            if isOpen then
                local skillButton = battleActionCtrl.m_skillCellList[i]
                local likeBtnPosition = self.view.liinoLikeBtn.position
                self.view.liinoLikeBtn.position = Vector3(skillButton.transform.position.x, likeBtnPosition.y, likeBtnPosition.z)
            end
        end
    end
end

BattleLiinoCtrl.PlayBottomEffect = HL.Method(HL.Number) << function(self, speed)
    if self.m_bottomEffTimerId >= 0 then
        self.m_bottomEffTimerId = self:_ClearTimer(self.m_bottomEffTimerId)
    end

    self.view.liinoBottomEff:ClearTween(false)
    self.view.liinoBottomEff.gameObject:SetActive(true)
    self.view.bottomAnim:PlayLoopAnimation(speed, true)

    self.m_bottomEffTimerId = self:_StartTimer(self.view.config.BOTTOM_EFFECT_DURATION, function()
        self.view.liinoBottomEff:PlayOutAnimation(function()
            self.view.liinoBottomEff.gameObject:SetActive(false)
        end)
        self.m_bottomEffTimerId = -1
    end)
end

BattleLiinoCtrl.OnBattleLiinoNormalSkillEvent = HL.StaticMethod(HL.Table) << function(args)
    local eventIndex, speed, scale = unpack(args)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if eventIndex == 0 then
        if not isOpen then
            ctrl = UIManager:Open(PANEL_ID)
        end
        ctrl:Show()
    elseif eventIndex == 1 then
        if not isOpen then
            return
        end

        if not ctrl:IsPlayingAnimationOut() then

            ctrl:PlayAnimationOutAndHide()
        end
    elseif eventIndex == 2 then
        ctrl:PlayBottomEffect(speed)
    elseif eventIndex == 3 then

    elseif eventIndex == 4 then
        local anim = ctrl.m_likeAnims[ctrl.m_curLikeAnimIndex]
        anim.gameObject:SetActive(true)
        anim:PlayInAnimation(function()
            anim.gameObject:SetActive(false)
        end, true, speed)
        ctrl.m_curLikeAnimIndex = ctrl.m_curLikeAnimIndex + 1
        if ctrl.m_curLikeAnimIndex > #ctrl.m_likeAnims then
            ctrl.m_curLikeAnimIndex = 1
        end
    elseif eventIndex == 5 then
        ctrl.view.likeAnim4.gameObject:SetActive(true)
        ctrl.view.likeAnim4.transform.localScale = Vector3(scale, scale, 1)
        ctrl.view.likeAnim4:PlayInAnimation(function()
            ctrl.view.likeAnim4.gameObject:SetActive(false)
        end, true, speed)
    elseif eventIndex == 6 then
        ctrl.view.randomLikeEff:PlayEffect()
    end
end

HL.Commit(BattleLiinoCtrl)
