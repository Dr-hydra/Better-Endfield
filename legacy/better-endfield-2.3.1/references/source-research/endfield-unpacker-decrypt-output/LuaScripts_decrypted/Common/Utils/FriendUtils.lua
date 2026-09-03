local FriendUtils = {}

local checkInputValid = function(str)

    return true, 'normal'
end

FriendUtils.FriendShowCharsCount = 4

FriendUtils.FriendDictIndex = 0
FriendUtils.BlackListDictIndex = 1
FriendUtils.FriendRequestDictIndex = 2
FriendUtils.StrangerDictIndex = 3
FriendUtils.NewFriendSearchDictIndex = 4

FriendUtils.ReportGroupType = {
    BusinessCard = 1,
    FriendList = 2,
    Blueprint = 3,
    SocialBuilding = 4,
}

FriendUtils.FRIEND_CELL_HEAD_FUNC = {
    BUSINESS_CARD = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_SHOW_BUSINESS_CARD,
            action = function()
                Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = id, isPhase = false })
            end
        }
    end,
    BUSINESS_CARD_PHASE = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_SHOW_BUSINESS_CARD,
            action = function()
                Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = id })
            end
        }
    end,
    REMOVE_FRIEND = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_REMOVE_FRIEND,
            action = function()
                local nameStr = ""
                local info = GameInstance.player.friendSystem.friendInfoDic[id]
                if info.remakeName and not string.isEmpty(info.remakeName) then
                    nameStr = string.format(Language.LUA_FRIEND_REMAKE_NAME, info.remakeName, info.name, info.shortId)
                else
                    nameStr = string.format(Language.LUA_FRIEND_NAME, info.name, info.shortId)
                end
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_FRIEND_TIP_REMOVE_FRIEND_CONTENT,
                    subContent = nameStr,
                    onConfirm = function()
                        Notify(MessageConst.FRIEND_CHAT_PLAYER_DELETE_LIST_CELL)
                        GameInstance.player.friendSystem:FriendDelete(id)
                    end
                })
            end
        }
    end,
    REMARK_MODIFY = function(id)
        local inputName = GameInstance.player.friendSystem.friendInfoDic[id].remakeName
        local arg = {
            content = Language.LUA_FRIEND_TIP_REMARK_MODIFY_CONTENT,
            input = true,
            inputHintText = Language.LUA_FRIEND_TIP_REMARK_MODIFY_SUB_CONTENT,
            inputPlaceholder = inputName,
            characterLimit = 15,
            checkInputValid = checkInputValid,
            onConfirm = function(changedName)
                GameInstance.player.friendSystem:RemarkManeModify(id, changedName)
            end
        }
        arg.inputPlaceholder = Language.LUA_FRIEND_TIP_REMARK_MODIFY_CONTENT
        if not string.isEmpty(inputName) then
            arg.inputName = inputName
        end
        return {
            text = Language.LUA_FRIEND_TIP_REMARK_MODIFY,
            action = function()
                Notify(MessageConst.SHOW_POP_UP, arg)
            end
        }
    end,
    ADD_BLACK_LIST = function(id)
        local nameStr = ""

        local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(id)
        if info.remakeName and not string.isEmpty(info.remakeName) then
            nameStr = string.format(Language.LUA_FRIEND_REMAKE_NAME, info.remakeName, info.name, info.shortId)
        else
            nameStr = string.format(Language.LUA_FRIEND_NAME, info.name, info.shortId)
        end
        return {
            text = Language.LUA_FRIEND_TIP_ADD_BLACK_LIST,
            action = function()
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_FRIEND_TIP_ADD_BLACK_LIST_CONTENT,
                    subContent = nameStr,
                    onConfirm = function()
                        Notify(MessageConst.FRIEND_CHAT_PLAYER_DELETE_LIST_CELL)
                        GameInstance.player.friendSystem:BlackListAdd(id)
                    end
                })
            end
        }
    end,
    REPORT = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_REPORT,
            action = function()
                UIManager:Open(PanelId.ReportPlayer, { roleId = id, reportType = FriendUtils.ReportGroupType.FriendList })
            end
        }
    end,
    REPORT_BUSINESS_CARD = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_REPORT,
            action = function()
                UIManager:Open(PanelId.ReportPlayer, { roleId = id, reportType = FriendUtils.ReportGroupType.BusinessCard })
            end
        }
    end,
    CHAR_INFO = function(callBack)
        return {
            text = Language.LUA_FRIEND_TIP_CHAR_INFO,
            action = function()
                callBack()
            end
        }
    end,
    CHAT = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_CHAT,
            action = function()


                if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem.isPSNOnly and not GameInstance.player.friendSystem:IsPsnFriend(id) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PSN_CHAT)
                    return
                end


                if GameInstance.player.friendSystem.isCommunicationRestricted then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PARENTAL_CONTROL_CHAT)
                    return
                end

                FriendUtils.FRIEND_CELL_INIT_FUNC.onMessageClick(id)
            end
        }
    end,
    VISIT_SHIP = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_VISIT_SHIP,
            action = function()
                FriendUtils.FRIEND_CELL_INIT_FUNC.onShipClick(id)
            end
        }
    end,
    ROLE_ID = function(id)
        return {
            text = "Role ID: " .. id,
            action = function()
                Unity.GUIUtility.systemCopyBuffer = id
                Notify(MessageConst.SHOW_TOAST, "Role ID copied to clipboard " .. id)
            end
        }
    end,
    SWITCH_AVATAR = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_SWITCH_AVATAR,
            action = function()
                PhaseManager:OpenPhase(PhaseId.FriendHeadSelectedPopUp)
            end
        }
    end,

    SIGNATURE_MODIFY = function(id)
        return {
            text = Language.LUA_SIGNATURE_MODIFY,
            action = function()
                Notify(MessageConst.SHOW_POP_UP, {
                    content = Language.LUA_SIGNATURE_MODIFY_CONTENT,
                    inputMore = true,
                    inputHintMoreText = Language.LUA_FRIEND_TIP_SIGNATURE_MODIFY_SUB_CONTENT,
                    inputName = GameInstance.player.friendSystem.SelfInfo.signature,
                    characterLimit = 40,
                    checkInputValid = checkInputValid,
                    onConfirm = function(changedSignature)
                        GameInstance.player.friendSystem:SignatureModify(changedSignature)
                    end
                })
            end
        }
    end,

    BUSINESS_CARD_TOPIC_MODIFY = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_BUSINESS_CARD_THEME,
            action = function()
                PhaseManager:GoToPhase(PhaseId.FriendThemeChange)
            end
        }
    end,

    PSN_INFO = function(id)
        return {
            text = Language.LUA_FRIEND_TIP_PSN_INFO,
            action = function()
                local success, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(id)
                if success and friendInfo.psnData and not string.isEmpty(friendInfo.psnData.AccountId) then
                    FriendUtils.FRIEND_CELL_INIT_FUNC.onPsnInfoClick(friendInfo.psnData.AccountId)
                else
                    logger.error("FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO: No PSN data for roleId: " .. id)
                end
            end
        }
    end,

    ADD_BIRTHDAY_INFO = function(id)
        return {
            text = Language.LUA_FRIEND_SET_BIRTHDAY,
            redDot = "SetBirthday",
            action = function()
                UIManager:Open(PanelId.SetBirthdayPopUp)
            end
        }
    end,

    SHOW_BIRTHDAY = function(id)
        return {
            text = Language.LUA_FRIEND_SHOW_BIRTHDAY,
            action = function()
                GameInstance.player.friendSystem:SetBirthdayVisible(true)
            end
        }
    end,

    HIDE_BIRTHDAY = function(id)
        return {
            text = Language.LUA_FRIEND_HIDE_BIRTHDAY,
            action = function()
                GameInstance.player.friendSystem:SetBirthdayVisible(false)
            end
        }
    end,
    VIEW_RECORD_TIPS = function(id, rectTransform)
        return {
            text = Language.LUA_FRIEND_VIEW_RECORD_TIPS,
            action = function()
                UIManager:Open(PanelId.FriendBusinessRecordTips, {
                    roleId = id,
                    transform = rectTransform,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftAlignBottom,
                })
            end
        }
    end,
    SHOW_SEASON_TOWER_RECORD = function()
        return {
            text = Language.LUA_FRIEND_SHOW_SEASON_TOWER_RECORD,
            action = function()
                GameInstance.player.friendSystem:HideSeasonTowerRecordFlagModify(false)
            end
        }
    end,
    HIDE_SEASON_TOWER_RECORD = function()
        return {
            text = Language.LUA_FRIEND_HIDE_SEASON_TOWER_RECORD,
            action = function()
                GameInstance.player.friendSystem:HideSeasonTowerRecordFlagModify(true)
            end
        }
    end,
}

