
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.OutOfScreenTargets
OutOfScreenTargetsCtrl = HL.Class('OutOfScreenTargetsCtrl', uiCtrl.UICtrl)







OutOfScreenTargetsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.TOGGLE_DEBUG_OUT_OF_SCREEN_TARGETS_MODE] = 'OnToggleDebugOutOfScreenTargetsMode',
}

OutOfScreenTargetsCtrl.m_arrows = HL.Field(HL.Table)

OutOfScreenTargetsCtrl.m_arrowsCache = HL.Field(HL.Table)

OutOfScreenTargetsCtrl.m_arrowsWithOutAnimation = HL.Field(HL.Table)

OutOfScreenTargetsCtrl.m_lateTickKey = HL.Field(HL.Number) << -1
OutOfScreenTargetsCtrl.m_isGPUMode = HL.Field(HL.Boolean) << true
OutOfScreenTargetsCtrl.m_isGPUModeInited = HL.Field(HL.Boolean) << false
OutOfScreenTargetsCtrl.m_isCPUModeInited = HL.Field(HL.Boolean) << false


OutOfScreenTargetsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.arrow.gameObject:SetActive(false)
    if self.m_isGPUMode then
        self.view.outOfScreenTargetsCtrlV2:OnCreate()
        self.m_isGPUModeInited = true
    else
        self.view.outOfScreenTargetsCtrl:OnCreate()
        self.m_isCPUModeInited = true
    end
end

OutOfScreenTargetsCtrl.OnClose = HL.Override() << function(self)
    if self.m_isGPUModeInited then
        if not self.m_isGPUMode then
            self.view.outOfScreenTargetsCtrlV2:DebugChangeImpl(false)
        end
        self.view.outOfScreenTargetsCtrlV2:OnClose()
    end

    if self.m_isCPUModeInited then
        if self.m_isGPUMode then
            self.view.outOfScreenTargetsCtrl:DebugChangeImpl(false)
        end
        self.view.outOfScreenTargetsCtrl:OnClose()
    end
end

OutOfScreenTargetsCtrl.OnShow = HL.Override() << function(self)
    if self.m_isGPUMode then
        self.view.outOfScreenTargetsCtrlV2:OnShow()
    else
        self.view.outOfScreenTargetsCtrl:OnShow()
    end
end

OutOfScreenTargetsCtrl.OnHide = HL.Override() << function(self)
    if self.m_isGPUMode then
        self.view.outOfScreenTargetsCtrlV2:OnHide()
    end
end

OutOfScreenTargetsCtrl.OnToggleDebugOutOfScreenTargetsMode = HL.Method() << function(self)
    self.m_isGPUMode = not self.m_isGPUMode
    GameAction.ShowUIToast("已切换到" .. (self.m_isGPUMode and "GPU模式" or "CPU模式"))
    if self.m_isGPUModeInited == false and self.m_isGPUMode then
        self.m_isGPUModeInited = true
        self.view.outOfScreenTargetsCtrlV2:OnCreate()
        self.view.outOfScreenTargetsCtrlV2:OnShow()
        if self.m_isCPUModeInited then
            self.view.outOfScreenTargetsCtrl:DebugChangeImpl(true)
        end
        return
    end

    if self.m_isCPUModeInited == false and not self.m_isGPUMode then
        self.m_isCPUModeInited = true
        self.view.outOfScreenTargetsCtrl:OnCreate()
        self.view.outOfScreenTargetsCtrl:OnShow()
        if self.m_isGPUModeInited then
            self.view.outOfScreenTargetsCtrlV2:DebugChangeImpl(true)
        end
        return
    end

    if self.m_isGPUModeInited then
        self.view.outOfScreenTargetsCtrlV2:DebugChangeImpl(not self.m_isGPUMode)
    end
    if self.m_isCPUModeInited then
        self.view.outOfScreenTargetsCtrl:DebugChangeImpl(self.m_isGPUMode)
    end
end

OutOfScreenTargetsCtrl._Update = HL.Method() << function(self)
    self:_UpdateOutOfScreenTargets()
end

do
    OutOfScreenTargetsCtrl._UpdateOutOfScreenTargets = HL.Method() << function(self)
        local hostileEnemies = GameWorld.battle.enemies
        local targetScrPoses = {}
        for _, v in cs_pairs(hostileEnemies) do
            local enemy = v
            if enemy ~= nil then
                local screenPos = CameraManager.mainCamera:WorldToScreenPoint(enemy.position)
                if not self:_IsPosInScreen(screenPos, Screen.width, Screen.height) then
                    table.insert(targetScrPoses, screenPos)
                end
            end
        end
        if #self.m_arrows > #targetScrPoses then
            for i = #self.m_arrows, #targetScrPoses+1, -1 do
                local arrow = self.m_arrows[i]
                table.remove(self.m_arrows, i)
                table.insert(self.m_arrowsWithOutAnimation, arrow)
                arrow.obj:GetComponent("UIAnimationWrapper"):PlayOutAnimation(function()
                    arrow.obj:SetActive(false)
                    for k, v in pairs(self.m_arrowsWithOutAnimation) do
                        if v == arrow then
                            table.remove(self.m_arrowsWithOutAnimation, k)
                            break
                        end
                    end
                    table.insert(self.m_arrowsCache, arrow)
                end)
            end
        end
        if #self.m_arrows < #targetScrPoses then
            for i = #self.m_arrows+1, #targetScrPoses do
                table.insert(self.m_arrows, self:_CreateOneArrow())
            end
        end
        for i = 1, #targetScrPoses do
            local arrow = self.m_arrows[i]
            if  arrow and arrow.rect then
                local uiPos, angle = self:_ScreenPosToUIPos(targetScrPoses[i])
                arrow.rect.anchoredPosition = uiPos
                arrow.rect.localRotation = Quaternion.Euler(0, 0, angle)
            end
        end
    end

    OutOfScreenTargetsCtrl._IsPosInScreen = HL.Method(HL.Userdata, HL.Number, HL.Number).Return(HL.Boolean) << function(self, screenPos, screenWidth, screenHeight)
        return screenPos.x >= 0 and screenPos.x <= screenWidth and screenPos.y >= 0 and screenPos.y <= screenHeight and screenPos.z >= 0
    end

    OutOfScreenTargetsCtrl._CreateOneArrow = HL.Method().Return(HL.Table) << function(self)
        local cacheCount = #self.m_arrowsCache
        if cacheCount > 0 then
            local cacheObj = self.m_arrowsCache[cacheCount]
            cacheObj.obj:SetActive(true)
            table.remove(self.m_arrowsCache, cacheCount)
            return cacheObj
        end
        local obj = CSUtils.CreateObject(self.view.arrow.gameObject, self.view.main.transform)
        obj:SetActive(true)
        local arrow = {}
        arrow.obj = obj
        arrow.rect = obj:GetComponent("RectTransform")
        return arrow
    end

    OutOfScreenTargetsCtrl._ScreenPosToUIPos = HL.Method(HL.Userdata).Return(HL.Userdata, HL.Number) << function(self, screenPos)
        local needRevert = screenPos.z < 0
        local x = screenPos.x - Screen.width * 0.5
        local y = screenPos.y - Screen.height * 0.5
        if needRevert then
            x = -x
            y = -y
        end
        local angle = math.atan(y, x)
        local uiPos = Vector2(self.view.config.ELLIPSE_X_RADIUS * math.cos(angle), self.view.config.ELLIPSE_Y_RADIUS * math.sin(angle))
        return uiPos, math.deg(angle)
    end

end

HL.Commit(OutOfScreenTargetsCtrl)
