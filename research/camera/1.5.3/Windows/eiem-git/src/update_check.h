#pragma once

#include <winhttp.h>
#pragma comment(lib, "winhttp.lib")


static int CompareVersions(const char *a, const char *b) {
  if (!a || !b) return 0;
  if (*a == 'v' || *a == 'V') a++;
  if (*b == 'v' || *b == 'V') b++;

  int a1 = 0, a2 = 0, a3 = 0;
  int b1 = 0, b2 = 0, b3 = 0;
  sscanf(a, "%d.%d.%d", &a1, &a2, &a3);
  sscanf(b, "%d.%d.%d", &b1, &b2, &b3);

  if (a1 != b1) return a1 < b1 ? -1 : 1;
  if (a2 != b2) return a2 < b2 ? -1 : 1;
  if (a3 != b3) return a3 < b3 ? -1 : 1;
  return 0;
}

static std::string HttpGet(const wchar_t *host, const wchar_t *path,
                           int timeout_ms = 5000,
                           const wchar_t *extraHeaders = nullptr) {
  std::string result;

  HINTERNET hSession = WinHttpOpen(
      L"EIEM-UpdateCheck/1.0",
      WINHTTP_ACCESS_TYPE_DEFAULT_PROXY,
      WINHTTP_NO_PROXY_NAME,
      WINHTTP_NO_PROXY_BYPASS, 0);
  if (!hSession) return result;

  WinHttpSetTimeouts(hSession, timeout_ms, timeout_ms, timeout_ms, timeout_ms);

  HINTERNET hConnect = WinHttpConnect(hSession, host,
                                      INTERNET_DEFAULT_HTTPS_PORT, 0);
  if (!hConnect) {
    WinHttpCloseHandle(hSession);
    return result;
  }

  HINTERNET hRequest = WinHttpOpenRequest(
      hConnect, L"GET", path, NULL,
      WINHTTP_NO_REFERER,
      WINHTTP_DEFAULT_ACCEPT_TYPES,
      WINHTTP_FLAG_SECURE);
  if (!hRequest) {
    WinHttpCloseHandle(hConnect);
    WinHttpCloseHandle(hSession);
    return result;
  }

  if (extraHeaders) {
    WinHttpAddRequestHeaders(hRequest, extraHeaders, (DWORD)-1,
                             WINHTTP_ADDREQ_FLAG_ADD);
  }

  BOOL ok = WinHttpSendRequest(hRequest,
                                WINHTTP_NO_ADDITIONAL_HEADERS, 0,
                                WINHTTP_NO_REQUEST_DATA, 0, 0, 0);
  if (!ok) {
    WinHttpCloseHandle(hRequest);
    WinHttpCloseHandle(hConnect);
    WinHttpCloseHandle(hSession);
    return result;
  }

  ok = WinHttpReceiveResponse(hRequest, NULL);
  if (!ok) {
    WinHttpCloseHandle(hRequest);
    WinHttpCloseHandle(hConnect);
    WinHttpCloseHandle(hSession);
    return result;
  }

  DWORD statusCode = 0;
  DWORD statusSize = sizeof(statusCode);
  WinHttpQueryHeaders(hRequest,
                      WINHTTP_QUERY_STATUS_CODE | WINHTTP_QUERY_FLAG_NUMBER,
                      WINHTTP_HEADER_NAME_BY_INDEX,
                      &statusCode, &statusSize, WINHTTP_NO_HEADER_INDEX);
  if (statusCode != 200) {
    Log("[UPDATE] HTTP %d from %S%S", statusCode, host, path);
    WinHttpCloseHandle(hRequest);
    WinHttpCloseHandle(hConnect);
    WinHttpCloseHandle(hSession);
    return result;
  }

  DWORD bytesAvailable = 0;
  DWORD bytesRead = 0;
  char buf[4096];
  while (WinHttpQueryDataAvailable(hRequest, &bytesAvailable) &&
         bytesAvailable > 0) {
    DWORD toRead = (bytesAvailable < sizeof(buf)) ? bytesAvailable : sizeof(buf);
    if (WinHttpReadData(hRequest, buf, toRead, &bytesRead) && bytesRead > 0) {
      result.append(buf, bytesRead);
    }
    if (result.size() > 65536) break;
  }

  WinHttpCloseHandle(hRequest);
  WinHttpCloseHandle(hConnect);
  WinHttpCloseHandle(hSession);
  return result;
}

