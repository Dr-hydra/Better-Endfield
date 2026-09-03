local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TipGemAttrNode = HL.Class('TipGemAttrNode', UIWidgetBase)


TipGemAttrNode.m_gemAttrCellCache = HL.Field(HL.Forward("UIListCache"))


TipGemAttrNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_gemAttrCellCache = UIUtils.genCellCache(self.view.attrCell)
end

TipGemAttrNode.InitTipGemAttrNode = HL.Method(HL.Number) << function(self, gemInstId)
    self:_FirstTimeInit()

    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    local attributeTermList, skillTermList = CharInfoUtils.classifyGemTerm(gemInst)

    self.m_gemAttrCellCache:Refresh(#attributeTermList, function(cell, index)
        local attributeInfo = attributeTermList[index]
        local termEffectCfg = attributeInfo.termEffectCfg
        local param = attributeInfo.param
        local desc = termEffectCfg.desc


        local forceShowPercent = termEffectCfg.calcType == GEnums.GemCalcType.Mul and UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.HAS_PERCENT or UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.DO_NOT_CARE
        local attributeShowInfo = AttributeUtils.generateAttributeShowInfo(termEffectCfg.attrId, param, {
            forceShowPercent = forceShowPercent,
            attrModifier = termEffectCfg.attrModifier
        })
        if not attributeShowInfo then
            return
        end

        local formatDesc = FormatUtils.replaceVars(desc, attributeShowInfo)

        cell.attrNameText.text = termEffectCfg.name
        cell.attrDescText:SetAndResolveTextStyle(formatDesc)

        LayoutRebuilder.ForceRebuildLayoutImmediate(cell.attrNameText.transform)
        LayoutRebuilder.ForceRebuildLayoutImmediate(cell.attrDescText.transform)
    end)
end

HL.Commit(TipGemAttrNode)
return TipGemAttrNode

