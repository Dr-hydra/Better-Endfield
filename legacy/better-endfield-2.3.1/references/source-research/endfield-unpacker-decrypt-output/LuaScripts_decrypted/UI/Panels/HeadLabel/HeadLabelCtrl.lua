local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HeadLabel

local HeadLabelType = CS.Beyond.UI.UIHeadLabel.HeadLabelType
local VisibleSource = CS.Beyond.UI.UIHeadLabel.VisibleSource
HeadLabelCtrl = HL.Class('HeadLabelCtrl', uiCtrl.UICtrl)

local CACHE_COUNT = 30







HeadLabelCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.UPDATE_HEAD_LABEL_ICON] = '_UpdateHeadLabelIcon',

}

HeadLabelCtrl.m_labelObjDict = HL.Field(HL.Table)

HeadLabelCtrl.m_labelLogicIdDict = HL.Field(HL.Table)

HeadLabelCtrl.m_labelObjPool = HL.Field(HL.Table)



HeadLabelCtrl._OnLevelPreStart = HL.StaticMethod() << function()
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, {}, false)
    ctrl:PreCacheHeadLabels()
end


HeadLabelCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_labelObjDict = {}
    self.m_labelObjPool = {}
    self.m_labelLogicIdDict = {}
end





HeadLabelCtrl.OnClose = HL.Override() << function(self)
    if self.m_labelObjDict ~= nil then
        for _, v in pairs(self.m_labelObjDict) do
            v.headLabel:Clear()
            GameObject.Destroy(v.headLabel.gameObject)
        end
        self.m_labelObjDict = nil
    end
    if self.m_labelObjPool ~= nil then
        for _, v in ipairs(self.m_labelObjPool) do
            GameObject.Destroy(v.headLabel.gameObject)
        end
        self.m_labelObjPool = nil
    end
end

HeadLabelCtrl.PreCacheHeadLabels = HL.Method() << function(self)
    if #self.m_labelObjPool <= 0 then
        for i = 1, CACHE_COUNT do
            local headLabel = self:_CreateHeadLabel()
            local csHeadLabel = headLabel.headLabel
            csHeadLabel.gameObject:SetActive(false)
            table.insert(self.m_labelObjPool, headLabel)
        end
    end
end

HeadLabelCtrl.GetEntityLabelIconPos = HL.Method(HL.Number).Return(HL.Any) << function(self, logicId)
    if self.m_labelLogicIdDict[logicId] == nil then
        return nil
    end

    return self.m_labelLogicIdDict[logicId].iconHolder.position
end

HeadLabelCtrl._OnAddHeadLabel = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, args, false)
    local targetObject = unpack(args)
    ctrl:_AddHeadLabel(targetObject)
end

HeadLabelCtrl._OnEnvTalkChanged = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, args, false)
    ctrl:ShowEnvTalk(args)
end

HeadLabelCtrl._OnStateChanged = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    local npc = entity.npc

    if not npc then
        logger.info("_OnStateChanged npc nil")
        return
    end

    local hasNpc, data = CS.Beyond.Gameplay.Core.NpcManager.TryGetValue(npc.npcId)
    ctrl:ShowState(data.headLabelStateData, nil, entity)
end

HeadLabelCtrl._OnGiftChanged = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    local npc = entity.npc

    if not npc then
        logger.info("_OnStateChanged npc nil")
        return
    end

    local hasNpc, data = CS.Beyond.Gameplay.Core.NpcManager.TryGetValue(npc.npcId)
    ctrl:UpdateGift(data.headLabelStateData, nil, entity)
end

HeadLabelCtrl._AddHeadLabel = HL.Method(HL.Any) << function(self, targetObject)
    local needInit = false
    if self.m_labelObjDict[targetObject] == nil then
        self.m_labelObjDict[targetObject] = self:_TryGetHeadLabel()
        needInit = true
    end

    local headLabel = self.m_labelObjDict[targetObject]
    local csHeadLabel = headLabel.headLabel
    csHeadLabel.gameObject:SetActive(true)
    headLabel.gameObject:SetLayerRecursive(UIConst.WORLD_UI_LAYER)
    headLabel.canvas.enabled = true

    csHeadLabel:SetTarget(targetObject)
    self:_RefreshNpcInfo(headLabel, targetObject)

    if needInit then
        self:_ClearStateNode(headLabel)
        self:_ClearEnvTalk(headLabel)
    end

    self.m_labelLogicIdDict[csHeadLabel.entityLogicId] = csHeadLabel
