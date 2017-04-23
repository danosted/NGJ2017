namespace Assets.Code.GameLogic
{
    using MonoBehaviours.Configuration;
    using Common;
    using DataAccess;
    using IoC;
    using Assets.Code.MonoBehaviours.Audio;
    using UnityEngine;

    public class AudioLogic : LogicBase
    {

        protected AudioSystem AudioSystem { get; set; }

        public AudioLogic(IoC container, PrefabManager prefabManager, GlobalConfiguration config) : base(container, prefabManager, config)
        {
        }

        public void InitializeAudio()
        {
            AudioSystem = PrefabManager.GetPrefab(Configuration.audio_system);
            AudioSystem.Activate(Container);
        }

        public void PlayAudioClipFromConfiguration(AudioClip clip)
        {
            AudioSystem.PlayAudioClip(clip);
        }

    }
}
