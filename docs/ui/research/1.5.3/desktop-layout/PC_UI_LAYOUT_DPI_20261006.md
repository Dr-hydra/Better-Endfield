# PC 管理器宽窗偏移与 DPI 修复（2026-10-06）

用户反馈不同分辨率下内容可能偏右，并明确希望页面左对齐。本轮在 `fix/android-pcui-and-desktop-layout` 分支实际复现并修正该布局问题，测试程序使用隔离配置，没有调整正在运行的用户窗口。

## 根因与改动

旧页面将 `StackPanel.MaxWidth=920` 与 Stretch 同时放在滚动视口内。窗口外部宽度 1600、客户区宽度 1584 时，开屏、界面增强、设置标题的 X 坐标分别为 618.5、536.5、675.5；同一内容上限并不能使各页位置一致，设置页部分控件超出右边界。仅设置 ScrollViewer.HorizontalContentAlignment=Stretch 的实验未解决问题；仅将 StackPanel 改为 Left 则缩成内容自身宽度，设置页只有 454，无法保持可伸缩布局。

新增 `PageContentPanel`：Grid 填满视口，左侧星号列保持原 920 内容上限，内部 StackPanel 随列宽伸展，多余空间留在右侧。10 个主页面保留原间距；BEM 创作者窗口采用同一容器，保留原 840 上限。1600 宽窗口下，上述三个标题的 X 坐标统一为 253。

另修正主窗口和 BEM/Toy/第三方模块/WebView Probe 的初始尺寸：原代码把固定数值直接传给 AppWindow.Resize；现在将原首选尺寸视作 DIP，按目标窗口实际 DPI 换算，限制在所属显示器工作区内并居中。子窗口先移到主窗口所在的显示器，再读取自身 DPI。只设置创建时的尺寸，不持续覆盖用户调整后的大小。单位差异可参阅 [Windows App SDK 窗口说明](https://learn.microsoft.com/en-us/windows/apps/develop/ui/windowing-overview)。

## 验证与产物

- 本机真实 WinUI 环境：96 DPI，显示器 2560×1440、工作区 2560×1392。初始主窗口 1180×860，位置 (690,266)。
- 13 个主页面 × 640/900/1180/1600 四个窗口宽度，共 52 个实际布局样本成功进入对应页面，命名控件无横向越界；已检查宽窄设置页和开屏页截图。
- 几何矩阵 133 项通过，覆盖 96/120/144/192 DPI、1280×720 至 3840×2160 工作区、负坐标显示器及四种首选窗口尺寸。
- 正式源码 Release 单文件 publish 成功；最终隔离布局探针构建零警告、零错误。探针和隔离配置替换仅存在于 build 副本，没有进入产品代码。
- 测试程序：`build/pcui-layout-audit/desktop/publish/BetterEndfield.exe`。
- 日志、读数和截图：`build/pcui-layout-audit/desktop/`，包括 `production-publish.log`、`geometry-tests.log`、`final-measurements/layout.json` 及 640/1600 的 model/settings PNG。

真实 150%/200% WinUI 显示、跨 DPI 显示器拖动以及带有大量真实数据的列表尚未验收；几何矩阵不代替这些实机检查。没有替换正式 Release 或本机安装，没有计算产物哈希。
