local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaWeaponResult
local PHASE_ID = PhaseId.GachaWeaponResult
GachaWeaponResultCtrl = HL.Class('GachaWeaponResultCtrl', uiCtrl.UICtrl)






GachaWeaponResultCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GachaWeaponResultCtrl.m_curFocusCell = HL.Field(HL.Any)


GachaWeaponResultCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickExit()
    end)
    self.view.shareBtn.onClick:AddListener(function()
        if self.m_curFocusCell then
            self.m_curFocusCell.hintRect.gameObject:SetActive(false)
        end
        self.view.btnNode.gameObject:SetActive(false)
        Notify(MessageConst.SHOW_COMMON_SHARE_PANEL, {
            type = "DrawWeapon",
            onClose = function()
                if self.m_curFocusCell then
                    self.m_curFocusCell.hintRect.gameObject:SetActive(true)
                end
                self.view.btnNode.gameObject:SetActive(true)
            end
        })
    end)

    local weapons = arg.weapons
    local maxRarity = 0
    local spriteDict = {}
    local weaponSpriteDict = {}
    for i, weapon in ipairs(weapons) do
        local cell = self.view['gachaWeaponResultCell' .. i]
        cell.gameObject:SetActive(true)
        local rarity = weapon.rarity
        maxRarity = math.max(maxRarity, rarity)
        for j = 4,6 do
            if j == rarity then
                cell["starBg" .. j].gameObject:SetActive(true)
                cell["starLight" .. j].gameObject:SetActive(true)
            else
                cell["starBg" .. j].gameObject:SetActive(false)
                cell["starLight" .. j].gameObject:SetActive(false)
            end
        end
        if #weapon.items > 0 then
            cell.itemNumberNode.gameObject:SetActive(true)
            local item = weapon.items[1]
            local itemId = item.id
            local itemData = Tables.itemTable:GetValue(itemId)
            local sprite
            if not spriteDict[itemId] then
                sprite = self:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
                spriteDict[itemId] = sprite
            else
                sprite = spriteDict[itemId]
            end
            cell.icon.sprite = sprite
            cell.countTxt.text = "×" .. item.count
        else
            cell.itemNumberNode.gameObject:SetActive(false)
        end
        cell.newNode.gameObject:SetActive(weapon.isNew)

        local weaponSprite
        local weaponItemData = Tables.itemTable:GetValue(weapon.weaponId)
        if not weaponSpriteDict[weaponItemData.iconId] then
            weaponSprite = self:LoadSprite(UIConst.UI_SPRITE_GACHA_WEAPON, weaponItemData.iconId)
            weaponSpriteDict[weaponItemData.iconId] = weaponSprite
        else
            weaponSprite = weaponSpriteDict[weaponItemData.iconId]
        end

        cell.weaponImg.sprite = weaponSprite
        cell.weaponShadowImg.sprite = weaponSprite
        cell.bgImageMask.enabled = (i ~= 1)
        cell.button.onClick:AddListener(function()
            WikiUtils.showWeaponPreview({ weaponId = weapon.weaponId })
        end)
        cell.hintRect.gameObject:SetActive(false)
        cell.button.onIsNaviTargetChanged = function(isTarget)
            cell.hintRect.gameObject:SetActive(isTarget)
            if isTarget then
                self.m_curFocusCell = cell
            elseif self.m_curFocusCell == cell then
                self.m_curFocusCell = nil
            end
        end
    end


    self:PlayAnimationIn()

    if maxRarity >= 6 then
        AudioManager.PostEvent("Au_UI_Gacha_Sum6_weapon")
    else
        AudioManager.PostEvent("Au_UI_Gacha_Sum_weapon")
    end


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.weaponNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            self:SetNaviTarget(self.view.gachaWeaponResultCell10.button)
        end
    end)
end

GachaWeaponResultCtrl._OnClickExit = HL.Method() << function(self)
    local arg = self.m_phase.arg
    if arg and arg.onComplete then
        arg.onComplete()
    end

    PhaseManager:ExitPhaseFast(PhaseId.GachaWeaponResult)
end

HL.Commit(GachaWeaponResultCtrl)
