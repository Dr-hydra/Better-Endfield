# 第一人称退出与切人生命周期修复（2026-10-02）

范围：Windows / Android 共用相机实现。没有启动游戏复现崩溃、部署模块、改变用户配置或计算产物哈希。

## 新确认的游戏侧退出路径

本轮重新读取了 `research/il2cpp-dumps/20260903-pc-current/IL2CPP_Dump_Normal/Gameplay.Beyond.dll.cs`，并用 Capstone 对本机当前 `E:\Endfield Game\GameAssembly.dll` 做只读静态反汇编。

该文件的 `SnapshotCameraController.get_isFirstPerson`、`SetFirstPerson`、`OnTickStageChanged` 与 `_ClearFirstPersonStatus` 代码仍对应已记录的命名契约。下面的 RVA 仅用于证据定位，不用于实现：

| 方法 | 本机离线 RVA | 观察到的行为 |
| --- | --- | --- |
| `get_isFirstPerson` | `0x04A4FCC0` | 返回 `isFirstPerson` 标志 |
| `SetFirstPerson(false)` | `0x05EFF208` | 移除 first-person CCS、置 `isFirstPerson=false`、置 `m_hasPendingFirstPersonExit=true`；没有立即清除 `m_postRotation.isOverride` |
| `OnTickStageChanged` | `0x05EFE8FC` | 只有待退出且进入 `AfterTransition` 阶段时，才恢复相机参数并调用 `_ClearFirstPersonStatus` |
| `_ClearFirstPersonStatus` | `0x05EFF640` | 恢复 camera offset、清除 `CameraPostRotation.isOverride`、恢复 post-rotation 参考方向并清除待退出标志 |

所以只检查 `isFirstPerson=false` 不足以证明游戏的旋转覆写已经退出。此前本模块调用 `SetFirstPerson(false)` 后没有追踪这段延迟退出；暂停相机阶段或错过 `AfterTransition` 时，后处理旋转可继续保持覆写。这是与用户所说“关闭后旋转仍受限”相关的具体代码路径；它不是本次用户操作的运行时采样，不能宣称已经实机证明所有残留均来自此处。

另一个明确的类型问题：dump 的 `Entity` 是实现若干接口的 sealed 托管类型，并没有继承 `UnityEngine.Component`。之前直接把它传给 `Component.get_transform` 的身体绑定路径已移除。

## 已实现

- 第一人称退出在已观察到的 Unity 相机线程处理；unscaled 心跳也处理退出与角色归属，无需等待常规 TailLateTick。其他线程读取 unscaledDeltaTime 不会执行第一人称 Unity 清理。
- 相机 push 在关闭请求、功能禁用或 main Camera 改变时停止第一人称修改。第一人称只允许与会话摄像机同 GameObject 的 Brain；详情/过场 Brain 不复用角色眼位。
- Android 对第一人称独立追踪 pump，不再由自由相机 heartbeat 的 generation 代表第一人称也处理过。配置请求即使排在一次既有 pump 后，也会在后备渲染帧中处理。
- 请求处理与完整帧处理分开：频繁的 unscaled 读取只做生命周期，不反复进行 GPU 捕获或身体转向。
- 主控角色为空、角色切换、ModelGo 变化或头/身体/颈锚点失效时，先停用旧 pose/facing，恢复旧 Renderer，再释放旧根引用；待新模型完整后统一绑定。相机 push 前再次检查当前主控和 ModelGo，覆盖角色切换发生在 TailLateTick 与 Brain push 之间的窗口。
- 绑定使用当前 `Entity → modelCom → ModelGo → Transform`；不使用另一个 `Player` tag 或模型根代替头骨。角色、ModelGo、body、head、neck 的托管 wrapper 均保留生命周期引用。
- 身体转向保留写入前的旋转；退出时仅在当前旋转仍等于本模块最后写入值时恢复，避免覆盖游戏已经接管的移动/动作朝向。
- 对 Cinemachine `CameraState&` 的第一人称覆写限定在原 push 调用期间，返回后还原原始位置、修正、FOV 和 near clip，避免污染被重复使用的游戏缓存。
- 仅追踪本会话真正调用过 `SetFirstPerson(false)` 的 Snapshot 退出。验证待退出标志、活动标志及所需 Unity 组件存活后，通过具名 `_ClearFirstPersonStatus` 完成该退出；恢复不就绪时保留引用并在后续主线程 pump 重试。用户随后重新启用的游戏拍照第一人称不被清理；其他会话留下的 pending 状态也不被任意修改。
- 第一人称退出后仍重试失败的 Mesh 恢复，保留已分配资源的所有权；重复恢复失败日志限为状态变化时一次。

