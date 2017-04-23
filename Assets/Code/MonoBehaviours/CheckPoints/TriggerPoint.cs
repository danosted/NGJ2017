namespace Assets.Code.MonoBehaviours.CheckPoints
{
    using System;
    using Assets.Code.Common;
    using Assets.Code.GameLogic;
    using Assets.Code.MonoBehaviours.UserInterface;
    using IoC;
    using Players;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class TriggerPoint : PrefabBase
    {
        public bool _isLethal = true;
        public bool GameCompleted;
        public int LevelScene;

        private bool _isTriggered { get; set; }
        private bool _canvasIsTriggered { get; set; }
        private float _secondsToWait { get; set; }
        private float _secondsWaited { get; set; }
        private CanvasManager _canvas { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            ResetTrigger();
        }

        private void ResetTrigger()
        {
            _isTriggered = false;
            _canvasIsTriggered = false;
            _secondsToWait = Configuration.param_trigger_freeze_seconds;
            _secondsWaited = 0f;
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            var player = other.GetComponent<Player>();
            if (player == null)
            {
                return;
            }
            _isTriggered = true;

        }

        private void Update()
        {
            if (_isTriggered && GameCompleted)
            {
                TriggerGameCompleted();
            }
            if (_isTriggered && _isLethal)
            {
                TriggerDeath();
            }
            if (_isTriggered && !_isLethal && !GameCompleted)
            {
                TriggerNextScene();
            }
        }

        private void TriggerGameCompleted()
        {
            if (!_canvasIsTriggered)
            {
                TriggerPlayerFreeze(Configuration.ui_game_completed_canvas_manager);
                Container.Resolve<UserInterfaceLogic>().InitializeGameCompletedCanvas();
                _canvasIsTriggered = true;
            }

        }

        private void TriggerNextScene()
        {
            if (!_canvasIsTriggered)
            {
                TriggerPlayerFreeze(Configuration.ui_next_level_canvas_manager);
                Container.Resolve<UserInterfaceLogic>().InitializeNextLevelCanvas();
                _canvasIsTriggered = true;
            }
            if (_secondsWaited >= _secondsToWait)
            {
                SceneManager.LoadScene(LevelScene);
            }
            _secondsWaited += Time.deltaTime;
        }

        private void TriggerPlayerFreeze(CanvasManager canvas)
        {
            if (!_canvasIsTriggered)
            {
                var player = PrefabManager.GetActivePrefab(Configuration.prefab_player);
                player.enabled = false;
            }
        }

        private void TriggerDeath()
        {
            if (!_canvasIsTriggered)
            {
                TriggerPlayerFreeze(Configuration.ui_game_over_canvas_manager);
            }

            if (_secondsWaited >= _secondsToWait)
            {
                var player = PrefabManager.GetActivePrefab(Configuration.prefab_player);
                PrefabManager.ReturnPrefab(player);
                PrefabManager.ReturnPrefab(_canvas);
                ResetTrigger();
                Container.Resolve<FlowLogic>().RestartAtLastCheckPoint();
                return;
            }
            _secondsWaited += Time.deltaTime;
        }
    }
}