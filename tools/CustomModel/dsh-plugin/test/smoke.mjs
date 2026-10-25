/**
 * Offline smoke test for the bundle plugin.
 *
 * It runs the plugin against a stub context so the packaged layout, the
 * frontmatter parse and the registered skill shape are checked without a
 * harness installation. It does not prove the harness accepts the row: that
 * needs a real `dsh plugin add` followed by `dsh skill list`.
 *
 *   node test/smoke.mjs
 */
import assert from 'node:assert/strict'
import { existsSync } from 'node:fs'
import { join } from 'node:path'
import { apply, inject, name } from '../lib/index.js'

assert.equal(name, 'bem-creator', 'bundle name must match the packaged skill name')
assert.deepEqual(inject, ['skills'], 'the plugin needs the skill registry')

const registered = []
let disposed = false
const ctx = {
  effect(fn) {
    const disposer = fn()
    return () => {
      disposed = true
      if (typeof disposer === 'function') disposer()
    }
  },
  skills: {
    register(skill) {
      registered.push(skill)
      return () => {}
    },
  },
}

apply(ctx)

assert.equal(registered.length, 1, 'the plugin must register exactly one skill')
const skill = registered[0]

assert.equal(skill.name, 'bem-creator')
assert.ok(skill.description.length > 100, 'the routing description must survive the frontmatter parse')
assert.equal(skill.source, 'bundled')
assert.equal(skill.provider, 'bem-creator')
assert.deepEqual(skill.invocation, { modelInvocable: true, userInvocable: true })
assert.ok(skill.path.endsWith('SKILL.md'), 'the instruction path must be absolute')

// The body must arrive without its frontmatter, and it must be the real body.
assert.ok(!skill.content.includes('\nname: bem-creator'), 'frontmatter leaked into the body')
assert.ok(skill.content.trimStart().startsWith('##'), 'body should start at the first heading')
assert.ok(skill.content.length > 30000, 'body looks truncated')

// resourceBase is what makes the skill's own relative references work after
// installation. Check the two kinds the body relies on.
assert.equal(skill.resourceBase.kind, 'directory')
for (const relative of ['references/BEM_SOURCE_CONVERSION_PLAYBOOK.md', 'scripts/bem_repair.py', 'scripts/bem_triage.py']) {
  assert.ok(existsSync(join(skill.resourceBase.path, relative)), `resourceBase does not resolve ${relative}`)
}

// Report every other relative reference the body makes, without failing: some
// are illustrative names rather than files.
const mentioned = new Set()
for (const match of skill.content.matchAll(/\b((?:references|scripts)\/[\w./-]+)/g)) {
  mentioned.add(match[1].replace(/[.,;:)]+$/, ''))
}
const unresolved = [...mentioned].filter(relative => {
  const base = join(skill.resourceBase.path, relative)
  return !existsSync(base) && !existsSync(`${base}.py`)
})
console.log(`checked ${mentioned.size} relative references, ${unresolved.length} do not resolve to a file`)
if (unresolved.length) console.log(`  unresolved: ${unresolved.join(', ')}`)

assert.equal(typeof skill.content, 'string')
console.log(`ok: ${skill.name} registers with a ${skill.content.length}-byte body from ${skill.resourceBase.path}`)
