local UIUtils = {}

function UIUtils.initLuaCustomConfig(self)
    self.luaCustomConfig = self.luaCustomConfig or self.transform:GetComponent("LuaCustomConfig")
    if not self.luaCustomConfig then
        return
    end

    self.config = {}

    self.config.HasValue = function(_, key)
        local flag = self.luaCustomConfig.itemDict:TryGetValue(key)
        if not flag then
            return false
        end
        return true
    end

    if UNITY_EDITOR and CS.Beyond.DebugDefines.realTimeLuaCustomConfig then

        setmetatable(self.config, {
            __index = function(_, key)
                local flag, item = self.luaCustomConfig.itemDict:TryGetValue(key)
                if flag then

                    local valueType = CS.Beyond.Lua.LuaCustomConfig.ValueType
                    if item.valueType == valueType.Bool then
                        return item.boolValue
                    elseif item.valueType == valueType.Int then
                        return item.intValue
                    elseif item.valueType == valueType.Float then
                        return item.floatValue
                    elseif item.valueType == valueType.String then
                        return item.stringValue
                    elseif item.valueType == valueType.Vector2 then
                        return item.vector2Value
                    elseif item.valueType == valueType.Vector3 then
                        return item.vector3Value
                    elseif item.valueType == valueType.Vector4 then
                        return item.vector4Value
                    elseif item.valueType == valueType.Color then
                        return item.colorValue
                    elseif item.valueType == valueType.Lua then
                        return lume.dostring("return " .. item.luaValue)
                    elseif item.valueType == valueType.FMODEvent then
                        return item.fmodEventValue
                    elseif item.valueType == valueType.GameObject then
                        return item.gameObjectValue
                    elseif item.valueType == valueType.RectTransform then
                        return item.rectTransformValue
                    elseif item.valueType == valueType.AnimationCurve then
                        return item.curveValue
                    elseif item.valueType == valueType.LayerMask then
                        return item.layerMaskValue
                    elseif item.valueType == valueType.Material then
                        return item.material
                    elseif item.valueType == valueType.Enum then
                        local exception = string.format(
                            "return CS.%s.__CastFrom(%d)",
                            item.enumTypeFullName,
                            item.enumValue
                        )
                        local enumValue = lume.dostring(exception)
                        return enumValue
                    end
                end
                logger.error("无法获取 LuaCustomConfig 配置项", key, self.luaCustomConfig.transform:PathFromRoot())
            end
        })
    else

        local componentConfig = {}
        self.luaCustomConfig:InitConfigTable(componentConfig)
        setmetatable(self.config, {
            __index = function(_, key)
                local componentValue = componentConfig[key]
                if componentValue ~= nil then

                    return componentValue
                end

                logger.error("无法获取 LuaCustomConfig 配置项", key, self.luaCustomConfig.transform:PathFromRoot())
            end
        })
    end

    getmetatable(self.config).__newindex = function()
        logger.error("请在面板上配置 LuaCustomConfig，勿在代码中修改 view.config 中的值！", self.luaCustomConfig.transform:PathFromRoot())
    end
end

function UIUtils.genCachedCellFunction(list, onMiss, isTitle)
    onMiss = onMiss or function(obj)
        return Utils.wrapLuaNode(obj)
    end

    local cache = {}
    local getCell = function(object)
        local cell = cache[object]
        if not cell then
            cell = onMiss(object)
            cache[object] = cell
        end
        return cell
    end


    return function(object)
        if not object then
            return
        end
        if type(object) == "number" then
            if not list then
                logger.error("genCachedCellFunction fail", list)
                return
            end
            local luaIndex = object

            if isTitle then
                object = list:GetGroupTitle(CSIndex(luaIndex))
            else
                object = list:Get(CSIndex(luaIndex))
            end
        end
        if object then
            return getCell(object)
        end
    end
end

function UIUtils.addChild(parent, prefab, keepScale)
    if not prefab or IsNull(prefab.gameObject) then
        logger.error("Invalid prefab", parent.transform:PathFromRoot())
    end
    local item = {}
    item.gameObject = CSUtils.CreateObject(prefab.gameObject, parent.gameObject)
    item.transform = item.gameObject.transform:GetComponent("RectTransform")
    if not keepScale then
        item.transform.localScale = Vector3.one
    end
    return item
end

local UIListCacheClass = require_ex("Common/Utils/UI/UIListCache").UIListCache


function UIUtils.genCellCache(itemTemplate, wrapFunction, parent)
    return UIListCacheClass(itemTemplate, wrapFunction, parent)
end

local UIGoCacheClass = require_ex("Common/Utils/UI/UIGoCache").UIGoCache


function UIUtils.genGoCache(goTemplate, wrapFunction, parent)
    return UIGoCacheClass(goTemplate, wrapFunction, parent)
end

function UIUtils.getStandardScreenX(x)
    return x / Screen.width * UIConst.CANVAS_DEFAULT_WIDTH
end

function UIUtils.getStandardScreenY(y)
    return y / Screen.height * UIConst.CANVAS_DEFAULT_HEIGHT
end

function UIUtils.getNormalizedScreenX(x)
    return x / Screen.width
end

function UIUtils.getNormalizedScreenY(y)
    return y / Screen.height
end

function UIUtils.isInputEventScopeValid(scope)
    scope = scope or Types.EInputBindingScope.IncludeStandalone
    local isValid
    if scope == Types.EInputBindingScope.EditorOnly then
        isValid = UNITY_EDITOR
    elseif scope == Types.EInputBindingScope.IncludeDev then
        isValid = UNITY_EDITOR or DEVELOPMENT_BUILD
    elseif scope == Types.EInputBindingScope.IncludeStandalone then
        isValid = UNITY_EDITOR or DEVELOPMENT_BUILD or UNITY_STANDALONE
    end
    return isValid
end

function UIUtils.bindInputPlayerAction(actionId, callback, groupId)
    return InputManagerInst:CreateBinding(actionId, callback, groupId)
end





function UIUtils.bindInputEvent(key, action, modifyKeys, timing, groupId)
    return InputManagerInst:CreateBinding(key, modifyKeys or "", timing or InputTimingType.OnClick, action, groupId or UIManager.persistentInputBindingKey)
end


function UIUtils.getGamepadKeyImageText(keyCode, isLongPress, scale, color)
    local keyStr = InputManager.GetStringByGamepadKeyCode(keyCode)
    local keyIconPath = InputManager.GetGamepadKeyIconPath(keyStr, isLongPress, false)
    if color then
        local colorStr = UIUtils.getStringByColor(color)
        return string.format("<image=\"%s\" scale=%.2f color=%s>", keyIconPath, scale, colorStr)
    end
    return string.format("<image=\"%s\" scale=%.2f>", keyIconPath, scale)
end



function UIUtils.setAsNaviTarget(targetSelectable)
    logger.error("[UIUtils] setAsNaviTarget 已废弃，请使用 self:SetNaviTarget(selectable)")
    InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(targetSelectable)
end





function UIUtils.setAsNaviTargetInSilentModeIfPhaseIsTop(targetNaviGroup, targetSelectable, needTopPhaseId)
    logger.error("[UIUtils] setAsNaviTargetInSilentModeIfPhaseIsTop 已废弃，请使用 self:SetNaviTarget(selectable)")
    local key = PhaseManager:GetPhaseDummyNaviLayerName(needTopPhaseId)
    if not string.isEmpty(key) then
        local needTopDummyLayer = LuaSystemManager.dummyNaviLayerSystem:GetDummyNaviLayerByKey(key)
        if needTopDummyLayer ~= nil then
            InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(targetSelectable, needTopDummyLayer)
        end
    else
        InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(targetSelectable)
    end
end





function UIUtils.setAsNaviTargetInSilentModeIfNecessary(targetNaviGroup, targetSelectable, needTopDummyLayerKey)
    logger.error("[UIUtils] setAsNaviTargetInSilentModeIfNecessary 已废弃，请使用 self:SetNaviTarget(selectable)")
    if string.isEmpty(needTopDummyLayerKey) then
        InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(targetSelectable)
        return
    end
    local needTopDummyLayer = LuaSystemManager.dummyNaviLayerSystem:GetDummyNaviLayerByKey(needTopDummyLayerKey)
    if needTopDummyLayer ~= nil then
        InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(targetSelectable, needTopDummyLayer)
    end
end



function UIUtils.changeAndTrySetNaviBindingType(naviGroup, naviBindingType)
    InputManagerInst.controllerNaviManager:ChangeAndTrySetNaviBindingType(naviGroup, naviBindingType)
end




function UIUtils.initUIDragHelper(uiDragItem, info)
    if uiDragItem.luaTable then
        local dragHelper = uiDragItem.luaTable[1]
        dragHelper:RefreshInfo(info)
        return dragHelper
    else
        local DragHelperClass = require_ex("Common/Utils/UI/UIDragHelper")
        return DragHelperClass(uiDragItem, info)
    end
end


function UIUtils.initUIDropHelper(uiDropItem, info)
    if uiDropItem.luaTable then
        local dropHelper = uiDropItem.luaTable[1]
        if BEYOND_DEBUG_COMMAND then


            if not dropHelper or type(dropHelper) ~= "userdata" then
                logger.error("InValid dropHelper", uiDropItem.luaTable, uiDropItem.transform:PathFromRoot())
            end
        end
        dropHelper:RefreshInfo(info)
        return dropHelper
    else
        local DropHelperClass = require_ex("Common/Utils/UI/UIDropHelper")
        return DropHelperClass(uiDropItem, info)
    end
end


function UIUtils.isTypeDropValid(dragHelper, acceptTypes)
    local isSourceValid = not acceptTypes.sources or lume.find(acceptTypes.sources, dragHelper.source)
    if not isSourceValid then
        return false
    end
    local isTypeValid = not acceptTypes.types or lume.find(acceptTypes.types, dragHelper.type)
    if not isTypeValid then
        return false
    end
    return true
end

function UIUtils.playItemDragAudio(itemId)
    local res, audioData = Tables.audioItemDragAndDrop:TryGetValue(itemId)
    if res and not string.isEmpty(audioData.audioDrag) then
        AudioAdapter.PostEvent(audioData.audioDrag)
        return
    end
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if itemData then
        local _, audioTypeData = Tables.audioItemTypeDragAndDrop:TryGetValue(itemData.showingType)
        if audioTypeData ~= nil and not string.isEmpty(audioTypeData.audioDrag) then
            AudioAdapter.PostEvent(audioTypeData.audioDrag)
        end
    end
end

