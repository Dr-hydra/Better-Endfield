local Types = {}

Types.EPanelOrderTypes = {
    UI3D = 1,
    BottomScreenEffect = 2,
    LowerHud = 3,
    Hud = 4,
    HudToast = 5,
    TopScreenEffect = 6,
    Window = 7,


    EmptyConfigWindow = 8,

    PopUp = 9,
    Guide = 10,
    Toast = 11,
    Loading = 12,
    FMV = 13,
    SystemToast = 14,
    System = 15,
    Debug = 16,
}
Types.MaxPanelOrderType = Types.EPanelOrderTypes.Debug

Types.EPanelMultiTouchTypes = {
    Both = 1,
    Enable = 2,
    Disable = 3,
}

Types.EInputBindingScope = {
    EditorOnly = 1,
    IncludeDev = 2,
    IncludeStandalone = 3,
}

Types.EPanelGyroscopeEffect = {
    Both = 1,
    Enable = 2,
    Disable = 3,
}

Types.EPanelMouseMode = {
    NeedShow = 1,
    NotNeedShow = 2,
    AutoShow = 3,
    ForceHide = 4,
}


_G.Types = Types
return Types
