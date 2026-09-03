local autoCalcOrderUICtrl = require_ex('UI/Panels/Base/AutoCalcOrderUICtrl')
local PANEL_ID = PanelId.ControllerHint
ControllerHintCtrl = HL.Class('ControllerHintCtrl', autoCalcOrderUICtrl.AutoCalcOrderUICtrl)





ControllerHintCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_CONTROLLER_HINT] = 'HideControllerHint',

    [MessageConst.REFRESH_CONTROLLER_HINT] = 'RefreshControllerHint',
    [MessageConst.REFRESH_CONTROLLER_HINT_CONTENT_IMMEDIATELY] = 'RefreshContentImmediately',
    [MessageConst.REFRESH_CONTROLLER_HINT_ORDER] = 'PanelOrderChanged',

    [MessageConst.PLAY_CONTROLLER_HINT_OUT_ANIM] = 'PlayOutAnim',
}

ControllerHintCtrl.m_barCellCache = HL.Field(HL.Forward('CommonCache'))

ControllerHintCtrl.m_curBarCells = HL.Field(HL.Table)



ControllerHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_attachedPanels = {}
    self.m_curBarCells = {}

    self.view.barCell.gameObject:SetActive(false)
    self.m_barCellCache = require_ex("Common/Utils/CommonCache")(function()
        local obj = CSUtils.CreateObject(self.view.barCell.gameObject, self.view.main.transform)
        local barCell = Utils.wrapLuaNode(obj)
        return barCell
    end, function(barCell)
        barCell.gameObject:SetActive(true)
    end, function(barCell)
        barCell:Clear()
        barCell.gameObject:SetActive(false)
    end)

    self.m_lateTickFunc = function()
        self:_LateTick()
    end
end
















ControllerHintCtrl.ShowControllerHint = HL.StaticMethod(HL.Table) << function(args)


    args.placeHolderPath = NotNull(args.placeHolderObject) and args.placeHolderObject.transform:PathFromRoot() or tostring(args.panelId)

    local self = ControllerHintCtrl.AutoOpen(PANEL_ID, nil)
    local newPanelArgs = self:_AddPanelArgs(args)
    self:_AttachToPanel(newPanelArgs)
end

ControllerHintCtrl.ToggleControllerHint = HL.StaticMethod(HL.Any) << function(args)
    local needShow, key = unpack(args)
    if needShow then
        UIManager:ShowWithKey(PANEL_ID, key)

        local self = ControllerHintCtrl.AutoOpen(PANEL_ID, nil)
        self:RefreshContentImmediately()
    else
        UIManager:HideWithKey(PANEL_ID, key)
    end
end

ControllerHintCtrl._AddPanelArgs = HL.Method(HL.Table).Return(HL.Table) << function(self, args)
    local panelId = args.panelId
    local panelArgs = self.m_attachedPanels[panelId]
    if not panelArgs then
        panelArgs = {
            panelId = panelId,
            offset = args.offset,
            count = 0,
            subArgs = {},
        }
    end
    if not panelArgs.subArgs[args.placeHolderObject] then
        panelArgs.count = panelArgs.count + 1
    end
    panelArgs.subArgs[args.placeHolderObject] = args
    return panelArgs
end

ControllerHintCtrl.HideControllerHint = HL.Method(HL.Table) << function(self, args)
    local panelId = args.panelId
    local panelArgs = self.m_attachedPanels[panelId]
    if not panelArgs then
        return
    end
    local placeHolderObject = args.placeHolderObject
    if placeHolderObject then
        panelArgs.subArgs[placeHolderObject] = nil
        if not next(panelArgs.subArgs) then
            self:_CustomHide(panelId)
        else
            panelArgs.count = panelArgs.count - 1
            self:_OnBarChanged(false)
        end
    else
        self:_CustomHide(panelId)
    end
end

ControllerHintCtrl.RefreshControllerHint = HL.Method() << function(self)

    for _, barCell in pairs(self.m_curBarCells) do
        barCell.keyHintCells:Update(function(cell)
            cell.actionKeyHint:UpdateKeyHint()
        end)
    end
end

ControllerHintCtrl.CustomSetPanelOrder = HL.Override(HL.Opt(HL.Number, HL.Table)) << function(self, maxOrder, args)
    self:SetSortingOrder(maxOrder, false)
    self.m_curArgs = args
    if self:IsHide() then
        self:Show()
    elseif not self:IsPlayingAnimationIn() then
        self.animationWrapper:SampleToInAnimationEnd()
    end
    self:_OnBarChanged(true)
end

ControllerHintCtrl.RefreshContentImmediately = HL.Method() << function(self)

    self:_OnBarChanged(false)
end