function UIUtils.playItemDropAudio(itemId)
    if string.isEmpty(itemId) then
        return
    end
    local res, audioData = Tables.audioItemDragAndDrop:TryGetValue(itemId)
    if res and not string.isEmpty(audioData.audioDrop) then
        AudioAdapter.PostEvent(audioData.audioDrop)
        return
    end
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if itemData then
        local _, audioTypeData = Tables.audioItemTypeDragAndDrop:TryGetValue(itemData.showingType)
        if audioTypeData ~= nil and not string.isEmpty(audioTypeData.audioDrop) then
            AudioAdapter.PostEvent(audioTypeData.audioDrop)
        end
    end
end




function UIUtils.screenPointToUI(screenPos, uiCamera, canvasRect)
    canvasRect = canvasRect or UIManager.uiCanvasRect
    local isInside, uiPos = Unity.RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, uiCamera)
    return uiPos, isInside
end

function UIUtils.objectPosToUI(pos, uiCamera, canvasRect)
    local screenPos = CameraManager.mainCamera:WorldToScreenPoint(pos)
    if screenPos.z < 0 then
        screenPos.x = -screenPos.x
        screenPos.y = -screenPos.y
    end
    return UIUtils.screenPointToUI(Vector2(screenPos.x, screenPos.y), uiCamera, canvasRect)
end

function UIUtils.getUIRectOfRectTransform(rectTransform, uiCamera)

    local rect = CSUtils.RectTransformToScreenRect(rectTransform, uiCamera)
    rect.y = Screen.height - rect.yMax
    local canvasRect = UIManager.uiCanvasRect.rect
    local scaleX = canvasRect.width / Screen.width
    local scaleY = canvasRect.height / Screen.height
    return Unity.Rect(rect.x * scaleX, rect.y * scaleY, rect.size.x * scaleX, rect.size.y * scaleY)
end

function UIUtils.addRectSizeKeepCenter(rect, addWidth, addHeight)
    local center = rect.center
    rect.size = rect.size + Vector2(addWidth or 0, addHeight or 0)
    rect.center = center
    return rect
end


function UIUtils.getSpritePath(path, name)
    if name then

        path = path .. "/" .. name
    end
    return UIConst.UI_SPRITE_PATH:format(path)
end

function UIUtils.getSpriteDevPath(path, name)
    if name then

        path = path .. "/" .. name
    end
    return UIConst.UI_SPRITE_DEV_PATH:format(path)
end



function UIUtils.isScreenPosInRectTransform(pos, rectTransform, uiCamera)
    return CS.Beyond.UI.UIUtils.IsScreenPosInRectTransform(pos, rectTransform, uiCamera)
end


function UIUtils.getTransformScreenRect(transform, uiCamera)
    local bounds = CSUtils.GetRectTransformBounds(transform)
    local min = bounds.min
    local size = bounds.size
    if uiCamera then
        min = uiCamera:WorldToScreenPoint(min)
        local max = uiCamera:WorldToScreenPoint(bounds.max)
        size = max - min
    end
    return Unity.Rect(min.x, Screen.height - (min.y + size.y), size.x, size.y)
end


function UIUtils.getRectTransformCenterPosition(rectTransform)
    return CSUtils.GetRectTransformCenterPosition(rectTransform)
end






function UIUtils.updateTipsPosition(contentRectTrans, targetTransform, canvasRectTrans, uiCamera, posType, padding, xOffset)
    if IsNull(targetTransform) then
        contentRectTrans.anchoredPosition = Vector2.zero
        return
    end
    local targetScreenRect = UIUtils.getTransformScreenRect(targetTransform, uiCamera)
    UIUtils.updateTipsPositionWithScreenRect(contentRectTrans, targetScreenRect, canvasRectTrans, uiCamera, posType, padding, xOffset)
end


function UIUtils.updateTipsPositionWithScreenRect(contentRectTrans, targetScreenRect, canvasRectTrans, uiCamera, posType, padding, xOffset)
    posType = posType or UIConst.UI_TIPS_POS_TYPE.RightDown
    LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTrans)
    if BEYOND_DEBUG then
        if contentRectTrans.pivot ~= Vector2(0.5, 0.5) then
            logger.error(string.format("Tips位置计算错误: [%s] 的锚点不在中心", contentRectTrans:PathFromRoot()))
        end
    end
    local width = contentRectTrans.rect.width
    local height = contentRectTrans.rect.height
    local canvasSize = canvasRectTrans.rect.size
    local oriScreenSize = Vector2(Screen.width, Screen.height)
    local xRation = oriScreenSize.x / canvasSize.x
    local yRation = oriScreenSize.y / canvasSize.y
    local widthInScreen = width * xRation
    local heightInScreen = height * yRation
    local halfHeightInScreen = heightInScreen / 2
    local halfWidthInScreen = widthInScreen / 2

    padding = padding or {}

    local paddingTop = (padding.top or 0) * yRation
    local paddingLeft = (padding.left or 0) * xRation
    local paddingRight = (padding.right or 0) * xRation
    local paddingBottom = (padding.bottom or 0) * yRation

    local screenSize = Vector2(oriScreenSize.x - (paddingLeft + paddingRight), oriScreenSize.y - (paddingTop + paddingBottom))
    targetScreenRect.x = targetScreenRect.x - paddingLeft
    targetScreenRect.y = targetScreenRect.y - paddingTop


    local screenPos = Vector2(0, 0)


    local finalXPos, finalYPos


    if posType == UIConst.UI_TIPS_POS_TYPE.MidBottom then

        local verticalSpaceEnough = true
        local downHeight = screenSize.y - targetScreenRect.yMax
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMax + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            local upHeightEnough = targetScreenRect.yMin >= heightInScreen
            if upHeightEnough then
                screenPos.y = targetScreenRect.yMin - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
            else
                screenPos.y = targetScreenRect.center.y
                verticalSpaceEnough = false
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Mid
            end
        end

        local rightWidth = screenSize.x - targetScreenRect.xMax
        local leftWidth = targetScreenRect.xMin
        if verticalSpaceEnough then
            if rightWidth >= halfWidthInScreen and leftWidth >= halfWidthInScreen then

                screenPos.x = targetScreenRect.center.x
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
            else
                if leftWidth < halfWidthInScreen then

                    screenPos.x = targetScreenRect.center.x + halfWidthInScreen - leftWidth
                    finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
                else

                    screenPos.x = targetScreenRect.center.x - (halfWidthInScreen - rightWidth)
                    finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
                end
            end
        else
            if leftWidth >= widthInScreen then
                screenPos.x = targetScreenRect.xMin - halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
            else
                screenPos.x = targetScreenRect.xMax + halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
            end
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.LeftTop then

        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local leftWidth = targetScreenRect.xMin
        if leftWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        else
            screenPos.x = 0
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.LeftTopOrRightTop then

        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local leftWidth = targetScreenRect.xMin
        if leftWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        else
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.RightTopOrLeftTop then

        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.RightTop then

        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else
            screenPos.x = screenSize.x - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.RightDown then

        local downHeight = screenSize.y - targetScreenRect.yMax
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMax + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            local upHeight = targetScreenRect.yMin
            if upHeight >= downHeight then

                screenPos.y = targetScreenRect.yMin - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
            else
                screenPos.y = screenSize.y - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
            end
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else
            local leftWidth = targetScreenRect.xMin
            if leftWidth >= widthInScreen then
                screenPos.x = targetScreenRect.xMin - halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
            else
                screenPos.x = screenSize.x - halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
            end
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.LeftDown then

        local downHeight = screenSize.y - targetScreenRect.yMax
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMax + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            local upHeight = targetScreenRect.yMin
            if upHeight >= downHeight then

                screenPos.y = targetScreenRect.yMin - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
            else
                screenPos.y = screenSize.y - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
            end
        end


        local leftWidth = targetScreenRect.xMin
        if leftWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        else
            screenPos.x = 0
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.MidTop then

        if targetScreenRect.yMin >= heightInScreen then
            screenPos.y = targetScreenRect.yMin - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        else
            local downHeight = screenSize.y - targetScreenRect.yMax
            if downHeight >= heightInScreen then
                screenPos.y = targetScreenRect.yMax + halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
            else
                screenPos.y = halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
            end
        end


        screenPos.x = targetScreenRect.center.x
        finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
    elseif posType == UIConst.UI_TIPS_POS_TYPE.LeftMid then

        local downHeight = screenSize.y - targetScreenRect.center.y
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.center.y
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Mid
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local leftWidth = targetScreenRect.xMin
        if leftWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        else
            screenPos.x = 0
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.RightMid then

        local downHeight = screenSize.y - targetScreenRect.center.y
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.center.y
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Mid
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else
            screenPos.x = screenSize.x - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.GuideTips then
        local outDistance = UIConst.UI_GUIDE_OUT_OF_SCREEN_DISTANCE
        if targetScreenRect.xMin < -outDistance or targetScreenRect.xMax - screenSize.x > outDistance or
            targetScreenRect.yMin < -outDistance or targetScreenRect.yMax - screenSize.y > outDistance then

            screenPos.x = targetScreenRect.x
            screenPos.y = targetScreenRect.y
            screenPos = screenPos - oriScreenSize / 2
            screenPos.y = -screenPos.y
            local canvasPos = Vector2(screenPos.x / xRation, screenPos.y / yRation)
            contentRectTrans.anchoredPosition = canvasPos
            return targetScreenRect.xMin < 0 and UIConst.UI_TIPS_X_POS_TYPE.Left or UIConst.UI_TIPS_X_POS_TYPE.Right,
                targetScreenRect.yMin < 0 and UIConst.UI_TIPS_Y_POS_TYPE.Top or UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        end


        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local leftWidth = targetScreenRect.xMin
        if leftWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMin - halfWidthInScreen - xOffset
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        else
            local rightWidth = screenSize.x - targetScreenRect.xMax
            if rightWidth >= widthInScreen then
                screenPos.x = targetScreenRect.xMax + halfWidthInScreen + xOffset
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
            else

                screenPos.x = targetScreenRect.center.x
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
                if targetScreenRect.yMin >= (screenSize.y - targetScreenRect.yMax) then
                    screenPos.y = targetScreenRect.yMin - halfHeightInScreen
                    finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
                else
                    screenPos.y = targetScreenRect.yMax + halfHeightInScreen
                    finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
                end
            end
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.FacTopViewOption then
        if targetScreenRect.center.y >= screenSize.y then
            screenPos.x = targetScreenRect.center.x
            screenPos.y = targetScreenRect.yMin - halfHeightInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        elseif targetScreenRect.center.y <= 0 then
            screenPos.x = targetScreenRect.center.x
            screenPos.y = targetScreenRect.yMax + halfHeightInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else

            local downHeight = screenSize.y - targetScreenRect.center.y
            if downHeight >= heightInScreen then
                screenPos.y = targetScreenRect.center.y
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Mid
            else
                screenPos.y = screenSize.y - halfHeightInScreen
                finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
            end

            local rightWidth = screenSize.x - targetScreenRect.xMax
            if rightWidth >= widthInScreen then
                screenPos.x = targetScreenRect.xMax + halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
            else
                screenPos.x = screenSize.x - halfWidthInScreen
                finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
            end
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.FacTopViewBuildActionIcons then
        if targetScreenRect.yMin >= heightInScreen then
            screenPos.y = targetScreenRect.yMin - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        else
            screenPos.y = targetScreenRect.yMax + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        end
        screenPos.x = targetScreenRect.center.x
        finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Mid
    elseif posType == UIConst.UI_TIPS_POS_TYPE.FacSmartAlertTop then
        screenPos.y = targetScreenRect.yMin - halfHeightInScreen
        screenPos.x = targetScreenRect.center.x
    elseif posType == UIConst.UI_TIPS_POS_TYPE.LeftAlignBottom then
        screenPos.x = targetScreenRect.xMin + halfWidthInScreen
        screenPos.y = targetScreenRect.yMax + halfHeightInScreen
        finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
    elseif posType == UIConst.UI_TIPS_POS_TYPE.AdaptiveRightTop then

        local downHeight = screenSize.y - targetScreenRect.yMin
        if downHeight >= heightInScreen then
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        else
            screenPos.y = screenSize.y - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else

            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    elseif posType == UIConst.UI_TIPS_POS_TYPE.DailyAbsentRightTop then

        if targetScreenRect.yMin >= heightInScreen then
            screenPos.y = targetScreenRect.yMax - halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Top
        else
            screenPos.y = targetScreenRect.yMin + halfHeightInScreen
            finalYPos = UIConst.UI_TIPS_Y_POS_TYPE.Bottom
        end


        local rightWidth = screenSize.x - targetScreenRect.xMax
        if rightWidth >= widthInScreen then
            screenPos.x = targetScreenRect.xMax + halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Right
        else

            screenPos.x = targetScreenRect.xMin - halfWidthInScreen
            finalXPos = UIConst.UI_TIPS_X_POS_TYPE.Left
        end
    end


    screenPos.x = lume.clamp(screenPos.x, halfWidthInScreen, screenSize.x - halfWidthInScreen) + paddingLeft
    screenPos.y = lume.clamp(screenPos.y, halfHeightInScreen, screenSize.y - halfHeightInScreen) + paddingTop

    screenPos = screenPos - oriScreenSize / 2
    screenPos.y = -screenPos.y


    local canvasPos = Vector2(screenPos.x / xRation, screenPos.y / yRation)
    contentRectTrans.anchoredPosition = canvasPos
    return finalXPos, finalYPos