FriendUtils.CELL_HEIGHT = 60

FriendUtils.isSeasonTowerRecordDisplayToggleEnabled = function()
    return Tables.globalConst.enableSeasonTowerRecordDisplayToggle == true
end

FriendUtils.isSeasonTowerRecordVisible = function(record)
    return record ~= nil and not record.ForceHide and (not record.Hide or not FriendUtils.isSeasonTowerRecordDisplayToggleEnabled()) and not GameInstance.player.gameSettingSystem.forbiddenSeasonTower
end

FriendUtils.SEASON_TOWER_RANK_NAMES = {[0] = "None", [1] = "D", [2] = "C", [3] = "B", [4] = "A", [5] = "S", [6] = "SS"}

FriendUtils.hasAnyRecordTagVisible = function(roleId)
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    if not success then
        return false
    end
    if FriendUtils.getSeasonTowerDisplayRecord(playerInfo) then
        return true
    end
    local ccRecord = playerInfo.contingencyContractBestRecord
    if ccRecord.Item1 ~= nil and ccRecord.Item2 > 0 then
        local activityData = GameInstance.player.activitySystem:GetActivity(ccRecord.Item1)
        if activityData ~= nil and activityData.gameplayEndTime - DateTimeUtils.GetCurrentTimestampBySeconds() > 0 then
            return true
        end
    end
    return false
