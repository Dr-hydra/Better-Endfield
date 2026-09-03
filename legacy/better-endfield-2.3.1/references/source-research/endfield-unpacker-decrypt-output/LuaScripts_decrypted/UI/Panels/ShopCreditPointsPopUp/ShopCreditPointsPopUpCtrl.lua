
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopCreditPointsPopUp
local PHASE_ID = PhaseId.ShopCreditPointsPopUp

local CREDIT_TEXT_FORMAT = "%s<color=#A7A7A7>/%s</color>"
ShopCreditPointsPopUpCtrl = HL.Class('ShopCreditPointsPopUpCtrl', uiCtrl.UICtrl)

ShopCreditPointsPopUpCtrl.m_visitRecord = HL.Field(HL.Any)

ShopCreditPointsPopUpCtrl.m_spaceship = HL.Field(HL.Any)

ShopCreditPointsPopUpCtrl.m_queryVisitInfo = HL.Field(HL.Boolean) << false

ShopCreditPointsPopUpCtrl.m_haveFriendInfo = HL.Field(HL.Boolean) << false





ShopCreditPointsPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_RECV_QUERY_VISIT_INFO] = 'OnRecvQueryVisitInfo',
}


ShopCreditPointsPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnBack.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.ShopCreditPointsPopUp)
    end)

    self.view.salesRecordsBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SpaceshipSalesRecords)
    end)

    self.view.blackImage.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.ShopCreditPointsPopUp)
    end)

    self.m_spaceship = GameInstance.player.spaceship
    self.m_spaceship:QueryVisitInfo()
    self.view.listNodeDaily.text.text = 0
    self.view.listCellToday.text.text = 0
    self.view.listCellYesterday.text.text = 0
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end
ShopCreditPointsPopUpCtrl.OnRecvQueryVisitInfo = HL.Method() << function(self)
    self.m_queryVisitInfo = true
    self.m_visitRecord = self.m_spaceship:GetRoomVisitRecord()
    self:UpdateCells()
end

ShopCreditPointsPopUpCtrl.UpdateCells = HL.Method() << function(self)

    local hasValue, value = GameInstance.player.globalVar:TryGetServerVar(GEnums.ServerGameVarEnum.SpaceShipDailyCreditReward)

    self.view.creditDetailsPoint:SetState((hasValue and value == 1) and "DaliyCollectAll" or "DaliyToBeCollected")
    self.view.listNodeDaily.text.text = Tables.spaceshipConst.dailyAddCreditCnt
    self.view.listNodeDaily.receiveNumTxt.text = Tables.spaceshipConst.dailyAddCreditCnt
    local allCreditCnt = 0
    for _, creditData in pairs(Tables.SpaceshipCreditTable) do
        if creditData.creditRewardCnt > allCreditCnt then
            allCreditCnt = creditData.creditRewardCnt
        end
    end

    local recvedCreditCnt, totalCreditCnt
    local todayNode = self.view.listCellToday
    recvedCreditCnt = self.m_visitRecord.today.recvedCreditCnt
    totalCreditCnt = self.m_visitRecord.today.totalCreditCnt
    todayNode.todayReceiveNumTxt.text = totalCreditCnt - recvedCreditCnt
    todayNode.text.text = string.format(CREDIT_TEXT_FORMAT, totalCreditCnt, allCreditCnt)
    local state = ""
    if recvedCreditCnt == totalCreditCnt then
        if recvedCreditCnt == 0 then
            state = "TodayNotObtained"
        else
            state = "TodayCollectAll"
        end
    else
        state = "TodayToBeCollected"
    end
    self.view.creditDetailsPoint:SetState(state)


    local yesterdayNode = self.view.listCellYesterday
    recvedCreditCnt = self.m_visitRecord.yesterday.recvedCreditCnt
    totalCreditCnt = self.m_visitRecord.yesterday.totalCreditCnt
    yesterdayNode.yesterdayReceiveNumTxt.text = totalCreditCnt - recvedCreditCnt
    yesterdayNode.text.text = string.format(CREDIT_TEXT_FORMAT, totalCreditCnt, allCreditCnt)
    if recvedCreditCnt == totalCreditCnt then
        if recvedCreditCnt == 0 then
            state = "YesterdayNotObtained"
        else
            state = "YesterdayCollectAll"
        end
    else
        state = "YesterdayToBeCollected"
    end
    self.view.creditDetailsPoint:SetState(state)
end

HL.Commit(ShopCreditPointsPopUpCtrl)
