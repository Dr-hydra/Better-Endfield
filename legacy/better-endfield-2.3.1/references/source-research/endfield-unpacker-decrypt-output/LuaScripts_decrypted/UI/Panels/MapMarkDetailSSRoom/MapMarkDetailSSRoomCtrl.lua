
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailSSRoom

MapMarkDetailSSRoomCtrl = HL.Class('MapMarkDetailSSRoomCtrl', uiCtrl.UICtrl)






MapMarkDetailSSRoomCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailSSRoomCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local commonArgs = {
        markInstId = markInstId,
        bigBtnActive = true,
    }
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)

    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local roomId = markRuntimeData.roomId or Tables.spaceshipConst.controlCenterRoomId
    local spaceshipSystem = GameInstance.player.spaceship

    local isBuild = spaceshipSystem:IsRoomBuild(roomId)
    local isLocked = spaceshipSystem:IsRoomLocked(roomId)
    local haveLevel, unlockLevel = Tables.SpaceshipAreaUnlockNeedCenterLvTable:TryGetValue(roomId)
    if isBuild then
        self.view.lockRoot.gameObject:SetActive(false)
        self.view.unlockRoot.gameObject:SetActive(false)
        return
    end

    self.view.lockRoot.gameObject:SetActive(isLocked)
    self.view.unlockRoot.gameObject:SetActive(not isLocked)

    if isLocked then
        if not haveLevel then
            logger.error("未配置房间的解锁等级，请检查[飞船建设升级表-飞船控制中心]" .. roomId)
            return
        end
        self.view.lockText.text = string.format(Language.LUA_SSROOM_MAP_DETAIL_LOCKED, unlockLevel)
    else
        self.view.unlockText.text = Language.LUA_SSROOM_MAP_DETAIL_UNLOCKED
    end
end


HL.Commit(MapMarkDetailSSRoomCtrl)
