namespace Assets.Code.MonoBehaviours.CheckPoints
{
    using Assets.Code.Common;
    using Assets.Code.GameLogic;
    using IoC;
    using Players;

    public class CheckPoint : PrefabBase
    {
        public int priority;

        public override void Activate(IoC container)
        {
            base.Activate(container);
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            var player = other.GetComponent<Player>();
            if(player == null)
            {
                return;
            }
            Container.Resolve<CheckPointLogic>().RegisterCheckPoint(this);
        }
    }
}