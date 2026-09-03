




LuaResourceLoader = HL.Class("LuaResourceLoader")

LuaResourceLoader.m_loader = HL.Field(HL.Any)

LuaResourceLoader.m_destroyed = HL.Field(HL.Boolean) << false

LuaResourceLoader.s_enableHashLoader = HL.StaticField(HL.Number) << 0

LuaResourceLoader.LuaResourceLoader = HL.Constructor() << function(self)
    self.m_loader = CS.Beyond.LuaResourceLoader()
    self.m_destroyed = false;
    LuaResourceLoader.s_enableHashLoader = 1
end

LuaResourceLoader._GetPathByHash = HL.StaticMethod(HL.String).Return(HL.String) << function(hash)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return CS.Beyond.Resource.HashStringPathProcessor.GetString(CS.Beyond.Resource.StringPathHash(LuaResourceLoader._PathToHash(hash)))
    else
        return hash
    end
end

LuaResourceLoader._PathToHash = HL.StaticMethod(HL.String).Return(HL.Number) << function(path)
    local hash = __beyond_calculate_ab_path_hash(path)

    return CS.Beyond.Resource.I18NAssetLoader.GetI18NResourceHash(hash)
end

LuaResourceLoader.LoadGameObject = HL.Method(HL.String).Return(CS.UnityEngine.GameObject, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadGameObject(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadGameObject(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadGameObject(path)
    end
end

LuaResourceLoader.LoadGameObjectAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadGameObjectAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadGameObjectAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadGameObjectAsync(path, newCallback)
    end
end

LuaResourceLoader.GetGameObjectByKey = HL.Method(HL.Number).Return(CS.UnityEngine.GameObject) << function(self,key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetGameObjectByKey(key)
end

LuaResourceLoader.LoadMaterial = HL.Method(HL.String).Return(CS.UnityEngine.Material, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadMaterial(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadMaterial(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadMaterial(path)
    end
end

LuaResourceLoader.LoadMaterialAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadMaterialAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadMaterialAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadMaterialAsync(path, newCallback)
    end
end

LuaResourceLoader.GetMaterialByKey = HL.Method(HL.Number).Return(CS.UnityEngine.Material) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetMaterialByKey(key)
end

LuaResourceLoader.LoadTexture = HL.Method(HL.String).Return(CS.UnityEngine.Texture, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadTexture(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadTexture(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadTexture(path)
    end
end

LuaResourceLoader.LoadTextureAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadTextureAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadTextureAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadTextureAsync(path, newCallback)
    end
end

LuaResourceLoader.GetTextureByKey = HL.Method(HL.Number).Return(CS.UnityEngine.Texture) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetTextureByKey(key)
end



LuaResourceLoader.LoadShader = HL.Method(HL.String).Return(CS.UnityEngine.Shader, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadShader(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadShader(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadShader(path)
    end
end

LuaResourceLoader.LoadShaderAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadShaderAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadShaderAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadShaderAsync(path, newCallback)
    end
end

LuaResourceLoader.GetShaderByKey = HL.Method(HL.Number).Return(CS.UnityEngine.Shader) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetShaderByKey(key)
end

LuaResourceLoader.LoadMesh = HL.Method(HL.String).Return(CS.UnityEngine.Mesh, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadMesh(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadMesh(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadMesh(path)
    end
end

LuaResourceLoader.LoadMeshAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadMeshAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadMeshAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadMeshAsync(path, newCallback)
    end
end

LuaResourceLoader.GetMeshByKey = HL.Method(HL.Number).Return(CS.UnityEngine.Mesh) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetMeshByKey(key)
end


LuaResourceLoader.LoadSprite = HL.Method(HL.String).Return(CS.UnityEngine.Sprite, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadSprite(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadSprite(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadSprite(path)
    end
end

LuaResourceLoader.LoadSpriteAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadSpriteAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadSpriteAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadSpriteAsync(path, newCallback)
    end
end

LuaResourceLoader.GetSpriteByKey = HL.Method(HL.Number).Return(CS.UnityEngine.Sprite) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetSpriteByKey(key)
end

LuaResourceLoader.LoadScriptableObject = HL.Method(HL.String).Return(CS.UnityEngine.ScriptableObject, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadScriptableObject(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadScriptableObject(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadScriptableObject(path)
    end
end

LuaResourceLoader.LoadScriptableObjectAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadScriptableObjectAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadScriptableObjectAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadScriptableObjectAsync(path, newCallback)
    end
end

LuaResourceLoader.GetScriptableObjectByKey = HL.Method(HL.Number).Return(CS.UnityEngine.ScriptableObject) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetScriptableObjectByKey(key)
end

LuaResourceLoader.LoadAnimatorController = HL.Method(HL.String).Return(CS.UnityEngine.RuntimeAnimatorController, HL.Number) << function(self, path)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadAnimatorController(LuaResourceLoader._GetPathByHash(path))
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadAnimatorController(LuaResourceLoader._PathToHash(path))
    else
        return self.m_loader:LoadAnimatorController(path)
    end
end

LuaResourceLoader.LoadAnimatorControllerAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return -1
    end
    local newCallback = self:_GetProtectCallback(callback)
    if LuaResourceLoader.s_enableHashLoader == 2 then
        return self.m_loader:LoadAnimatorControllerAsync(LuaResourceLoader._GetPathByHash(path), newCallback)
    elseif LuaResourceLoader.s_enableHashLoader == 1 then
        return self.m_loader:LoadAnimatorControllerAsync(LuaResourceLoader._PathToHash(path), newCallback)
    else
        return self.m_loader:LoadAnimatorControllerAsync(path, newCallback)
    end
end

LuaResourceLoader.LoadI18NAsset = HL.Method(HL.String, HL.Any).Return(HL.Any, HL.Number) << function(self, path, type)
    if not self:_CheckLoader() then
        return
    end
    if not self:_CheckPathHash(path) then
        return nil, -1
    end
    return self.m_loader:LoadI18NAsset(LuaResourceLoader._PathToHash(path), type)
end

LuaResourceLoader.GetAnimatorControllerByKey = HL.Method(HL.Number).Return(CS.UnityEngine.RuntimeAnimatorController) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    return self.m_loader:GetAnimatorControllerByKey(key)
end

LuaResourceLoader.DisposeHandleByKey = HL.Method(HL.Number) << function(self, key)
    if not self:_CheckLoader() then
        return
    end
    self.m_loader:DisposeHandleByKey(key)
end

LuaResourceLoader.DisposeAllHandles = HL.Method(HL.Opt(HL.Boolean)) << function(self, notDestroy)
    self.m_loader:DisposeAllHandles()
    self.m_destroyed = not notDestroy
end

LuaResourceLoader._GetProtectCallback = HL.Method(HL.Function).Return(HL.Function) << function(self, callback)
    return function(obj)
        if self.m_destroyed then
            return
        end
        callback(obj)
    end
end

LuaResourceLoader._CheckLoader = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_destroyed then
        logger.critical("can not load resource from destroy loader")
        return false
    end
    return true
end

LuaResourceLoader._CheckPathHash = HL.Method(HL.String).Return(HL.Boolean) << function(self, path)
    local isExist = CS.Beyond.Resource.ResourceManager.RawCheckExists(__beyond_calculate_ab_path_hash(path))
    if not isExist then
        logger.error(path .. " is not exist")
    end
    return isExist
end

HL.Commit(LuaResourceLoader)


