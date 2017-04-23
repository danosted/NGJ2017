namespace Assets.Code.GameLogic
{
    using IoC;
    using Common;
    using DataAccess;
    using MonoBehaviours.Configuration;
    using MonoBehaviours.UserInterface;
    using UnityEngine;
    using Assets.Code.MonoBehaviours.Players;
    using System;

    public class UserInterfaceLogic : LogicBase
    {
        public CanvasManager CurrentCanvas { get; private set; }

        public UserInterfaceLogic(IoC container, PrefabManager prefabManager, GlobalConfiguration config) : base(container, prefabManager, config)
        {
        }

        internal void InitializeGameCanvas()
        {
            var transitionCam = PrefabManager.GetPrefab(Configuration.prefab_transition_view);
            InitializeCanvas(Configuration.ui_transtition_canvas);
        }

        internal void InitializeGameOverCanvas()
        {
            InitializeCanvas(Configuration.ui_game_over_canvas_manager);
        }

        internal void InitializeGameCompletedCanvas()
        {
            InitializeCanvas(Configuration.ui_game_completed_canvas_manager);
        }

        internal void InitializeGameEndCanvas()
        {
            var transitionCam = PrefabManager.GetPrefab(Configuration.prefab_transition_view);
            InitializeCanvas(Configuration.ui_game_end_canvas_manager);
        }

        internal void InitializeIntroCanvas()
        {
            var transitionCam = PrefabManager.GetPrefab(Configuration.prefab_transition_view);
            InitializeCanvas(Configuration.ui_intro_canvas_manager);
        }

        internal void InitializeGameMenuCanvas()
        {
            InitializeCanvas(Configuration.ui_game_completed_canvas_manager);
        }

        private void InitializeCanvas(CanvasManager canvas)
        {
            if (CurrentCanvas != null)
            {
                PrefabManager.ReturnPrefab(CurrentCanvas);
            }
            CurrentCanvas = PrefabManager.GetPrefab(canvas);
            CurrentCanvas.Activate(Container);
        }

        private void InitializeCanvas(CanvasManager canvas, Camera camera)
        {
            if (CurrentCanvas != null)
            {
                PrefabManager.ReturnPrefab(CurrentCanvas);
            }
            CurrentCanvas = PrefabManager.GetPrefab(canvas);
            CurrentCanvas.Activate(Container, camera);
        }

        internal void InitializeNextLevelCanvas()
        {
            InitializeCanvas(Configuration.ui_next_level_canvas_manager);
        }
    }
}