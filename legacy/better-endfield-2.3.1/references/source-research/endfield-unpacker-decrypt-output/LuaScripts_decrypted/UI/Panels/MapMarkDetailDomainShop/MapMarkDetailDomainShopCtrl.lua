
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailDomainShop


local shopSystem = GameInstance.player.shopSystem

MapMarkDetailDomainShopCtrl = HL.Class('MapMarkDetailDomainShopCtrl', uiCtrl.UICtrl)






MapMarkDetailDomainShopCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailDomainShopCtrl.m_markInstId = HL.Field(HL.String) << ""

MapMarkDetailDomainShopCtrl.m_commonArgs = HL.Field(HL.Table)

MapMarkDetailDomainShopCtrl.m_info = HL.Field(HL.Table)




MapMarkDetailDomainShopCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()
end



MapMarkDetailDomainShopCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_markInstId = arg.markInstId
end

MapMarkDetailDomainShopCtrl._UpdateData = HL.Method() << function(self)

    local markInstId = self.m_markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not getRuntimeDataSuccess then
        logger.error("地图详情页获取实例数据失败" .. self.m_instId)
        return
    end
    local detail = markRuntimeData.detail
    local shopChannelId = detail.systemInstId

    local shopChannelCfg = Tables.shopChannelDevelopmentTable[shopChannelId]
    local groupId = shopChannelCfg.shopGroupId

    local groupData = shopSystem:GetShopGroupData(groupId)
    local curLv
    if groupData == nil or groupData.domainChannelData == nil then
        curLv = 0
    else
        local hasLv
        hasLv, curLv = groupData.domainChannelData.channelLevelMap:TryGetValue(shopChannelId)
        curLv = hasLv and curLv or 0
    end
    local isUnlock = curLv > 0
    local maxLv = DomainPOIUtils.GetDomainShopChannelMaxLevel(shopChannelId)
    local _, maxLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(maxLv)
    local isFinalMax = false
    if maxLvChannelCfg then
        isFinalMax = maxLvChannelCfg.isFinalMaxLevel
    end

    local desc = ""
    local descList = {}
    local _, curLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(curLv)
    if curLvChannelCfg and not string.isEmpty(curLvChannelCfg.channelDesc) then
        table.insert(descList, curLvChannelCfg.channelDesc)
    end
    local _, nextLvChannelCfg = shopChannelCfg.channelLevelMap:TryGetValue(curLv + 1)
    if nextLvChannelCfg and not string.isEmpty(nextLvChannelCfg.upgradeDesc) and curLv < maxLv then
        table.insert(descList, nextLvChannelCfg.upgradeDesc)
    end
    for _, curDesc in pairs(descList) do
        desc = desc .. curDesc
    end

    local canSkipPreconditions = DomainPOIUtils._CanSkipDomainShopChannelPreconditions(shopChannelCfg)
    local upgradeQuestId = canSkipPreconditions and "" or Tables.shopDomainConst.domainShopUnlockQuestId
    local questIsComplete = true
    local questState
    if string.isEmpty(upgradeQuestId) then
        questState = CS.Beyond.Gameplay.MissionSystem.QuestState.Completed
    else
        questState = GameInstance.player.mission:GetQuestState(upgradeQuestId)
    end
    questIsComplete = questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Completed

    self.m_info = {
        curLv = curLv,
        isUnlock = isUnlock,
        isMaxLv = isFinalMax and curLv >= maxLv,
    }

    self.m_commonArgs = {}
    local commonArgs = self.m_commonArgs
    commonArgs.markInstId = self.m_markInstId
    commonArgs.descText = desc
    commonArgs.bigBtnActive = questIsComplete

    if not questIsComplete then
        if questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Processing
            or questState == CS.Beyond.Gameplay.MissionSystem.QuestState.Paused
        then
            local upgradeMissionId = GameInstance.player.mission:GetMissionIdByQuestId(upgradeQuestId)
            commonArgs.jumpInfo = {
                onJump = function()
                    PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = upgradeMissionId, useBlackMask = true})
                end,
                jumpText = Language.LUA_DOMAIN_SHOP_UNLOCK_QUEST_DESC,
            }
        elseif not string.isEmpty(upgradeQuestId) then
            commonArgs.hintInfo = {
                hintText = Language.LUA_POI_UPGRADE_NEED_COMPLETE_TASK,
                importantHint = true,
            }
        end
    end
end



MapMarkDetailDomainShopCtrl._InitUI = HL.Method() << function(self)
    self.view.mapMarkDetailCommon:_FirstTimeInit()
end

MapMarkDetailDomainShopCtrl._RefreshAllUI = HL.Method() << function(self)
    local info = self.m_info
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(self.m_commonArgs)

    self.view.mapMarkDetailCommonStateController:SetState(info.isUnlock and "Unlocked" or "Locked")
    self.view.lvStateNode:SetState(info.isMaxLv and "Max" or "Nrl")
    self.view.lvNumTxt.text = info.curLv
end





HL.Commit(MapMarkDetailDomainShopCtrl)
