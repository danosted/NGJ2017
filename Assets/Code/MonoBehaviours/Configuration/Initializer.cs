namespace Assets.Code.MonoBehaviours.Configuration
{
    using UnityEngine;
    using IoC;
    using GameLogic;
    using DataAccess;
    using UnityEngine.SceneManagement;
    using System;

    public class Initializer : MonoBehaviour
    {
        [Header("Global Configuration Prefab"), Tooltip("Find under Assets/Prefabs/Configuration/")]
        public GlobalConfiguration GlobalConfiguration;
        private IoC Container { get; set; }
        private PrefabManager PrefabManager { get; set; }

        private bool _introTriggered;
        private float _introActiveSeconds;

        /// <summary>
        /// Master awake - no other awake methods should be used
        /// </summary>
        void Awake()
        {
            // Initialize "IoC" container with the configuration to distribute
            Container = new IoC(GlobalConfiguration);
            PrefabManager = Container.Resolve<PrefabManager>();

            // Initialize game...
            if(SceneManager.GetActiveScene().name == GlobalConfiguration.scene_end_game_scene_name)
            {
                Container.Resolve<UserInterfaceLogic>().InitializeGameEndCanvas();
            }
            else if (SceneManager.GetActiveScene().name == GlobalConfiguration.scene_intro_scene_name)
            {
                Container.Resolve<UserInterfaceLogic>().InitializeIntroCanvas();
                _introTriggered = true;
                _introActiveSeconds = 0f;
            }
            else
            {
                InitializeGame();
            }
        }
        

        private void InitializeGame()
        {
            // Initialize Game
            var control = Container.Resolve<FlowLogic>();
            control.StartGame();
        }

        /// <summary>
        /// Global Input Check
        /// TODO 2 (DRO): maybe this should be located elsewhere
        /// </summary>
        void Update()
        {
            // Force game stop
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Container.Resolve<FlowLogic>().RestartGame();
            }

            // Intro sequence
            if (_introTriggered)
            {
                if(GlobalConfiguration.param_game_start_after_seconds <= _introActiveSeconds)
                {
                    SceneManager.LoadScene(GlobalConfiguration.scene_level_one_name);
                }
                _introActiveSeconds += Time.deltaTime;
            }
        }
    }
}
