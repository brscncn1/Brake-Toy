using UnityEngine;
namespace BrakeToy
{
    public sealed class BookRamp:MonoBehaviour
    {
        public bool InUse,Launched;
        public void Place(float x,float z){transform.position=new Vector3(x,0,z);InUse=true;Launched=false;gameObject.SetActive(true);}
        public void Release(){InUse=false;gameObject.SetActive(false);}
    }
}
