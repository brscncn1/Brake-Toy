using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BrakeToy.Editor
{
    /// <summary>Creates actual editable assets once; never overwrites an existing prototype scene.</summary>
    public static class PrototypeBuilder
    {
        private const string Root = "Assets/BrakeToy/";
        private const string ScenePath = Root + "Scenes/ToyRoom.unity";

        [InitializeOnLoadMethod]
        private static void ScheduleFirstImport() => EditorApplication.delayCall += FirstImport;

        private static void FirstImport()
        {
            if (File.Exists(ScenePath) || EditorApplication.isPlayingOrWillChangePlaymode) return;
            if (SceneManager.GetActiveScene().isDirty)
            {
                Debug.Log("Brake Toy: mevcut sahne korunuyor. Kaydettikten sonra Tools > Brake Toy > Create Phase 1 Prototype seçin.");
                return;
            }
            Create();
        }

        [MenuItem("Tools/Brake Toy/Create Phase 1 Prototype")]
        public static void Create()
        {
            if (File.Exists(ScenePath))
            {
                Debug.Log("Brake Toy sahnesi zaten var; üzerine yazılmadı. Assets/BrakeToy/Scenes/ToyRoom.unity dosyasını açın.");
                return;
            }
            if (!Application.isBatchMode && !EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            foreach (string folder in new[] { "Scenes", "Art/Materials", "Art/Models", "Art/Textures", "Art/Animations", "Audio",
                "Prefabs/Cars", "Prefabs/Obstacles", "Prefabs/Environments", "Prefabs/UI", "ScriptableObjects", "Settings" })
                Directory.CreateDirectory(Root + folder);
            AssetDatabase.Refresh();

            var config = LoadOrCreate<GameConfig>(Root + "ScriptableObjects/Phase1Config.asset");
            SetupPipeline();
            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Material red = Material("Sparky_Red", new Color(.95f, .12f, .17f), .35f);
            Material cream = Material("Cream", new Color(1, .92f, .73f), .25f);
            Material rubber = Material("Rubber", new Color(.07f, .10f, .15f), .15f);
            Material glass = Material("Blue_Plastic", new Color(.13f, .55f, .7f), .65f);
            Material wood = Material("Wood", new Color(.65f, .40f, .22f), .1f);
            Material woodLight = Material("Wood_Light", new Color(.76f, .50f, .29f), .1f);
            Material teal = Material("Teal_Block", new Color(.1f, .68f, .62f), .3f);
            Material yellow = Material("Yellow_Block", new Color(1, .72f, .15f), .3f);
            Material purple = Material("Book_Purple", new Color(.44f, .25f, .70f), .25f);
            Material pink = Material("Eraser_Pink", new Color(.99f, .39f, .51f), .15f);

            GameObject carPrefab = BuildCar(red, cream, rubber, glass);
            GameObject book = BuildObstacle("Book", new Vector3(1.9f, .7f, 1.25f), purple, cream);
            GameObject blocks = BuildObstacle("BlockTower", new Vector3(1.35f, 1.55f, 1.3f), teal, yellow);
            GameObject eraser = BuildObstacle("Eraser", new Vector3(1.6f, .55f, 1.0f), pink, cream);
            GameObject floorPrefab = BuildFloor(wood, woodLight, cream);

            var systems = new GameObject("Brake Toy — Phase 1");
            var game = systems.AddComponent<GameManager>();
            game.Config = config;
            game.Input = systems.AddComponent<InputController>();
            var car = (GameObject)PrefabUtility.InstantiatePrefab(carPrefab);
            game.Player = car.GetComponent<PlayerController>();
            var worldObject = new GameObject("Toy Room — recycled world");
            var world = worldObject.AddComponent<WorldScroller>();
            game.World = world;
            world.FloorChunks = new Transform[7];
            for (int i = 0; i < world.FloorChunks.Length; i++)
            {
                var floor = (GameObject)PrefabUtility.InstantiatePrefab(floorPrefab, worldObject.transform);
                floor.name = "Floor " + i;
                floor.transform.position = Vector3.forward * (i * world.ChunkLength);
                world.FloorChunks[i] = floor.transform;
            }

            // Authored, repeating exercise: one object per row, no procedural fairness claims.
            float[] xs = { 0, -1.8f, 1.7f, -.7f, 2, -2, .8f, -1.4f };
            GameObject[] prefabs = { book, blocks, eraser };
            world.Obstacles = new ObstacleView[xs.Length];
            for (int i = 0; i < xs.Length; i++)
            {
                var obstacle = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[i % prefabs.Length], worldObject.transform);
                obstacle.transform.position = new Vector3(xs[i], 0, 22 + i * world.ObstacleSpacing);
                world.Obstacles[i] = obstacle.GetComponent<ObstacleView>();
            }
            BuildCamera(config);
            BuildLight();
            game.UI = BuildUI();
            ConfigurePlayer();
            EditorSceneManager.SaveScene(scene, ScenePath);
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
            AssetDatabase.SaveAssets();
            Selection.activeObject = game;
            Debug.Log("Brake Toy Faz 1 oluşturuldu. Game görünümünde 9:16 oranını seçip Play düğmesine basın.");
        }

        private static T LoadOrCreate<T>(string path) where T : ScriptableObject
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset) return asset;
            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            return asset;
        }

        private static void SetupPipeline()
        {
            string path = Root + "Settings/MobileURP.asset";
            var pipeline = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(path);
            if (!pipeline)
            {
                var renderer = LoadOrCreate<UniversalRendererData>(Root + "Settings/MobileRenderer.asset");
                pipeline = UniversalRenderPipelineAsset.Create(renderer);
                pipeline.msaaSampleCount = 2;
                pipeline.renderScale = 1;
                pipeline.shadowDistance = 35;
                pipeline.mainLightShadowmapResolution = 1024;
                pipeline.shadowCascadeCount = 1;
                pipeline.supportsCameraDepthTexture = false;
                pipeline.supportsCameraOpaqueTexture = false;
                AssetDatabase.CreateAsset(pipeline, path);
            }
            GraphicsSettings.defaultRenderPipeline = pipeline;
            QualitySettings.renderPipeline = pipeline;
        }

        private static Material Material(string name, Color color, float smoothness)
        {
            string path = Root + "Art/Materials/" + name + ".mat";
            var existing = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (existing) return existing;
            var shader = Shader.Find("Universal Render Pipeline/Lit");
            if (!shader) throw new System.InvalidOperationException("URP Lit shader bulunamadı; paket yüklemesini kontrol edin.");
            var material = new Material(shader) { name = name };
            material.SetColor("_BaseColor", color);
            material.SetFloat("_Smoothness", smoothness);
            AssetDatabase.CreateAsset(material, path);
            return material;
        }

        private static GameObject Part(string name, Transform parent, Vector3 position, Vector3 size, Material material,
            PrimitiveType type = PrimitiveType.Cube)
        {
            var obj = GameObject.CreatePrimitive(type);
            obj.name = name;
            obj.transform.SetParent(parent, false);
            obj.transform.localPosition = position;
            obj.transform.localScale = size;
            obj.GetComponent<Renderer>().sharedMaterial = material;
            Object.DestroyImmediate(obj.GetComponent<Collider>());
            return obj;
        }

        private static GameObject SavePrefab(GameObject instance, string path)
        {
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var prefab = existing ? existing : PrefabUtility.SaveAsPrefabAsset(instance, path);
            Object.DestroyImmediate(instance);
            return prefab;
        }

        private static GameObject BuildCar(Material red, Material cream, Material rubber, Material glass)
        {
            var car = new GameObject("Sparky");
            var controller = car.AddComponent<PlayerController>();
            var visual = new GameObject("Visual");
            visual.transform.SetParent(car.transform, false);
            controller.Visual = visual.transform;
            Part("Body", visual.transform, new Vector3(0, .29f, 0), new Vector3(.65f, .25f, 1.3f), red);
            Part("Cabin", visual.transform, new Vector3(0, .5f, -.08f), new Vector3(.48f, .22f, .52f), glass);
            Part("Roof", visual.transform, new Vector3(0, .63f, -.12f), new Vector3(.5f, .06f, .4f), red);
            Part("Hood stripe", visual.transform, new Vector3(0, .42f, .43f), new Vector3(.14f, .015f, .35f), cream);
            Part("Front bumper", visual.transform, new Vector3(0, .22f, .66f), new Vector3(.6f, .09f, .08f), cream);
            foreach (float x in new[] { -.32f, .32f })
            foreach (float z in new[] { -.43f, .43f })
            {
                var wheel = Part("Wheel", visual.transform, new Vector3(x, .19f, z), new Vector3(.34f, .07f, .34f), rubber, PrimitiveType.Cylinder);
                wheel.transform.localRotation = Quaternion.Euler(0, 0, 90);
                var hub = Part("Hubcap", visual.transform, new Vector3(x * 1.18f, .19f, z), new Vector3(.18f, .015f, .18f), cream, PrimitiveType.Cylinder);
                hub.transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            foreach (float x in new[] { -.21f, .21f })
                Part("Headlight", visual.transform, new Vector3(x, .32f, .657f), new Vector3(.14f, .10f, .025f), cream);
            var collider = car.AddComponent<BoxCollider>();
            collider.center = new Vector3(0, .35f, 0);
            collider.size = new Vector3(.78f, .7f, 1.4f);
            collider.isTrigger = true;
            return SavePrefab(car, Root + "Prefabs/Cars/Sparky.prefab");
        }

        private static GameObject BuildObstacle(string name, Vector3 size, Material body, Material detail)
        {
            var obj = new GameObject(name);
            if (name == "Book")
            {
                Part("Pages", obj.transform, new Vector3(0, size.y / 2, 0), size - new Vector3(.08f, .08f, .06f), detail);
                foreach (float y in new[] { .035f, size.y - .035f })
                    Part("Cover", obj.transform, new Vector3(0, y, 0), new Vector3(size.x, .07f, size.z), body);
                Part("Spine", obj.transform, new Vector3(-size.x / 2 + .03f, size.y / 2, 0), new Vector3(.06f, size.y, size.z), body);
            }
            else if (name == "BlockTower")
            {
                for (int i = 0; i < 3; i++)
                    Part("Block " + i, obj.transform, new Vector3(0, .24f + i * .48f, 0), new Vector3(size.x, .46f, size.z), i % 2 == 0 ? body : detail);
                foreach (float x in new[] { -.34f, .34f })
                foreach (float z in new[] { -.32f, .32f })
                    Part("Stud", obj.transform, new Vector3(x, 1.49f, z), new Vector3(.3f, .06f, .3f), body, PrimitiveType.Cylinder);
            }
            else
            {
                Part("Rubber", obj.transform, new Vector3(-size.x * .12f, size.y / 2, 0), new Vector3(size.x * .76f, size.y, size.z), body);
                Part("Paper sleeve", obj.transform, new Vector3(size.x * .38f, size.y / 2, 0), new Vector3(size.x * .24f, size.y, size.z), detail);
            }
            var obstacle = obj.AddComponent<ObstacleView>();
            obstacle.Hitbox = obj.AddComponent<BoxCollider>();
            obstacle.Hitbox.center = new Vector3(0, size.y / 2, 0);
            obstacle.Hitbox.size = size;
            obstacle.Hitbox.isTrigger = true;
            return SavePrefab(obj, Root + "Prefabs/Obstacles/" + name + ".prefab");
        }

        private static GameObject BuildFloor(Material wood, Material lighter, Material border)
        {
            var floor = new GameObject("Wooden floor chunk");
            for (int i = 0; i < 6; i++)
                Part("Floorboard " + i, floor.transform, new Vector3(0, -.13f, -7.5f + i * 3),
                    new Vector3(40, .22f, 2.985f), i % 2 == 0 ? wood : lighter);
            foreach (float x in new[] { -3.9f, 3.9f })
            {
                Part("Track border", floor.transform, new Vector3(x, .005f, 0), new Vector3(.06f, .015f, 18), border);
                for (int i = 0; i < 6; i++)
                    Part("Decorative floor dot", floor.transform, new Vector3(x * 1.08f, .01f, -7.5f + i * 3),
                        new Vector3(.12f, .015f, .12f), border, PrimitiveType.Cylinder);
            }
            return SavePrefab(floor, Root + "Prefabs/Environments/WoodFloor.prefab");
        }

        private static void BuildCamera(GameConfig config)
        {
            var obj = new GameObject("Portrait Camera");
            obj.tag = "MainCamera";
            var camera = obj.AddComponent<Camera>();
            camera.backgroundColor = new Color(.18f, .28f, .35f);
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.allowHDR = false;
            obj.AddComponent<AudioListener>();
            var additional = obj.AddComponent<UniversalAdditionalCameraData>();
            additional.renderPostProcessing = false;
            var frame = obj.AddComponent<PortraitCamera>();
            frame.Config = config;
            frame.Reframe();
        }

        private static void BuildLight()
        {
            var obj = new GameObject("Warm window light");
            obj.transform.rotation = Quaternion.Euler(50, -30, 0);
            var light = obj.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.5f;
            light.color = new Color(1, .93f, .8f);
            light.shadows = LightShadows.Soft;
            RenderSettings.sun = light;
            RenderSettings.ambientMode = AmbientMode.Flat;
            RenderSettings.ambientLight = new Color(.5f, .57f, .65f);
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(.65f, .48f, .32f);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 48;
            RenderSettings.fogEndDistance = 115;
        }

        private static PrototypeUI BuildUI()
        {
            var events = new GameObject("Event System", typeof(EventSystem), typeof(InputSystemUIInputModule));
            events.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();
            var canvasObj = new GameObject("Prototype UI", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvasObj.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080, 1920);
            scaler.matchWidthOrHeight = .5f;
            var ui = canvasObj.AddComponent<PrototypeUI>();
            var safe = Rect("Safe Area", canvasObj.transform, Vector2.zero, Vector2.one);
            ui.SafeArea = safe;
            var hint = Label("Steering hint", safe, "SÜRÜKLE  •  KAÇIN", 36, Color.white);
            SetRect(hint.rectTransform, new Vector2(.1f, .9f), new Vector2(.75f, .97f));
            ui.PauseButton = Button("Pause", safe, "II", out _);
            SetRect(ui.PauseButton.GetComponent<RectTransform>(), new Vector2(.79f, .9f), new Vector2(.94f, .97f));

            var overlay = Rect("Overlay", safe, Vector2.zero, Vector2.one);
            var shade = overlay.gameObject.AddComponent<Image>();
            shade.color = new Color(.025f, .065f, .12f, .87f);
            ui.Overlay = overlay.gameObject;
            ui.Heading = Label("Heading", overlay, "BRAKE TOY", 78, new Color(1, .78f, .26f));
            SetRect(ui.Heading.rectTransform, new Vector2(.05f, .61f), new Vector2(.95f, .77f));
            ui.Description = Label("Description", overlay, "", 38, Color.white);
            SetRect(ui.Description.rectTransform, new Vector2(.08f, .37f), new Vector2(.92f, .60f));
            ui.ActionButton = Button("Play or retry", overlay, "OYNA", out Text actionText);
            ui.ActionText = actionText;
            SetRect(ui.ActionButton.GetComponent<RectTransform>(), new Vector2(.16f, .23f), new Vector2(.84f, .33f));
            var phase = Label("Prototype version", overlay, "FAZ 1  /  TOY ROOM", 26, new Color(.65f, .77f, .81f));
            SetRect(phase.rectTransform, new Vector2(.1f, .12f), new Vector2(.9f, .18f));
            return ui;
        }

        private static RectTransform Rect(string name, Transform parent, Vector2 min, Vector2 max)
        {
            var obj = new GameObject(name, typeof(RectTransform));
            obj.transform.SetParent(parent, false);
            var rect = (RectTransform)obj.transform;
            SetRect(rect, min, max);
            return rect;
        }
        private static void SetRect(RectTransform rect, Vector2 min, Vector2 max)
        { rect.anchorMin = min; rect.anchorMax = max; rect.offsetMin = rect.offsetMax = Vector2.zero; }

        private static Text Label(string name, Transform parent, string text, int size, Color color)
        {
            var rect = Rect(name, parent, Vector2.zero, Vector2.one);
            var label = rect.gameObject.AddComponent<Text>();
            label.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            label.text = text;
            label.fontSize = size;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = color;
            label.raycastTarget = false;
            return label;
        }

        private static Button Button(string name, Transform parent, string text, out Text label)
        {
            var rect = Rect(name, parent, Vector2.zero, Vector2.one);
            var image = rect.gameObject.AddComponent<Image>();
            image.color = new Color(1, .73f, .17f);
            var button = rect.gameObject.AddComponent<Button>();
            button.targetGraphic = image;
            label = Label("Label", rect, text, 42, new Color(.08f, .13f, .19f));
            return button;
        }

        private static void ConfigurePlayer()
        {
            PlayerSettings.companyName = "Brake Toy";
            PlayerSettings.productName = "Brake Toy";
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToPortrait = true;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = false;
            PlayerSettings.allowedAutorotateToLandscapeRight = false;
            PlayerSettings.defaultScreenWidth = 540;
            PlayerSettings.defaultScreenHeight = 960;
            PlayerSettings.SetApplicationIdentifier(UnityEditor.Build.NamedBuildTarget.Android, "com.braketoy.prototype");
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.SetScriptingBackend(UnityEditor.Build.NamedBuildTarget.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.runInBackground = false;
        }
    }
}
