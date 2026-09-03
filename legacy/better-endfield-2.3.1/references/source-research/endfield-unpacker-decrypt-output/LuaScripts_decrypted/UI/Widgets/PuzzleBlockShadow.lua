local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PuzzleBlockShadow = HL.Class('PuzzleBlockShadow', UIWidgetBase)

PuzzleBlockShadow.m_tweenCore = HL.Field(HL.Any)

PuzzleBlockShadow._OnDestroy = HL.Override() << function(self)
    if self.m_tweenCore then
        self.m_tweenCore:Kill()
    end
end


PuzzleBlockShadow._OnFirstTimeInit = HL.Override() << function(self)
end

PuzzleBlockShadow.InitPuzzleBlockShadow = HL.Method(HL.Table) << function(self, data)
    self:_FirstTimeInit()

    local calcPivot = UIUtils.calcPivotVecByData(data, self.config.PUZZLE_CELL_SIZE, self.config.PUZZLE_CELL_PADDING)
    self.view.viewRect.pivot = calcPivot
    self.view.viewRect.localPosition = Vector3.zero
    self.view.viewRect.localRotation = Quaternion.identity
    self.view.viewImage:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, data.resPath.."_block")
    self.view.viewImage:SetNativeSize()

    for _ = 1, data.rawRotationCount do
        self.view.viewRect:Rotate(0, 0, -90)
    end

    self:SetVisible(false)
end

PuzzleBlockShadow.Rotate = HL.Method(HL.Number) << function(self, rotateCount)
    self.m_tweenCore = self.view.viewRect:DORotate(Vector3(0, 0, -90 * (rotateCount % 4)), 0.2)
end

PuzzleBlockShadow.RecoverRotation = HL.Method(HL.Number) << function(self, rotateCount)
    self.view.viewRect.localRotation = Quaternion.Euler(0, 0, -90 * (rotateCount % 4))
end

PuzzleBlockShadow.SetPosition = HL.Method(Vector3) << function(self, position)
    self.view.rectTransform.position = position
end

PuzzleBlockShadow.SetVisible = HL.Method(HL.Boolean) << function(self, visible)
    self.view.gameObject:SetActiveIfNecessary(visible)
end

PuzzleBlockShadow.SetLegal = HL.Method(HL.Boolean) << function(self, legal)
    self.view.viewImage.color = legal and Color.white or Color.red
end

HL.Commit(PuzzleBlockShadow)
return PuzzleBlockShadow

