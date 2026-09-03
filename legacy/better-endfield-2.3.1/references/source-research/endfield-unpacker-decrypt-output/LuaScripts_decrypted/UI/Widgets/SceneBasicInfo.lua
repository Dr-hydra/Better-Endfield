local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SceneBasicInfo = HL.Class('SceneBasicInfo', UIWidgetBase)


SceneBasicInfo._OnFirstTimeInit = HL.Override() << function(self)

end






SceneBasicInfo.InitSceneBasicInfo = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local hasValue

    local levelDescData
    hasValue, levelDescData = Tables.levelDescTable:TryGetValue(args.levelId)
    if hasValue then
        self.view.txtName:SetPhoneticText(GEnums.PhoneticType.DomainNamePhonetic, args.levelId)
    end

    self.view.btn.targetGraphic.raycastTarget = false

    self.view.btn.onClick:RemoveAllListeners()
    if args.onClick then
        self.view.btn.targetGraphic.raycastTarget = true
        self.view.btn.onClick:AddListener(function()
            args.onClick(args.levelId)
        end)
    end

    self.view.btn.onHoverChange:RemoveAllListeners()
    if args.onHoverChanged then
        self.view.btn.targetGraphic.raycastTarget = true
        self.view.btn.onHoverChange:AddListener(function(isHover)
            args.onHoverChanged(args.levelId, isHover)
        end)
    end
end

HL.Commit(SceneBasicInfo)
return SceneBasicInfo

