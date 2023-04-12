using System.Collections.Generic;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Components.Animator.Data;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorCharacter : IPackageAble
    {
        /// <summary>
        /// Packages the state of this animator character for serialization.
        /// </summary>
        /// <returns>The resulting config.</returns>
        public AnimatorCharacterConfig PackageIntoConfig()
        {
            List<AssetReference<Texture>?>? speakingList = new List<AssetReference<Texture>?>();
            if (Speaking != null)
            {
                foreach (var speaking in Speaking)
                {
                    speakingList.Add(speaking.ToAssetReference<Texture>());
                }
            }

            return new AnimatorCharacterConfig
            {
                BlinkEvery = BlinkEvery,
                BlinkLength = BlinkLength,

                Idle = Idle?.ToAssetReference<Texture>(),
                Blinking = IdleBlink?.ToAssetReference<Texture>(),
                Speaking = speakingList,
                SpeakingBlinking = SpeakingBlink?.ToAssetReference<Texture>()
            };
        }
    }
}
