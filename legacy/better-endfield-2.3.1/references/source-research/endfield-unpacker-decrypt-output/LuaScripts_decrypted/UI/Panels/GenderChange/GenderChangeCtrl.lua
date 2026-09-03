
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GenderChange
local GENDER_CHANGE_TIME_LIMIT_SECONDS <const> = 24 * 3600
local UI_TEXT_CANNOT_CHANGE <const> = "ui_common_character_gender_cannot_change"
local UI_TEXT_GENDER_CHANGE_TIME <const> = "ui_common_character_gender_change_time"
local UI_TEXT_GENDER_CHANGE_TIPS <const> = "ui_common_character_gender_change_confirm"

GenderChangeCtrl = HL.Class('GenderChangeCtrl', uiCtrl.UICtrl)

GenderChangeCtrl.m_lastGenderSetTime = HL.Field(HL.Int) << 0

GenderChangeCtrl.m_selectCallback = HL.Field(HL.Any)





GenderChangeCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GenderChangeCtrl.OnGenderChangeStart = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhase(PhaseId.GenderChange, arg)
end


GenderChangeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_lastGenderSetTime = Utils.getClientVar("set_gender_time_stamp", 0)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local targetGender = CS.Proto.GENDER.GenFemale
    if Utils.getPlayerGender() == CS.Proto.GENDER.GenFemale then

        local swapSprite = self.view.leftIcon.sprite
        self.view.leftIcon.sprite = self.view.rightIcon.sprite
        self.view.rightIcon.sprite = swapSprite
        targetGender = CS.Proto.GENDER.GenMale
    end

    if curTime - self.m_lastGenderSetTime <= GENDER_CHANGE_TIME_LIMIT_SECONDS then
        local coolDownSeconds = GENDER_CHANGE_TIME_LIMIT_SECONDS - (curTime - self.m_lastGenderSetTime)
        coolDownSeconds = math.max(coolDownSeconds, 1)
        local leftTimeString = UIUtils.getLeftTime(coolDownSeconds)

        self.view.subText.gameObject:SetActive(false)
        self.view.confirmButton.gameObject:SetActive(false)

        self.view.warningNode.gameObject:SetActive(true)
        self.view.warningText.text = string.format(I18nUtils.GetText(UI_TEXT_GENDER_CHANGE_TIME), leftTimeString)

        self.view.unavailableButton.text = I18nUtils.GetText(UI_TEXT_CANNOT_CHANGE)
    else
        self.view.unavailableButton.gameObject:SetActive(false)
        self.view.warningNode.gameObject:SetActive(false)

        self.view.subText.gameObject:SetActive(true)
    end

    self.view.contentText.text = UIUtils.resolveTextGender(I18nUtils.GetText(UI_TEXT_GENDER_CHANGE_TIPS))


    self.view.unavailableKeyHint.overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid

    arg = arg or {}
    self.m_selectCallback = unpack(arg)
    self.view.cancelButton.onClick:AddListener(function() self:_OnBtnCloseClick() end)
    self.view.confirmButton.onClick:AddListener(function() self:_OnBtnConfirmClick() end)
end

GenderChangeCtrl._OnBtnCloseClick = HL.Method() << function(self)
    if self.m_selectCallback then
        self.m_selectCallback(false)
        self.m_selectCallback = nil
    end
end

GenderChangeCtrl._OnBtnConfirmClick = HL.Method() << function(self)
    if self.m_selectCallback then
        self.m_selectCallback(true)
        self.m_selectCallback = nil
    end
end

GenderChangeCtrl.OnShow = HL.Override() << function(self)

end

GenderChangeCtrl.OnClose = HL.Override() << function(self)

end

HL.Commit(GenderChangeCtrl)
