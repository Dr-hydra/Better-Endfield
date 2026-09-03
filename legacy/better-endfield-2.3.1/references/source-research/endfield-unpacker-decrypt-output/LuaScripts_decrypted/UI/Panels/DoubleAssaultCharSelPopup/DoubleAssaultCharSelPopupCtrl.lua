
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DoubleAssaultCharSelPopup
local activityUtils = require_ex('Common/Utils/ActivityUtils')

DoubleAssaultCharSelPopupCtrl = HL.Class('DoubleAssaultCharSelPopupCtrl', uiCtrl.UICtrl)

local SelfTeamIndex = 1
DoubleAssaultCharSelPopupCtrl.m_activityId = HL.Field(HL.String) << ''





DoubleAssaultCharSelPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
}


DoubleAssaultCharSelPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId or ''

    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    local gameId = arg.gameId
    local selectedTeamIndex = arg.teamIndex
    local teamConfigIds = activityUtils.getDoubleAssaultTeamConfigIds(gameId)
    if teamConfigIds == nil then
        return
    end

    local commonTeamId = activityUtils.getDoubleAssaultDefaultTeamId(gameId)
    if commonTeamId == nil then
        return
    end

    local isSelfSelected = selectedTeamIndex == SelfTeamIndex
    self.view.selBtbState:SetState(isSelfSelected and 'SelectState' or 'UnSelectState')
    self.view.selBtn.onClick:RemoveAllListeners()
    self.view.selBtn.onClick:AddListener(function()
        if isSelfSelected then
            return
        end
        if not string.isEmpty(self.m_activityId) and not GameInstance.player.activitySystem:GetActivity(self.m_activityId) then
            UIManager:Close(PANEL_ID)
            return
        end
        arg.onSelect(SelfTeamIndex, commonTeamId)
        self:PlayAnimationOutAndClose()
    end)
    InputManagerInst:ToggleGroup(self.view.selBtn.groupId, not isSelfSelected)

    local genTeamCell = UIUtils.genCellCache(self.view.doubleAssaultCharSelSystemCell)
    genTeamCell:Refresh(teamConfigIds.Count - SelfTeamIndex, function(cell, index)
        local teamIndex = index + SelfTeamIndex
        local teamId = teamConfigIds[CSIndex(teamIndex)]
        local success, teamCfg = Tables.charTeamTable:TryGetValue(teamId)
        if not success then
            logger.error("没有找到对应的队伍配置，teamId:" .. teamId)
            return
        end

        cell.charTxt.text = string.format(Language.LUA_ACT_DOUBLE_ASSAULT_CHAR_SEL_TEAM_NAME, string.char(string.byte('A') + CSIndex(index)))

        cell.selBtn.onClick:RemoveAllListeners()
        cell.selBtn.onClick:AddListener(function()
            if teamIndex == selectedTeamIndex then
                return
            end
            if not string.isEmpty(self.m_activityId) and not GameInstance.player.activitySystem:GetActivity(self.m_activityId) then
                UIManager:Close(PANEL_ID)
                return
            end
            arg.onSelect(teamIndex, teamId)
            self:PlayAnimationOutAndClose()
        end)
        local isSelected = teamIndex == selectedTeamIndex
        cell.selBtnState:SetState(isSelected and 'SelectState' or 'UnSelectState')
        InputManagerInst:ToggleGroup(cell.selBtn.groupId, not isSelected)

        local charGenCell = UIUtils.genCellCache(cell.charHeadCell)
        charGenCell:Refresh(teamCfg.presetCharList.Count, function(charCell, charIndex)
            local presetCharId = teamCfg.presetCharList[CSIndex(charIndex)]

            charCell:InitCharFormationHeadCell({

                charPresetId = presetCharId,
                isReplaceable = true,
                isTrail = true,
            })
        end)

    end)

    if DeviceInfo.usingController then
        local target
        if selectedTeamIndex == SelfTeamIndex then
            target = self.view.doubleAssaultCharSelCell
        else
            local targetCell = genTeamCell:Get(selectedTeamIndex - SelfTeamIndex)
            if targetCell then
                target = targetCell.inputBindingGroupNaviDecorator
            end
        end
        target = target or self.view.doubleAssaultCharSelCell
        if target then
            self:SetNaviTarget(target)
        end
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

DoubleAssaultCharSelPopupCtrl.OnActivityUpdated = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if string.isEmpty(self.m_activityId) or activityId ~= self.m_activityId then
        return
    end

    if not GameInstance.player.activitySystem:GetActivity(activityId) then
        UIManager:Close(PANEL_ID)
    end
end










HL.Commit(DoubleAssaultCharSelPopupCtrl)