end



function UIUtils.rectTransToPadding(rectTransform)
    local anchoredPos = rectTransform.anchoredPosition
    local size = rectTransform.rect.size
    local parentSize = rectTransform.parent.rect.size
    local left = (parentSize.x - size.x) / 2 + anchoredPos.x
    local right = (parentSize.x - size.x) / 2 - anchoredPos.x
    local top = (parentSize.y - size.y) / 2 - anchoredPos.y
    local bottom = (parentSize.y - size.y) / 2 + anchoredPos.y
    return {
        top = top,
        left = left,
        right = right,
        bottom = bottom,
    }
end

function UIUtils.screenPosToWorldPos(x, y, yPlane)
    local ray = CameraManager.mainCamera:ScreenPointToRay(Vector3(x, y, 0))
    local length = (yPlane - ray.origin.y) / ray.direction.y
    local worldPos = ray.origin + ray.direction * length
    return worldPos
end

function UIUtils.changeAlpha(target, a)
    local color = target.color
    color.a = a
    target.color = color
end

function UIUtils.changeColorExceptAlpha(target, color)
    local a = target.color.a
    color.a = a
    target.color = color
end

function UIUtils.isPosInScreen(worldPos, camera, xFrame, yFrame)
    xFrame = xFrame or 0
    yFrame = yFrame or 0
    local xMin = xFrame
    local xMax = Screen.width - xFrame
    local yMin = yFrame
    local yMax = Screen.height - yFrame

    camera = camera or CameraManager.mainCamera
    local pos = camera:WorldToScreenPoint(worldPos)
    return pos.x >= xMin and pos.y >= yMin and pos.z >= 0 and pos.x <= xMax and pos.y <= yMax, pos
end

function UIUtils.getRemainingText(t)
    local hour = math.floor(t / 3600)
    t = t % 3600
    local min = math.floor(t / 60)
    t = math.floor(t % 60)
    return string.format("%02d:%02d:%02d", hour, min, t)
end

function UIUtils.getRemainingTextToMinute(t)
    local min = math.floor(t / 60)
    t = math.floor(t % 60)
    return string.format("%02d:%02d", min, t)
end

function UIUtils.getItemRarity(itemId)
    local data = Tables.itemTable:GetValue(itemId)
    return data.rarity
end

function UIUtils.getItemUseDesc(itemId)
    return CS.Beyond.Gameplay.TacticalItemUtil.GetUseItemDesc(itemId)
end

function UIUtils.getItemEquippedDesc(itemId)
    return CS.Beyond.Gameplay.TacticalItemUtil.GetEquipItemDesc(itemId)
end

function UIUtils.getItemEquippedExtraDesc(itemId)
    return CS.Beyond.Gameplay.TacticalItemUtil.GetEquipItemExtraDesc(itemId)
end

function UIUtils.getItemRarityColor(rarity)
    local rarityColorStr = Tables.rarityColorTable[rarity]
    return UIUtils.getColorByString(rarityColorStr.color)
end

function UIUtils.getCharRarityColor(rarity)
    return UIUtils.getItemRarityColor(rarity)
end

function UIUtils.getColorByString(strColor, a)
    local r = 0
    local g = 0
    local b = 0
    a = a or 255
    if string.len(strColor) == 6 then
        local strR = string.sub(strColor, 1, 2)
        local strG = string.sub(strColor, 3, 4)
        local strB = string.sub(strColor, 5, 6)
        r = tonumber(strR, 16)
        g = tonumber(strG, 16)
        b = tonumber(strB, 16)
    end
    if string.len(strColor) == 8 then
        local strR = string.sub(strColor, 1, 2)
        local strG = string.sub(strColor, 3, 4)
        local strB = string.sub(strColor, 5, 6)
        local strA = string.sub(strColor, 7, 8)
        r = tonumber(strR, 16)
        g = tonumber(strG, 16)
        b = tonumber(strB, 16)
        a = tonumber(strA, 16)
    end
    local color = CS.UnityEngine.Color(r / 255, g / 255, b / 255, a / 255)
    return color
end

function UIUtils.getStringByColor(color)
    local r = math.floor(color.r * 255 + 0.5)
    local g = math.floor(color.g * 255 + 0.5)
    local b = math.floor(color.b * 255 + 0.5)
    local a = math.floor(color.a * 255 + 0.5)
    return string.format("#%02X%02X%02X%02X", r, g, b, a)
end

function UIUtils.setSpecialFillAmount(img, percent, minMaxVector2)
    img.fillAmount = percent * (minMaxVector2.y - minMaxVector2.x) + minMaxVector2.x
end

function UIUtils.checkInputValid(value)

    return true

end

function UIUtils.getStringLength(str)
    return CS.Beyond.I18n.I18nUtils.GetStringLength(str)
end

function UIUtils.getNumString(num, isPrice)
    if isPrice and num < 100000 then
        return string.format("%d", num)
    end
    local curLang = CS.Beyond.I18n.I18nUtils.curEnvLang
    local isChineseStyleLang = curLang == GEnums.EnvLang.CN
    if isChineseStyleLang then
        local wan = num / 10000
        if wan < 1 then
            return string.format("%d", num)
        end
        if wan < 10000 then
            return UIUtils._getNumAbbrStr(wan, Language.LUA_NUM_UNIT_WAN, isPrice)
        end
        local yi = wan / 10000
        return UIUtils._getNumAbbrStr(yi, Language.LUA_NUM_UNIT_YI, isPrice)
    else
        local m = num / 1000 / 1000
        local k = num / 1000
        if m >= 1 then
            return UIUtils._getNumAbbrStr(m, Language.LUA_NUM_UNIT_MILLION, isPrice)
        elseif k >= 1 then
            return UIUtils._getNumAbbrStr(k, Language.LUA_NUM_UNIT_THOUSAND, isPrice)
        else
            return string.format("%d", num)
        end
    end
end

function UIUtils._getNumAbbrStr(num, text, isCeiling)
    local carryFunc = function(x)

        return isCeiling and math.ceil(x - 1e-10) or math.floor(x + 1e-10)
    end


    if num < 100 then
        if num < 10 then

            if carryFunc(num * 100) % 10 > 0 then
                return string.format("%.2f%s", carryFunc(num * 100) / 100, text)
            elseif math.floor(num * 10) % 10 > 0 then
                return string.format("%.1f%s", carryFunc(num * 10) / 10, text)
            end
        else

            if carryFunc(num * 10) % 10 > 0 then
                return string.format("%.1f%s", carryFunc(num * 10) / 10, text)
            end
        end
    end

    return string.format("%d%s", carryFunc(num), text)
end


function UIUtils.ceilToTenthStr(num)
    return string.format("%.1f", math.ceil(num * 10 - 1e-6) * 0.1)
end


function UIUtils.floorToTenthStr(num)
    return string.format("%.1f", math.floor(num * 10 + 1e-6) * 0.1)
end

local ROMAN_VAL = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1}
local ROMAN_SYM = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"}




function UIUtils.intToRoman(num)
    local roman = ""
    for i = 1, #ROMAN_VAL do
        while num >= ROMAN_VAL[i] do
            num = num - ROMAN_VAL[i]
            roman = roman .. ROMAN_SYM[i]
        end
    end
    return roman
end

function UIUtils.setSizeDeltaX(rect, value)
    local size = rect.sizeDelta
    size.x = value
    rect.sizeDelta = size
end

function UIUtils.setSizeDeltaY(rect, value)
    local size = rect.sizeDelta
    size.y = value
    rect.sizeDelta = size
end

