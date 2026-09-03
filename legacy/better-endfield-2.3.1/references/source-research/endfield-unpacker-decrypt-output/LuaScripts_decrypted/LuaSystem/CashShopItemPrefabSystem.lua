local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')
CashShopItemPrefabSystem = HL.Class('CashShopItemPrefabSystem', LuaSystemBase.LuaSystemBase)

CashShopItemPrefabSystem.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

CashShopItemPrefabSystem.newNodePrefab = HL.Field(HL.Any)
CashShopItemPrefabSystem.discountPrefab = HL.Field(HL.Any)
CashShopItemPrefabSystem.restrictionPrefab = HL.Field(HL.Any)
CashShopItemPrefabSystem.timePrefab = HL.Field(HL.Any)
CashShopItemPrefabSystem.tagHot = HL.Field(HL.Any)
CashShopItemPrefabSystem.tagNewcomer = HL.Field(HL.Any)
CashShopItemPrefabSystem.tagMultiple = HL.Field(HL.Any)
CashShopItemPrefabSystem.tagCostPerformance = HL.Field(HL.Any)
CashShopItemPrefabSystem.tagRecommend = HL.Field(HL.Any)

CashShopItemPrefabSystem.lockNode = HL.Field(HL.Any)
CashShopItemPrefabSystem.soldOutNode = HL.Field(HL.Any)
CashShopItemPrefabSystem.numberNode = HL.Field(HL.Any)

CashShopItemPrefabSystem.weaponCaseCell = HL.Field(HL.Any)

CashShopItemPrefabSystem.CashShopItemPrefabSystem = HL.Constructor() << function(self)
end

CashShopItemPrefabSystem.OnInit = HL.Override() << function(self)
    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    self.newNodePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/NewNode.prefab")
    self.discountPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagDiscount.prefab")
    self.restrictionPrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagRestriction.prefab")
    self.timePrefab = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagTime.prefab")
    self.tagHot = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagHot.prefab")
    self.tagNewcomer = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagNewcomer.prefab")
    self.tagMultiple = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagMultiple.prefab")
    self.tagCostPerformance = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagCostPerformance.prefab")
    self.tagRecommend = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemTag/TagRecommend.prefab")
    self.lockNode = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemLockNode.prefab")
    self.soldOutNode = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemSoldOutNode.prefab")
    self.numberNode = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/CashShopItemNumber.prefab")
end

CashShopItemPrefabSystem.OnRelease = HL.Override() << function(self)
    self.newNodePrefab = nil
    self.discountPrefab = nil
    self.restrictionPrefab = nil
    self.timePrefab = nil
    self.tagHot = nil
    self.tagNewcomer = nil
    self.tagMultiple = nil
    self.tagCostPerformance = nil
    self.tagRecommend = nil
    self.lockNode = nil
    self.soldOutNode = nil
    self.numberNode = nil
    self.weaponCaseCell = nil
    self.m_resourceLoader:DisposeAllHandles()
end

CashShopItemPrefabSystem.LoadWeaponCaseCell = HL.Method() << function(self)
    if self.weaponCaseCell ~= nil then
        return
    end
    self.weaponCaseCell = self.m_resourceLoader:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/CashShop/Widgets/ShopWeaponCaseCell.prefab")
end

HL.Commit(CashShopItemPrefabSystem)
return CashShopItemPrefabSystem
