namespace Assets.Code.MonoBehaviours.Configuration
{
    using Players;
    using UnityEngine;
    using UserInterface;
    using Audio;

    public class GlobalConfiguration : MonoBehaviour
    {
        [Header("Player")]
        public Player prefab_player;
        [Header("Voices")]
        public Voice prefab_voice1;
        public Voice prefab_voice2;
        public Voice prefab_voice3;
        public Voice prefab_god_cam;
        [Header("UI")]
        public CanvasManager ui_game_canvas_manager;
        public CanvasManager ui_game_over_canvas_manager;
        public CanvasManager ui_game_menu_canvas_manager;
        [Header("Game State Params")]
        public bool param_game_over;
        public bool param_game_started { get; set; }
        [Header("Player Params")]
        public Vector3 param_player_initial_position;
        public float param_player_jump_magnitude;
        public float param_player_jump_fade;
        public float param_player_move_magnitude;
        [Header("Trigger params")]
        public float param_trigger_freeze_seconds;
        [Header("Audio")]
        public AudioSystem audio_system;
        public AudioClipConfiguration audio_background_01;
    }
}
