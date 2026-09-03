local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local ANIM_SELECTED_IN = "spacessformulacell_selectedcell_in"
local ANIM_SELECTED_OUT = "spacessformulacell_selectedcell_out"

SSFormulaCell = HL.Class('SSFormulaCell', UIWidgetBase)

SSFormulaCell.m_onClickFunc = HL.Field(HL.Function)

SSFormulaCell.m_info = HL.Field(HL.Any)


SSFormulaCell._OnFirstTimeInit = HL.Override() << function(self)


    self:RegisterMessage(MessageConst.ON_SPACESHIP_GROW_CABIN_SOW, function()
        self:_TryUpdateSowFormulaCell()
    end)

    self:RegisterMessage(MessageConst.ON_SPACESHIP_GROW_CABIN_BREED, function()
        self:_TryUpdateBreedFormulaCell()
    end)

end

SSFormulaCell.InitSSFormulaCell = HL.Method(HL.Table, HL.Function) << function(self, info, onClickFunc)
    self:_FirstTimeInit()

    self.m_info = info
    self.m_onClickFunc = onClickFunc

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if self.m_onClickFunc then
            self.m_onClickFunc()
        end
    end)

    self:_UpdateCommonFormulaInfo()


    self:_TryUpdateManufacturingFormulaCell()


    self:_TryUpdateSowFormulaCell()


    self:_TryUpdateBreedFormulaCell()
end

SSFormulaCell.SetSelected = HL.Method(HL.Boolean, HL.Boolean) << function(self, selected, isInit)
    if selected then
        self:SetNaviTarget(self.view.button)
    end
    if isInit then
        if selected then
            self.view.root:SampleClipAtPercent(ANIM_SELECTED_IN, 1)
        else
            self.view.root:SampleClipAtPercent(ANIM_SELECTED_OUT, 1)
        end
    else
        if selected then
            self.view.root:Play(ANIM_SELECTED_IN)
        else
            self.view.root:Play(ANIM_SELECTED_OUT)
        end
    end
end

SSFormulaCell._UpdateCommonFormulaInfo = HL.Method() << function(self)
    local info = self.m_info
    local itemCfg = Tables.itemTable[info.itemId]

    self.view.nameTxt.text = itemCfg.name
    self.view.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
    UIUtils.setItemRarityImage(self.view.rarityLine, itemCfg.rarity)

    self.view.lock.gameObject:SetActiveIfNecessary(not info.isUnlock)
    self.view.onWorking.gameObject:SetActiveIfNecessary(info.isWorking)
    self.view.onWorkingDeco.gameObject:SetActiveIfNecessary(info.isWorking)

    if info.roomAttrType ~= nil then
        local haveCharSkill = GameInstance.player.spaceship:IsRoomAttrHaveCharSkill(info.roomId, info.roomAttrType, false)
        self.view.accIconNode.gameObject:SetActiveIfNecessary(haveCharSkill)
    else
        self.view.accIconNode.gameObject:SetActiveIfNecessary(false)
    end
end

SSFormulaCell._TryUpdateManufacturingFormulaCell = HL.Method() << function(self)
    local isMfg = self.m_info.isMfg == true
    self.view.manufactureNode.gameObject:SetActiveIfNecessary(isMfg)
    if not isMfg then
        return
    end

    local mfgFormulaCfg = Tables.spaceshipManufactureFormulaTable[self.m_info.formulaId]
    local rate = GameInstance.player.spaceship:GetRoomProduceRate(self.m_info.roomId, mfgFormulaCfg.roomAttrType)
    local totalProgress = mfgFormulaCfg.totalProgress

    local node = self.view.manufactureNode
    node.capacityTxt.text = mfgFormulaCfg.perCapacity
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(totalProgress / rate)

    self:_UpdateItemCount(false)
end

SSFormulaCell._TryUpdateSowFormulaCell = HL.Method() << function(self)
    local isSow = self.m_info.isSow == true
    self.view.sowNode.gameObject:SetActiveIfNecessary(isSow)
    if not isSow then
        return
    end

    local sowFormulaCfg = Tables.spaceshipGrowCabinFormulaTable[self.m_info.formulaId]
    local seedItemCfg = Tables.itemTable[sowFormulaCfg.seedItemId]
    local rate = GameInstance.player.spaceship:GetRoomProduceRate(self.m_info.roomId, sowFormulaCfg.roomAttrType)
    local time = sowFormulaCfg.totalProgress / rate
    local seedOwnCount = Utils.getItemCount(sowFormulaCfg.seedItemId)

    local node = self.view.sowNode

    node.seedIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, seedItemCfg.iconId)
    node.timeTxt.text = UIUtils.getLeftTimeToSecond(time)

    local colorTbl = SpaceshipConst.SOW_FORMULA_COUNT_STATE_COLOR_STR
    local seedCountStateColor = seedOwnCount < sowFormulaCfg.seedItemCount and colorTbl[2] or colorTbl[1]
    node.ratioTxt.text = string.format("<color=#%s>%d</color>/%d", seedCountStateColor, seedOwnCount, sowFormulaCfg.seedItemCount)

    self:_UpdateItemCount(false)
end

SSFormulaCell._TryUpdateBreedFormulaCell = HL.Method() << function(self)
    local isBreed = self.m_info.isBreed == true
    self.view.breedNode.gameObject:SetActiveIfNecessary(isBreed)
    if not isBreed then
        return
    end

    local breedFormulaCfg = Tables.spaceshipGrowCabinSeedFormulaTable[self.m_info.formulaId]
    local materialItemCfg = Tables.itemTable[breedFormulaCfg.materialItemId]
    local outcomeItemCfg = Tables.itemTable[breedFormulaCfg.outcomeseedItemId]
    local ownMaterialItemCount = Utils.getItemCount(breedFormulaCfg.materialItemId)

    local node = self.view.breedNode
    node.outcomeCountTxt.text = breedFormulaCfg.outcomeseedItemCount
    node.outcomeIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, outcomeItemCfg.iconId)
    node.materialCountTxt.text = breedFormulaCfg.materialItemCount
    node.materialIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, materialItemCfg.iconId)

    self:_UpdateItemCount(ownMaterialItemCount < breedFormulaCfg.materialItemCount)
end

SSFormulaCell._UpdateItemCount = HL.Method(HL.Boolean) << function(self, isLack)
    local itemCount = Utils.getItemCount(self.m_info.itemId)
    local colorStr = isLack and SpaceshipConst.FORMULA_CELL_OWN_COUNT_COLOR_STR[2] or
            SpaceshipConst.FORMULA_CELL_OWN_COUNT_COLOR_STR[1]
    self.view.countTxt.text = string.format(Language.LUA_SPACESHIP_ROOM_OWN_ITEM_WITH_COLOR_FORMAT, colorStr, itemCount)
end


HL.Commit(SSFormulaCell)
return SSFormulaCell