没有向游戏的俯仰/水平范围字段写入任何猜测值。`first_person_side_look_limit` 仍是本模块身体转向的触发角度，游戏相机朝向仍由输入管线生成。

## 验证

`native/tests/camera_playback/first_person_tests.cpp` 包含实际生产翻译单元，以类型化场景对象模拟 Host。误把 Entity 当 Component、访问已销毁的 Transform，都会立即使测试失败。

集成后的 Windows Release 测试，**313 项断言通过**（包含最初 222 项检查及骨架归属补充）：

- 关闭后只驱动 unscaled 心跳、工作线程 heartbeat 拒绝 Unity 操作、关闭请求在下一 push 前生效。
- 主 Brain 与另一 Brain 隔离；原 push 看到修改后的状态，返回后原始缓存字节完全恢复。
- A → null → B、旧角色仍存活、旧锚点已销毁、同 Entity 更换 ModelGo。
- 同一个 ModelGo 内替换骨架、旧骨骼仍存活但已脱离当前身体：先停止旧锚点访问，再绑定新骨骼。
- 身体转向的所属恢复，以及游戏接管后的旋转保留。
- main Camera 更换结束会话。
- Snapshot 待退出在无常规阶段 tick 时完成；组件缺失时保留清理所有权，后续恢复；用户自己的新拍照会话及无所属 pending 状态保留。

构建：

```powershell
cmake -S native/tests/camera_playback -B artifacts/camera-lifecycle-tests -A x64
cmake --build artifacts/camera-lifecycle-tests --config Release --target camera_first_person_tests
& artifacts/camera-lifecycle-tests/Release/camera_first_person_tests.exe
```

Windows `BetterEndfield.Camera` 生产目标编译通过。`native/tests/android_rebuild/camera_test.cpp` 增加了自由相机 generation 不能掩盖第一人称关闭、连续后备帧关闭及同帧新退出请求三条生产回归；Linux 主机模拟套件需有 Clang/JDK 的 Linux 环境，本机未安装 WSL，因此本子任务没有声称运行了该套件。Android 原生编译由本轮集成检查另行记录。

集成审核还增加了 `unity.object.op_equality`、`unity.component.game_object` 和 `unity.transform.parent.get` 的第一人称必需契约，避免缺少存活/归属检查时继续开启；Android 的第一人称 generation 仅在完整帧 pump 增加，轻量 heartbeat/push 清理不能掩盖下一 nativeRender 的完整更新。

## 仍需实机核验

已有日志没有本次第一人称切人崩溃栈。上面的错类型调用、null 过渡保留旧锚点、相机会话归属和退出调度均有代码证据并已修复，但尚不能把其中某一项称为该次崩溃唯一根因。

发布或测试构建应在双端核验：普通退出、冻结世界后退出、连续切角色/队伍、进入退出详情/拍照/菜单、切后台返回，以及 BEM 模型热切换。如果第一人称已 inactive 且 Snapshot 的 pending/rotation override 已解除后仍有限角，需要记录对应版本的游戏相机控制状态继续定位，而不是扩大无条件相机字段修改。
