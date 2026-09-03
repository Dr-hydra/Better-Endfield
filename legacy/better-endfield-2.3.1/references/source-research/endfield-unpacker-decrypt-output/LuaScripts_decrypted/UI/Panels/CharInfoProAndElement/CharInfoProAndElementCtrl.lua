
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoProAndElement

CharInfoProAndElementCtrl = HL.Class('CharInfoProAndElementCtrl', uiCtrl.UICtrl)






CharInfoProAndElementCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CharInfoProAndElementCtrl.m_professionCache = HL.Field(HL.Forward("UIListCache"))



CharInfoProAndElementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    end)

    self.m_professionCache = UIUtils.genCellCache(self.view.contentNode.professionCell)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "CharInfoProAndElement", self.view.inputGroup.groupId)
end

CharInfoProAndElementCtrl.OnShow = HL.Override() << function(self)
    self:_Refresh()
end





CharInfoProAndElementCtrl._Refresh = HL.Method() << function(self)
    local proList = Tables.charProfessionTable
    local proTable = {}

    for _, proData in pairs(proList) do
        local profession = proData.profession
        local proSpriteName = CharInfoUtils.getCharProfessionIconName(profession)
        local sprite = self:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)
        local data = {
            name = proData.name,
            desc = proData.desc,
            sprite = sprite,
            sortId = proData.sortId,
        }
        table.insert(proTable, data)
    end
    table.sort(proTable, Utils.genSortFunction({ "sortId" }, true))

    self.m_professionCache:Refresh(#proTable, function(cell, luaIndex)
        local data = proTable[luaIndex]
        cell.titleText.text = data.name
        cell.descText:SetAndResolveTextStyle(data.desc)
        cell.professionIcon.sprite = data.sprite
    end)
end

HL.Commit(CharInfoProAndElementCtrl)
