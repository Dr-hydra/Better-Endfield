local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SkillTipCell = HL.Class('SkillTipCell', UIWidgetBase)


SkillTipCell._OnFirstTimeInit = HL.Override() << function(self)

end

SkillTipCell.info = HL.Field(HL.Table)

SkillTipCell.onClick = HL.Field(HL.Function)

SkillTipCell.InitSkillTipCell = HL.Method(HL.Any, HL.Boolean, HL.Opt(HL.Function)) << function(self, info, isSelectable, onClick)
    self:_FirstTimeInit()

    self.info = info
    self.view.btnSelect.gameObject:SetActive(isSelectable)
    self.view.btnSelect.onClick:RemoveAllListeners()
    if isSelectable then
        if onClick then
            self.view.btnSelect.onClick:AddListener(function()
                local isInFight = Utils.isInFight()
                if isInFight then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SKILL_IN_FIGHT_FORBID_INTERACT_TOAST)
                    return
                end
                onClick(self.info)
            end)
        end
    end
    self:_InitBaseInfo()
end

SkillTipCell._InitBaseInfo = HL.Method() << function(self)
    local skillData = self.info.skillData
    local charId = self.info.charId
    local patchData = skillData.patchData
    local skillId = patchData.skillId
    local level = skillData.level
    local realMaxLevel = skillData.realMaxLevel
    local bundleData = skillData.bundleData
    local skillType = bundleData.skillType
    local showBottom = false
    local inUse = skillData.inUse
    local skillTypeText = CharInfoUtils.getSkillTypeName(skillType)

    if skillData.showInUse == nil then
        skillData.showInUse = false

        if skillType == Const.SkillTypeEnum.NormalSkill then
            showBottom = true
            skillData.showInUse = true
        end
    end

    local showInUse = skillData.showInUse
    showBottom = showInUse


    self.view.buttonSkill:InitCharInfoSkillButton(skillData)

    self.view.skillNameTxt.text = patchData.skillName

    self.view.skillTypeTxt.text = skillTypeText

    local description = Utils.SkillUtil.GetSkillDescription(skillId, level)
    self.view.itemDescTxt:SetAndResolveTextStyle(description)
    local rankText = string.format("RANK %d", level)
    if skillType == Const.SkillTypeEnum.NormalAttack then
        rankText = "RANK MAX"
    end
    self.view.rankText.text = rankText

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.itemDescTxt.transform)
    local descHeight = self.view.itemDescTxt.transform.rect.size.y
    local descShowHeight = lume.clamp(descHeight, self.view.config.MIN_CONTENT_HEIGHT, self.view.config.MAX_CONTENT_HEIGHT)
    local canScroll = descHeight > descShowHeight
    self.view.detailScrollLayoutElement.preferredHeight = descShowHeight
    if canScroll then
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.detailScroll.transform)
    end


    self.view.bottomButtons.gameObject:SetActive(showBottom)

    local unlock = skillData.unlock

    if showBottom then
        self.view.btnSelect.gameObject:SetActive(unlock and not inUse and showInUse)
        self.view.bgSelect.gameObject:SetActive(unlock and inUse and showInUse)
        self.view.unableText.gameObject:SetActive(not unlock)
    end

    if not unlock then
        local textNum = Language[string.format("LUA_NUM_%d", bundleData.breakStage)]
        self.view.unableText.text = string.format(Language.LUA_SKILL_UNLOCK_HINT, textNum)
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.bottomButtons.transform)

end

SkillTipCell.PlayAnim = HL.Method(HL.String, HL.Opt(HL.Function)) << function(self, name, callback)
    if callback then
        self.view.animationWrapper:PlayWithTween(name, function()
            callback()
        end)
    else
        self.view.animationWrapper:PlayWithTween(name)
    end
end

HL.Commit(SkillTipCell)
return SkillTipCell
