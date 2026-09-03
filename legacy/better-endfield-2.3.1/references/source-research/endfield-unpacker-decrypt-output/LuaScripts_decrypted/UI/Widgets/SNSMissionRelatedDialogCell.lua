local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSMissionRelatedDialogCell = HL.Class('SNSMissionRelatedDialogCell', UIWidgetBase)

SNSMissionRelatedDialogCell.m_dialogId = HL.Field(HL.String) << ""

SNSMissionRelatedDialogCell.m_onClickDialogCellFunc = HL.Field(HL.Function)


SNSMissionRelatedDialogCell._OnFirstTimeInit = HL.Override() << function(self)

    self:RegisterMessage(MessageConst.ON_SNS_DIALOG_MODIFY, function(args)
        local _, dialogId = unpack(args)
        self:_OnSNSDialogModify(dialogId)
    end)

    self.view.btnClick.onClick:AddListener(function()
        self:_OnClickDialogCell()
    end)
end

SNSMissionRelatedDialogCell.InitSNSMissionRelatedDialogCell = HL.Method(HL.String, HL.Function)
        << function(self, dialogId, onClickFunc)
    self:_FirstTimeInit()

    self.m_dialogId = dialogId
    self.m_onClickDialogCellFunc = onClickFunc

    self:_InitInfo()
    self:_RefreshDialogInfo()
    self:_RefreshDialogInfoEndState()

    self.view.redDot:InitRedDot("SNSMissionDialogCell", dialogId)
end

SNSMissionRelatedDialogCell.SetSelected = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.bgNode:SetState(isOn and "Selected" or "Normal")
end

SNSMissionRelatedDialogCell._InitInfo = HL.Method() << function(self)
    local dialogCfg = Tables.sNSDialogTable[self.m_dialogId]
    local chatId = dialogCfg.chatId
    local chatCfg = Tables.sNSChatTable[chatId]

    self.view.headIcon:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, chatCfg.icon)

    local missionId = dialogCfg.relatedMissionId
    local meta = GameInstance.player.mission:GetMissionMetaAsset(missionId)
    local icon = UIConst.MISSION_VIEW_TYPE_CONFIG[meta.viewType].missionIcon
    self.view.missionIconN:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, icon)
    self.view.missionIconS:LoadSprite(UIConst.UI_SPRITE_MISSION_TYPE_ICON, icon)

    local importanceStr = meta.missionImportance:ToString()
    self.view.bgNode:SetState(importanceStr)
end

SNSMissionRelatedDialogCell._OnSNSDialogModify = HL.Method(HL.String) << function(self, dialogId)
    if dialogId ~= self.m_dialogId then
        return
    end


    self:_RefreshDialogInfoEndState()
end

SNSMissionRelatedDialogCell._RefreshDialogInfo = HL.Method() << function(self)
    local latestContent = SNSUtils.getFirstContent(self.m_dialogId)
    local richStyleContent = SNSUtils.resolveTextStyleWithPlayerName(latestContent)
    self.view.descTxtN:SetAndResolveTextStyle(richStyleContent)
    self.view.descTxtS:SetAndResolveTextStyle(richStyleContent)
end

SNSMissionRelatedDialogCell._RefreshDialogInfoEndState = HL.Method() << function(self)
    local isEnd = GameInstance.player.sns:DialogHasEnd(self.m_dialogId)
    self.view.canvasGroup.alpha = isEnd and 0.6 or 1
end

SNSMissionRelatedDialogCell._OnClickDialogCell = HL.Method() << function(self)
    if self.m_onClickDialogCellFunc then
        local chatId = Tables.sNSDialogTable[self.m_dialogId].chatId
        self.m_onClickDialogCellFunc(chatId, self.m_dialogId)
    end
end

HL.Commit(SNSMissionRelatedDialogCell)
return SNSMissionRelatedDialogCell