end

FriendUtils.getSeasonTowerDisplayRecord = function(playerInfo)
    if not playerInfo or not FriendUtils.isSeasonTowerRecordVisible(playerInfo.seasonTowerRecord) then
        return nil
    end
    return playerInfo.seasonTowerDisplayRecord
end

FriendUtils.hasSeasonTowerHistoryRecord = function(playerInfo)
    if not playerInfo or not FriendUtils.isSeasonTowerRecordVisible(playerInfo.seasonTowerRecord) then
        return false
    end
    local historyRecord = playerInfo.seasonTowerHistoryRecord
    return historyRecord ~= nil and historyRecord.Count > 0
end


FriendUtils.FRIEND_CELL_INIT_FUNC = {
    onFriendPlayerClick = function(rectTransform, id, charInfoCallBack, shipBtnShow)
        local args = {
            transform = rectTransform,
            cellHeight = FriendUtils.CELL_HEIGHT,
        }
        if DeviceInfo.inputType == DeviceInfo.InputType.Controller then
            args.actions = {}
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.CHAT(id))
            if DeviceInfo.inputType == DeviceInfo.InputType.Controller and GameInstance.player.friendSystem:GetCharInfoByRoleId(id).Count > 0 then
                table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.CHAR_INFO(charInfoCallBack))
            end
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id))

            if shipBtnShow then
                table.insert(args.actions, 1, FriendUtils.FRIEND_CELL_HEAD_FUNC.VISIT_SHIP(id))
            end
        else
            args.actions = {
                [1] = FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id),
                [2] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id),
                [3] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id),
                [4] = FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id),
                [5] = FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id),
            }
        end
        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        if BEYOND_DEBUG then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ROLE_ID(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,
    onStrangerPlayerClick = function(rectTransform, id, charInfoCallBack)
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = {}
        }
        if DeviceInfo.inputType == DeviceInfo.InputType.Controller and GameInstance.player.friendSystem:GetCharInfoByRoleId(id).Count > 0 then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.CHAR_INFO(charInfoCallBack))
        end
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id))
        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,
    onBusinessCardFriendPlayerClick = function(rectTransform, id, recordTipsTransform)
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = {}
        }

        local isFriend = GameInstance.player.friendSystem.friendInfoDic:ContainsKey(id) and not (FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem.isPSNOnly and not GameInstance.player.friendSystem:IsPsnFriend(id))

        if FriendUtils.hasAnyRecordTagVisible(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.VIEW_RECORD_TIPS(id, recordTipsTransform or rectTransform))
        end


        if isFriend then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.CHAT(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id))
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id))
        end
        if not GameInstance.player.friendSystem:PlayerInBlackList(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
        end
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT_BUSINESS_CARD(id))


        if GameInstance.player.friendSystem.isCommunicationRestricted then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PARENTAL_CONTROL_CHAT)
            return
        end

        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,
    onBusinessCardStrangerPlayerClick = function(rectTransform, id, recordTipsTransform)
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = {}
        }
        if FriendUtils.hasAnyRecordTagVisible(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.VIEW_RECORD_TIPS(id, recordTipsTransform or rectTransform))
        end
        if not GameInstance.player.friendSystem:PlayerInBlackList(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
        end
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT_BUSINESS_CARD(id))
        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,

    onCommonFriendPlayerClick = function(rectTransform, id)
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = {}
        }
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMARK_MODIFY(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REMOVE_FRIEND(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id))
        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,
    onCommonStrangerPlayerClick = function(rectTransform, id)
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = {}
        }
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BLACK_LIST(id))
        table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.REPORT(id))
        if FriendUtils.isPsnPlatform() and GameInstance.player.friendSystem:IsPsnFriend(id) then
            table.insert(args.actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.PSN_INFO(id))
        end
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,

    onSelfClick = function(rectTransform, id, recordTipsTransform)
        local actions = {}
        local tipsRect = recordTipsTransform or rectTransform
        local success, selfInfo = GameInstance.player.friendSystem:TryGetFriendInfo(id)
        if not FriendUtils.isSeasonTowerRecordDisplayToggleEnabled() then
            if FriendUtils.hasAnyRecordTagVisible(id) then
                table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.VIEW_RECORD_TIPS(id, tipsRect))
            end
        else
            local isHidden = success and selfInfo.seasonTowerRecord and selfInfo.seasonTowerRecord.Hide
            if isHidden then
                table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.SHOW_SEASON_TOWER_RECORD())
            else
                if FriendUtils.hasAnyRecordTagVisible(id) then
                    table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.VIEW_RECORD_TIPS(id, tipsRect))
                end
                table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.HIDE_SEASON_TOWER_RECORD())
            end
        end
        if GameInstance.player.friendSystem.isCommunicationRestricted then
            table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.SWITCH_AVATAR(id))
            table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_TOPIC_MODIFY(id))
        else
            table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.SWITCH_AVATAR(id))
            table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.SIGNATURE_MODIFY(id))
            table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_TOPIC_MODIFY(id))
        end
        if Utils.isSystemUnlocked(GEnums.UnlockSystemType.Birthday) then
            if GameInstance.player.friendSystem:IsBirthdayAlreadySet() then
                if GameInstance.player.friendSystem:IsBirthdayVisible(id) then
                    table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.HIDE_BIRTHDAY(id))
                else
                    table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.SHOW_BIRTHDAY(id))
                end
            else
                table.insert(actions, FriendUtils.FRIEND_CELL_HEAD_FUNC.ADD_BIRTHDAY_INFO(id))
            end
        end
        local args = {
            transform = rectTransform,
            cellHeight = 60,
            actions = actions
        }
        Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, args)
    end,

    onMessageClick = function(id)
        if not GameInstance.player.friendChatSystem:IsChatCreated(id) then
            GameInstance.player.friendChatSystem.luaCreateChatCallback = function()
                PhaseManager:GoToPhase(PhaseId.SNS, { roleId = id })
            end
            Notify(MessageConst.FRIEND_CHAT_PLAYER_ADD_LIST_CELL)
            GameInstance.player.friendChatSystem:CreateChat(id)
        else
            PhaseManager:GoToPhase(PhaseId.SNS, { roleId = id })
        end
    end,
    onShipClick = function(id)
        GameInstance.player.friendSystem:SendFriendVisitSpaceShip(id)
    end,
    onChatClick = function(id)

        if not GameInstance.player.friendChatSystem:IsChatCreated(id) then
            GameInstance.player.friendChatSystem.luaCreateChatCallback = function()
                PhaseManager:GoToPhase(PhaseId.SNS, { roleId = id })
            end
            Notify(MessageConst.FRIEND_CHAT_PLAYER_ADD_LIST_CELL)
            GameInstance.player.friendChatSystem:CreateChat(id)
        else
            PhaseManager:GoToPhase(PhaseId.SNS, { roleId = id })
        end

    end,
    onSpaceshipVisitorClick = function(id)
        Notify(MessageConst.ON_CLICK_SPACESHIP_VISITOR_FRIEND, { id })
    end,
    onAcceptClick = function(id)
        GameInstance.player.friendSystem:AcceptRequest(id)
    end,
    onNotAcceptClick = function(id)
        GameInstance.player.friendSystem:RejectRequest(id)
    end,
    onAddClick = function(id, nodeId)
        if GameInstance.player.friendSystem:PlayerInBlackList(id) then
            local errorMsg = Tables.errorCodeTable:GetValue(1065)
            Notify(MessageConst.SHOW_TOAST, errorMsg.text)
            return
        end

        local stack = PhaseManager:GetPhaseStack()
        local phaseId = ""
        for i = 0, stack:Count() - 1 do
            local item = stack:Get(stack:TopIndex() - i)
            if item.phaseId ~= PhaseId.FriendBusinessCardPreview then
                phaseId = PhaseManager:GetPhaseName(item.phaseId)
                break
            end
        end
        local panelId = ""
        if phaseId == "Friend" then
            panelId = UIManager:IsOpen(PanelId.SearchNewFriendList) and "SearchNewFriendList" or "StrangerList"
        end
        GameInstance.player.friendSystem:AddFriend(id, phaseId , panelId, tostring(nodeId))
    end,
    onRemoveClick = function(id)
        GameInstance.player.friendSystem:BlackListDelete(id)
    end,
    onPsnInfoClick = function(id)
        CS.Beyond.PS5Main.ShowUserProfileDialog(id)
    end
}

