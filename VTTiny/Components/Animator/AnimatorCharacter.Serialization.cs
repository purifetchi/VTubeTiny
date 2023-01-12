using VTTiny.Assets;
using VTTiny.Components.Animator.Data;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorCharacter
    {
        /// <summary>
        /// Packages the state of this animator character for serialization.
        /// </summary>
        /// <returns>The resulting config.</returns>
        internal AnimatorCharacterConfig PackageIntoConfig()
        {
            return new AnimatorCharacterConfig
            {
                BlinkEvery = BlinkEvery,
                BlinkLength = BlinkLength,

                Idle = Idle?.ToAssetReference<Texture>(),
                Blinking = IdleBlink?.ToAssetReference<Texture>(),
                Speaking = Speaking?.ToAssetReference<Texture>(),
                SpeakingBlinking = SpeakingBlink?.ToAssetReference<Texture>()
            };
        }
    }
}
