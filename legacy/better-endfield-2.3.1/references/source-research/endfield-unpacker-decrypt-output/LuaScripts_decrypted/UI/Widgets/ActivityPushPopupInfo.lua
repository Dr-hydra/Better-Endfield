local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityPushPopupInfo = HL.Class('ActivityPushPopupInfo', UIWidgetBase)

ActivityPushPopupInfo.m_pushId = HL.Field(HL.String) << ""
ActivityPushPopupInfo.m_activityId = HL.Field(HL.String) << ""
ActivityPushPopupInfo.m_pushCfg = HL.Field(HL.Any)
ActivityPushPopupInfo.m_rewardCellCache = HL.Field(HL.Any)


ActivityPushPopupInfo._OnFirstTimeInit = HL.Override() << function(self)
end

ActivityPushPopupInfo.InitActivityPushPopupInfo = HL.Method(HL.Any) << function(self, arg)
    self:_FirstTimeInit()
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs()
end

ActivityPushPopupInfo._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_pushId = arg.pushId
    local _, pushCfg = Tables.activityPushPopupTable:TryGetValue(self.m_pushId)
    self.m_pushCfg = pushCfg
    self.m_activityId = pushCfg.activityId
end

ActivityPushPopupInfo._InitUI = HL.Method() << function(self)

    if not string.isEmpty(self.m_pushCfg.bgFullImg) then
        self.view.bgFullImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY_PUSH_BG, self.m_pushCfg.bgFullImg)
    end
    if not string.isEmpty(self.m_pushCfg.bgImg) then
        local bgImgPath = self.m_pushCfg.bgImg

        if self.m_pushCfg.showGender then
            local gender = GameInstance.player.playerInfoSystem.gender
            if gender == CS.Proto.GENDER.GenMale then
                bgImgPath = bgImgPath .. "_male"
            elseif gender == CS.Proto.GENDER.GenFemale then
                bgImgPath = bgImgPath .. "_female"
            end
        end
        self.view.bgImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY_PUSH_BG, bgImgPath)
    end


    local infoNode = self.view.infoNode
    infoNode.txtName:SetAndResolveTextStyle(self.m_pushCfg.title)
    infoNode.detailsTxt:SetAndResolveTextStyle(self.m_pushCfg.desc)
    local suc, color = CS.UnityEngine.ColorUtility.TryParseHtmlString(self.m_pushCfg.decoColor)
    if suc then
        infoNode.decoImg.color = color
    end
    infoNode.activityTurnNode:SetState(self.m_pushCfg.style)


    local gotoNode = self.view.gotoNode
    local state
    if self.m_pushCfg.isPreview then
        state = "Preview"
        local _ , activityCfg = Tables.activityTable:TryGetValue(self.m_activityId)
        local activityStartTime = Utils.getTimeIdOpenTimeStamp(activityCfg.timeId)
        gotoNode.countDownText:InitCountDownText(activityStartTime, nil, function(leftSec)
            local leftTime = UIUtils.getLeftTime(leftSec)
            return string.format(Language.LUA_ACTIVITY_RECOMMEND_POPUP_START_COUNTDOWN_TEXT, leftTime)
        end)
    else
        state = "Detail"
        gotoNode.btnDetail.onClick:AddListener(function()
            if not GameInstance.player.activitySystem:GetActivity(self.m_activityId) then
                logger.error("推送拍脸{0}对应活动页签还未可见，因此无法跳转；请策划检查推送时间配置或更新服务器",self.m_activityId)
                UIManager:Close(PanelId.ActivityPushPopup)
            end
            ActivityUtils.GameEventLogActivityPushPopupClick(self.m_activityId, "visit_goto")
            PhaseManager:GoToPhase(PhaseId.ActivityCenter, {
                activityId = self.m_activityId,
                gotoCenter = true,
            })
            local _, ctrl = UIManager:IsOpen(PanelId.ActivityPushPopup)
            if ctrl then
                ctrl:PlayAnimationOutAndClose()
            end
        end)
    end
    gotoNode.stateController:SetState(state)


    self.m_rewardCellCache = UIUtils.genCellCache(gotoNode.rewardItem)
end

ActivityPushPopupInfo._RefreshAllUIs = HL.Method() << function(self)
    local rewardId = self.m_pushCfg.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    self.m_rewardCellCache:Refresh(#rewardBundles, function(cell, luaIndex)
        local reward = {
            id = rewardBundles[luaIndex].id,
            count = rewardBundles[luaIndex].count,
            forceHidePotentialStar = true,
        }
        cell:InitItem(reward, function()
            cell:ShowTips()
            ActivityUtils.GameEventLogActivityPushPopupClick(self.m_activityId, "visit_reward")
        end)
        cell:SetExtraInfo({
            tipsPosTransform = cell.view.content,
            isSideTips = true,
        })

        local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
        local isVisible = rewardTableData and rewardTableData.itemBundleVisibleList and rewardTableData.itemBundleVisibleList[CSIndex(luaIndex)] or 0
        if isVisible == 1 then
            cell.view.countNode.gameObject:SetActive(true)
        else
            cell.view.countNode.gameObject:SetActive(false)
        end
    end)
end

HL.Commit(ActivityPushPopupInfo)
return ActivityPushPopupInfo