FriendUtils.FRIEND_CELL_INIT_CONFIG = {
    Friend = {
        stateName = "Normal",
        onPlayerClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onFriendPlayerClick,
        onMessageClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onMessageClick,
        onShipClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onShipClick,
        onChatClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onChatClick,
        onPsnInfoClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onPsnInfoClick,
        infoDicIndex = FriendUtils.FriendDictIndex,
        maxLen = Tables.globalConst.friendListPageMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_WORLD_LEVEL,
                keys = { "isCurrentShip", "roleType", "searchSort", "adventureLevel", "lastDateTime", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_ADD_FRIEND_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "addFriendTime", "lastDateTime", "adventureLevel", "helpFlag", "roleId" },
            }
        },
        showContingencyContractActivityState = true,
    },
    Black = {
        stateName = "BlackList",
        onRemoveClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onRemoveClick,
        onPsnInfoClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onPsnInfoClick,
        infoDicIndex = FriendUtils.BlackListDictIndex,
        maxLen = Tables.globalConst.friendBlackListMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
        },
    },
    FriendRequest = {
        stateName = "FriendRequest",
        onPlayerClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onStrangerPlayerClick,
        onAcceptClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onAcceptClick,
        onNotAcceptClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onNotAcceptClick,
        infoDicIndex = FriendUtils.FriendRequestDictIndex,
        maxLen = Tables.globalConst.friendRequestListPageMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_REQUEST_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "addFriendTime", "adventureLevel", "lastDateTime","helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_WORLD_LEVEL,
                keys = { "isCurrentShip", "roleType", "searchSort", "adventureLevel", "addFriendTime", "lastDateTime","helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
        },
        showContingencyContractActivityState = true,
    },
    Stranger = {
        stateName = "Stranger",
        onPlayerClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onStrangerPlayerClick,
        onAddClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onAddClick,
        infoDicIndex = FriendUtils.StrangerDictIndex,
        maxLen = Tables.globalConst.friendRecommendMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_WORLD_LEVEL,
                keys = { "isCurrentShip", "roleType", "searchSort", "adventureLevel", "lastDateTime", "addFriendTime", "helpFlag", "roleId" },
            },
        },
        showContingencyContractActivityState = true,
    },
    NewFriendSearch = {
        stateName = "Stranger",
        onPlayerClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onStrangerPlayerClick,
        onAddClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onAddClick,
        infoDicIndex = FriendUtils.NewFriendSearchDictIndex,
        maxLen = Tables.globalConst.friendSearchListPageMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_WORLD_LEVEL,
                keys = { "isCurrentShip", "roleType", "searchSort", "adventureLevel", "lastDateTime", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_ADD_FRIEND_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "addFriendTime", "lastDateTime", "adventureLevel", "helpFlag", "roleId" },
            }
        },
        showContingencyContractActivityState = true,
    },
    Share = {
        stateName = "share",
        onPlayerClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onFriendPlayerClick,
        onMessageClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onMessageClick,
        onShipClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onShipClick,
        onChatClick = FriendUtils.FRIEND_CELL_INIT_FUNC.onChatClick,
        infoDicIndex = FriendUtils.FriendDictIndex,
        maxLen = Tables.globalConst.friendListPageMaxLen,
        sortOptions = {
            {
                name = Language.LUA_FRIEND_LAST_DATE_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "lastDateTime", "adventureLevel", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_WORLD_LEVEL,
                keys = { "isCurrentShip", "roleType", "searchSort", "adventureLevel", "lastDateTime", "addFriendTime", "helpFlag", "roleId" },
            },
            {
                name = Language.LUA_FRIEND_ADD_FRIEND_TIME,
                keys = { "isCurrentShip", "roleType", "searchSort", "addFriendTime", "lastDateTime", "adventureLevel", "helpFlag", "roleId" },
            }
        },
    },
}

