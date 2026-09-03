local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Radio
RadioCtrl = HL.Class('RadioCtrl', uiCtrl.UICtrl)







RadioCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = 'OnToggleFacTopView',
}

RadioCtrl.m_spriteName = HL.Field(HL.String) << ""

RadioCtrl.m_needHide = HL.Field(HL.Boolean) << false

RadioCtrl.m_isHiding = HL.Field(HL.Boolean) << false

RadioCtrl.m_curShow = HL.Field(HL.Any)

RadioCtrl.m_bottomMidOriY = HL.Field(HL.Number) << -1

RadioCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)
    self.view.bottomMidMain.transform:DOKill()
    local targetY
    if active then
        targetY = self.m_bottomMidOriY + self.view.config.BOTTOM_MID_UP_LENGTH
    else
        targetY = self.m_bottomMidOriY
    end

    if self:IsShow() then
        self.view.bottomMidMain.transform:DOLocalMoveY(targetY, self.view.config.BOTTOM_MID_UP_DURATION):SetEase(CS.DG.Tweening.Ease.InOutSine);
    else
        local pos = self.view.bottomMidMain.transform.localPosition
        pos.y = targetY
        self.view.bottomMidMain.transform.localPosition = pos
    end


end

RadioCtrl.ShowRadioUI = HL.Method(HL.Any, HL.Userdata, HL.Number).Return(HL.Number) << function(self, curShowData, radioSingleData, index)
    self.m_curShow = curShowData
    local actorName = radioSingleData.actorName
    local iconSuffix = radioSingleData.iconSuffix
    local radioText = UIUtils.resolveTextCinematic(radioSingleData.radioText)
    actorName = UIUtils.resolveTextCinematic(actorName)

    local noActor = string.isEmpty(actorName)

    self.view.textTalkCenterNode.gameObject:SetActive(noActor)
    self.view.bottomMid.gameObject:SetActive(not noActor)

    local num
    if noActor then
        self.view.textTalkCenter:SetText(radioText)
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.textTalkCenter.transform)
        self.view.textTalk:RefreshAutoScrollData()
        self.view.textTalkCenter:Play()
        num = self.view.textTalkCenter.totalCharacterNum
    else
        self.view.textName:SetAndResolveTextStyle(UIUtils.removePattern(actorName, UIConst.NARRATIVE_ANONYMITY_PATTERN))
        self.view.textTalk:SetText(radioText)
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.bottomMid.transform)
        self.view.textTalk:RefreshAutoScrollData()
        self.view.textTalk:Play()
        num = self.view.textTalk.totalCharacterNum
    end

    local spriteName = ""
    if not string.isEmpty(iconSuffix) then
        self.view.charImage.gameObject:SetActive(true)
        self.view.charBlueMask.gameObject:SetActive(true)
        spriteName = UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. iconSuffix
        self.view.charImage:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, spriteName)
        self.view.charBlueMask:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, spriteName)
        self.m_curShow.icon = iconSuffix
    elseif not self.m_curShow.icon then
        self.view.charImage.gameObject:SetActive(false)
        self.view.charBlueMask.gameObject:SetActive(false)
    end

    if not string.isEmpty(spriteName) then
        self.m_curShow.spriteName = spriteName
        self.view.infoNode.gameObject:SetActive(true)
        if self.m_spriteName ~= spriteName or index == 1 then

            self.view.infoNode:PlayInAnimation()
        end
    else
        self.view.infoNode.gameObject:SetActive(false)
    end

    self.m_spriteName = spriteName
    return num
end

RadioCtrl.OnShow = HL.Override() << function(self)
    if self.m_bottomMidOriY < 0 then
        self.m_bottomMidOriY = self.view.bottomMidMain.transform.localPosition.y
    end
end

RadioCtrl.OnHide = HL.Override() << function(self)
    self.m_isHiding = false
end

RadioCtrl.TryPlayInfoNodeOut = HL.Method() << function(self)
    if self.view.infoNode.gameObject.activeSelf then
        self.view.infoNode:PlayOutAnimation()
    end
end

RadioCtrl.ShowSelf = HL.Method() << function(self)
    self.m_needHide = false
    self.m_isHiding = false
    if self:IsShow() then
        return
    end
    self:Show()
end

RadioCtrl.HideSelf = HL.Method(HL.Opt(HL.Boolean)) << function(self, useAnim)
    if self:IsHide() then
        return
    end

    if self.m_isHiding then
        return
    end

    if useAnim then
        self.m_needHide = true
        self.m_isHiding = true
        self:PlayAnimationOutWithCallback(function()
            if self.m_needHide then
                self:Hide()
            end
        end)
    else
        self:Hide()
    end
end

HL.Commit(RadioCtrl)
