using Raylib_cs;
using VTTiny.Assets.Management;

namespace VTTiny.Components.Animator.Data
{
    internal class AnimatorStateConfig
    {
        public string Name { get; set; }

        public bool IsDefaultState { get; set; } = false;

        public KeyboardKey Key { get; set; } = KeyboardKey.KEY_NULL;

        public AnimatorCharacterConfig CharacterConfig { get; set; }

        /// <summary>
        /// Constructs an animator state from the config.
        /// </summary>
        /// <param name="database">The database to resolve from.</param>
        /// <returns>The newly created animator state.</returns>
        public AnimatorState ToAnimatorState(AssetDatabase database)
        {
            return new AnimatorState
            {
                Key = Key,
                IsDefaultState = IsDefaultState,
                Name = Name,

                Character = CharacterConfig?.ToAnimatorCharacter(database)
            };
        }
    }
}
