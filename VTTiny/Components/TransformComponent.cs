using System;
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
        /// The global rotation, affected by the local rotation and the parent transform's rotation.
        /// </summary>
        public float Rotation { get; private set; }

        /// <summary>
        /// The local position (goes from [0, 0] in the top left of the screen).
        /// </summary>
        public Vector2Int LocalPosition { get; set; }

        /// <summary>
        /// The local rotation, in degrees.
        /// </summary>
        public float LocalRotation { get; set; }

        /// <inheritdoc/>
        public override void Update()
        {
            if (Parent.ParentActor == null)
            {
                Position = LocalPosition;
                Rotation = LocalRotation;
                return;
            }

            const float DEG2RAD = MathF.PI / 180f;

            (var sin, var cos) = MathF.SinCos(Parent.ParentActor.Transform.Rotation * DEG2RAD);

            // Transform the point relative to the rotation of the parent.
            var localPos = new Vector2Int(
                (int)(LocalPosition.X * cos - LocalPosition.Y * sin),
                (int)(LocalPosition.Y * cos + LocalPosition.X * sin)
                );

            // If we have a parent, our position is the local position added to the parent transform position.
            Position = localPos + Parent.ParentActor.Transform.Position;
            Rotation = LocalRotation + Parent.ParentActor.Transform.Rotation;
        }

        /// <inheritdoc/>
        public override void RenderEditorGUI()
        {
            LocalPosition = EditorGUI.DragVector2("Local Position", LocalPosition);
            LocalRotation = EditorGUI.DragFloat("Local Rotation", LocalRotation, 0.1f);
        }
    }
}
