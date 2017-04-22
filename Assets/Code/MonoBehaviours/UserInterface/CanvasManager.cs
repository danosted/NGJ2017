namespace Assets.Code.MonoBehaviours.UserInterface
{
    using IoC;
    using Common;
    using UnityEngine;
    
    [RequireComponent(typeof(Canvas))]
    public class CanvasManager : PrefabBase
    {
        public override void Activate(IoC container)
        {
            base.Activate(container);
            gameObject.SetActive(true);

            // Activate all UI parts
            var uiprefabs = GetComponentsInChildren<PrefabBase>();
            foreach(var p in uiprefabs)
            {
                if (p == this) continue;
                p.Activate(container);
            }
        }

        public void Activate(IoC container, Camera camera)
        {
            base.Activate(container);
            gameObject.SetActive(true);

            var canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = camera;
            gameObject.SetActive(true);

            // Activate all UI parts
            var uiprefabs = GetComponentsInChildren<PrefabBase>();
            foreach (var p in uiprefabs)
            {
                if (p == this) continue;
                p.Activate(container);
            }
        }
    }
}
