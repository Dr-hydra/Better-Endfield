local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ControllerSideMenuBtn = HL.Class('ControllerSideMenuBtn', UIWidgetBase)


ControllerSideMenuBtn.m_extraArgs = HL.Field(HL.Table)



ControllerSideMenuBtn._OnFirstTimeInit = HL.Override() << function(self)
    self.view.button.onClick:AddListener(function()
        self:_OpenMenu()
    end)
end

ControllerSideMenuBtn.InitControllerSideMenuBtn = HL.Method(HL.Opt(HL.Table)) << function(self, extraArgs)
    self:_FirstTimeInit()
    self.m_extraArgs = extraArgs or {}
end

ControllerSideMenuBtn._OpenMenu = HL.Method() << function(self)
    local args = {
        title = Language[self.view.menuBtnList.title],
        menuBtnList = self.view.menuBtnList,
        btnInfos = {},
        hintPlaceholder = self:GetUICtrl().view.controllerHintPlaceholder,
    }

    local csCount = self.view.menuBtnList.menuItems.Count
    for k, v in pairs(self.view.menuBtnList.menuItems) do
        v.priority = k
        if v:IsValid() then
            table.insert(args.btnInfos, v)
        end
    end
    if self.m_extraArgs.extraBtnInfos then
        for k, v in ipairs(self.m_extraArgs.extraBtnInfos) do
            v.priority = v.priority or (csCount + k)
            table.insert(args.btnInfos, v)
        end
    end
    table.sort(args.btnInfos, Utils.genSortFunction({"priority"}, true))

    if self.m_extraArgs then
        setmetatable(args, { __index = self.m_extraArgs })
    end

    if self.m_extraArgs.preOpenMenu then
        self.m_extraArgs.preOpenMenu()
    end
    UIManager:Open(PanelId.ControllerSideMenu, args)
end

HL.Commit(ControllerSideMenuBtn)
return ControllerSideMenuBtn
