using VTTiny.Components.Animator.Data;

namespace VTTiny.Components.Animator
{
    public partial class AnimatorState
    {
        /// <summary>
        /// Packages the internal state into a config for serialization.
        /// </summary>
        /// <returns>The config.</returns>
        internal AnimatorStateConfig PackageIntoConfig()
        {
            return new AnimatorStateConfig
            {
                IsDefaultState = IsDefaultState,
                Key = Key,
                Name = Name,

                CharacterConfig = Character?.PackageIntoConfig()
            };
        }
    }
}
