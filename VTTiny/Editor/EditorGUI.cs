﻿using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using ImGuiNET;
using Raylib_cs;
using VTTiny.Assets;
using VTTiny.Assets.Management;
using VTTiny.Audio;
using VTTiny.Base;
using VTTiny.Components;
using VTTiny.Extensions;
using VTTiny.Plugins;
using VTTiny.Scripting.Nodes;

namespace VTTiny.Editor
{
    /// <summary>
    /// Various helpers for drawing the editor GUI.
    /// </summary>
    public static class EditorGUI
    {
        /// <summary>
        /// This serves as a cache for the components, which we can then use in combo boxes for
        /// component instantiation.
        /// </summary>
        private static string[] ComponentTypeCache { get; set; } = null;

        /// <summary>
        /// This serves as a cache for all of the keycode names that Raylib can use.
        /// </summary>
        private static string[] KeyNameCache { get; set; } = null;

        /// <summary>
        /// This serves as a cache for all of the node types.
        /// </summary>
        private static string[] NodeCache { get; set; } = null;

        /// <summary>
        /// This serves as a cache for all of the keycodes that Raylib can use.
        /// </summary>
        private static KeyboardKey[] KeyCache { get; set; } = null;

        /// <summary>
        /// Stores the current component index for the combo box.
        /// </summary>
        private static int _comboBoxComponentIndex = 0;

        static EditorGUI()
        {
            CollectComponents();
            CollectKeyCodes();
            CollectNodes();
        }

        /// <summary>
        /// Collects all of the keycodes into the cache.
        /// </summary>
        private static void CollectKeyCodes()
        {
            KeyCache = Enum.GetValues<KeyboardKey>()
                           .ToArray();

            KeyNameCache = KeyCache.Select(key => key.ToString())
                                   .ToArray();
        }

        /// <summary>
        /// Collects all of the types that derive from Component and caches them for later use
        /// in the component combo box.
        /// 
        /// TODO: This is insanely ugly, and won't work for external plugin components. I wish
        /// we didn't have to rely on reflection in this way.
        /// </summary>
        private static void CollectComponents()
        {
            var components = PluginManager.GetTypesInLoadedAssemblies()
                                     .Where(type => type.IsSubclassOf(typeof(Component)) && !type.IsAbstract && type.IsClass)
                                     .Select(type => type.FullName)
                                     .ToArray();

            ComponentTypeCache = components;
        }

        /// <summary>
        /// Collects all of the types derived from the Node type, so we can later instantiate them.
        /// </summary>
        private static void CollectNodes()
        {
            var nodes = PluginManager.GetTypesInLoadedAssemblies()
                .Where(type => type.IsSubclassOf(typeof(Node)) && !type.IsAbstract && type.IsClass)
                .Select(type => type.FullName)
                .ToArray();

            NodeCache = nodes;
        }

        /// <summary>
        /// Constructs a draggable gui component for a given vector value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="vector">The beginning vector.</param>
        /// <returns>The updated vector value.</returns>
        public static Vector2Int DragVector2(string label, Vector2Int vector)
        {
            var copy = (Vector2)vector;
            ImGui.DragFloat2(label, ref copy, 1f);

            return copy;
        }

        /// <summary>
        /// Constructs a text input gui component for a given string value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The string value.</param>
        /// <returns>The modified string value.</returns>
        public static string InputText(string label, string value)
        {
            ImGui.InputText(label, ref value, 1024);
            return value;
        }

        /// <summary>
        /// Constructs a draggable int gui component for a given integer value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The integer value.</param>
        /// <returns>The modified integer value.</returns>
        public static int DragInt(string label, int value)
        {
            ImGui.DragInt(label, ref value);
            return value;
        }

        /// <summary>
        /// Constructs a draggable float gui component for a given float value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The float value.</param>
        /// <param name="step">The step value (what it should increment by).</param>
        /// <returns>The modified float value.</returns>
        public static float DragFloat(string label, float value, float step = 1f)
        {
            ImGui.DragFloat(label, ref value, step);
            return value;
        }

        /// <summary>
        /// Constructs a checkbox with the given value as a starting value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="value">The bool value.</param>
        /// <returns>The modified bool value.</returns>
        public static bool Checkbox(string label, bool value)
        {
            ImGui.Checkbox(label, ref value);
            return value;
        }