function UIUtils.mapScreenPosToEllipseEdge (screenPos, ellipseXRadius, ellipseYRadius, ellipseYOffset)
    ellipseYOffset = ellipseYOffset or 0
    local x = screenPos.x
    local y = screenPos.y - ellipseYOffset

    local angle = math.atan(screenPos.y, screenPos.x)
    local k = y / x
    local uiPos = Vector2.zero
    local a = ellipseXRadius
    local b = ellipseYRadius
    uiPos.x = a * b / math.sqrt(b * b + a * a * k * k)
    if x < 0 then
        uiPos.x = -uiPos.x
    end
    uiPos.y = uiPos.x * k

    local shiftedScreenPos = Vector2(x, y)
    if uiPos.magnitude < shiftedScreenPos.magnitude then
        uiPos.y = uiPos.y + ellipseYOffset
        return uiPos, math.deg(angle), true
    end

    return screenPos, math.deg(angle), false
end

function UIUtils.resolveOriginalText(text)
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveOriginalText(text)
end

function UIUtils.resolveTextCinematic(text)
    local cfg = {
        playerName = true,
        gender = true,
    }
    return UIUtils.resolveText(text, cfg)
end

function UIUtils.resolveNarrativeEntryLinks(text)
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveNarrativeEntryLinks(text)
end

function UIUtils.resolveNarrativeTextWithEntryLinks(text)
    local rawText, entryLinks = UIUtils.resolveNarrativeEntryLinks(text)
    return UIUtils.resolveTextCinematic(rawText), entryLinks
end

function UIUtils.resolveLinkTypeFromId(linkId)
    if string.startWith(linkId, CS.Beyond.Gameplay.GameplayUIUtils.NARRATIVE_LINK_PREFIX) then
        return UIConst.UI_TEXT_LINK_TYPE.Narrative
    elseif string.startWith(linkId, CS.Beyond.UI.UIText.HYPERLINK_ID_PREFIX) then
        return UIConst.UI_TEXT_LINK_TYPE.Hyperlink
    else
        return UIConst.UI_TEXT_LINK_TYPE.Default
    end
end



function UIUtils.resolveText(text, cfg)
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveText(text, cfg.playerName, cfg.gender)
end

function UIUtils.resolveTextPlayerName(text)
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveTextPlayerName(text)
end

function UIUtils.resolveTextGender(text)
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveTextGender(text)
end

function UIUtils.resolveTextGenderWithNpcGender(text, gender)
    local genderInt = gender and gender:ToInt() or 0
    return CS.Beyond.Gameplay.GameplayUIUtils.ResolveTextGenderWithNpcGender(text, genderInt)
end

function UIUtils.genDynamicBlackScreenMaskData(systemName, fadeInTime, fadeOutTime, fadeInCallback)
    local maskData = CS.Beyond.Gameplay.UICommonMaskData()
    maskData.fadeInTime = fadeInTime
    maskData.fadeBeforeTime = 0
    maskData.fadeOutTime = fadeOutTime
    if fadeInCallback ~= nil then
        maskData.fadeInCallback = function()
            fadeInCallback()
        end
    end

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
        maskData.extraData.desc = systemName
    end

    return maskData
end

function UIUtils.genDynamicBlackScreenMaskDataWithWaitTime(systemName, fadeInTime, fadeOutTime, fadeWaitTime, fadeInCallback)
    local maskData = CS.Beyond.Gameplay.UICommonMaskData()
    maskData.fadeInTime = fadeInTime
    maskData.fadeBeforeTime = 0
    maskData.fadeWaitTime = fadeWaitTime
    maskData.fadeOutTime = fadeOutTime
    if fadeInCallback ~= nil then
        maskData.fadeInCallback = function()
            fadeInCallback()
        end
    end

    if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
        maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
        maskData.extraData.desc = systemName
    end

    return maskData
end

local rewardItemRarityEffectNames = {
    [1] = "normaGlow",
    [2] = "greenGlow",
    [3] = "blueGlow",
    [4] = "purpleGlow",
    [5] = "goldGlow",
}
function UIUtils.setRewardItemRarityGlow(cell, rarity)
    local count = #rewardItemRarityEffectNames
    rarity = math.min(count, rarity)
    for k = 1, count do
        local name = rewardItemRarityEffectNames[k]
        cell.view[name].gameObject:SetActiveIfNecessary(k == rarity)
    end
end

function UIUtils.getItemTypeName(itemId)
    local itemCfg = Tables.itemTable:GetValue(itemId)
    local itemTypeCfg = Tables.itemTypeTable:GetValue(itemCfg.type:GetHashCode())
    local defaultTypeName = itemTypeCfg.name

    if itemCfg.type == GEnums.ItemType.Weapon then
        local weaponCfg = Tables.weaponBasicTable:GetValue(itemId)
        if not weaponCfg then
            return defaultTypeName
        end

        local weaponTypeInt = weaponCfg.weaponType:ToInt()
        local weaponTypeName = Language[string.format("LUA_WEAPON_TYPE_%d", weaponTypeInt)]
        return weaponTypeName
    end


    if itemCfg.type == GEnums.ItemType.Equip then
        local _, equipBasicCfg = Tables.equipTable:TryGetValue(itemId)
        if not equipBasicCfg then
            return defaultTypeName
        end

        local equipTemplateId = itemId
        local _, equipTemplate = Tables.equipTable:TryGetValue(equipTemplateId)
        if not equipTemplate then
            return defaultTypeName
        end

        local equipType = equipTemplate.partType
        local equipTypeName = Language[UIConst.CHAR_INFO_EQUIP_TYPE_TILE_PREFIX .. LuaIndex(equipType:ToInt())]
        return equipTypeName
    end

    if itemCfg.type == GEnums.ItemType.NormalBuilding or itemCfg.type == GEnums.ItemType.FuncBuilding then
        if FactoryUtils.isDecoBuildingItem(itemId) then
            return Language.LUA_FAC_DECO_BUILDING_NAME
        end
    end

    return defaultTypeName
end

function UIUtils.displayItemBasicInfos(view, loader, itemId, instId)
    local data = Tables.itemTable:GetValue(itemId)
    local itemType = data.type

    if view.itemNameTxt then
        view.itemNameTxt.text = UIUtils.getItemName(itemId, instId)
    end

    if view.itemIcon then
        view.itemIcon:InitItemIcon(itemId, true, instId)
    end

    if view.itemTypeTxt then
        local itemTypeName = UIUtils.getItemTypeName(itemId)
        view.itemTypeTxt.text = itemTypeName
    end
    if view.rarityLine then
        UIUtils.setItemRarityImage(view.rarityLine, data.rarity)
    end
end

function UIUtils.getItemName(itemId, instId)
    local data = Tables.itemTable:GetValue(itemId)
    if not instId then
        return data.name
    end

    if data.type == GEnums.ItemType.WeaponGem then
        local leadTermId = CharInfoUtils.getGemLeadSkillTermId(instId)
        if not leadTermId then
            return data.name
        end

        local leadTermCfg = Tables.gemTable:GetValue(leadTermId)
        return string.format(Language.LUA_ITEM_COMPOSITE_NAME, data.name, leadTermCfg.tagName)
    end

    return data.name
end

function UIUtils.checkIfReachAdventureLv(needLv)
    local adventureLevelData = GameInstance.player.adventure.adventureLevelData
    return adventureLevelData.lv >= needLv
end

function UIUtils.displayCommercialItemInfo(view, loader, itemId, instId)
    local itemData = Tables.itemTable[itemId]
    if itemData.type == GEnums.ItemType.GemLockedTermBox then

        view.stateCtrl:SetState("GemLockedTermBox")

        local tipsGemWidget = view.tipsGemAttributeNode
        tipsGemWidget:InitTipsGemAttributeNode()
        tipsGemWidget:RefreshView(itemId)
    end
end

function UIUtils.displayWeaponInfo(view, loader, itemId, instId)
    local itemData = Tables.itemTable[itemId]

    local weaponInstData = instId and CharInfoUtils.getWeaponByInstId(instId)
    view.starGroup:InitStarGroup(itemData.rarity)
    view.potentialStar:InitWeaponPotentialStar(weaponInstData and weaponInstData.refineLv or 0)
    view.weaponGemSlimNode:InitWeaponGemSlimNode(weaponInstData and weaponInstData.attachedGemInstId or 0)
    if weaponInstData then
        view.tipWeaponLevelNode:InitTipWeaponLevelNode(itemId, instId)
        view.weaponAttributeNode:InitWeaponAttributeNode(instId, weaponInstData.attachedGemInstId)
        view.weaponSkillNode:InitWeaponSkillNode(instId)
    else
        local hasValue

        local weaponBasicData

        local weaponBreakThroughDetailList
        local initMaxLevel, breakThroughCount, maxBreakthroughLevel = 0, 0, 0
        hasValue, weaponBasicData = Tables.weaponBasicTable:TryGetValue(itemId)
        if hasValue then
            hasValue, weaponBreakThroughDetailList = Tables.weaponBreakThroughTemplateTable:TryGetValue(weaponBasicData.breakthroughTemplateId)
            if hasValue then
                breakThroughCount = #weaponBreakThroughDetailList.list
                if breakThroughCount > 1 then
                    initMaxLevel = weaponBreakThroughDetailList.list[1].breakthroughLv
                    maxBreakthroughLevel = breakThroughCount - 1
                end
            end
        end
        view.tipWeaponLevelNode:InitTipWeaponLevelNodeNoInst(1, initMaxLevel, 0, maxBreakthroughLevel)
        view.weaponAttributeNode:InitWeaponAttributeNodeByTemplateId(itemId)
        view.weaponSkillNode:InitWeaponSkillNodeByTemplateId(itemId, 0, 0, false)
    end

    if view.equippedNode then
        if instId and instId > 0 then
            view.equippedNode:InitEquipNodeByWeaponInstId(instId)
        else
            view.equippedNode.gameObject:SetActive(false)
        end
        if view.equippedSpace then
            view.equippedSpace.gameObject:SetActive(view.equippedNode.gameObject.activeSelf)
        end
    end
end

function UIUtils.displayEquipInfo(view, loader, itemId, instId)
    local equipCfg = Tables.equipTable[itemId]
    view.equipLvTxt.text = equipCfg.minWearLv
    view.equipSuitNode:InitEquipSuitNode(itemId)
    local hasInst = CharInfoUtils.getEquipByInstId(instId) ~= nil
    if hasInst then
        view.weaponAttributeNode:InitEquipAttributeNode(instId)
    else
        view.weaponAttributeNode:InitEquipAttributeNodeByTemplateId(itemId)
    end
    if view.equippedNode then
        if hasInst then
            view.equippedNode:InitEquippedNodeByEquipInstId(instId)
        else
            view.equippedNode.gameObject:SetActive(false)
        end
        if view.equippedSpace then
            view.equippedSpace.gameObject:SetActive(view.equippedNode.gameObject.activeSelf)
        end
    end
