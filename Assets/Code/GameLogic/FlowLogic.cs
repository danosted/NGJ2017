namespace Assets.Code.GameLogic
{
    using MonoBehaviours.Configuration;
    using Common;
    using DataAccess;
    using DataAccess.DTOs;
    using IoC;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Assets.Code.Utilities;

    public class FlowLogic : LogicBase
    {

        public FlowLogic(IoC container, PrefabManager prefabManager, GlobalConfiguration config) : base(container, prefabManager, config)
        {
        }

        public void InitializeGame()
        {
            // Initialize UI
            Container.Resolve<UserInterfaceLogic>().InitializeGameMenuCanvas();

            // Initialize Audio
            Container.Resolve<AudioLogic>().InitializeAudio();

        }

        public void StartGame()
        {
            // Change to game UI
            //Container.Resolve<UserInterfaceLogic>().InitializeGameCanvas();
            //Container.Resolve<ScreenUtil>();
            
            // Create the player
            var player = PrefabManager.GetPrefab(Configuration.prefab_player);
            player.Activate(Container);


            // Initialize voices
            var voice1 = PrefabManager.GetPrefab(Configuration.prefab_voice1);
            voice1.Activate(Container);

            var voice2 = PrefabManager.GetPrefab(Configuration.prefab_voice2);
            voice2.Activate(Container);

            var voice3 = PrefabManager.GetPrefab(Configuration.prefab_voice3);
            voice3.Activate(Container);
        }

        public void GameOver()
        {
            Container.Resolve<UserInterfaceLogic>().InitializeGameOverCanvas();
            PrefabManager.Shutdown();
        }

        public void RestartGame()
        {
            // TODO 2 (DRO): this could be done more efficiently
            SceneManager.LoadScene(0);
        }
    }
}