function FriendUtils.friendInfo2SortInfo(csFriendInfo, searchSort)
    return {
        roleId = csFriendInfo.roleId,
        name = csFriendInfo.name,
        lastDateTime = csFriendInfo.lastDateTime,

        addFriendTime = csFriendInfo.addOrRequestTime,
        adventureLevel = csFriendInfo.adventureLevel,
        roleType = CS.Beyond.Gameplay.RoleType.Unknown:GetHashCode() - csFriendInfo.roleType:GetHashCode(),
        searchSort = searchSort,
        accountId = csFriendInfo.psnData ~= nil and csFriendInfo.psnData.AccountId or "",
        helpFlag = csFriendInfo.helpFlag:GetHashCode(),
        isCurrentShip = (csFriendInfo.roleId == GameInstance.player.spaceship:GetFriendRoleInfo().roleId and GameInstance.player.spaceship:GetFriendRoleInfo().roleId ~= 0) and 1 or 0,
    }
end

function FriendUtils.getFriendInfoByRoleId(roleId, searchKey, ignoreRichFont)
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    local nameStr = ""
    local avatarPath = ""
    local avatarFramePath = ""
    local psName = ""
    if success then
        psName = playerInfo.psName
        if playerInfo.remakeName and not string.isEmpty(playerInfo.remakeName) then
            if not ignoreRichFont then
                nameStr = string.format(Language.LUA_FRIEND_REMAKE_NAME, playerInfo.remakeName, playerInfo.name, playerInfo.shortId)
            else
                nameStr = string.format(Language.LUA_FRIEND_REMAKE_NAME_NO_RICH_TEXT, playerInfo.remakeName, playerInfo.name, playerInfo.shortId)
            end
        else
            nameStr = string.format(Language.LUA_FRIEND_NAME, playerInfo.name, playerInfo.shortId)
        end
        if searchKey and not string.isEmpty(searchKey) then
            local rep = string.format(Language.LUA_FRIEND_NAME_SEARCH, searchKey)
            nameStr = string.gsub(nameStr, searchKey, rep)
        end

        if playerInfo.userAvatarId and playerInfo.userAvatarFrameId then
            local succ, avatarPathInfo = Tables.UserAvatarTable:TryGetValue(playerInfo.userAvatarId)
            if succ then
                avatarPath = avatarPathInfo.icon
            end
            avatarFramePath = Tables.userAvatarTableFrame:GetValue(playerInfo.userAvatarFrameId).icon
        end
    end
    return nameStr, avatarPath, avatarFramePath, psName
