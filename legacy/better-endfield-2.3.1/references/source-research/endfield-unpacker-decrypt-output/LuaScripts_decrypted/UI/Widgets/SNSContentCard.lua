local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentCard = HL.Class('SNSContentCard', SNSContentBase)

SNSContentCard._OnSNSContentInit = HL.Override() << function(self)
    local chatId = self.m_contentCfg.contentParam[0]
    local dialogId = self.m_contentCfg.contentParam[1]

    local chatTableData = Tables.sNSChatTable[chatId]

    self.view.headIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, chatTableData.icon)
    self.view.nameTxt.text = chatTableData.name


    local organization
    if organization ~= nil and organization ~= "" then
        self.view.organization.gameObject:SetActiveIfNecessary(true)
        self.view.noOrganization.gameObject:SetActiveIfNecessary(false)

        self.view.organizationTxt.text = organization
    else
        self.view.organization.gameObject:SetActiveIfNecessary(false)
        self.view.noOrganization.gameObject:SetActiveIfNecessary(true)
    end

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.SNS, {chatId = chatId, dialogId = dialogId})
    end)
end



SNSContentCard.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentCard.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.button
end



HL.Commit(SNSContentCard)
return SNSContentCard