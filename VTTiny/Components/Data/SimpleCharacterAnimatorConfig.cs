using VTTiny.Assets;
using VTTiny.Assets.Management;

namespace VTTiny.Components.Data
{
    internal class SimpleCharacterAnimatorConfig
    {
        public AssetReference<Texture>? Idle { get; set; }
        public AssetReference<Texture>? Speaking { get; set; }
        public AssetReference<Texture>? Blinking { get; set; }

        public float BlinkEvery { get; set; } = 1.5f;
        public float BlinkLength { get; set; } = 0.1f;

        /// <summary>
        /// Load the states into an animator component.
        /// </summary>
        /// <param name="animator">The animator.</param>
        public void LoadStates(SimpleCharacterAnimatorComponent animator)
        {
            animator.SetTextureForState(Idle?.Resolve(animator.Parent.OwnerStage.AssetDatabase), SimpleCharacterAnimatorComponent.State.Idle);
            animator.SetTextureForState(Speaking?.Resolve(animator.Parent.OwnerStage.AssetDatabase), SimpleCharacterAnimatorComponent.State.Speaking);
            animator.SetTextureForState(Blinking?.Resolve(animator.Parent.OwnerStage.AssetDatabase), SimpleCharacterAnimatorComponent.State.Blinking);
        }
    }
}
