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
    public class EndGameText : PrefabBase
    {
        private Text Text { get; set; }

        public float _timeUntilShowing;
        private float _currentTimeWaited;
        private float _textFadeMagnitude;
        private Color _hiddenColor;
        private TextState _textState;

        private enum TextState
        {
            ShowingColor,
            HidingColor
        }

        public override void Activate(IoC container)
        {
            base.Activate(container);
            Text = GetComponent<Text>();
            _textFadeMagnitude = Configuration.param_floating_text_fade_speed;
            _hiddenColor = Color.white;
            _hiddenColor.a = 0;
            _textState = TextState.HidingColor;
            _currentTimeWaited = 0f;
            Text.color = _hiddenColor;
        }

        void ShowNewText()
        {
            Text.color = _hiddenColor;
            _textState = TextState.ShowingColor;
        }

        void Update()
        {
            if(_timeUntilShowing <= _currentTimeWaited && _textState != TextState.ShowingColor)
            {
                ShowNewText();
            }
            if (_textState == TextState.ShowingColor)
            {
                var alphaValue = Text.color.a + _textFadeMagnitude * Time.deltaTime;
                alphaValue = alphaValue > 255 ? 255 : alphaValue;
                Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alphaValue);
            }
            _currentTimeWaited += Time.deltaTime;
        }
    }
}
