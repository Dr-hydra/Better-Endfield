local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharSkillNodeNew = HL.Class('CharSkillNodeNew', UIWidgetBase)

CharSkillNodeNew.m_skillCells = HL.Field(HL.Forward("UIListCache"))

CharSkillNodeNew.m_lastSelectIndex = HL.Field(HL.Number) << -1






CharSkillNodeNew.m_arg = HL.Field(HL.Any)

CharSkillNodeNew._OnFirstTimeInit = HL.Override() << function(self)
    self.m_skillCells = UIUtils.genCellCache(self.view.skillCell)
    self.m_lastSelectIndex = -1





    self:RegisterMessage(MessageConst.ON_CHAR_DECK_ATTR_CHANGED, function(arg)
        self:_OnCharDeckAttrChanged(arg)
    end)
end









CharSkillNodeNew.InitCharSkillNodeNew = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    self.m_arg = arg

    self:_RefreshUI()
end

CharSkillNodeNew._RefreshUI = HL.Method() << function(self)
    local arg = self.m_arg
    local charInstId = arg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    self.m_skillCells:Refresh(#UIConst.CHAR_INFO_SKILL_SHOW_ORDER, function(cell, luaIndex)
        local skillGroupType = UIConst.CHAR_INFO_SKILL_SHOW_ORDER[luaIndex]
        cell:InitCharInfoSkillButtonNew(charInst, skillGroupType, function()
            Notify(MessageConst.SHOW_CHAR_SKILL_TIP, {
                skillGroupType = skillGroupType,
                charInstId = charInstId,
                transform = arg.tipsNode or cell.view.showTipTransform,
                isSingleChar = arg.isSingleChar,
                hideBtnUpgrade = arg.hideBtnUpgrade,
                tipPosType = arg.tipPosType,


                cell = cell,
                enableCloseActionOnController = arg.enableCloseActionOnController,
            })
        end)
        self:_RefreshOneStanceBadge(cell, charInst, skillGroupType)
    end)
end



CharSkillNodeNew._RefreshOneStanceBadge = HL.Method(HL.Any, HL.Any, HL.Any) << function(self, cell, charInst, skillGroupType)
    local stance = cell.view.stanceSwitchSkill
    if not stance then
        return
    end
    local skillGroupCfg = CharInfoUtils.getCharSkillGroupCfgByType(charInst.templateId, skillGroupType)
    if not skillGroupCfg or not CharInfoUtils.hasBothSkillGroupConditions(skillGroupCfg) then
        stance.gameObject:SetActive(false)
        return
    end
    stance.gameObject:SetActive(true)
    local activeIdx = CharInfoUtils.getActiveSkillGroupConditionIdx(charInst.instId, skillGroupCfg)
    stance.yangImg.gameObject:SetActive(activeIdx == 1)
    stance.yinImg.gameObject:SetActive(activeIdx == 2)
end



CharSkillNodeNew._OnCharDeckAttrChanged = HL.Method(HL.Table) << function(self, arg)
    if not self.m_arg then
        return
    end
    local instId = unpack(arg)
    if instId ~= self.m_arg.charInstId then
        return
    end
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    if not charInst then
        return
    end
    local count = self.m_skillCells:GetCount()
    for i = 1, count do
        local cell = self.m_skillCells:GetItem(i)
        if cell.gameObject.activeSelf then
            local skillGroupType = UIConst.CHAR_INFO_SKILL_SHOW_ORDER[i]
            cell:RefreshSkillIcon()
            self:_RefreshOneStanceBadge(cell, charInst, skillGroupType)
        end
    end
end

CharSkillNodeNew.RefreshSkillSelect = HL.Method(HL.Opt(HL.Number)) << function(self, selectIndex)
    local count = self.m_skillCells:GetCount()
    for i = 1, count do
        local cell = self.m_skillCells:GetItem(i)
        cell:SetSelect(selectIndex == i)
    end
end

HL.Commit(CharSkillNodeNew)
return CharSkillNodeNew

