namespace Assets.Code.MonoBehaviours.CheckPoints
{
    using Assets.Code.Common;
    using Assets.Code.GameLogic;
    using Assets.Code.MonoBehaviours.UserInterface;
    using IoC;
    using Players;
    using UnityEngine;

    public class TriggerPoint : PrefabBase
    {
        public bool isLethal = true;

        private bool _isTriggered { get; set; }
        private bool _playerIsReturned { get; set; }
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
            _playerIsReturned = false;
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
            if (_isTriggered && isLethal)
            {
                if (!_playerIsReturned)
                {
                    var player = PrefabManager.GetActivePrefab(Configuration.prefab_player);
                    PrefabManager.ReturnPrefab(player);
                    _canvas = PrefabManager.GetPrefab(Configuration.ui_game_over_canvas_manager);
                    _canvas.Activate(Container);
                    _playerIsReturned = true;
                }
                
                if (_secondsWaited >= _secondsToWait)
                {
                    ResetTrigger();
                    PrefabManager.ReturnPrefab(_canvas);
                    Container.Resolve<FlowLogic>().RestartAtLastCheckPoint();
                    return;
                }
                _secondsWaited += Time.deltaTime;
            }
        }
    }
}