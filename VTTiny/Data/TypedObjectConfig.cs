﻿using System;
using System.Text.Json;
using VTTiny.Plugins;

namespace VTTiny.Data
{
    /// <summary>
    /// Typed serialized object configuration data.
    /// </summary>
    public class TypedObjectConfig
    {
        public string Namespace { get; set; } = "VTTiny.Components";
        public string Type { get; set; }
        public string Name { get; set; }
        public JsonElement? Parameters { get; set; }

        /// <summary>
        /// Tries to resolve the type for this object.
        /// </summary>
        /// <typeparam name="T">The parent type.</typeparam>
        /// <param name="resolvedType">The resolved type if it exists.</param>
        /// <returns>Whether we could successfuly resolve this type.</returns>
        public bool TryResolveType<T>(out Type resolvedType)
        {
            resolvedType = null;

            var type = PluginManager.FindTypeInLoadedAssemblies($"{Namespace}.{Type}");
            if (type == null)
                return false;

            if (!type.IsSubclassOf(typeof(T)))
                return false;

            resolvedType = type;
            return true;
        }
    }
}
