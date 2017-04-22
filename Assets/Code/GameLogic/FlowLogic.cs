namespace Assets.Code.GameLogic
{
    using MonoBehaviours.Configuration;
    using Common;
    using DataAccess;
    using DataAccess.DTOs;
    using IoC;
    using UnityEngine;
    using UnityEngine.SceneManagement;

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
            Container.Resolve<UserInterfaceLogic>().InitializeGameCanvas();
            
            // Create an object
            var obj = PrefabManager.GetPrefab(Configuration.prefab_moveable_object);
            obj.Activate(Container, Vector3.zero);
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
