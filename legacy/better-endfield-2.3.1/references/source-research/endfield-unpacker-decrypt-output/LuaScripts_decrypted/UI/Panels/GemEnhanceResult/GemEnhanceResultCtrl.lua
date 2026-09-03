
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemEnhanceResult

GemEnhanceResultCtrl = HL.Class('GemEnhanceResultCtrl', uiCtrl.UICtrl)






GemEnhanceResultCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GemEnhanceResultCtrl.m_termLevelCellCache = HL.Field(HL.Forward("UIListCache"))








GemEnhanceResultCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.btnClose.onClick:AddListener(function()
        if args.onConfirm ~= nil then
            args.onConfirm()
        end
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local gemInst = CharInfoUtils.getGemByInstId(args.gemInstId)
    if not gemInst then
        return
    end
    if args.termIndex >= gemInst.termList.Count then
        logger.error(string.format("GemEnhanceResultCtrl.OnCreate error termIndex %d, termCount %d, gemInstId %d",
            args.termIndex, gemInst.termList.Count, args.gemInstId))
        return
    end
    local term = gemInst.termList[args.termIndex]

    self.view.successContent.gameObject:SetActive(args.isSuccess)
    self.view.failContent.gameObject:SetActive(not args.isSuccess)
    self.view.successVfx.gameObject:SetActive(args.isSuccess)
    self.view.obtainTips.gameObject:SetActive(not args.isSuccess)

    self.view.listCellGem.item:InitItem({ id = gemInst.templateId, instId = gemInst.instId }, true)
    local _, termCfg = Tables.gemTable:TryGetValue(term.termId)
    if termCfg then
        self.view.txtAttrName:SetAndResolveTextStyle(string.format(Language.LUA_GEM_CARD_SKILL_ACTIVE, termCfg.tagName))
    end
    local cost = args.isSuccess and term.cost - 1 or term.cost
    self.view.txtAttrValueBefore.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, cost)
    self.view.imgArrow.gameObject:SetActive(args.isSuccess)
    self.view.txtAttrValueAfter.gameObject:SetActive(args.isSuccess)
    if args.isSuccess then
        self.view.txtAttrValueAfter.text = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_ADD_FORMAT, term.cost)
    end
    self.m_termLevelCellCache = UIUtils.genCellCache(self.view.grooveCell)
    self.m_termLevelCellCache:Refresh(term.cost, function(cell, luaIndex)
        cell.progress.gameObject:SetActive(luaIndex <= cost)
        cell.progressAdd.gameObject:SetActive(luaIndex > cost)
    end)
    local isMax = CharInfoUtils.isGemTermEnhanceMax(term.termId, term.cost)
    self.view.maxNode.gameObject:SetActive(isMax)

    if not args.isSuccess then
        local returnItemData = Tables.itemTable[Tables.gemConst.gemEnhancementItemId]
        self.view.imgIconReturn:LoadSprite(UIConst.UI_SPRITE_ITEM, returnItemData.iconId)
        self.view.txtReturn.text = string.format("X%d", Tables.gemConst.gemEnhancementItemRefundNum)
        AudioAdapter.PostEvent("Au_UI_Popup_EssenceEtchingFail_Open")
    else
        AudioAdapter.PostEvent("Au_UI_Popup_EssenceEtchingSuccess_Open")
    end
    self:_StartTimer(self.view.config.REWARD_AUDIO_DELAY_TIME, function()
        AudioAdapter.PostEvent("Au_UI_Popup_RewardsItem_Open")
    end)
end

HL.Commit(GemEnhanceResultCtrl)
