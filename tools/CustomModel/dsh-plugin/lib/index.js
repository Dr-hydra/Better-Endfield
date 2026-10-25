/**
 * DeepSeek Harness bundle for the bem-creator skill.
 *
 * The instruction body, its references and its scripts ship inside this package
 * under `skills/bem-creator/`. Harness filesystem discovery scans a fixed set of
 * roots one level deep and cannot see inside an installed package, so the skill
 * is contributed to the registry as a runtime skill instead. `resourceBase`
 * points at the packaged directory, which is what makes the skill's own relative
 * references (`references/…`, `scripts/…`) resolve after installation.
 *
 * No `@deepseek-ai/*` package is imported. The registry contract is the only
 * surface this bundle depends on, so a harness refactor of its internal packages
 * cannot break resolution here.
 */
import { readFileSync } from 'node:fs'
import { fileURLToPath } from 'node:url'

export const name = 'bem-creator'
export const inject = ['skills']

const skillDirectory = fileURLToPath(new URL('../skills/bem-creator/', import.meta.url))
const instructionFile = fileURLToPath(new URL('../skills/bem-creator/SKILL.md', import.meta.url))

/**
 * Split a leading `---` block into flat `key: value` fields.
 *
 * The skill frontmatter is deliberately flat: only `name` and `description` are
 * read, both single-line. A full YAML parser would be a dependency this bundle
 * does not need.
 */
function splitFrontmatter(text, source) {
  const match = /^---\r?\n([\s\S]*?)\r?\n---[ \t]*(?:\r?\n|$)/.exec(text)
  if (!match) throw new Error(`${source}: SKILL.md has no frontmatter block`)
  const fields = new Map()
  for (const line of match[1].split(/\r?\n/)) {
    const pair = /^([A-Za-z0-9_-]+)[ \t]*:[ \t]*(.*)$/.exec(line)
    if (!pair) continue
    const quoted = /^(['"])([\s\S]*)\1$/.exec(pair[2].trim())
    fields.set(pair[1], quoted ? quoted[2] : pair[2].trim())
  }
  return { fields, body: text.slice(match[0].length) }
}

export function apply(ctx) {
  const { fields, body } = splitFrontmatter(readFileSync(instructionFile, 'utf8'), instructionFile)
  const skillName = fields.get('name')
  const description = fields.get('description')
  if (!skillName || !description) {
    throw new Error(`${instructionFile}: frontmatter needs both name and description`)
  }
  if (skillName !== name) {
    throw new Error(`${instructionFile}: frontmatter name "${skillName}" does not match the bundle name "${name}"`)
  }
  const skill = {
    name: skillName,
    description,
    path: instructionFile,
    content: body,
    source: 'bundled',
    provider: name,
    resourceBase: { kind: 'directory', path: skillDirectory },
    invocation: { modelInvocable: true, userInvocable: true },
  }
  const whenToUse = fields.get('whenToUse')
  if (whenToUse) skill.whenToUse = whenToUse
  // `register` returns the registry's disposer; `ctx.effect` ties it to this
  // plugin's fiber so unloading the bundle removes the skill.
  ctx.effect(() => ctx.skills.register(skill))
}
