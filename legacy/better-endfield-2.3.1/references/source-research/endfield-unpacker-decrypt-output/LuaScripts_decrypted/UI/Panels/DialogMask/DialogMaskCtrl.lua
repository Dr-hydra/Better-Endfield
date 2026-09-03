
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogMask

DialogMaskCtrl = HL.Class('DialogMaskCtrl', uiCtrl.UICtrl)






DialogMaskCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DialogMaskCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

DialogMaskCtrl._InitBorderMask = HL.Method() << function(self, arg)
    local useBlack = true
    if GameWorld.dialogManager.dialogTree then
        local dialogType = GameWorld.dialogManager.dialogTree.dialogType
        local normal = dialogType == Const.DialogType.Normal
        if normal then
            useBlack = false
        end
    else
        useBlack = false
    end

    if useBlack then
        local screenWidth = Screen.width
        local screenHeight = Screen.height

        local maxScreenWidth = UIConst.MAX_DIALOG_ASPECT_RATIO * screenHeight
        local borderSize = (screenWidth - maxScreenWidth) / 2
        local ratio = self.view.transform.rect.width / Screen.width

        self.view.leftBorder.gameObject:SetActive(true)
        self.view.rightBorder.gameObject:SetActive(true)


    else
        self.view.leftBorder.gameObject:SetActive(false)
        self.view.rightBorder.gameObject:SetActive(false)
    end
end

DialogMaskCtrl.OnShow = HL.Override() << function(self)
    self:_InitBorderMask()
end








HL.Commit(DialogMaskCtrl)