end


function UIUtils.displayWeaponGemInfo(view, loader, itemId, instId)
    view.gemSkillNode:InitGemSkillNode(instId)
    if view.equippedNode then
        if instId and instId > 0 then
            view.equippedNode:InitEquippedNodeByGemInstId(instId)
        else
            view.equippedNode.gameObject:SetActive(false)
        end
        if view.equippedSpace then
            view.equippedSpace.gameObject:SetActive(view.equippedNode.gameObject.activeSelf)
        end
    end
    if view.domainTagNode then
        local domainId
        local isInst = instId and instId > 0
        if isInst then
            local gemInst = CharInfoUtils.getGemByInstId(instId)
            if gemInst then
                domainId = gemInst.domainId
            end
        end
        view.domainTagNode:InitDomainTagNode(domainId)
    end

    local hasGemInst = instId and instId > 0
    local isPerfectMatch = false
    if hasGemInst then
        isPerfectMatch = UIUtils.getGemWishListPerfectMatch(instId)
    end
    if view.gemNode then
        view.gemNode.gameObject:SetActive(isPerfectMatch)

        local showEmptyNode = not isPerfectMatch
        if view.emptyInfoNode then

            view.emptyInfoNode.gameObject:SetActive(showEmptyNode)
        else

            view.emptyNode.gameObject:SetActive(showEmptyNode)
        end
    end
    if view.weaponCompatibleNode then
        if hasGemInst then
            view.weaponCompatibleNode:InitWeaponCompatibleNode(instId)
        else
            view.weaponCompatibleNode.gameObject:SetActive(false)
        end
    end
end

function UIUtils.displayGiftItemTags(view, itemId)
    local _, giftData = Tables.giftItemTable:TryGetValue(itemId)
    if not giftData or #giftData.tagList == 0 then
        view.gameObject:SetActive(false)
        return
    end
    view.gameObject:SetActive(true)
    view.tagCellCache = view.tagCellCache or UIUtils.genCellCache(view.tagCell)
    view.tagCellCache:Refresh(#giftData.tagList, function(cell, index)
        local tagId = giftData.tagList[CSIndex(index)]
        local _, tagData = Tables.tagDataTable:TryGetValue(tagId)
        if tagData then
            cell.nameTxt.text = tagData.tagName
        end
    end)
end

function UIUtils.checkText(text, errHint)
    if string.isEmpty(text) then
        return errHint
    end
    return text
end





function UIUtils.getShortLeftTime(leftSec)
    if leftSec < Const.SEC_PER_MIN then
        return string.format(Language.TIME_FORMAT_MIN, 0)
    elseif leftSec < Const.SEC_PER_HOUR then
        return string.format(Language.TIME_FORMAT_MIN, math.floor(leftSec / Const.SEC_PER_MIN))
    elseif leftSec < Const.SEC_PER_DAY then
        return string.format(Language.TIME_FORMAT_HOUR, math.floor(leftSec / Const.SEC_PER_HOUR))
    else
        return string.format(Language.TIME_FORMAT_DAY, math.floor(leftSec / Const.SEC_PER_DAY))
    end
end


function UIUtils.getSecondsLeftTime(leftSec)
    if leftSec < 0 then
        leftSec = 0
    end
    leftSec = math.floor(leftSec)
    return string.format(Language.TIME_FORMAT_SEC, leftSec)
end






function UIUtils.getFullLeftTime(leftSec)
    if leftSec < 0 then
        leftSec = 0
    end
    local days = math.floor(leftSec / Const.SEC_PER_DAY)
    local hours = math.floor((leftSec % Const.SEC_PER_DAY) / Const.SEC_PER_HOUR)
    local minutes = math.floor((leftSec % Const.SEC_PER_HOUR) / Const.SEC_PER_MIN)
    if days >= 1 then
        return string.format(Language.TIME_FORMAT_DAY_HOUR_MIN, days, hours, minutes)
    end
    return UIUtils.getLeftTime(leftSec)
end





function UIUtils.getLeftTime(leftSec)
    if leftSec < Const.SEC_PER_MIN then
        return string.format(Language.TIME_FORMAT_MIN, 0)
    elseif leftSec < Const.SEC_PER_HOUR then
        return string.format(Language.TIME_FORMAT_MIN, math.floor(leftSec / Const.SEC_PER_MIN))
    elseif leftSec < Const.SEC_PER_DAY then
        local hourTime = math.floor(leftSec / Const.SEC_PER_HOUR)
        local minTime = math.floor((leftSec % Const.SEC_PER_HOUR) / Const.SEC_PER_MIN)
        return string.format(Language.TIME_FORMAT_HOUR_MIN, hourTime, minTime)
    else
        local dayTime = math.floor(leftSec / Const.SEC_PER_DAY)
        local hourTime = math.floor((leftSec % Const.SEC_PER_DAY) / Const.SEC_PER_HOUR)
        return string.format(Language.TIME_FORMAT_DAY_HOUR, dayTime, hourTime)
    end
end




function UIUtils.getLeftTimeToSecond(leftSec, forceLong)
    leftSec = lume.round(leftSec)
    if leftSec <= Const.SEC_PER_HOUR and not forceLong then
        local format = Language.TIME_FORMAT_ONE_COLON
        return string.format(format, math.floor(leftSec / Const.SEC_PER_MIN), math.fmod(leftSec, Const.SEC_PER_MIN))
    else
        local hourTime = math.floor(leftSec / Const.SEC_PER_HOUR)
        local minTime = math.floor((leftSec % Const.SEC_PER_HOUR) / Const.SEC_PER_MIN)
        local secTime = math.fmod((leftSec % Const.SEC_PER_HOUR), Const.SEC_PER_MIN)
        local format = Language.TIME_FORMAT_TWO_COLON
        return string.format(format, hourTime, minTime, secTime)
    end
end

function UIUtils.setItemSprite(img, id, self, isBig)
    local data = Tables.itemTable:GetValue(id)
    local sprite
    if isBig then
        img:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, data.iconId)
    end
    if not sprite then
        img:LoadSprite(UIConst.UI_SPRITE_ITEM, data.iconId)
    end
end

function UIUtils.tryGetTagList(itemId, itemType)
    if not itemType then
        local itemData = Tables.itemTable[itemId]
        itemType = itemData.type
    end

    if itemType == GEnums.ItemType.NormalBuilding or itemType == GEnums.ItemType.FuncBuilding then
        local machineId = FactoryUtils.getItemBuildingId(itemId)
        local succ, machineId2TagIdData = Tables.factoryMachineId2tagIdsTable:TryGetValue(machineId)
        if not succ then
            return false
        end
        return true, machineId2TagIdData.tagIds
    end

    local succ, ingredientId2TagIdData = Tables.factoryResourceItemId2TagIdTable:TryGetValue(itemId)
    if not succ then
        return false
    end

    local tagIds = {}
    local count = 0
    local facCore = GameInstance.player.remoteFactory.core
    for craftId, tagId in pairs(ingredientId2TagIdData.craftId2TagId) do
        if facCore:IsFormulaVisible(craftId) then
            if not lume.find(tagIds, tagId) then
                tagIds[count] = tagId
                count = count + 1
            end
        end
    end

    if count == 0 then
        return false
    end
    tagIds.Count = count
    return true, tagIds
end



function UIUtils.getTrackerColorByMissionImportance(t)
    return DataManager.worldSetting.missionIconColor[t]
end


function UIUtils.getNumTextByLanguage(num)
    return Language[string.format("LUA_NUM_%d", num)]
end

function UIUtils.setFacBuffColorText(uiText, text, isBuff)
    local colorStr = UIConst.FAC_BUILDING_DEBUFF_COLOR_STR
    if isBuff then
        colorStr = UIConst.FAC_BUILDING_BUFF_COLOR_STR
    end
    uiText.text = string.format(UIConst.COLOR_STRING_FORMAT,
        colorStr, text)
end

function UIUtils.childrenArrayActive(root, activeCount)
    local childrenCount = root.transform.childCount
    for i = 0, childrenCount - 1 do
        local child = root.transform:GetChild(i)
        child.gameObject:SetActive(i < activeCount)
    end
end

function UIUtils.useItemOnTip(itemId)
    if GameInstance.playerController.mainCharacter == nil or
        GameInstance.playerController.mainCharacter:HasTag(CS.Beyond.Gameplay.PredefinedTag.ForbiddenUsingItem) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAR_TAG_FORBIDDEN)
        return false
    end
    if GameInstance.player.forbidSystem:IsItemForbidden(itemId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAR_TOAST_GAME_MODE_FORBID)
        return false
    end
    local useItemData = Tables.useItemTable:GetValue(itemId)
    if useItemData.uiType == GEnums.ItemUseUiType.SingleHeal or
        useItemData.uiType == GEnums.ItemUseUiType.AllHeal or
        useItemData.uiType == GEnums.ItemUseUiType.Revive or
        useItemData.uiType == GEnums.ItemUseUiType.Alive or
        useItemData.uiType == GEnums.ItemUseUiType.SingleUsp or
        useItemData.uiType == GEnums.ItemUseUiType.AllUsp then
        UIManager:Open(PanelId.TacticalItem, { itemId = itemId })
        return true
    elseif useItemData.uiType == GEnums.ItemUseUiType.Throw then
        if GameInstance.player.inventory:UseItem(Utils.getCurrentScope(), itemId) then
            return true
        end
    end
    return false
end

