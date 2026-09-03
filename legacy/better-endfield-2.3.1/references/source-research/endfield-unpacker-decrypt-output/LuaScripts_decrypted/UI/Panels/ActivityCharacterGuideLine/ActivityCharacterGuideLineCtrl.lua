
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCharacterGuideLine

ActivityCharacterGuideLineCtrl = HL.Class('ActivityCharacterGuideLineCtrl', uiCtrl.UICtrl)






ActivityCharacterGuideLineCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityCharacterGuideLineCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityCharacterGuideLineCtrl.m_bgNode = HL.Field(HL.Any)


ActivityCharacterGuideLineCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local suc,info = Tables.activityCharacterGuideLineTable:TryGetValue(args.activityId)
    if suc then
        local path = string.format(UIConst.UI_ACTIVITY_CHAR_GUIDE_LINE_PREFAB_PATH, info.bgNode)
        local prefab = self:LoadGameObject(path)
        if self.m_bgNode then
            CSUtils.ClearUIComponents(self.m_bgNode)
            GameObject.DestroyImmediate(self.m_bgNode)
        end
        self.m_bgNode = CSUtils.CreateObject(prefab, self.view.bgNode)
        self.m_bgNode.name = "BgNodeMain"
        self.view.btnDetailRedDot:InitRedDot("ActivityCharacterGuideLineBtnDetail", self.m_activityId)
        self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
            ActivityUtils.setFalseNewUnlockCharacterGuideLine(self.m_activityId)
        end)
    end
end

HL.Commit(ActivityCharacterGuideLineCtrl)
