namespace Assets.Code.GameLogic
{
    using MonoBehaviours.Configuration;
    using Common;
    using DataAccess;
    using IoC;
    using Assets.Code.MonoBehaviours.CheckPoints;
    using System;
    using UnityEngine;

    public class CheckPointLogic : LogicBase
    {
        public CheckPoint LastCheckPoint { get; private set; }

        public CheckPointLogic(IoC container, PrefabManager prefabManager, GlobalConfiguration config) : base(container, prefabManager, config)
        {
        }

        public void RegisterCheckPoint(CheckPoint checkpoint)
        {
            Debug.LogFormat("CheckPoint priority: {0}.", checkpoint.priority);
            if (LastCheckPoint == null)
            {
                LastCheckPoint = checkpoint;
                return;
            }
            if(LastCheckPoint.priority < checkpoint.priority)
            {
                LastCheckPoint = checkpoint;
            }
        }

        internal void ActivateCheckPoints()
        {
            var checkpoints = UnityEngine.Object.FindObjectsOfType<CheckPoint>();
            foreach(var chk in checkpoints)
            {
                chk.Activate(Container);
            }
        }

        internal void ActivateTriggerPoints()
        {
            var triggers = UnityEngine.Object.FindObjectsOfType<TriggerPoint>();
            foreach (var trigger in triggers)
            {
                trigger.Activate(Container);
            }
        }
    }
}
