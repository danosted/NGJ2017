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

        private float _blackScreenLingerSeconds;
        private float _imageCurrentLinger;
        private float _imageFadeMagnitude;
        private Color _hiddenColor;

        public override void Activate(IoC container)
        {
            base.Activate(container);

            Image = GetComponent<Image>();
            _blackScreenLingerSeconds = Configuration.param_game_completed_fade_linger_seconds;
            _imageFadeMagnitude = Configuration.param_fade_to_black_speed;
            _hiddenColor = Color.black;
            _hiddenColor.a = 0;
            ShowNewText();
        }

        void ShowNewText()
        {
            Image.color = _hiddenColor;
            _imageCurrentLinger = 0f;
        }

        void Update()
        {
            if(_blackScreenLingerSeconds <= _imageCurrentLinger)
            {
                Container.Resolve<FlowLogic>().GameOver();
                return;
            }
            var alphaValue = Image.color.a + _imageFadeMagnitude * Time.deltaTime;
            alphaValue = alphaValue > 255 ? 255 : alphaValue;
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alphaValue);
            _imageCurrentLinger += Time.deltaTime;
        }
    }
}
