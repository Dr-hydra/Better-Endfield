# 创意工坊入口与 Android 模块管理迁移（2026-10-06）

用户反馈第三方模块页下载按钮缺失，并要求 PC 主导航使用“创意工坊”，Android 将创意工坊和第三方模块统一放到增强功能页。

## 缺失原因及来源

G 盘构建时的源码中，第三方模块页只有导入和刷新。下载站入口此前已在 E 盘工作区和远端提交 `35216279f716b9a7b90bf565ec7e25e8999705b9` 加入，但没有进入 G 盘当次源码；不是 publish 跳过了按钮文件。本轮 fetch 确认该远端提交，并沿用其中的 `https://146.235.16.65:8443/endfield/` 及系统浏览器打开方式。

该远端提交还包含完整下载站代码。本轮按用户要求迁移客户端入口，没有合并无关站点或部署改动。

## 入口变化

PC 主导航新增“创意工坊 / Workshop”，点击使用现有外部浏览器入口；保留第三方模块管理页。导航项不参与页面选择，点击后当前页和未完成操作不变。打开失败显示简短操作结果，中英文名称随原语言设置更新。

Android 增强功能页新增“第三方模块”和“创意工坊”。移除原顶层第三方模块标签，新增独立 `ThirdPartyModulesActivity` 复用原管理页；导入回调、模块配置和生命周期清理沿用原路径。原 `settings_page=third_party_modules` 冷启动与 saved page 5 兼容转入增强页后打开管理页，重建时不再次自动弹出。原自定义模型页、三项模型实验及已有配置 key 未改。

## 验证和产物

- PC 隔离窗口实际触发 ItemInvoked，拦截系统打开目标以避免启动用户浏览器。中文和英文均打开原下载站地址，保持当前第三方模块页；名称、图标和选择策略核验通过。截图和读数在 `build/pcui-layout-audit/workshop/desktop/probe-key/`。
- PC Release 单文件 publish 成功，最终日志 `workshop/desktop/publish-final.log`。
- Android 10 个真实页面解析块的旧入口/状态恢复检查，以及 9 个真实新 Activity 的导入转发、刷新、返回、滚动和清理检查通过；Manifest、顶层标签和工坊目标检查通过。生产 Activity 通过 Android 37 SDK 编译。
- Android `assembleRelease` 和原签名政策验证成功，APK Signature Scheme v2 验证通过。日志在 `build/pcui-layout-audit/workshop/android/`。
- 测试程序：`build/pcui-layout-audit/workshop/BetterEndfield-3.5.1-Workshop-test-win-x64.exe`。
- 测试 APK：`build/pcui-layout-audit/workshop/BetterEndfield-3.5.1-Workshop-test-Android-arm64.apk`。

用户运行中的 PC 窗口没有操作；BetterEndfield.ini、ui-settings.json 和模型 runtime.ini 与检查前副本逐字节一致。未替换安装或正式 Release，没有计算产物哈希。实际浏览器下载、手机页面操作尚未真机验收。

## 手柄结论

Android 系统原生提供手柄按键与摇杆事件：按键通过 KeyEvent，摇杆通过 MotionEvent 和 SOURCE_JOYSTICK 交给应用；接收系统事件仍需游戏实现对应操作。参阅 [Android 手柄输入文档](https://developer.android.com/develop/ui/views/touch-and-input/game-controllers/controller-input)。

本地 Android 游戏样本已确认 Controller=2 且原输入提供器正常分支接受请求，详见 [输入契约审计](../android-input-layout/ANDROID_INPUT_CONTRACT_AUDIT_20261006.md)。若客户端原生支持对应设备，应优先使用该模式。本轮没有添加手柄模式选择或键鼠映射；现有 PCUI 开关持续强制 Keyboard，使用原生手柄时关闭该开关可保留游戏自身输入判断。没有连接手柄验证具体按键、摇杆和设备适配。
