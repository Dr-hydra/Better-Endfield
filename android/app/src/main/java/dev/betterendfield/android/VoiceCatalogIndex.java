package dev.betterendfield.android;

import android.content.Context;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;

final class VoiceCatalogIndex {
    record CharacterChoice(String characterId, String displayName, int languages) {}

    private final List<CharacterChoice> characters;
    private final int catalogCount;

    private VoiceCatalogIndex(List<CharacterChoice> characters, int catalogCount) {
        this.characters = characters;
        this.catalogCount = catalogCount;
    }

    List<CharacterChoice> characters() {
        return characters;
    }

    int catalogCount() {
        return catalogCount;
    }

    static VoiceCatalogIndex load(Context context) throws Exception {
        JSONObject names = new JSONObject(readAsset(context, "character-names.json"));
        JSONObject root = new JSONObject(readAsset(context, "voice-catalog-index.json"));
        if (root.optInt("schemaVersion") != 1 ||
                !"betterendfield-voice-catalog-index".equals(root.optString("kind"))) {
            throw new IOException("桌面端语气词对照表版本不受支持");
        }

        JSONArray catalogs = root.getJSONArray("catalogs");
        Map<String, Integer> coverage = new TreeMap<>();
        for (int index = 0; index < catalogs.length(); ++index) {
            JSONObject catalog = catalogs.getJSONObject(index);
            String characterId = catalog.getString("characterId");
            int language = catalog.getInt("language");
            if (!"*".equals(characterId) && language >= 0 && language < 4) {
                coverage.merge(characterId, 1 << language, (left, right) -> left | right);
            }
        }

        List<CharacterChoice> choices = new ArrayList<>();
        for (Map.Entry<String, Integer> entry : coverage.entrySet()) {
            String id = entry.getKey();
            choices.add(new CharacterChoice(
                    id,
                    names.optString(id, id) + "  ·  " + id,
                    entry.getValue()));
        }
        choices.add(new CharacterChoice("*", "其他角色（默认规则）", 0x0f));
        return new VoiceCatalogIndex(
                Collections.unmodifiableList(choices), catalogs.length());
    }

    private static String readAsset(Context context, String name) throws IOException {
        try (InputStream input = context.getAssets().open(name);
             ByteArrayOutputStream output = new ByteArrayOutputStream()) {
            byte[] buffer = new byte[8192];
            int count;
            while ((count = input.read(buffer)) >= 0) {
                output.write(buffer, 0, count);
            }
            return new String(output.toByteArray(), StandardCharsets.UTF_8);
        }
    }
}
