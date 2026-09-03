local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local State = {
    None = "None",
    Locked = "Locked",
    CanUnlock = "CanUnlock",
    Unlocked = "Unlocked",
}

FacTechTreeLayerCell = HL.Class('FacTechTreeLayerCell', UIWidgetBase)

FacTechTreeLayerCell.m_layerId = HL.Field(HL.String) << ""

FacTechTreeLayerCell.m_state = HL.Field(HL.String) << State.None


FacTechTreeLayerCell._OnFirstTimeInit = HL.Override() << function(self)

end

FacTechTreeLayerCell.InitFacTechTreeLayerCell = HL.Method(HL.String, HL.Number, HL.Number, HL.Number, HL.Function)
        << function(self, layerId, sizeX, sizeY, notchAdapterX, onClickFunc)
    self:_FirstTimeInit()

    self.gameObject.name = "Layer-" .. layerId

    self.m_layerId = layerId
    self.view.rectTransform.sizeDelta = Vector2(sizeX + notchAdapterX * 2, sizeY)
    self.view.craft.anchoredPosition = Vector2(notchAdapterX, 0)
    self.view.unlock.anchoredPosition = Vector2(notchAdapterX, 0)

    self.view.craftBtn.onClick:RemoveAllListeners()
    self.view.craftBtn.onClick:AddListener(function()
        onClickFunc()
    end)

    self.view.craftBtn.onHoverChange:AddListener(function(isHover)
        self:_OnHoverChangeCraftBtn(isHover)
    end)

    self.view.infoBtn.onClick:AddListener(function()
        onClickFunc()
    end)

    local layerData = Tables.facSTTLayerTable[self.m_layerId]
    local order = layerData.order
    local spriteSrcName = string.format("deco_factechtreenew_shadow0%s", tostring(order))
    self.view.decoNumberN:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, spriteSrcName)
    self.view.decoNumberL:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, spriteSrcName)

    local layerName = layerData.name
    self.view.txtN.text = layerName
    self.view.txtU.text = layerName
    self.view.txtL.text = layerName

    self.view.stateController:SetState(layerData.isTBD and "TBD" or "Normal")

    self:Refresh(true)
end

FacTechTreeLayerCell.Refresh = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local layerData = Tables.facSTTLayerTable[self.m_layerId]

    local isEnough = true
    for _, costItem in pairs(layerData.costItems) do
        if Utils.getItemCount(costItem.costItemId) < costItem.costItemCount then
            isEnough = false
            break
        end
    end

    local facTechTreeSystem = GameInstance.player.facTechTreeSystem
    local isLocked = facTechTreeSystem:LayerIsLocked(self.m_layerId)
    local hasPreLayer = not string.isEmpty(layerData.preLayer)
    local canLock = isLocked and isEnough and
            (not hasPreLayer or hasPreLayer and not facTechTreeSystem:LayerIsLocked(layerData.preLayer))

    local preState = self.m_state
    if not isLocked then
        self.m_state = State.Unlocked
    elseif canLock then
        self.m_state = State.CanUnlock
    else
        self.m_state = State.Locked
    end

    if isInit then
        self.view.stateController:SetState(self.m_state)
    else
        if preState == State.CanUnlock and self.m_state == State.Unlocked then
            self.view.animationWrapper:Play("factechtreelayer_unlock")
        elseif preState == State.Locked and self.m_state == State.CanUnlock then
            local length = self:GetUnlockClipLength()
            self:_StartTimer(length, function()
                self.view.animationWrapper:Play("factechtreelayer_waitlock")
            end)
        end
    end
end

FacTechTreeLayerCell.OnLayerInputEnableChange = HL.Method(HL.Boolean) << function(self, isEnabled)
    self.view.stateController:SetState(isEnabled and "ControllerEnable" or "ControllerDisable")
end

FacTechTreeLayerCell.GetUnlockClipLength = HL.Method().Return(HL.Number) << function(self)

    local clipLength = self.view.animationWrapper:GetClipLength("factechtreelayer_unlock")
    return clipLength / 2
end

FacTechTreeLayerCell._OnHoverChangeCraftBtn = HL.Method(HL.Boolean) << function(self, isHover)
    if not isHover then
        return
    end

    if self.m_state ~= State.CanUnlock then
        return
    end

    AudioAdapter.PostEvent("Au_UI_Hover_FacTreeUpgrade")
end

HL.Commit(FacTechTreeLayerCell)
return FacTechTreeLayerCell

