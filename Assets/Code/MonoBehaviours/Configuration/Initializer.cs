namespace Assets.Code.MonoBehaviours.Configuration
{
    using UnityEngine;
    using IoC;
    using GameLogic;
    using DataAccess;

    public class Initializer : MonoBehaviour
    {
        [Header("Global Configuration Prefab"), Tooltip("Find under Assets/Prefabs/Configuration/")]
        public GlobalConfiguration GlobalConfiguration;
        private IoC Container { get; set; }
        private PrefabManager PrefabManager { get; set; }

        /// <summary>
        /// Master awake - no other awake methods should be used
        /// </summary>
        void Awake()
        {
            // Initialize "IoC" container with the configuration to distribute
            Container = new IoC(GlobalConfiguration);
            PrefabManager = Container.Resolve<PrefabManager>();

            // Initialize game...
            InitializeGame();

        }

        private void InitializeGame()
        {
            // Initialize Game
            var control = Container.Resolve<FlowLogic>();
            control.InitializeGame();
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
                Container.Resolve<FlowLogic>().GameOver();
            }
            if (Input.GetMouseButtonDown(0))
            {
                // If game is not started, we start the game
                if (!PrefabManager.GetConfiguration().param_game_started)
                {
                    PrefabManager.GetConfiguration().param_game_started = true;
                    Container.Resolve<FlowLogic>().StartGame();
                }

                // If game is over, we restart the game
                if (!PrefabManager.GetConfiguration().param_game_over) return;
                Container.Resolve<FlowLogic>().RestartGame();
            }
        }
    }
}
