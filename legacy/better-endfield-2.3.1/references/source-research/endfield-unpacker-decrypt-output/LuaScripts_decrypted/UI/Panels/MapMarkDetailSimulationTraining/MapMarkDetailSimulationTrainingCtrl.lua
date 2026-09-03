local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSimulationTraining

MapMarkDetailSimulationTrainingCtrl = HL.Class('MapMarkDetailSimulationTrainingCtrl', uiCtrl.UICtrl)






MapMarkDetailSimulationTrainingCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailSimulationTrainingCtrl.m_markInstId = HL.Field(HL.String) << ""


MapMarkDetailSimulationTrainingCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.mapMarkDetailCommon.gameObject:SetActive(true)
    local markInstId = args.markInstId
    self.m_markInstId = markInstId

    local commonArgs = {}
    commonArgs.markInstId = self.m_markInstId
    commonArgs.leftBtnActive = false
    commonArgs.rightBtnActive = false
    commonArgs.bigBtnActive = true
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)

    local simulationTrainingSystem = GameInstance.player.simulationTrainingSystem
    local curLevel = simulationTrainingSystem.curLevel
    local maxLevel = simulationTrainingSystem.maxLevel
    local dailyPlayCnt = simulationTrainingSystem.dailyPlayCnt
    local maxPlayTimes = Tables.simulationTrainingConst.playTimesLimit

    if curLevel == 0 then
        self.view.lvNumTxt.text = curLevel
        self.view.lvNumTxt.color = UIUtils.getColorByString("e0e0e0")
        self.view.maxNode.gameObject:SetActive(false)
        self.view.lvStateNode.gameObject:SetActive(false)
        self.view.canPlayNode.gameObject:SetActive(false)
        self.view.finishNode.gameObject:SetActive(false)
    else
        self.view.lvStateNode.gameObject:SetActive(true)
        self.view.maxNode.gameObject:SetActive(curLevel == maxLevel)
        self.view.lvNumTxt.text = curLevel
        if curLevel == maxLevel then
            self.view.lvNumTxt.color = UIUtils.getColorByString("fffc00")
        else
            self.view.lvNumTxt.color = UIUtils.getColorByString("e0e0e0")
        end
        self.view.canPlayNode.gameObject:SetActive(dailyPlayCnt > 0)
        self.view.finishNode.gameObject:SetActive(dailyPlayCnt == 0)
        local countText = string.format(Language.LUA_SIMULATION_TRAINING_MAP_MARK_SHOW_PLAY_COUNT, dailyPlayCnt, maxPlayTimes)
        self.view.playNumTxt.text = I18nUtils.CombineStringWithLanguageSpilt("", countText)
    end
end

HL.Commit(MapMarkDetailSimulationTrainingCtrl)
