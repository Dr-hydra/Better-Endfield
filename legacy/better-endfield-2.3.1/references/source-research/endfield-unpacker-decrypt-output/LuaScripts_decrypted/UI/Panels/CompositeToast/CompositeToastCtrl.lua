
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CompositeToast
CompositeToastCtrl = HL.Class('CompositeToastCtrl', uiCtrl.UICtrl)







CompositeToastCtrl.s_messages = HL.StaticField(HL.Table) << {

}


CompositeToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local needItems = arg[3]
    local count = #needItems
    local node
    for k = 1, 3 do
        local tmp = self.view[k .. "Item"]
        if k == count then
            tmp.gameObject:SetActive(true)
            node = tmp
        else
            tmp.gameObject:SetActive(false)
        end
    end
    for i = 1, #needItems do
        local _, item = Tables.itemTable:TryGetValue(needItems[i].id)
        node.transform:GetChild(i - 1):GetComponent(typeof(CS.Beyond.UI.UIImage)):LoadSprite(UIConst.UI_SPRITE_ITEM, item.iconId)
    end
    self:_StartTimer(1, function()
        UIManager:Close(PANEL_ID)
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, arg[1])
    end)
end











HL.Commit(CompositeToastCtrl)
