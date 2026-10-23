using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
namespace BrakeToy.Editor
{
    public static class JoyfulUpgrade
    {
        public static void Apply()
        {
            var scene=EditorSceneManager.OpenScene(JoyfulSceneBuilder.ScenePath);var g=Object.FindFirstObjectByType<GameManager>();g.Arcade.DistanceRecordKey="BrakeToy.v07.distance";JoyfulArt.LoadPalette();
            foreach(var definition in g.Arcade.Definitions)
            {
                string prefab=AssetDatabase.GetAssetPath(definition.Prefab);var root=PrefabUtility.LoadPrefabContents(prefab);var h=root.GetComponent<ArcadeHazard>();
                try{if(definition.Kind==0&&h.Model.name!="Wind-up space rocket"){Object.DestroyImmediate(h.Model.gameObject);h.Model=JoyfulArt.Rocket(h.transform);JoyfulArt.Combine(h.Model,"ToyRocket");h.Height=1.62f;}if(definition.Kind==6&&!h.Model.Find("Wheel unit")){Object.DestroyImmediate(h.Model.gameObject);h.Model=JoyfulArt.Bus(h.transform);JoyfulArt.Combine(h.Model,"WheeledBus");}h.HalfWidth=definition.Kind==3?.48f:definition.Kind==6?.9f:definition.Kind==2?.56f:.68f;h.HalfLength=definition.Kind==3?.48f:definition.Kind==6?.60f:definition.Kind==2?.66f:.45f;definition.HalfWidth=h.HalfWidth;definition.HalfLength=h.HalfLength;EditorUtility.SetDirty(definition);PrefabUtility.SaveAsPrefabAsset(root,prefab);}finally{PrefabUtility.UnloadPrefabContents(root);}
            }
            foreach(var pair in new[]{new[]{"4","Mango","HoneyFabric"},new[]{"1","Rose","DollFabric"},new[]{"1","Hair","YarnHair"}})
            {
                string path=JoyfulArt.Root+"Materials/"+pair[2]+".mat";var fabric=AssetDatabase.LoadAssetAtPath<Material>(path);
                if(!fabric){fabric=new Material(AssetDatabase.LoadAssetAtPath<Material>(JoyfulArt.Root+"Materials/"+pair[1]+".mat")){name=pair[2]};string cotton="Assets/BrakeToy/Art/Living/ThirdParty/cotton_jersey/cotton_jersey_";fabric.SetTexture("_BaseMap",AssetDatabase.LoadAssetAtPath<Texture2D>(cotton+"diff_1k.jpg"));fabric.SetTexture("_BumpMap",AssetDatabase.LoadAssetAtPath<Texture2D>(cotton+"nor_gl_1k.jpg"));fabric.SetFloat("_BumpScale",.25f);fabric.EnableKeyword("_NORMALMAP");fabric.SetFloat("_Smoothness",.12f);AssetDatabase.CreateAsset(fabric,path);}
                string prefab=JoyfulArt.Root+"Prefabs/Toy_"+pair[0]+".prefab";var root=PrefabUtility.LoadPrefabContents(prefab);
                try{foreach(var r in root.GetComponentsInChildren<Renderer>()){var mats=r.sharedMaterials;for(int i=0;i<mats.Length;i++)if(mats[i].name=="Joyful "+pair[1])mats[i]=fabric;r.sharedMaterials=mats;}PrefabUtility.SaveAsPrefabAsset(root,prefab);}finally{PrefabUtility.UnloadPrefabContents(root);}
            }
            foreach(var ramp in g.GetComponent<JoyfulRun>().Ramps)foreach(var f in ramp.GetComponentsInChildren<MeshFilter>(true))if(f.name.Contains("Purple")&&f.sharedMesh.bounds.max.y>.69f)
            {string path=JoyfulArt.Root+"Meshes/RampCoverCalibrated.asset";var mesh=AssetDatabase.LoadAssetAtPath<Mesh>(path);if(!mesh){mesh=Object.Instantiate(f.sharedMesh);var v=mesh.vertices;for(int i=0;i<v.Length;i++)v[i].y-=.044f;mesh.vertices=v;mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,path);}f.sharedMesh=mesh;}
            if(!GameObject.Find("Joyful table frame aligned"))
            {
                foreach(var f in GameObject.Find("Bedroom Set").GetComponentsInChildren<MeshFilter>())
                {var v=f.sharedMesh.vertices;bool changed=false;for(int i=0;i<v.Length;i++){var p=f.transform.TransformPoint(v[i]);if(p.y<-.15f&&p.z<30.1f&&Mathf.Abs(p.x)>5.3f&&Mathf.Abs(p.x)<6.5f){p.x*=.86f;v[i]=f.transform.InverseTransformPoint(p);changed=true;}}if(changed){var mesh=Object.Instantiate(f.sharedMesh);mesh.vertices=v;mesh.RecalculateBounds();AssetDatabase.CreateAsset(mesh,AssetDatabase.GenerateUniqueAssetPath(JoyfulArt.Root+"Meshes/TableFrameAligned.asset"));f.sharedMesh=mesh;}}
                new GameObject("Joyful table frame aligned").transform.SetParent(GameObject.Find("Bedroom Set").transform,false);
            }
            EditorSceneManager.MarkSceneDirty(scene);EditorSceneManager.SaveScene(scene);AssetDatabase.SaveAssets();
        }
    }
}
