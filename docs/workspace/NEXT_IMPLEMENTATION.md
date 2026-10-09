# Better Endfield Next 4.0.0

分支：`feat/better-endfield-next`。来源提交：`ad33357bb1bb453d5925022e2be7bf507245d6ea`。

## 产品范围

第一人称已从 Windows、Android 和共用相机运行时移除。专用文件、工具、测试及公共文件快照保存在 `legacy/retired-before-next/first-person/`，来源和 SHA-256 见归档清单，归档不参与现行构建。

第三方模块仅隐藏桌面导航、Android 增强页和旧入口路由；模块装载器、管理实现、原生 ABI 与网页桥继续保留。入口原始文件快照在同一归档的 `third-party-entry/`。新版内部 ABI 和标识已经更新，旧二进制不承诺兼容。

显示名称为 Better Endfield Next；现有 Logo 和主题素材保留。产品版本 4.0.0，BEM Tools 独立版本继续由原工具链维护。BEM/MMD 用户素材格式继续支持，需手动重新导入。

## 新安装身份

- Windows 主程序 `BetterEndfieldNext.exe`，原生文件和模块 ID、导出前缀、配置、管道、互斥量采用 Next 身份。
- Windows 安装器 AppId `5E32B9A0-79D7-4870-B1C0-93021A7CA978`，安装目录 `%LocalAppData%/Programs/Better Endfield Next`，设置目录 `%LocalAppData%/BetterEndfieldNext`。
- Android applicationId/namespace `dev.betterendfield.next`，versionCode 40000，Provider authority 和 JNI/Xposed 入口同步更新，原生库采用 `betterendfieldnext_*` 名称。
- 旧版需先卸载，Windows 同时卸载旧 XInput 代理；新版重新安装和设置。Android 重新启用框架模块、选定游戏作用域并重启游戏。
- 新版不会从旧设置目录或旧 Android 应用读取数据，不自动迁移旧缓存、模型安装索引或模块安装记录。

## 签名和混淆

Android 采用独立的新正式发布密钥，构建校验新证书 SHA-256。Windows 当前采用独立内部自签代码签名证书，签名有效性与公共 CA 信任是不同属性；没有将证书自动加入信任存储。证书指纹是可提交的发布信息，私钥、密码和本机配置被 Git 排除，应通过独立安全备份保存。

密钥初始化：`pwsh -File scripts/InitializeNextSigning.ps1 -JdkRoot <JDK>`。初始化脚本拒绝覆盖已有 Next 身份。其他机器构建应使用安全转移的同一 Next 发布材料，而不是重新生成密钥。

Android Release 默认启用 R8 名称混淆、优化和资源收缩；保留 Xposed、动态 JNI 注册、安装器原生方法/回调、WebView JavaScriptInterface 等必要契约。映射保存在构建目录 `next/android/gradle/app/outputs/mapping/release/`。

Windows Release 发布在单文件打包前使用 Obfuscar.GlobalTool 2.2.50 处理产品程序集。XAML/WinUI、公开 API 和数据模型保留必要名称，服务层非公开方法与类型可混淆。混淆输出和 Mapping.txt 保留在对应 obj 的 `next-obfuscated/`，不随安装包分发。使用 `dotnet tool install Obfuscar.GlobalTool --version 2.2.50 --tool-path toolchains/obfuscar --allow-roll-forward` 安装工具。

原生 C++ Release 启用优化与符号收敛；Windows 使用 LTO、函数/数据分段和链接去重，Android 隐藏非导出符号。没有加入原生控制流混淆或运行时加壳。内容格式、游戏方法契约、系统规定的加载名称不随意更改。

混淆映射和调试符号应与每次发布产物一起内部留档；代码哈希变化不等于所有特征消失，也不保证第三方识别结果。

## 构建

```powershell
pwsh -File scripts/BuildBetterEndfieldNext.ps1
pwsh -File scripts/BuildInstaller.ps1
android/gradlew.bat -p android :app:assembleRelease :app:lintRelease --no-daemon
```

正式 Windows 构建在打包前签名自有程序和 DLL，安装器生成后单独签名；第三方依赖保留其原签名。产物位于 `releases/4.0.0/`。Windows 和 Android 使用独立 Next 构建目录，避免复用旧安装身份的缓存。

## 验证记录

- Windows 原生模块和 WinUI 全量 Release 发布通过，Obfuscar 混淆程序集成功嵌入单文件，混淆后程序启动检查通过，窗口标题为 Better Endfield Next。
- Android `assembleRelease`、`lintRelease` 通过；R8 映射确认非入口类已重命名；APK v2 签名验证通过，包名 `dev.betterendfield.next`、versionCode 40000、versionName 4.0.0，与新证书指纹一致。
- 相机 CTest 9 项通过；Windows 第三方模块管理/通信 35 项、双端网页桥、隐藏入口路由及保留管理 Activity 生命周期回归通过。
- 修正 Windows 单独时间冻结时的模块启用条件，配置序列化 5 项及生产运行时回归通过；实际原生装载器/Echo 通信生命周期和 XInput 启动拦截 3 项通过。
- Windows 模型悬浮窗设置 28 项，Android FOV 保存/回滚、模型外观准备、热切换事务、66 项目录状态和 35 项重连策略回归通过。
- Android MMD 导入 9 个案例通过，含实际 ZIP/7z 编解码及未打包的 Zstandard 可选编解码入口拒绝。
- 工作区配置 11 项、测试注册表 17 项、新安装身份/归档边界 5 项通过；归档 SHA-256 校验通过，私钥及本机凭据确认被 Git 排除。
- 寻访网页导入导出标识调整对应的 4 项回归通过。
- Windows 安装器构建及新身份签名通过；当前证书为内部自签，系统信任状态不等同于受信任公共发布证书。

本轮未安装到游戏目录、未注入游戏、未覆盖手机安装。编译、离线回归和启动检查不替代游戏内相机、模型、MMD 等实机效果验证。
