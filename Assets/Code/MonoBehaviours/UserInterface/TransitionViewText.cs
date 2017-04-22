namespace Assets.Code.MonoBehaviours.UserInterface
{
    using IoC;
    using Common;
    using UnityEngine;
    using UnityEngine.UI;
    using GameLogic;
    using System.Collections.Generic;
    using System.Linq;

    [RequireComponent(typeof(Text))]
    public class TransitionViewText : PrefabBase
    {
        [TextArea(3, 10)]
        public string csvList;

        private List<string> _textList;

        private Text Text { get; set; }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            Text = GetComponent<Text>();
            _textList = csvList.Split(';').ToList();
        }

        void Update()
        {
            // TODO 1 (DRO): Time based update of text list with fade effect, configurable values
        }
    }
}
