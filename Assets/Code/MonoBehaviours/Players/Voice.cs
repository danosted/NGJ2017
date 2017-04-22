namespace Assets.Code.MonoBehaviours.Players
{
    using UnityEngine;
    using IoC;
    using DataAccess;
    using Common;

    public class Voice : PrefabBase
    {
        protected Transform PlayerTransform { get; set; }
        protected Collider PlayerCollider { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            PlayerTransform = PrefabManager.GetActivePrefab(Configuration.prefab_player).transform;
            PlayerCollider = PlayerTransform.GetComponent<Collider>();
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            PrefabManager.ReturnPrefab(this);
        }

        protected virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z);
            transform.position = transform.position - Vector3.forward * PlayerCollider.bounds.extents.x * 2f;
        }
    }
}
