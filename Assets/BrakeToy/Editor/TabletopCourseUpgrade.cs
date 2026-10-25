using System;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace BrakeToy.Editor
{
    public static class TabletopCourseUpgrade
    {
        private const string Root = "Assets/BrakeToy/Art/Adventure/Prefabs/";

        // Explicit, bounded migration; never rebuild the room or old toy prefabs.
        public static void Apply()
        {
            var scene = EditorSceneManager.OpenScene(AdventureSceneBuilder.ScenePath);
            var game = Object.FindFirstObjectByType<GameManager>();
            var definitions = game.Arcade.Definitions.Take(7).ToList();
            var book = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/BrakeToy/Art/Workshop/Prefabs/Hazard_0.prefab");
            var sources = new[] { book.GetComponent<ArcadeHazard>().Model.gameObject,
                GameObject.Find("Spiral notebook"), GameObject.Find("Pencil cup") };
            string[] names = { "Desk book", "Spiral notebook", "Pencil cup" };
            for (int i = 0; i < sources.Length; i++)
            {
                if (!sources[i]) throw new InvalidOperationException("Missing desk model " + names[i]);
                var root = new GameObject(names[i]);
                try
                {
                    var hazard = root.AddComponent<ArcadeHazard>();
                    var model = Object.Instantiate(sources[i], root.transform);
                    model.name = names[i] + " visual";
                    model.transform.localPosition = Vector3.zero;
                    model.transform.localRotation = Quaternion.identity;
                    model.transform.localScale = Vector3.one;
                    foreach (var collider in model.GetComponentsInChildren<Collider>()) Object.DestroyImmediate(collider);
                    foreach (var behaviour in model.GetComponentsInChildren<MonoBehaviour>()) Object.DestroyImmediate(behaviour);
                    var renderers = model.GetComponentsInChildren<Renderer>();
                    Bounds bounds = renderers[0].bounds;
                    foreach (var renderer in renderers.Skip(1)) bounds.Encapsulate(renderer.bounds);
                    float scale = Mathf.Min(1.5f / bounds.size.x, 1.9f / bounds.size.z, 1.9f / bounds.size.y);
                    model.transform.localScale = Vector3.one * scale;
                    model.transform.localPosition = new Vector3(-bounds.center.x * scale,
                        -bounds.min.y * scale, -bounds.center.z * scale);
                    hazard.Model = model.transform;
                    hazard.HalfWidth = bounds.size.x * scale * .5f;
                    hazard.HalfLength = bounds.size.z * scale * .5f;
                    hazard.Height = bounds.size.y * scale;
                    var prefab = PrefabUtility.SaveAsPrefabAsset(root, Root + names[i] + ".prefab");
                    string path = Root + "DeskDefinition_" + i + ".asset";
                    var definition = AssetDatabase.LoadAssetAtPath<ObstacleDefinition>(path);
                    bool fresh = !definition;
                    if (fresh) definition = ScriptableObject.CreateInstance<ObstacleDefinition>();
                    definition.Kind = i + 7;
                    definition.PoolSize = 16;
                    definition.Prefab = prefab.GetComponent<ArcadeHazard>();
                    definition.HalfWidth = hazard.HalfWidth;
                    definition.HalfLength = hazard.HalfLength;
                    definition.ReadabilityNotes = "Restored desk object; collision encloses visible mesh bounds.";
                    if (fresh) AssetDatabase.CreateAsset(definition, path);
                    else EditorUtility.SetDirty(definition);
                    definitions.Add(definition);
                }
                finally { Object.DestroyImmediate(root); }
            }
            game.Arcade.Definitions = definitions.ToArray();
            game.Arcade.BuildCatalogPool = true;
            game.GetComponent<AdventureEncounters>().DenseCourse = true;
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            Debug.Log("TABLETOP COURSE upgraded: ten hazard kinds, desk models restored, room preserved.");
        }
    }
}
