using UnityEngine;
namespace BrakeToy
{
    public sealed class RoomDayCycle:MonoBehaviour
    {
        public Light SunLight,RoomLamp;public Renderer Sky;public Transform Sun,Moon,Clouds;public GameObject Stars,Birds;
        public float CycleSeconds=120;[Range(0,1)]public float TimeOfDay=.18f;public bool Animate=true;
        public Transform[] BirdWings;
        private Material skyMaterial;private float clock;
        private void Start(){skyMaterial=new Material(Sky.sharedMaterial);Sky.sharedMaterial=skyMaterial;Apply();}
        private void Update(){if(Animate){clock+=Time.unscaledDeltaTime;TimeOfDay=Mathf.Repeat(TimeOfDay+Time.unscaledDeltaTime/CycleSeconds,1);}Apply();}
        public void Apply()
        {
            float angle=(TimeOfDay-.25f)*Mathf.PI*2,day=Mathf.SmoothStep(0,1,Mathf.InverseLerp(-.18f,.30f,Mathf.Cos(angle)));
            Color sky=Color.Lerp(new Color(.022f,.035f,.13f),new Color(.32f,.73f,1),day);
            if(skyMaterial){skyMaterial.SetColor("_BaseColor",sky);skyMaterial.SetColor("_EmissionColor",sky*.65f);}
            Sun.localPosition=new Vector3(Mathf.Sin(angle)*3,Mathf.Cos(angle)*2.6f-.4f,0);Moon.localPosition=new Vector3(-Mathf.Sin(angle)*3,-Mathf.Cos(angle)*2.6f-.4f,-.02f);
            Sun.gameObject.SetActive(day>.04f);Moon.gameObject.SetActive(day<.94f);Stars.SetActive(day<.38f);Birds.SetActive(day>.55f);
            SunLight.intensity=Mathf.Lerp(.28f,1.32f,day);SunLight.color=Color.Lerp(new Color(.48f,.59f,1),new Color(1,.94f,.78f),day);SunLight.transform.rotation=Quaternion.Euler(Mathf.Lerp(12,55,day),125+Mathf.Sin(angle)*35,0);
            RenderSettings.ambientSkyColor=Color.Lerp(new Color(.30f,.35f,.57f),new Color(.65f,.75f,.85f),day);RenderSettings.ambientEquatorColor=Color.Lerp(new Color(.28f,.29f,.40f),new Color(.48f,.54f,.59f),day);RenderSettings.ambientGroundColor=new Color(.24f,.22f,.25f);
            if(RoomLamp)RoomLamp.intensity=Mathf.Lerp(2.2f,.35f,day);
            if(Clouds)Clouds.localPosition=new Vector3(Mathf.Sin(clock*.025f)*1.4f,0,0);
            Birds.transform.localPosition=new Vector3(Mathf.Repeat(clock*.38f,11)-5.5f,Mathf.Sin(clock*.4f)*.3f,0);
            if(BirdWings!=null)for(int i=0;i<BirdWings.Length;i++)BirdWings[i].localRotation=Quaternion.Euler(0,0,Mathf.Sin(clock*8+i)*22);
        }
        private void OnDestroy(){if(skyMaterial)Destroy(skyMaterial);}
    }
}
