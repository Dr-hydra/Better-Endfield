local panelConfig = require_ex("UI/Panels/PanelConfig").config

UIWorldFreezeManager = HL.Class('UIWorldFreezeManager')


do

    UIWorldFreezeManager.m_timeScaleHandle = HL.Field(HL.Number) << -1

    UIWorldFreezeManager.m_activePanelCount = HL.Field(HL.Number) << 0


    UIWorldFreezeManager.m_pauseGameModePanelCount = HL.Field(HL.Number) << 0

    UIWorldFreezeManager.m_activePanels = HL.Field(HL.Table)

    UIWorldFreezeManager.m_pauseGameModePanels = HL.Field(HL.Table)
end


do
    UIWorldFreezeManager.UIWorldFreezeManager = HL.Constructor() << function(self)
        self.m_activePanels = {}
        self.m_pauseGameModePanels = {}
        self:_RegisterMessages()
    end

    UIWorldFreezeManager.IsUIWorldFreeze = HL.Method().Return(HL.Boolean) << function(self)
        return self.m_timeScaleHandle ~= -1
    end
end


do
    UIWorldFreezeManager._RegisterMessages = HL.Method() << function(self)
        Register(MessageConst.ON_BEFORE_UI_PANEL_OPEN, function(name)
            self:_OnPanelActivate(name)
        end)
        Register(MessageConst.ON_UI_PANEL_CLOSED, function(name)
            self:_OnPanelDeActivate(name)
        end)
        Register(MessageConst.ON_UI_PANEL_SHOW, function(name)
            self:_OnPanelActivate(name)
        end)
        Register(MessageConst.ON_UI_PANEL_HIDE, function(name)
            self:_OnPanelDeActivate(name)
        end)
    end

    UIWorldFreezeManager._OnPanelActivate = HL.Method(HL.String) << function(self, panelName)
        local panelCfg = panelConfig[panelName]
        if not panelCfg or not panelCfg.freezeWorld then
            return
        end

        if not self.m_activePanels[panelName] then
            self.m_activePanels[panelName] = true
            self.m_activePanelCount = self.m_activePanelCount + 1


            if self.m_activePanelCount == 1 then
                self:_FreezeWorld(true)
            end
        end

        if panelCfg.dontPauseGameMode then
            return
        end

        if not self.m_pauseGameModePanels[panelName] then
            self.m_pauseGameModePanels[panelName] = true
            self.m_pauseGameModePanelCount = self.m_pauseGameModePanelCount + 1


            if self.m_pauseGameModePanelCount == 1 then
                self:_PauseGameMode(true)
            end
        end
    end

    UIWorldFreezeManager._OnPanelDeActivate = HL.Method(HL.String) << function(self, panelName)
        if self.m_timeScaleHandle == -1 then
            return
        end

        local panelCfg = panelConfig[panelName]
        if not panelCfg or not panelCfg.freezeWorld then
            return
        end





        if self.m_activePanels[panelName] then
            self.m_activePanels[panelName] = nil
            self.m_activePanelCount = self.m_activePanelCount - 1
        end


        if self.m_activePanelCount == 0 then
            self:_FreezeWorld(false)
        end

        if panelCfg.dontPauseGameMode then
            return
        end

        if self.m_pauseGameModePanels[panelName] then
            self.m_pauseGameModePanels[panelName] = nil
            self.m_pauseGameModePanelCount = self.m_pauseGameModePanelCount - 1
        end


        if self.m_pauseGameModePanelCount == 0 then
            self:_PauseGameMode(false)
        end
    end

    UIWorldFreezeManager._FreezeWorld = HL.Method(HL.Boolean) << function(self, isFrozen)
        if isFrozen then
            self.m_timeScaleHandle = Utils.FreezeWorldByUI()
            AudioAdapter.PostEvent("au_global_contr_fullscreen_menu_pause")

            if UIManager.uiCamera.cullingMask == UIConst.LAYERS.Nothing then
                UIManager:OnToggleUiAction({ true })
                logger.important(CS.Beyond.EnableLogType.DevOnly,
                    "[UIWorldFreezeManager] freeze world when uiCamera culling mask is nothing")
            end
        else
            if self.m_timeScaleHandle ~= -1 then
                Utils.ResumeWorldByUI(self.m_timeScaleHandle)
                self.m_timeScaleHandle = -1
                AudioAdapter.PostEvent("au_global_contr_fullscreen_menu_resume")
            end
        end
    end

    UIWorldFreezeManager._PauseGameMode = HL.Method(HL.Boolean) << function(self, isPaused)
        if not GameWorld.isInited then
            return
        end
        if isPaused then
            GameWorld.subGameManager:TryPauseGameTime(GEnums.GameTimeFreezeReason.UI)
        else
            GameWorld.subGameManager:TryResumeGameTime(GEnums.GameTimeFreezeReason.UI)
        end
    end
end

HL.Commit(UIWorldFreezeManager)
return UIWorldFreezeManager
