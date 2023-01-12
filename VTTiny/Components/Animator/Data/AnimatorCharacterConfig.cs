using VTTiny.Assets.Management;
using VTTiny.Assets;

namespace VTTiny.Components.Animator.Data
{
    internal class AnimatorCharacterConfig
    {
        public AssetReference<Texture>? Idle { get; set; }
        public AssetReference<Texture>? Speaking { get; set; }
        public AssetReference<Texture>? Blinking { get; set; }
        public AssetReference<Texture>? SpeakingBlinking { get; set; }

        public float BlinkEvery { get; set; } = 1.5f;
        public float BlinkLength { get; set; } = 0.1f;

        /// <summary>
        /// Constructs an animator character from its config.
        /// </summary>
        /// <param name="database">The database to resolve the texture assets from.</param>
        /// <returns>The newly created animator character.</returns>
        public AnimatorCharacter ToAnimatorCharacter(AssetDatabase database)
        {
            return new AnimatorCharacter
            {
                BlinkEvery = BlinkEvery,
                BlinkLength = BlinkLength,

                Idle = Idle?.Resolve(database),
                Speaking = Speaking?.Resolve(database),
                IdleBlink = Blinking?.Resolve(database),
                SpeakingBlink = SpeakingBlinking?.Resolve(database),
            };
        }
    }
}
