local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharTalentNode = HL.Class('CharTalentNode', UIWidgetBase)

CharTalentNode.m_talentCellCache = HL.Field(HL.Forward("UIListCache"))


CharTalentNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_talentCellCache = UIUtils.genCellCache(self.view.talentCell)
end

CharTalentNode.InitCharTalentNodeByInstId = HL.Method(HL.Number) << function(self, charInstId)
    self:_FirstTimeInit()

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    self:InitCharTalentNodeByTemplateId(charInfo.templateId, charInfo.breakStage)
end

CharTalentNode.InitCharTalentNodeByTemplateId = HL.Method(HL.String, HL.Number) << function(self, templateId, breakStage)
    self:_FirstTimeInit()

    local talents = CharInfoUtils.getCharTalentInTable(templateId, breakStage)

    self.m_talentCellCache:Refresh(UIConst.CHAR_MAX_TALENT_NUM, function(cell, index)
        local talentInfo = talents[index]

        if talentInfo == nil then
            cell.talentName.text = Language.LUA_LOCKED_TIP
        else
            cell.talentName.text = talentInfo.talentName
        end

        cell.lockedIcon.gameObject:SetActive(talentInfo == nil)
        cell.button.interactable = talentInfo ~= nil
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self:_OnTalentClicked(index, cell, talentInfo)
        end)
    end)

end

CharTalentNode._OnTalentClicked = HL.Method(HL.Number, HL.Table, HL.Table) << function(self, index, cell, talentInfo)
    local arg = {
        skillId = talentInfo.skillId,
        transform = cell.transform,
        unlock = false,
        onClose = function()
            self:RefreshTalentSelect(-1)
        end
    }
    self:RefreshTalentSelect(index)

    Notify(MessageConst.SHOW_CHAR_SKILL_TIP, arg)
end

CharTalentNode.RefreshTalentSelect = HL.Method(HL.Opt(HL.Number)) << function(self, selectIndex)
    local count = self.m_talentCellCache:GetCount()
    for i = 1, count do
        local cell = self.m_talentCellCache:GetItem(i)
        cell.imageSelect.gameObject:SetActive(selectIndex == i)
    end
end


HL.Commit(CharTalentNode)
return CharTalentNode

