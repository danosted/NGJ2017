namespace Assets.Code.MonoBehaviours.CheckPoints
{
    using Assets.Code.Common;
    using Assets.Code.GameLogic;
    using IoC;
    using Players;

    public class TriggerPoint : PrefabBase
    {
        public bool isLethal = true;

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
            if (isLethal)
            {
                Container.Resolve<FlowLogic>().RestartAtLastCheckPoint();
            }
        }
    }
}