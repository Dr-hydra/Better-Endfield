
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SnapshotControllerEditSticker

SnapshotControllerEditStickerCtrl = HL.Class('SnapshotControllerEditStickerCtrl', uiCtrl.UICtrl)






SnapshotControllerEditStickerCtrl.s_messages = HL.StaticField(HL.Table) << {

}



SnapshotControllerEditStickerCtrl.m_OnExitEdit = HL.Field(HL.Function)





SnapshotControllerEditStickerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_OnExitEdit = arg
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self:BindInputPlayerAction("snapshot_controller_move_sticker_only_show", function()

    end)
    self:BindInputPlayerAction("snapshot_controller_exit_sticker_edit", function()
        if self.m_OnExitEdit then
            self.m_OnExitEdit()
        end
    end)
end


HL.Commit(SnapshotControllerEditStickerCtrl)
