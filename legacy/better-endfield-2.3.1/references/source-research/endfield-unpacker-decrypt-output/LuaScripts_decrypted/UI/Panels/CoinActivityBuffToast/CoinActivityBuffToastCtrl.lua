
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CoinActivityBuffToast

local BUFF_TYPE = {
    MAGNET = 1,
    INCREASE = 2,
}

CoinActivityBuffToastCtrl = HL.Class('CoinActivityBuffToastCtrl', uiCtrl.UICtrl)

CoinActivityBuffToastCtrl.m_showingToastCor = HL.Field(HL.Thread)





CoinActivityBuffToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CoinActivityBuffToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local descTxt = self.view.coinActivityBuffToast.descTxt

    local titleTxt = self.view.coinActivityBuffToast.titleTxt

    local rootState = self.view.coinActivityBuffToast.rootState

    local buffType, title, content = unpack(arg)
    titleTxt.text = title
    descTxt.text = content
    if buffType == BUFF_TYPE.MAGNET then
        rootState:SetState("RangeBuff")
    else
        rootState:SetState("MultipleBuff")
    end

    self.m_showingToastCor = self:_StartCoroutine(function()
        local time = self.view.config.TIME
        coroutine.wait(time)

        self.view.coinActivityBuffToast.animationWrapper:PlayOutAnimation(function()
            self:Close()
        end)
    end)
end





CoinActivityBuffToastCtrl.OnClose = HL.Override() << function(self)
    if self.m_showingToastCor then
        self.m_showingToastCor = self:_ClearCoroutine(self.m_showingToastCor)
    end
end



CoinActivityBuffToastCtrl.ShowToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("CoinActivityBuffToast", function()
        if not Utils.isInDungeon() then


            logger.error(ELogChannel.Dungeon, "error, try to open CoinActivityBuffToastCtrl out of dungeon")
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






HL.Commit(CoinActivityBuffToastCtrl)
