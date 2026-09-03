
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BoxGame
BoxGameCtrl = HL.Class('BoxGameCtrl', uiCtrl.UICtrl)







BoxGameCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_BOX_GAME_PANEL] = 'HidePanel',

}


BoxGameCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.main.gameObject:SetActive(false)
    self.view.buttonCancel.onClick:AddListener(function()
        GameWorld.gameMechManager.boxGameBrain:PutDownCurrentBox()
    end)
end

BoxGameCtrl.ShowPanel = HL.StaticMethod() << function()
    local ctrl = BoxGameCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_ShowPanel()
end

BoxGameCtrl._ShowPanel = HL.Method() << function(self)
    self.view.main.gameObject:SetActiveIfNecessary(true)
end

BoxGameCtrl.HidePanel = HL.Method() << function(self)
    local ctrl = BoxGameCtrl.AutoOpen(PANEL_ID, nil, false)
    self.view.main.gameObject:SetActiveIfNecessary(false)
end

HL.Commit(BoxGameCtrl)
