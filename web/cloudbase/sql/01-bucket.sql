-- 这是这个后端唯一需要的 SQL：建一个公开可读的存储桶。
--
-- 排行榜的数据面完全不碰数据库。本环境的 PostgreSQL 按核秒计费，而且是被唤醒就
-- 开始算、不是按查询的 CPU 算——公开榜单每个访客都是一次读，用数据库撑读路径
-- 等于花钱买实例常驻（实测一个开发日 828 核秒 = 78.7 资源点）。数据全部是云存储
-- 对象，浏览器用 <script> 从桶前面的 CDN 直接拉。
--
-- 对象大小上限设成 2 MB：最大的对象是分类榜单（每关前三名 × 至多 49 关），
-- 实际不到 100 KB，留一个数量级的余量。

insert into storage.buckets (id, name, public, file_size_limit)
values ('combat', 'combat', true, 2000000)
on conflict (id) do update set public = true, file_size_limit = 2000000;
