namespace Assets.Code.MonoBehaviours.Players
{
    using UnityEngine;
    using IoC;
    using DataAccess;
    using Common;

    public class TransitionView : PrefabBase
    {
        protected Transform PlayerTransform { get; set; }
        protected Collider PlayerCollider { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            PrefabManager.ReturnPrefab(this);
        }
    }
}
