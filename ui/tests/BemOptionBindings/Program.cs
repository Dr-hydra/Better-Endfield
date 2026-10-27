using System.Text.Json;
using BetterEndfieldNext.UI.Services;

using var document = JsonDocument.Parse("""
{
  "target": {
    "resources": [{"id":"body","name":"world_postmodel"},{"id":"ui","name":"ui_uimodel"}],
    "components": [
      {"id":0,"mesh_name":"SharedLongMeshName","resource":"body","renderer_path":"root/body/renderer"},
      {"id":1,"mesh_name":"SharedLongMeshName","resource":"ui","renderer_path":"root/ui/renderer"},
      {"id":2,"mesh_name":"Accessory","resource":"body","renderer_path":"root/accessory"}
    ]
  },
  "option_groups": [
    {"id":"outfit"},
    {"id":"part","available_when":{"eq":["outfit","b"]}},
    {"id":"color"},{"id":"constraint_only"}
  ],
  "texture_slots": [{"id":"color_slot","candidates":[
    {"texture":0,"when":{"eq":["color","red"]}},
    {"texture":null,"when":{"not":{"eq":["color","red"]}}}
  ]}],
  "meshes": [{"draws":[{"when":{"eq":["part","on"]},"textures":[{"slot":"color_slot"}]}]}],
  "component_rules": [
    {"target":0,"candidates":[{"operation":"replace","mesh":0}]},
    {"target":1,"candidates":[{"operation":"hide","when":{"eq":["outfit","b"]}}]},
    {"target":2,"candidates":[{"operation":"keep","material_overrides":[{"textures":[{"slot":"color_slot"}]}]}]}
  ],
  "selection_constraints": [{"eq":["constraint_only","yes"]}]
}
""");
int checks = 0;
void Expect(bool result, string message) { if (!result) throw new Exception(message); checks++; }
var bindings = BemOptionBindings.Read(document.RootElement);
Expect(bindings["outfit"].Select(c => c.Id).SequenceEqual(new[] { 0, 1 }), "Availability dependencies must propagate to dependent parts.");
Expect(bindings["part"].Select(c => c.Id).SequenceEqual(new[] { 0 }), "Draw predicates must bind their owning receiver.");
Expect(bindings["color"].Select(c => c.Id).SequenceEqual(new[] { 0, 2 }), "Texture-only controls must include draws and keep-material overrides.");
Expect(bindings["constraint_only"].Count == 0, "A constraint must not invent a direct component binding.");
Expect(bindings["outfit"].Count == 2, "Equal names in different resources must remain distinct.");
Expect(bindings["outfit"][0].FullName.Contains("root/body/renderer"), "Full renderer path must be available for inspection/copy.");
Expect(bindings["outfit"][1].Resource == "ui_uimodel", "Explicit resource names must be retained.");
string longName = new string('模', 240) + "_末尾";
using var longDocument = JsonDocument.Parse(document.RootElement.GetRawText().Replace("SharedLongMeshName", longName));
Expect(BemOptionBindings.Read(longDocument.RootElement)["outfit"][0].Name == longName, "Display shortening must never truncate stored names.");
using var legacy = JsonDocument.Parse("{\"target\":{\"components\":[]}}");
Expect(BemOptionBindings.Read(legacy.RootElement).Count == 0, "Legacy packages have no synthetic source options in metadata.");
Console.WriteLine($"{checks} option binding checks passed.");
