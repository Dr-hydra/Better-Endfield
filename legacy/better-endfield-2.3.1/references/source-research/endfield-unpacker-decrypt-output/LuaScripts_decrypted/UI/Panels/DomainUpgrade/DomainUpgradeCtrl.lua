local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainUpgrade

DomainUpgradeCtrl = HL.Class('DomainUpgradeCtrl', uiCtrl.UICtrl)

local MAIN_HUD_TOAST_TYPE = "DomainUpgrade"





DomainUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}



DomainUpgradeCtrl.m_levelUpInfoQueue = HL.Field(HL.Forward("Queue"))

DomainUpgradeCtrl.m_aniPlayInfo = HL.Field(HL.Table)

DomainUpgradeCtrl.m_domainInfo = HL.Field(HL.Table)

DomainUpgradeCtrl.m_showToastInfo = HL.Field(HL.Table)




DomainUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_levelUpInfoQueue = require_ex("Common/Utils/DataStructure/Queue")()
end

DomainUpgradeCtrl.OnClose = HL.Override() << function(self)
    if self.m_showToastInfo ~= nil then
        Notify(MessageConst.ON_SHOW_DOMAIN_TOAST, self.m_showToastInfo)
        self.m_showToastInfo = nil
    end
end

DomainUpgradeCtrl.ShowUpgrade = HL.StaticMethod(HL.Any) << function(arg)
    if LuaSystemManager.mainHudActionQueue:HasRequestWaiting(MAIN_HUD_TOAST_TYPE) then

        return
    end
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainDevelopment) then
        return
    end
    local domainId, preLv, preExp = unpack(arg)
    LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_TOAST_TYPE, function()
        local self = UIManager:AutoOpen(PANEL_ID)
        self:_StartShow(domainId, preLv, preExp)
    end)
end

DomainUpgradeCtrl._StartShow = HL.Method(HL.String, HL.Number, HL.Number) << function(self, domainId, preLv, preExp)
    self:_UpdateData(domainId, preLv, preExp)
    self.view.domainIconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_ICON_BIG, self.m_domainInfo.domainIcon)
    self:_TryPlayUpgrade()
end

DomainUpgradeCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self.animationWrapper:ClearTween(false)
    self:_ClearCache()
    self:Close()
end




DomainUpgradeCtrl._UpdateData = HL.Method(HL.String, HL.Number, HL.Number) << function(self, domainId, preLv, preExp)

    local _, domainData = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
    local totalTargetLv = domainData.lv
    local totalTargetExp = domainData.exp
    local _, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    local domainLvCfg = domainCfg.domainDevelopmentLevel[preLv - 1]
    local preLvMaxExp = domainLvCfg.levelUpExp

    self.m_domainInfo = {
        domainIcon = domainCfg.domainIcon,
        domainName = domainCfg.domainName,
        domainColor = UIUtils.getColorByString(domainCfg.domainColor),

        audKeyUpToastNotLevelUpEnhance = domainCfg.audKeyUpToastNotLevelUpEnhance,
        audKeyUpToastLevelUpPreEnhance = domainCfg.audKeyUpToastLevelUpPreEnhance,
        audKeyUpToastLevelUpMoment = domainCfg.audKeyUpToastLevelUpMoment,
        audKeyUpToastLevelUpAfterEnhance = domainCfg.audKeyUpToastLevelUpAfterEnhance,
    }

    local info = {
        curLv = preLv,
        targetLv = totalTargetLv,
        curExp = preExp,
        targetExp = totalTargetExp,
        maxExp = preLvMaxExp,
        totalAddExp = totalTargetExp - preExp,
    }
    if preLv == totalTargetLv then

        info.isLevelUp = false
        info.levelUpMode = false
        self.m_levelUpInfoQueue:Push(info)
        self.m_showToastInfo = nil
    else

        info.isLevelUp = true
        info.levelUpMode = true
        info.targetExp = info.maxExp
        self.m_levelUpInfoQueue:Push(info)
        self.m_levelUpInfoQueue:Push({
            curLv = totalTargetLv,
            targetLv = totalTargetLv,
            curExp = preLvMaxExp,
            targetExp = totalTargetExp,
            maxExp = domainData.curLevelData.levelUpExp,
            totalAddExp = -1,

            isLevelUp = false,
            levelUpMode = true,
        })

        self.m_showToastInfo = {
            domainId = domainId,
            preLv = preLv,
        }
    end
