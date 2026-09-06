import { useMemo, useState } from "preact/hooks";
import {
  SUPPORTED_SCHEMA,
  buildSkillIndex,
  buffDisplay,
  buffShortName,
  characterIcon,
  characterName,
  dungeonName,
  formatCompact,
  formatDuration,
  formatNumber,
  iconPath,
  skillDisplay,
  skillName,
  suitName,
  talentName,
  totalRdps,
  weaponName,
} from "../lib/combat";
import {
  CONTRIBUTION_CATEGORIES,
  SKILL_CATEGORIES,
  contributionCategory,
  stackingType,
  stackingTypeName,
} from "../lib/categories";
import { describeNominalEffects } from "../lib/buffTerms";
import { useBuffTable } from "../lib/useBuffTable";
import type {
  CombatDictionary,
  CombatRecord,
  ContributorSummary,
  EquipmentDictionaryEntry,
  SquadMember,
  SuitDictionaryEntry,
  WeaponDictionaryEntry,
} from "../types";
import { TimelineTracks } from "./TimelineTracks";

interface Props {
  record: CombatRecord;
  dictionary: CombatDictionary;
  sourceLabel?: string;
  actions?: preact.ComponentChildren;
}

const attrNames: Record<number, string> = {
  1: "生命值", 2: "攻击力", 3: "防御力", 39: "力量", 40: "敏捷", 41: "智识", 42: "意志",
};

function imageFallback(event: Event) {
  (event.currentTarget as HTMLImageElement).style.display = "none";
}

export function CombatDetail({ record, dictionary, sourceLabel = `SCHEMA ${SUPPORTED_SCHEMA}`, actions }: Props) {
  const [tab, setTab] = useState<"overview" | "contributors" | "skills" | "diagnostics">("overview");
  const rdps = totalRdps(record);
  const criticalRate = record.hitCount ? record.criticalCount / record.hitCount * 100 : 0;
  const skillIndex = useMemo(() => buildSkillIndex(record.squad, dictionary), [record.squad, dictionary]);
  // Changes when the shipped buff table arrives, so the icons it carries
  // replace the borrowed portraits without a reload.
  const buffTable = useBuffTable(record);

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

      <SquadStrip record={record} dictionary={dictionary} skillIndex={skillIndex} />

      <TimelineTracks record={record} dictionary={dictionary} skillIndex={skillIndex} buffTable={buffTable} />

      <nav class="detail-tabs">
        {([
          ["overview", "输出构成"],
          ["contributors", "增益归因"],
          ["skills", "技能明细"],
          ["diagnostics", "记录自检"],
        ] as const).map(([id, label]) => (
          <button key={id} class={tab === id ? "active" : ""} onClick={() => setTab(id)}>{label}</button>
        ))}
      </nav>

      {tab === "overview" && <OverviewPanel record={record} dictionary={dictionary} />}
      {tab === "contributors" && <ContributorPanel record={record} dictionary={dictionary} />}
      {tab === "skills" && <SkillPanel record={record} dictionary={dictionary} skillIndex={skillIndex} />}
      {tab === "diagnostics" && <DiagnosticsPanel record={record} />}
    </main>
  );
}

