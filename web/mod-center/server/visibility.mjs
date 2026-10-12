export const visibilityOf = row => row.admin_hidden ? 'admin-hidden' : row.author_hidden ? 'author-hidden' : 'public';

export function createVisibility({ db, canAdmin }) {
  const columns = new Set(db.prepare('PRAGMA table_info(resources)').all().map(column => column.name));
  if (!columns.has('author_hidden')) db.exec('ALTER TABLE resources ADD COLUMN author_hidden INTEGER NOT NULL DEFAULT 0 CHECK(author_hidden IN (0,1))');
  if (!columns.has('admin_hidden')) db.exec('ALTER TABLE resources ADD COLUMN admin_hidden INTEGER NOT NULL DEFAULT 0 CHECK(admin_hidden IN (0,1))');
  if (!columns.has('hide_reason')) db.exec("ALTER TABLE resources ADD COLUMN hide_reason TEXT NOT NULL DEFAULT ''");
  const canRead = (row, user) => visibilityOf(row) === 'public' || Boolean(user && (row.owner_id === user.id || canAdmin(user)));
  const requireReadable = (row, user) => {
    if (!canRead(row, user)) {
      const visibility = visibilityOf(row);
      throw Object.assign(new Error(visibility === 'admin-hidden' ? '作品已被管理员隐藏，暂时无法查看。' : '作品已被作者隐藏，暂时无法查看。'), { status: 403, visibility, reason: row.hide_reason });
    }
  };
  function change(row, user, hidden, reason) {
    if (canAdmin(user)) {
      if (hidden) db.prepare('UPDATE resources SET admin_hidden=1,hide_reason=? WHERE id=?').run(reason, row.id);
      else db.prepare("UPDATE resources SET admin_hidden=0,author_hidden=0,hide_reason='' WHERE id=?").run(row.id);
    } else {
      if (row.owner_id !== user.id) throw Object.assign(new Error('只有作者或管理员可以更改作品展示状态。'), { status: 403 });
      if (row.admin_hidden) throw Object.assign(new Error('作品已被管理员隐藏，只有管理员可以恢复。'), { status: 403 });
      db.prepare("UPDATE resources SET author_hidden=?,hide_reason='' WHERE id=?").run(hidden ? 1 : 0, row.id);
    }
  }
  return { canRead, requireReadable, change };
}
