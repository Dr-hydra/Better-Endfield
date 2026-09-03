local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacEnvironmental = HL.Class('FacEnvironmental', UIWidgetBase)

FacEnvironmental.m_curShow = HL.Field(HL.Boolean) << false


FacEnvironmental._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_PLAYER_FAC_ENVIRONMENTAL_CHANGED, function(args)
        local env = unpack(args)
        local show = env ~= GEnums.FacEnvGenEnvType.None:GetHashCode()
        local changeShow = self.m_curShow ~= show
        if changeShow then
            self.m_curShow = show
            if show then
                self.gameObject:SetActiveIfNecessary(true)
                self.view.anim:PlayInAnimation()
            else
                self.view.anim:PlayOutAnimation(function()

                    if NotNull(self.gameObject) and not self.m_curShow then
                        self.gameObject:SetActiveIfNecessary(false)
                    end
                end)
            end
        end
        if show then
            local envState = GEnums.FacEnvGenEnvType.__CastFrom(env)
            self.view.stateController:SetState(envState:ToString())
        end
    end, true)
end

FacEnvironmental.InitFacEnvironmental = HL.Method() << function(self)
    self:_FirstTimeInit()

    local env = GameInstance.remoteFactoryManager.playerCurrentGridInfoProvider:GetEnvInfo()
    local show = env ~= GEnums.FacEnvGenEnvType.None:GetHashCode()
    self.m_curShow = show
    self.gameObject:SetActive(show)
    if show then
        self.view.anim:PlayInAnimation()
        local envState = GEnums.FacEnvGenEnvType.__CastFrom(env)
        self.view.stateController:SetState(envState:ToString())
    end
end

HL.Commit(FacEnvironmental)
return FacEnvironmental