function SquadStrip({
  record,
  dictionary,
  skillIndex,
}: {
  record: CombatRecord;
  dictionary: CombatDictionary;
  skillIndex: ReturnType<typeof buildSkillIndex>;
}) {
  const [openId, setOpenId] = useState<string | null>(null);
  const ordered = useMemo(() => {
    const rank = (member: SquadMember) => record.characters[member.charId]?.damage ?? 0;
    return [...record.squad].sort((a, b) => rank(b) - rank(a));
  }, [record.characters, record.squad]);
  const open = ordered.find((member) => member.charInstId === openId) ?? null;

  return (
    <section class="squad-block" aria-label="队伍与装备状态">
      <div class="squad-strip">
        {ordered.map((member) => {
          const direct = record.characters[member.charId];
          const contribution = record.rdpsCharacters[member.charId];
          const weaponRaw = member.weapon ? dictionary.w[member.weapon.templateId] : undefined;
          const weapon = typeof weaponRaw === "string" ? null : weaponRaw as WeaponDictionaryEntry | undefined;
          const active = member.charInstId === openId;
          return (
            <button
              type="button"
              class={`squad-chip ${active ? "active" : ""}`}
              key={member.charInstId}
              aria-expanded={active}
              onClick={() => setOpenId(active ? null : member.charInstId)}
            >
              <div class="avatar-frame">
                <img src={characterIcon(dictionary, member.charId)} alt="" onError={imageFallback} />
                <span>{characterName(dictionary, member.charId).slice(0, 1)}</span>
              </div>
              <div class="squad-chip-copy">
                <h3>{characterName(dictionary, member.charId)}</h3>
                <p>LV.{member.level} · 突破 {member.breakStage} · 潜能 {member.potential}</p>
                <div class="squad-chip-loadout">
                  {member.weapon ? (
                    <span class="loadout-tag weapon">
                      <i class="item-icon tiny">{weapon?.i && <img src={iconPath("items", weapon.i)} alt="" onError={imageFallback} />}</i>
                      {weaponName(dictionary, member.weapon.templateId)}
                    </span>
                  ) : <span class="loadout-tag empty">未装备武器</span>}
                  {member.equipSuits.map((suit) => (
                    <span class="loadout-tag suit" key={suit.suitId}>{suitName(dictionary, suit.suitId)}<em>×{suit.count}</em></span>
                  ))}
                </div>
              </div>
              <div class="squad-chip-metrics">
                <span><b>{formatNumber((direct?.damage ?? 0) / record.durationSeconds)}</b><small>DPS</small></span>
                <span><b>{formatNumber((contribution?.damage ?? 0) / record.durationSeconds)}</b><small>rDPS</small></span>
                <i class="squad-chip-caret">{active ? "收起 ▲" : "详情 ▼"}</i>
              </div>
            </button>
          );
        })}
      </div>
      {open && <SquadDetail member={open} dictionary={dictionary} skillIndex={skillIndex} />}
    </section>
  );
}

function SquadDetail({
  member,
  dictionary,
  skillIndex,
}: {
  member: SquadMember;
  dictionary: CombatDictionary;
  skillIndex: ReturnType<typeof buildSkillIndex>;
}) {
  const weaponRaw = member.weapon ? dictionary.w[member.weapon.templateId] : undefined;
  const weapon = typeof weaponRaw === "string" ? null : weaponRaw as WeaponDictionaryEntry | undefined;
  return (
    <div class="squad-detail">
      <div class="squad-detail-block">
        <h4>技能</h4>
        <div class="detail-list">
          {member.skillGroups.map((skill) => {
            const id = skill.skillGroupId || skill.skillId || "";
            const display = skillDisplay(skillIndex, dictionary, id);
            const talent = display.icon ? "" : talentName(dictionary, id);
            const icon = display.icon || characterIcon(dictionary, member.charId);
            const category = SKILL_CATEGORIES.find((item) => item.id === display.category);
            return (
              <div class="detail-row" key={id}>
                <span class="square-icon">{icon && <img src={icon} alt="" onError={imageFallback} />}</span>
                <span class="detail-copy">
                  <b>{talent || display.name}</b>
                  <small>{category?.name ?? (talent ? "天赋" : "技能")}</small>
                </span>
                <strong>{skill.level}<i>/{skill.maxLevel}</i></strong>
              </div>
            );
          })}
          {!member.skillGroups.length && <p class="empty-inline">未记录技能</p>}
        </div>
      </div>

      <div class="squad-detail-block">
        <h4>武器</h4>
        {member.weapon ? (
          <div class="detail-row wide">
            <span class="square-icon large">{weapon?.i && <img src={iconPath("items", weapon.i)} alt="" onError={imageFallback} />}</span>
            <span class="detail-copy">
              <b>{weapon?.n || member.weapon.templateId}</b>
              <small>LV.{member.weapon.level} · 突破 {member.weapon.breakthroughLevel} · 潜能 {member.weapon.refineLevel}</small>
              {weapon?.sn && <small class="detail-effect">{weapon.sn}</small>}
            </span>
          </div>
        ) : <p class="empty-inline">未装备武器</p>}
        <h4 class="spaced">套装</h4>
        <div class="detail-list">
          {member.equipSuits.map((item) => {
            const raw = dictionary.su[item.suitId];
            const suit = typeof raw === "string" ? null : raw as SuitDictionaryEntry | undefined;
            return (
              <div class="detail-row" key={item.suitId}>
                <span class="square-icon">{suit?.i && <img src={iconPath("suits", suit.i)} alt="" onError={imageFallback} />}</span>
                <span class="detail-copy">
                  <b>{suit?.n || item.suitId}</b>
                  <small>{suit?.sn || `${item.count} 件套`}</small>
                </span>
                <strong>×{item.count}</strong>
              </div>
            );
          })}
          {!member.equipSuits.length && <p class="empty-inline">无套装效果</p>}
        </div>
      </div>

      <div class="squad-detail-block wide">
        <h4>装备</h4>
        <div class="detail-list two-column">
          {member.equipments.map((equipment) => {
            const data = dictionary.eq?.[equipment.templateId] as EquipmentDictionaryEntry | undefined;
            const suitRaw = data?.su ? dictionary.su[data.su] : undefined;
            const suit = typeof suitRaw === "string" ? null : suitRaw as SuitDictionaryEntry | undefined;
            return (
              <div class="detail-row" key={`${equipment.slot}:${equipment.templateId}`}>
                <span class="square-icon">{data?.i && <img src={iconPath("items", data.i)} alt="" onError={imageFallback} />}</span>
                <span class="detail-copy">
                  <b>{data?.n || equipment.templateId}</b>
                  <small>{["躯干", "手部", "饰品", "模块"][data?.p ?? equipment.slot] || `部位 ${equipment.slot}`} · {suit?.n || "散件"}</small>
                  {data?.m?.length ? (
                    <small class="detail-effect">
                      {data.m.slice(0, 4).map((modifier) =>
                        `${attrNames[modifier.type] || `属性${modifier.type}`} ${formatNumber(modifier.value, modifier.value < 10 ? 1 : 0)}${
                          equipment.enhanceLevels[String(modifier.index)] ? ` +${equipment.enhanceLevels[String(modifier.index)]}` : ""}`).join(" · ")}
                    </small>
                  ) : null}
                </span>
              </div>
            );
          })}
          {!member.equipments.length && <p class="empty-inline">未记录装备</p>}
        </div>
      </div>
    </div>
  );
}

