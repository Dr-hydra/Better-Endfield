local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')
ShopTradeGoodsCellPrefabSystem = HL.Class('ShopTradeGoodsCellPrefabSystem', LuaSystemBase.LuaSystemBase)

ShopTradeGoodsCellPrefabSystem.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

ShopTradeGoodsCellPrefabSystem.lockNodePrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.soldOutPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.bulkOperationPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.bulkBorderPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.selectBorderPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.centerInfoPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.itemBundleCountPrefab = HL.Field(HL.Any)
ShopTradeGoodsCellPrefabSystem.countdownPrefab = HL.Field(HL.Any)

ShopTradeGoodsCellPrefabSystem.ShopTradeGoodsCellPrefabSystem = HL.Constructor() << function(self)
end

ShopTradeGoodsCellPrefabSystem.OnInit = HL.Override() << function(self)
    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    self.lockNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellLockState.prefab")
    self.soldOutPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellSellOutState.prefab")
    self.bulkOperationPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellBulkOperation.prefab")
    self.bulkBorderPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellBulkBorder.prefab")
    self.selectBorderPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellSelectBorder.prefab")
    self.centerInfoPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellCenterInfo.prefab")
    self.itemBundleCountPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellItemBundleCount.prefab")
    self.countdownPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Shop/Widgets/ShopTradeGoodsCellCountdown.prefab")
end

ShopTradeGoodsCellPrefabSystem.OnRelease = HL.Override() << function(self)
    self.lockNodePrefab = nil
    self.soldOutPrefab = nil
    self.bulkOperationPrefab = nil
    self.bulkBorderPrefab = nil
    self.selectBorderPrefab = nil
    self.centerInfoPrefab = nil
    self.itemBundleCountPrefab = nil
    self.countdownPrefab = nil
    self.m_resourceLoader:DisposeAllHandles()
end


HL.Commit(ShopTradeGoodsCellPrefabSystem)
return ShopTradeGoodsCellPrefabSystem
