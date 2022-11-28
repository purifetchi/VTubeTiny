using System;
using System.Collections.Generic;
using ImGuiNET;
using VTTiny.Components;
using VTTiny.Data;
using VTTiny.Editor;

namespace VTTiny.Scenery
{
    public class StageActor
    {
        /// <summary>
        /// The stage that owns this actor.
        /// </summary>
        public Stage OwnerStage { get; private set; }

        /// <summary>
        /// The parent actor of this stage actor.
        /// </summary>
        public StageActor ParentActor { get; set; }

        /// <summary>
        /// The name of this actor.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The transform component attached to this actor.
        /// </summary>
        public TransformComponent Transform { get; private set; }

        private readonly List<Component> _components;

        public StageActor(Stage stage, string name)
        {
            OwnerStage = stage;
            Name = name;

            _components = new();

            // Create a transform, as all stage actors have one.
            Transform = AddComponent<TransformComponent>();
        }

        /// <summary>
        /// Build all the components from a component config.
        /// </summary>
        /// <param name="componentConfigs"></param>
        internal void BuildComponentsFromConfig(IList<ComponentConfig> componentConfigs)
        {
            if (componentConfigs == null)
                return;

            foreach (var componentConfig in componentConfigs)
            {
                // Get the type of the constructor, which .NET expects as {namespace}.{type}
                var type = Type.GetType($"{componentConfig.Namespace}.{componentConfig.Type}");
                if (type == null)
                {
                    Console.WriteLine($"Couldn't instantiate component {componentConfig.Namespace}.{componentConfig.Type} in actor {Name}.");
                    continue;
                }

                var component = ConstructComponentFromType(type);
                component.InheritParametersFromConfig(componentConfig.Parameters);
            }
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
            foreach (var component in _components)
                component.Render();
        }

        /// <summary>
        /// Renders this stage actor's editor GUI and all of the editor GUIs for the components within.
        /// </summary>
        internal void RenderEditorGUI()
        {
            if (ImGui.TreeNode($"{Name}"))
            {
                ImGui.Text($"Parented to: {ParentActor?.Name}");
                ImGui.Text("Components");

                foreach (var component in _components)
                {
                    if (ImGui.TreeNode($"{component.GetType().Name}"))
                    {
                        component.RenderEditorGUI();

                        ImGui.TreePop();
                    }
                }

                if (EditorGUI.ComponentDropdown(out Type componentType))
                    ConstructComponentFromType(componentType);

                ImGui.TreePop();
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
        public T AddComponent<T>() where T: Component, new()
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

            _components.Add(component);
        }

        /// <summary>
        /// Get a component given its type.
        /// </summary>
        /// <typeparam name="T">The type of the component (Must derive from VTTiny.Components.Component).</typeparam>
        /// <returns>Either null if not found, or the component.</returns>
        public T GetComponent<T>() where T: Component
        {
            foreach (var component in _components)
            {
                if (component.GetType() == typeof(T))
                    return (T)component;
            }

            return null;
        }
    }
}