function OverviewPanel({ record, dictionary }: { record: CombatRecord; dictionary: CombatDictionary }) {
  const direct = useMemo(
    () => Object.entries(record.characters).sort((a, b) => b[1].damage - a[1].damage),
    [record.characters],
  );
  const contribution = useMemo(
    () => Object.entries(record.rdpsCharacters).sort((a, b) => b[1].damage - a[1].damage),
    [record.rdpsCharacters],
  );
  const maxRdps = Math.max(1, ...contribution.map(([, value]) => value.damage));
  const usedCategories = useMemo(() => {
    const totals = Array(CONTRIBUTION_CATEGORIES.length).fill(0);
    for (const [, value] of contribution) {
      value.rdpsByContribution.forEach((amount, index) => { totals[index] += amount; });
    }
    return CONTRIBUTION_CATEGORIES.map((category, index) => ({ category, index, total: totals[index] }))
      .filter((entry) => entry.total > 0);
  }, [contribution]);

  return (
    <section class="analysis-grid">
      <article class="panel chart-panel">
        <div class="section-heading"><div><span class="eyebrow">OUTPUT / 直接伤害</span><h2>伤害构成</h2></div></div>
        <div class="bar-chart">
          {direct.map(([id, value]) => (
            <div class="bar-row" key={id}>
              <span>{characterName(dictionary, id)}</span>
              <div class="stacked">
                {value.damageByCategory.map((amount, index) => amount > 0 && (
                  <i
                    key={index}
                    title={`${SKILL_CATEGORIES[index].name} ${formatCompact(amount)}`}
                    style={{ width: `${amount / record.totalDamage * 100}%`, background: SKILL_CATEGORIES[index].color }}
                  />
                ))}
              </div>
              <strong>{formatNumber(value.damage / record.durationSeconds)}</strong>
              <small>{(value.damage / record.totalDamage * 100).toFixed(1)}%</small>
            </div>
          ))}
        </div>
        <div class="legend">
          {SKILL_CATEGORIES.map((category) => (
            <span key={category.id}><i style={{ background: category.color }} />{category.name}</span>
          ))}
        </div>
      </article>

      <article class="panel chart-panel">
        <div class="section-heading"><div><span class="eyebrow">RDPS / 贡献归因</span><h2>队伍贡献</h2></div></div>
        <div class="bar-chart">
          {contribution.map(([id, value]) => (
            <div class="bar-row" key={id}>
              <span>{characterName(dictionary, id)}</span>
              <div class="stacked">
                {value.rdpsByContribution.map((amount, index) => amount > 0 && (
                  <i
                    key={index}
                    title={`${CONTRIBUTION_CATEGORIES[index].name} ${formatCompact(amount)}`}
                    style={{ width: `${amount / maxRdps * 100}%`, background: CONTRIBUTION_CATEGORIES[index].color }}
                  />
                ))}
              </div>
              <strong>{formatNumber(value.damage / record.durationSeconds)}</strong>
              <small>{(value.damage / record.totalDamage * 100).toFixed(1)}%</small>
            </div>
          ))}
        </div>
        <div class="legend">
          {usedCategories.map(({ category, total }) => (
            <span key={category.id} title={category.hint}>
              <i style={{ background: category.color }} />{category.name}
              <em>{formatCompact(total)}</em>
            </span>
          ))}
        </div>
      </article>
    </section>
  );
}