end

DomainUpgradeCtrl._ClearCache = HL.Method() << function(self)
    if self.m_aniPlayInfo and self.m_aniPlayInfo.tween then
        self.m_aniPlayInfo.tween:Kill()
    end

    self.m_aniPlayInfo = nil
    self.m_levelUpInfoQueue:Clear()
end




DomainUpgradeCtrl._StartPlayUpgradeAni = HL.Method() << function(self)
    local info = self.m_aniPlayInfo.basicInfo
    local upgradeNode = self.view.upgradeNode
    local maxExp = info.maxExp > 0 and info.maxExp or 1

    self.view.levelUpStateCtrl:SetState(info.levelUpMode and "LevelUp" or "NotLevelUp")
    self.view.curExpTxt.text = info.curExp
    self.view.curExpBar.value = info.curExp / maxExp
    self.view.targetExpTxt.text = info.maxExp > 0 and "/" .. info.maxExp or "/-"
    self.view.targetExpBar.value = info.targetExp / maxExp
    if info.isLevelUp then
        upgradeNode.descTxtStateCtrl:SetState("LevelUp")
        upgradeNode.descTxt.text = string.format(Language.LUA_DOMAIN_UPGRADE_DESC, self.m_domainInfo.domainName)
    else
        upgradeNode.descTxtStateCtrl:SetState("NotLevelUp")
    end

    upgradeNode.InitialStateCtrl:SetState("InitialState")
    upgradeNode.curLvTxt.text = info.curLv
    upgradeNode.targetLvTxt.text = info.targetLv
    upgradeNode.diffExpTxt.text = "+" .. info.totalAddExp
    upgradeNode.diffExpNode.gameObject:SetActive(info.totalAddExp > 0)

    local color = self.m_domainInfo.domainColor
    self.view.colorImage1.color = color
    self.view.colorImage2.color = color
    self.view.upgradeNode.arrowColorImage1.color = color
    self.view.upgradeNode.arrowColorImage2.color = color


    local inAniName
    if info.levelUpMode == false or info.isLevelUp == true then

        inAniName = "domainupgrade_up"
    else
        inAniName = "domainupgrade_levelup_become_up_next"
    end


    local audKey = info.isLevelUp and self.m_domainInfo.audKeyUpToastLevelUpPreEnhance or self.m_domainInfo.audKeyUpToastLevelUpAfterEnhance
    AudioAdapter.PostEvent(audKey)

    self.view.animationWrapper:Play(inAniName,
        function()
            self.m_aniPlayInfo.tween = DOTween.To(
                function()
                    return info.curExp
                end,
                function(value)
                    value = math.floor(value)
                    if not self.view or IsNull(self.view.curExpTxt) then
                        return
                    end
                    self.view.curExpTxt.text = value
                    self.view.curExpBar.value = value / maxExp
                end,
                info.targetExp,
                self.view.config.PROGRESS_INCREASE_ANI_DURATION
            )

            AudioAdapter.PostEvent(self.m_domainInfo.audKeyUpToastNotLevelUpEnhance)
            if info.isLevelUp then
                self.m_aniPlayInfo.tween:OnComplete(function()
                    AudioAdapter.PostEvent(self.m_domainInfo.audKeyUpToastLevelUpMoment)
                    self.view.animationWrapper:Play("domainupgrade_levelup", function()
                        self.view.animationWrapper:Play("domainupgrade_levelup_become_up_pre", function()
                            self:_TryPlayUpgrade()
                        end)
                    end)
                end)
            else
                self.m_aniPlayInfo.tween:OnComplete(function()
                    self:_TryPlayUpgrade()
                end)
            end
        end
    )
end




DomainUpgradeCtrl._TryPlayUpgrade = HL.Method() << function(self)
    if self.m_levelUpInfoQueue:Count() <= 0 then
        self:_CompleteCloseSelf()
        return
    end

    local lvUpInfo = self.m_levelUpInfoQueue:Pop()
    self.m_aniPlayInfo = {
        basicInfo = lvUpInfo,
        tween = nil,
    }

    self:_StartPlayUpgradeAni()
end

DomainUpgradeCtrl._CompleteCloseSelf = HL.Method() << function(self)
    self.view.animationWrapper:PlayOutAnimation(function()
        self:_ClearCache()
        self:Close()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
    end)
end


HL.Commit(DomainUpgradeCtrl)
