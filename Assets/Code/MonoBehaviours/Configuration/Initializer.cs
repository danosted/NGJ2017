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
        }
    }
}
