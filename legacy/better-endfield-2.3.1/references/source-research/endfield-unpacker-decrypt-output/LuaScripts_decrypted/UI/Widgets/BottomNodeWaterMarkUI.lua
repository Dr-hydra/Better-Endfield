local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BottomNodeWaterMarkUI = HL.Class('BottomNodeWaterMarkUI', UIWidgetBase)


BottomNodeWaterMarkUI._OnFirstTimeInit = HL.Override() << function(self)

end

BottomNodeWaterMarkUI.InitBottomNodeWaterMarkUI = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()
    self.view.logoImage:LoadSprite(UIConst.UI_COMMON_SHARE_LOGO_SPRITE_PATH, "deco_endfield_logo")
    if arg.type then
        self.view.stateController:SetState(arg.type)
        self.view.playerNameText.text = GameInstance.player.playerInfoSystem.playerName
        self.view.uidText.text = GameInstance.player.playerInfoSystem.roleId
        if arg.type == "Blueprint" then
            self.view.codeIdTxt.text = arg.codeId
            self.view.shareText.text = arg.title
        elseif arg.type == "PhotoShot" then
            self.view.apertureText.text = arg.aperture
            self.view.focusText.text = arg.focus
        elseif arg.type == "ContingencyContractSettlement" then

            local node = self.view.settlementNode
            node.timeTxt.text = arg.timeStamp
            node.codeTxt.text = arg.showCodeId
        elseif arg.type == "EchoesOfWar" then

            local node = self.view.settlementNode
            node.timeTxt.text = arg.timeStamp
        end
    end
end

HL.Commit(BottomNodeWaterMarkUI)
return BottomNodeWaterMarkUI

