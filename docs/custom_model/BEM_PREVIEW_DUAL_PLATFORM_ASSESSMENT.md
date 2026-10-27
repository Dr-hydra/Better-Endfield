# BEM 双端可视化预览可行性评估

评估日期：2026-10-27。范围：Windows 管理器与 Android 管理器。本文是源码评估，不代表已经完成双端 GPU 或游戏内验收。

## 结论

双端可行。建议从 DSH 提取共用的 WebGL2 渲染核心，两端使用本地页面承载，由现有 C++ BEM 核心负责包读取、规则求值与按需数据供应。不要把 DSH 服务端或 Node.js 打进管理器。

Windows 使用已有 WinUI WebView2 接入经验；Android 新增系统 WebView 页面。共用页面、着色器、相机和交互，平台分别实现文件及二进制数据适配。现有组合器逻辑不因预览被替换。

## 实际依赖

| 项目 | Windows | Android |
| --- | --- | --- |
| 网页渲染宿主 | WebView2 Runtime；项目已有 ToyAnalysisWindow 使用它 | 系统 WebView 提供方；需运行时探测 WebGL2 |
| 用户额外安装 | Runtime 缺失时需要安装；应用自包含发布不等于包含它 | 通常无需额外安装；WebView 缺失、禁用、过旧或 GPU 黑名单时不能保证预览 |
| Node / Python / DSH | 均无需引入 | 均无需引入 |
| Three.js / Unity / Blender | 当前 DSH 是直接 WebGL2，无需这些依赖 | 同左 |
| Zstd / BEM 读取 | 复用现有原生核心，新增预览接口或工具入口 | 复用现有原生核心，新增 JNI 预览接口 |
| BCn 解码 | 可沿用已有 JS worker，或复用仓库 bcdec | 安装器已有 bcdec；需暴露只读预览接口 |
| ASTC 解码 | 若要求预览 Android 包，可新增使用仓库 astcenc 的解码构建 | APK 已链接 astcenc；现有纹理安装路径主要用于编码，不能当作现成预览解码 API |
| 本地内容承载 | WebResourceRequested / 虚拟主机映射 | 可自行实现受限 shouldInterceptRequest；若用 WebViewAssetLoader，则增加 AndroidX WebKit 构建依赖 |

推荐优先系统 Runtime，不随 PC 安装包附带 Fixed Version 浏览器；后者会显著增加分发体积，需另行实际测量。不能承诺零系统依赖，但可以做到无新增独立建模软件、解释器或在线服务依赖。

## DSH 可复用部分

- `tools/CustomModel/dsh-plugin/src/client/index.js`：直接 WebGL2 Stage、着色器、旋转/缩放、鼠标与双指操作、可见部件控制、形变和资源释放。
- `lib/assets/decode-worker.js` 与 `lib/vendor/bcn.js`：离线程 BCn 解码。
- `lib/bem/scene.js`：场景 JSON 与二进制 blob 分离的接口设计。
- 前端目前依赖 DSH 提供的 React、模块装载器和 `/dsh-bem/` 路由，不能原样作为独立页面运行；建议只提取 Stage，相机与解码核心，壳层使用轻量 DOM 和原生管理器控件。
- Node 服务端使用 fs、Buffer、node:zlib；应换成现有 C++ 数据服务，不引入 Node 运行时。
- 三个前端源文件合计约 89 KB，说明现有自写渲染核心较轻；这不是最终包体增量估计，尚不包含宿主、原生接口和新增解码构建。

## 直接照搬的正确性问题

1. `scene.js` 导出 optionGroups 时未保留组的 `available_when`，也未导出全局 constraints。直接使用会允许运行时不合法的组合，不能用于验证刚实现的源包联动。
2. `evalWhen(false, state)` 当前返回 true，且 `candidate.when || null` / `draw.when || null` 会丢失显式 false。需要完整支持布尔条件并对未知语法明确报错。
3. 未见完整 texture_slots 的求值与导出路径，已有 material_overrides 传递也不等于完整渲染支持。必须从规范和原生加载结果对齐。
4. 场景主要使用 `target.components`，没有完整保留 1.4+ 多资源对象展示语义。普通 world/UI、技能和武器应可分组查看；技能/武器的整体组合约束必须继续遵守。
5. 旧包贴图有按名称猜测的回退；材质、光照和法线采用简化处理，不能把它称作游戏效果一致性预览。
6. 只包含替换网格；原包未携带的游戏原始部件、keep 恢复部分与完整角色骨骼无法凭空补齐。
7. 不能用当前静态查看器承诺游戏动作、驱动、根偏移和全部材质行为。首版应清楚区分已支持和不可视化能力。

