using System.Collections.Generic;
using System.Diagnostics;
using Raylib_cs;
using VTTiny.Assets.Management;
using VTTiny.Data;
using VTTiny.Rendering;

namespace VTTiny.Scenery
{
    /// <summary>
    /// The class responsible for managing and drawing all of the actors.
    /// </summary>
    public partial class Stage
    {
        /// <summary>
        /// The asset database for this stage.
        /// </summary>
        public AssetDatabase AssetDatabase { get; private set; }

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

        /// <summary>
        /// The target framerate this stage runs at.
        /// </summary>
        public int TargetFPS { get; private set; }

        /// <summary>
        /// Should the scene render the bounding boxes of its actors?
        /// </summary>
        public bool RenderBoundingBoxes { get; private set; } = false;

        /// <summary>
        /// Should this stage be broadcasted via Spout? (WINDOWS ONLY)
        /// </summary>
        public bool BroadcastViaSpout { get; private set; } = false;

        /// <summary>
        /// The instance of VTubeTiny this scene is tied to.
        /// </summary>
        internal VTubeTiny VTubeTiny { get; private set; } = null;

        /// <summary>
        /// The rendering context for this scene.
        /// </summary>
        public IRenderingContext RenderingContext { get; private set; }

        private List<StageActor> _actors;
        private Stopwatch _timer;

        private double _lastUpdateTime = 0;

        /// <summary>
        /// The default stage constructor.
        /// </summary>
        private Stage()
        {
            SetTargetFPS(TargetFPS);

            _timer = new Stopwatch();
            _timer.Start();
        }

        /// <summary>
        /// Creates a blank scene.
        /// </summary>
        /// <returns>A blank scene with default values.</returns>
        public static Stage Blank(VTubeTiny vtubetiny)
        {
            var refreshRate = Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor());
            return new Stage
            {
                _actors = new(),
                ClearColor = new(0, 255, 0, 255),
                Dimensions = new(800, 400),
                RenderingContext = new GenericRaylibRenderingContext(),
                TargetFPS = refreshRate,
                VTubeTiny = vtubetiny,
                BroadcastViaSpout = false,
                AssetDatabase = new()
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

            AssetDatabase.LoadConfig(config);

            ResizeStage(config.Dimensions);

            ClearColor = config.ClearColor;
            SetTargetFPS(config.FPSLimit);

            SetSpoutOrDefaultContext<GenericRaylibRenderingContext>(config.BroadcastViaSpout);

            CreateActorsFromConfigList(config.Actors);

            return this;
        }

        /// <summary>
        /// Enables spout and sets the rendering context or constructs a fallback rendering context specified by T.
        /// </summary>
        /// <param name="enabled">Whether it should be enabled</param>
        /// <typeparam name="T">The rendering context to set when disabling spout.</typeparam>
        public void SetSpoutOrDefaultContext<T>(bool enabled) where T: IRenderingContext, new()
        {
            if (BroadcastViaSpout == enabled)
                return;

            BroadcastViaSpout = enabled;

            if (BroadcastViaSpout)
                ReplaceRenderingContext(new SpoutFramebufferRenderingContext());
            else
                ReplaceRenderingContext(new T());
        }

        /// <summary>
        /// Set the target FPS of this stage.
        /// </summary>
        /// <param name="fps">The frames per second value.</param>
        public void SetTargetFPS(int fps)
        {
            TargetFPS = fps;
            Raylib.SetTargetFPS(TargetFPS);
        }

        /// <summary>
        /// Replaces the current active rendering context.
        /// </summary>
        /// <param name="context">The new rendering context.</param>
        internal void ReplaceRenderingContext(IRenderingContext context)
        {
            RenderingContext?.Dispose();
            RenderingContext = context;

            ResizeStage(Dimensions);
        }

        /// <summary>
        /// Resizes the stage.
        /// </summary>
        /// <param name="dimensions">The new dimensions</param>
        private void ResizeStage(Vector2Int dimensions)
        {
            Dimensions = dimensions;
            RenderingContext.Resize(dimensions);
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
        /// Returns an enumerable containing all of the actors.
        /// </summary>
        /// <returns>An enumerable containing all actors.</returns>
        public IEnumerable<StageActor> GetActors()
        {
            return _actors;
        }

        /// <summary>
        /// Removes an actor from the stage.
        /// </summary>
        /// <param name="actor">The StageActor to be removed.</param>
        /// <returns>Whether removing the actor was successful.</returns>
        public bool RemoveActor(StageActor actor)
        {
            actor.Destroy();

            // Check if any other actor is parented to this one. If so, unlink the parent.
            foreach (var stageActor in _actors)
            {
                if (stageActor.ParentActor != actor)
                    continue;

                stageActor.TryReparent(null);
            }

            return _actors.Remove(actor);
        }

        /// <summary>
        /// Tries to get the actor that overlaps with the given position.
        /// </summary>
        /// <param name="position">The position to test with.</param>
        /// <returns>Either the first actor that overlapped with the position, or null.</returns>
        public StageActor HitTest(Vector2Int position)
        {
            for (var i = _actors.Count - 1; i >= 0; i--)
            {
                var actor = _actors[i];
                if (actor.HitTest(position))
                    return actor;
            }

            return null;
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
            RenderingContext.Begin(ClearColor);

            foreach (var actor in _actors)
            {
                actor.Render();

                if (RenderBoundingBoxes)
                    actor.RenderBoundingBox();
            }

            RenderingContext.End();
        }

        /// <summary>
        /// Render all the actors within this scene.
        /// </summary>
        internal void Destroy()
        {
            foreach (var actor in _actors)
                actor.Destroy();

            _actors.Clear();

            AssetDatabase.Destroy();
            AssetDatabase = null;

            RenderingContext.Dispose();
        }
    }
}
