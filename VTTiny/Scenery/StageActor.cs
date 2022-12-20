using System;
using System.Collections.Generic;
using Raylib_cs;
using VTTiny.Components;

namespace VTTiny.Scenery
{
    /// <summary>
    /// An object in a stage, can contain components that modify what it does.
    /// </summary>
    public partial class StageActor
    {
        /// <summary>
        /// The stage that owns this actor.
        /// </summary>
        public Stage OwnerStage { get; private set; }

        /// <summary>
        /// The parent actor of this stage actor.
        /// </summary>
        public StageActor ParentActor { get; private set; }

        /// <summary>
        /// The name of this actor.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Allow/disable rendering of this component.
        /// </summary>
        public bool AllowRendering { get; set; } = true;

        /// <summary>
        /// The transform component attached to this actor.
        /// </summary>
        public TransformComponent Transform { get; private set; }

        private readonly List<Component> _components;
        private readonly List<RendererComponent> _renderables;

        public StageActor(Stage stage, string name)
        {
            OwnerStage = stage;
            Name = name;

            _components = new();
            _renderables = new();

            // Create a transform, as all stage actors have one.
            Transform = AddComponent<TransformComponent>();
        }

        /// <summary>
        /// Update all the components of this stage actor.
        /// </summary>
        internal void Update()
        {
            foreach (var component in _components)
                component.Update();
        }

        /// <summary>
        /// Render this stage actor.
        /// </summary>
        internal void Render()
        {
            if (_renderables.Count < 1 || !AllowRendering)
                return;

            foreach (var component in _renderables)
                component.Render();
        }

        /// <summary>
        /// Renders the bounding box of this actor (if one exists).
        /// </summary>
        internal void RenderBoundingBox()
        {
            if (_renderables.Count < 1)
                return;

            foreach (var component in _renderables)
                component.DrawBoundingBox();
        }

        /// <summary>
        /// Destroy this stage actor.
        /// </summary>
        internal void Destroy()
        {
            foreach (var component in _components)
                component.Destroy();

            _components.Clear();
            _renderables.Clear();
        }

        /// <summary>
        /// Constructs a component from a given type.
        /// </summary>
        /// <param name="type">The type to construct</param>
        /// <returns>The component.</returns>
        private Component ConstructComponentFromType(Type type)
        {
            // Get the constructor of the component from the type and instantiate the component.
            var ctor = type.GetConstructor(Array.Empty<Type>());
            var component = (Component)ctor.Invoke(Array.Empty<object>());
            InitializeComponent(component);

            return component;
        }

        /// <summary>
        /// Add a new component onto the components list given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (Must derive from VTTiny.Components.Component).</typeparam>
        /// <returns>The new component.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            var component = new T();
            InitializeComponent(component);

            return component;
        }

        /// <summary>
        /// Initializes a component.
        /// </summary>
        /// <param name="component">The component.</param>
        internal void InitializeComponent(Component component)
        {
            component.SetParent(this);
            component.Start();

            if (component is RendererComponent renderable)
                _renderables.Add(renderable);
            _components.Add(component);
        }

        /// <summary>
        /// Get a component given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (Must derive from VTTiny.Components.Component).</typeparam>
        /// <returns>Either null if not found, or the component.</returns>
        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                    return (T)component;
            }

            return null;
        }

        /// <summary>
        /// Removes a component from an actor.
        /// </summary>
        /// <param name="component">The component to remove.</param>
        /// <returns>True if the component was removed, false otherwise.</returns>
        public bool RemoveComponent(Component component)
        {
            // Every actor MUST have a transform.
            if (component == Transform)
                return false;

            if (!_components.Contains(component))
                return false;

            component.Destroy();

            if (component is RendererComponent renderable)
                _renderables.Remove(renderable);
            return _components.Remove(component);
        }

        /// <summary>
        /// Tries reparenting an actor to a different actor.
        /// </summary>
        /// <param name="newParent">The new parent actor.</param>
        /// <returns>True if reparenting succeeded, false otherwise.</returns>
        public bool TryReparent(StageActor newParent)
        {
            if (newParent == null)
            {
                ParentActor = null;
                return true;
            }

            // We need to walk the chain of the parents of this actor, to ensure that
            // we aren't causing a cyclic reparent somewhere down the line.
            var actor = newParent;
            do
            {
                if (actor == this)
                    return false;

                actor = actor.ParentActor;
            } while (actor != null);

            ParentActor = newParent;
            return true;
        }

        /// <summary>
        /// Checks if the given position overlaps any of the renderables of this actor.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Whether the position overlapped any of the renderables.</returns>
        public bool HitTest(Vector2Int position)
        {
            foreach (var renderable in _renderables)
            {
                if (Raylib.CheckCollisionPointRec(position, renderable.GetBoundingBox()))
                    return true;
            }

            return false;
        }
    }
}
