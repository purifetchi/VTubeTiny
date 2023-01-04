using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Editor;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorCharacter
    {
        /// <summary>
        /// Draw the editor gui for this character.
        /// </summary>
        /// <param name="assetDatabase">The asset database to resolve the textures from.</param>
        public void DrawEditorGUI(AssetDatabase assetDatabase)
        {
            BlinkEvery = EditorGUI.DragFloat("Blink every", BlinkEvery);
            BlinkLength = EditorGUI.DragFloat("Blink length", BlinkLength);

            if (EditorGUI.AssetDropdown("Idle", assetDatabase, Idle, out Texture newIdle))
                Idle = newIdle;

            if (EditorGUI.AssetDropdown("Idle blinking", assetDatabase, IdleBlink, out Texture newBlinking))
                IdleBlink = newBlinking;

            if (EditorGUI.AssetDropdown("Speaking", assetDatabase, Speaking, out Texture newSpeaking))
                Speaking = newSpeaking;

            if (EditorGUI.AssetDropdown("Speaking blinking", assetDatabase, SpeakingBlink, out Texture newSpeakingBlink))
                SpeakingBlink = newSpeakingBlink;
        }
    }
}
