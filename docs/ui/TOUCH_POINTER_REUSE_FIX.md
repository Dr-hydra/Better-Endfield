# Windows 串流触控：UI 按下后不释放

## 方案来源与本次整合范围

本修复的指针复用问题分析及现成方案依据来自
[orangetooth/endfield-touch-patch](https://github.com/orangetooth/endfield-touch-patch)，
具体参考其 [v3 原理说明](https://github.com/orangetooth/endfield-touch-patch/blob/55a4553a126da4efc29623d03f47e881e4f1fd0e/README.md#原理)
与同一提交的 `profiles.json`。该项目的第四处修改取消了通往统一指针复用路径的条件跳转，
用于避免后续鼠标位置事件清掉尚未处理的触摸抬起状态。

这里沿用其修复思路，在 Better Endfield 的 UI 模块中实现运行时适配。
这不是提交者或问题报告者原创的触控方案；问题报告者提供了复现场景及本机实测验证。
本改动没有复制或分发游戏 DLL，也没有移植上游补丁的固定文件偏移或汇编字节。

## 复现场景

Windows PC 版启用手机 UI，通过 iOS Moonlight / Sunshine 串流操作。
部分领奖、任务跳过等按钮会显示已经按下，却不正常释放或触发点击；
连续点按可能仍无效，而电脑鼠标能够操作。原生触控输入与鼠标位置事件混用时的
统一指针状态复用，是与该现象相符的原因；仅处理鼠标转触控的抬起重试不能覆盖这一层。

## Better 中的处理方式

使用 Unity 已有的 `InputSystemUIInputModule.pointerBehavior` 设置，
在手机 UI 启用期间选用 `UIPointerBehavior.AllPointersAsIs`。
鼠标与每根手指因此使用独立 UI 指针记录。
其语义见 [Unity 官方文档](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/api/UnityEngine.InputSystem.UI.UIPointerBehavior.html)。

- 通过 Host 的元数据接口解析字段偏移、`AllPointersAsIs` 枚举值以及方法签名，不硬编码客户端 RVA。
- 在 `GetPointerStateIndexFor(InputControl, bool)` 原函数执行前应用策略。
  输入回调早于 `EventSystem.Update`，因此不能只在处理完 UI 的帧末设置。
- 同时覆盖 `Process()`，让已有 UI 实例及手机 UI 开关的恢复请求得到处理。
- 按实例保存原值，并用 GC handle 保持被保存对象的地址有效。
- 手机 UI 关闭后，在该实例下一次路由/处理输入时恢复原值；模块停止时释放所有保存状态。
  已被游戏改为其他值的设置不会被恢复流程覆盖。
- 元数据、对象保留或钩子注册失败时不强行设置指针策略，并记录原因。
- 仅接入 Windows 路径，不改变 Android 的 PC 布局实现。

该方案继续使用 Better 的手机 UI 开关和启动流程，不直接修改磁盘上的 `GameAssembly.dll`。
它仍属于非官方运行时修改，不代表规避完整性检查、反作弊或账号风险。

## 验证

报告问题的用户在 Endfield 1.6.3 / Better Endfield Next 4.0.4 / iOS Moonlight + Sunshine
环境安装本地整合构建后确认问题消失。实机反馈针对本地整合构建；
本次提交仅提取指针策略及其接入，不包含单独的鼠标释放容错或诊断改动。

`BetterEndfieldNext.TouchPointerPolicyTests` 使用假的 Host 和一个简化路由模型，检查策略在
原方法之前生效、参数及返回值透传、多指与鼠标记录分离、多个实例各自恢复、后续游戏设置、
对象保留失败、停止时清理、元数据缺失及动态枚举值。
这些测试不是 Unity 或 iOS 串流的端到端测试。

Windows 本机可构建并运行：

```powershell
cmake -S native -B build/native -A x64
cmake --build build/native --config Release --target BetterEndfieldNext.UiModule BetterEndfieldNext.TouchPointerPolicyTests
.\build\native\Release\BetterEndfieldNext.TouchPointerPolicyTests.exe
```

上面的路径对应 Visual Studio 等多配置生成器；单配置生成器的测试可执行文件直接位于
`build/native/BetterEndfieldNext.TouchPointerPolicyTests.exe`。

## 致谢

- 感谢 [@orangetooth](https://github.com/orangetooth) 在 [endfield-touch-patch](https://github.com/orangetooth/endfield-touch-patch) 项目中对 Unity 输入系统指针复用机制的深入逆向与原理解析。
- 感谢 [@yumi233](https://github.com/yumi233) 提交 [Issue #29](https://github.com/Dr-hydra/Better-Endfield/issues/29)，提供完整的复现与验证用例，并将该方案规范适配至 Better Endfield 运行时体系。