static std::string JsonExtractString(const std::string &json, const char *key) {
  char pattern[128];
  snprintf(pattern, sizeof(pattern), "\"%s\"", key);

  size_t pos = json.find(pattern);
  if (pos == std::string::npos) return "";

  pos = json.find(':', pos + strlen(pattern));
  if (pos == std::string::npos) return "";

  pos = json.find('"', pos + 1);
  if (pos == std::string::npos) return "";
  pos++;

  std::string value;
  for (size_t i = pos; i < json.size(); i++) {
    if (json[i] == '\\' && i + 1 < json.size()) {
      char next = json[i + 1];
      if (next == '"') { value += '"'; i++; }
      else if (next == 'n') { value += '\n'; i++; }
      else if (next == 'r') { value += '\r'; i++; }
      else if (next == 't') { value += '\t'; i++; }
      else if (next == '\\') { value += '\\'; i++; }
      else { value += json[i]; }
    } else if (json[i] == '"') {
      break;
    } else {
      value += json[i];
    }
  }
  return value;
}

static void CheckForUpdates() {
  g_updateChecking = true;
  g_updateCheckFailed = false;
  g_updateIsLatest = false;
  g_updateAvailable = false;
  g_updateResultTime = 0;
  Log("[UPDATE] Checking for updates... (current: v%s)", EIEM_VERSION);

  bool found = false;
  std::string version, url, changelog;

  {
    std::string body = HttpGet(
        L"api.github.com",
        L"/repos/Sasye/EIEM/releases/latest",
        3000,
        L"Accept: application/vnd.github+json\r\nUser-Agent: EIEM-UpdateCheck/1.0\r\n");

    if (!body.empty()) {
      version = JsonExtractString(body, "tag_name");
      url = JsonExtractString(body, "html_url");
      changelog = JsonExtractString(body, "body");

      if (!version.empty()) {
        Log("[UPDATE] GitHub API: latest=%s", version.c_str());
        found = true;
      }
    }
    if (!found) {
      Log("[UPDATE] GitHub API unreachable, trying jsDelivr fallback...");
    }
  }

  if (!found) {
    std::string body = HttpGet(
        L"cdn.jsdelivr.net",
        L"/gh/Sasye/EIEM@main/VERSION",
        3000);

    if (!body.empty()) {
      size_t nl = body.find('\n');
      if (nl != std::string::npos) {
        version = body.substr(0, nl);
        while (!version.empty() && (version.back() == '\r' || version.back() == ' '))
          version.pop_back();

        url = body.substr(nl + 1);
        while (!url.empty() && (url.back() == '\r' || url.back() == '\n' || url.back() == ' '))
          url.pop_back();

        if (!version.empty()) {
          Log("[UPDATE] jsDelivr: latest=%s", version.c_str());
          found = true;
        }
      }
    }
    if (!found) {
      Log("[UPDATE] jsDelivr also unreachable, skipping update check.");
    }
  }

  if (found && CompareVersions(EIEM_VERSION, version.c_str()) < 0) {
    const char *verStr = version.c_str();
    if (*verStr == 'v' || *verStr == 'V') verStr++;
    strncpy(g_latestVersion, verStr, sizeof(g_latestVersion) - 1);
    g_latestVersion[sizeof(g_latestVersion) - 1] = '\0';

    strncpy(g_updateUrl, url.c_str(), sizeof(g_updateUrl) - 1);
    g_updateUrl[sizeof(g_updateUrl) - 1] = '\0';

    if (!changelog.empty()) {
      strncpy(g_updateChangelog, changelog.c_str(), sizeof(g_updateChangelog) - 1);
      g_updateChangelog[sizeof(g_updateChangelog) - 1] = '\0';
    } else {
      g_updateChangelog[0] = '\0';
    }

    MemoryBarrier();
    g_updateAvailable = true;
    g_updateDismissed = false;

    Log("[UPDATE] New version available: v%s -> v%s", EIEM_VERSION, g_latestVersion);
    Log("[UPDATE] URL: %s", g_updateUrl);
  } else if (found) {
    Log("[UPDATE] Already up to date (v%s)", EIEM_VERSION);
    g_updateIsLatest = true;
  } else {
    Log("[UPDATE] Check failed — all endpoints unreachable.");
    g_updateCheckFailed = true;
  }

  g_updateResultTime = GetTickCount();
  g_updateChecking = false;
}

static DWORD WINAPI UpdateCheckThread(LPVOID) {
  for (int i = 0; i < 40 && !g_cursorShowAction; i++)
    Sleep(500);
  Sleep(10000);
  CheckForUpdates();
  return 0;
}
