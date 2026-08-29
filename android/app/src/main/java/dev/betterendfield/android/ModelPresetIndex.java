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

final class ModelPresetIndex {
    record Asset(String path, String pathHash, String label) {}

    record Action(
            String id,
            String displayName,
            String path,
            String pathHash,
            boolean nativeLoop) {
        @Override
        public String toString() {
            return id;
        }
    }

    record Character(
            String id,
            String displayName,
            String modelPath,
            String modelPathHash,
            String modelBundleHash,
            Asset sitLoop,
            Asset sitSpecial,
            Asset sitToWalk,
            String defaultActionId,
            List<Action> actions) {
        @Override
        public String toString() {
            return displayName;
        }
    }

    private final List<Character> characters;
    private final int actionCount;

    private ModelPresetIndex(List<Character> characters, int actionCount) {
        this.characters = characters;
        this.actionCount = actionCount;
    }

    List<Character> characters() {
        return characters;
    }

    int actionCount() {
        return actionCount;
    }

    Character findCharacter(String id) {
        for (Character character : characters) {
            if (character.id().equalsIgnoreCase(id)) return character;
        }
        return null;
    }

    static ModelPresetIndex load(Context context) throws Exception {
        JSONObject names = new JSONObject(readAsset(context, "character-names.json"));
        JSONObject root = new JSONObject(readAsset(context, "character-presets.json"));
        if (root.optInt("schemaVersion") != 1) {
            throw new IOException("桌面模型预设表版本不受支持");
        }
        JSONArray encodedCharacters = root.getJSONArray("characters");
        List<Character> characters = new ArrayList<>();
        int actionCount = 0;
        for (int index = 0; index < encodedCharacters.length(); ++index) {
            JSONObject item = encodedCharacters.getJSONObject(index);
            String id = item.getString("id");
            JSONObject model = item.getJSONObject("model");
            JSONArray encodedActions = item.getJSONArray("actions");
            List<Action> actions = new ArrayList<>();
            for (int actionIndex = 0; actionIndex < encodedActions.length(); ++actionIndex) {
                JSONObject action = encodedActions.getJSONObject(actionIndex);
                actions.add(new Action(
                        action.getString("id"),
                        action.optString("displayName", action.getString("id")),
                        action.getString("path"),
                        action.getString("pathHash"),
                        action.optBoolean("nativeLoop", false)));
            }
            actionCount += actions.size();
            characters.add(new Character(
                    id,
                    names.optString(id, id) + "  ·  " + id,
                    model.getString("path"),
                    model.getString("pathHash"),
                    model.optString("bundleHash", ""),
                    readAsset(item.getJSONObject("sitLoop")),
                    readAsset(item.getJSONObject("sitSpecial")),
                    readAsset(item.getJSONObject("sitToWalk")),
                    item.getString("defaultActionId"),
                    Collections.unmodifiableList(actions)));
        }
        if (characters.isEmpty()) {
            throw new IOException("桌面模型预设表为空");
        }
        return new ModelPresetIndex(Collections.unmodifiableList(characters), actionCount);
    }

    private static Asset readAsset(JSONObject value) throws Exception {
        return new Asset(
                value.getString("path"),
                value.getString("pathHash"),
                value.optString("displayName", ""));
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
