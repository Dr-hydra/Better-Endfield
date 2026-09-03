
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopChoicenessBattlePass

ShopChoicenessBattlePassCtrl = HL.Class('ShopChoicenessBattlePassCtrl', uiCtrl.UICtrl)






ShopChoicenessBattlePassCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ShopChoicenessBattlePassCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.weaponText.text = Language.LUA_BATTLEPASS_PREVIEW_WEAPON_NAME
    local seasonData = BattlePassUtils.GetSeasonData()
    if seasonData == nil then
        return
    end
    local cardId = BattlePassUtils.GetSeasonData().bussinessCardId
    local cardItemId = Tables.businessCardTopicTable[cardId].itemId
    local businessCardName = Tables.itemTable[cardItemId].name
    self.view.businessCardText.text = string.format(Language.LUA_BATTLEPASS_PREVIEW_BUSINESS_CARD_NAME, businessCardName)


    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = GameInstance.player.battlePassSystem.seasonData.closeTime - curServerTime
    leftSec = math.max(leftSec, 0)
    self.view.endTimeTxt.text = string.format(Language.LUA_BATTLEPASS_NEW_SEASON_PANEL_SEASON_END_TIME, UIUtils.getLeftTime(leftSec))


    self.view.weaponPreviewBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.BattlePassWeaponCase, { itemId = BattlePassUtils.GetSeasonData().weaponBoxId, isPreview = true })
    end)
    self.view.businessCardBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.FriendThemeChange, { selectId = BattlePassUtils.GetSeasonData().bussinessCardId })
    end)


    self.view.contentBtn.onClick:AddListener(function()

        EventLogManagerInst:GameEvent_RecommendRedirect(
            "BP",
            CashShopConst.CashShopCategoryType.Pack,
            arg.id
        )
        PhaseManager:OpenPhaseFast(PhaseId.BattlePass, {
            popupPanelId = 'BattlePassAdvancedPlanBuy',
            popupPhase = true,
            fromPhase = PhaseId.CashShop,
        })
        PhaseManager:ExitPhaseFast(PhaseId.CashShop)
    end)
end

HL.Commit(ShopChoicenessBattlePassCtrl)
