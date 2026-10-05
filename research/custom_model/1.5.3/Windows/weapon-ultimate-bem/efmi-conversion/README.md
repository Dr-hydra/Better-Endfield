# 庄方宜 EFMI 大招扩展：独立 BEM 1.4 样包

G 盘已有两套明确的大招 EFMI 源。本次从原始 `.buf` 和 `.dds` 重建独立扩展，
不覆盖 `G:/zmd_bem/成品`，不修改原包 ID 或转换台账。

| 来源 | 新包范围 | 有限选项 | 局部纹理 |
| --- | --- | --- | ---: |
| M0178：庄方宜-大招状态-终极状态 by 幽魂小猫 | 每资源 9 replace、1 hide、2 keep | 10 组，1024 态 | 15 |
| M0184：庄方宜-心灵+大招形态中的“大招形态/mod.ini” | 每资源 10 replace、2 keep | 原大招 INI 无持久选项 | 5 |

每包只含两个 Windows LOD0 资源：`chr_0030_zhuangfy_ult_postmodel` 和
`abilityentity_chr_0030_zhuangfy_ult_postmodel`，总计 24 个目标组件。
镜像采用另一套网格/骨架，保持原状。源未覆盖的两个 VFX body 保持原状。
M0178 的 Component1 明确 `handling=skip` 且 draw callback 被注释，因此按源意图隐藏。

交付文件位于仓库 `build/bem14/samples/`：

- `M0178-zhuangfy-ultimate-extension-Windows-LOD0.bem`
- `M0184-zhuangfy-ultimate-extension-Windows-LOD0.bem`
- 同名 `.report.json`：精简范围、限制和证据。

新 package ID 为 `dev.bem14.m0178.ultimate`、`dev.bem14.m0184.ultimate`。
两份原 1.1 成品仍是普通 postmodel/UI 合同；M0184 原包的普通形态 4 组 24 态不受影响。
每个扩展可与其原普通形态包并存；两个大招扩展占用同一组资源，不能一起启用。

## 核实的映射与完整性

十个源 Component 的原始游戏 IB CRC32C 与原索引数，均精确命中当前大招 LOD0 网格。
这属于既有 EFMI 游戏资源身份核对，未计算任何生成产物哈希。
ultimate 与 ability 的 12 对 renderer 使用同一 Mesh 对象、同一 bindpose 数组、相同骨骼
名称/顺序、材质对象/槽和相对 Transform 空间。两资源分别引用自己的活体 donor，
只共享几何 payload。

全部 base VB0/VB1/VB2 原样保留，未拿 `VB2_LOD` 替代 LOD0。骨骼索引按真实原生
palette 顺序绑定；所有索引（含零权重槽）在范围内，显式 ushort 权重逐顶点和为 65535。
iris 使用原生 stride4 隐式权重流。hair 与 eyeshadow 的两个材质槽/重复子网格 pass 均保留。
包写入后再次逐字节比对 19 份源组件在两个资源中的全部流、每段索引及所有材质槽，均通过。

Python 检查全部 1025 个选项组合及 2050 个按资源选择计划。C++ 两包验证均返回 0，
覆盖默认/末端共 3 个选择并比较两种加载模式。管理器使用生产 `ReadMetadata` /
`ConflictsWith` 对 6 个实际包执行 15 对冲突检查；普通与大招并存、大招互斥、武器独立符合预期。
管理器报告在相邻 `weapon-conversion/actual-manager-pairs.json`。

## 纹理与验证限制

M0178 的 15 张局部纹理、M0184 的 5 张局部纹理均通过原生数据 CRC32C 加 DX11
descriptor 身份与真实材质引用连接，不沿用旧报告的骨骼或纹理属性猜测。

M0178 另 7 张 DDS 的 mip0 能按原 EFMI descriptor 重建源 INI 的 original hash，
因此保留当前游戏纹理。这证明该身份规则下源 base mip 没有作者改动，**不证明当前原生
材质属性或完整 mip 链字节一致**。这些源 DDS 仅存 mip0。雨滴 `7d330dbd` 还有额外
原生 mip0 逐字节相同证据；详细记录见 `original-texture-audit.json`。

`fe1d6277` 没有套入材质。M0178 的显式 `ps-t2` 在兼容原生 Shader 记录中属于
Global shadow 资源，不是 PerMaterial 属性；无法用 BEM 局部材质替换准确承载。
M0184 同 hash 的 global `this` 只有跨包旁证，没有直接 slot 证据。
两包因此保留 `source_coverage_complete: false`，没有声称完整全局 Shader 行为等价。

没有安装、启动游戏或进行对象池/开大切换/视觉验收；`runtime_verified`、`render_verified`
均为 false。当前样包仅声明 Windows LOD0，不是 Android donor 证据。

## 复跑

先用本仓库正式 NativeAssetReader 对相邻 `inputs/` 运行 `--resource-identities`，
输出本目录 `native-identities.json`；`--texture-bytes native-textures` 为额外逐字节比较提供原始纹理。
本地原始游戏图谱、材质字节和生成 BEM 均被 Git 忽略。

```powershell
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/efmi-conversion/audit_inputs.py
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/efmi-conversion/audit_original_textures.py
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/efmi-conversion/build_extensions.py
python research/custom_model/1.5.3/Windows/weapon-ultimate-bem/efmi-conversion/validate_extensions.py
```

可复跑脚本只解析数据，不执行源 Mod 的脚本、广告或 INI 命令。
`M0178-conversion-report.json` / `M0184-conversion-report.json` 保存完整映射和限制；
`M0178-readback-validation.json` / `M0184-readback-validation.json` 与相应 native 日志保存验收证据。
