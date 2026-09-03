local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharPassiveSkillNode = HL.Class('CharPassiveSkillNode', UIWidgetBase)

CharPassiveSkillNode.m_passiveSkillCellCache = HL.Field(HL.Forward("UIListCache"))


CharPassiveSkillNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_passiveSkillCellCache = UIUtils.genCellCache(self.view.passiveSkillCell)

end









CharPassiveSkillNode.InitCharPassiveSkillNode = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    local charInstId = arg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local luaPassiveSkillNodeList = {}
    local _, passiveSkillNodeList, _ = CharInfoUtils.classifyTalentNode(charInst.templateId)

    local passiveSkillAList = passiveSkillNodeList[0]
    local passiveSkillBList = passiveSkillNodeList[1]

    if passiveSkillAList then
        table.insert(luaPassiveSkillNodeList, passiveSkillAList)
    end
    if passiveSkillBList then
        table.insert(luaPassiveSkillNodeList, passiveSkillBList)
    end

    local highestA
    if passiveSkillAList then
        highestA = passiveSkillAList[1]
        for i, passiveSkillNodeCfg in ipairs(passiveSkillAList) do
            local isActive = CharInfoUtils.getPassiveSkillNodeStatus(charInst.instId, passiveSkillNodeCfg.nodeId)
            if isActive then
                highestA = passiveSkillNodeCfg
            end
        end
    end


    local highestB
    if passiveSkillBList then
        highestB = passiveSkillBList[1]
        for i, passiveSkillNodeCfg in ipairs(passiveSkillBList) do
            local isActive = CharInfoUtils.getPassiveSkillNodeStatus(charInst.instId, passiveSkillNodeCfg.nodeId)
            if isActive then
                highestB = passiveSkillNodeCfg
            end
        end
    end

    local highestList = {}
    if highestA then
        table.insert(highestList, highestA)
    end
    if highestB then
        table.insert(highestList, highestB)
    end
    self.m_passiveSkillCellCache:Refresh(#highestList, function(cell, index)
        local passiveSkillNodeCfg = highestList[index]
        local isActive = CharInfoUtils.getPassiveSkillNodeStatus(charInst.instId, passiveSkillNodeCfg.nodeId)

        cell.stageLevelCellGroup:InitStageLevelCellGroupByPassiveNodeList(charInst.instId, luaPassiveSkillNodeList[index])
        cell.stageBg.gameObject:SetActive(cell.stageLevelCellGroup.gameObject.activeSelf)
        if not cell.stageBgCellCache then
            cell.stageBgCellCache = UIUtils.genCellCache(cell.stageBgCell)
        end
        cell.stageBgCellCache:Refresh(#luaPassiveSkillNodeList[index])
        cell.name.text = passiveSkillNodeCfg.passiveSkillNodeInfo.name
        cell.lockIcon.gameObject:SetActive(not isActive)
        cell.icon.gameObject:SetActive(isActive)
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, passiveSkillNodeCfg.passiveSkillNodeInfo.iconId)
        cell.name.color = isActive and self.view.config.TEXT_COLOR_DEFAULT or self.view.config.TEXT_COLOR_LOCK

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            Notify(MessageConst.SHOW_CHAR_SKILL_TIP, {
                isPassiveSkill = true,
                skillId = passiveSkillNodeCfg.passiveSkillNodeInfo.index,
                skillLevel = passiveSkillNodeCfg.passiveSkillNodeInfo.level,
                charInstId = charInstId,
                transform = arg.tipsNode or cell.showTipTransform,
                isSingleChar = arg.isSingleChar,
                hideBtnUpgrade = arg.hideBtnUpgrade,
                tipPosType = arg.tipPosType,
                isLock = not isActive,


                cell = cell,
                enableCloseActionOnController = arg.enableCloseActionOnController,
            })
        end)
    end)
end

HL.Commit(CharPassiveSkillNode)
return CharPassiveSkillNode

