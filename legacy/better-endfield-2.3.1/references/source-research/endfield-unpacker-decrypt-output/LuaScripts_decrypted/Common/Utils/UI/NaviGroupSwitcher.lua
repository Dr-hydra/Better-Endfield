NaviGroupSwitcher = HL.Class("NaviGroupSwitcher")

NaviGroupSwitcher.m_groupsInfos = HL.Field(HL.Table)

NaviGroupSwitcher.m_bindingGroupId = HL.Field(HL.Number) << -1

NaviGroupSwitcher.m_prevBindingId = HL.Field(HL.Number) << -1

NaviGroupSwitcher.m_nextBindingId = HL.Field(HL.Number) << -1

NaviGroupSwitcher.m_isReverse = HL.Field(HL.Boolean) << false










NaviGroupSwitcher.NaviGroupSwitcher = HL.Constructor(HL.Number, HL.Opt(HL.Table, HL.Boolean)) <<
function(self, parentGroupId, groupInfos, isReverse)
    self.m_isReverse = isReverse == true
    self.m_bindingGroupId = InputManagerInst:CreateGroup(parentGroupId)
    self.m_prevBindingId = InputManagerInst:CreateBindingByActionId("common_switch_area_prev", function()
        self:Move(self.m_isReverse)
    end, self.m_bindingGroupId)
    self.m_nextBindingId = InputManagerInst:CreateBindingByActionId("common_switch_area_next", function()
        self:Move(not self.m_isReverse)
    end, self.m_bindingGroupId)
    self:ChangeGroupInfos(groupInfos)
end

NaviGroupSwitcher._OnTopLayerChanged = HL.Method() << function(self)
end

NaviGroupSwitcher._GetGroupInfo = HL.Method(HL.Boolean).Return(HL.Opt(HL.Table)) << function(self, isNext)
    local count = #self.m_groupsInfos
    local isCurrentGroupFunc = function(naviGroup)
        return NotNull(naviGroup) and naviGroup.IsTopLayer
    end
    for k, info in ipairs(self.m_groupsInfos) do
        local naviGroup = info.naviGroup
        local isCurrentGroup = isCurrentGroupFunc(naviGroup)
        if not isCurrentGroup and info.subGroups ~= nil then
            for _, subGroup in ipairs(info.subGroups) do
                if isCurrentGroupFunc(subGroup) then
                    isCurrentGroup = true
                    break
                end
            end
        end
        if isCurrentGroup then
            return self.m_groupsInfos[(k - 1 + (isNext and 1 or -1)) % count + 1]
        end
    end
    return self.m_groupsInfos[1]
end

NaviGroupSwitcher.ChangeGroupInfos = HL.Method(HL.Opt(HL.Table)) << function(self, groupInfos)
    self:ClearGroupInfos()
    if not groupInfos then
        return
    end
    self.m_groupsInfos = groupInfos
    for _, info in ipairs(self.m_groupsInfos) do
        info._callback = function()
            self:_OnTopLayerChanged()
        end
        info.naviGroup.onIsTopLayerChanged:AddListener(info._callback)
    end
end

NaviGroupSwitcher.ClearGroupInfos = HL.Method() << function(self)
    if not self.m_groupsInfos then
        return
    end
    for _, info in ipairs(self.m_groupsInfos) do
        info.naviGroup.onIsTopLayerChanged:RemoveListener(info._callback)
    end
    self.m_groupsInfos = nil
end

NaviGroupSwitcher.Move = HL.Method(HL.Boolean) << function(self, isNext)
    local info = self:_GetGroupInfo(isNext)
    if not info then
        return
    end
    local forceDefault = info.forceDefault == true
    info.naviGroup:NaviToThisGroup(forceDefault)
end

NaviGroupSwitcher.ToggleActive = HL.Method(HL.Boolean) << function(self, active)
    InputManagerInst:ToggleGroup(self.m_bindingGroupId, active)
end

HL.Commit(NaviGroupSwitcher)
