local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')


Monster = HL.Class('Monster', UIWidgetBase)


Monster._OnFirstTimeInit = HL.Override() << function(self)
    self.redDot = self.view.redDot
end



Monster.redDot = HL.Field(HL.Forward("RedDot"))

Monster.id = HL.Field(HL.String) << ""

Monster.m_isSelected = HL.Field(HL.Boolean) << false

Monster.m_showingHover = HL.Field(HL.Boolean) << false

Monster.m_enableHoverTips = HL.Field(HL.Boolean) << true



Monster._ResetOnInit = HL.Method() << function(self)
    self.view.button.onIsNaviTargetChanged = nil
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onHoverChange:RemoveAllListeners()
    self.view.button.onLongPress:RemoveAllListeners()
    if self.redDot then
        self.redDot:Stop()
    end
end






Monster.InitMonster = HL.Method(HL.Opt(HL.Any, HL.Any, HL.String, HL.Boolean))
    << function(self, monsterTemplateId, onClick, clickableEvenEmpty)




    self:_FirstTimeInit()
    self:_ResetOnInit()


    local isEmpty = monsterTemplateId == nil or string.isEmpty(monsterTemplateId)
    self.view.content.gameObject:SetActive(not isEmpty)

    if isEmpty then
        self.id = ""
        if clickableEvenEmpty then
            self.view.button.enabled = true
            self.view.button.onClick:AddListener(function()
                onClick(monsterBundle)
            end)
        elseif DeviceInfo.usingController then
            self.view.button.enabled = true
        else
            self.view.button.enabled = false
        end

        self:_UpdateIcon(nil)
        return
    end

    if self.id ~= monsterTemplateId then
        self.id = monsterTemplateId
        self:SetSelected(false)
    end


    local _, data = Tables.EnemyTemplateDisplayInfoTable:TryGetValue(monsterTemplateId)

    if not data then
        logger.error("EnemyTemplateDisplayInfoTable not found: " .. monsterTemplateId)
        return
    end

    self.view.name.text = data.nickname
    if self.view.nameScrollText then
        self.view.nameScrollText:ForceUpdate()
    end

    self:_UpdateIcon(data)

    if onClick then
        self.view.button.enabled = true
        if onClick then
            self.view.button.onClick:AddListener(function()
                onClick(monsterTemplateId)
            end)
        end
    else
        self.view.button.enabled = false
    end

    self.view.button.onHoverChange:RemoveAllListeners()
    if self.view.config.SHOW_HOVER_TIP and not isEmpty then
        self.view.button.onHoverChange:AddListener(function(isHover)
            if not self.m_enableHoverTips and isHover then
                return
            end

            local _, typeInfo = Tables.displayEnemyTypeTable:TryGetValue(data.displayType)
            if isHover and not self.m_isSelected then
                Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                    mainText = data.name,
                    subText = typeInfo.name,
                    delay = self.view.config.HOVER_TIP_DELAY,
                })
                self.m_showingHover = true
            else
                Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
                self.m_showingHover = false
            end
        end)
    end
end

Monster.SetSelected = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, isSelected, forceUpdate)
    self.m_isSelected = isSelected == true
    if isSelected and self.view.config.SHOW_HOVER_TIP then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        self.m_showingHover = false
    end
    self.view.selectedBG.gameObject:SetActive(isSelected)
    self.view.selectedNode.gameObject:SetActive(isSelected)
end

Monster.SetEnableHoverTips = HL.Method(HL.Boolean) << function(self, enabled)
    self.m_enableHoverTips = enabled
    if DeviceInfo.usingController and self.view.button.isNaviTarget then
        if not enabled and self.m_showingHover then
            Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
            self.m_showingHover = false
        end
    end
end



Monster._UpdateIcon = HL.Method(HL.Opt(HL.Any)) << function(self, data)
    local loadPath = self.view.config.USE_BIG_ICON and UIConst.UI_SPRITE_MONSTER_ICON_BIG or UIConst.UI_SPRITE_MONSTER_ICON
    self.view.icon:LoadSprite(loadPath, data.templateId)
end

Monster._OnDestroy = HL.Override() << function(self)
    if self.m_showingHover then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
        self.m_showingHover = false
    end
end

HL.Commit(Monster)
return Monster

