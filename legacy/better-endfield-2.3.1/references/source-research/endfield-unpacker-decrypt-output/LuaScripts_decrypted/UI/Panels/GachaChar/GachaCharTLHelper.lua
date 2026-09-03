GachaCharTLHelper = HL.Class('GachaCharTLHelper')


GachaCharTLHelper.m_directors = HL.Field(HL.Table)

GachaCharTLHelper.m_actorDirector = HL.Field(CS.UnityEngine.Playables.PlayableDirector)

GachaCharTLHelper.m_root = HL.Field(Transform)

GachaCharTLHelper.m_updateKey = HL.Field(HL.Number) << -1

GachaCharTLHelper.m_loopStartTime = HL.Field(HL.Number) << -1

GachaCharTLHelper.m_loopEndTime = HL.Field(HL.Number) << -1

GachaCharTLHelper.m_args = HL.Field(HL.Table)

GachaCharTLHelper.m_exCamera = HL.Field(Transform)

GachaCharTLHelper.showNameTime = HL.Field(HL.Number) << -1

GachaCharTLHelper.showContentTime  = HL.Field(HL.Number) << -1

GachaCharTLHelper.m_triggerOnceTime = HL.Field(HL.Number) << -1

GachaCharTLHelper.m_triggerOnceTimeCallback = HL.Field(HL.Function)


GachaCharTLHelper.GachaCharTLHelper = HL.Constructor(CS.UnityEngine.Transform, HL.Table) << function(self, root, args)
    self.m_directors = {}
    self.m_args = args
    local mainDir = root:GetComponent("CutsceneRootComponent").director
    mainDir.enabled = false
    for k = 0, root.childCount - 1 do
        local child = root:GetChild(k)
        local succ, dir = child:TryGetComponent(typeof(CS.UnityEngine.Playables.PlayableDirector))
        if succ and dir ~= mainDir then
            table.insert(self.m_directors, dir)
            if child.name == "Actor" then
                self.m_actorDirector = dir
            end
        end
    end


    local timeline = self.m_actorDirector.playableAsset;
    self.showNameTime = -1
    self.showContentTime = -1
    for _, track in cs_pairs(timeline:GetOutputTracks()) do
        if (track.name == "TimePointTrack") then
            for _, timelineClip in cs_pairs(track:GetClips()) do
                if timelineClip.displayName == "ShowNamePoint" then
                    self.showNameTime = timelineClip.start
                elseif timelineClip.displayName == "ShowContentPoint" then
                    self.showContentTime = timelineClip.start
                end
            end
            break
        end
    end


    self.m_updateKey = LuaUpdate:Add("TailTick", function(deltaTime)
        self:TailTick(deltaTime)
    end)

    local succ, timeInfo = CS.Beyond.Gameplay.Core.TimelineUtils.GetClipTimeInfo(self.m_actorDirector, "Loop Track", "LoopPlayableClip")
    if succ then
        self.m_loopStartTime = timeInfo.x
        self.m_loopEndTime = timeInfo.y
    else
        logger.error("没找到LoopTrack", self.m_actorDirector.transform:PathFromRoot())
    end

    self.m_exCamera = root:Find("ExternalCamera")


    local charModel = self.m_actorDirector.transform:GetChild(0)


    local charModelAnimator = charModel:GetComponent("Animator")
    charModelAnimator.runtimeAnimatorController = nil


    local charModelIk = charModel:GetComponent("LookAtIK")
    charModelIk.enabled = false


    local mbcList = charModel:GetComponentsInChildren(typeof(CS.MagicaCloth.MagicaBoneCloth), true)
    for _, mbc in cs_pairs(mbcList) do
        mbc.gameObject:SetActive(false)
    end
end

GachaCharTLHelper.inLoopTrack = HL.Field(HL.Boolean) << false

GachaCharTLHelper.PlayFromStart = HL.Method() << function(self)
    self.inLoopTrack = false
    for _, dir in ipairs(self.m_directors) do
        dir:RebuildGraph()
    end
    self:SetTime(0, true)
end

GachaCharTLHelper.SampleToBeginning = HL.Method() << function(self)
    self.inLoopTrack = false
    for _, dir in ipairs(self.m_directors) do
        dir:Stop()
        dir.time = 0
        dir:Evaluate()
    end
    self:TailTick(0)
end

GachaCharTLHelper.GetTime = HL.Method().Return(HL.Number) << function(self)
    return self.m_actorDirector.time
end


GachaCharTLHelper.SetTime = HL.Method(HL.Number, HL.Boolean) << function(self, time, play)
    for _, dir in ipairs(self.m_directors) do
        dir.time = time
        if play then
            dir:Evaluate()
            dir:Play()
        else
            dir:Evaluate()
        end
    end
    self:TailTick(0)
end


GachaCharTLHelper.OnDispose = HL.Method() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

GachaCharTLHelper.JumpToLoopSection = HL.Method(HL.Opt(HL.Number)) << function(self, offset)
    self:SetTime(self.m_loopStartTime + (offset or 0), true)
end

GachaCharTLHelper.SetTriggerOnceTimer = HL.Method(HL.Number, HL.Function) << function(self, time, callback)
    self.m_triggerOnceTime = time
    self.m_triggerOnceTimeCallback = callback
end

GachaCharTLHelper.TailTick = HL.Method(HL.Number) << function(self, deltaTime)
    if not self.inLoopTrack then
        if self.m_actorDirector.time >= self.m_loopStartTime then
            self.inLoopTrack = true
            logger.info("GachaCharTLHelper inLoopTrack = true")
            if self.m_args.onLoopChanged then
                self.m_args.onLoopChanged(true)
            end
        end
    else
        if self.m_actorDirector.time < self.m_loopStartTime then
            self.inLoopTrack = false
            logger.info("GachaCharTLHelper inLoopTrack = false")
            if self.m_args.onLoopChanged then
                self.m_args.onLoopChanged(false)
            end
        end
    end

    if self.m_triggerOnceTimeCallback ~= nil then
        if self.m_actorDirector.time >= self.m_triggerOnceTime then
            self.m_triggerOnceTime = -1
            self.m_triggerOnceTimeCallback()
            self.m_triggerOnceTimeCallback = nil
        end
    end
end

HL.Commit(GachaCharTLHelper)
return GachaCharTLHelper
