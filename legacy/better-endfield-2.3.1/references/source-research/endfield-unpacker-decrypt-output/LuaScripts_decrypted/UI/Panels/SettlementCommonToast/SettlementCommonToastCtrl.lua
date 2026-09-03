local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementCommonToast
local settlementSystem = GameInstance.player.settlementSystem
SettlementCommonToastCtrl = HL.Class('SettlementCommonToastCtrl', uiCtrl.UICtrl)

local MAIN_HUD_TOAST_TYPE = "SettlementToast"

SettlementCommonToastCtrl.m_timerId = HL.Field(HL.Any) << nil






SettlementCommonToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = 'InterruptMainHudActionQueue',
}


SettlementCommonToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end






SettlementCommonToastCtrl._OnShowLink = HL.StaticMethod(HL.Table) << function(args)
    local inQueue = args[4]
    if inQueue then
        LuaSystemManager.mainHudActionQueue:AddRequest(MAIN_HUD_TOAST_TYPE, function()
            SettlementCommonToastCtrl._InternalOnShowLink(args, true)
        end)
    else
        SettlementCommonToastCtrl._InternalOnShowLink(args, false)
    end
end

SettlementCommonToastCtrl._InternalOnShowLink = HL.StaticMethod(HL.Table, HL.Boolean) << function(args, inQueue)
    local self = UIManager:AutoOpen(PANEL_ID)
    self.view.linkNode.gameObject:SetActiveIfNecessary(true)
    self.view.levelUpNode.gameObject:SetActiveIfNecessary(false)

    local settlementId, mainText, subText = args[1], args[2], args[3]
    if subText then
        subText = Language[subText]
        subText = subText or Language.LUA_SETTLEMENT_LINK_AND_UNLOCK
        self.view.unlockSubText.text = subText
    end

    if settlementId ~= nil then
        mainText = mainText or Tables.settlementBasicDataTable[settlementId].settlementName
    end
    self.view.unlockMainText.text = mainText
    self.view.interlinkageAniWrapper:Play("unlock_toast", function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if inQueue then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAIN_HUD_TOAST_TYPE)
            end
        end)
    end)
    AudioAdapter.PostEvent("Au_UI_Toast_SettlementCommonToastPanel_Unlock_Open")
end





SettlementCommonToastCtrl._OnShowUpgrade = HL.StaticMethod(HL.Table) << function(args)
    local self = UIManager:AutoOpen(PANEL_ID)
    self.view.linkNode.gameObject:SetActiveIfNecessary(false)
    self.view.levelUpNode.gameObject:SetActiveIfNecessary(true)

    local settlementId, mainText = unpack(args)
    if settlementId ~= nil then
        if not mainText then
            AudioAdapter.PostEvent("Au_UI_Toast_SettlementCommonToastPanel_LevelUp_Open")
        end
        mainText = mainText or Tables.settlementBasicDataTable[settlementId].settlementName
        local level = settlementSystem:GetSettlementLevel(settlementId)
        self.view.levelText.text = tostring(level)
        self.view.levelText2.text = tostring(level + 1)
        self.view.textOpenLv.gameObject:SetActive(false)
    end
    self.view.levelUpMainText.text = mainText
    self.view.levelUpAniWrapper:Play("level_up_toast", function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)
end

SettlementCommonToastCtrl.InterruptMainHudActionQueue = HL.Method() << function(self)
    self.animationWrapper:ClearTween(false)
    self:Close()
end

HL.Commit(SettlementCommonToastCtrl)
