using UnityEngine;

namespace BrakeToy
{
    public sealed class NightRoomLamps : MonoBehaviour
    {
        public bool TrackSpill = true;
        public Light ReadingLight { get; private set; }
        public Light CornerLight { get; private set; }
        private Material shade, metal;
        private Transform fixtures;

        public void Initialize()
        {
            if (fixtures) return;
            fixtures = new GameObject("Two dim bedroom lamps").transform;
            fixtures.SetParent(transform, false);
            shade = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            shade.SetColor("_BaseColor", new Color(1, .72f, .37f));
            shade.EnableKeyword("_EMISSION");
            metal = new Material(shade);
            metal.DisableKeyword("_EMISSION");
            metal.SetColor("_BaseColor", new Color(.13f, .22f, .26f));
            // The left fixture sits beside the radio on the existing shelf.
            float top = GetComponent<TidyRoom>() ? GetComponent<TidyRoom>().ShelfTop : 3.03f;
            ReadingLight = Fixture("Shelf reading lamp", new Vector3(-7.85f, top + .03f, 27), 1.6f);
            ReadingLight.type = LightType.Spot;
            ReadingLight.spotAngle = 64;
            ReadingLight.innerSpotAngle = 36;
            ReadingLight.range = 16;
            ReadingLight.transform.LookAt(new Vector3(-1.6f, 0, 22));
            // A shallow base rests on the headboard; no tall pole through the bed or toy platforms.
            CornerLight = Fixture("Headboard reading lamp", new Vector3(9.5f, 1.38f, 31.17f), 1.6f, true);
            CornerLight.type = LightType.Point;
            CornerLight.range = 10;
        }

        private Light Fixture(string name, Vector3 position, float height, bool shallowBase = false)
        {
            var root = new GameObject(name).transform;
            root.SetParent(fixtures, false);
            root.position = position;
            Part("Weighted base", root, new Vector3(0, .08f, 0), new Vector3(1.1f, .08f, shallowBase ? .4f : 1.1f), metal);
            Part("Lamp stem", root, new Vector3(0, height * .5f, 0), new Vector3(.10f, height * .5f, .10f), metal);
            Part("Warm fabric shade", root, new Vector3(0, height, 0), new Vector3(1.2f, .42f, 1.2f), shade);
            var light = new GameObject("Warm local light").AddComponent<Light>();
            light.transform.SetParent(root, false);
            light.transform.localPosition = new Vector3(0, height - .25f, 0);
            light.color = new Color(1, .70f, .37f);
            light.shadows = LightShadows.None;
            return light;
        }
        private static void Part(string name, Transform parent, Vector3 position, Vector3 scale, Material material)
        {
            var part = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            part.name = name;
            part.transform.SetParent(parent, false);
            part.transform.localPosition = position;
            part.transform.localScale = scale;
            Destroy(part.GetComponent<Collider>());
            part.GetComponent<Renderer>().sharedMaterial = material;
        }
        public void Apply(float day)
        {
            if (!ReadingLight) return;
            float night = 1 - Mathf.SmoothStep(0, 1, day);
            ReadingLight.intensity = TrackSpill ? 1.6f * night : 0;
            CornerLight.intensity = .9f * night;
            shade.SetColor("_EmissionColor", new Color(1, .44f, .12f) * (.05f + night * .9f));
        }
        private void OnDestroy()
        {
            if (fixtures) Destroy(fixtures.gameObject);
            if (shade) Destroy(shade);
            if (metal) Destroy(metal);
        }
    }
}
