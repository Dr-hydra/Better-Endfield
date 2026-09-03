local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local Index2Radio = {
    [-1] = -1,
    [1] = 1,
    [2] = 2,
}

CustomRewardRadioComp = HL.Class('CustomRewardRadioComp', UIWidgetBase)

CustomRewardRadioComp.m_costStamina = HL.Field(HL.Number) << -1

CustomRewardRadioComp.m_onRadioChangedFunc = HL.Field(HL.Function)

CustomRewardRadioComp.m_curSelectRadioIndex = HL.Field(HL.Number) << -1

CustomRewardRadioComp.m_isStaminaActivityOn = HL.Field(HL.Boolean) << false


CustomRewardRadioComp._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_ITEM_COUNT_CHANGED, function(args)
        self:_OnItemCountChanged(args)
    end)

    self.view.radioPartOneBtn.onClick:AddListener(function()
        self:_OnClickPartOneBtn()
    end)

    self.view.radioPartTwoBtn.onClick:AddListener(function()
        self:_OnClickPartTwoBtn()
    end)
end

CustomRewardRadioComp.InitCustomRewardRadioComp = HL.Method(HL.Number, HL.Function)
        << function(self, costStamina, onRadioChangedFunc)
    self:_FirstTimeInit()

    self.m_costStamina = costStamina
    self.m_onRadioChangedFunc = onRadioChangedFunc

    self:_InitData()
    self:_RefreshView()
end

CustomRewardRadioComp._InitData = HL.Method() << function(self)

    local activityInfo = ActivityUtils.getStaminaReduceInfo()
    self.m_isStaminaActivityOn = activityInfo.activityUsable

    local bpDoubleCount = Utils.getItemCount(Tables.dungeonConst.doubleStaminaTicketItemId)
    local staminaValue = bpDoubleCount * Tables.dungeonConst.staminaPerDoubleStaminaTicket
    local doubleStaminaTickedEnough = staminaValue >= self.m_costStamina

    self.m_curSelectRadioIndex = doubleStaminaTickedEnough and -1 or 1
end

CustomRewardRadioComp._RefreshView = HL.Method() << function(self)
    self.view.stateController:SetState(self.m_isStaminaActivityOn and "ActivityOn" or "ActivityOff")
    if not self.m_isStaminaActivityOn then
        self.view.radioPartOneTxt:SetAndResolveTextStyle(string.format(Language.LUA_RADIO_ONE_DESC, self.m_costStamina))
        self.view.radioPartTwoTxt:SetAndResolveTextStyle(string.format(Language.LUA_RADIO_TWO_DESC, self.m_costStamina * 2))

        local bpDoubleCount = Utils.getItemCount(Tables.dungeonConst.doubleStaminaTicketItemId)
        local staminaValue = bpDoubleCount * Tables.dungeonConst.staminaPerDoubleStaminaTicket
        local doubleStaminaTickedEnough = staminaValue >= self.m_costStamina
        local consume = math.ceil(self.m_costStamina / Tables.dungeonConst.staminaPerDoubleStaminaTicket)
        self.view.radioPartTwoNumTxt.text = UIUtils.setCountColorByCustomColor(consume, not doubleStaminaTickedEnough, self.config.ITEM_LACK_COLOR_STR)

        self.view.radioPartTwoNode:SetState(doubleStaminaTickedEnough and "Possess" or "NotPossess")
        self.view.radioPartTwoBtn.gameObject:SetActive(doubleStaminaTickedEnough)

        self.view.radioPartOneNode:SetState(self.m_curSelectRadioIndex == 1 and "Select" or "Unselect")
        self.view.radioPartTwoNode:SetState(self.m_curSelectRadioIndex == 2 and "Select" or "Unselect")
    end
    self.m_onRadioChangedFunc(self.m_isStaminaActivityOn and 1 or Index2Radio[self.m_curSelectRadioIndex])
end

CustomRewardRadioComp._OnClickPartOneBtn = HL.Method() << function(self)

    if self.m_curSelectRadioIndex == 1 then
        return
    end

    if self.m_curSelectRadioIndex == 1 then

        self.view.radioPartOneNode:SetState("Unselect")
        self.m_curSelectRadioIndex = -1
    else
        self.view.radioPartOneNode:SetState("Select")
        self.view.radioPartTwoNode:SetState("Unselect")
        self.m_curSelectRadioIndex = 1
    end
    self.m_onRadioChangedFunc(Index2Radio[self.m_curSelectRadioIndex])
end

CustomRewardRadioComp._OnClickPartTwoBtn = HL.Method() << function(self)

    if self.m_curSelectRadioIndex == 2 then
        return
    end

    if self.m_curSelectRadioIndex == 2 then

        self.view.radioPartTwoNode:SetState("Unselect")
        self.m_curSelectRadioIndex = -1
    else
        self.view.radioPartOneNode:SetState("Unselect")
        self.view.radioPartTwoNode:SetState("Select")
        self.m_curSelectRadioIndex = 2
    end
    self.m_onRadioChangedFunc(Index2Radio[self.m_curSelectRadioIndex])
end

CustomRewardRadioComp._OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    local changedItemId2DiffCount = unpack(args)
    local itemId = Tables.dungeonConst.doubleStaminaTicketItemId
    if not changedItemId2DiffCount:ContainsKey(itemId) then
        return
    end

    self:_RefreshView()
end

CustomRewardRadioComp.SetDefaultTarget = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:SetNaviTarget(self.view.radioPartOneBtn)
end

HL.Commit(CustomRewardRadioComp)
return CustomRewardRadioComp