end

function FriendUtils.openFriendCharInfo(roleId, charTemplateId, templateIdList)
    local phaseFriend = require_ex("Phase/Friend/PhaseFriend")
    phaseFriend.PhaseFriend.s_mainFriendCharTemplateId = charTemplateId
    GameInstance.player.friendSystem:FriendCharQuery(roleId, templateIdList)
end

function FriendUtils.getFriendCellInitConfigByRoleId(roleId)
    local RoleType = CS.Beyond.Gameplay.RoleType

    local roleType = GameInstance.player.friendSystem:GetRoleTypeByRoleId(roleId)
    if roleType == RoleType.Friend then
        return FriendUtils.FRIEND_CELL_INIT_CONFIG.Friend
    elseif roleType == RoleType.BlackList then
        return FriendUtils.FRIEND_CELL_INIT_CONFIG.Black
    elseif roleType == RoleType.Stranger then
        return FriendUtils.FRIEND_CELL_INIT_CONFIG.Stranger
    end
end

function FriendUtils.isPsnPlatform()


    return CS.UnityEngine.Application.platform == CS.UnityEngine.RuntimePlatform.PS5
end

function FriendUtils.canShareBuilding()

    if GameInstance.player.friendSystem.isCommunicationRestricted then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_TIP_PARENTAL_CONTROL_CHAT)
        return false
    end

    if GameInstance.player.friendChatSystem.isSocialBuildingShareCountLimited then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SOCIAL_BUILDING_SHARE_COUNT_LIMITED)
        return false
    end
    return true
