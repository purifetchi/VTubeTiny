﻿using System;
using Raylib_cs;
using VTTiny.Data;
using VTTiny.Editor;
using VTTiny.Scenery;

namespace VTTiny
{
    /// <summary>
    /// The VTubeTiny runner.
    /// </summary>
    public class VTubeTiny
    {
        /// <summary>
        /// The configuration of this VTubeTiny instance.
        /// </summary>
        public Config Config { get; private set; }

        /// <summary>
        /// The currently active stage.
        /// </summary>
        public Stage ActiveStage { get; private set; }

        /// <summary>
        /// The VTubeTiny editor instance.
        /// </summary>
        private VTubeTinyEditor Editor { get; set; }

        /// <summary>
        /// Instantiates a new instance of VTubeTiny.
        /// </summary>
        /// <param name="config">The stage configuration data.</param>
        /// <param name="verbose">Should the console output be verbose? (Show Raylib specific messages)</param>
        /// <param name="withEditor">Should we also instantiate an instance of the editor?</param>
        public VTubeTiny(Config config, bool verbose = false, bool withEditor = false)
        {
            Config = config;
            SetVerbosity(verbose);

            if (withEditor)
                CreateEditor();
        }

        /// <summary>
        /// Creates a new editor instance for this VTubeTiny instance.
        /// </summary>
        private void CreateEditor()
        {
            Editor = new VTubeTinyEditor(this);
        }

        /// <summary>
        /// Set a new stage.
        /// </summary>
        /// <param name="stage">The stage to set.</param>
        public void SetActiveStage(Stage stage)
        {
            if (ActiveStage != null)
                ActiveStage.Destroy();

            ActiveStage = stage;
        }

        /// <summary>
        /// Set whether we should show Raylib output.
        /// </summary>
        /// <param name="verbose">Whether we should show the Raylib output.</param>

        public void SetVerbosity(bool verbose)
        {
            if (!verbose)
                Raylib.SetTraceLogLevel(TraceLogLevel.LOG_FATAL);
            else
                Raylib.SetTraceLogLevel(TraceLogLevel.LOG_INFO);
        }

        /// <summary>
        /// Starts this instance of VTubeTiny.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("VTubeTiny initializing.");

            Raylib.InitWindow(800, 600, "VTubeTiny");
            Raylib.SetTargetFPS(60);

            var stage = Stage.Blank()
                             .WithConfig(Config);

            ActiveStage = stage;

            Editor?.Initialize();

            RenderLoop();
        }

        private void RenderLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                ActiveStage.Update();
                ActiveStage.Render();
                Editor?.Render();
            }

            ActiveStage.Destroy();
            Editor?.Destroy();

            Raylib.CloseWindow();

            Console.WriteLine("Destroyed Raylib context, goodbye.");
        }
    }
}