推荐将 ResolveBemSelection / LoadBem 的解析与选择结果作为权威；扩展紧凑的预览数据输出接口，而非在 JS 再造一套不完整的合法性规则。现有 LoadBem 已有 resource_id、解压预算与延迟贴图支持，适合继续扩展；但其输出还需转换为前端需要的顶点/索引布局。

## 安卓贴图与内存

DSH 当前主动不发送 ASTC 贴图，因此原样迁移会使这类包只有素模。代码注释称 WebGL2 不暴露 ASTC 过于绝对：Khronos 定义了 WEBGL_compressed_texture_astc，能否使用取决于具体 WebView/GPU 的扩展探测。

推荐路径：

1. 探测并优先直接上传可用的压缩纹理格式，减少 RGBA 展开成本。
2. 不支持时，由原生解码器按需解码选中的纹理；ASTC 需新增解码调用、预算控制和测试。
3. 移动端优先使用合适的 mip/降采样分辨率，限制解码并发与缓存，不照搬 PC 纹理质量和四 worker 上限。
4. 后台暂停绘制，关闭即释放 GPU/CPU 资源，处理 WebGL context lost，取消过期的选项和包加载任务。

一张 4096×4096 RGBA8 基础层就是 64 MiB；完整 mip 链 GPU 存储约 85.3 MiB，尚未计入 CPU 副本和 WebView 自身开销。因此包文件小不代表预览内存小。无需先引入大引擎，但必须限制峰值内存。

## 推荐接入形态

- 共用离线 HTML/JS/CSS 和版本化 scene/blob 协议，不依赖 CDN，不需要对外开放 HTTP 端口。
- Windows 在管理器独立预览窗口承载；Android 在管理器 Activity 承载，不放到注入游戏的悬浮层。
- 本地虚拟来源只暴露当前会话的资产 ID，不暴露任意文件路径；二进制走资源请求，不经 JSON/base64 往返。
- 正常界面保留原生选项与“关联部件”详情；预览支持点击/选中部件高亮、其他部件淡化及同步选项，长名称放侧栏或底部抽屉。
- 组合编辑时不为每次点击反复生成正式 BEM；应复用组合计划生成临时预览结果或内存描述，并让取消/过期任务可丢弃。该预览入口仍需实现，现有成品包查看器不能直接覆盖全部组合编辑流程。

## 实施顺序与验收

1. 抽出共享 Stage，补规范条件、资源分组、贴图绑定的回归测试；先用 PC WebView2 显示真实包。
2. 增加只读原生 scene/blob 接口并接 Android WebView，验证 worker/模块、本地来源和机型 WebGL2 能力。
3. 加入 BCn/ASTC 适配、按需加载及移动内存预算，再接组合编辑中的实时预览。
4. 验收源包多选项联动、条件 draw/texture slot、隐藏选项记忆、整体技能/武器、纹理切换和反复打开/关闭；另做真实 PC/Android GPU 测试。

本次仅评估，不修改或覆盖应用产物。已运行 DSH client.test.mjs 和 host.test.mjs，均通过；host 测试跳过了缺失的 temp/v3-check.bem 1.5 真实包夹具。client 测试使用模拟 WebGL，并不能证明真实驱动编译和画面正确。

## 参考依据

- 项目：`ui/BetterEndfieldNext.UI/Views/ToyAnalysisWindow.cs`、`android/app/build.gradle.kts`、`android/app/src/main/cpp/CMakeLists.txt`、`android/app/src/main/cpp/installer/texture_install.cpp`、`native/modules/custom_model/bem.h`。
- WebView2 分发：https://learn.microsoft.com/en-us/microsoft-edge/webview2/concepts/distribution
- WebView2 资源拦截：https://learn.microsoft.com/en-us/dotnet/api/microsoft.web.webview2.core.corewebview2.webresourcerequested
- Khronos ASTC 扩展：https://registry.khronos.org/webgl/extensions/WEBGL_compressed_texture_astc/
- Android WebViewAssetLoader 官方文档本次网络获取失败；具体系统 WebView/资源拦截行为仍需最小原型验证，不据此宣称已通过设备验证。

项目及 DSH 插件使用 AGPL-3.0-only，插件 vendored BCn 解码器标注 GPL-3.0-or-later。复用时保留对应许可证和来源说明。
