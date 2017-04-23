namespace Assets.Code.MonoBehaviours.Audio
{
    using Common;
    using IoC;
    using UnityEngine;

    public class AudioSystem : PrefabBase
    {
        private AudioSource AudioSource { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            AudioSource = GetComponent<AudioSource>();
        }

        public void PlayAudioClip(AudioClip audioClip)
        {
            AudioSource.PlayOneShot(audioClip);
            //PrefabManager.ReturnPrefab(audioClip);
        }
    }
}
