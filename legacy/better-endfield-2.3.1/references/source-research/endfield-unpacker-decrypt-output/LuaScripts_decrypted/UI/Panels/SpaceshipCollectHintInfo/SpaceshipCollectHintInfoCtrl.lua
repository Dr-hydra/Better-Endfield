
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipCollectHintInfo
local PHASE_ID = PhaseId.SpaceshipCollectHintInfo
SpaceshipCollectHintInfoCtrl = HL.Class('SpaceshipCollectHintInfoCtrl', uiCtrl.UICtrl)






SpaceshipCollectHintInfoCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SpaceshipCollectHintInfoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SpaceshipCollectHintInfo)
    end)

    local spaceship = GameInstance.player.spaceship
    local rooms = {}
    for roomId, roomInfo in pairs(spaceship.rooms) do
        local succ, products = spaceship:GetRoomProduct(roomId)
        if succ and products.Count > 0 then
            local _, data = spaceship:TryGetRoom(roomId)
            local productsLua = {}
            for k, v in pairs(products) do
                local iData = Tables.itemTable[k]
                table.insert(productsLua, {
                    id = k,
                    count = v,
                    sortId1 = iData.sortId1,
                    sortId2 = iData.sortId2,
                })
            end
            table.sort(productsLua, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

            table.insert(rooms, {
                id = roomId,
                room = roomInfo,
                data = data,
                sortId = 1,
                products = productsLua,
            })
        end
    end
    table.sort(rooms, Utils.genSortFunction({"sortId"}))

    local cells = UIUtils.genCellCache(self.view.roomCell)
    cells:Refresh(#rooms, function(cell, index)
        self:_RefreshCell(cell, rooms[index])
    end)
end

SpaceshipCollectHintInfoCtrl._RefreshCell = HL.Method(HL.Table, HL.Table) << function(self, cell, info)
    local roomInfo = info.room
    local roomTypeData = Tables.spaceshipRoomTypeTable[info.data.roomType]
    cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
    cell.nameTxt.text = info.data.name
    cell.iconBg.color = UIUtils.getColorByString(roomTypeData.color)


    local typeTxtStr = SpaceshipConst.TYPE_TXT_MAP[roomInfo.type]
    if typeTxtStr then
        cell.typeNode.gameObject:SetActive(true)
        cell.typeTxt.text = typeTxtStr
    else
        cell.typeNode.gameObject:SetActive(false)
    end

    local charCells = UIUtils.genCellCache(cell.charCell)
    local curMaxCount = roomInfo.maxStationCharNum
    local curCount = roomInfo.stationedCharList.Count
    charCells:Refresh(curMaxCount, function(charCell, index)
        if index <= curCount then
            charCell.view.simpleStateController:SetState("Normal")
            local charId = roomInfo.stationedCharList[CSIndex(index)]
            charCell:InitSSCharHeadCell({
                charId = charId,
                targetRoomId = roomInfo.id,
                onClick = function()
                    Notify(MessageConst.SHOW_SPACESHIP_CHAR_TIPS, {
                        key = charCell.transform,
                        charId = charId,
                        transform = charCell.transform,
                        blockOtherInput = true
                    })
                end,
            })
            charCell.view.staminaNode.gameObject:SetActive(false)
            charCell.view.workStateNode.gameObject:SetActive(false)
        else
            charCell.view.simpleStateController:SetState("Empty")
        end
    end)

    local itemCells = UIUtils.genCellCache(cell.item)
    itemCells:Refresh(#info.products, function(itemCell, index)
        local v = info.products[index]
        itemCell:InitItem(v, true)
    end)
end

HL.Commit(SpaceshipCollectHintInfoCtrl)
