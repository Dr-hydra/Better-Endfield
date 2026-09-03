
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoTips

CharInfoTipsCtrl = HL.Class('CharInfoTipsCtrl', uiCtrl.UICtrl)






CharInfoTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_CLOSE_INFO_TIP] = '_CloseTips',
}








CharInfoTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_CloseTips()
    end)
end










CharInfoTipsCtrl.ShowCharTacticalItemTips = HL.StaticMethod(HL.Table) << function(args)
    local self = UIManager:AutoOpen(PANEL_ID)
    self:_ShowTacticalItemTips(args)
end





CharInfoTipsCtrl._ShowTacticalItemTips = HL.Method(HL.Table) << function(self, args)
    self.view.stateCtrl:SetState("tacticalItem")
    local itemId = args.itemId
    local itemCfg = Tables.itemTable[itemId]

    self.view.skillName.text = itemCfg.name
    self.view.skillTypeName.text = Language.ui_char_formation_tactical_item_type
    self.view.desc:SetAndResolveTextStyle(UIUtils.getItemUseDesc(itemId))
    self.view.descSec:SetAndResolveTextStyle(UIUtils.getItemEquippedDesc(itemId))
    self.view.btnExchange.gameObject:SetActive(not args.isLocked)
    self.view.medicalInfoNode.notConfigurable.gameObject:SetActive(args.isLocked)
    local itemCount = GameInstance.player.inventory:GetTacticalItemCount(
        Utils.getCurrentScope(), itemId, args.charInstId)
    local itemCountPrefix = args.isLocked and Language.LUA_TACTICAL_ITEM_CARRY_TRAIL or Language.LUA_TACTICAL_ITEM_CARRY
    self.view.medicalInfoNode.num.text = UIUtils.setCountColor(itemCountPrefix..itemCount, itemCount <= 0)
    if not args.isLocked then
        self.view.btnExchange.onClick:RemoveAllListeners()
        self.view.btnExchange.onClick:AddListener(function()
            CharInfoUtils.openCharInfoBestWay({
                pageType = UIConst.CHAR_INFO_PAGE_TYPE.EQUIP,
                initCharInfo = {
                    instId = args.charInstId,
                    templateId = args.charTemplateId,
                    isSingleChar = true,
                },
                forceSkipIn = true,
                extraArg = {
                    slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL
                }
            })
            self:Close()
        end)
    end

    self:_UpdateLayoutAndPosition(args)
end

CharInfoTipsCtrl._UpdateLayoutAndPosition = HL.Method(HL.Table) << function(self, args)













    UIUtils.updateTipsPosition(self.view.content, args.targetTransform,
        self.view.rectTransform, self.uiCamera, args.tipPosType)
end

CharInfoTipsCtrl._CloseTips = HL.Method() << function(self)
    self:Close()
end

HL.Commit(CharInfoTipsCtrl)
