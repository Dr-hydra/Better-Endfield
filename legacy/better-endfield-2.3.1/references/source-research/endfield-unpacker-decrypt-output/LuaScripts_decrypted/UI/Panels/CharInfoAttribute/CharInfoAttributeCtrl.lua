local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoAttribute
CharInfoAttributeCtrl = HL.Class('CharInfoAttributeCtrl', uiCtrl.UICtrl)







CharInfoAttributeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = 'OnSelectCharChange',
    [MessageConst.CHAR_INFO_PAGE_CHANGE] = 'OnSelectTabChange',
    [MessageConst.ON_UNLOCK_SKILL] = '_OnUnlockSkill',
    [MessageConst.CHAR_NORMAL_SKILL_CHANGE] = '_OnCharNormalSkillChange',
}

CharInfoAttributeCtrl.m_charInfo = HL.Field(HL.Table)

CharInfoAttributeCtrl.m_curMainControlTab = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

CharInfoAttributeCtrl.m_curSelectSlotIndex = HL.Field(HL.Number) << -1

CharInfoAttributeCtrl.m_overviewAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoAttributeCtrl.m_basicInfoStarCellCache = HL.Field(HL.Forward("UIListCache"))

CharInfoAttributeCtrl.m_talentCellCache = HL.Field(HL.Table)

CharInfoAttributeCtrl.m_talentList = HL.Field(HL.Forward("UIListCache"))


CharInfoAttributeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local mainControlTab = arg.mainControlTab or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW
    local phase = arg.phase

    self.m_charInfo = initCharInfo
    self.m_curMainControlTab = mainControlTab
    self.m_talentCellCache = {}
    self.m_talentList = UIUtils.genCellCache(self.view.overviewNode.talentNode.talentList)

    self:_InitActionEvent()
    self:_RefreshAttributePanel(initCharInfo, mainControlTab)
end

CharInfoAttributeCtrl._OnCharNormalSkillChange = HL.Method(HL.Table) << function(self, arg)
    local instId = self.m_charInfo.instId
    local templateId = self.m_charInfo.templateId
    self.view.overviewNode.charSkillNode:RefreshSkills(instId, templateId)
    self.view.overviewNode.charSkillNode:RefreshSkillSelect()
end

CharInfoAttributeCtrl._OnUnlockSkill = HL.Method(HL.Table) << function(self, arg)
    local instId = self.m_charInfo.instId
    local templateId = self.m_charInfo.templateId
    self.view.overviewNode.charSkillNode:RefreshSkills(instId, templateId)
end

CharInfoAttributeCtrl.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charInfo = charInfo
    self.view.overviewNode.redDot:InitRedDot("CharBreak", self.m_charInfo.instId)
    self:_RefreshAttributePanel(charInfo, self.m_curMainControlTab)
end

CharInfoAttributeCtrl.OnSelectTabChange = HL.Method(HL.Number) << function(self, tabType)
    if tabType == self.m_curMainControlTab then
        return
    end
    self.m_curMainControlTab = tabType
    self:_RefreshAttributePanel(self.m_charInfo, tabType)
end

CharInfoAttributeCtrl.OnSelectWhenEnabled = HL.Method(HL.Number) << function(self, tabType)
    local charInfo = self.m_charInfo
    local instId = charInfo.instId
    local curExp, levelUpExp, curLevel, maxLevel = CharInfoUtils.getCharExpInfo(instId)
    local realMaxLevel = Tables.characterConst.maxLevel

    if curLevel < realMaxLevel and curLevel < maxLevel then
        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE
        })
        return
    end
end

CharInfoAttributeCtrl._InitActionEvent = HL.Method() << function(self)
    local overviewNode = self.view.overviewNode
    self.m_overviewAttributeCellCache = UIUtils.genCellCache(overviewNode.charInfoAttributeCell)

    self.m_basicInfoStarCellCache = UIUtils.genCellCache(overviewNode.basicInfoNode.iconStarShadow)

    overviewNode.levelUpButton.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE
        })
    end)
    overviewNode.detailButton.onClick:AddListener(function()
        self:_OnClickDetailButton()
    end)

    overviewNode.redDot:InitRedDot("CharBreak", self.m_charInfo.instId)
