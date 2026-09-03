local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

local DECK_ATTR_CHANGED_TOAST_DELAY = 0.25

CharInfoSystem = HL.Class('CharInfoSystem', LuaSystemBase.LuaSystemBase)

CharInfoSystem.m_charActiveSkillGroupConditionIdxMap = HL.Field(HL.Table)
CharInfoSystem.m_pendingDeckAttrChangedCharMap = HL.Field(HL.Table)
CharInfoSystem.m_deckAttrChangedToastTimerId = HL.Field(HL.Number) << -1



CharInfoSystem.CharInfoSystem = HL.Constructor() << function(self)
    self.m_charActiveSkillGroupConditionIdxMap = {}
    self.m_pendingDeckAttrChangedCharMap = {}
    self.m_deckAttrChangedToastTimerId = -1
end

CharInfoSystem.OnInit = HL.Override() << function(self)
    self.m_charActiveSkillGroupConditionIdxMap = {}
    self.m_pendingDeckAttrChangedCharMap = {}
    self.m_deckAttrChangedToastTimerId = -1
    self:_BindMsg()
    self:_RefreshAllActiveSkillGroupConditionIdx()
end

CharInfoSystem.OnRelease = HL.Override() << function(self)
    self.m_charActiveSkillGroupConditionIdxMap = {}
    self.m_pendingDeckAttrChangedCharMap = {}
    if self.m_deckAttrChangedToastTimerId > 0 then
        self.m_deckAttrChangedToastTimerId = self:_ClearTimer(self.m_deckAttrChangedToastTimerId)
    end
end

CharInfoSystem._BindMsg = HL.Method() << function(self)
    self:RegisterMessage(MessageConst.ON_CHAR_DECK_ATTR_CHANGED, function(arg)
        self:_OnCharDeckAttrChanged(arg)
    end)
end





CharInfoSystem._RefreshAllActiveSkillGroupConditionIdx = HL.Method() << function(self)
    local charInfoList = CharInfoUtils.getAllCharInfoList()
    for _, charInfo in ipairs(charInfoList) do
        self.m_charActiveSkillGroupConditionIdxMap[charInfo.instId] = self:_GetActiveSkillGroupConditionIdx(charInfo.instId, charInfo.templateId)
    end
end

CharInfoSystem._GetFirstActiveSkillGroupConditionData = HL.Method(HL.Number, HL.String).Return(HL.Opt(HL.Table))
        << function(self, charInstId, templateId)
    for _, skillGroupType in ipairs(UIConst.CHAR_INFO_SKILL_SHOW_ORDER) do
        local skillGroupCfg = CharInfoUtils.getCharSkillGroupCfgByType(templateId, skillGroupType)
        if skillGroupCfg and CharInfoUtils.hasBothSkillGroupConditions(skillGroupCfg) then
            return CharInfoUtils.getSkillGroupConditionDisplayData(charInstId, skillGroupCfg)
        end
    end

    return nil
end

CharInfoSystem._GetActiveSkillGroupConditionIdx = HL.Method(HL.Number, HL.String).Return(HL.Number)
        << function(self, charInstId, templateId)
    local data = self:_GetFirstActiveSkillGroupConditionData(charInstId, templateId)
    return data and data.index or 0
end


CharInfoSystem._OnCharDeckAttrChanged = HL.Method(HL.Table) << function(self, arg)
    local instId = unpack(arg)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    if not charInfo then
        return
    end

    self.m_pendingDeckAttrChangedCharMap[instId] = true
    if self.m_deckAttrChangedToastTimerId > 0 then
        logger.info("[CharInfoSystem] _OnCharDeckAttrChanged: 在倒计时窗口内, 直接return")
        return
    end

    logger.info("[CharInfoSystem] _OnCharDeckAttrChanged: 设立倒计时")
    self.m_deckAttrChangedToastTimerId = self:_StartTimer(DECK_ATTR_CHANGED_TOAST_DELAY, function()
        self:_FlushDeckAttrChangedToast()
    end)
end

CharInfoSystem._FlushDeckAttrChangedToast = HL.Method() << function(self)
    self.m_deckAttrChangedToastTimerId = -1
    local pendingCharMap = self.m_pendingDeckAttrChangedCharMap
    self.m_pendingDeckAttrChangedCharMap = {}
    for instId, _ in pairs(pendingCharMap) do
        self:_TryNotifyDeckAttrChangedToast(instId)
    end
end

CharInfoSystem._TryNotifyDeckAttrChangedToast = HL.Method(HL.Number) << function(self, instId)
    logger.info("[CharInfoSystem] _TryNotifyDeckAttrChangedToast, instId: " .. tostring(instId))

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    if not charInfo then
        return
    end

    local oldIdx = self.m_charActiveSkillGroupConditionIdxMap[instId] or 0
    local data = self:_GetFirstActiveSkillGroupConditionData(instId, charInfo.templateId)
    local newIdx = data and data.index or 0
    if oldIdx ~= 0 and newIdx ~= 0 and oldIdx ~= newIdx and data and not string.isEmpty(data.toast) then
        Notify(MessageConst.SHOW_TOAST, data.toast)
    end
    self.m_charActiveSkillGroupConditionIdxMap[instId] = newIdx
end



HL.Commit(CharInfoSystem)
return CharInfoSystem