        /// <summary>
        /// Constructs a color editor gui component for a given color value.
        /// </summary>
        /// <param name="label">The label to attach to the gui component.</param>
        /// <param name="color">The color value.</param>
        /// <returns>The modified color value.</returns>
        public static Color ColorEdit(string label, Color color)
        {
            var vec = color.ToVector4();
            ImGui.ColorEdit4(label, ref vec);
            return vec.ToColor();
        }

        /// <summary>
        /// Creates an image button.
        /// </summary>
        /// <param name="texture">The texture to be used as the image.</param>
        /// <param name="x">The desired x size.</param>
        /// <param name="y">The desired y size.</param>
        /// <returns>Whether it was pressed or not.</returns>
        public static bool ImageButton(Texture texture, int x, int y)
        {
            if (texture == null)
                return ImGui.Button("No Image", new Vector2(x, y));
            return ImGui.ImageButton(new IntPtr(texture.TextureId), new Vector2(x, y));
        }

        /// <summary>
        /// Creates a text label.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void Text(string text)
        {
            ImGui.Text(text);
        }

        /// <summary>
        /// Creates a component list.
        /// </summary>
        /// <param name="nodeType">The type of the node, if it was added.</param>
        /// <returns>Whether any of the nodes were attempted to be instantiated.</returns>
        public static bool NodeList(out Type nodeType)
        {
            nodeType = default;

            foreach (var typeName in NodeCache)
            {
                if (ImGui.MenuItem(typeName))
                {
                    nodeType = PluginManager.FindTypeInLoadedAssemblies(typeName);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a component dropdown with an add button.
        /// </summary>
        /// <param name="componentType">The type of the component (if it was added).</param>
        /// <returns>True if the add button was clicked.</returns>
        public static bool ComponentDropdown(out Type componentType)
        {
            ImGui.Combo(" ", ref _comboBoxComponentIndex, ComponentTypeCache, ComponentTypeCache.Length);
            ImGui.SameLine();

            if (ImGui.Button("Add Component"))
            {
                componentType = PluginManager.FindTypeInLoadedAssemblies(ComponentTypeCache[_comboBoxComponentIndex]);
                return true;
            }

            componentType = default;
            
            return false;
        }

        /// <summary>
        /// Creates an actor dropdown.
        /// </summary>
        /// <param name="stage">The stage to iterate the actors of.</param>
        /// <param name="currentSelectedActor">The currently selected actor.</param>
        /// <param name="newSelectedActor">The newly selected actor (if it was changed).</param>
        /// <returns>True if we've switched the actor.</returns>
        public static bool ActorDropdown(Scenery.Stage stage, Scenery.StageActor currentSelectedActor, out Scenery.StageActor newSelectedActor)
        {
            newSelectedActor = null;

            if (!ImGui.BeginCombo(" ##ActorDropdown", $"{currentSelectedActor?.Name ?? "No actor selected."}"))
                return false;

            if (ImGui.Selectable("None", currentSelectedActor == null))
            {
                ImGui.EndCombo();
                return true;
            }

            foreach (var actor in stage.GetActors())
            {
                if (!ImGui.Selectable(actor.Name, actor == currentSelectedActor))
                    continue;
                
                ImGui.EndCombo();
                newSelectedActor = actor;
                return true;
            }

            ImGui.EndCombo();
            return false;
        }

        /// <summary>
        /// Shows a dropdown for all assets of a type.
        /// </summary>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <param name="label">The label to display.</param>
        /// <param name="database">The asset database.</param>
        /// <param name="currentAsset">The current asset.</param>
        /// <param name="newAsset">The new asset, if selected.</param>
        /// <returns>Whether we've selected a new asset.</returns>
        public static bool AssetDropdown<T>(string label, AssetDatabase database, T currentAsset, out T newAsset) 
            where T : Asset
        {
            newAsset = default;

            currentAsset?.RenderAssetPreview();

            var state = ImGui.BeginCombo($"{label}##AssetDropdown", $"{currentAsset?.Name ?? "No asset selected."}");

            // Handle this dropdown as a drag'n'drop target.
            if (ImGui.BeginDragDropTarget())
            {
                var payload = ImGui.AcceptDragDropPayload("Asset");
                var id = -1;

                unsafe
                {
                    // If the payload actually has data, read the id.
                    if ((nint)payload.NativePtr != 0)
                        id = Unsafe.Read<int>((void*)payload.Data);
                }

                ImGui.EndDragDropTarget();

                if (id != -1)
                {
                    newAsset = database.GetAsset<T>(id);
                    return true;
                }
            }

            if (!state)
                return false;

            if (ImGui.Selectable("None", currentAsset == null))
            {
                ImGui.EndCombo();
                return true;
            }

            foreach (var asset in database.GetAllAssetsOfType<T>())
            {
                if (!ImGui.Selectable(asset.Name ?? asset.Id.ToString(), asset == currentAsset))
                    continue;

                ImGui.EndCombo();

                newAsset = asset;
                return true;
            }

            ImGui.EndCombo();

            return false;
        }

        /// <summary>
        /// Creates a keycode dropdown gui element.
        /// </summary>
        /// <param name="key">The previous key.</param>
        /// <returns>The new key.</returns>
        public static KeyboardKey KeycodeDropdown(KeyboardKey key)
        {
            // Cast the current key into the index for the cache.
            var index = Array.IndexOf(KeyCache, key);
            ImGui.Combo(" ", ref index, KeyNameCache, KeyNameCache.Length);

            return KeyCache[index];
        }

        /// <summary>
        /// Shows a text input window for renaming.
        /// </summary>
        /// <param name="namedObject">The object to rename.</param>
        /// <param name="vtubetiny">The VTubeTiny instance for which it's invoked.</param>
        public static void ShowRenameWindow(INamedObject namedObject, VTubeTiny vtubetiny)
        {
            ShowTextInputWindow($"Renaming '{namedObject.Name}'.", namedObject.Name, name => namedObject.Name = name, vtubetiny);
        }

        /// <summary>
        /// Shows a text input window.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="initial">The initial string.</param>
        /// <param name="callback">The callback.</param>
        /// <param name="vtubetiny">The VTubeTiny instance for which it's invoked.</param>
        public static void ShowTextInputWindow(string title, string initial, Action<string> callback, VTubeTiny vtubetiny)
        {
            vtubetiny.Editor.AddWindow(new UI.TextInputWindow(title, initial, callback));
        }

        /// <summary>
        /// Shows a message box.
        /// </summary>
        /// <param name="title">The title of the message box.</param>
        /// <param name="message">The message within this message box.</param>
        /// <param name="vtubetiny">The VTubeTiny instance for which it's invoked.</param>
        public static void MessageBox(string title, string message, VTubeTiny vtubetiny)
        {
            vtubetiny.Editor.AddWindow(new UI.MessageBoxWindow(title, message));
        }

        /// <summary>
        /// Shows a progress bar that changes the color based on a threshold.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="threshold">The threshold.</param>
        /// <param name="max">The maximum value.</param>
        public static void ReactiveProgressBar(float value, float threshold, float max)
        {
            // If the value exceeds the threshold, change the progress bar to be green.
            ImGui.PushStyleColor(ImGuiCol.PlotHistogram,
                // Otherwise make it red.
                value >= threshold ? new Vector4(0, 255, 0, 255) : new Vector4(255, 0, 0, 255));

            var size = new Vector2Int(300, 20); // The size of the progress bar was way too big on 4k
            ImGui.ProgressBar(value / max, size);

            ImGui.PopStyleColor();
        }

        /// <summary>
        /// Shows a dropdown for the listenable devices.
        /// </summary>
        /// <param name="label">The label for the dropdown.</param>
        /// <param name="currentDevice">The current listenable device.</param>
        /// <param name="newDevice">The picked listenable device.</param>
        /// <returns>Whether we've switched the selection.</returns>
        public static bool ListenableDeviceDropdown(string label, IListenableDevice currentDevice, out IListenableDevice newDevice)
        {
            newDevice = default;

            if (!ImGui.BeginCombo($"{label}##MicrophoneDropdown",
                    $"{currentDevice?.Name ?? "No microphone selected."}"))
            {
                return false;
            }

            if (ImGui.Selectable("None", currentDevice == null))
            {
                ImGui.EndCombo();
                return true;
            }

            foreach (var device in ListenableDeviceHelper.GetAllListenableDevices())
            {
                if (!ImGui.Selectable(device.Name, device == currentDevice))
                    continue;

                ImGui.EndCombo();

                newDevice = device;
                return true;
            }

            ImGui.EndCombo();

            return false;
        }
    }
}
