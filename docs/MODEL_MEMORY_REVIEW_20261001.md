# BEM 加载内存检查（2026-10-01）

本次只做代码和现有包分析，未修改解析器或运行时、未打 APK、未启动游戏。ADB 设备 `78572d34` 上游戏当前没有进程，不能提供瞬时 GPU 峰值实测；`artifacts/model-memory-device-meminfo.txt` 记录了这个限制。旧日志仅用于验证重复构造路径，不能当成当前版本性能数据。

## 结论

可以改善，优先处理明确的重复副本与重复贴图上传。需要区分 CPU RAM、Unity 可读资源副本、GPU 资源及驱动 staging；Android GPU 使用系统内存，不能把进程总内存全部叫作显存。文件被 Zstd 压缩得小，不代表上传后的贴图小。

| 本地 PC 格式包、默认选择 | 文件 | 所选 decoded payload | 其中唯一贴图 | 每组件至少上传一份贴图 |
|---|---:|---:|---:|---:|
| `typhoea-campus.bem` | 33.31 MiB | 155.72 MiB | 153.00 MiB | 196.00 MiB |
| `typhoea-rabbit-texture-pin-fixed.bem` | 17.48 MiB | 101.17 MiB | 89.00 MiB | 89.00 MiB |

以上从包 header/directory 和 `bem_v11.selection_plan` 计算，不是 GPU 监测。196 MiB 是默认组件贴图引用汇总，预期各贴图都匹配成功时的 payload 字节数；不同 source Texture 在同组件里还可能多次上传。校园包存在最多 43 MiB 的跨组件同 payload 重复节省空间，但是否全部可合并取决于实际 source 对象及 sampler 状态。Android 安装后 ASTC/RGBA 格式会改变这些数字。

## 已证实的路径

1. **解析期两份 decoded 数据：CPU 峰值，双端共用。**
   `native/modules/custom_model/bem.cpp:287` 的 `Container::cache` 保存 decoded vector，`Payload` 返回 `const vector&`。随后 `:386`、`:441`、`:474` 复制到 `BemTexture::data` / component streams / indices；draw indices 路径 `:470` 也从 cache 追加到目标 buffer。整个 Container 到 `LoadBem` 的 `File` action 返回才释放。校园包默认数据约 155.72 MiB，因此仅 cache+输出就约 311 MiB，再叠加当前压缩块、解压临时块、manifest/其他进程内存。上传开始前 cache 释放，不能把这个数字再直接叠到 GPU 上传阶段。

2. **贴图 cache 是每组件局部：真实 GPU 重复候选，双端共用。**
   `native/modules/custom_model/module.cpp:1776` 的 `PrepareDrawMaterials` 和 `:1794` 的 `PrepareKeepMaterials` 各创建局部 `texture_cache`。同一个 `(BEM texture index, source Texture object)` 只在该组件内复用。校园包 `cloth_02_D/N/P` 被 C2/C4/C5 使用，代码会分别构造；旧 Android 日志也出现这些重复 `built t=`。`ApplyTextureMask` 在 `:1757` 使用二元 key，并在 `:1760` 复制原对象 sampler，不能直接把 key 改成仅 texture index 而忽略 wrap/filter/aniso/mip bias。

