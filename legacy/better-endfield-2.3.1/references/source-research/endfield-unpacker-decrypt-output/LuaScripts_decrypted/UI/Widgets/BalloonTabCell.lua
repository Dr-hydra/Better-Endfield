local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')


local ANIM_SELECT = "ballooncell_select"

local ANIM_REFRESH = "ballooncell_refresh"

local ANIM_TEXT_SCALE = "ballooncell_textscale"
local ANIM_STATE_OTHERS = CS.Beyond.UI.UIConst.AnimationState.Others
local DRAG_IMG_COLOR = Color(1, 1, 1, 1)

BalloonTabCell = HL.Class('BalloonTabCell', UIWidgetBase)

BalloonTabCell.m_ctrl = HL.Field(HL.Any)

BalloonTabCell.m_index = HL.Field(HL.Number) << -1

BalloonTabCell.m_liftNum = HL.Field(HL.Number) << 0

BalloonTabCell.m_lastDisplayCount = HL.Field(HL.Number) << -1

BalloonTabCell.m_lastLeftCount = HL.Field(HL.Number) << -1

BalloonTabCell.m_isPlayingSelectAnim = HL.Field(HL.Boolean) << false

BalloonTabCell.m_isPlayingCountChangeAnim = HL.Field(HL.Boolean) << false


BalloonTabCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.balloonDragItem.onBeginDragEvent:RemoveAllListeners()
    self.view.balloonDragItem.onBeginDragEvent:AddListener(function(eventData)
        if not self.m_ctrl then
            return
        end
        self.m_ctrl.m_phase.nowDragTabItemData = {
            liftNum = self.m_liftNum
        }
        if self.m_ctrl.m_phase then
            self.m_ctrl.m_phase:OnBeginDragCell(eventData)
            self.m_ctrl:RefreshAllTabCellView()
        end
    end)

    self.view.balloonDragItem.onDragEvent:RemoveAllListeners()
    self.view.balloonDragItem.onDragEvent:AddListener(function(eventData)
        if not IsNull(self.view.balloonDragItem.curDragObj) then
            self.view.balloonDragItem.curDragObj.gameObject:SetActive(true)
        end
        if self.m_ctrl and self.m_ctrl.m_phase then
            self.m_ctrl.m_phase:OnDraggingCell(eventData)
        end
    end)

    self.view.balloonDragItem.onEndDragEvent:RemoveAllListeners()
    self.view.balloonDragItem.onEndDragEvent:AddListener(function(eventData)
        if self.m_ctrl and self.m_ctrl.m_phase then
            self.m_ctrl.m_phase:OnEndDragCell(eventData)
            self:_UpdateSelectAnimation()
        end
    end)

    self.view.balloonDragItem.onUpdateDragObject:RemoveAllListeners()
    self.view.balloonDragItem.onUpdateDragObject:AddListener(function(dragObj, srcObj)
        self:_RefreshDragObjectView(dragObj)
    end)

    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(active)
        if not self.m_ctrl or not self.m_ctrl.m_phase then
            return
        end
        local blockTypeEnum = self.m_ctrl.m_balloonTypes[self.m_index]
        if not blockTypeEnum then
            return
        end
        local count = self.m_ctrl.m_phase.curLevel:GetLeftCountByType(blockTypeEnum)
        if active then
            self.m_ctrl.balloonLiftNum = self.m_liftNum
            self.m_ctrl.m_phase:OnEndDrag()
            InputManagerInst:ToggleBinding(self.m_ctrl.m_tabEquipBalloonBindingId, count > 0)
            self.m_ctrl:ToggleChessboardSelectBindingId(false)
            self.m_ctrl:ToggleChessboardBindingId(false)
            self.m_ctrl:ToggleSwitch2ChessboardOrListBindingId(true)
        elseif self.m_liftNum == self.m_ctrl.balloonLiftNum then
            self.m_ctrl.balloonLiftNum = -1
            InputManagerInst:ToggleBinding(self.m_ctrl.m_tabEquipBalloonBindingId, false)
            self.m_ctrl:ToggleSwitch2ChessboardOrListBindingId(false)
        end
    end)
end

BalloonTabCell.InitBalloonTabCell = HL.Method(HL.Any, HL.Number, HL.Number) << function(self, ctrl, index, liftNum)
    self:_FirstTimeInit()
    if self.m_index == index and liftNum == self.m_liftNum then
        self:RefreshCellView()
        return
    end
    self.m_ctrl = ctrl
    self.m_index = index
    self.m_liftNum = liftNum
    self.m_lastDisplayCount = -1
    self.m_lastLeftCount = -1
    self.m_isPlayingSelectAnim = false
    self.m_isPlayingCountChangeAnim = false

    self.view.balloonDragItem.luaTable = {
        liftNum = liftNum,
    }
    self:RefreshCellView()
end

