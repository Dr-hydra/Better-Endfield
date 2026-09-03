
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapDetectPopUp

MapDetectPopUpCtrl = HL.Class('MapDetectPopUpCtrl', uiCtrl.UICtrl)





MapDetectPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MAP_DETECTOR_ENTITY_CHANGE] = 'OnMapDetectorEntityChange',
    [MessageConst.ON_MAP_DETECTOR_SHOW_MAP] = 'OnMapDetectorShowMap',
    [MessageConst.SHOW_TOAST] = 'OnShowToast',
}

MapDetectPopUpCtrl.m_itemInfo = HL.Field(HL.Table)

MapDetectPopUpCtrl.m_itemData = HL.Field(HL.Userdata)

MapDetectPopUpCtrl.m_itemId = HL.Field(HL.String) << ""

MapDetectPopUpCtrl.m_mapInstId = HL.Field(HL.String) << ""



MapDetectPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.cancelButton.onClick:AddListener(function()
        self:_DoClose()
    end)
    self.view.confirmButton.onClick:AddListener(function()
        self:_OnConfirm()
    end)
    local itemId = arg
    if type(arg) == "table" then
        itemId = arg.itemId
        self.m_mapInstId = arg.mapInstId or ""
    end
    self:_InitItemTableData(itemId)
    self:_RefreshUI()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

MapDetectPopUpCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if string.isEmpty(self.m_itemId) then
        return
    end
    return {
        itemId = self.m_itemId,
        mapInstId = self.m_mapInstId,
        isMapDetectPopUp = true,
    }
end

MapDetectPopUpCtrl.OnShow = HL.Override() << function(self)
end

MapDetectPopUpCtrl.OnHide = HL.Override() << function(self)
end

MapDetectPopUpCtrl.OnClose = HL.Override() << function(self)
end

MapDetectPopUpCtrl._InitItemTableData = HL.Method(HL.String) << function(self, itemId)
    self.m_itemId = itemId
    self.m_itemData = Tables.itemTable:GetValue(itemId)
    self.m_itemInfo = {
        id = self.m_itemData.id,
        count = 1
    }
end

MapDetectPopUpCtrl.OnShowToast = HL.Method(HL.Any) << function(self, arg)
    local code = arg and arg[3]
    if code and (code == CS.Proto.CODE.ErrSceneMapMarkDetectorNotFound or
        code == CS.Proto.CODE.ErrSceneMapMarkDetectorNotInDomain or
        code == CS.Proto.CODE.ErrSceneMapMarkDetectorAllFound)
    then
        self:Close()
    end
end

MapDetectPopUpCtrl._RefreshUI = HL.Method() << function(self)
    self.view.contentText.text = Language.LUA_MAP_DETECT_USE_ITEM_TITLE
    self.view.subText.text = Language.LUA_MAP_DETECT_USE_ITEM_SUB_TITLE
    self.view.costItemNode.costItemCell.ownCountTxt.text = GameInstance.player.inventory:GetItemCount(
        Utils.getCurrentScope(),
        Utils.getCurrentChapterId(),
        self.m_itemId)
    self.view.costItemNode.costItemCell.item:InitItem(self.m_itemInfo, true)
end
MapDetectPopUpCtrl._OnConfirm = HL.Method() << function(self)
    if not GameWorld.mapRegionManager:IsUnlockAllMistMapInLevel(GameWorld.worldInfo.curLevelId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_MAP_MIST_LOCKED_TOAST)
        return
    end
    GameInstance.player.inventory:UseMapDetectorItem(self.m_itemId, 1)
end

MapDetectPopUpCtrl._DoClose = HL.Method() << function(self)
    if self.m_mapInstId ~= "" then

        GameInstance.player.mapManager:RefreshCombinedUnitAndGetStaticMarkState(self.m_mapInstId)
        MapUtils.openMap(self.m_mapInstId, GameWorld.worldInfo.curLevelId, {
            forceDoNotShowDetail = true
        })
        self:Close()
    else
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end
end

MapDetectPopUpCtrl.OnMapDetectorShowMap = HL.Method(HL.Table) << function(self, args)
    local entityId = unpack(args)
    local success, instId
    for _, markType in ipairs(MapUtils.getDetectorMarkTypes()) do
        success, instId = GameInstance.player.mapManager:GetMapMarkInstId(markType, tostring(entityId))
        if success then
            break
        end
    end
    if instId then
        self.m_mapInstId = instId
    else
        logger.error("MapDetect:找不到对应的instId:".. entityId .. "对应的MapMark")
    end
    self:_DoClose()
end

MapDetectPopUpCtrl.OnMapDetectorEntityChange = HL.Method(HL.Table) << function(self, args)
    local entities, isVisible, isMapDetector = unpack(args)
    if not isMapDetector then
        return
    end
    local toastText
    local entityId = entities[CSIndex(1)]
    if not entityId then
        return
    end
    local instId, detectedType = self:_GetInstIdByEntityId(tostring(entityId))
    local toastKey = MapUtils.getDetectorToastKey(detectedType)
    toastText = string.format(Language[toastKey], entities.Count)
    Notify(MessageConst.SHOW_TOAST, toastText)
end

MapDetectPopUpCtrl._GetInstIdByEntityId = HL.Method(HL.String).Return(HL.String, GEnums.MarkType) << function(self, entityId)
    local success, instId, instIdType
    for _, markType in ipairs(MapUtils.getDetectorMarkTypes()) do
        success, instId = GameInstance.player.mapManager:GetMapMarkInstId(markType, tostring(entityId))
        if success then
            instIdType = markType
            break
        end
    end
    instId = instId or ""
    return instId, instIdType
end

HL.Commit(MapDetectPopUpCtrl)
