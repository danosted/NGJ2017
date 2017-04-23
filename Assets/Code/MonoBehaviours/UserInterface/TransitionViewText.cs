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

        private float _textLingerSeconds;
        private float _textHiddenSeconds;
        private float _textCurrentLinger;
        private float _textCurrentHidden;
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
            _textList = csvList.Split(';').ToList();
            _textLingerSeconds = Configuration.param_floating_text_linger_seconds;
            _textHiddenSeconds = Configuration.param_floating_text_hidden_seconds;
            _textFadeMagnitude = Configuration.param_floating_text_fade_speed;
            _hiddenColor = Color.white;
            _hiddenColor.a = 0;
            _textState = TextState.ShowingColor;
            ShowNewText();
        }

        void ShowNewText()
        {
            var ran = Random.Range(0, _textList.Count());
            Text.color = _hiddenColor;
            Text.text = _textList[ran];
            _textCurrentLinger = 0f;
            _textState = TextState.ShowingColor;
        }

        void Update()
        {
            if(_textLingerSeconds <= _textCurrentLinger)
            {
                _textState = TextState.HidingColor;
            }
            if (_textState == TextState.ShowingColor)
            {
                var alphaValue = Text.color.a + _textFadeMagnitude * Time.deltaTime;
                alphaValue = alphaValue > 255 ? 255 : alphaValue;
                Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alphaValue);
                _textCurrentLinger += Time.deltaTime;
            }
            else if(_textState == TextState.HidingColor)
            {
                var alphaValue = Text.color.a - _textFadeMagnitude * Time.deltaTime;
                alphaValue = alphaValue < 0f ? 0f : alphaValue;
                Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, alphaValue);
                var chk = alphaValue <= 0f;
                //var chk1 = Text.color - _hiddenColor;
                if (chk && _textHiddenSeconds <= _textCurrentHidden)
                {
                    ShowNewText();
                }
                _textCurrentHidden += Time.deltaTime;
            }
        }
    }
}
