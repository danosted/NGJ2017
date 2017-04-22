namespace Assets.Code.MonoBehaviours.Configuration
{
    using Obstacles;
    using UnityEngine;
    using UserInterface;
    using Audio;

    public class GlobalConfiguration : MonoBehaviour
    {
        [Header("Moving Objects")]
        public MoveableObject prefab_moveable_object;
        [Header("UI")]
        public CanvasManager ui_game_canvas_manager;
        public CanvasManager ui_game_over_canvas_manager;
        public CanvasManager ui_game_menu_canvas_manager;
        [Header("Game Params")]
        public bool param_game_over;
        public bool param_game_started { get; set; }
        [Header("Audio")]
        public AudioSystem audio_system;
        public AudioClipConfiguration audio_background_01;
    }
}
