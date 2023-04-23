using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Editor;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorCharacter : IEditableGUI
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
            
            if (EditorGUI.AssetDropdown("Speaking blinking", assetDatabase, SpeakingBlink, out Texture newSpeakingBlink))
                SpeakingBlink = newSpeakingBlink;
            
            //Continuously add Speaking textures

            Speaking ??= new List<Texture>()
            {
                new Texture()
            };

            if (Speaking.Count() >= 0)
            {
                if (Speaking.Any() && Speaking.Last() != default)
                {
                    var i = Speaking.Last();
                    if (i.Id != default)
                    {
                        //Add button to add new speaking texture
                        Speaking.Add(new Texture());
                        
                    }
                }

                if (!Speaking.Any())
                {
                    Speaking.Add(new() );
                }
                
                for (int i = 0; i < Speaking.Count(); i++)
                {
                    if (EditorGUI.AssetDropdown("Speaking " + i, assetDatabase, Speaking[i], out Texture newSpeaking))
                        Speaking[i] = newSpeaking;
                }
            }
            else
            {
                if (!Speaking.Any())
                {
                    //Make Temporary Speaking texture
                    Speaking.Add(new Texture());
                }
                if (EditorGUI.AssetDropdown("Speaking", assetDatabase, Speaking[0] , out Texture newSpeaking))
                    Speaking[0] = newSpeaking;
            }
        }
    }
}
