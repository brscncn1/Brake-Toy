using UnityEngine;
namespace BrakeToy
{
    /// <summary>One phase-locked layout for table props. No gameplay tuning or collision changes.</summary>
    [DefaultExecutionOrder(600)]
    public sealed class TidyRoom:MonoBehaviour
    {
        public Transform[] Cells,NearProps;public Vector3[] CellStarts,PropStarts;
        public Transform Radio,Shelf;public float ShelfTop=3.03f;
        private GameManager game;
        private void Awake(){game=GetComponent<GameManager>();}
        public static float PositionZ(float start,float distance)=>Mathf.Repeat(start+10-distance,64)-10;
        public void Apply(float distance)
        {for(int i=0;i<Cells.Length;i++){var p=CellStarts[i];p.z=PositionZ(p.z,distance);Cells[i].position=p;}for(int i=0;i<NearProps.Length;i++){var p=PropStarts[i];p.z=PositionZ(p.z,distance);NearProps[i].position=p;}}
        private void LateUpdate(){Apply(game.Arcade.Distance);}
        public static Bounds BoundsOf(Transform root)
        {var filters=root.GetComponentsInChildren<MeshFilter>(true);Bounds b=new Bounds(root.position,Vector3.zero);bool first=true;foreach(var f in filters){if(!f.sharedMesh)continue;var local=f.sharedMesh.bounds;for(int i=0;i<8;i++){var p=f.transform.TransformPoint(local.center+Vector3.Scale(local.extents,new Vector3((i&1)==0?-1:1,(i&2)==0?-1:1,(i&4)==0?-1:1)));if(first){b=new Bounds(p,Vector3.zero);first=false;}else b.Encapsulate(p);}}return b;}
    }
}
