using Raylib_cs;

namespace VTTiny.Components.Data
{
    internal class SimpleCharacterAnimatorConfig
    {
        public string Idle { get; set; }
        public string Speaking { get; set; }
        public string Blinking { get; set; }

        public float BlinkEvery { get; set; } = 1.5f;
        public float BlinkLength { get; set; } = 0.1f;

        /// <summary>
        /// Load the states into an animator component.
        /// </summary>
        /// <param name="animator">The animator.</param>
        public void LoadStates(SimpleCharacterAnimatorComponent animator)
        {
            if (!string.IsNullOrEmpty(Idle))
                animator.SetTextureForState(new Texture(Idle), SimpleCharacterAnimatorComponent.State.Idle);

            if (!string.IsNullOrEmpty(Speaking))
                animator.SetTextureForState(new Texture(Speaking), SimpleCharacterAnimatorComponent.State.Speaking);

            if (!string.IsNullOrEmpty(Blinking))
                animator.SetTextureForState(new Texture(Blinking), SimpleCharacterAnimatorComponent.State.Blinking);
        }
    }
}
