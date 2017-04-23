namespace Assets.Code.MonoBehaviours.UserInterface
{
    using IoC;
    using Common;
    using UnityEngine;
    using UnityEngine.UI;
    using Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine.SceneManagement;
    using Assets.Code.GameLogic;

    [RequireComponent(typeof(Image))]
    public class GameOverFade : PrefabBase
    {

        private Image Image { get; set; }

        private float _textLingerSeconds;
        private float _textCurrentLinger;
        private float _textFadeMagnitude;
        private Color _hiddenColor;

        public override void Activate(IoC container)
        {
            base.Activate(container);

            Image = GetComponent<Image>();
            _textLingerSeconds = Configuration.param_game_completed_fade_linger_seconds;
            _textFadeMagnitude = Configuration.param_floating_text_fade_speed;
            _hiddenColor = Color.black;
            _hiddenColor.a = 0;
            ShowNewText();
        }

        void ShowNewText()
        {
            Image.color = _hiddenColor;
            _textCurrentLinger = 0f;
        }

        void Update()
        {
            if(_textLingerSeconds <= _textCurrentLinger)
            {
                Container.Resolve<FlowLogic>().GameOver();
                return;
            }
            var alphaValue = Image.color.a + _textFadeMagnitude * Time.deltaTime;
            alphaValue = alphaValue > 255 ? 255 : alphaValue;
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alphaValue);
            _textCurrentLinger += Time.deltaTime;
        }
    }
}
