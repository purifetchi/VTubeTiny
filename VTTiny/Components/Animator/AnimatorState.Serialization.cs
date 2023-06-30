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
            return (AnimatorStateConfig)new AnimatorStateConfig()
                .From(this);
        }
    }
}
