local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipEnhanceLevelNode = HL.Class('EquipEnhanceLevelNode', UIWidgetBase)

EquipEnhanceLevelNode.m_levelCellCache = HL.Field(HL.Forward("UIListCache"))

EquipEnhanceLevelNode.enabled = HL.Field(HL.Boolean) << false

EquipEnhanceLevelNode.isEnhanced = HL.Field(HL.Boolean) << false

EquipEnhanceLevelNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_levelCellCache = UIUtils.genCellCache(self.view.lvDotCell)
end






EquipEnhanceLevelNode.InitEquipEnhanceLevelNode = HL.Method(HL.Table) << function(self, args)
    if not EquipTechUtils.canShowEquipEnhanceNode(args.equipInstId) then
        self.view.gameObject:SetActive(false)
        self.enabled = false
        self.isEnhanced = false
        return
    end
    self.enabled = true
    self.view.gameObject:SetActiveIfNecessary(true)

    self:_FirstTimeInit()

    self.view.enhanceNode:InitEquipEnhanceNode(args)
    local enhancedLevel, maxEnhanceLevel = self.view.enhanceNode:GetEnhanceLevel()
    self.isEnhanced = enhancedLevel > 0
    self.m_levelCellCache:Refresh(maxEnhanceLevel, function(cell ,luaIndex)
        local color = luaIndex < enhancedLevel and self.config.COLOR_ENHANCED or self.config.COLOR_NORMAL
        if luaIndex == enhancedLevel then
             color = args.showNextLevel and self.config.COLOR_NEXT_ENHANCED or self.config.COLOR_ENHANCED
        end
        cell.imgDot.color = color
    end)
end

HL.Commit(EquipEnhanceLevelNode)
return EquipEnhanceLevelNode