end

HeadLabelCtrl._TryGetMissionHeadLabelIcon = HL.StaticMethod(HL.String).Return(HL.Any) << function(npcId)
    local res, proxy = GameWorld.npcProxyMgr:TryGetNpcProxyByNpcId(npcId)
    local proxyId = ""
    if res then
        proxyId = proxy.npcRuntimeProxyData.proxyId
    end
    local res, icon = GameInstance.player.mission:GetNpcHeadIcon(npcId, proxyId)
    return icon
end

HeadLabelCtrl._RefreshNpcHeadLabel = HL.Method(HL.Table, HL.String, HL.Any) << function(self, headLabel, npcId, npc)
    local hasNpc, data = CS.Beyond.Gameplay.Core.NpcManager.TryGetValue(npcId)
    if hasNpc then

        if (string.isEmpty(data.title)) then
            headLabel.title.gameObject:SetActive(false)
        else
            headLabel.title.gameObject:SetActive(true)
            headLabel.title.text = data.title
        end


        if (string.isEmpty(data.name)) then
            headLabel.name.gameObject:SetActive(false)
        else
            headLabel.name.gameObject:SetActive(true)
            headLabel.name.text = data.name
        end

        local csHeadLabel = headLabel.headLabel

        csHeadLabel:SetSubRootVisible(HeadLabelType.Text, VisibleSource.System, true, true)


        local missionHeadLabelIcon = HeadLabelCtrl._TryGetMissionHeadLabelIcon(npcId)
        local sprite

        if not string.isEmpty(missionHeadLabelIcon) then
            sprite = self:LoadSprite(UIConst.UI_SPRITE_HEAD_LABEL_ICON, missionHeadLabelIcon)
        elseif not string.isEmpty(data.headLabelIcon) then
            sprite = self:LoadSprite(UIConst.UI_SPRITE_HEAD_LABEL_ICON, data.headLabelIcon)
        end

        if sprite ~= nil then
            headLabel.icon.gameObject:SetActive(true)
            headLabel.bg.gameObject:SetActive(true)
            headLabel.icon.sprite = sprite
            headLabel.icon:SetNativeSize()
        else
            headLabel.icon.gameObject:SetActive(false)
            headLabel.bg.gameObject:SetActive(false)
        end


        headLabel.iconGift.gameObject:SetActive(false)

        csHeadLabel.missionHeadLabelIcon = missionHeadLabelIcon
        csHeadLabel.headLabelIcon = data.headLabelIcon

        self:ShowState(data.headLabelStateData, headLabel, npc)
    else
        logger.warn("_RefreshNpcInfo warn, data nil, npcId: " .. npcId)

        local csHeadLabel = headLabel.headLabel
        csHeadLabel:SetSubRootVisible(HeadLabelType.Text, VisibleSource.System, false, true)
    end
end

HeadLabelCtrl._RefreshNpcInfo = HL.Method(HL.Table, HL.Any) << function(self, headLabel, target)
    logger.info("_RefreshNpcInfo, target: " .. tostring(target) .. ", headLabel: " .. tostring(headLabel))
    if not target or not headLabel then
        logger.error("_RefreshNpcInfo error, target: " .. tostring(target) .. ", headLabel: " .. tostring(headLabel))
        return
    end

    if (target.objectType == Const.ObjectType.Npc and (target.npc and not target.npc.hideHeadLabel)) then
        local entity = target
        local npc = entity.npc
        if not npc then
            logger.info("_RefreshNpcInfo npc nil")
            return
        end

        self:_RefreshNpcHeadLabel(headLabel, npc.npcId, entity)

    else
        headLabel.icon.gameObject:SetActive(false)
    end
end

