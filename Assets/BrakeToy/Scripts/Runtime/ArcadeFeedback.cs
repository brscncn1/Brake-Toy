using UnityEngine;
using UnityEngine.Rendering;

namespace BrakeToy
{
    /// <summary>Two reusable ground ribbons for Combo Fever; visual only, no movement or collider changes.</summary>
    public sealed class ArcadeFeedback : MonoBehaviour
    {
        private GameManager game;
        private ArcadeDirector arcade;
        private Material material;
        private Mesh mesh;
        private MeshRenderer ribbon;
        private readonly Vector3[] vertices=new Vector3[16];
        public bool FeverVisible => ribbon && ribbon.enabled;
        public void Initialize(GameManager manager,ArcadeDirector director)
        {
            game=manager;arcade=director;
            material=new Material(game.Player.GetComponentInChildren<MeshRenderer>().sharedMaterial){name="Combo Fever cyan — runtime"};
            material.SetColor("_BaseColor",new Color(.05f,.85f,.72f));material.SetColor("_EmissionColor",new Color(.06f,.65f,.55f));material.EnableKeyword("_EMISSION");material.SetFloat("_Metallic",0);
            var obj=new GameObject("Combo Fever ribbons — visual only",typeof(MeshFilter),typeof(MeshRenderer));obj.transform.SetParent(transform,false);
            mesh=new Mesh{name="Reusable Fever ribbons"};mesh.MarkDynamic();mesh.vertices=vertices;
            var indices=new int[36];
            for(int side=0;side<2;side++)for(int segment=0;segment<3;segment++)
            {int a=side*8+segment*2,b=a+1,c=a+2,d=a+3,k=(side*3+segment)*6;indices[k]=a;indices[k+1]=b;indices[k+2]=c;indices[k+3]=b;indices[k+4]=d;indices[k+5]=c;}
            mesh.triangles=indices;obj.GetComponent<MeshFilter>().sharedMesh=mesh;ribbon=obj.GetComponent<MeshRenderer>();ribbon.sharedMaterial=material;ribbon.shadowCastingMode=ShadowCastingMode.Off;ribbon.receiveShadows=false;ribbon.enabled=false;
        }
        private void LateUpdate()
        {
            if(!ribbon)return;ribbon.enabled=game.State==RunState.Running&&arcade.Style.Fever;if(!ribbon.enabled)return;
            for(int side=0;side<2;side++)for(int point=0;point<4;point++)
            {
                float width=.035f*(1-point/4f),x=game.Player.X+(side==0?-.23f:.23f)+Mathf.Sin(arcade.Elapsed*7-point)*.025f*point;
                int i=side*8+point*2;vertices[i]=new Vector3(x-width,.035f,-.7f-point*.65f);vertices[i+1]=new Vector3(x+width,.035f,-.7f-point*.65f);
            }
            mesh.vertices=vertices;mesh.RecalculateNormals();mesh.RecalculateBounds();
        }
        private void OnDestroy(){if(mesh)Destroy(mesh);if(material)Destroy(material);}
    }
}
