
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCustomEntry
local PHASE_ID = PhaseId.DungeonCustomEntry
DungeonCustomEntryCtrl = HL.Class('DungeonCustomEntryCtrl', uiCtrl.UICtrl)






DungeonCustomEntryCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DungeonCustomEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local dungeonCustomInfoArg = arg.dungeonCustomInfoArg
    self.view.dungeonCustomInfo:InitDungeonCustomInfo(dungeonCustomInfoArg)

    if string.isEmpty(arg.title) then
        self.view.bgNode.titleParent.gameObject:SetActive(false)
    else
        self.view.bgNode.titleParent.gameObject:SetActive(true)
        self.view.bgNode.titleTxt.text = arg.title
    end
    self.view.bgNode.btnClose.onClick:RemoveAllListeners()
    self.view.bgNode.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.DungeonCustomEntry)
    end)

    local goButtonText = arg.goButtonText or dungeonCustomInfoArg.entryButtonText
    if goButtonText then
        self.view.btnGo.text = goButtonText
    end
    self.view.btnGo.gameObject:SetActive(true)
    self.view.btnGo.onClick:RemoveAllListeners()
    self.view.btnGo.onClick:AddListener(function()
        if arg.onGoClick then
            arg.onGoClick()
        elseif dungeonCustomInfoArg.onEntryClick then
            dungeonCustomInfoArg.onEntryClick()
        end
    end)

    local showJumpButton = arg.showJumpButton == true
    self.view.btnJump.gameObject:SetActive(showJumpButton)
    self.view.btnJump.onClick:RemoveAllListeners()
    if arg.jumpButtonText then
        self.view.btnJump.text = arg.jumpButtonText
    end
    if showJumpButton then
        self.view.btnJump.onClick:AddListener(function()
            if arg.onJumpClick then
                arg.onJumpClick()
            end
        end)
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end











HL.Commit(DungeonCustomEntryCtrl)
