
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.InstructionBook
InstructionBookCtrl = HL.Class('InstructionBookCtrl', uiCtrl.UICtrl)







InstructionBookCtrl.s_messages = HL.StaticField(HL.Table) << {

}

InstructionBookCtrl.id = HL.Field(HL.Any)

InstructionBookCtrl.onClose = HL.Field(HL.Any) << nil


InstructionBookCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local id
    if type(arg) == "table" then
        id = arg.id
        self.onClose = arg.onClose
    else
        id = arg
    end
    self.id = id
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)
    self.view.maskBtn.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    local succ, data = Tables.instructionBook:TryGetValue(id)
    if succ then
        self.view.titleText:SetAndResolveTextStyle(data.title)
        self.view.contentTxt:SetAndResolveTextStyle(data.content)
    else
        self.view.titleText.text = id
        self.view.contentTxt.text = id
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

InstructionBookCtrl.OnClose = HL.Override() << function(self)
    if self.onClose ~= nil then
        self.onClose()
    end
end
HL.Commit(InstructionBookCtrl)
