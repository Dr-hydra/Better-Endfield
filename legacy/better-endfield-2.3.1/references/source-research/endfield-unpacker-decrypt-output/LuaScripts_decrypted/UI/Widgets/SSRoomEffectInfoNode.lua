local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SSRoomEffectInfoNode = HL.Class('SSRoomEffectInfoNode', UIWidgetBase)



SSRoomEffectInfoNode.m_attrInfoList = HL.Field(HL.Table)

SSRoomEffectInfoNode.m_cells = HL.Field(HL.Forward('UIListCache'))

SSRoomEffectInfoNode.m_clueCells = HL.Field(HL.Forward('UIListCache'))



SSRoomEffectInfoNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_cells = UIUtils.genCellCache(self.view.roomInfoCell)
end








SSRoomEffectInfoNode.InitSSRoomEffectInfoNode = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local fromCS = args.attrsMap ~= nil
    if fromCS then
        self.m_attrInfoList = {}
        local combineAttrTable = {}
        for k, v in pairs(args.attrsMap) do
            local attrType = GEnums.SpaceshipRoomAttrType.__CastFrom(k)
            local succ, data = Tables.spaceshipRoomAttrTypeTable:TryGetValue(attrType)
            if succ then
                local combineType = SpaceshipConst.SPACESHIP_ATTR_COMBINE_TYPE[attrType]
                if not combineType then
                    table.insert(self.m_attrInfoList, {
                        type = attrType,
                        typeData = data,
                        sortId = data.sortId,
                        attr = v,
                    })
                end
                if combineType and not combineAttrTable[combineType] then
                    combineAttrTable[combineType] = true
                end
            end
        end
    else
        self.m_attrInfoList = args.attrInfoList
    end

    table.sort(self.m_attrInfoList, Utils.genSortFunction({"sortId"}, false))
    local color = args.color
    self.m_cells:Refresh(#self.m_attrInfoList, function(cell, index)
        local info = self.m_attrInfoList[index]
        cell.nameTxt.text = info.typeData.name
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, info.typeData.icon)
        local isPercent = info.typeData.isPercent
        local showRate = info.typeData.showRate
        cell.gameObject:SetActive(true)
        if showRate > 0 then
            local maxValue = 10000
            if info.type == GEnums.SpaceshipRoomAttrType.PSRecoveryRate then
                maxValue = Tables.spaceshipConst.maxPhysicalStrength
            end
            cell.valueTxt.gameObject:SetActive(true)
            if isPercent then
                cell.valueTxt.text = lume.round(10000 * info.attr.Value * showRate / maxValue) .. "%"
            else
                cell.valueTxt.text = lume.round(10000 * info.attr.Value * showRate / maxValue)
            end
        else
            cell.valueTxt.gameObject:SetActive(false)
        end

        cell.hintBtn.onClick:RemoveAllListeners()
        if string.isEmpty(info.typeData.desc) then
            cell.hintBtn.gameObject:SetActive(false)
        else
            cell.hintBtn.gameObject:SetActive(true)
            cell.hintBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_COMMON_TIPS, {
                    text = info.typeData.desc,
                    transform = cell.hintBtn.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
                })
            end)
        end


        if info.type == GEnums.SpaceshipRoomAttrType.GuestRoomClueProbabilityIncreaseShow then
            if not self.m_clueCells then
                self.m_clueCells = UIUtils.genCellCache(cell.upgradeClue)
            end
            cell.basicValueNode.gameObject:SetActive(false)
            cell.skillValueNode.gameObject:SetActive(false)
            local targetAttrs
            local targetCount = 0
            if fromCS then
                targetAttrs = GameInstance.player.spaceship:ParseRoomAttClueAddition(info.attr)
                targetCount = targetAttrs.Count
            else
                targetAttrs = info.attr.clueSkill
                targetCount = lume.count(info.attr.clueSkill)
            end
            local clueShowDataTable = {}
            if targetCount > 0 then
                for k, v in pairs(targetAttrs) do
                    local attrType
                    if fromCS then
                        attrType = GEnums.SpaceshipRoomAttrType.__CastFrom(k)
                    else
                        attrType = SpaceshipConst.SPACESHIP_CLUE_INDEX_2_ATTR_TYPE[k]
                    end
                    local succ, data = Tables.spaceshipRoomAttrTypeTable:TryGetValue(attrType)
                    if succ then
                        table.insert(clueShowDataTable, {
                            type = attrType,
                            typeData = data,
                            sortId = data.sortId,
                            attr = v,
                        })
                    end
                end
                table.sort(clueShowDataTable, Utils.genSortFunction({"sortId"}, false))
            end
            if #clueShowDataTable == 0 then
                cell.gameObject:SetActive(false)
                return
            end
            self.m_clueCells:Refresh(#clueShowDataTable, function(clueCell, clueIndex)
                local clueInfo = clueShowDataTable[clueIndex]
                clueCell.gameObject:SetActive(true)
                clueCell.clueNumberTxt.text = clueInfo.typeData.name

                for i, value in pairs(Tables.spaceshipConst.collectClueXRateIncreaseList) do
                    local clueArrowIndex = LuaIndex(i)
                    local clueArrowCell = clueCell["clueArrowImg0" .. clueArrowIndex]
                    if clueArrowCell then
                        if fromCS then
                            local active = math.floor(clueInfo.attr.Value * 1000 - value * 1000) >= 0
                            clueArrowCell.gameObject:SetActive(active)
                        else
                            clueArrowCell.gameObject:SetActive(clueInfo.attr >= clueArrowIndex)
                        end
                    end
                end
            end)
        else
            cell.upgradeClue.gameObject:SetActive(false)
            local baseValue, addFromCharStation, addFromCharSkill
            if fromCS then
                baseValue, addFromCharStation, addFromCharSkill = GameInstance.player.spaceship:ParseRoomAttr(info.attr)
            else
                baseValue, addFromCharStation, addFromCharSkill = info.attr.baseValue, info.attr.addFromCharStation, info.attr.addFromCharSkill
            end

            if showRate > 0 and addFromCharStation ~= 0 then
                cell.basicValueNode.gameObject:SetActive(true)
                cell.basicValueTxt.text = self:_GetNumStr(lume.round(addFromCharStation * showRate), isPercent)
            else
                cell.basicValueNode.gameObject:SetActive(false)
            end
            if showRate > 0 and addFromCharSkill ~= 0 then
                cell.skillValueNode.gameObject:SetActive(true)
                cell.skillValueNode.color = color
                cell.skillValueTxt.text = self:_GetNumStr(lume.round(addFromCharSkill * showRate), isPercent)
            else
                cell.skillValueNode.gameObject:SetActive(false)
            end
        end
    end)
end

SSRoomEffectInfoNode._GetNumStr = HL.Method(HL.Number, HL.Boolean).Return(HL.String) << function(self, num, isPercent)
    local sign = num > 0 and "+" or ""
    if isPercent then
        return string.format("%s%d%%", sign, math.floor(num))
    else
        return string.format("%s%d", sign, math.floor(num))
    end
end


HL.Commit(SSRoomEffectInfoNode)
return SSRoomEffectInfoNode
