# Windows 相机增强输入修复（2026-10-01）

反馈：主键盘热键有效，自由相机的 PageUp/PageDown、鼠标和小键盘等控制无效。

## 排查结果

主键盘热键通过 `GetAsyncKeyState` 读取。导航区和小键盘则优先依赖
底层键盘 Hook 的扫描码状态；原来的键盘和鼠标 Hook 都将游戏 EXE 的句柄
传给 `SetWindowsHookExW`，回调实际位于相机 DLL，且安装失败没有诊断日志。
微软要求 `hmod` 对应回调所在的 DLL，见
[SetWindowsHookExW](https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw)。

原鼠标控制将回调里的屏幕坐标减去 `GetCursorPos`，依赖“光标位置尚未更新”
这一假设。Windows API 探针使用模拟鼠标事件时，两者相同，计算结果为 `(0, 0)`。
这项探针检查的是 API 时序；反馈现场的真实硬件和游戏尚未验收。

## 修改

- 使用回调地址取得键盘 Hook 所属模块。安装失败记录错误码，每秒重试，
  同一错误不重复刷日志。
- 观察游戏成功调用 `GetRawInputData`、`GetRawInputBuffer` 得到的输入包。
  鼠标使用相对移动计数和有符号滚轮量，适用于游戏锁定/重置光标的情况。
- 同时读取原始键盘扫描码，作为导航区、小键盘输入的另一来源。
  保持 NumLock 开关下的物理键区分，独立 PageUp/PageDown 与小键盘 9/3
  不混用，主 Enter 与小键盘 Enter 不混用。
- 仅在游戏有焦点时处理输入；鼠标还要求自由相机正在运行且鼠标转向已启用。
  查询长度、只读包头、读取失败和截断包不会产生相机输入。
- 沿用游戏已有的设备注册、消息处理和返回值。绝对设备坐标不作为相对位移。

## 验证

- MSVC x64 Release 相机 DLL 构建通过。
- 相机测试项目 8/8 通过；新增 Windows 输入测试包含 104 项检查，覆盖单包和
  批量读取、鼠标转向/FOV、按下/释放、两种 NumLock 状态、焦点和异常数据。
- 同步修正旧角色运行时测试的配置写入方式，使其匹配现有生产配置类型。
- DLL：`artifacts/betterendfield-native-build/stage/Release/modules/BetterEndfield.Camera.dll`。

待游戏内验收：退出游戏后替换相机 DLL；进入自由相机，检查方向键移动、
PageUp/PageDown 升降、鼠标转向、滚轮 FOV、小键盘滚转/FOV/重置，以及冻结世界
后的控制。分别检查 NumLock 开关、切后台再返回和鼠标转向选项关闭的情况。
