using Newtonsoft.Json.Linq;
using Raylib_cs;
using VTTiny.Components.Data;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class KeyToggleableComponent : Component
    {
        /// <summary>
        /// The key that will toggle the rendering of this actor.
        /// </summary>
        public KeyboardKey ToggleKey { get; set; } = KeyboardKey.KEY_NULL;

        public override void Update()
        {
            if (Raylib.IsKeyPressed(ToggleKey))
                Parent.AllowRendering = !Parent.AllowRendering;
        }

        internal override void InheritParametersFromConfig(JObject parameters)
        {
            var config = JsonObjectToConfig<KeyToggleableConfig>(parameters);
            ToggleKey = config.ToggleKey;
        }

        protected override object PackageParametersIntoConfig()
        {
            return new KeyToggleableConfig
            {
                ToggleKey = ToggleKey
            };
        }

        internal override void RenderEditorGUI()
        {
            ToggleKey = EditorGUI.KeycodeDropdown(ToggleKey);
        }
    }
}
