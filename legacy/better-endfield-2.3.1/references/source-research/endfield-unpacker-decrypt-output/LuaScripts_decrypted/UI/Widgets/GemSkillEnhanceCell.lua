local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GemSkillEnhanceCell = HL.Class('GemSkillEnhanceCell', UIWidgetBase)


GemSkillEnhanceCell._OnFirstTimeInit = HL.Override() << function(self)

end






GemSkillEnhanceCell.InitGemSkillEnhanceCell = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local isEmpty = args == nil or args.termId == nil
    self.view.noSkillNode.gameObject:SetActive(isEmpty)
    self.view.skillNode.gameObject:SetActive(not isEmpty)
    if isEmpty then
        return
    end
    local _, termCfg = Tables.gemTable:TryGetValue(args.termId)
    if not termCfg then
        logger.LogError("GemSkillEnhanceCell.InitGemSkillEnhanceCell: termCfg is nil, termId = " .. tostring(args.termId))
        return
    end
    self.view.skillNode.txtAttrName:SetAndResolveTextStyle(string.format(Language.LUA_GEM_CARD_SKILL_ACTIVE, termCfg.tagName))
    self.view.skillNode.rankValue.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, args.termLevel)
    CSUtils.UIContainerResize(self.view.skillNode.levelNode, args.termLevel, 1)
    if args.onClick then
        self.view.skillNode.btnEnhance.onClick:RemoveAllListeners()
        self.view.skillNode.btnEnhance.onClick:AddListener(function()
            args.onClick(args.termId, args.termLevel)
        end)
    end
    self.view.stateController:SetState("notTarget")
    self.view.skillNode.btnEnhance.onIsNaviTargetChanged = function(isTarget)
        local stateName = isTarget and "naviTarget" or "notTarget"
        self.view.stateController:SetState(stateName)
    end
    local isMax = CharInfoUtils.isGemTermEnhanceMax(args.termId, args.termLevel)
    self.view.skillNode.stateController:SetState(isMax and "Max" or "Normal")
end

HL.Commit(GemSkillEnhanceCell)
return GemSkillEnhanceCell

