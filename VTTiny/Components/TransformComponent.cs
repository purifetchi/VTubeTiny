using VTTiny.Editor;

namespace VTTiny.Components
{
    public class TransformComponent : Component
    {
        /// <summary>
        /// The global position, affected by the local position and the parent transform's position.
        /// </summary>
        public Vector2Int Position { get; private set; }

        /// <summary>
        /// The local position (goes from [0, 0] in the top left of the screen).
        /// </summary>
        public Vector2Int LocalPosition { get; set; }

        public override void Update()
        {
            if (Parent.ParentActor == null)
            {
                Position = LocalPosition;
                return;
            }

            // If we have a parent, our position is the local position added to the parent transform position.
            Position = LocalPosition + Parent.ParentActor.Transform.Position;
        }

        internal override void RenderEditorGUI()
        {
            LocalPosition = EditorGUI.DragVector2("Local Position", LocalPosition);
        }
    }
}
