local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CoinTaskFinishToast
local DEFAULT_TOAST_TIME = 3

CoinTaskFinishToastCtrl = HL.Class('CoinTaskFinishToastCtrl', uiCtrl.UICtrl)

CoinTaskFinishToastCtrl.m_showingToastCor = HL.Field(HL.Thread)






CoinTaskFinishToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CoinTaskFinishToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local taskName = ""
    if type(arg) == "table" then
        taskName = arg.taskName or ""
    end
    self.view.taskFinishDetailTxt.text = taskName

    self.m_showingToastCor = self:_StartCoroutine(function()
        coroutine.wait(DEFAULT_TOAST_TIME)
        self:PlayAnimationOut()
    end)
end





CoinTaskFinishToastCtrl.OnClose = HL.Override() << function(self)
    if self.m_showingToastCor then
        self.m_showingToastCor = self:_ClearCoroutine(self.m_showingToastCor)
    end
end



CoinTaskFinishToastCtrl.ShowToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("CoinTaskFinishToast", function()
        if not Utils.isInDungeon() then

            logger.error(ELogChannel.Dungeon, "error, try to open CoinTaskFinishToastCtrl out of dungeon")
            return
        end

        local isOpen, _ = UIManager:IsOpen(PANEL_ID)
        if isOpen then
            UIManager:Close(PANEL_ID)
        end

        UIManager:AutoOpen(PANEL_ID, args)
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end



HL.Commit(CoinTaskFinishToastCtrl)