BalloonTabCell._RefreshDragObjectView = HL.Method(CS.UnityEngine.GameObject) << function(self, dragObj)
    if IsNull(dragObj) then
        return
    end
    local luaReference = dragObj:GetComponent(typeof(CS.Beyond.Lua.LuaReference))
    if not luaReference then
        return
    end

    local ref = {}
    luaReference:BindToLua(ref)
    if ref.image then
        ref.image.color = DRAG_IMG_COLOR
        ref.image:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BALLOON, "icon_balloon_type_" .. self.m_liftNum)
    end
    if ref.image1 then
        ref.image1.color = DRAG_IMG_COLOR
        ref.image1.fillAmount = 0
    end
    if ref.image2 then
        ref.image2.gameObject:SetActive(false)
    end
    if ref.dragItem then
        ref.dragItem.enabled = false
    end
end

BalloonTabCell._IsDraggingSelf = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_ctrl or not self.m_ctrl.m_phase or not self.m_ctrl.m_phase.nowDragTabItemData then
        return false
    end
    return self.m_ctrl.m_phase.nowDragTabItemData.liftNum == self.m_liftNum
end

BalloonTabCell._PlaySelectAnimation = HL.Method() << function(self)
    if self.m_isPlayingSelectAnim then
        return
    end
    self.m_isPlayingSelectAnim = true
    self.view.animationWrapper:Play(ANIM_SELECT, nil, ANIM_STATE_OTHERS, 0, true)
end

BalloonTabCell._StopSelectAnimation = HL.Method() << function(self)
    self.m_isPlayingSelectAnim = false
    self.view.animationWrapper:ClearTween(false)
    self.view.animationWrapper:SampleClip(ANIM_SELECT, 0, false)
end

BalloonTabCell._UpdateSelectAnimation = HL.Method() << function(self)
    if self.m_isPlayingCountChangeAnim then
        return
    end
    if self:_IsDraggingSelf() then
        self:_PlaySelectAnimation()
    else
        self:_StopSelectAnimation()
    end
end

BalloonTabCell._OnCountChangeAnimationFinished = HL.Method() << function(self)
    self.m_isPlayingCountChangeAnim = false
    self:_UpdateSelectAnimation()
end

BalloonTabCell._PlayCountChangeAnimation = HL.Method(HL.Boolean, HL.Boolean) << function(self, playRefresh, playTextScale)
    if not playRefresh and not playTextScale then
        self:_UpdateSelectAnimation()
        return
    end

    if playRefresh then
        self:_StopSelectAnimation()
        self.m_isPlayingCountChangeAnim = true
    end
    if playRefresh then
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_Put_B")
        self.view.animationWrapper:Play(ANIM_REFRESH, function()
            self:_OnCountChangeAnimationFinished()
        end, ANIM_STATE_OTHERS, 0, false)
    end
    if playTextScale then
        self.view.balloonTxtAnimationWrapper:Play(ANIM_TEXT_SCALE)
        if not playRefresh then
            self:_UpdateSelectAnimation()
        end
    end
end

BalloonTabCell.RefreshCellView = HL.Method() << function(self)
    if not self.m_ctrl or not self.m_ctrl.m_phase then
        return
    end
    local blockTypeEnum = self.m_ctrl.m_balloonTypes[self.m_index]
    if not blockTypeEnum then
        return
    end
    local curLevel = self.m_ctrl.m_phase.curLevel

    local leftCount = curLevel:GetLeftCountByType(blockTypeEnum)

    local isDraggingTabLiftNum = self.m_ctrl.m_phase.nowDragTabItemData and self.m_ctrl.m_phase.nowDragTabItemData.liftNum == self.m_liftNum
    local displayCount = isDraggingTabLiftNum and math.max(leftCount - 1, 0) or leftCount
    local displayCountText = tostring(displayCount)
    local hasLastDisplayCount = self.m_lastDisplayCount >= 0
    local hasLastLeftCount = self.m_lastLeftCount >= 0
    local suppressCountAnim = self.m_ctrl:IsSuppressTabCellCountAnim()

    local playTextScale = not suppressCountAnim and hasLastDisplayCount and self.view.balloonTxt.text ~= displayCountText

    local tabDragReturned = hasLastDisplayCount and displayCount > self.m_lastDisplayCount and leftCount == self.m_lastLeftCount

    local playRefresh = not suppressCountAnim and hasLastLeftCount and (leftCount > self.m_lastLeftCount or tabDragReturned)

    self.view.stateController:SetState(displayCount > 0 and "Normal" or "Empty")
    self.view.balloonDragItem.enabled = leftCount > 0
    if leftCount <= 0 and not IsNull(self.view.balloonDragItem.curDragObj) then
        self.view.balloonDragItem.curDragObj.gameObject:SetActive(false)
    end

    self.view.forceTxt.text = self.m_liftNum
    self.view.balloonTxt.text = displayCountText
    self.view.balloonTypeTxt.text = Language[MiniGameBalloonUtils.BALLOON_TYPE_NAME[blockTypeEnum]]
    self.view.balloonImg:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BALLOON, "icon_balloon_type_" .. self.m_liftNum)
    self.m_lastDisplayCount = displayCount
    self.m_lastLeftCount = leftCount
    self:_PlayCountChangeAnimation(playRefresh, playTextScale)
end

HL.Commit(BalloonTabCell)
return BalloonTabCell

