# EFMI 原资源身份补全

BEM Tools 1.1.3；Windows x64，manifest `2954fa80-23c1-1579-2b22-4ecfd6d70418`。

## 已完成

从游戏离线依赖闭包读取原索引数据、原纹理 mip 数据及色彩空间，再关联现有运行时骨骼、材质、纹理格式证据。32 个角色接入 **360 个完整单子网格部件入口、1,181 项角色内纹理 hash 对应**。纹理数按角色累计，共享纹理会在不同角色重复出现，不代表 1,181 张不同游戏贴图。

佩丽卡现有原生目录包含 12 个可识别入口，37 项纹理身份（含公共贴图别名），全部从游戏资源重建；没有读取或适配佩丽卡源 Mod。未知入口仍拒绝，不通过角色名、组件编号或顶点数量猜角色。

正式 catalog 中只放名称、身份、布局和证据，不分发游戏几何或纹理像素。重复 hash 的公共身体/脸部纹理分别绑定原生名称并共享 BEM payload，保持 EFMI 对同 hash 资源统一覆盖的行为。原来两项已核实雨雪全局输入身份共享给同快照角色，仍逐次检查来源内容；源包改写全局效果仍拒绝。

## 算法及证据

标准 ComponentN 模板使用原 `ib_hash`。EFMI 默认启用 region hashing，XXMI 对绘制区域内的原始索引字节计算 CRC32C；此身份不包含 Buffer 创建描述符。当前只批准首偏移为零、基顶点为零、完整覆盖一个子网格的情况。

纹理采用 `texture_hash=0` 的数据算法再追加 44 字节 `D3D11_TEXTURE2D_DESC`。固定政策为原始完整 mip 链、类型化格式、单层 Texture2D、DEFAULT usage、SRV bind，无 MSAA/CPU/misc 标志。对紧密排列的原始 mip0，数据长度取 `min(width*height, mip0_bytes)`；色彩空间由序列化字段读取，同时检查运行时 GraphicsFormat 和尺寸属于原 mip 链。不会用 `_D` 等文件名后缀推断 sRGB。

该描述符政策由管理员 22 项已审阅纹理身份一致性验证支撑，不宣称所有 GPU 创建方式等价。低分辨率驻留链、不同纹理 hash 模式、动态生成纹理、非上述描述符可能产生不同身份；工具不枚举描述符碰撞来强行放行。

官方代码来源：

- [EFMI ComponentN 模板](https://github.com/SpectrumQT/EFMI-Tools/blob/eaa903624529e4cff93fa5c6cf999b3d44d40eff/efmi-tools/templates/mod.ini.j2)
- [EFMI 配置](https://github.com/SpectrumQT/EFMI-Package/blob/3cc2f4b89e791983484ac00ad5a3e15779957350/EFMI/d3dx.ini)
- [XXMI 区域和纹理 hash 算法](https://github.com/SpectrumQT/XXMI-Libs-Package/blob/43e5c9b16f12526f3dbf65355cc2b8f4f3e69138/DirectX11/ResourceHash.cpp)
- [Unity GraphicsFormat 枚举](https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Graphics/GraphicsEnums.cs)

这里计算的是请求补全的原游戏资源标识，没有计算交付产物哈希。

## 验证与剩余限制

- 管理员既有 11 项部件及 22 项纹理身份全部重算一致；庄方宜、洁尔佩塔各 4 项已审阅部件身份也一致。
- 庄方宜既有 10 项全局纹理记录中 8 项与完整原生纹理身份一致。另外 2 项是旧专用配方中对应同名纹理的其他源 hash，本次政策无法独立导出，未推广为通用别名；旧专用配方继续保留自己的证据和规则。
- 27 项针对性检查覆盖 CRC32C 标准向量与续算、描述符锚点、格式/快照拒绝、既有身份变化停止发布、共享纹理绑定、未支持布局和重新采集后的重新核验。
- 32 个角色分别通过合成标准入口的角色匹配和几何准备检查。它证明目录和转换链可用，不是 32 个真实作者包或实机渲染测试。
- 打包后的 1.1.3 CLI 重转管理员既有标准 ZIP 成功，原生 BEM 读取器接受包及 world/UI 路由。本轮没有重复游戏实机测试。
- 10 个多子网格部件不进入自动入口表，完整列表在 `catalog/source-identities.json`。其离线原索引数据可读，但当前转换器不能自动还原它们的段与材质关系；不是要求重新采集整个角色。
- 原生资料中的 62 个布局/多子网格限制、两处 world/UI 不等价排除继续生效。RabbitFX、自定义 Shader、非标准分发/骨架流程和 Hash/LOD 的专门规则未因此放行。

`entries` 不再只有女管理员，普通用户也不需要提供角色 JSON。自动转换是否就绪仍以整个源包检查为准。

## 开发工具

`NativeAssetReader --resource-identities` 输出原索引 CRC 与原纹理各 mip 的 CRC/色彩元数据，不写原像素文件。`import_efmi_identities.py` 将文件＋PathID、原生材质引用和直接运行时绑定连接到 catalog，已有核实的对应必须继续一致才发布。

`developer-tools/CompleteSourceIdentities.ps1` 串行调用读取器并入库，供游戏更新后复用。例：

```powershell
dotnet build tools/CustomModel/NativeAssetReader/NativeAssetReader.csproj -c Release -p:AnimeStudioDir=<兼容后端目录> -o artifacts/native-parser/reader-identities
./tools/CustomModel/developer-tools/CompleteSourceIdentities.ps1 -Databases artifacts/native-parser/sweep-offline -Reader artifacts/native-parser/reader-identities/NativeAssetReader.dll -Runs <run1>,<run2>
```

先完成当前版本原生采集与离线入库，再执行本步骤。重新导入原生 catalog 时保留旧身份作回归锚点，但状态变为需重新核验，自动转换暂不使用；完成本步骤后才恢复匹配。游戏更新导致身份变化时会停止并要求复核，不能沿用旧 verified 标记。所有开发入口仅使用原游戏资源，不依赖某个作者 Mod 包。
