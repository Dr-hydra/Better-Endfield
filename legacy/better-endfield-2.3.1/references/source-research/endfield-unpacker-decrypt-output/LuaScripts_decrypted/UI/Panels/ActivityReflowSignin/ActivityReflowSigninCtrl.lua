

local activityCharSignCommonCtrl = require_ex('UI/Panels/ActivityCharSignCommon/ActivityCharSignCommonCtrl')
local PANEL_ID = PanelId.ActivityReflowSignin

ActivityReflowSigninCtrl = HL.Class('ActivityReflowSigninCtrl', activityCharSignCommonCtrl.ActivityCharSignCommonCtrl)

HL.Commit(ActivityReflowSigninCtrl)