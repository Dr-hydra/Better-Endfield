
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MusicPlayerPopup

MusicPlayerPopupCtrl = HL.Class('MusicPlayerPopupCtrl', uiCtrl.UICtrl)

MusicPlayerPopupCtrl.m_musicIdList = HL.Field(HL.Table)

MusicPlayerPopupCtrl.m_getItemCell = HL.Field(HL.Function)





MusicPlayerPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MusicPlayerPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullMask.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
    self.view.rewardsScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)

    local unlockList = GameInstance.player.spaceship:GetUnlockMusicIds()
    self.m_musicIdList = {}
    if unlockList ~= nil then
        for i = 1, unlockList.Count do
            local musicId = unlockList[CSIndex(i)]
            if not RedDotUtils.checkMusicIsRead(musicId) then
                table.insert(self.m_musicIdList, musicId)
            end
        end
    end
    self.view.rewardsScrollList:UpdateCount(#self.m_musicIdList)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

MusicPlayerPopupCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local musicId = self.m_musicIdList[index]

    local hasValue, itemCfg = Tables.itemTable:TryGetValue(musicId)
    if not hasValue then
        return
    end
    local musicData = Tables.spaceshipMusicTable[musicId]
    cell:Init(itemCfg.name, musicData.duration)
end

HL.Commit(MusicPlayerPopupCtrl)