function UIUtils.lineLog(title, sep)
    local lines = {
        string.format("====== %s ======", title)
    }
    sep = sep or " :: "
    return function(...)
        local t = {...}
        local st = {}
        for _, v in ipairs(t) do
            st[#st + 1] = tostring(v)
        end
        local line = table.concat(st, sep)
        if not line:isEmpty() then
            table.insert(lines, line)
        else
            logger.error(table.concat(lines, "\n"))
        end
    end
end

function UIUtils.parseAllRecipedAsInput(itemId)

end

function UIUtils.getCellNums(width, space, cellWidthList)
    local nums = {}
    local count = #cellWidthList
    local curWidth = 0
    local curCellNum = 0
    for i = 1, count do
        local cellWidth = cellWidthList[i]
        if curWidth == 0 then
            curWidth = cellWidth
        else
            curWidth = curWidth + cellWidth + space
        end

        if curWidth > width then
            local num = i - curCellNum - 1
            table.insert(nums, num)
            curCellNum = i - 1
            curWidth = cellWidth
        end
    end

    if count > curCellNum then
        table.insert(nums, count - curCellNum)
    end

    return nums
end

function UIUtils.setItemStorageCountText(storageCountNode, itemId, needCount, ignoreInSafeZone)
    local inventory = GameInstance.player.inventory
    local itemData = Tables.itemTable[itemId]
    local isMoneyType = inventory:IsMoneyType(itemData.type)
    local valuableDepotType = itemData.valuableTabType
    local isValuableItem = valuableDepotType ~= GEnums.ItemValuableDepotType.Factory
    local count, bagCount, _ = Utils.getItemCount(itemId, ignoreInSafeZone, true)
    if ignoreInSafeZone or Utils.isInSafeZone() or isMoneyType or isValuableItem then
        storageCountNode:InitStorageNode(count, needCount, true)
    else
        storageCountNode:InitStorageNode(bagCount, needCount, false)
    end
end

function UIUtils.setNoEnoughCountColor(countStr)
    UIUtils.setCountColor(countStr, true)
end

function UIUtils.setCountColor(countStr, isLack)
    if isLack then
        return string.format(UIConst.COLOR_STRING_FORMAT, UIConst.COUNT_NOT_ENOUGH_COLOR_STR, countStr)
    else
        return countStr
    end
end

function UIUtils.setCountColorByCustomColor(countStr, isLack, customLackColorStr)
    if isLack then
        local color = string.isEmpty(customLackColorStr) and UIConst.COUNT_NOT_ENOUGH_COLOR_STR or customLackColorStr
        return string.format(UIConst.COLOR_STRING_FORMAT, color, countStr)
    else
        return countStr
    end
end

function UIUtils.PlayAnimationAndToggleActive(animationWrapper, isOn, callback)
    animationWrapper:ClearTween(false)
    if animationWrapper.gameObject.activeSelf == isOn then
        if isOn then

            animationWrapper:SampleToInAnimationEnd()
        end
        if callback then
            callback()
        end
        return
    end

    if isOn then
        animationWrapper.gameObject:SetActive(true)
        animationWrapper:PlayInAnimation(callback)
    else
        animationWrapper:PlayOutAnimation(function()
            if animationWrapper and animationWrapper.gameObject then
                animationWrapper.gameObject:SetActiveIfNecessary(false)
            end
            if callback then
                callback()
            end
        end)
    end
end

function UIUtils.inTimeline()
    local inTimeline = GameWorld.cutsceneManager.isMainTimelinePlaying
    return inTimeline
end

function UIUtils.inCG()
    local inCG = VideoManager.isPlayingFMV
    return inCG
end

function UIUtils.inDialog()
    local inDialog = GameWorld.dialogManager.isPlaying
    return inDialog
end

function UIUtils.inCinematic()
    return UIUtils.inCG() or UIUtils.inTimeline() or UIUtils.inDialog()
end

function UIUtils.inDungeon()
    return GameInstance.dungeonManager.inDungeon
end

function UIUtils.IsPhaseLevelOnTop()
    return PhaseManager:GetTopPhaseId() == PhaseId.Level
end


function UIUtils.usingBlockTransition()
    local BlockGlitchTransition = require_ex("UI/Panels/BlockGlitchTransition/BlockGlitchTransitionCtrl")
    return BlockGlitchTransition.BlockGlitchTransitionCtrl.s_renderTextureHandle ~= nil
end

function UIUtils.getTextShowDuration(text, tSpeed)
    local speed = tSpeed or 1
    local duration = I18nUtils.GetTextShowDuration(text)
    return duration / speed
end

function UIUtils.removePattern(str, pattern)
    local result = ""
    for match in string.gmatch(str, pattern) do
        local cleaned = string.gsub(match, "{.*}", "")
        result = result .. cleaned
    end
    if string.isEmpty(result) then
        result = str
    end
    return result
end

function UIUtils.loadSprite(loader, path, name)
    local fullPath = UIUtils.getSpritePath(path, name)
    if BEYOND_DEBUG then

        local fullDevPath = UIUtils.getSpriteDevPath(path, name)
        if ResourceManager.CheckExists(fullDevPath) then
            fullPath = fullDevPath
        end
    end
    local pathHash = __beyond_calculate_ab_path_hash(fullPath)
    if not ResourceManager.CheckExistsWithStringPathHash(pathHash) then

        logger.error("资源不存在", fullPath, pathHash)
        return nil
    end
    local sprite = loader:LoadSprite(fullPath)
    return sprite
end

function UIUtils.getPuzzleColorByColorType(colorType)
    local colorStr = UIConst.MINI_PUZZLE_GAME_ECOLOR_STR[colorType]
    colorStr = string.isEmpty(colorStr) and "FF00FF" or colorStr
    return UIUtils.getColorByString(colorStr)
end

function UIUtils.calcPivotVecByData(data, puzzleCellSize, puzzleCellPadding)

    local centerCell = data.originBlocks[0]
    local xMax = centerCell.x
    local xMin = centerCell.x
    local yMax = centerCell.y
    local yMin = centerCell.y
    for _, originBlock in pairs(data.originBlocks) do
        xMax = math.max(xMax, originBlock.x)
        xMin = math.min(xMin, originBlock.x)
        yMax = math.max(yMax, originBlock.y)
        yMin = math.min(yMin, originBlock.y)
    end

    local width = (xMax - xMin + 1) * (puzzleCellSize + 2 * puzzleCellPadding)
    local height = (yMax - yMin + 1) * (puzzleCellSize + 2 * puzzleCellPadding)
    local centerX = ((centerCell.x - xMin) * 2 + 1) * (puzzleCellSize / 2 + puzzleCellPadding)
    local centerY = ((centerCell.y - yMin) * 2 + 1) * (puzzleCellSize / 2 + puzzleCellPadding)
    return Vector2(centerX / width, centerY / height)
end

function UIUtils.splitItem(slotIndex)
    local toSlot = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope()):GetFirstEmptySlotIndex()
    if toSlot < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_TIPS_TOAST_1)
        return
    end
    UIManager:Open(PanelId.ItemSplit, {
        slotIndex = slotIndex,
    })
end

function UIUtils.getRewardFirstItem(rewardId)

    local rewardTableData = Tables.rewardTable[rewardId]
    return rewardTableData.itemBundles[0]
end

function UIUtils.getSoilRewardFirstItem(rewardId)

    local rewardTableData = Tables.rewardSoilTable[rewardId]
    return rewardTableData.itemBundles[0]
end



function UIUtils.getAvatarIconByItemId(itemId)
    for _, avatarData in pairs(Tables.userAvatarTable) do
        if avatarData.itemId == itemId then
            return avatarData.icon
        end
    end
    return nil
end

function UIUtils.getRewardItems(rewardId, items)

    local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
    items = items or {}
    if rewardTableData then

        for _, v in pairs(rewardTableData.itemBundles) do
            table.insert(items, v)
        end
    else
        logger.error("RewardTable表数据缺失！reward id：" .. rewardId)
    end
    return items
end


function UIUtils.getRewardItemsMergeSameId(rewardId, items)
    local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
    items = items or {}
    if rewardTableData then

        for _, v in pairs(rewardTableData.itemBundles) do
            local found = false

            for i, existingItem in ipairs(items) do
                if existingItem.id == v.id then
                    local mergedItem = {
                        id = v.id,
                        count = existingItem.count + v.count
                    }
                    items[i] = mergedItem
                    found = true
                    break
                end
            end
            if not found then
                table.insert(items, {
                    id = v.id,
                    count = v.count,
                })
            end
        end
    else
        logger.error("RewardTable表数据缺失！reward id：" .. rewardId)
    end
    return items
end

function UIUtils.getMonsterIconByMonsterId(monsterId)
    return string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Sprites/Wiki/MonsterImage/%s.png", monsterId)
end

function UIUtils.getParentCenterAnchoredPosition(rectTransform)
    local position = Vector2.zero
    if rectTransform == nil then
        return position
    end

    if rectTransform.parent == nil then
        return position
    end

    local parentRectTransform = rectTransform.parent:GetComponent("RectTransform")
    if parentRectTransform == nil then
        return position
    end

    local anChorOffset = Vector2(
        parentRectTransform.rect.width * (0.5 - rectTransform.anchorMin.x),
        parentRectTransform.rect.height * (0.5 - rectTransform.anchorMin.y)
    )

    local pivotOffset = Vector2(
        rectTransform.rect.width * (rectTransform.pivot.x - 0.5),
        rectTransform.rect.height * (rectTransform.pivot.y - 0.5)
    );

    return anChorOffset + pivotOffset;
end

function UIUtils.getRomanNumberText(number)
    if number < 1 or number > 10 then
        return ""
    end
    return Language[string.format("ui_common_roman_num_%d", number)]
end

function UIUtils.setItemRarityImage(img, rarity)
    local rarityColor = UIUtils.getItemRarityColor(rarity)
    img.color = rarityColor
end









function UIUtils.getEnemyInfoByIdAndLevel(enemyId, enemyLevel)
    local hasEnemyCfg, enemyCfg = Tables.enemyTable:TryGetValue(enemyId)
    local hasEnemyDisplayInfoCfg, enemyDisplayInfoCfg = Tables.enemyDisplayInfoTable:TryGetValue(enemyId)

    if not hasEnemyCfg and not hasEnemyDisplayInfoCfg then
        logger.error(string.format("敌人表和敌人展示信息表中都找不到enemyId:%s。", enemyId))
        return
    end


    local enemyTemplateId = hasEnemyCfg and enemyCfg.templateId or (hasEnemyDisplayInfoCfg and enemyDisplayInfoCfg.templateId)
    local hasEnemyTemplateDisplayCfg, enemyTemplateDisplayCfg = Tables.enemyTemplateDisplayInfoTable:TryGetValue(enemyTemplateId)
    if not hasEnemyTemplateDisplayCfg then
        logger.error(string.format("敌人模板展示信息表中都找不到enemyTemplateId:%s，enemyId:%s。", enemyTemplateId, enemyId))
        return
    end

    local enemyInfo = {}

    enemyInfo.name = (hasEnemyDisplayInfoCfg and not string.isEmpty(enemyDisplayInfoCfg.name)) and
            enemyDisplayInfoCfg.name or enemyTemplateDisplayCfg.name
    enemyInfo.desc = (hasEnemyDisplayInfoCfg and not string.isEmpty(enemyDisplayInfoCfg.description)) and
            enemyDisplayInfoCfg.description or enemyTemplateDisplayCfg.description
    enemyInfo.level = enemyLevel
    enemyInfo.templateId = enemyTemplateId

    enemyInfo.ability = {}

    local abilityIds = (hasEnemyDisplayInfoCfg and enemyDisplayInfoCfg.abilityDescIds.Count > 0) and
            enemyDisplayInfoCfg.abilityDescIds or enemyTemplateDisplayCfg.abilityDescIds
    for _, abilityId in pairs(abilityIds) do
        local abilityDescCfg = Tables.enemyAbilityDescTable[abilityId]
        table.insert(enemyInfo.ability, {
            abilityId = abilityDescCfg.abilityId,
            name = abilityDescCfg.name,
            description = abilityDescCfg.description,
        })
    end

    return enemyInfo
