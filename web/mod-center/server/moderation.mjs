export function createModeration({ db, canAdmin, text, fail, rate, transaction }) {
  const columns = new Set(db.prepare('PRAGMA table_info(users)').all().map(column => column.name));
  if (!columns.has('banned_at')) db.exec('ALTER TABLE users ADD COLUMN banned_at INTEGER');
  if (!columns.has('ban_reason')) db.exec("ALTER TABLE users ADD COLUMN ban_reason TEXT NOT NULL DEFAULT ''");
  if (!columns.has('banned_by')) db.exec("ALTER TABLE users ADD COLUMN banned_by TEXT NOT NULL DEFAULT ''");

  const isBanned = user => Boolean(user?.banned_at);
  const banMessage = user => user?.ban_reason ? `账号已被封禁（原因：${user.ban_reason}）。` : '账号已被封禁，无法执行此操作。';
  const requireNotBanned = user => {
    if (isBanned(user)) fail(403, banMessage(user));
  };
  const canBanTarget = (actor, target) => Boolean(actor && target && canAdmin(actor) && target.id !== actor.id && !canAdmin(target));

  const adminUserView = row => ({
    id: row.id,
    login: row.login,
    avatar: row.avatar,
    profile_url: row.profile_url,
    github_id: row.github_id || '',
    is_admin: canAdmin(row),
    banned: Boolean(row.banned_at),
    banned_at: row.banned_at || null,
    ban_reason: row.ban_reason || '',
    works_count: row.works_count ?? db.prepare('SELECT COUNT(*) AS n FROM resources WHERE owner_id=?').get(row.id).n,
    comments_count: row.comments_count ?? db.prepare('SELECT COUNT(*) AS n FROM comments WHERE user_id=?').get(row.id).n,
  });

  function setBan({ actor, targetId, banned, reason = '', hideWorks = false, deleteComments = false }) {
    if (!canAdmin(actor)) fail(403, '只有管理员可以封禁或解封用户。');
    rate(`ban:${actor.id}`, 60, 3600000);
    if (typeof banned !== 'boolean') fail(400, '账号封禁状态不正确。');
    const target = db.prepare('SELECT * FROM users WHERE id=?').get(targetId);
    if (!target) fail(404, '用户不存在。');
    if (target.id === actor.id || canAdmin(target)) fail(403, '不能封禁管理员账号。');
    const cleanReason = banned ? text(reason, 500, '封禁原因') : '';
    const now = Date.now();
    transaction(() => {
      if (banned) {
        db.prepare('UPDATE users SET banned_at=?,ban_reason=?,banned_by=? WHERE id=?').run(now, cleanReason, actor.id, target.id);
        db.prepare('DELETE FROM sessions WHERE user_id=?').run(target.id);
        if (hideWorks) {
          db.prepare('UPDATE resources SET admin_hidden=1,hide_reason=? WHERE owner_id=? AND admin_hidden=0')
            .run(cleanReason || '发布者账号已被封禁', target.id);
        }
        if (deleteComments) {
          db.prepare('DELETE FROM comments WHERE user_id=?').run(target.id);
        }
      } else {
        db.prepare("UPDATE users SET banned_at=NULL,ban_reason='',banned_by='' WHERE id=?").run(target.id);
      }
    });
    return adminUserView(db.prepare('SELECT * FROM users WHERE id=?').get(target.id));
  }

  function listUsers({ viewer, query = '', banned = '', page = 1 }) {
    if (!canAdmin(viewer)) fail(403, '只有管理员可以查看用户管理列表。');
    if (banned && !['1', '0'].includes(banned)) fail(400, '账号封禁状态不正确。');
    const where = [], params = [];
    if (query) {
      where.push('(u.login LIKE ? OR u.github_id LIKE ? OR u.id LIKE ?)');
      params.push(...Array(3).fill(`%${query}%`));
    }
    if (banned === '1') where.push('u.banned_at IS NOT NULL');
    if (banned === '0') where.push('u.banned_at IS NULL');
    const clause = where.length ? `WHERE ${where.join(' AND ')}` : '';
    const total = db.prepare(`SELECT COUNT(*) AS total FROM users u ${clause}`).get(...params).total;
    const bannedTotal = db.prepare('SELECT COUNT(*) AS total FROM users WHERE banned_at IS NOT NULL').get().total;
    const rows = db.prepare(`
      SELECT u.*,
        (SELECT COUNT(*) FROM resources r WHERE r.owner_id=u.id) AS works_count,
        (SELECT COUNT(*) FROM comments c WHERE c.user_id=u.id) AS comments_count
      FROM users u ${clause}
      ORDER BY (u.banned_at IS NOT NULL) DESC, COALESCE(u.banned_at, 0) DESC, u.login ASC
      LIMIT 24 OFFSET ?
    `).all(...params, (page - 1) * 24);
    return {
      items: rows.map(adminUserView),
      total,
      bannedTotal,
      page,
      pages: Math.max(1, Math.ceil(total / 24)),
    };
  }

  return { isBanned, banMessage, requireNotBanned, canBanTarget, adminUserView, setBan, listUsers };
}
