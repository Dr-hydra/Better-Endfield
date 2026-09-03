import { parseCombatRecordObject } from "./combat";
import dictionaryJson from "../data/combat-dict.min.json";
import type { CombatDictionary, CombatRecordV11, HomePayload, LeaderboardEntry } from "../types";

const API_BASE = (import.meta.env.VITE_CLOUDBASE_API as string | undefined)?.replace(/\/$/, "") ?? "";

function endpoint(path: string): string {
  if (!API_BASE) throw new Error("尚未配置 CloudBase API 地址");
  return `${API_BASE}${path}`;
}

async function responseJson<T>(response: Response): Promise<T> {
  const payload = (await response.json().catch(() => ({}))) as { message?: string } & T;
  if (!response.ok) throw new Error(payload.message || `请求失败（${response.status}）`);
  return payload;
}

export async function getHome(dungeonId: string): Promise<HomePayload> {
  const response = await fetch(endpoint(`/home?dungeonId=${encodeURIComponent(dungeonId)}`), {
    headers: { Accept: "application/json" },
  });
  return responseJson<HomePayload>(response);
}

export async function getPublicRecord(shortId: string): Promise<CombatRecordV11> {
  const response = await fetch(endpoint(`/records/${encodeURIComponent(shortId)}`));
  const payload = await responseJson<{ record: unknown }>(response);
  return parseCombatRecordObject(payload.record, dictionaryJson as CombatDictionary);
}

export async function publishRecord(args: {
  text: string;
  ownerToken: string;
  nickname: string;
  avatar?: string;
  bvid?: string;
}): Promise<{ shortId: string; entry: LeaderboardEntry }> {
  const multipartThreshold = 700_000;
  if (args.text.length > multipartThreshold) {
    const chunks = Array.from(
      { length: Math.ceil(args.text.length / multipartThreshold) },
      (_, index) => args.text.slice(index * multipartThreshold, (index + 1) * multipartThreshold),
    );
    const init = await fetch(endpoint("/records/multipart/init"), {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ownerToken: args.ownerToken, parts: chunks.length }),
    });
    const { uploadId } = await responseJson<{ uploadId: string }>(init);
    for (let index = 0; index < chunks.length; index += 1) {
      const part = await fetch(endpoint(`/records/multipart/${uploadId}/${index}`), {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ ownerToken: args.ownerToken, value: chunks[index] }),
      });
      await responseJson<{ ok: true }>(part);
    }
    const complete = await fetch(endpoint(`/records/multipart/${uploadId}/complete`), {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        ownerToken: args.ownerToken,
        nickname: args.nickname,
        avatar: args.avatar,
        bvid: args.bvid,
      }),
    });
    return responseJson(complete);
  }
  const response = await fetch(endpoint("/records"), {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(args),
  });
  return responseJson(response);
}

export async function updateRecordVideo(shortId: string, ownerToken: string, bvid: string) {
  const response = await fetch(endpoint(`/records/${encodeURIComponent(shortId)}/video`), {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ ownerToken, bvid }),
  });
  return responseJson<{ ok: true }>(response);
}