function ContributorPanel({ record, dictionary }: { record: CombatRecord; dictionary: CombatDictionary }) {
  const [source, setSource] = useState("all");
  const sources = useMemo(
    () => [...new Set(record.contributors.map((item) => item.sourceId).filter(Boolean))],
    [record.contributors],
  );
  const rows = useMemo(
    () => record.contributors.filter((item) => source === "all" || item.sourceId === source),
    [record.contributors, source],
  );
  const maxCredited = Math.max(1, ...rows.map((row) => row.credited));

  return (
    <section class="panel contributor-panel">
      <div class="section-heading">
        <div>
          <span class="eyebrow">ATTRIBUTION / 每次命中的证据</span>
          <h2>增益归因明细</h2>
        </div>
        <label class="inline-select">
          <span>提供者</span>
          <select value={source} onChange={(event) => setSource((event.currentTarget as HTMLSelectElement).value)}>
            <option value="all">全部</option>
            {sources.map((id) => <option value={id} key={id}>{characterName(dictionary, id)}</option>)}
          </select>
        </label>
      </div>
      <div class="contributor-head">
        <span>状态</span><span>提供者</span><span>类别</span><span>机制</span>
        <span>命中</span><span>平均倍率</span><span>归功伤害</span>
      </div>
      <div class="contributor-rows">
        {rows.map((row) => {
          const category = contributionCategory(row.contributionType);
          const display = buffDisplay(dictionary, {
            buffId: row.buffId,
            sourceKind: row.sourceKind,
            sourceTemplateId: row.sourceTemplateId,
            sourceSkillId: row.sourceSkillId,
            sourceId: row.sourceId,
            contributionType: row.contributionType,
          });
          return (
            <div class={`contributor-row ${row.excluded ? "excluded" : ""}`} key={row.key}>
              <span class="contributor-name">
                <i class="square-icon tiny">{display.icon && <img src={display.icon} alt="" onError={imageFallback} />}</i>
                <span class="contributor-copy">
                  <b>{display.name}{row.peakLayers > 1 ? ` ×${row.peakLayers}` : ""}</b>
                  <small class="mono">
                    {[display.kind, buffShortName(row.buffId), stackingSummary(row)].filter(Boolean).join(" · ")}
                  </small>
                  {/* What the buff is configured to do, next to what the fight
                      got out of it. A gap between the two is the interesting
                      part: it means the buff was up but not being used. */}
                  {!!row.nominalEffects?.length && (
                    <small class="contributor-nominal">{describeNominalEffects(row.nominalEffects)}</small>
                  )}
                </span>
              </span>
              <span>{row.sourceId ? characterName(dictionary, row.sourceId) : "—"}{row.external ? "" : " · 自身"}</span>
              <span><i class="swatch" style={{ background: category.color }} />{category.name}</span>
              <span class="mono">{mechanismLabel(row.mechanism)}</span>
              <span class="mono">{row.hits}</span>
              <span class="mono">
                ×{row.meanMultiplier.toFixed(3)}
                {row.maxMultiplier - row.minMultiplier > 0.001 && (
                  <small> {row.minMultiplier.toFixed(3)}–{row.maxMultiplier.toFixed(3)}</small>
                )}
              </span>
              <span class="contributor-credit">
                <i style={{ width: `${row.credited / maxCredited * 100}%`, background: category.color }} />
                <b>{formatNumber(row.credited)}</b>
              </span>
            </div>
          );
        })}
        {!rows.length && <p class="empty-inline">该记录没有归因到任何增益</p>}
      </div>
    </section>
  );
}

// Only worth showing when the game actually lets this status carry layers;
// the replace-style types would just add noise to every row.
function stackingSummary(row: ContributorSummary): string {
  const stacking = row.stacking;
  if (!stacking) return "";
  const type = stackingType(stacking.type);
  if (!type?.layered) return "不叠加";
  return stacking.maxStack > 0
    ? `${stackingTypeName(stacking.type)} 上限 ${stacking.maxStack} 层`
    : `${stackingTypeName(stacking.type)} 无上限`;
}

function mechanismLabel(mechanism: string): string {
  switch (mechanism) {
    case "zoneWrite": return "乘区写入";
    case "attribute": return "属性";
    case "instantModifier": return "瞬时修饰";
    case "artsStrength": return "法术强度";
    default: return mechanism;
  }
}

