local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponSkillNode = HL.Class('WeaponSkillNode', UIWidgetBase)

WeaponSkillNode.m_weaponSkillCellCache = HL.Field(HL.Forward("UIListCache"))



WeaponSkillNode._OnFirstTimeInit = HL.Override() << function(self)

    self.m_weaponSkillCellCache = UIUtils.genCellCache(self.view.weaponSkillCell)
end

WeaponSkillNode.InitWeaponSkillNode = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, weaponInstId, tryArgs)
    self:_FirstTimeInit()
    self:_RefreshSkillNode(weaponInstId, tryArgs)
end

WeaponSkillNode.InitWeaponSkillNodeByTemplateId = HL.Method(HL.String, HL.Number, HL.Number, HL.Boolean) << function(self, templateId, breakThroughLv, refineLv, isGemMax)
    self:_FirstTimeInit()
    self:_RefreshSkillNodeByTemplateId(templateId, breakThroughLv, refineLv, isGemMax)
end

WeaponSkillNode._RefreshSkillNode = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, weaponInstId, tryArgs)
    local _, weaponInst = GameInstance.player.inventory:TryGetWeaponInst(Utils.getCurrentScope(), weaponInstId)
    local weaponInstData = weaponInst.instData
    if not weaponInstData then
        logger.error("CharInfoUtils->Can't get weapon inst from inventory, weaponInstId: " .. weaponInstId)
        return
    end

    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(weaponInstData.templateId)
    if not weaponCfg then
        logger.error("WeaponExhibitOverview->Can't get weapon basic info, templateId: " .. weaponInstData.templateId)
        return
    end

    local fromBreakthroughLv = weaponInstData.breakthroughLv
    local fromRefineLv = weaponInstData.refineLv
    local tryGemInstId
    local tryRefineLv
    local tryBreakthroughLv
    local skillOffset
    local onlyPotentialSkill = false
    local recommendSkillIdList
    if tryArgs then
        tryGemInstId = tryArgs.tryGemInstId
        tryRefineLv = tryArgs.tryRefineLv
        tryBreakthroughLv = tryArgs.tryBreakthroughLv
        if tryArgs.fromBreakthroughLv ~= nil then
            fromBreakthroughLv = tryArgs.fromBreakthroughLv
            fromRefineLv = tryArgs.fromRefineLv
        end
        skillOffset = tryArgs.skillOffset or 0
        onlyPotentialSkill = tryArgs.onlyPotentialSkill == true
        if not string.isEmpty(tryArgs.tryCharId) then
            local _, recommendCfg = Tables.charWpnSkillRecommendTable:TryGetValue(CSCharUtils.GetVirtualCharTemplateId(tryArgs.tryCharId))
            if recommendCfg then
                recommendSkillIdList = recommendCfg.weaponSkillIds
            end
        end
    else
        tryGemInstId = weaponInstData.attachedGemInstId
        tryRefineLv = weaponInstData.refineLv
        tryBreakthroughLv = weaponInstData.breakthroughLv
        skillOffset = 0
    end
    local _, fromSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(Utils.getCurrentScope(), weaponInstId, nil, fromBreakthroughLv, fromRefineLv)
    local _, toSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(Utils.getCurrentScope(), weaponInstId, tryGemInstId, tryBreakthroughLv, tryRefineLv)
    if tryBreakthroughLv and tryBreakthroughLv > 4 then
        logger.error("WeaponSkillNode->_RefreshSkillNode: tryBreakthroughLv is greater than 4, tryBreakthroughLv: " .. tryBreakthroughLv)
    end
    local _, descList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillDescription(Utils.getCurrentScope(), weaponInstId, tryGemInstId, tryBreakthroughLv, tryRefineLv)


    self.m_weaponSkillCellCache:Refresh(toSkillList.Count, function(cell, index)
        local skillId = toSkillList[CSIndex(index)].skillId

        if onlyPotentialSkill and skillId ~= weaponCfg.weaponPotentialSkill then
            cell.view.gameObject:SetActive(false)
            return
        end
        local desc
        if index > descList.Count then
            desc = ""
            logger.error(string.format("WeaponSkillNode->descList.Count is less than offsetIndex, weaponTemplateId %s, weaponInstId %s, offsetIndex:%s ",weaponInstData.templateId, weaponInst.instId, offsetIndex))
        else
            desc = descList[CSIndex(index)]
        end
        cell:InitWeaponSkillCell(toSkillList[CSIndex(index)], fromSkillList[CSIndex(index)], desc, tryArgs and tryArgs.onlyShowDiff)
        if cell.view.recommendImg then
            local isRecommend = recommendSkillIdList ~= nil and lume.find(recommendSkillIdList, skillId) ~= nil
            cell.view.recommendImg.gameObject:SetActive(isRecommend)
        end
    end)
end

WeaponSkillNode._RefreshSkillNodeByTemplateId = HL.Method(HL.String, HL.Number, HL.Number, HL.Boolean) << function(self, templateId, breakThroughLv, refineLv, isGemMax)
    local _, fromSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(templateId, nil, breakThroughLv, refineLv)
    local toSkillList = fromSkillList
    if isGemMax then
        _, toSkillList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillIdAndLevel(templateId, nil, breakThroughLv, refineLv)
        for _, skill in cs_pairs(toSkillList) do
            skill.level = skill.maxLevel
        end
    end
    if breakThroughLv and breakThroughLv > 4 then
        logger.error("WeaponSkillNode->_RefreshSkillNodeByTemplateId: breakThroughLv is greater than 4, breakThroughLv: " .. breakThroughLv)
    end
    local _, descList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponSkillDescription(templateId, breakThroughLv, refineLv, isGemMax)

    self.m_weaponSkillCellCache:Refresh(fromSkillList.Count, function(cell, index)
        cell:InitWeaponSkillCell(toSkillList[CSIndex(index)], fromSkillList[CSIndex(index)], descList[CSIndex(index)])
    end)
end

HL.Commit(WeaponSkillNode)
return WeaponSkillNode