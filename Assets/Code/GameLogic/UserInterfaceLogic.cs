namespace Assets.Code.GameLogic
{
    using IoC;
    using Common;
    using DataAccess;
    using MonoBehaviours.Configuration;
    using MonoBehaviours.UserInterface;

    public class UserInterfaceLogic : LogicBase
    {
        public CanvasManager CurrentCanvas { get; private set; }

        public UserInterfaceLogic(IoC container, PrefabManager prefabManager, GlobalConfiguration config) : base(container, prefabManager, config)
        {
        }

        internal void InitializeGameCanvas()
        {
            InitializeCanvas(Configuration.ui_next_level_canvas_manager);
        }

        internal void InitializeGameOverCanvas()
        {
            InitializeCanvas(Configuration.ui_game_over_canvas_manager);
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
    }
}