3. **现有 Texture 上传已释放 Unity CPU 副本。**
   `CreateTextureFromBem` 位于 `module.cpp:1477`，在 `:1505` 使用 native pointer `LoadRawTextureData`，没有先创建托管 byte array；`:1517` 已是 `Apply(false, true)`，保留包内 mip、不重算、上传后放弃 Unity CPU 可读副本。该调用不会释放 `BemTexture::data`。Unity 官方文档确认这一参数仅释放 CPU texture copy：[Texture2D.Apply](https://docs.unity3d.com/ScriptReference/Texture2D.Apply.html)。

4. **128 MiB / 10 秒缓存是 CPU cache，不是 GPU cache。**
   `module.cpp:2332`—`:2367` 持有 `shared_ptr<const BemPocData>`，避免连续 world/UI 解压；按 decoded 字节限制 128 MiB，TTL 10 秒。校园包 155.72 MiB 超过上限不会缓存，逆兔默认 101.17 MiB 可以缓存。降低 TTL/上限可能省常驻 CPU RAM，但使后续资源到达重新解压，不能自动解决显存峰值。

5. **Mesh 可读 CPU 副本仍保留。**
   `module.cpp:803` 使用 `UploadMeshData(false)`，明确用于 declaration 检查与后续 discovery；双端 `BuildMeshFromComponent` 都在 `:1385` 调它。Android `android_mesh_builder.cpp:286` 创建 writable MeshData，`:321` ApplyAndDispose，`:334` 获取已提交 Mesh 的只读 snapshot 并逐字节比较，然后作用域释放。不能把这个 snapshot 直接说成完整 GPU 回读或第二份大型 GPU buffer：Unity 官方说明在 dispose 前不修改 Mesh 时默认不复制数据：[Mesh.AcquireReadOnlyMeshData](https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Mesh.AcquireReadOnlyMeshData.html)。

6. **Android UI donor 增加载入原资源，但 world 已复用 UI replacement。**
   `module.cpp:2393` 的 world 交付先确保 UI donor；`world_resource_adapter.inc:12` 加载 UI donor，`:15` 优先读已完成结果，`:64` / `:86` 复用 source custom mesh/material。并不是正常 world/UI 永远生成两份完整替换资源。必须保留 donor 的精确布局/骨骼校验；不能直接删除 UI 加载。资源 handle 有 RAII Dispose，不长期强持有。

7. **成功资产只有弱跟踪，最终释放依赖游戏。**
   `module.cpp:347`—`:368` 的 ConstructionScope 临时强 root、失败销毁；`:1604` 发布后释放 roots；`:2171`—`:2197` completed record 仅弱引用。`:2253` prune 只清 bookkeeping，不主动销毁成功 Texture/Mesh；游戏的 renderer/material 会继续持有它们，原资源及其他 clone 也可能仍在 cache。原资源与新资源在 transaction prepare/publish 阶段同时存在。不可随意 `Destroy` 游戏原 Texture/Mesh，因为它们可能为其他角色/UI/LOD共享；也不能把清 completed record 等同于释放显存。Unity `Destroy` 本身延迟到当前 Update 后执行：[Object.Destroy](https://docs.unity3d.com/ScriptReference/Object.Destroy.html)。

## 建议的实现顺序

1. **把 texture_cache 提升到一次 PrepareResource 事务作用域。** 保留当前 `(texture index, source Texture object)` key，传入 Draw/Keep preparation；只合并原来就明确等价的对象，不改变匹配/校验/rollback。风险低，双端一处修改；需要验证跨组件相同 source 只上传一次、不同 sampler/source 仍分开、失败事务共享对象只销毁一次。
2. **解析器按 selected plan payload 消费次数移动最后一次使用。** 最后消费从 Container cache move 到输出，indices concat 追加后及时清已无消费者 payload。共享 payload 可能被多个 stream/texture/draw 引用，不能无条件 move 第一次。目标降低解析期 RAM，尤其 Android；需覆盖重复 payload ID、indices 拼接、截断输入、所有 option plan。另一方案是改运行时 vector 为 shared backing buffer/view，侵入性更大。
3. **安卓安装时可选贴图上限/分辨率档。** 现有 `texture_install.cpp:134`—`:195` 已按能力选择 color ASTC 6×6、linear ASTC 4×4，unsupported ASTC 时用 RGBA32；只为超过 8192 / 64 MiB 限制降分辨率。可增加用户可选 2K/4K 上限、保留 mip metadata与色彩/法线语义。对 GPU 和 upload staging 都有效，代价是画质；不能把非 ASTC 设备强行改成 ASTC。4K RGBA32 单 mip 64 MiB，ASTC 4×4约16 MiB、6×6约7.12 MiB，减半长宽减少约3/4像素存储（方形且尺寸整齐时）。
4. **模型校验完成后释放 Mesh 可读 CPU 副本，需游戏验证。** 可尝试 `UploadMeshData(true)`，官方确认释放 system-memory mesh copy：[Mesh.UploadMeshData](https://docs.unity3d.com/ScriptReference/Mesh.UploadMeshData.html)。这是 CPU RAM 优化；必须确认后续 renderer refresh、skinning、LOD与 metadata discovery不会依赖 Read/Write，目前不是可以直接切参数的无风险变更。
5. **更进一步：按帧预算构建，或弱 GPU资产复用。** 当前资源 hook 同步准备整个角色后事务发布；简单把循环变成异步会在游戏使用前未完成，破坏交付与回滚。需要独立等待交付/占位机制，改动较大。弱 GPU cache 要以 package revision、appearance、格式与 donor/sampler fingerprint 做 key，跟踪 clone 及生命周期；不要用强 root 让显存永不释放。

## 下一次实测

无需先增加大量 renderer 诊断。复现时以 same package/appearance、同一画质、冷启动及重复切队各一轮，记录解析前后 decoded CPU bytes、实际新建 Texture2D 数及声明字节、准备/提交/完成时间。ADB `dumpsys meminfo` 用于进程 Native/Graphics/GL统计；它不保证看到完整的 GPU driver residency，应配 GPU/系统工具采样并区分首帧峰值与长期保留。先做上述第1、2项再对照，用实测决定是否需要更大的异步加载重构。

## 实验实现及验证（随后用户授权）

已在双端共享 native runtime 落地前两项，`loading_optimization` 默认 `false`，与 `skip_validation` 分开。`LoadBem` / `ParseBem` 末尾可选 `bool loading_optimization=false`；metadata inspect/installer 默认不使用它，也不改变校验。开启时对真实所选 payload 预数消费者，非末次消费保留私有输出，末次 move/释放；多 draw indices 仍按原顺序拼接并在消费后及时释放。默认关闭时保留原 parser cache 与各组件独立贴图缓存行为。

材质事务共享 cache 仍用 `(selected BEM texture index, original Texture object)` key，复用只限一次 `PrepareResource`，未添加长期 GPU cache或强 root。不同原 Texture对象仍分别复制 sampler。失败由 ConstructionScope 独占资产表销毁，cache本身不销毁对象；共享Texture只被创建/登记一次。未切换 `UploadMeshData(true)`、未改画质、未删除 UI donor或强制卸载原资产。

以下是同一个默认 option plan 的 native decoder 自身统计，不是进程/GPU峰值。`remaining` 是 Decode 返回时 cache snapshot，关闭模式的 cache也会在文件Container销毁时释放；不能把remaining当作LoadBem返回后的长期内存。

| 包 | 模式 | decoded cache峰值 | 写输出时payload复制量 | move转移量 | Decode结束cache |
|---|---|---:|---:|---:|---:|
| 校园 | 关闭 | 163,288,374 B | 163,398,222 B | 0 B | 163,288,374 B |
| 校园 | 开启 | 67,108,864 B | 418,212 B | 162,980,010 B | 0 B |
| 逆兔修正版 | 关闭 | 106,083,002 B | 106,083,002 B | 0 B | 106,083,002 B |
| 逆兔修正版 | 开启 | 16,777,216 B | 541,200 B | 105,541,802 B | 0 B |

Windows原生编译通过；39项Python测试通过；CustomModelBindingTests通过。覆盖重复stream/texture payload ID、重复draw indices及拼接、keep override/null texture slot、BEM1.0多组件、截断输入、开关关闭保留旧语义、不同source Texture不合并、失败共享Texture只销毁一次。真实校园/逆兔默认选择的开关前后全部mesh/texture字节、元数据及实际访问payload序列相同。

validator测试命令可加 `--loading-optimization --compare-loading --options <group:choice&...>`，比较同一selection关闭/开启的全部输出；日志在 `artifacts/android-model-diagnostics/loading-optimization-*.log`。Android native编译由主任务统一检查；本次未进行游戏内GPU实测。
