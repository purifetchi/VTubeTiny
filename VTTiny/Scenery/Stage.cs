using System.Collections.Generic;
using System.Diagnostics;
using Raylib_cs;
using ImGuiNET;
using VTTiny.Data;

namespace VTTiny.Scenery
{
    public class Stage
    {
        /// <summary>
        /// The dimensions of this stage.
        /// </summary>
        public Vector2Int Dimensions { get; private set; }

        /// <summary>
        /// The background color of this stage.
        /// </summary>
        public Color ClearColor { get; private set; }

        /// <summary>
        /// The time in seconds ever since the scene became active.
        /// </summary>
        public double Time { get => _timer.ElapsedMilliseconds / 1000.0d; }

        /// <summary>
        /// The time between this and the last frame.
        /// </summary>
        public double DeltaTime { get => Time - _lastUpdateTime; }

        private List<StageActor> _actors;
        private Stopwatch _timer;

        private double _lastUpdateTime = 0;

        /// <summary>
        /// Creates a blank scene.
        /// </summary>
        /// <returns>A blank scene with default values.</returns>
        public static Stage Blank()
        {
            var timer = new Stopwatch();
            timer.Start();

            return new Stage 
            { 
                _actors = new(),
                _timer = timer,
                ClearColor = new Color(0, 255, 0, 255),
                Dimensions = new Vector2Int { X = 800, Y = 480 }
            };
        }

        /// <summary>
        /// Attaches a config to a scene.
        /// </summary>
        /// <param name="config">The config.</param>
        /// <returns>This scene.</returns>
        public Stage WithConfig(Config config)
        {
            if (config == null)
                return this;

            Dimensions = config.Dimensions;
            ClearColor = config.ClearColor;

            Raylib.SetWindowSize(Dimensions.X, Dimensions.Y);
            Raylib.SetTargetFPS(config.FPSLimit);

            CreateActorsFromConfigList(config.Actors);

            return this;
        }

        /// <summary>
        /// Creates actors from the actor config list.
        /// </summary>
        /// <param name="actorConfigs">The actor config list.</param>
        private void CreateActorsFromConfigList(IList<ActorConfig> actorConfigs)
        {
            if (actorConfigs == null)
                return;

            foreach (var config in actorConfigs)
            {
                var actor = CreateActor(config.Name);
                actor.Transform.LocalPosition = config.Position;

                // Try to set the parent of this actor
                if (!string.IsNullOrEmpty(config.ParentActorName))
                {
                    var parent = FindActor(config.ParentActorName);
                    if (parent == null)
                        System.Console.WriteLine($"Couldn't find parent {config.ParentActorName} for actor {actor.Name}!");

                    else
                        actor.ParentActor = parent;
                }

                actor.BuildComponentsFromConfig(config.Components);

                System.Console.WriteLine($"Actor {actor.Name} instantiated.");
            }
        }

        /// <summary>
        /// Add an actor to the stage.
        /// </summary>
        /// <param name="name">(OPTIONAL) The name of this actor. 'StageActor' by default.</param>
        /// <returns>The newly created actor.</returns>
        public StageActor CreateActor(string name = "StageActor")
        {
            var actor = new StageActor(this, name);
            _actors.Add(actor);

            return actor;
        }

        /// <summary>
        /// Try to find an actor by its name.
        /// </summary>
        /// <param name="name">The actor's name.</param>
        /// <returns>Either an actor or null.</returns>
        public StageActor FindActor(string name)
        {
            return _actors.Find(actor => actor.Name == name);
        }

        /// <summary>
        /// Update all the actors within this scene.
        /// </summary>
        internal void Update()
        {
            foreach (var actor in _actors)
                actor.Update();

            _lastUpdateTime = Time;
        }

        /// <summary>
        /// Render all the actors within this scene.
        /// </summary>
        internal void Render()
        {
            foreach (var actor in _actors)
                actor.Render();
        }

        /// <summary>
        /// Renders the editor GUI for this scene and all the actors within this scene.
        /// </summary>
        internal void RenderEditorGUI()
        {
            if (ImGui.CollapsingHeader("Stage"))
            {
                ImGui.Indent();

                ImGui.Text($"Scene Dimensions: {Dimensions}");
                ImGui.Text("Actors");
                
                foreach (var actor in _actors)
                {
                    actor.RenderEditorGUI();

                    ImGui.Separator();
                }
               
                if (ImGui.Button("Add Actor"))
                    CreateActor();

                ImGui.Unindent();
            }
        }

        /// <summary>
        /// Render all the actors within this scene.
        /// </summary>
        internal void Destroy()
        {
            foreach (var actor in _actors)
                actor.Destroy();

            _actors.Clear();
        }
    }
}
