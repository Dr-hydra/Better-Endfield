# OneChannelLC 父进程校验局部分析

日期：2026-10-15。对象：当前 CN 1.6.3 `G:\Arknights Endfield\OneChannelLC.dll`（395,240 字节）。本记录仅针对该 DLL 的静态逻辑，不代表其他渠道配置相同。没有修改游戏文件、启动游戏或建立 GameAssembly 全量数据库。

## 核心结论

当前校验不是单纯匹配 `Games.exe` 名称。它读取**直接父进程**的完整映像路径，提取文件名与配置允许列表比较，然后验证该磁盘文件的 Authenticode 签名及签名证书指纹。自行编译同名 helper 不能满足后两项。

本段检查没有对安装目录作固定路径比较，也没有遍历祖先进程。完整路径用于查找父进程对应的磁盘文件和验证其签名。真正的、签名完整且指纹符合当前配置的官方 Games.exe 满足这些静态条件；仅凭静态分析不宣布任何替代启动流程已实测成功。

## 函数及判断顺序

地址为相对于 DLL ImageBase 的 RVA；首选基址是 `0x180000000`。

| RVA | 作用 | 直接证据 |
| --- | --- | --- |
| `0x8C70` | 取得给定 PID 的直接父 PID | Toolhelp 进程快照中匹配 PID，返回 `PROCESSENTRY32W.th32ParentProcessID` |
| `0x8D00` | 取得进程完整路径并小写化 | `OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION)`、`QueryFullProcessImageNameW`、`towlower` |
| `0x144D0` | 主校验 | 仅对 `getpid()` 调一次 `0x8C70`；从路径提取 basename；依序执行名称、信任及证书指纹检查 |
| `0x89A0` | 字符串比较 | 两端转小写，要求长度相同并 `memcmp` 全部相同；不是子串或正则 |
| `0x13AA0` | 签名信任检查 | 动态调用 `WinVerifyTrust`，使用 `WINTRUST_ACTION_GENERIC_VERIFY_V2` GUID 和文件路径 |
| `0x140C0` | 从签名读取证书 | `CryptQueryObject` 读取文件的 PKCS#7 嵌入签名，再提取签名者证书 |
| `0x13B70` | 枚举签名者/嵌套签名 | `CryptMsgGetParam`、`CertFindCertificateInStore` 等 |
| `0x141D0` | 提取证书指纹 | 读取证书属性 3（SHA-1）和 107（SHA-256），交给十六进制编码函数 |
| `0x8B60` | 指纹编码 | 每字节转换为两个十六进制字符，未加入冒号或空格 |

主校验中的结果码：

- `10100`：没有允许的父进程名称命中。
- `10101`：名称命中，但签名信任检查失败，或无法读取签名证书信息。
- `10102`：配置包含证书指纹列表，但签名证书指纹没有命中。
- `0`：检查成功。

指纹配置非空时，对枚举到的签名证书指纹与配置逐项比较，至少一项命中才能通过。指纹配置为空时仍先执行签名信任检查和证书读取，不能等同于关闭校验。

## 配置和导出 ABI

`0xEA70` 中四个编码键已复原：`checkItems`、`name`、`parentNames`、`fingerprints`。结构如下，实际名称和指纹必须取自当前 U8SDK 配置；这里不将示例当作当前完整允许列表。

```json
{
  "checkItems": [
    {
      "name": "Launcher",
      "parentNames": ["Games.exe"],
      "fingerprints": ["actual_certificate_fingerprint_from_current_config"]
    }
  ]
}
```

Windows x64 标准 ABI：

```cpp
// Ordinal 1, RVA 0x2D9D0：同步执行校验并返回结果码。
uint32_t OCL_1(const char* userDataPath, const char* configJson);

// Ordinal 2, RVA 0x2DC60：内部 JSON 结果字符串，调用方不释放。
const char* OCL_2();

// Ordinal 3, RVA 0x2DC80：尝试启动官方启动器，不是校验所需接口。
bool OCL_3(int isCN);
```

首参不是回调。`0x2D420` 将首参作为字符串转换并追加 `\sdklogs\OneChannelLC.log`；可指定研究目录避免写入正式游戏日志。配置为空、JSON 不合法或没有检查项时返回 `10000`。

## 复现资料

- `allowlist-OneChannelLC.i64`：仅这个约 395 KB DLL 的研究库，约 6.7 MB。
- `allowlist-ida-export.py`：导出上述函数和直接调用者的局部伪代码。
- `allowlist-function-*.c`：IDA 生成伪代码；部分 C++ 返回对象参数未完整恢复，应结合调用点和汇编判断。
- `allowlist-decode.py` / `allowlist-decoded.json`：静态复原配置键及动态加载的加密 API 名，不执行客户端代码，不计算产物哈希。

早先“导入表未见 WinVerifyTrust/Cert/Crypt”不能用于排除签名校验：这些 API 由 `0xDB10` 解码名称，再经 `LoadLibraryExA` 和 `GetProcAddress` 动态解析，现已获得直接证据。
