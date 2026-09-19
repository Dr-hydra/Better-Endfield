# 模型消费入口续查（2026-09-14）

实机 0.0.37：安洁 FinishWithAsset asset=00000044F82B6F80，tick=4526593；16ms 后带位置父节点 Instantiate 使用相同 original，clone=00000044F82D1920，41 个 SMR。未筛选 SMS-ENTER 为零，未达到日志预算；此前 NPC 装配路径仍未获运行时支持。

本轮只对 5 个消费方法局部解码，model-consumer.json：

- BaseModelViewPart._OnLoadModelFinish，0370B9D0；在 0370BA64 调用 OnLoadFinish。
- BaseModelViewPart._OnLoadUseHandleFinish，0370B730；在 0370B7CF 调用 OnLoadFinish。
- BaseModelViewPart.OnLoadFinish，0370BAC0；存在间接分派，不能仅据调用名称表判断缺失。
- BaseModelViewPart.PostDealLoadedModel，0372E700；0372E79F 调用 Transform.SetParent，随后处理变换与渲染器。dump 保存模型于 m_model、SMR 数组于 m_meshes。
- ComplexModelViewPart.PostDealLoadedModel，0372E200；0372E226 调用基类 PostDealLoadedModel，随后效果设置、RenderHelper、取得 Animator，并在 0372E32E 读取 runtimeAnimatorController。正常实现的这段不走 CreateSMSInfoForPostModel；IFix 分支仍可能分派其他实现。

这是一条独立于 NPCAvatarManager 的 gameplay view 消费候选；尚不能仅凭类名认定安洁确实使用它，也未定位完整骨骼初始化。

0.0.38 通过 metadata 添加基类/Complex 两个 void(instance,MethodInfo) 后处理钩子，在原调用前后以 get_model 得到对象，记录模型、Renderer、Mesh 身份与 bones 数量。仅目标角色日志，不留存对象，不替换资源、不设置骨骼。编译和 diff check 通过，已更新测试目录，需实机确认。
