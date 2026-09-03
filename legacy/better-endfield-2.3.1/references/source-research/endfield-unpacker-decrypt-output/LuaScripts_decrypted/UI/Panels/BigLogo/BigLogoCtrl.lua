
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BigLogo

BigLogoCtrl = HL.Class('BigLogoCtrl', uiCtrl.UICtrl)






BigLogoCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SHOW_BIG_LOGO] = '_OnShowBigLogo',
    [MessageConst.ON_LOAD_NEW_CUTSCENE] = '_OnLoadNewCutscene',
    [MessageConst.ON_LOAD_NEW_DLG_TIMELINE] = '_OnLoadNewDialogTimeline',
}

BigLogoCtrl.m_timelineHandle = HL.Field(HL.Userdata)

BigLogoCtrl.m_onCanvasChangedClosure = HL.Field(HL.Function)



BigLogoCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_timelineHandle = unpack(args)
end

BigLogoCtrl.OnClose = HL.Override() << function(self)
    if self.m_onCanvasChangedClosure then
        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:RemoveListener(self.m_onCanvasChangedClosure)
        self.m_onCanvasChangedClosure = nil
    end
end




BigLogoCtrl._OnShowBigLogo = HL.Method(HL.Table) << function(self, args)
    local playParam = unpack(args)
    local sprite = playParam.sprite
    local useStretchImage = playParam.useStretchImage
    local showOnTop = playParam.showOnTop
    local hideBackground = playParam.hideBackground
    local useOriginalImage = playParam.useOriginalImage
    local useFitImage = playParam.useFitImage

    self.view.bg.gameObject:SetActive(not hideBackground)

    if useOriginalImage then
        self.view.originImageNode.sprite = sprite
    elseif useStretchImage then
        local imageAspectRatio = sprite.rect.width / sprite.rect.height
        if showOnTop then
            if math.abs(self.view.stretchImageTopAspectRatioFitter.aspectRatio - imageAspectRatio) > 0.01 then
                self.view.stretchImageTopAspectRatioFitter.aspectRatio = imageAspectRatio
            end
            self.view.stretchImageTop.sprite = sprite
        else
            if math.abs(self.view.stretchImageBottomAspectRatioFitter.aspectRatio - imageAspectRatio) > 0.01 then
                self.view.stretchImageBottomAspectRatioFitter.aspectRatio = imageAspectRatio
            end
            self.view.stretchImageBottom.sprite = sprite
        end
    elseif useFitImage then
        if showOnTop then
            self.view.fitImageTop.sprite = sprite
        else
            self.view.fitImageBottom.sprite = sprite
        end

        self:_RefreshFitImageSize()

        self.view.fitImageTop.transform.offsetMin = Vector2.zero
        self.view.fitImageTop.transform.offsetMax = Vector2.zero
        self.view.fitImageBottom.transform.offsetMin = Vector2.zero
        self.view.fitImageBottom.transform.offsetMax = Vector2.zero

        if self.m_onCanvasChangedClosure == nil then
            self.m_onCanvasChangedClosure = function() self:_RefreshFitImageSize() end
            UIManager.m_uiCanvasScaleHelper.onCanvasChanged:AddListener(self.m_onCanvasChangedClosure)
        end
    else
        self.view.nameImg.sprite = sprite
    end
end

BigLogoCtrl._RefreshFitImageSize = HL.Method() << function(self)
    local sprite
    if NotNull(self.view.fitImageTop.sprite) then
        sprite = self.view.fitImageTop.sprite
    end

    if NotNull(self.view.fitImageBottom.sprite) then
        sprite = self.view.fitImageBottom.sprite
    end

    if sprite == nil then
        return
    end

    local screenWidth = self.view.fitImageMain.transform.rect.width
    local screenHeight = self.view.fitImageMain.transform.rect.height
    local w = sprite.rect.width
    local h = sprite.rect.height

    local offsetMin, offsetMax = NarrativeUtils.GetFitImageOffset(screenWidth, screenHeight, w, h)
    self.view.fitNodeTop.transform.offsetMin = offsetMin
    self.view.fitNodeTop.transform.offsetMax = offsetMax
    self.view.fitNodeBottom.transform.offsetMin = offsetMin
    self.view.fitNodeBottom.transform.offsetMax = offsetMax
end

BigLogoCtrl._OnLoadNewCutscene = HL.Method(HL.Any) << function(self, args)
    self.view.bigLogoMain.gameObject:SetActive(false)
    self.view.stretchImageMain.gameObject:SetActive(false)

    local cinematicMgr = GameWorld.cutsceneManager
    cinematicMgr:BindBigLogo(self.m_timelineHandle, self.view.bigLogoPanel)
end

BigLogoCtrl._OnLoadNewDialogTimeline = HL.Method(HL.Any) << function(self, args)
    self.view.bigLogoMain.gameObject:SetActive(false)
    self.view.stretchImageMain.gameObject:SetActive(false)

    local dialogTimelineManager = GameWorld.dialogTimelineManager
    dialogTimelineManager:BindBigLogo(self.m_timelineHandle, self.view.bigLogoPanel)
end



HL.Commit(BigLogoCtrl)