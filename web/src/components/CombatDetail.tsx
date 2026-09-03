import { useMemo } from "preact/hooks";
import {
  characterName,
  dungeonName,
  formatDuration,
  formatNumber,
  iconPath,
  skillEntry,
  skillName,
  totalRdps,
} from "../lib/combat";
import type {
  CombatDictionary,
  CombatRecordV11,
  EquipmentDictionaryEntry,
  SkillDictionaryEntry,
  SuitDictionaryEntry,
  WeaponDictionaryEntry,
} from "../types";
import { TimelineCanvas } from "./TimelineCanvas";

interface Props {
  record: CombatRecordV11;
  dictionary: CombatDictionary;
  sourceLabel?: string;
  actions?: preact.ComponentChildren;
}

const attrNames: Record<number, string> = {
  1: "生命值",
  2: "攻击力",
  3: "防御力",
  39: "力量",
  40: "敏捷",
  41: "智识",
  42: "意志",
};

function imageFallback(event: Event) {
  (event.currentTarget as HTMLImageElement).style.display = "none";
}

export function CombatDetail({ record, dictionary, sourceLabel = "SCHEMA 11", actions }: Props) {
  const rdps = totalRdps(record);
  const characters = useMemo(
    () => Object.entries(record.characters).sort((a, b) => b[1].damage - a[1].damage),
    [record.characters],
  );
  const skills = useMemo(
    () => Object.entries(record.skills).sort((a, b) => b[1].damage - a[1].damage).slice(0, 12),
    [record.skills],
  );
  const criticalRate = record.hitCount ? record.criticalCount / record.hitCount * 100 : 0;

  return (
    <main class="detail-page">
      <header class="detail-hero panel">
        <div>
          <span class="eyebrow">{sourceLabel} / {dungeonName(dictionary, record.dungeonId)}</span>
          <h1>{dungeonName(dictionary, record.dungeonId) || "战斗记录"}</h1>
          <p>{new Date(record.startedUnixSeconds * 1000).toLocaleString("zh-CN")} · SESSION {record.sessionId}</p>
        </div>
        <div class="hero-actions">{actions}</div>
      </header>

      <section class="metric-grid" aria-label="核心战斗数据">
        <article class="metric-card metric-primary"><span>DPS</span><strong>{formatNumber(record.dps)}</strong><small>每秒有效伤害</small></article>
        <article class="metric-card"><span>rDPS</span><strong>{formatNumber(rdps)}</strong><small>含队伍贡献归因</small></article>
        <article class="metric-card"><span>总伤害</span><strong>{formatNumber(record.totalDamage)}</strong><small>{formatNumber(record.hitCount)} 次命中</small></article>
        <article class="metric-card"><span>战斗用时</span><strong>{formatDuration(record.durationSeconds)}</strong><small>暴击率 {criticalRate.toFixed(1)}%</small></article>
      </section>

      <section class="panel" aria-labelledby="squad-title">
        <div class="section-heading">
          <div><span class="eyebrow">LOADOUT / 配置快照</span><h2 id="squad-title">队伍详情</h2></div>
          <span class="data-note">记录开始时</span>
        </div>
        <div class="squad-grid">
          {record.squad.map((member) => {
            const char = dictionary.c[member.charId];
            const charData = typeof char === "string" ? null : char;
            const weaponRaw = member.weapon ? dictionary.w[member.weapon.templateId] : undefined;
            const weapon = typeof weaponRaw === "string" ? null : weaponRaw as WeaponDictionaryEntry | undefined;
            return (
              <article class="operator-card" key={member.charInstId}>
                <div class="operator-header">
                  <div class="avatar-frame">
                    {charData?.i && <img src={`./${charData.i}`} alt="" onError={imageFallback} />}
                    <span>{characterName(dictionary, member.charId).slice(0, 1)}</span>
                  </div>
                  <div><h3>{characterName(dictionary, member.charId)}</h3><p>LV.{member.level} · 突破 {member.breakStage} · 潜能 {member.potential}</p></div>
                  <span class="rarity">{charData?.r ? `${charData.r}★` : "—"}</span>
                </div>
                <div class="loadout-block">
                  <h4>技能等级</h4>
                  <div class="skill-list">
                    {member.skillGroups.map((skill) => {
                      const id = skill.skillGroupId || skill.skillId || "";
                      const entry = skillEntry(dictionary, id);
                      const data = typeof entry === "string" ? null : entry as SkillDictionaryEntry | undefined;
                      return <div class="skill-row" key={id}>
                        <span class="square-icon">{data?.i && <img src={iconPath("skills", data.i)} alt="" onError={imageFallback} />}</span>
                        <span><b>{skillName(dictionary, id)}</b><small>{data?.c || "skill"}</small></span>
                        <strong>{skill.level}<i>/{skill.maxLevel}</i></strong>
                      </div>;
                    })}
                  </div>
                </div>
                <div class="loadout-block">
                  <h4>武器</h4>
                  {member.weapon ? <div class="weapon-row">
                    <span class="item-icon">{weapon?.i && <img src={iconPath("items", weapon.i)} alt="" onError={imageFallback} />}</span>
                    <span><b>{weapon?.n || member.weapon.templateId}</b><small>LV.{member.weapon.level} · 突破 {member.weapon.breakthroughLevel}</small></span>
                    <em>潜能 {member.weapon.refineLevel}</em>
                    {(weapon?.sn || member.weapon.skills.length > 0) && <p class="weapon-skill">{weapon?.sn || skillName(dictionary, member.weapon.skills[0]?.skillId || "")} · LV.{member.weapon.skills[0]?.level || member.weapon.refineLevel + 1}</p>}
                  </div> : <p class="empty-inline">未装备武器</p>}
                </div>
                <div class="loadout-block">
                  <h4>装备</h4>
                  <div class="equipment-list">
                    {member.equipments.map((equipment) => {
                      const data = dictionary.eq?.[equipment.templateId] as EquipmentDictionaryEntry | undefined;
                      const suitRaw = data?.su ? dictionary.su[data.su] : undefined;
                      const suit = typeof suitRaw === "string" ? null : suitRaw as SuitDictionaryEntry | undefined;
                      return <div class="equipment-row" key={`${equipment.slot}:${equipment.templateId}`}>
                        <span class="item-icon small">{data?.i && <img src={iconPath("items", data.i)} alt="" onError={imageFallback} />}</span>
                        <span class="equipment-copy"><b>{data?.n || equipment.templateId}</b><small>{["躯干", "手部", "饰品", "模块"][data?.p ?? equipment.slot] || `部位 ${equipment.slot}`} · {suit?.n || "散件"}</small>
                          {data?.m?.length ? <span class="attribute-line">{data.m.slice(0, 3).map((modifier) => `${attrNames[modifier.type] || `属性${modifier.type}`} ${formatNumber(modifier.value, modifier.value < 10 ? 1 : 0)}${equipment.enhanceLevels[String(modifier.index)] ? ` +${equipment.enhanceLevels[String(modifier.index)]}` : ""}`).join(" · ")}</span> : null}
                        </span>
                      </div>;
                    })}
                    {!member.equipments.length && <p class="empty-inline">未记录装备</p>}
                  </div>
                </div>
                {!!member.equipSuits.length && <div class="suit-tags">{member.equipSuits.map((item) => {
                  const raw = dictionary.su[item.suitId];
                  const suit = typeof raw === "string" ? null : raw as SuitDictionaryEntry | undefined;
                  return <span key={item.suitId}>{suit?.n || item.suitId} ×{item.count}{suit?.sn ? ` · ${suit.sn}` : ""}</span>;
                })}</div>}
              </article>
            );
          })}
        </div>
      </section>

      <section class="analysis-grid">
        <article class="panel chart-panel">
          <div class="section-heading"><div><span class="eyebrow">OUTPUT / 角色贡献</span><h2>伤害构成</h2></div></div>
          <div class="bar-chart">
            {characters.map(([id, value]) => <div class="bar-row" key={id}>
              <span>{characterName(dictionary, id)}</span>
              <div><i style={{ width: `${Math.max(2, value.damage / record.totalDamage * 100)}%` }} /></div>
              <strong>{formatNumber(value.damage / record.durationSeconds)}</strong>
              <small>{(value.damage / record.totalDamage * 100).toFixed(1)}%</small>
            </div>)}
          </div>
        </article>
        <article class="panel skill-panel">
          <div class="section-heading"><div><span class="eyebrow">DETAIL / 技能明细</span><h2>技能伤害</h2></div></div>
          <div class="damage-table" role="table">
            {skills.map(([id, value], index) => <div class="damage-row" role="row" key={id}>
              <span>{String(index + 1).padStart(2, "0")}</span><b>{skillName(dictionary, id)}</b>
              <strong>{formatNumber(value.damage)}</strong><small>{formatNumber(value.hits)} HIT</small>
            </div>)}
          </div>
        </article>
      </section>

      <TimelineCanvas record={record} dictionary={dictionary} />
    </main>
  );
}