HeadLabelCtrl._UpdateHeadLabelIcon = HL.Method(HL.Any) << function(self, args)

    local targetObject = unpack(args)
    if targetObject then
        local headLabel = self.m_labelObjDict[targetObject]
        if headLabel then
            local npc = targetObject.npc
            if npc then
                self:_RefreshNpcHeadLabel(headLabel, npc.npcId, targetObject)
            end
        end
    end
end

HeadLabelCtrl._CreateHeadLabel = HL.Method().Return(HL.Table) << function(self)
    local prefab = self.view.config.CHAR_HEAD_LABEL
    if DeviceInfo.usingTouch then
        prefab = self.view.config.CHAR_HEAD_LABEL_MOBILE
    end

    if not UNITY_EDITOR then

        local barRoot = prefab.transform:Find("BarRoot")
        local childrenCount = barRoot.transform.childCount
        for i = 0, childrenCount - 1 do
            local child = barRoot.transform:GetChild(i)
            if child.name ~= "TxtRoot" then
                child.gameObject:SetActive(false)
            end
        end
    end

    local obj = self:_CreateWorldGameObject(prefab)
    local result = Utils.wrapLuaNode(obj)
    return result
end

HeadLabelCtrl._TryGetHeadLabel = HL.Method().Return(HL.Table) << function(self)
    if self.m_labelObjPool ~= nil and #self.m_labelObjPool > 0 then
        local result = self.m_labelObjPool[#self.m_labelObjPool]
        table.remove(self.m_labelObjPool, #self.m_labelObjPool)
        return result
    else
        return self:_CreateHeadLabel()
    end
end

HeadLabelCtrl._OnRemoveHeadLabel = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = HeadLabelCtrl.AutoOpen(PANEL_ID, args, false)
    local entity = unpack(args)
    if ctrl.m_labelObjDict[entity] ~= nil then
        local cell = ctrl.m_labelObjDict[entity]
        cell.headLabel:Clear()
        if #ctrl.m_labelObjPool < CACHE_COUNT then
            cell.gameObject:SetLayerRecursive(UIConst.HIDE_LAYER)
            cell.canvas.enabled = false
            table.insert(ctrl.m_labelObjPool, cell)
        else
            GameObject.Destroy(cell.headLabel.gameObject)
        end

        ctrl.m_labelObjDict[entity] = nil
    end
end

HeadLabelCtrl._ClearStateNode = HL.Method(HL.Table) << function(self, headLabel)
    local stateNode = headLabel.stateNode
    stateNode.gameObject:SetActive(false)
end

HeadLabelCtrl._ClearEnvTalk = HL.Method(HL.Table) << function(self, headLabel)
    local csHeadLabel = headLabel.headLabel
    local bubbleNode = headLabel.bubbleNode

    if not csHeadLabel or not bubbleNode then
        return
    end

    if bubbleNode and bubbleNode.gameObject.activeInHierarchy then
        bubbleNode:PlayWithTween("headlabel_bubble_out", function()
            csHeadLabel:SetSubRootVisible(HeadLabelType.Bubble, VisibleSource.System, false, false)

            csHeadLabel:ClearEmojis()
            csHeadLabel:SetVisibleDirty()
        end)
    else
        csHeadLabel:SetSubRootVisible(HeadLabelType.Bubble, VisibleSource.System, false, false)

        csHeadLabel:ClearEmojis()
        csHeadLabel:SetVisibleDirty()
    end
end

HeadLabelCtrl.ShowEnvTalk = HL.Method(HL.Table) << function(self, args)
    local show, targetObject, envTalkSingleData, gender = unpack(args)
    if targetObject == nil then
        logger.error("ShowEnvTalk npc nil, npcId: " .. envTalkSingleData.npcId .. "!!!")
        return
    end

    local headLabel = self.m_labelObjDict[targetObject]


    if not show and not headLabel then
        return
    end

    if headLabel == nil then
        self:_AddHeadLabel(targetObject)
        headLabel = self.m_labelObjDict[targetObject]
    end

    local csHeadLabel = headLabel.headLabel
    local bubbleNode = headLabel.bubbleNode

    if not csHeadLabel or not bubbleNode then
        return
    end

    show = show and envTalkSingleData and (not string.isEmpty(envTalkSingleData.text) or not string.isEmpty(envTalkSingleData.emojiId))

    bubbleNode:ClearTween()
    if show then
        if not string.isEmpty(envTalkSingleData.text) then
            local text = UIUtils.resolveTextPlayerName(envTalkSingleData.text)
            text = UIUtils.resolveTextGenderWithNpcGender(text, gender)
            headLabel.bubbleText:SetAndResolveTextStyle(text)
            headLabel.bubble.gameObject:SetActive(true)
        else
            headLabel.bubble.gameObject:SetActive(false)
        end
        if not string.isEmpty(envTalkSingleData.emojiId) then
            local path = UIConst.EMOJI_PREFAB_PATH:format(envTalkSingleData.emojiId)
            local prefab = self.loader:LoadGameObject(path)
            if prefab then
                UIUtils.addChild(headLabel.emoji.transform, prefab)
            else
                logger.error("No emoji", path, envTalkSingleData.emojiId, envTalkSingleData.envTalkId)
            end
            headLabel.emoji.gameObject:SetActive(true)
        else
            headLabel.emoji.gameObject:SetActive(false)
        end
        csHeadLabel:SetSubRootVisible(HeadLabelType.Bubble, VisibleSource.System, true, false)

        if bubbleNode.gameObject.activeInHierarchy then
            bubbleNode:PlayInAnimation()
        end
    else
        self:_ClearEnvTalk(headLabel)
    end

    csHeadLabel:SetVisibleDirty()
end

HeadLabelCtrl.ShowState = HL.Method(HL.Any, HL.Opt(HL.Any, HL.Any)) << function(self, data, headLabel, targetObject)
    if type(data) == "table" then
        data = unpack(data)
    end

    if not data then
        if headLabel then
            headLabel.stateNode.gameObject:SetActive(false)
        end
        return
    end

    local show = data.show
    local text = data.text
    local icon = data.icon
    local percent = data.percent
    local colorNum = data.color or 1
    local hasGift = data.hasGift

    if not headLabel then
        if targetObject == nil then
            logger.error("ShowState target nil!!! data: ", data)
            return
        end
        headLabel = self.m_labelObjDict[targetObject]

        if not show and not headLabel then
            return
        end

        if headLabel == nil then
            self:_AddHeadLabel(targetObject)
            headLabel = self.m_labelObjDict[targetObject]
        end
    end

    local csHeadLabel = headLabel.headLabel
    local stateNode = headLabel.stateNode

    if not csHeadLabel or not stateNode then
        return
    end

    self:UpdateGift(data, headLabel, targetObject)

    if show then
        if not string.isEmpty(text) then
            stateNode.text.gameObject:SetActive(true)
            stateNode.text:SetAndResolveTextStyle(text)
        else
            stateNode.text.gameObject:SetActive(false)
        end

        if not string.isEmpty(icon) then
            stateNode.icon:LoadSprite(UIConst.UI_SPRITE_HIDE_LABEL_STATE_ICON, icon)
            stateNode.icon.gameObject:SetActive(true)
        else
            stateNode.icon.gameObject:SetActive(false)
        end

        local colorName = string.format("STATE_COLOR_%d", colorNum)
        local color = headLabel.config[colorName]
        stateNode.line.color = color

        stateNode.line.fillAmount = percent
        stateNode.gameObject:SetActive(true)
    else
        self:_ClearStateNode(headLabel)
    end
end

HeadLabelCtrl.UpdateGift = HL.Method(HL.Any, HL.Opt(HL.Any, HL.Any)) << function(self, data, headLabel, targetObject)
    if type(data) == "table" then
        data = unpack(data)
    end

    if not data then
        if headLabel then
            headLabel.iconGift.gameObject:SetActive(false)
        end
        return
    end

    if not headLabel and targetObject then
        headLabel = self.m_labelObjDict[targetObject]
    end

    if headLabel then
        local hasSpaceshipGift = true
        local hasGift = data.hasGift
        if hasGift and targetObject and targetObject.npcInteractCom then
            hasSpaceshipGift = targetObject.npcInteractCom:HasSpaceshipGift()
        end

        headLabel.iconGift.gameObject:SetActive(hasGift and hasSpaceshipGift)
    end
end

HL.Commit(HeadLabelCtrl)
