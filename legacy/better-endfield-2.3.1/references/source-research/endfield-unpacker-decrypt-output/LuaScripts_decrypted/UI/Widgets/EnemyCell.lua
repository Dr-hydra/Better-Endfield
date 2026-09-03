local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EnemyCell = HL.Class('EnemyCell', UIWidgetBase)


EnemyCell._OnFirstTimeInit = HL.Override() << function(self)
end

EnemyCell.InitEnemyCell = HL.Method(HL.Table, HL.Function) << function(self, info, onClickFunc)
    self:_FirstTimeInit()

    self.view.enemyImageN:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON, info.templateId)
    self.view.enemyImageS:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON, info.templateId)

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if onClickFunc then
            onClickFunc()
        end
    end)
end

EnemyCell.SetSelected = HL.Method(HL.Boolean) << function(self, selected)
    self.view.normalNode.gameObject:SetActiveIfNecessary(not selected)
    self.view.selectNode.gameObject:SetActiveIfNecessary(selected)
    self.view.selected.gameObject:SetActiveIfNecessary(selected)
end

HL.Commit(EnemyCell)
return EnemyCell

