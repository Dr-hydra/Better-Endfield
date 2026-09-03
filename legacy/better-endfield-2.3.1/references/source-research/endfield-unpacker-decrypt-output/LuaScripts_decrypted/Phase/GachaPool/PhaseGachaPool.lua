
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.GachaPool
PhaseGachaPool = HL.Class('PhaseGachaPool', phaseBase.PhaseBase)





PhaseGachaPool.s_messages = HL.StaticField(HL.Table) << {

}



PhaseGachaPool._OnInit = HL.Override() << function(self)
    PhaseGachaPool.Super._OnInit(self)
end




PhaseGachaPool.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    AudioAdapter.LoadAndPinEventsAsync({ UIConst.GACHA_MUSIC_UI, UIConst.GACHA_MUSIC_DROP_BIN })
    if not self.arg or type(self.arg) ~= "table" then
        self.arg = {}
    end
    self.arg.phase = self

    if transitionType == PhaseConst.EPhaseState.TransitionIn and not fastMode then

        local pools = {}

        local csGacha = GameInstance.player.gacha
        for id, csInfo in pairs(csGacha.poolInfos) do
            if csInfo.isChar and csInfo.isOpenValid then
                local info = {
                    id = id,

                    data = csInfo.data,
                    sortId = csInfo.data.sortId,
                }
                table.insert(pools, info)
            end
        end
        table.sort(pools, Utils.genSortFunction({ "sortId" }, true))

        local targetIndex = 1
        local targetPoolId = self.arg.poolId
        local count = #pools
        if not string.isEmpty(targetPoolId) then
            for i = 1, count do
                if targetPoolId == pools[i].id then
                    targetIndex = i
                    break
                end
            end
        end

        if targetIndex <= count then
            local uiPrefabName = pools[targetIndex].data.uiPrefab
            local path = string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Gacha/Widgets/%s.prefab", uiPrefabName)
            self.m_resourceLoader:LoadGameObjectAsync(path, function()
                logger.info(path, "预载完成")
            end)
        end
    end
end

PhaseGachaPool._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaPool._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    AudioAdapter.UnpinEvent(UIConst.GACHA_MUSIC_UI)
    AudioAdapter.UnpinEvent(UIConst.GACHA_MUSIC_DROP_BIN)
end

PhaseGachaPool._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseGachaPool._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseGachaPool._OnActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaPool._OnDeActivated = HL.Override() << function(self)
    LuaSystemManager.gachaSystem:UpdateGachaSettingState()
end

PhaseGachaPool._OnDestroy = HL.Override() << function(self)
    PhaseGachaPool.Super._OnDestroy(self)
end



PhaseGachaPool.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}

    local gachaPoolPanelItem = self.m_panel2Item[PanelId.GachaPool]
    if gachaPoolPanelItem then
        arg.poolId = gachaPoolPanelItem.uiCtrl.m_curPoolId
        arg.subPanelArg = gachaPoolPanelItem.uiCtrl:GetSubPanelArg()
        arg.rewardQueueRestoreInfo = gachaPoolPanelItem.uiCtrl:GetRewardQueueRestoreInfo()
    end
    arg.phase = nil
    return arg
end

HL.Commit(PhaseGachaPool)