end





function UIUtils.isItemForbidden(dungeonId, itemId)
    local _, subGameInstData = DataManager.subGameInstDataTable:TryGetValue(dungeonId)
    if not subGameInstData then
        return false
    end

    local _, gameModeData = DataManager.gameModeTable:TryGetValue(subGameInstData.modeId)
    if not gameModeData then
        return false
    end

    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if not itemData then
        return false
    end
    local _, itemUseData = Tables.useItemTable:TryGetValue(itemId)

    if not gameModeData.functionSettings then
        return false
    end

    for index, functionSetting in cs_pairs(gameModeData.functionSettings) do
        if functionSetting.modeFuncType == GEnums.GameModeFuncType.ForbidUseItemType then
            local funcParams = functionSetting.funcParams
            if funcParams and funcParams.itemTypes and
                funcParams.itemTypes:Contains(itemData.type) then
                return true
            end
        elseif functionSetting.modeFuncType == GEnums.GameModeFuncType.ForbidUseItemGameModeTagType then
            local funcParams = functionSetting.funcParams
            if funcParams and funcParams.itemTags and itemUseData then
                for _, tag in pairs(itemUseData.gameModeForbidTagList) do
                    if funcParams.itemTags:Contains(tag) then
                        return true
                    end
                end
            end
        end
    end

    return false
end



function UIUtils.convertRewardItemBundlesToDataList(itemBundles, isIncremental)
    local rewardItemDataList = {}

    if itemBundles == nil or itemBundles.Count == 0 then
        return rewardItemDataList
    end

    for index = 0, itemBundles.Count - 1 do
        local rewardItem = itemBundles[index]
        local itemId = rewardItem.id
        local success, itemData = Tables.itemTable:TryGetValue(itemId)
        if success then
            table.insert(rewardItemDataList, {
                id = itemId,
                count = rewardItem.count,
                rarity = itemData.rarity,
                sortId1 = itemData.sortId1,
                sortId2 = itemData.sortId2,
            })
        end
    end

    table.sort(rewardItemDataList, Utils.genSortFunction({ "rarity", "sortId1", "sortId2", "id" }, isIncremental))

    return rewardItemDataList
end


function UIUtils.convertLuaRewardItemBundlesToDataList(itemBundles, isIncremental)
    local rewardItemDataList = {}

    if itemBundles == nil or itemBundles.Count == 0 then
        return rewardItemDataList
    end

    for index = 1, #itemBundles do
        local rewardItem = itemBundles[index]
        local itemId = rewardItem.id
        local success, itemData = Tables.itemTable:TryGetValue(itemId)
        if success then
            table.insert(rewardItemDataList, {
                id = itemId,
                count = rewardItem.count,
                rarity = itemData.rarity,
                sortId1 = itemData.sortId1,
                sortId2 = itemData.sortId2,
            })
        end
    end

    table.sort(rewardItemDataList, Utils.genSortFunction({ "rarity", "sortId1", "sortId2", "id" }, isIncremental))

    return rewardItemDataList
end



function UIUtils.getMobileHudPanelMaxHeight(panelHeight, minHudHeight, maxHudHeight)


    local minSupHeight = CS.Beyond.UI.UIConst.STANDARD_VERTICAL_RESOLUTION

    local maxSupHeight = CS.Beyond.UI.UIConst.STANDARD_HORIZONTAL_RESOLUTION

    local realHeight = lume.clamp(panelHeight, minSupHeight, maxSupHeight)
    local realMaxHeight = (realHeight - minSupHeight) / (maxSupHeight - minSupHeight) *
            (maxHudHeight - minHudHeight) + minHudHeight

    return realMaxHeight
end