end


CharInfoAttributeCtrl._RefreshAttributePanel = HL.Method(HL.Table, HL.Number, HL.Opt(HL.Number, HL.Number, HL.Number))
        << function(self, charInfo, mainControlTab, selectedEquipInstId, compareEquipInstId, slotIndex)
    local isOverview = mainControlTab == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW
    self.view.overviewNode.gameObject:SetActive(isOverview)

    if isOverview then
        self:_RefreshOverviewNode(charInfo)
        return
    end
end

CharInfoAttributeCtrl._RefreshOverviewNode = HL.Method(HL.Table) << function(self, charInfo)
    local instId = charInfo.instId
    local templateId = charInfo.templateId
    local allAttributeList = CharInfoUtils.getCharFinalAttributes(instId)
    local showAttributeList = CharInfoUtils.generateBasicAttributeShowInfoList(allAttributeList)
    local overviewNode = self.view.overviewNode

    self.m_overviewAttributeCellCache:Refresh(#showAttributeList, function(cell, index)
        local attributeInfo = showAttributeList[index]
        local attributeKey = Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attributeInfo.attributeType]
        cell.numText.text = attributeInfo.showValue
        cell.mainText.text = attributeInfo.showName
        cell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, attributeInfo.iconName)
    end)
    overviewNode.detailButton.transform:SetAsLastSibling()
    overviewNode.charSkillNode:SetShowMode(UIConst.CHAR_SKILL_MODE.ShowSkillTypeName)
    overviewNode.charSkillNode:RefreshSkills(instId, templateId)
    overviewNode.attributeTitle.titleText.text = Language.LUA_CHAR_INFO_OVERVIEW_ATTRIBUTE_TITLE
    overviewNode.attributeTitle.shadowTitleText.text = Language.LUA_CHAR_INFO_OVERVIEW_ATTRIBUTE_TITLE

    overviewNode.skillTitle.titleText.text = Language.LUA_CHAR_INFO_OVERVIEW_SKILL_TITLE
    overviewNode.skillTitle.shadowTitleText.text = Language.LUA_CHAR_INFO_OVERVIEW_SKILL_TITLE


    local realMaxLevel = Tables.characterConst.maxLevel

    local curExp, levelUpExp, curLevel, maxLevel = CharInfoUtils.getCharExpInfo(instId)
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    overviewNode.levelUpButton.gameObject:SetActive(curLevel < realMaxLevel and curLevel < maxLevel)
    overviewNode.breakButton.gameObject:SetActive(curLevel < realMaxLevel and curLevel >= maxLevel)
    overviewNode.levelText.text = curLevel
    overviewNode.maxLevel.text = maxLevel
    overviewNode.expBar.value = curExp / levelUpExp
    overviewNode.levelBreakNode:InitLevelBreakNode(charInfo.breakStage)

    local characterTable = Tables.characterTable
    local charData = characterTable:GetValue(templateId)
    local basicInfoNode = overviewNode.basicInfoNode


    basicInfoNode.textName.text = charData.name
    basicInfoNode.textNameShadow.text = charData.name
    local elementSpriteName = UIConst.UI_CHAR_ELEMENT_PREFIX .. charData.energyShardType:ToInt()
    basicInfoNode.elementIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_ELEMENT, elementSpriteName)
    basicInfoNode.elementIconShadow:LoadSprite(UIConst.UI_SPRITE_CHAR_ELEMENT,
            elementSpriteName)
    local proSpriteName = CharInfoUtils.getCharProfessionIconName(charData.profession)
    basicInfoNode.imageProShadow:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)
    basicInfoNode.imagePro:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)
    self.m_basicInfoStarCellCache:Refresh(charData.rarity)


    local breakStage = charInfo.breakStage
    local maxBreakStage = Tables.characterConst.maxBreak
    local talents = CharInfoUtils.getCharBreakStageTalents(templateId, breakStage, maxBreakStage, true)
    self:_RefreshTalent(talents)



    LayoutRebuilder.ForceRebuildLayoutImmediate(overviewNode.basicInfoNode.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(overviewNode.attributeInfo)
    LayoutRebuilder.ForceRebuildLayoutImmediate(overviewNode.skillInfo)
    LayoutRebuilder.ForceRebuildLayoutImmediate(overviewNode.talentNode.transform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(overviewNode.mainInfoNode)
end

CharInfoAttributeCtrl._RefreshTalent = HL.Method(HL.Any) << function(self, talents)
    local unlockTalents = talents.unlockTalents
    local enhancedTalents = talents.enhancedTalents
    local unlockCount = #unlockTalents
    local enhancedCount = #enhancedTalents

    local num = unlockCount + enhancedCount
    local row = math.ceil(num / UIConst.TALENT_COLUMN_NUM)
    self:_ClearTalent()
    self.m_talentList:Refresh(row, function(talentList, rowIndex)
        local cellNum = math.min(num - (rowIndex - 1) * UIConst.TALENT_COLUMN_NUM, UIConst.TALENT_COLUMN_NUM)
        local cellCache = UIUtils.genCellCache(talentList.buttonTalent)
        self.m_talentCellCache[rowIndex] = cellCache
        cellCache:Refresh(cellNum, function(cell, columnIndex)
            local cellIndex = (rowIndex - 1) * UIConst.TALENT_COLUMN_NUM + columnIndex
            local unlock = cellIndex > enhancedCount
            local talent = enhancedTalents[cellIndex] or unlockTalents[cellIndex - enhancedCount]
            local talentData = talent.talentData
            local text = cell.text
            text.text = talentData.talentName

            if unlock then
                UIUtils.changeAlpha(text, UIConst.LOCK_ALPHA)
            else
                UIUtils.changeAlpha(text, 1)
            end

            LayoutRebuilder.ForceRebuildLayoutImmediate(cell.transform)
            cell.lockedIcon.gameObject:SetActive(unlock)
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                local talentInfo = {
                    talentData = talentData,
                    nextBreakStage = talent.nextBreakStage,
                    transform = cell.transform,
                    unlock = unlock,
                }
                self:Notify(MessageConst.ON_CHAR_SHOW_TALENT_TIPS, talentInfo)
                self:_OnShowTalent(cellIndex)
            end)
        end)
    end)
end

CharInfoAttributeCtrl._ClearTalent = HL.Method() << function(self)
    for _, cellCache in pairs(self.m_talentCellCache) do
        cellCache:Refresh(0)
    end

    self.m_talentList:Refresh(0)
    self.m_talentCellCache = {}
end

CharInfoAttributeCtrl._OnClickDetailButton = HL.Method() << function(self)
    local phase = self.m_phase
    if phase then
        phase:CreateCharInfoPanel(PanelId.CharInfoFullAttribute, {
            initCharInfo = self.m_charInfo,
            mainControlTab = self.m_curMainControlTab,
            phase = self.m_phase
        })
    end
end

CharInfoAttributeCtrl._OnShowSkills = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_OnShowTalent(-1)
end

CharInfoAttributeCtrl._OnShowTalent = HL.Method(HL.Number) << function(self, talentIndex)
    self.view.overviewNode.charSkillNode:RefreshSkillSelect()
    for rowIndex, cellCache in pairs(self.m_talentCellCache) do
        for columnIndex = 1, cellCache:GetCount() do
            local cellIndex = (rowIndex - 1) * UIConst.TALENT_COLUMN_NUM + columnIndex
            local cell = cellCache:Get(columnIndex)
            cell.imageSelect.gameObject:SetActive(cellIndex == talentIndex)
        end
    end

    self.view.overviewNode.charSkillNode:RefreshSkillSelect()
end

HL.Commit(CharInfoAttributeCtrl)