ControllerHintCtrl._OnBarChanged = HL.Method(HL.Boolean) << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if not self.m_curArgs then
        return
    end
    if self:IsPlayingAnimationOut() then
        return
    end

    local subArgs = self.m_curArgs.subArgs
    local newCells = {}


    for obj, cell in pairs(self.m_curBarCells) do
        local args = subArgs[obj]
        if not args then
            self.m_barCellCache:Cache(cell)
        else
            newCells[obj] = cell
        end
    end

    for obj, args in pairs(subArgs) do
        local cell = newCells[obj]
        if not cell then

            cell = self.m_barCellCache:Get()
            newCells[obj] = cell
        end

        cell:InitControllerHintBarCell(args, false)
        self:_RefreshSingleBarContent(cell, true)
    end

    self.m_curBarCells = newCells
end

ControllerHintCtrl._RefreshAllBars = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if not self.m_curArgs then
        return
    end
    if self:IsPlayingAnimationOut() then
        return
    end
    local subArgs = self.m_curArgs.subArgs
    for panelObj, barCell in pairs(self.m_curBarCells) do

        self:_RefreshSingleBarContent(barCell, false)
    end
end

ControllerHintCtrl._RefreshSingleBarContent = HL.Method(HL.Forward('ControllerHintBarCell'), HL.Boolean) << function(self, barCell, isInit)
    local args = barCell.args

    local needMouseHint = args.isMain and not self:IsPlayingAnimationIn()
    local infoList = barCell:GetKeyHintInfos(needMouseHint)

    local count = #infoList
    if count > 0 then
        if barCell.keyHintCells:GetCount() == 0 then
            isInit = true
        end

        barCell.keyHintCells:Refresh(count, function(cell, index)
            local info = infoList[index]
            barCell:_UpdateCell(cell, info)
        end, nil, function(cell)
            barCell:_ClearRedDot(cell)
            cell.actionKeyHint:SetActionId(nil)
        end)

        self:_SetContentTransformState(barCell, args, isInit, args.useBG)
        barCell:RefreshBarBgVisibleState()
        barCell.gameObject:SetActive(true)
    else
        barCell.gameObject:SetActive(false)

        barCell.keyHintCells:Refresh(0, nil, nil, function(cell)
            barCell:_ClearRedDot(cell)
            cell.actionKeyHint:SetActionId(nil)
        end)
    end
end

