local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemSkillNode = HL.Class('GemSkillNode', UIWidgetBase)

GemSkillNode.m_gemSkillCellCache = HL.Field(HL.Forward("UIListCache"))


GemSkillNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_gemSkillCellCache = UIUtils.genCellCache(self.view.gemSkillCell)

end

GemSkillNode.InitGemSkillNode = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, gemInstId, tryArg)
    self:_FirstTimeInit()

    self:_RefreshSkillNode(gemInstId, tryArg)
end

GemSkillNode._RefreshSkillNode = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, gemInstId, tryArg)
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    self.view.gameObject:SetActive(gemInst ~= nil)
    if not gemInst then
        return
    end
    local activeTagMap
    if tryArg and tryArg.weaponInstId then
        local weaponInst = CharInfoUtils.getWeaponInstInfo(tryArg.weaponInstId)
        local _, weaponSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(Utils.getCurrentScope(), tryArg.weaponInstId, nil, weaponInst.breakthroughLv, weaponInst.refineLv)
        activeTagMap = {}
        for _, skillLevelInfo in pairs(weaponSkillList) do
            local skillCfg = CharInfoUtils.getSkillCfg(skillLevelInfo.skillId, skillLevelInfo.level)
            activeTagMap[skillCfg.tagId] = true
        end
    end
    local termCount = gemInst.termList.Count
    self.m_gemSkillCellCache:Refresh(termCount, function(cell, index)
        local termId = gemInst.termList[CSIndex(index)].termId
        local termLevel = gemInst.termList[CSIndex(index)].cost
        local _, termCfg = Tables.gemTable:TryGetValue(termId)

        local isActive = true
        if activeTagMap ~= nil then
            isActive = activeTagMap[termCfg.tagId] == true
        end
        cell:InitGemSkillCell(termId, termLevel, isActive)
        cell.view.bottomLine.gameObject:SetActive(index < termCount)
    end)
end

HL.Commit(GemSkillNode)
return GemSkillNode

