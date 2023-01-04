using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Components.Animator;

namespace VTTiny.Components.Data
{
    internal class SimpleCharacterAnimatorConfig
    {
        public AssetReference<Texture>? Idle { get; set; }
        public AssetReference<Texture>? Speaking { get; set; }
        public AssetReference<Texture>? Blinking { get; set; }
        public AssetReference<Texture>? SpeakingBlinking { get; set; }

        public float BlinkEvery { get; set; } = 1.5f;
        public float BlinkLength { get; set; } = 0.1f;

        /// <summary>
        /// Constructs a new config from an existing character.
        /// 
        /// We do it this way to preserve backwards compatibility with earlier versions.
        /// </summary>
        /// <returns>A new animator config.</returns>
        /// <param name="character">The character.</param>
        public static SimpleCharacterAnimatorConfig FromCharacter(AnimatorCharacter character)
        {
            return new SimpleCharacterAnimatorConfig
            {
                BlinkEvery = character.BlinkEvery,
                BlinkLength = character.BlinkLength,

                Idle = character.Idle?.ToAssetReference<Texture>(),
                Blinking = character.IdleBlink?.ToAssetReference<Texture>(),

                Speaking = character.Speaking?.ToAssetReference<Texture>(),
                SpeakingBlinking = character.SpeakingBlink?.ToAssetReference<Texture>()
            };
        }

        /// <summary>
        /// Constructs an animator character from the configuration.
        /// </summary>
        /// <param name="assetDatabase">The database to resolve all the assets from.</param>
        public AnimatorCharacter ConstructCharacterPackage(AssetDatabase assetDatabase)
        {
            return new AnimatorCharacter
            {
                BlinkEvery = BlinkEvery,
                BlinkLength = BlinkLength,

                Idle = Idle?.Resolve(assetDatabase),
                IdleBlink = Blinking?.Resolve(assetDatabase),

                Speaking = Speaking?.Resolve(assetDatabase),
                SpeakingBlink = SpeakingBlinking?.Resolve(assetDatabase),
            };
        }
    }
}