ControllerHintCtrl._SetContentTransformState = HL.Method(HL.Forward('ControllerHintBarCell'), HL.Table, HL.Boolean, HL.Boolean) <<
function(self, barCell, args, isInit, showBg)
    if IsNull(args.transform) then


        if not args.__hintTransformWarned then
            args.__hintTransformWarned = true
            logger.error(string.format(
                "[ControllerHint] transform 缺失，跳过提示条定位。panelId=%s posType=%s placeHolder=%s",
                tostring(args.panelId), tostring(args.posType), tostring(args.placeHolderPath)), args.groupIds)
        end
        return
    end
    local targetScreenRect = UIUtils.getTransformScreenRect(args.transform, self.uiCamera)
    local posType = args.posType
    local useFullBG = args.useFullBG
    local barCellView = barCell.view
    local originalPaddingLeft = self.view.barCell.view.contentHorizontalLayoutGroup.padding.left
    local originalPaddingRight = self.view.barCell.view.contentHorizontalLayoutGroup.padding.right


    if posType == UIConst.CONTROLLER_HINT_POS_TYPE.Center then
        if isInit then
            barCellView.contentHorizontalLayoutGroup.childAlignment = CS.UnityEngine.TextAnchor.MiddleCenter
            barCellView.contentHorizontalLayoutGroup.padding.left = originalPaddingLeft
            barCellView.contentHorizontalLayoutGroup.padding.right = originalPaddingLeft
            barCellView.centerBG.gameObject:SetActive(showBg and not useFullBG)
            barCellView.leftBG.gameObject:SetActive(false)
            barCellView.rightBG.gameObject:SetActive(false)
            barCellView.fullBG.gameObject:SetActive(showBg and useFullBG)
        end
        local pos = UIUtils.screenPointToUI(targetScreenRect.center, self.uiCamera, self.view.transform)
        pos.x = 0
        pos.y = -pos.y
        barCellView.rectTransform.anchoredPosition = pos
    elseif posType == UIConst.CONTROLLER_HINT_POS_TYPE.Left then
        if isInit then
            barCellView.contentHorizontalLayoutGroup.childAlignment = CS.UnityEngine.TextAnchor.MiddleLeft
            barCellView.contentHorizontalLayoutGroup.padding.left = originalPaddingRight
            barCellView.contentHorizontalLayoutGroup.padding.right = originalPaddingLeft
            barCellView.centerBG.gameObject:SetActive(false)
            barCellView.leftBG.gameObject:SetActive(showBg and not useFullBG)
            barCellView.rightBG.gameObject:SetActive(false)
            barCellView.fullBG.gameObject:SetActive(showBg and useFullBG)
        end
        local pos = Vector2(targetScreenRect.xMin, targetScreenRect.yMax)
        pos = UIUtils.screenPointToUI(pos, self.uiCamera, self.view.transform)
        pos.y = self.view.transform.rect.size.y / 2 - pos.y
        pos.x = self.view.transform.rect.size.x / 2 + pos.x
        barCellView.rectTransform.anchoredPosition = pos
    elseif posType == UIConst.CONTROLLER_HINT_POS_TYPE.Right then
        if isInit then
            barCellView.contentHorizontalLayoutGroup.childAlignment = CS.UnityEngine.TextAnchor.MiddleRight
            barCellView.contentHorizontalLayoutGroup.padding.left = originalPaddingLeft
            barCellView.contentHorizontalLayoutGroup.padding.right = originalPaddingRight
            barCellView.centerBG.gameObject:SetActive(false)
            barCellView.leftBG.gameObject:SetActive(false)
            barCellView.rightBG.gameObject:SetActive(showBg and not useFullBG)
            barCellView.fullBG.gameObject:SetActive(showBg and useFullBG)
        end
        local pos = Vector2(targetScreenRect.xMax, targetScreenRect.yMax)
        pos = UIUtils.screenPointToUI(pos, self.uiCamera, self.view.transform)
        pos.y = self.view.transform.rect.size.y / 2 - pos.y
        pos.x = - (self.view.transform.rect.size.x / 2 - pos.x)
        barCellView.rectTransform.anchoredPosition = pos
    end


    if isInit then
        local viewPoint
        if posType == UIConst.CONTROLLER_HINT_POS_TYPE.Center then
            viewPoint = Vector2.one / 2
        elseif posType == UIConst.CONTROLLER_HINT_POS_TYPE.Left then
            viewPoint = Vector2.zero
        elseif posType == UIConst.CONTROLLER_HINT_POS_TYPE.Right then
            viewPoint = Vector2(1, 0)
        end
        barCellView.rectTransform.pivot = viewPoint
        barCellView.rectTransform.anchorMin = viewPoint
        barCellView.rectTransform.anchorMax = viewPoint
    end


    if useFullBG then
        local fullBgTrans = barCellView.fullBG.transform
        if isInit then
            fullBgTrans.pivot = args.rectTransform.pivot
        end
        fullBgTrans.position = args.transform.position
        if DeviceInfo.isMobile then
            local curMarginSize = (self.view.transform.rect.width - args.rectTransform.rect.width) / 2
            if fullBgTrans.pivot.x == 0 then

                local tmpPos = fullBgTrans.anchoredPosition
                tmpPos.x = tmpPos.x - curMarginSize
                fullBgTrans.anchoredPosition = tmpPos
            elseif fullBgTrans.pivot.x == 1 then

                local tmpPos = fullBgTrans.anchoredPosition
                tmpPos.x = tmpPos.x + curMarginSize
                fullBgTrans.anchoredPosition = tmpPos
            end
            UIUtils.setSizeDeltaX(fullBgTrans, self.view.transform.rect.width)
        else
            UIUtils.setSizeDeltaX(fullBgTrans, args.rectTransform.rect.width)
        end
    else
        if args.customWidth > 0 then
            UIUtils.setSizeDeltaX(barCellView.centerBG, args.customWidth)
        end
    end
end





ControllerHintCtrl.m_lateTickFunc = HL.Field(HL.Function)

ControllerHintCtrl.OnShow = HL.Override() << function(self)
    ControllerHintCtrl.Super.OnShow(self)
    InputManagerInst.onInputLateTick = InputManagerInst.onInputLateTick + self.m_lateTickFunc
end

ControllerHintCtrl.OnHide = HL.Override() << function(self)
    ControllerHintCtrl.Super.OnHide(self)
    InputManagerInst.onInputLateTick = InputManagerInst.onInputLateTick - self.m_lateTickFunc
    self:_ClearKeyHints()
end

ControllerHintCtrl.OnClose = HL.Override() << function(self)
    ControllerHintCtrl.Super.OnClose(self)
    InputManagerInst.onInputLateTick = InputManagerInst.onInputLateTick - self.m_lateTickFunc
    self:_ClearKeyHints()
end

ControllerHintCtrl._ClearKeyHints = HL.Method() << function(self)
    for _, barCell in pairs(self.m_curBarCells) do

        barCell.keyHintCells:Refresh(0, nil, nil, function(cell)
            barCell:_ClearRedDot(cell)
            cell.actionKeyHint:SetActionId(nil)
        end)
        self.m_barCellCache:Cache(barCell)
    end
    self.m_curBarCells = {}
end






ControllerHintCtrl._LateTick = HL.Method() << function(self)


    self:_RefreshAllBars()
end




HL.Commit(ControllerHintCtrl)
