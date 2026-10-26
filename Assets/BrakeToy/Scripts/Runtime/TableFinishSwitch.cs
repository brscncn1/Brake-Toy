using UnityEngine;

namespace BrakeToy
{
    public sealed class TableFinishSwitch : MonoBehaviour
    {
        public Renderer[] Surfaces;
        public Material[] Original;
        public Material ReferenceFinish;
        public bool ReferenceEnabled = true;
        public void Apply(bool enabled)
        {
            ReferenceEnabled=enabled;
            for(int i=0;i<Surfaces.Length;i++)
                Surfaces[i].sharedMaterial=enabled?ReferenceFinish:Original[i];
        }
        public void Toggle()=>Apply(!ReferenceEnabled);
        private void Start()=>Apply(ReferenceEnabled);
    }
}
