
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PuzzleTrackPopup
local PHASE_ID = PhaseId.PuzzleTrackPopup

PuzzleTrackPopupCtrl = HL.Class('PuzzleTrackPopupCtrl', uiCtrl.UICtrl)

PuzzleTrackPopupCtrl.m_luaIndex = HL.Field(HL.Number) << -1

PuzzleTrackPopupCtrl.m_totalNum = HL.Field(HL.Number) << -1

PuzzleTrackPopupCtrl.m_blocks = HL.Field(HL.Table)

PuzzleTrackPopupCtrl.m_animationWrapper = HL.Field(HL.Any)





PuzzleTrackPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


PuzzleTrackPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnReturn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnEmpty.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnJump.onClick:AddListener(function()
        self:_OnBtnJump()
    end)

    self.view.btnLeft.onClick:AddListener(function()
        local target = self.m_luaIndex - 1
        self.m_luaIndex = target < 1 and self.m_totalNum or target
        self:_RefreshBlock()
    end)

    self.view.btnRight.onClick:AddListener(function()
        local target = self.m_luaIndex + 1
        self.m_luaIndex = target > self.m_totalNum and 1 or target
        self:_RefreshBlock()
    end)

    self.m_animationWrapper = self.animationWrapper

    self.m_blocks = arg.blocks
    self.m_totalNum = #arg.blocks

    local multiple = self.m_totalNum > 1
    self.view.txtNumber.gameObject:SetActive(multiple)
    self.view.btnLeft.gameObject:SetActive(multiple)
    self.view.btnRight.gameObject:SetActive(multiple)

    self.m_luaIndex = 1
    local selectedBlockId = arg.selectBlockId
    for luaIndex, blockData in ipairs(arg.blocks) do
        if blockData.rawId == selectedBlockId then
            self.m_luaIndex = luaIndex
            break
        end
    end

    self:_RefreshBlock()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

PuzzleTrackPopupCtrl._RefreshBlock = HL.Method() << function(self)
    self.view.imageAnimation:Play("puzzletrackpopup_image_in")

    local block = self.m_blocks[self.m_luaIndex]
    self.view.image:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, block.resPath..UIConst.UI_MINIGAME_PUZZLE_GREY_BLOCK_SUFFIX)
    self.view.txtNumber.text = string.format("%d/%d", self.m_luaIndex, self.m_totalNum)
end

PuzzleTrackPopupCtrl._OnBtnJump = HL.Method() << function(self)
    local mapMgr = GameInstance.player.mapManager
    local block = self.m_blocks[self.m_luaIndex]
    local succ, instId = mapMgr:GetMapMarkInstId(GEnums.MarkType.PuzzlePiece, block.rawId)

    if succ then
        MapUtils.openMapAndSetMarkVisibleIfNecessary(instId)
    else
        self:Notify(MessageConst.SHOW_TOAST, string.format("id为%s的拼块未获取到instId", block.rawId))
    end
end

HL.Commit(PuzzleTrackPopupCtrl)
