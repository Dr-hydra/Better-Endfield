/**
 * Layout helper for the plugin's own tests.
 *
 * `lib/index.js` resolves `skills/bem-creator` relative to itself, which is the
 * shape the package has after installation. In the repository the skill lives
 * under `tools/CustomModel/` and is copied in by `scripts/BuildDshPlugin.py`, so
 * a test that runs from a checkout has to stage the two together. Inside the
 * staged tree the directories already sit side by side and nothing is copied.
 */
import { cpSync, existsSync, mkdtempSync } from 'node:fs'
import { tmpdir } from 'node:os'
import { dirname, join, resolve } from 'node:path'
import { fileURLToPath } from 'node:url'

/** The `dsh-plugin` directory this test tree lives in. */
export const pluginDirectory = resolve(dirname(fileURLToPath(import.meta.url)), '..')

/**
 * Return a directory where `lib/` and `skills/` sit beside each other.
 *
 * @returns {{ root: string, temporary: string | null }} `temporary` is the
 * directory to remove afterwards, or null when no copy was needed.
 */
export function stagePlugin() {
  if (existsSync(join(pluginDirectory, 'skills/bem-creator/SKILL.md'))) {
    return { root: pluginDirectory, temporary: null }
  }
  const source = resolve(pluginDirectory, '../skills/bem-creator')
  if (!existsSync(join(source, 'SKILL.md'))) {
    throw new Error(`skill source not found at ${source}; run this test from the repository or a staged package`)
  }
  const temporary = mkdtempSync(join(tmpdir(), 'bem-plugin-test-'))
  cpSync(join(pluginDirectory, 'lib'), join(temporary, 'lib'), { recursive: true })
  cpSync(source, join(temporary, 'skills/bem-creator'), {
    recursive: true,
    filter: (from) => !/[\\/]__pycache__([\\/]|$)/.test(from) && !from.endsWith('.pyc'),
  })
  return { root: temporary, temporary }
}
