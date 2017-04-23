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
    using MonoBehaviours.Players;

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

            // Initialize Audio
            Container.Resolve<AudioLogic>().InitializeAudio();

            // Initialize Checkpoints and triggers
            Container.Resolve<CheckPointLogic>().ActivateCheckPoints();
            Container.Resolve<CheckPointLogic>().ActivateTriggerPoints();

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

            // Initialize god cam
            var godcam = PrefabManager.GetPrefab(Configuration.prefab_god_cam);
            godcam.Activate(Container);
            if (!Configuration.param_god_view_enabled)
            {
                godcam.gameObject.SetActive(false);
                voice1.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0f, 0.25f), new Vector2(0.33f, 0.5f));
                voice2.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.335f, 0.25f), new Vector2(0.33f, 0.5f));
                voice3.GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.67f, 0.25f), new Vector2(0.33f, 0.5f));
            }
        }

        public void GameOver()
        {
            //Container.Resolve<UserInterfaceLogic>().InitializeGameOverCanvas();
            //PrefabManager.Shutdown();
            PrefabManager.GetConfiguration().param_game_over = true;
            SceneManager.LoadScene(Configuration.param_end_game_scene_name);
        }

        public void RestartAtLastCheckPoint()
        {
            var chkPoint = Container.Resolve<CheckPointLogic>().LastCheckPoint;
            var player = PrefabManager.GetPrefab(Configuration.prefab_player);
            player.Activate(Container);
            if (chkPoint == null)
            {
                Debug.Log("No checkpoint. Restarting at beginning.");
                player.transform.position = Configuration.param_player_initial_position;
                return;
            }
            player.transform.position = chkPoint.transform.position;
        }

        public void RestartGame()
        {
            // TODO 2 (DRO): this could be done more efficiently
            SceneManager.LoadScene(0);
        }
    }
}
