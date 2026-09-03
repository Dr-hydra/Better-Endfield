local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSContentBase = HL.Class('SNSContentBase', UIWidgetBase)

SNSContentBase.m_contentInfo = HL.Field(HL.Table)

SNSContentBase.m_contentCfg = HL.Field(HL.Any)

SNSContentBase.m_loadingFinishCallBack = HL.Field(HL.Function)

SNSContentBase.m_notifyCellSizeChange = HL.Field(HL.Function)

SNSContentBase._OnSNSContentInit = HL.Virtual() << function(self)
end

SNSContentBase.InitSNSContentBase = HL.Method(HL.Table, HL.Opt(HL.Function, HL.Function))
        << function(self, contentInfo, loadingFinishCallBack, notifyCellSizeChange)
    self:_FirstTimeInit()

    self.m_contentInfo = contentInfo
    self.m_loadingFinishCallBack = loadingFinishCallBack
    self.m_notifyCellSizeChange = notifyCellSizeChange

    local dialogId = contentInfo.dialogId
    local contentCfg = Tables.sNSDialogTable[dialogId].dialogContentData[contentInfo.contentId]
    self.m_contentCfg = contentCfg

    local isSelf = contentCfg.speaker == Tables.sNSConst.myselfSpeaker
    local needEffect = not contentInfo.isLoaded

    if needEffect and self.view.animationWrapper then
        self.view.animationWrapper:PlayInAnimation()
    end

    if self.view.stateController then
        self.view.stateController:SetState(isSelf and "Self" or "Other")
    end

    self:_OnSNSContentInit()
end

SNSContentBase.HasEmojiComp = HL.Virtual().Return(HL.Boolean) << function(self)
    return false
end

SNSContentBase.IsTypeVote = HL.Virtual().Return(HL.Boolean) << function(self)
    return false
end



SNSContentBase.CanSetTarget = HL.Virtual().Return(HL.Boolean) << function(self)
    return false
end

SNSContentBase.GetNaviTarget = HL.Virtual().Return(HL.Any) << function(self)
    return nil
end




HL.Commit(SNSContentBase)
return SNSContentBase