function UIUtils.commonAdaptHudTrack(view, config)
    local layoutGroup = view.verticalLayoutGroup
    local padding = layoutGroup.padding.vertical
    local spacing = layoutGroup.spacing
    local diffY = config.DIFF_HEIGHT or 0
    LayoutRebuilder.ForceRebuildLayoutImmediate(view.rectTransform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(view.objectiveContent)

    local hasVaryingNode = view.varyingNode.gameObject.activeInHierarchy
    local varyingHeight = hasVaryingNode and view.varyingNode.rect.height + spacing or 0
    local contentHeight =  view.objectiveContent.rect.height + spacing + (hasVaryingNode and 0 or diffY)
    local realMainHeight = padding + view.headNode.rect.height + contentHeight + varyingHeight
    local width = view.rectTransform.rect.width


    local rectHeight = realMainHeight
    local canScrollContent = false
    if DeviceInfo.usingTouch then


        local minSupHeight = CS.Beyond.UI.UIConst.STANDARD_VERTICAL_RESOLUTION

        local maxSupHeight = CS.Beyond.UI.UIConst.STANDARD_HORIZONTAL_RESOLUTION

        local realHeight = lume.clamp(view.rectTransform.rect.height, minSupHeight, maxSupHeight)
        local realMaxHeight = (realHeight - minSupHeight) / (maxSupHeight - minSupHeight) *
                (config.MAX_MAIN_RECT_HEIGHT_FOR_MOBILE - config.MIN_MAIN_RECT_HEIGHT_FOR_MOBILE) +
                config.MIN_MAIN_RECT_HEIGHT_FOR_MOBILE

        rectHeight = math.min(realMainHeight, realMaxHeight)
        canScrollContent = realMainHeight > realMaxHeight
    end

    view.rectTransform.sizeDelta = Vector2(width, rectHeight)
    view.contentScrollView.enabled = canScrollContent
    view.arrowBottomNode.gameObject:SetActiveIfNecessary(canScrollContent)

    if canScrollContent then
        view.contentScrollView:ScrollTo(Vector2(0, -1), true)
    end

    return canScrollContent
end


function UIUtils.commonAdaptHudTrackV2(view, config, canFoldThresholdOffsetY)
    local layoutGroup = view.verticalLayoutGroup
    local padding = layoutGroup.padding.vertical
    local spacing = layoutGroup.spacing
    local canFoldThresholdOffsetY = canFoldThresholdOffsetY or 0
    LayoutRebuilder.ForceRebuildLayoutImmediate(view.rectTransform)
    LayoutRebuilder.ForceRebuildLayoutImmediate(view.objectiveContent)

    local contentHeight =  view.objectiveContent.rect.height + spacing
    local realMainHeight = padding + view.headNode.rect.height + contentHeight


    local rectFoldHeight = realMainHeight
    local rectUnfoldHeight = realMainHeight
    local scrollState = UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCantScroll
    local ignoreScrollMask = false
    if DeviceInfo.usingTouch then
        local canFoldThresholdForMobile = config.CAN_FOLD_THRESHOLD_FOR_MOBILE + canFoldThresholdOffsetY



        local minSupHeight = CS.Beyond.UI.UIConst.STANDARD_VERTICAL_RESOLUTION

        local maxSupHeight = CS.Beyond.UI.UIConst.STANDARD_HORIZONTAL_RESOLUTION

        local realCanvasHeight = lume.clamp(view.rectTransform.rect.height, minSupHeight, maxSupHeight)
        local maxTrackHudHeight = realCanvasHeight - UIConst.ON_TRACK_HUD_CONST_HEIGHT -
                realCanvasHeight * UIConst.JOYSTICK_IN_SCREEN_HEIGHT_PROPORTION * (1 - UIConst.TRACK_HUD_UNFOLD_OCCLUSION_JOYSTICK_PROPORTION)
                + canFoldThresholdOffsetY

        if realMainHeight <= canFoldThresholdForMobile then
            scrollState = UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCantScroll
            rectFoldHeight = realMainHeight
            rectUnfoldHeight = realMainHeight
            ignoreScrollMask = false
        elseif realMainHeight <= canFoldThresholdForMobile + config.CAN_FOLD_THRESHOLD_FOR_MOBILE_OFFSET then
            scrollState = UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCantScroll
            rectFoldHeight = canFoldThresholdForMobile
            rectUnfoldHeight = realMainHeight
            ignoreScrollMask = true
        elseif realMainHeight <= maxTrackHudHeight then
            scrollState = UIConst.TRACK_HUD_SCROLL_STATE.CanScrollWhenFold
            rectFoldHeight = canFoldThresholdForMobile
            rectUnfoldHeight = realMainHeight
            ignoreScrollMask = false
        else
            scrollState = UIConst.TRACK_HUD_SCROLL_STATE.AlwaysCanScroll
            rectFoldHeight = canFoldThresholdForMobile
            rectUnfoldHeight = maxTrackHudHeight
            ignoreScrollMask = false
        end
    end

    return ignoreScrollMask, scrollState, rectFoldHeight, rectUnfoldHeight
end

function UIUtils.getMovingItemMode()
    local mode
    if UIUtils.isBatchMovingItem() then
        mode = CS.Proto.ITEM_MOVE_MODE.BatchItemId
    elseif UIUtils.isQuickMovingItem() then
        mode = CS.Proto.ITEM_MOVE_MODE.Grid
    elseif UIUtils.isQuickMovingHalfItem() then
        mode = CS.Proto.ITEM_MOVE_MODE.HalfGrid
    end
    return mode
end

function UIUtils.isBatchMovingItem()
    return Input.GetKey(Unity.KeyCode.LeftControl) or Input.GetKey(Unity.KeyCode.RightControl)
end

function UIUtils.isQuickMovingItem()
    return Input.GetKey(Unity.KeyCode.LeftShift) or Input.GetKey(Unity.KeyCode.RightShift)
end

function UIUtils.isQuickMovingHalfItem()
    return Input.GetKey(Unity.KeyCode.LeftAlt) or Input.GetKey(Unity.KeyCode.RightAlt)
end

function UIUtils.getSettlementEnhanceEffectDesc(enhanceMoneyProduceSpeedRate, enhanceMoneyProfitRate, enhanceExpProfitRate)
    local effectStr = ""

    if enhanceMoneyProduceSpeedRate ~= 0 then
        effectStr = string.format(Language.LUA_SETTLEMENT_ENHANCE_MONEY_PRODUCE_SPEED, enhanceMoneyProduceSpeedRate) .. "\n"
    end

    if enhanceMoneyProfitRate ~= 0 then
        effectStr = effectStr .. string.format(Language.LUA_SETTLEMENT_ENHANCE_MONEY_PROFIT, enhanceMoneyProfitRate) .. "\n"
    end

    if enhanceExpProfitRate ~= 0 then
        effectStr = effectStr .. string.format(Language.LUA_SETTLEMENT_ENHANCE_EXP_PROFIT, enhanceExpProfitRate)
    end
    return effectStr
end


function UIUtils.showItemSideTips(itemCell, tipsPosType, tipsPosTransform, onBeforeJump)
    local posInfo
    if DeviceInfo.usingController then
        posInfo = {
            tipsPosType = tipsPosType or UIConst.UI_TIPS_POS_TYPE.RightDown,
            tipsPosTransform = tipsPosTransform or itemCell.transform,
            isSideTips = true,
            onBeforeJump = onBeforeJump,
        }
        itemCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end
    itemCell:ShowTips(posInfo)
end




function UIUtils.bindHyperlinkPopup(bindTarget, hyperlinkUITextGroupId, inputGroupId, actionId)
    if string.isEmpty(actionId) then
        actionId = "hyperlink_show_popup"
    end
    local bindId = UIUtils.bindInputPlayerAction(actionId, function()
        Notify(MessageConst.SHOW_HYPERLINK_POPUP_BY_GROUP_ID, hyperlinkUITextGroupId)
    end, inputGroupId)
    local curValid = CS.Beyond.UI.UIText.IsHyperlinkUITextGroupDisplayable(hyperlinkUITextGroupId)
    InputManagerInst:ToggleBinding(bindId, curValid)
    MessageManager:Register(MessageConst.HYPERLINK_UITEXT_GROUP_DISPLAYABLE_CHANGE, function(args)
        local groupId, isValid = unpack(args)
        if groupId == hyperlinkUITextGroupId then
            InputManagerInst:ToggleBinding(bindId, isValid)
            logger.info("[Hyperlink] bind displayable change :", isValid)
        end
    end, bindTarget)
end


function UIUtils.bindControllerCamZoom(groupId)
    local inId = UIUtils.bindInputPlayerAction("cam_zoom_in_ct", function()
        Utils.zoomCamera(4)
    end, groupId)
    local outId = UIUtils.bindInputPlayerAction("cam_zoom_out_ct", function()
        Utils.zoomCamera(-4)
    end, groupId)
    return inId, outId
end

function UIUtils.setTabIcons(cell, iconPath, iconName)
    cell.selectedIcon:LoadSprite(iconPath, iconName)
    cell.defaultIcon:LoadSprite(iconPath, iconName .. "_shadow")
end

function UIUtils.setTabIconsWithFullPath(cell, iconPath)
    cell.selectedIcon:LoadSprite(iconPath)
    cell.defaultIcon:LoadSprite(iconPath .. "_shadow")
end

function UIUtils.hideItemTipsOnLoseFocus(isFocused)
    if not isFocused then
        Notify(MessageConst.HIDE_ITEM_TIPS)
    end
end





function UIUtils.waitMsgExecute(msg, msgGroupKey, callback)
    local registerKey
    registerKey = MessageManager:Register(msg, function(msgArg)
        if not callback(msgArg) then
            MessageManager:Unregister(registerKey)
            Notify(MessageConst.ON_WAIT_MSG_EXECUTE_COMPLETE, registerKey)
        end
    end, msgGroupKey)
    return registerKey
end







function UIUtils.updateStaminaNode(node, staminaInfo)
    node.descStaminaTxt.text = string.isEmpty(staminaInfo.descStamina) and Language["ui_dungeon_details_ap_refresh"] or
            staminaInfo.descStamina

    local costStamina = staminaInfo.costStamina
    local isLack = GameInstance.player.inventory.curStamina < costStamina
    node.costStaminaTxt.text = UIUtils.setCountColor(costStamina, isLack)

    local delStamina = staminaInfo.delStamina
    local hasDelStamina = delStamina ~= nil and type(delStamina) == "number"
    node.delStaminaTxt.text = hasDelStamina and delStamina or 0
    node.delStaminaTxt.gameObject:SetActive(hasDelStamina)
end















function UIUtils.initSearchInput(inputField, initInfo)
    if inputField == nil or initInfo == nil then
        return
    end

    if initInfo.characterLimit ~= nil then
        inputField.characterLimit = initInfo.characterLimit
    else
        inputField.characterLimit = UIConst.INPUT_FIELD_NAME_CHARACTER_LIMIT
    end

    if initInfo.clearBtn ~= nil then
        initInfo.clearBtn.gameObject:SetActive(not string.isEmpty(inputField.text))
    end

    inputField.onValueChanged:RemoveAllListeners()
    inputField.onValueChanged:AddListener(function(str)
        if initInfo.clearBtn ~= nil then
            local isPsController = Utils.checkIsPSDevice()
            if isPsController then

                initInfo.clearBtn.gameObject:SetActive(false)
            else
                initInfo.clearBtn.gameObject:SetActive(not string.isEmpty(str))
            end
        end

        if initInfo.onInputValueChanged ~= nil then
            initInfo.onInputValueChanged(str)
        end
    end)


    if initInfo.onInputSubmit ~= nil then
        inputField.onSubmit:RemoveAllListeners()
        inputField.onSubmit:AddListener(function(newText)
            if Utils.checkIsPSDevice() then
                return
            end
            initInfo.onInputSubmit(newText)
        end)
    end


    inputField.onFocused:RemoveAllListeners()
    inputField.onFocused:AddListener(function()
        local isPsController = Utils.checkIsPSDevice()
        if initInfo.clearBtn ~= nil and isPsController then
            initInfo.clearBtn.gameObject:SetActive(false)
        end

        if initInfo.onInputFocused ~= nil then
            initInfo.onInputFocused()
        end
    end)


    inputField.onEndEdit:RemoveAllListeners()
    inputField.onEndEdit:AddListener(function(newText)
        local isPsController = Utils.checkIsPSDevice()
        if initInfo.clearBtn ~= nil and isPsController then
            initInfo.clearBtn.gameObject:SetActive(not string.isEmpty(inputField.text))
        end

        if initInfo.onInputEndEdit ~= nil then
            initInfo.onInputEndEdit(newText)
        end
    end)


    if initInfo.clearBtn ~= nil and initInfo.onClearClick ~= nil then
        initInfo.clearBtn.onClick:RemoveAllListeners()
        initInfo.clearBtn.onClick:AddListener(function()
            initInfo.onClearClick()
            CS.UnityEngine.EventSystems.EventSystem.current:SetSelectedGameObject(inputField.gameObject);
        end)
    end


    if initInfo.searchBtn ~= nil and initInfo.onSearchClick ~= nil then
        initInfo.searchBtn.onClick:RemoveAllListeners()
        initInfo.searchBtn.onClick:AddListener(function()
            initInfo.onSearchClick()
        end)
    end
end


function UIUtils.getUIVideoFullPath(path)
    if string.isEmpty(path) then
        return false
    end
    return CS.Beyond.Gameplay.View.VideoManager.TryGetVideoPlayFullPath('UI/' .. path)
end


function UIUtils.inDungeonOrFocusMode()
    return GameInstance.dungeonManager.inDungeon or FocusModeUtils.isInFocusMode
end

function UIUtils.isUIGOActive(go)
    return go.activeInHierarchy and go.layer ~= UIConst.HIDE_LAYER
end

function UIUtils.getSerialNum(intNum)

    local romanNumerals = {
        {1000, "M"},
        {900, "CM"},
        {500, "D"},
        {400, "CD"},
        {100, "C"},
        {90, "XC"},
        {50, "L"},
        {40, "XL"},
        {10, "X"},
        {9, "IX"},
        {5, "V"},
        {4, "IV"},
        {1, "I"}
    }
    local result = ""
    for _, pair in ipairs(romanNumerals) do
        local value = pair[1]
        local numeral = pair[2]
        while intNum >= value do
            result = result .. numeral
            intNum = intNum - value
        end
    end
    return result
end




function UIUtils.isGemPerfectMatchWeapon(gemInstId, weaponTemplateId)
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    if not gemInst or gemInst.termList.Count < 3 then
        return false
    end

    local weaponCfg = Tables.weaponBasicTable[weaponTemplateId]
    if not weaponCfg or #weaponCfg.weaponSkillList < 3 then
        return false
    end

    local gemTagSet = {}
    for i = 0, gemInst.termList.Count - 1 do
        local termId = gemInst.termList[i].termId
        local _, termCfg = Tables.gemTable:TryGetValue(termId)
        if termCfg then
            gemTagSet[termCfg.tagId] = true
        end
    end

    for _, skillId in pairs(weaponCfg.weaponSkillList) do
        local _, skillCfg = Tables.skillPatchTable:TryGetValue(skillId)
        if not skillCfg then
            return false
        end
        local skillPatchData = skillCfg.SkillPatchDataBundle[0]
        if not gemTagSet[skillPatchData.tagId] then
            return false
        end
    end

    return true
end



function UIUtils.getGemWishListPerfectMatch(gemInstId)
    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    if not gemInst or gemInst.termList.Count < 3 then
        return false, {}
    end


    local _, gemItemCfg = Tables.itemTable:TryGetValue(gemInst.templateId)
    if gemItemCfg and gemItemCfg.rarity <= 4 then
        return false, {}
    end

    local tagInfos = {}
    for i = 0, gemInst.termList.Count - 1 do
        local termId = gemInst.termList[i].termId
        local _, termCfg = Tables.gemTable:TryGetValue(termId)
        if not termCfg then
            return false, {}
        end
        table.insert(tagInfos, { termType = termCfg.termType, tagId = termCfg.tagId })
    end

    table.sort(tagInfos, function(a, b)
        if a.termType ~= b.termType then
            local cmp = a.termType:CompareTo(b.termType)
            return cmp < 0
        end
        return a.tagId < b.tagId
    end)

    local key = tagInfos[1].tagId .. "+" .. tagInfos[2].tagId .. "+" .. tagInfos[3].tagId

    local _, weaponList = Tables.gemTagKeyToWeaponTable:TryGetValue(key)
    if not weaponList then
        return false, {}
    end

    local matchList = {}
    local wishList = GameInstance.player.inventory.weaponGemWishList
    for i = 0, weaponList.list.Count - 1 do
        local weaponId = weaponList.list[i]
        if wishList:Contains(weaponId) then
            table.insert(matchList, weaponId)
        end
    end
    return #matchList > 0, matchList
end


_G.UIUtils = UIUtils
return UIUtils
