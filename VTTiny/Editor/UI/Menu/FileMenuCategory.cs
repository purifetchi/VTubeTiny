﻿using System;
using VTTiny.Editor.Native;
using VTTiny.Scenery;
using VTTiny.Serialization;

namespace VTTiny.Editor.UI
{
    /// <summary>
    /// The file category for a menu.
    /// </summary>
    internal class FileMenuCategory : MenuCategory
    {
        /// <summary>
        /// Creates a new file menu category.
        /// </summary>
        /// <param name="menuBar">The menu bar we should be attached to.</param>
        public FileMenuCategory(MenuBar menuBar)
            : base("File", menuBar)
        {

        }

        protected override void InitializeBaseActions()
        {
            AddAction("New Stage", editor =>
            {
                var stage = Stage.Blank(editor.VTubeTiny);
                editor.VTubeTiny.SetActiveStage(stage);
                editor.ForEachWindowOfType<IStageAwareWindow>(window => window.OnStageChange(editor.VTubeTiny.ActiveStage));
            });

            AddAction("Load Stage", editor =>
            {
                var path = FileDialog.OpenFile();
                if (string.IsNullOrEmpty(path))
                    return;

                editor.VTubeTiny.LoadConfigFromFile(path);
                editor.VTubeTiny.ReloadStage();

                editor.ForEachWindowOfType<IStageAwareWindow>(window => window.OnStageChange(editor.VTubeTiny.ActiveStage));
            }, true);

            AddAction("Save Stage", editor =>
            {
                var path = FileDialog.SaveFile();
                if (string.IsNullOrEmpty(path))
                    return;

                JsonSerializationHelper.ExportStageToFile(path, editor.VTubeTiny.ActiveStage);
            });

            AddAction("Quit", editor =>
            {
                Environment.Exit(0);
            }, true);
        }
    }
}
