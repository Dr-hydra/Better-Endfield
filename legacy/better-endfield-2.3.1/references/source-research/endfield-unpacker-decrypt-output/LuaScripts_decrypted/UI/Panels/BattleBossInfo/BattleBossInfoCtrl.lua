local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleBossInfo

BattleBossInfoCtrl = HL.Class('BattleBossInfoCtrl', uiCtrl.UICtrl)






BattleBossInfoCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_ADD_HEAD_BAR] = '_OnAddHeadBar',
    [MessageConst.ON_REMOVE_HEAD_BAR] = '_OnRemoveHeadBar',
}

do

    BattleBossInfoCtrl.m_updateKey = HL.Field(HL.Number) << -1

    BattleBossInfoCtrl.m_targetAbilitySystem = HL.Field(HL.Userdata) << nil
end


BattleBossInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.canvasGroup.alpha = 0
    local inFightEnemies = GameWorld.battle.enemies
    for _, enemyPtr in cs_pairs(inFightEnemies) do
        local enemy = enemyPtr.abilityCom
        if enemy.showBigHeadBar and enemy.alive then
            self:_ShowTargetInfo(enemy)
            break
        end
    end

    UIManager:SetTopOrder(PanelId.MainHud)
end

BattleBossInfoCtrl.OnShow = HL.Override() << function(self)
    if self.m_targetAbilitySystem ~= nil then
        self.view.canvasGroup.alpha = 1
        self.view.anim:PlayInAnimation()
    end
    self.view.headBar:Refresh()
end

BattleBossInfoCtrl.OnHide = HL.Override() << function(self)
end

BattleBossInfoCtrl.OnClose = HL.Override() << function(self)
    self.view.headBar:Clear()
    self:_ClearUpdate()
end

do
    BattleBossInfoCtrl._OnAddHeadBar = HL.Method(HL.Table) << function(self, args)
        local targetAbilitySystem = unpack(args)
        if targetAbilitySystem and targetAbilitySystem.showBigHeadBar then
            if targetAbilitySystem.alive and targetAbilitySystem ~= self.m_targetAbilitySystem then
                self:_ShowTargetInfo(targetAbilitySystem)
            elseif not targetAbilitySystem.alive and targetAbilitySystem == self.m_targetAbilitySystem then
                self:_HideTargetInfo()
            end
        end
    end

    BattleBossInfoCtrl._OnRemoveHeadBar = HL.Method(HL.Table) << function(self, args)
        local targetAbilitySystem = unpack(args)
        if targetAbilitySystem and targetAbilitySystem.showBigHeadBar then
            if targetAbilitySystem == self.m_targetAbilitySystem then
                self:_HideTargetInfo()
            end
        end
    end

    BattleBossInfoCtrl._HideTargetInfo = HL.Method() << function(self)
        self.view.anim:ClearTween()
        if self.m_targetAbilitySystem.alive then
            self.view.anim:PlayOutAnimation(function()
                self.view.canvasGroup.alpha = 0
                self.view.headBar:Clear()
            end)
        else
            self.view.anim:PlayWithTween("battlebossinfo_broken", function()
                self.view.canvasGroup.alpha = 0
                self.view.headBar:Clear()
            end)
        end

        self.m_targetAbilitySystem = nil
        self:_ClearUpdate()
    end

    BattleBossInfoCtrl._ShowTargetInfo = HL.Method(HL.Userdata) << function(self, targetAbilitySystem)
        self.view.anim:ClearTween()
        self.m_targetAbilitySystem = targetAbilitySystem

        self.view.canvasGroup.alpha = 1
        self.view.anim:PlayInAnimation()
        local enemyId = targetAbilitySystem.entity.templateData.id
        self.view.name.text = DataManager.enemyDataTable:GetEnemyName(enemyId)
        self.view.headBar:SetTarget(targetAbilitySystem)

        if self.m_updateKey < 0 then
            self.m_updateKey = LuaUpdate:Add("Tick", function(deltaTime)
                self:_UpdateTargetInfo(deltaTime)
            end)
        end
    end

    BattleBossInfoCtrl._UpdateTargetInfo = HL.Method(HL.Number) << function(self, deltaTime)
        self.view.headBar:UpdateData(deltaTime)
    end

    BattleBossInfoCtrl._ClearUpdate = HL.Method() << function(self)
        if self.m_updateKey > 0 then
            LuaUpdate:Remove(self.m_updateKey)
            self.m_updateKey = -1
        end
    end

    BattleBossInfoCtrl.GetFollowPointPosition = HL.Method().Return(HL.Boolean, Vector3) << function(self)
        return self.view.followPoint.gameObject.activeInHierarchy, self.view.followPoint.position
    end
end

HL.Commit(BattleBossInfoCtrl)
