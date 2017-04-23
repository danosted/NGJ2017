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
        public bool param_god_view_enabled;
        [Header("UI")]
        public CanvasManager ui_next_level_canvas_manager;
        public CanvasManager ui_game_over_canvas_manager;
        public CanvasManager ui_game_completed_canvas_manager;
        public CanvasManager ui_game_end_canvas_manager;
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
        public AudioClip audio_jump;
        public AudioClip audio_wall_bump;
        public AudioClip audio_walk_cycle;
        public AudioClip audio_end_of_game;
        public AudioClip audio_pit_fall;
        [Header("Transition View")]
        public TransitionView prefab_transition_view;
        public CanvasManager ui_transtition_canvas;
        [Header("Floating Text")]
        public float param_floating_text_linger_seconds;
        public float param_floating_text_fade_speed;
        public float param_floating_text_hidden_seconds;
        [Header("Scenes")]
        public string param_end_game_scene_name;
        public float param_game_completed_fade_linger_seconds;
    }
}
