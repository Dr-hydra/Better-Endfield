"use strict";

const cloud = require("@cloudbase/node-sdk");
const app = cloud.init({ env: cloud.SYMBOL_CURRENT_ENV });
const db = app.database();

exports.main = async () => {
  const expired = await db.collection("combat_records")
    .where({ ranked: false, expireAt: db.command.lt(new Date()) })
    .limit(100)
    .get();
  let deleted = 0;
  for (const record of expired.data || []) {
    const parts = await db.collection("combat_record_parts").where({ recordId: record._id }).get();
    await Promise.all((parts.data || []).map((part) => db.collection("combat_record_parts").doc(part._id).remove()));
    await db.collection("combat_records").doc(record._id).remove();
    deleted += 1;
  }
  const staleUploads = await db.collection("combat_uploads")
    .where({ createdAt: db.command.lt(new Date(Date.now() - 24 * 60 * 60 * 1000)) })
    .limit(100)
    .get();
  await Promise.all((staleUploads.data || []).map((item) => db.collection("combat_uploads").doc(item._id).remove()));
  return { deleted, staleUploads: staleUploads.data?.length || 0 };
};
