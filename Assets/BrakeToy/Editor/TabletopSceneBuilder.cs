using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BrakeToy.Editor
{
    /// <summary>A separate, reproducible visual milestone. Phase 1 assets remain intact.</summary>
    public static class TabletopSceneBuilder
    {
        public const string ScenePath = "Assets/BrakeToy/Scenes/ToyRoomTabletop.unity";
        private const string Root = "Assets/BrakeToy/Art/Tabletop/";
        private const string Prefabs = "Assets/BrakeToy/Prefabs/Tabletop/";
        private static readonly Dictionary<string, Material> palette = new Dictionary<string, Material>();

        [MenuItem("Tools/Brake Toy/Open Bedroom Tabletop v0.2")]
        public static void Open()
        {
            if (!File.Exists(ScenePath)) Create();
            else EditorSceneManager.OpenScene(ScenePath);
        }

        [MenuItem("Tools/Brake Toy/Create Bedroom Tabletop v0.2")]
        public static void Create()
        {
            if (File.Exists(ScenePath))
            {
                Debug.Log("Tabletop scene already exists; preserved. Open " + ScenePath);
                return;
            }
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            if (!File.Exists("Assets/BrakeToy/Scenes/ToyRoom.unity")) PrototypeBuilder.Create();
            foreach (string path in new[] { Root + "Materials", Root + "Meshes", Root + "Textures", Prefabs }) Directory.CreateDirectory(path);
            AssetDatabase.Refresh();
            var scene = EditorSceneManager.OpenScene("Assets/BrakeToy/Scenes/ToyRoom.unity");
            EditorSceneManager.SaveScene(scene, ScenePath);
            palette.Clear();
            MakePalette();
            var game = Object.FindFirstObjectByType<GameManager>();
            var config = AssetDatabase.LoadAssetAtPath<GameConfig>("Assets/BrakeToy/ScriptableObjects/RoomConfig.asset");
            if (!config)
            {
                config = Object.Instantiate(game.Config);
                config.name = "RoomConfig";
                config.CameraPitch = 20;
                config.VerticalFieldOfView = 50;
                AssetDatabase.CreateAsset(config, "Assets/BrakeToy/ScriptableObjects/RoomConfig.asset");
            }
            game.name = "Brake Toy — Bedroom Tabletop v0.2";
            game.Config = config;
            var camera = Camera.main;
            camera.GetComponent<PortraitCamera>().Config = config;
            camera.GetComponent<PortraitCamera>().Reframe();
            camera.backgroundColor = new Color(.34f, .46f, .58f);

            ReplaceCar(game);
            ReplaceWorld(game.World);
            BuildBedroom();
            ImproveUI(game.UI);
            RenderSettings.fog = false;
            RenderSettings.ambientMode = AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = new Color(.45f, .53f, .62f);
            RenderSettings.ambientEquatorColor = new Color(.40f, .43f, .46f);
            RenderSettings.ambientGroundColor = new Color(.22f, .18f, .14f);
            var sun = RenderSettings.sun;
            if (sun) { sun.transform.rotation = Quaternion.Euler(42, -34, 0); sun.intensity = 1.35f; sun.color = new Color(1, .94f, .83f); }
            PlayerSettings.bundleVersion = "0.2.0";
            PlayerSettings.Android.bundleVersionCode = 2;
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true),
                new EditorBuildSettingsScene("Assets/BrakeToy/Scenes/ToyRoom.unity", true) };
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            AssetDatabase.SaveAssets();
            Selection.activeObject = game;
            Debug.Log("Bedroom tabletop v0.2 generated: " + ScenePath);
        }

        [MenuItem("Tools/Brake Toy/Refine Tabletop Surfaces")]
        public static void Refine()
        {
            EditorSceneManager.OpenScene(ScenePath);
            palette.Clear(); MakePalette(true);
            foreach (string file in Directory.GetFiles(Root + "Meshes", "box_*.asset"))
            {
                string[] parts = Path.GetFileNameWithoutExtension(file).Substring(4).Split('_');
                if (parts.Length != 4) continue;
                float x = float.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
                float y = float.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                float z = float.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture);
                float radius = float.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture);
                RoundedBox(new Vector3(x, y, z), radius, true);
            }
            if (RenderSettings.sun) RenderSettings.sun.intensity = 1.35f;
            RenderSettings.ambientEquatorColor = new Color(.40f, .43f, .46f);
            GameObject.Find("Toy shelf").transform.position = new Vector3(-9.8f, -7, 32);
            GameObject.Find("Blue star bed").transform.position = new Vector3(10.5f, -7, 31);
            GameObject.Find("Pencil cup").transform.localPosition = new Vector3(5, 0, 10);
            GameObject.Find("Spiral notebook").transform.localPosition = new Vector3(-4.8f, .12f, 13);
            GameObject.Find("Chair back").transform.localPosition = new Vector3(-8, -1, 17);
            GameObject.Find("Chair seat").transform.localPosition = new Vector3(-8, -4, 18);
            if (!GameObject.Find("Woven play rug")) Box("Woven play rug", GameObject.Find("Bedroom Set").transform, new Vector3(-10, -7.01f, 25), new Vector3(7, .05f, 12), "Teal", .02f);
            foreach (Transform item in GameObject.Find("Bedroom Set").transform)
                if (item.name == "Chair leg") item.localPosition = new Vector3(item.localPosition.x < -9 ? -9.5f : -6.5f, -5.6f, 18);
            foreach (Transform item in GameObject.Find("Table structure").transform)
                item.localPosition = new Vector3(Mathf.Sign(item.localPosition.x) * (item.name == "Table leg" ? 5.7f : 5.85f), item.localPosition.y, item.localPosition.z);
            var tabletop = PrefabUtility.LoadPrefabContents(Prefabs + "TableSurface.prefab");
            tabletop.transform.Find("Table surface").GetComponent<MeshFilter>().sharedMesh = RoundedBox(new Vector3(12, .32f, 18), .015f);
            foreach (Transform item in tabletop.transform) if (item.name == "Table rim") item.localPosition = new Vector3(Mathf.Sign(item.localPosition.x) * 5.94f, -.26f, 0);
            PrefabUtility.SaveAsPrefabAsset(tabletop, Prefabs + "TableSurface.prefab");
            PrefabUtility.UnloadPrefabContents(tabletop);
            ConnectDecor(GameObject.Find("Table decorations — no collision").transform);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
            AssetDatabase.SaveAssets();
            Debug.Log("Tabletop bevel normals and subtle wood grain refined.");
        }

        private static void MakePalette(bool refreshTexture = false)
        {
            Mat("HoneyWood", new Color(.60f, .37f, .18f), .22f);
            Mat("DarkWood", new Color(.32f, .18f, .09f), .18f);
            Mat("FloorWood", new Color(.56f, .55f, .47f), .10f);
            Mat("Cream", new Color(.92f, .86f, .69f), .25f);
            Mat("Paper", new Color(.93f, .90f, .78f), .08f);
            Mat("Red", new Color(.78f, .045f, .065f), .48f);
            Mat("Coral", new Color(.87f, .24f, .24f), .30f);
            Mat("Rubber", new Color(.025f, .037f, .05f), .10f);
            Mat("Glass", new Color(.045f, .18f, .25f), .75f);
            Mat("Teal", new Color(.055f, .49f, .46f), .35f);
            Mat("Blue", new Color(.075f, .27f, .58f), .25f);
            Mat("Lavender", new Color(.41f, .21f, .61f), .22f);
            Mat("Yellow", new Color(.97f, .65f, .10f), .22f);
            Mat("Pink", new Color(.91f, .31f, .44f), .12f);
            Mat("Wall", new Color(.42f, .54f, .64f), .02f);
            Mat("WallSide", new Color(.60f, .65f, .65f), .02f);
            Mat("Blanket", new Color(.10f, .24f, .47f), .05f);
            Mat("Teddy", new Color(.56f, .33f, .16f), .05f);
            Mat("Sky", new Color(.55f, .76f, .91f), .05f);
            Material wood = palette["HoneyWood"];
            palette["FloorWood"].SetColor("_BaseColor", new Color(.56f, .55f, .47f));
            EditorUtility.SetDirty(palette["FloorWood"]);
            string texturePath = Root + "Textures/wood-grain.png";
            if (!File.Exists(texturePath) || refreshTexture)
            {
                var texture = new Texture2D(256, 256, TextureFormat.RGB24, false);
                var random = new System.Random(819);
                var colors = new Color[256 * 256];
                for (int y = 0; y < 256; y++) for (int x = 0; x < 256; x++)
                {
                    float grain = .97f + .022f * (Mathf.PerlinNoise(x * .37f, y * .016f) - .5f)
                        + .008f * Mathf.Sin(x * 1.71f + Mathf.Sin(y * .021f) * 1.4f) + (float)random.NextDouble() * .004f;
                    colors[y * 256 + x] = new Color(grain, grain, grain);
                }
                texture.SetPixels(colors); texture.Apply();
                File.WriteAllBytes(texturePath, texture.EncodeToPNG());
                Object.DestroyImmediate(texture);
                AssetDatabase.ImportAsset(texturePath);
                var importer = (TextureImporter)AssetImporter.GetAtPath(texturePath);
                importer.wrapMode = TextureWrapMode.Repeat; importer.mipmapEnabled = true;
                importer.maxTextureSize = 256; importer.SaveAndReimport();
            }
            wood.SetTexture("_BaseMap", AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath));
            wood.SetTextureScale("_BaseMap", new Vector2(3, 2));
            EditorUtility.SetDirty(wood);
        }

        private static Material Mat(string name, Color color, float smoothness)
        {
            string path = Root + "Materials/" + name + ".mat";
            var material = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (!material)
            {
                material = new Material(Shader.Find("Universal Render Pipeline/Lit")) { name = name };
                material.SetColor("_BaseColor", color);
                material.SetFloat("_Smoothness", smoothness);
                AssetDatabase.CreateAsset(material, path);
            }
            palette[name] = material;
            return material;
        }

        private static Transform Group(string name, Transform parent = null, Vector3 position = default)
        {
            var obj = new GameObject(name);
            obj.transform.SetParent(parent, false); obj.transform.localPosition = position;
            return obj.transform;
        }

        private static GameObject Box(string name, Transform parent, Vector3 position, Vector3 size, string color, float radius = .06f)
        {
            var obj = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer));
            obj.transform.SetParent(parent, false); obj.transform.localPosition = position;
            obj.GetComponent<MeshFilter>().sharedMesh = RoundedBox(size, Mathf.Min(radius, Mathf.Min(size.x, Mathf.Min(size.y, size.z)) * .45f));
            obj.GetComponent<Renderer>().sharedMaterial = palette[color];
            return obj;
        }

        private static Mesh RoundedBox(Vector3 size, float radius, bool rebuild = false)
        {
            string key = (size.x + "_" + size.y + "_" + size.z + "_" + radius).Replace(',', '.');
            string path = Root + "Meshes/box_" + key + ".asset";
            Mesh saved = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (saved && !rebuild) return saved;
            const int subdivisions = 4;
            var vertices = new List<Vector3>(); var normals = new List<Vector3>();
            var uv = new List<Vector2>(); var triangles = new List<int>();
            Vector3 half = size * .5f, inner = half - Vector3.one * radius;
            Vector3[] faceNormals = { Vector3.right, Vector3.left, Vector3.up, Vector3.down, Vector3.forward, Vector3.back };
            foreach (var normal in faceNormals)
            {
                Vector3 u = Mathf.Abs(normal.y) > .5f ? Vector3.right : Vector3.up;
                Vector3 v = Vector3.Cross(normal, u);
                int first = vertices.Count;
                for (int y = 0; y <= subdivisions; y++) for (int x = 0; x <= subdivisions; x++)
                {
                    // Put the bevel boundary in the mesh explicitly. Uniform grids would
                    // interpolate sloped corner normals across metres of a large flat table.
                    float hu = Vector3.Scale(u, half).magnitude, hv = Vector3.Scale(v, half).magnitude;
                    float[] cu = { -hu, -hu + radius, 0, hu - radius, hu };
                    float[] cv = { -hv, -hv + radius, 0, hv - radius, hv };
                    Vector3 point = Vector3.Scale(normal, half) + u * cu[x] + v * cv[y];
                    Vector3 core = new Vector3(Mathf.Clamp(point.x, -inner.x, inner.x), Mathf.Clamp(point.y, -inner.y, inner.y), Mathf.Clamp(point.z, -inner.z, inner.z));
                    Vector3 n = (point - core).normalized;
                    vertices.Add(core + n * radius); normals.Add(n); uv.Add(new Vector2(x / (float)subdivisions, y / (float)subdivisions));
                }
                for (int y = 0; y < subdivisions; y++) for (int x = 0; x < subdivisions; x++)
                {
                    int a = first + y * (subdivisions + 1) + x, b = a + 1, c = a + subdivisions + 1, d = c + 1;
                    triangles.Add(a); triangles.Add(b); triangles.Add(d); triangles.Add(a); triangles.Add(d); triangles.Add(c);
                }
            }
            var mesh = saved ? saved : new Mesh { name = "Rounded toy box" };
            mesh.Clear();
            mesh.SetVertices(vertices); mesh.SetNormals(normals); mesh.SetUVs(0, uv); mesh.SetTriangles(triangles, 0); mesh.RecalculateBounds();
            if (!saved) AssetDatabase.CreateAsset(mesh, path); else EditorUtility.SetDirty(mesh);
            return mesh;
        }

        private static GameObject Shape(string name, Transform parent, Vector3 position, Vector3 size, string color, PrimitiveType type)
        {
            var obj = GameObject.CreatePrimitive(type); obj.name = name;
            Object.DestroyImmediate(obj.GetComponent<Collider>());
            obj.transform.SetParent(parent, false); obj.transform.localPosition = position; obj.transform.localScale = size;
            obj.GetComponent<Renderer>().sharedMaterial = palette[color];
            return obj;
        }

        private static GameObject Save(GameObject instance, string name)
        {
            string path = Prefabs + name + ".prefab";
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var asset = existing ? existing : PrefabUtility.SaveAsPrefabAsset(instance, path);
            Object.DestroyImmediate(instance);
            return asset;
        }

        private static void ReplaceCar(GameManager game)
        {
            var car = Group("Sparky — rounded toy racer");
            var controller = car.gameObject.AddComponent<PlayerController>();
            var visual = Group("Visual", car); controller.Visual = visual;
            Box("Chassis", visual, new Vector3(0, .18f, 0), new Vector3(.64f, .15f, 1.24f), "Rubber");
            Box("Rounded red body", visual, new Vector3(0, .32f, 0), new Vector3(.66f, .27f, 1.29f), "Red", .12f);
            Box("Blue canopy", visual, new Vector3(0, .51f, -.04f), new Vector3(.48f, .25f, .53f), "Glass", .10f);
            Box("Roof", visual, new Vector3(0, .64f, -.09f), new Vector3(.41f, .06f, .31f), "Red");
            foreach (float x in new[] { -.075f, .075f })
            {
                Box("Hood racing stripe", visual, new Vector3(x, .462f, .43f), new Vector3(.057f, .012f, .32f), "Cream", .003f);
                Box("Roof racing stripe", visual, new Vector3(x, .676f, -.09f), new Vector3(.057f, .012f, .24f), "Cream", .003f);
                Box("Spoiler mount", visual, new Vector3(x * 2.7f, .45f, -.55f), new Vector3(.04f, .16f, .05f), "Rubber", .01f);
            }
            Box("Rear spoiler", visual, new Vector3(0, .55f, -.56f), new Vector3(.73f, .07f, .18f), "Red", .025f);
            Box("Front bumper", visual, new Vector3(0, .23f, .65f), new Vector3(.57f, .08f, .06f), "Cream", .02f);
            var wheels = new List<Transform>();
            foreach (float x in new[] { -.32f, .32f }) foreach (float z in new[] { -.43f, .43f })
            {
                var wheel = Shape("Rubber wheel", visual, new Vector3(x, .18f, z), new Vector3(.34f, .068f, .34f), "Rubber", PrimitiveType.Cylinder);
                wheel.transform.localRotation = Quaternion.Euler(0, 0, 90); wheels.Add(wheel.transform);
                var hub = Shape("Cream hub", wheel.transform, new Vector3(0, Mathf.Sign(x) * .97f, 0), new Vector3(.5f, .09f, .5f), "Cream", PrimitiveType.Cylinder);
                hub.transform.localRotation = Quaternion.identity;
            }
            foreach (float x in new[] { -.22f, .22f })
            {
                Box("Headlight", visual, new Vector3(x, .34f, .635f), new Vector3(.14f, .10f, .04f), "Cream", .025f);
                Box("Tail light", visual, new Vector3(x, .30f, -.641f), new Vector3(.13f, .09f, .03f), "Yellow", .02f);
            }
            var collider = car.gameObject.AddComponent<BoxCollider>();
            collider.center = new Vector3(0, .35f, 0); collider.size = new Vector3(.78f, .7f, 1.4f); collider.isTrigger = true;
            var animation = car.gameObject.AddComponent<ToyWheelAnimation>(); animation.Wheels = wheels.ToArray();
            var prefab = Save(car.gameObject, "SparkyTabletop");
            Object.DestroyImmediate(game.Player.gameObject);
            var instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            game.Player = instance.GetComponent<PlayerController>();
            instance.GetComponent<ToyWheelAnimation>().Game = game;
            PrefabUtility.RecordPrefabInstancePropertyModifications(instance.GetComponent<ToyWheelAnimation>());
        }

        private static void ReplaceWorld(WorldScroller world)
        {
            var floor = Group("Honey wood tabletop chunk");
            Box("Table surface", floor, new Vector3(0, -.18f, 0), new Vector3(12, .32f, 18), "HoneyWood", .015f);
            foreach (float x in new[] { -5.94f, 5.94f }) Box("Table rim", floor, new Vector3(x, -.26f, 0), new Vector3(.12f, .42f, 18), "DarkWood", .01f);
            var floorPrefab = Save(floor.gameObject, "TableSurface");
            for (int i = 0; i < world.FloorChunks.Length; i++)
            {
                Vector3 position = world.FloorChunks[i].position;
                Object.DestroyImmediate(world.FloorChunks[i].gameObject);
                var piece = (GameObject)PrefabUtility.InstantiatePrefab(floorPrefab, world.transform);
                piece.transform.position = position; world.FloorChunks[i] = piece.transform;
            }
            var variants = new[] { MakeObstacle("Book", new Vector3(1.9f, .7f, 1.25f)),
                MakeObstacle("Blocks", new Vector3(1.35f, 1.55f, 1.3f)), MakeObstacle("Eraser", new Vector3(1.6f, .55f, 1)) };
            for (int i = 0; i < world.Obstacles.Length; i++)
            {
                Vector3 position = world.Obstacles[i].transform.position;
                Object.DestroyImmediate(world.Obstacles[i].gameObject);
                var obstacle = (GameObject)PrefabUtility.InstantiatePrefab(variants[i % variants.Length], world.transform);
                obstacle.transform.position = position; world.Obstacles[i] = obstacle.GetComponent<ObstacleView>();
            }
        }

        private static GameObject MakeObstacle(string name, Vector3 size)
        {
            var root = Group(name + " — toy");
            if (name == "Book")
            {
                Box("Cream page block", root, new Vector3(0, .35f, 0), size - new Vector3(.10f, .10f, .06f), "Paper", .03f);
                foreach (float y in new[] { .04f, .66f }) Box("Purple cover", root, new Vector3(0, y, 0), new Vector3(size.x, .08f, size.z), "Lavender", .035f);
                Box("Spine", root, new Vector3(-.90f, .35f, 0), new Vector3(.10f, .7f, 1.25f), "Lavender", .035f);
                for (int i = 0; i < 4; i++) Box("Page edges", root, new Vector3(0, .18f + i * .11f, -.60f), new Vector3(1.66f, .012f, .018f), "Cream", .002f);
                Box("Cover frame", root, new Vector3(.1f, .71f, 0), new Vector3(.95f, .016f, .66f), "Yellow", .007f);
                Box("Cover inset", root, new Vector3(.1f, .722f, 0), new Vector3(.81f, .014f, .53f), "Teal", .006f);
                Star("Cover star", root, new Vector3(.1f, .735f, 0), .22f, "Cream");
            }
            else if (name == "Blocks")
            {
                for (int i = 0; i < 3; i++) Box("Rounded block", root, new Vector3(0, .23f + i * .48f, 0), new Vector3(1.35f, .46f, 1.3f), i % 2 == 0 ? "Teal" : "Yellow", .08f);
                foreach (float x in new[] { -.34f, .34f }) foreach (float z in new[] { -.32f, .32f })
                    Shape("Stud", root, new Vector3(x, 1.49f, z), new Vector3(.31f, .06f, .31f), "Teal", PrimitiveType.Cylinder);
            }
            else
            {
                Box("Pink rubber", root, new Vector3(0, .275f, 0), size, "Pink", .10f);
                Box("Cream paper sleeve", root, new Vector3(.15f, .28f, 0), new Vector3(.61f, .57f, 1.01f), "Paper", .06f);
                Box("Sleeve stripe", root, new Vector3(.15f, .575f, 0), new Vector3(.22f, .012f, .78f), "Teal", .005f);
            }
            var obstacle = root.gameObject.AddComponent<ObstacleView>();
            obstacle.Hitbox = root.gameObject.AddComponent<BoxCollider>();
            obstacle.Hitbox.center = Vector3.up * size.y * .5f; obstacle.Hitbox.size = size; obstacle.Hitbox.isTrigger = true;
            return Save(root.gameObject, "Obstacle" + name);
        }

        private static void Star(string name, Transform parent, Vector3 position, float radius, string color)
        {
            string path = Root + "Meshes/star.asset";
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(path);
            if (!mesh)
            {
                var verts = new Vector3[11]; var tris = new int[30];
                for (int i = 0; i < 10; i++)
                {
                    float a = i * Mathf.PI / 5, r = i % 2 == 0 ? 1 : .45f;
                    verts[i + 1] = new Vector3(Mathf.Sin(a) * r, 0, Mathf.Cos(a) * r);
                    tris[i * 3] = 0; tris[i * 3 + 1] = i + 1; tris[i * 3 + 2] = (i + 1) % 10 + 1;
                }
                mesh = new Mesh { name = "Toy star" }; mesh.vertices = verts; mesh.triangles = tris; mesh.RecalculateNormals(); mesh.RecalculateBounds(); AssetDatabase.CreateAsset(mesh, path);
            }
            var obj = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer));
            obj.transform.SetParent(parent, false); obj.transform.localPosition = position; obj.transform.localScale = Vector3.one * radius;
            obj.GetComponent<MeshFilter>().sharedMesh = mesh; obj.GetComponent<Renderer>().sharedMaterial = palette[color];
        }

        private static void BuildBedroom()
        {
            var room = Group("Bedroom Set");
            Box("Room floor", room, new Vector3(0, -7.3f, 19), new Vector3(42, .5f, 86), "FloorWood", .03f);
            for (int i = -10; i <= 10; i++) Box("Floorboard seam", room, new Vector3(i * 2, -7.045f, 19), new Vector3(.025f, .01f, 86), "DarkWood", .002f);
            Box("Back wall", room, new Vector3(0, 6.5f, 43), new Vector3(42, 27, .5f), "Wall", .04f);
            Box("Left wall", room, new Vector3(-20, 6.5f, 19), new Vector3(.5f, 27, 48), "WallSide", .04f);
            Box("Right wall", room, new Vector3(20, 6.5f, 19), new Vector3(.5f, 27, 48), "WallSide", .04f);
            Box("Back skirting", room, new Vector3(0, -6.35f, 42.65f), new Vector3(40, 1.1f, .22f), "Cream");
            Box("Woven play rug", room, new Vector3(-10, -7.01f, 25), new Vector3(7, .05f, 12), "Teal", .02f);
            BuildWindow(room);
            BuildShelf(room);
            BuildBed(room);
            BuildTableFurniture(room);
            BuildPoster(room);
            BuildTableDecor(room);
        }

        private static void BuildWindow(Transform parent)
        {
            var window = Group("Window and curtains", parent, new Vector3(-9.2f, 9.5f, 42.5f));
            Box("Daylight sky", window, Vector3.zero, new Vector3(10.6f, 11.4f, .15f), "Sky");
            Box("Window sill", window, new Vector3(0, -5.7f, -.65f), new Vector3(12, .42f, 1.8f), "Cream");
            foreach (float x in new[] { -5.35f, 0, 5.35f }) Box("Window frame upright", window, new Vector3(x, 0, -.18f), new Vector3(.24f, 11.6f, .35f), "Cream");
            foreach (float y in new[] { -5.7f, 0, 5.7f }) Box("Window frame rail", window, new Vector3(0, y, -.18f), new Vector3(10.8f, .24f, .35f), "Cream");
            Shape("Sun disk", window, new Vector3(2.6f, 3, -.12f), new Vector3(2.3f, 2.3f, .06f), "Yellow", PrimitiveType.Sphere);
            foreach (float x in new[] { -6.2f, 6.2f }) for (int i = 0; i < 4; i++)
                Shape("Curtain fold", window, new Vector3(x + (i - 1.5f) * .42f, -.3f, -.6f), new Vector3(.65f, 6.2f, .60f), "Yellow", PrimitiveType.Cylinder);
            Box("Curtain rail", window, new Vector3(0, 6.4f, -.4f), new Vector3(15, .22f, .3f), "DarkWood");
        }

        private static void BuildShelf(Transform parent)
        {
            var shelf = Group("Toy shelf", parent, new Vector3(-9.8f, -7, 32));
            Box("Shelf back", shelf, new Vector3(0, 5, 1.2f), new Vector3(8, 10, .28f), "Cream");
            foreach (float x in new[] { -4f, 4f }) Box("Shelf side", shelf, new Vector3(x, 5, 0), new Vector3(.34f, 10, 2.6f), "HoneyWood");
            foreach (float y in new[] { .3f, 3.5f, 6.7f, 10f }) Box("Shelf board", shelf, new Vector3(0, y, 0), new Vector3(8.3f, .3f, 2.6f), "HoneyWood");
            string[] colors = { "Teal", "Yellow", "Coral", "Lavender", "Blue", "Cream" };
            for (int i = 0; i < 6; i++)
            {
                float height = 1.8f + (i % 3) * .35f;
                Box("Shelf book", shelf, new Vector3(-3.1f + i * .68f, 6.85f + height / 2, -.2f), new Vector3(.55f, height, 1.55f), colors[i]);
                Box("Book spine label", shelf, new Vector3(-3.1f + i * .68f, 7.45f, -1), new Vector3(.31f, .12f, .03f), "Paper", .01f);
            }
            Box("Blue toy bin", shelf, new Vector3(-2, 1.75f, -.1f), new Vector3(3.2f, 2.5f, 2), "Blue", .16f);
            Box("Red toy bin", shelf, new Vector3(1.9f, 1.75f, -.1f), new Vector3(3.2f, 2.5f, 2), "Coral", .16f);
            foreach (float x in new[] { -2f, 1.9f }) Box("Bin handle", shelf, new Vector3(x, 2.5f, -1.12f), new Vector3(.9f, .18f, .04f), "Rubber");
            var teddy = Group("Teddy bear", shelf, new Vector3(1.9f, 4.8f, -.3f));
            Shape("Body", teddy, Vector3.zero, new Vector3(1.5f, 1.7f, 1.2f), "Teddy", PrimitiveType.Sphere);
            Shape("Head", teddy, new Vector3(0, 1.1f, 0), Vector3.one * 1.35f, "Teddy", PrimitiveType.Sphere);
            foreach (float x in new[] { -.6f, .6f })
            {
                Shape("Ear", teddy, new Vector3(x, 1.6f, 0), Vector3.one * .48f, "Teddy", PrimitiveType.Sphere);
                Shape("Foot", teddy, new Vector3(x, -.55f, -.3f), Vector3.one * .65f, "Teddy", PrimitiveType.Sphere);
                Shape("Eye", teddy, new Vector3(x * .45f, 1.22f, -.59f), Vector3.one * .12f, "Rubber", PrimitiveType.Sphere);
            }
            Shape("Muzzle", teddy, new Vector3(0, .9f, -.55f), new Vector3(.65f, .45f, .28f), "Cream", PrimitiveType.Sphere);
            Shape("Nose", teddy, new Vector3(0, 1, -.71f), Vector3.one * .15f, "Rubber", PrimitiveType.Sphere);
        }

        private static void BuildBed(Transform parent)
        {
            var bed = Group("Blue star bed", parent, new Vector3(10.5f, -7, 31));
            Box("Bed base", bed, new Vector3(0, 2.4f, 0), new Vector3(8, 1, 14), "HoneyWood", .2f);
            Box("Mattress", bed, new Vector3(0, 3.3f, 0), new Vector3(7.7f, 1, 13.5f), "Paper", .3f);
            Box("Blue blanket", bed, new Vector3(0, 3.85f, -1.6f), new Vector3(7.8f, .6f, 10.4f), "Blanket", .25f);
            Box("Headboard", bed, new Vector3(0, 5, 6.6f), new Vector3(8.4f, 7, .6f), "HoneyWood", .3f);
            foreach (float x in new[] { -3.7f, 3.7f })
            {
                Box("Bed post", bed, new Vector3(x, 3, 6.7f), new Vector3(.55f, 8, .55f), "DarkWood", .15f);
                Shape("Post cap", bed, new Vector3(x, 7.2f, 6.7f), Vector3.one * .9f, "HoneyWood", PrimitiveType.Sphere);
            }
            Box("Soft pillow", bed, new Vector3(0, 4.2f, 4.3f), new Vector3(5.7f, .8f, 2.7f), "Cream", .35f);
            for (int i = 0; i < 12; i++) Star("Blanket star", bed, new Vector3(-2.8f + i % 4 * 1.8f, 4.16f, -5.8f + i / 4 * 3), .35f, i % 3 == 0 ? "Yellow" : "Cream");
            Box("Room rug", parent, new Vector3(11.3f, -7.01f, 15), new Vector3(10, .06f, 11), "Blanket", .02f);
            Box("Bedside cabinet", parent, new Vector3(6.9f, -4.6f, 37), new Vector3(3.6f, 4.8f, 3.2f), "HoneyWood", .15f);
            Shape("Lamp stem", parent, new Vector3(6.9f, -1, 37), new Vector3(.23f, 1.5f, .23f), "DarkWood", PrimitiveType.Cylinder);
            Shape("Lamp shade", parent, new Vector3(6.9f, .9f, 37), new Vector3(2.6f, 1, 2.6f), "Yellow", PrimitiveType.Cylinder);
        }

        private static void BuildTableFurniture(Transform parent)
        {
            var table = Group("Table structure", parent);
            foreach (float x in new[] { -5.85f, 5.85f })
            {
                Box("Side apron", table, new Vector3(x, -.8f, 10), new Vector3(.3f, 1.3f, 60), "DarkWood");
                foreach (float z in new[] { -9f, 27f }) Box("Table leg", table, new Vector3(Mathf.Sign(x) * 5.7f, -3.65f, z), new Vector3(.9f, 6.8f, .9f), "HoneyWood", .15f);
            }
            Box("Chair back", parent, new Vector3(-8, -1, 17), new Vector3(3.6f, 8, .7f), "HoneyWood", .15f);
            Box("Chair seat", parent, new Vector3(-8, -4, 18), new Vector3(4, .55f, 3.7f), "HoneyWood", .15f);
            foreach (float x in new[] { -9.5f, -6.5f }) Box("Chair leg", parent, new Vector3(x, -5.6f, 18), new Vector3(.45f, 2.8f, .45f), "DarkWood");
        }

        private static void BuildPoster(Transform parent)
        {
            var poster = Group("Space poster", parent, new Vector3(3.8f, 11, 42.6f));
            Box("Poster frame", poster, Vector3.zero, new Vector3(6.4f, 9, .20f), "Cream");
            Box("Poster blue", poster, new Vector3(0, 0, -.12f), new Vector3(5.9f, 8.5f, .04f), "Blanket", .01f);
            var rocket = Box("Rocket body", poster, new Vector3(.1f, .2f, -.22f), new Vector3(1.25f, 4.1f, .17f), "Coral", .2f);
            rocket.transform.localRotation = Quaternion.Euler(0, 0, -22);
            Shape("Rocket window", poster, new Vector3(.3f, 1.1f, -.36f), new Vector3(.65f, .65f, .07f), "Sky", PrimitiveType.Sphere);
            for (int i = 0; i < 7; i++)
            {
                var star = Group("Poster star", poster, new Vector3(-2 + i % 3 * 1.8f, -3.2f + i / 3 * 2.7f, -.18f));
                star.localRotation = Quaternion.Euler(-90, 0, 0);
                Star("Star", star, Vector3.zero, .25f, "Yellow");
            }
        }

        private static void BuildTableDecor(Transform parent)
        {
            var decor = Group("Table decorations — no collision", parent);
            var cup = Group("Pencil cup", decor, new Vector3(5, 0, 10));
            Shape("Cup body", cup, new Vector3(0, .8f, 0), new Vector3(1.35f, .8f, 1.35f), "Cream", PrimitiveType.Cylinder);
            Shape("Cup opening", cup, new Vector3(0, 1.61f, 0), new Vector3(1.13f, .015f, 1.13f), "DarkWood", PrimitiveType.Cylinder);
            string[] colors = { "Red", "Blue", "Yellow", "Teal", "Lavender" };
            for (int i = 0; i < 5; i++)
            {
                var pencil = Shape("Colored pencil", cup, new Vector3((i - 2) * .21f, 2.2f, (i % 2) * .24f - .12f), new Vector3(.16f, 1.3f, .16f), colors[i], PrimitiveType.Cylinder);
                pencil.transform.localRotation = Quaternion.Euler((i - 2) * 4, 0, (i - 2) * 5);
                Shape("Pencil wood tip", cup, new Vector3((i - 2) * .3f, 3.5f, (i % 2) * .24f - .12f), new Vector3(.17f, .30f, .17f), "HoneyWood", PrimitiveType.Sphere);
            }
            var notebook = Group("Spiral notebook", decor, new Vector3(-4.8f, .12f, 13));
            notebook.localRotation = Quaternion.Euler(0, -12, 0);
            Box("Notebook cover", notebook, Vector3.zero, new Vector3(2.3f, .22f, 3.4f), "Blue");
            Box("Notebook pages", notebook, new Vector3(0, .15f, 0), new Vector3(2.15f, .10f, 3.23f), "Paper", .02f);
            for (int i = 0; i < 9; i++)
            {
                Box("Ruled line", notebook, new Vector3(.1f, .206f, -1.3f + i * .31f), new Vector3(1.7f, .005f, .015f), "Sky", .001f);
                Shape("Spiral binding", notebook, new Vector3(-1.10f, .25f, -1.3f + i * .31f), new Vector3(.18f, .24f, .10f), "Rubber", PrimitiveType.Sphere);
            }
            foreach (float x in new[] { -4.7f, 4.7f }) for (int i = 0; i < 3; i++) Star("Table sticker", decor, new Vector3(x, .005f, 4 + i * 9), .21f, "Yellow");
            ConnectDecor(decor);
        }

        private static void ConnectDecor(Transform decor)
        {
            var scroller = decor.GetComponent<TabletopDecorScroller>();
            if (!scroller) scroller = decor.gameObject.AddComponent<TabletopDecorScroller>();
            scroller.World = Object.FindFirstObjectByType<WorldScroller>();
            scroller.Targets = new Transform[decor.childCount];
            for (int i = 0; i < decor.childCount; i++) scroller.Targets[i] = decor.GetChild(i);
            EditorUtility.SetDirty(scroller);
        }

        private static void ImproveUI(PrototypeUI ui)
        {
            ui.Overlay.GetComponent<Image>().color = new Color(.035f, .055f, .09f, .32f);
            var panelObject = new GameObject("Menu card", typeof(RectTransform), typeof(Image));
            panelObject.transform.SetParent(ui.Overlay.transform, false); panelObject.transform.SetAsFirstSibling();
            var rect = (RectTransform)panelObject.transform; rect.anchorMin = new Vector2(.07f, .25f); rect.anchorMax = new Vector2(.93f, .74f); rect.offsetMin = rect.offsetMax = Vector2.zero;
            panelObject.GetComponent<Image>().color = new Color(.045f, .095f, .15f, .96f);
            SetRect(ui.Heading.rectTransform, new Vector2(.08f, .60f), new Vector2(.92f, .70f)); ui.Heading.fontSize = 76;
            SetRect(ui.Description.rectTransform, new Vector2(.12f, .41f), new Vector2(.88f, .59f)); ui.Description.fontSize = 35;
            SetRect(ui.ActionButton.GetComponent<RectTransform>(), new Vector2(.17f, .29f), new Vector2(.83f, .38f));
            ui.PauseButton.GetComponent<Image>().color = new Color(.045f, .095f, .15f, .90f);
            ui.PauseButton.GetComponentInChildren<Text>().color = Color.white;
            foreach (var label in ui.GetComponentsInChildren<Text>(true))
            {
                if (label.name == "Prototype version") { label.text = "v0.2  /  BEDROOM TABLETOP"; SetRect(label.rectTransform, new Vector2(.08f, .18f), new Vector2(.92f, .24f)); }
                if (label.name == "Steering hint") { label.text = "TOY ROOM"; label.fontStyle = FontStyle.Bold; label.alignment = TextAnchor.MiddleLeft; }
            }
        }
        private static void SetRect(RectTransform rect, Vector2 min, Vector2 max)
        { rect.anchorMin = min; rect.anchorMax = max; rect.offsetMin = rect.offsetMax = Vector2.zero; }
    }
}
