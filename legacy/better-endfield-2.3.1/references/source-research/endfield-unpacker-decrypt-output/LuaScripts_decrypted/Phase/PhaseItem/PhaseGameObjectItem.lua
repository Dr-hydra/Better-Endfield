



local phaseSubItem = require_ex('Phase/Core/PhaseSubItem')
PhaseGameObjectItem = HL.Class("PhaseGameObjectItem", phaseSubItem.PhaseSubItem)

do


    PhaseGameObjectItem.go = HL.Field(HL.Userdata)

    PhaseGameObjectItem.view = HL.Field(HL.Table)

    PhaseGameObjectItem.cacheName = HL.Field(HL.String) << ""
end

do

    PhaseGameObjectItem._OnInit = HL.Override() << function(self)
        self.go = nil
        self.view = {}
    end

    PhaseGameObjectItem.BindGameObject = HL.Method(HL.Any) << function(self, go)
        self.go = go
        self.view = Utils.wrapLuaNode(go)
        self:_GameObjectInit()
    end

    PhaseGameObjectItem.SetActive = HL.Method(HL.Boolean) << function (self, active)
        if self.go then
            self.go:SetActive(active)
        end
    end

    PhaseGameObjectItem._GameObjectInit = HL.Virtual() << function (self)
        if self.view and self.view.config then
            if self.view.config:HasValue("INIT_POS") then
                self.go.transform.position = self.view.config.INIT_POS
            end
        end
    end
end

do

    PhaseGameObjectItem._DoTransitionInCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end


    PhaseGameObjectItem._DoTransitionBehindCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end


    PhaseGameObjectItem._DoTransitionOutCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    end

    PhaseGameObjectItem._CheckAllTransitionDone = HL.Override().Return(HL.Boolean) << function(self)
        return true
    end

    PhaseGameObjectItem._OnDestroy = HL.Override() << function(self)
        if string.isEmpty(self.cacheName) then
            CSUtils.ClearUIComponents(self.go)
            GameObject.DestroyImmediate(self.go)
        else
            PhaseManager:TryCacheGo(self.phaseId, self.cacheName, self.go)
        end

        self.go = nil
        self.view = {}
    end
end

HL.Commit(PhaseGameObjectItem)