function SkillPanel({
  record,
  dictionary,
  skillIndex,
}: {
  record: CombatRecord;
  dictionary: CombatDictionary;
  skillIndex: ReturnType<typeof buildSkillIndex>;
}) {
  const skills = useMemo(
    () => Object.entries(record.skills).sort((a, b) => b[1].damage - a[1].damage),
    [record.skills],
  );
  const max = Math.max(1, ...skills.map(([, value]) => value.damage));
  return (
    <section class="panel skill-panel">
      <div class="section-heading"><div><span class="eyebrow">DETAIL / 技能明细</span><h2>技能伤害</h2></div></div>
      <div class="damage-table" role="table">
        {skills.map(([id, value], index) => {
          const display = skillDisplay(skillIndex, dictionary, id);
          return (
            <div class="damage-row" role="row" key={id}>
              <span>{String(index + 1).padStart(2, "0")}</span>
              <span class="square-icon">
                {display.icon && <img src={display.icon} alt="" onError={imageFallback} />}
              </span>
              <b>
                {skillName(dictionary, id)}
                <small>
                  {characterName(dictionary, value.actorId)} · {SKILL_CATEGORIES[value.category].name}
                  {display.maxLevel ? ` · LV.${display.level}/${display.maxLevel}` : ""}
                </small>
              </b>
              <div class="damage-bar">
                <i style={{ width: `${value.damage / max * 100}%`, background: SKILL_CATEGORIES[value.category].color }} />
              </div>
              <strong>{formatNumber(value.damage)}</strong>
              <small>{value.hits} HIT · 暴 {value.hits ? Math.round(value.criticalHits / value.hits * 100) : 0}%</small>
            </div>
          );
        })}
      </div>
    </section>
  );
}

function DiagnosticsPanel({ record }: { record: CombatRecord }) {
  const attribution = record.diagnostics?.attribution;
  const checks = attribution ? [
    { label: "带账本的命中", value: `${attribution.hits}`, ok: true },
    { label: "含外部贡献的命中", value: `${attribution.hitsWithExternal}`, ok: true },
    { label: "修饰器集合读取失败", value: `${attribution.liveSetFailures}`, ok: attribution.liveSetFailures === 0 },
    { label: "属性求和不一致", value: `${attribution.groupSumMismatch}`, ok: attribution.groupSumMismatch === 0 },
    { label: "乘区残差无法解释", value: `${attribution.zoneResidualUnexplained}`, ok: attribution.zoneResidualUnexplained === 0 },
    { label: "补记的迟到 buff", value: `${attribution.lateBuffs}`, ok: true },
    { label: "已排除的失效叠层", value: `${attribution.staleMapBuffs}`, ok: true },
    { label: "不随攻击力缩放的命中", value: `${attribution.attackUnlinked}`, ok: true },
  ] : [];

  return (
    <section class="panel diagnostics-panel">
      <div class="section-heading">
        <div><span class="eyebrow">SELF-CHECK / 记录自检</span><h2>归因质量</h2></div>
        <span class="data-note">游戏版本 {record.dictionary.gameVersion} · {record.dictionary.hotfixVersion}</span>
      </div>
      {attribution ? (
        <div class="diagnostic-grid">
          {checks.map((check) => (
            <div class={`diagnostic-card ${check.ok ? "" : "warn"}`} key={check.label}>
              <span>{check.label}</span>
              <strong>{check.value}</strong>
            </div>
          ))}
        </div>
      ) : <p class="empty-inline">该记录没有自检数据</p>}

      <h3 class="subheading">伤害乘区表</h3>
      <div class="zone-table">
        {record.zones.map((zone) => (
          <div class="zone-row" key={zone.index}>
            <span class="mono">#{zone.index}</span>
            <b>{zone.name}</b>
            <span>{zone.multiply ? "乘法区" : "加法区"}{zone.mergeSides ? " · 双方合并" : ""}</span>
            <span class="zone-roles">
              {zone.roles.length ? zone.roles.map((role) => <em key={role}>{roleLabel(role)}</em>) : <em class="muted">—</em>}
            </span>
          </div>
        ))}
        {!record.zones.length && <p class="empty-inline">该记录没有乘区表</p>}
      </div>
    </section>
  );
}

function roleLabel(role: string): string {
  switch (role) {
    case "damageType": return "伤害类型";
    case "ignite": return "点燃/异常";
    case "skillType": return "技能类型";
    case "brokenUnit": return "破韧";
    case "enhanced": return "增幅";
    case "vulnerable": return "脆弱";
    default: return role;
  }
}
