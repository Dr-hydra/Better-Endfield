local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')
ItemPrefabSystem = HL.Class('ItemPrefabSystem', LuaSystemBase.LuaSystemBase)


ItemPrefabSystem.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

ItemPrefabSystem.potentialStarPrefab = HL.Field(HL.Any)

ItemPrefabSystem.redDotPrefab = HL.Field(HL.Any)

ItemPrefabSystem.redDotLimitTime = HL.Field(HL.Any)

ItemPrefabSystem.lockNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.gemEquippedNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.spTypeIconNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.liquidIconPrefab = HL.Field(HL.Any)

ItemPrefabSystem.gemAttrIconPrefab = HL.Field(HL.Any)

ItemPrefabSystem.compositeIconBGPrefab = HL.Field(HL.Any)

ItemPrefabSystem.levelNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.equipEnhanceNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.itemLimitTimeMarkNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.itemRewardTypeTagPrefab = HL.Field(HL.Any)

ItemPrefabSystem.gemPerfectIconPrefab = HL.Field(HL.Any)

ItemPrefabSystem.skipChapterTagNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.skipChapterInvalidNodePrefab = HL.Field(HL.Any)

ItemPrefabSystem.ItemPrefabSystem = HL.Constructor() << function(self)
end


ItemPrefabSystem.OnInit = HL.Override() << function(self)
    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    self.potentialStarPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/WeaponInfo/Widget/SimplePotentialStar.prefab")
    self.redDotPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/RedDot.prefab")
    self.redDotLimitTime = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/RedDotLimitTime.prefab")
    self.lockNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/ItemLock.prefab")
    self.gemEquippedNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonGemEquipped.prefab")
    self.spTypeIconNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonSpTypeIconNode.prefab")
    self.liquidIconPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonLiquidIcon.prefab")
    self.gemAttrIconPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonGemAttrIcon.prefab")
    self.compositeIconBGPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonCompositeIconBG.prefab")
    self.levelNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonLevelNode.prefab")
    self.equipEnhanceNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Equip/Widgets/EquipEnhanceNode.prefab")
    self.itemLimitTimeMarkNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemLimitTimeMarkNode.prefab")
    self.itemRewardTypeTagPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemRewardTypeTag.prefab")
    self.gemPerfectIconPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonGemPerfectIcon.prefab")
    self.skipChapterTagNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonSkipChapterTag.prefab")
    self.skipChapterInvalidNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Common/Widgets/Item/ItemAddonSkipChapterInvalid.prefab")
end

ItemPrefabSystem.OnRelease = HL.Override() << function(self)
    self.potentialStarPrefab = nil
    self.redDotPrefab = nil
    self.lockNodePrefab = nil
    self.gemEquippedNodePrefab = nil
    self.spTypeIconNodePrefab = nil
    self.liquidIconPrefab = nil
    self.gemAttrIconPrefab = nil
    self.compositeIconBGPrefab = nil
    self.levelNodePrefab = nil
    self.equipEnhanceNodePrefab = nil
    self.itemLimitTimeMarkNodePrefab = nil
    self.itemRewardTypeTagPrefab = nil
    self.gemPerfectIconPrefab = nil
    self.skipChapterTagNodePrefab = nil
    self.skipChapterInvalidNodePrefab = nil
    self.m_resourceLoader:DisposeAllHandles()
end


HL.Commit(ItemPrefabSystem)
return ItemPrefabSystem
