namespace Assets.Code.MonoBehaviours.Audio
{
    using Common;
    using IoC;
    using UnityEngine;

    public class AudioClipConfiguration : PrefabBase
    {

        public AudioClip[] AudioClips;

        public override void Activate(IoC container)
        {
            base.Activate(container);
            
        }


    }
}
