local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')


SNSContentPRTS = HL.Class('SNSContentPRTS', SNSContentBase)

SNSContentPRTS._OnSNSContentInit = HL.Override() << function(self)
    local contentParam = self.m_contentCfg.contentParams
    if string.isEmpty(contentParam) then
        logger.error("SNSContentPRTS._OnSNSContentInit fail", self.m_contentInfo.dialogId, self.m_contentInfo.contentId)
        return
    end

    local jumpArgs = Json.decode(contentParam)
    if jumpArgs.isFirstLvId then
        local succ, firstLvData = Tables.prtsFirstLv:TryGetValue(jumpArgs.id)
        self.view.titleText.text = succ and firstLvData.name or jumpArgs.id
        if not succ then
            logger.error(string.format("一级条目配置表中到找不到id:%s", jumpArgs.id))
        end
    else
        local succ, prtsTableData = Tables.prtsAllItem:TryGetValue(jumpArgs.id)
        self.view.titleText.text = succ and prtsTableData.name or jumpArgs.id
        if not succ then
            logger.error(string.format("叙事收集物中到找不到id:%s", jumpArgs.id))
        end
    end

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()



        if GameInstance.player.spaceship.isViewingFriend then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
            return
        end

        local prts = GameInstance.player.prts
        local unlock = jumpArgs.isFirstLvId and prts:IsFirstLvUnlock(jumpArgs.id) or prts:IsPrtsUnlocked(jumpArgs.id)
        if unlock then
            PhaseManager:GoToPhase(PhaseId[jumpArgs.phaseId], jumpArgs)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNS_DONT_HAVE_PRTS_DATA)
        end
    end)
end



SNSContentPRTS.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentPRTS.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.button
end



HL.Commit(SNSContentPRTS)
return SNSContentPRTS