end

function FriendUtils.simpleIgnoreCaseReplace(input, pattern, sLanguage)

    local utf8Chars = {}
    for char in input:gmatch("[%z\1-\127\194-\244][\128-\191]*") do
        table.insert(utf8Chars, char)
    end

    local utf8Pattern = {}
    for char in pattern:gmatch("[%z\1-\127\194-\244][\128-\191]*") do
        table.insert(utf8Pattern, char)
    end

    local lowerInput = string.lower(input)
    local lowerPattern = string.lower(pattern)


    local lowerUtf8Chars = {}
    for char in lowerInput:gmatch("[%z\1-\127\194-\244][\128-\191]*") do
        table.insert(lowerUtf8Chars, char)
    end

    local lowerUtf8Pattern = {}
    for char in lowerPattern:gmatch("[%z\1-\127\194-\244][\128-\191]*") do
        table.insert(lowerUtf8Pattern, char)
    end

    local result = ""
    local i = 1

    while i <= #utf8Chars do
        local match = true


        for j = 1, #utf8Pattern do
            if i + j - 1 > #lowerUtf8Chars or lowerUtf8Chars[i + j - 1] ~= lowerUtf8Pattern[j] then
                match = false
                break
            end
        end

        if match then

            for j = 1, #utf8Pattern do
                local original = utf8Chars[i + j - 1]
                result = result .. string.format(sLanguage, original)
            end
            i = i + #utf8Pattern
        else

            result = result .. utf8Chars[i]
            i = i + 1
        end
    end
    return result
end


_G.FriendUtils = FriendUtils
return FriendUtils
