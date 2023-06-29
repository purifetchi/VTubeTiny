using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Raylib_cs;
using VTTiny.Base;
using VTTiny.Components;
using VTTiny.Editor;
using VTTiny.Extensions;

namespace VTTiny.Scenery
{
    /// <summary>
    /// An object in a stage, can contain components that modify what it does.
    /// </summary>
    public partial class StageActor : IEditorStageTreeNode
    {
        /// <summary>
        /// The stage that owns this actor.
        /// </summary>
        public Stage OwnerStage { get; init; }

        /// <summary>
        /// The parent actor of this stage actor.
        /// </summary>
        public StageActor ParentActor { get; private set; }

        /// <summary>
        /// The name of this actor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Allow/disable rendering of this component.
        /// </summary>
        public bool AllowRendering { get; set; } = true;

        /// <summary>
        /// Stop the actor from running its update method.
        /// </summary>
        public bool Paused { get; set; } = false;

        /// <summary>
        /// The transform component attached to this actor.
        /// </summary>
        public TransformComponent Transform { get; init; }

        /// <summary>
        /// The components of this actor.
        /// </summary>
        private readonly List<Component> _components;

        /// <summary>
        /// The renderables this component has.
        /// </summary>
        private readonly List<RendererComponent> _renderables;

        /// <summary>
        /// The child list of this actor.
        /// </summary>
        private List<StageActor> _children;

        /// <summary>
        /// Constructs a new stage actor.
        /// </summary>
        /// <param name="stage">The stage this actor is a part of.</param>
        /// <param name="name">The name of this actor.</param>
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
            if (Paused)
            {
                if (HasChildren)
                {
                    foreach (var child in _children)
                        child.Update();
                }

                Transform.Update();
                return;
            }

            foreach (var component in _components)
                component.Update();

            if (HasChildren)
            {
                foreach (var child in _children)
                    child.Update();
            }
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

            if (HasChildren)
            {
                foreach (var child in _children)
                    child.Render();
            }
        }

        /// <summary>
        /// Renders the bounding box of this actor (if one exists).
        /// </summary>
        internal void RenderBoundingBox()
        {
            if (_renderables.Count < 1 || !AllowRendering)
                return;

            foreach (var component in _renderables)
                component.DrawBoundingBox();

            if (HasChildren)
            {
                foreach (var child in _children)
                    child.RenderBoundingBox();
            }
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
            var component = type.Construct<Component>();
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
        /// Returns whether the actor contains a component of a type.
        /// </summary>
        /// <param name="type">The type of the component.</param>
        /// <returns>Whether the actor contains the component.</returns>
        internal bool ContainsComponent(Type type)
        {
            return _components.Any(component => component.GetType() == type);
        }

        /// <summary>
        /// Processes the component dependencies for a given component and instantiates them.
        /// </summary>
        /// <param name="component">The component to process the dependencies of.</param>
        private void ProcessComponentDependencyChain(Component component)
        {
            var deps = component.GetType()
                .GetCustomAttributes<DependsOnComponentAttribute>()
                .Select(attrib => attrib.ComponentType)
                .Where(type => !ContainsComponent(type));

            foreach (var dependency in deps)
            {
                ConstructComponentFromType(dependency);
                Console.WriteLine($"Resolved unsatisfied dependency {dependency.FullName} for component {component.GetType().FullName}.");
            }
        }

        /// <summary>
        /// Initializes a component.
        /// </summary>
        /// <param name="component">The component.</param>
        private void InitializeComponent(Component component)
        {
            ProcessComponentDependencyChain(component);

            component.SetParent(this);
            component.Start();

            if (component is RendererComponent renderable)
                _renderables.Add(renderable);

            _components.Add(component);
        }

        /// <summary>
        /// Get a component given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (or the interface it inherits).</typeparam>
        /// <returns>Either null if not found, or the component.</returns>
        public T GetComponent<T>()
        {
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                    return typedComponent;
            }

            return default;
        }

        /// <summary>
        /// Gets all the components given their type (or inherited interface).
        /// </summary>
        /// <typeparam name="T">The type of the component (or the interface it inherits).</typeparam>
        /// <returns>A list of all of the components.</returns>
        public T[] GetComponents<T>()
        {
            var components = new List<T>();
            foreach (var component in _components)
            {
                if (component is T typedComponent)
                    components.Add(typedComponent);
            }

            return components.ToArray();
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
        /// Adds an actor to the child list of this actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
        internal void AddChild(StageActor actor)
        {
            // Lazy initialize the child list, as we don't really need it if an actor has
            // no children.
            _children ??= new();

            if (_children.Contains(actor))
                return;

            _children.Add(actor);
        }

        /// <summary>
        /// Removes an actor from the child list of this actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
        internal void RemoveChild(StageActor actor)
        {
            _children ??= new();

            if (!_children.Contains(actor))
                return;

            _children.Remove(actor);
        }

        /// <summary>
        /// Tries reparenting an actor to a different actor.
        /// </summary>
        /// <param name="newParent">The new parent actor.</param>
        /// <param name="transformPosition">Should we transform our current position to the position relative to the parent?</param>
        /// <returns>True if reparenting succeeded, false otherwise.</returns>
        public bool TryReparent(StageActor newParent, bool transformPosition = false)
        {
            ParentActor?.RemoveChild(this);

            if (newParent == null)
            {
                Transform.LocalPosition = Transform.Position;
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
            ParentActor.AddChild(this);

            if (transformPosition)
                Transform.LocalPosition = Transform.Position - ParentActor.Transform.Position;

            return true;
        }

        /// <summary>
        /// Checks if the given position overlaps any of the renderables of this actor.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <returns>Whether the position overlapped any of the renderables.</returns>
        public bool HitTest(Vector2Int position)
        {
            if (!AllowRendering)
                return false;

            foreach (var renderable in _renderables)
            {
                if (Raylib.CheckCollisionPointRec(position, renderable.GetBoundingBox()))
                    return true;
            }

            return false;
        }
    }
}
