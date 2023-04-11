/*******************************************************************************************
*
*   raylib-extras [ImGui] example - Simple Integration
*
*	This is a simple ImGui Integration
*	It is done using C++ but with C style code
*	It can be done in C as well if you use the C ImGui wrapper
*	https://github.com/cimgui/cimgui
*
*   Copyright (c) 2021 Jeffery Myers
*
********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using ImGuiNET;
using Raylib_cs;

namespace rlImGui_cs
{
    public static class rlImGui
    {
        internal static IntPtr ImGuiContext = IntPtr.Zero;

        private static ImGuiMouseCursor CurrentMouseCursor = ImGuiMouseCursor.COUNT;
        private static Dictionary<ImGuiMouseCursor, MouseCursor> MouseCursorMap;
        private static KeyboardKey[] KeyEnumMap;
        private static ImGuiKey[] keys = {
            ImGuiKey.Tab, ImGuiKey.LeftArrow, ImGuiKey.RightArrow,
            ImGuiKey.UpArrow, ImGuiKey.DownArrow, ImGuiKey.PageUp,
            ImGuiKey.PageDown, ImGuiKey.Home,  ImGuiKey.End,
            ImGuiKey.Insert,  ImGuiKey.Delete, ImGuiKey.Backspace,
            ImGuiKey.Space, ImGuiKey.Enter, ImGuiKey.Escape,
            ImGuiKey.A, ImGuiKey.C, ImGuiKey.V,
            ImGuiKey.X, ImGuiKey.Y, ImGuiKey.Z,
        };

        private static Texture2D FontTexture;

        public static void Setup(bool darkTheme = true)
        {
            MouseCursorMap = new Dictionary<ImGuiMouseCursor, MouseCursor>();
            KeyEnumMap = Enum.GetValues(typeof(KeyboardKey)) as KeyboardKey[];

            FontTexture.id = 0;

            BeginInitImGui();

            if (darkTheme)
                ImGui.StyleColorsDark();
            else
                ImGui.StyleColorsLight();

            EndInitImGui();
        }

        public static void BeginInitImGui()
        {
            ImGuiContext = ImGui.CreateContext();
        }

        private static void SetupMouseCursors()
        {
            MouseCursorMap.Clear();
            MouseCursorMap[ImGuiMouseCursor.Arrow] = MouseCursor.MOUSE_CURSOR_ARROW;
            MouseCursorMap[ImGuiMouseCursor.TextInput] = MouseCursor.MOUSE_CURSOR_IBEAM;
            MouseCursorMap[ImGuiMouseCursor.Hand] = MouseCursor.MOUSE_CURSOR_POINTING_HAND;
            MouseCursorMap[ImGuiMouseCursor.ResizeAll] = MouseCursor.MOUSE_CURSOR_RESIZE_ALL;
            MouseCursorMap[ImGuiMouseCursor.ResizeEW] = MouseCursor.MOUSE_CURSOR_RESIZE_EW;
            MouseCursorMap[ImGuiMouseCursor.ResizeNESW] = MouseCursor.MOUSE_CURSOR_RESIZE_NESW;
            MouseCursorMap[ImGuiMouseCursor.ResizeNS] = MouseCursor.MOUSE_CURSOR_RESIZE_NS;
            MouseCursorMap[ImGuiMouseCursor.ResizeNWSE] = MouseCursor.MOUSE_CURSOR_RESIZE_NWSE;
            MouseCursorMap[ImGuiMouseCursor.NotAllowed] = MouseCursor.MOUSE_CURSOR_NOT_ALLOWED;
        }

        public static unsafe void ReloadFonts()
        {
            ImGui.SetCurrentContext(ImGuiContext);
            ImGuiIOPtr io = ImGui.GetIO();

            int width, height, bytesPerPixel;
            io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out width, out height, out bytesPerPixel);

            Image image = new Image
            {
                data = pixels,
                width = width,
                height = height,
                mipmaps = 1,
                format = PixelFormat.PIXELFORMAT_UNCOMPRESSED_R8G8B8A8,
            };

            FontTexture = Raylib.LoadTextureFromImage(image);

            io.Fonts.SetTexID(new IntPtr(FontTexture.id));
        }

        public static void EndInitImGui()
        {
            SetupMouseCursors();

            ImGui.SetCurrentContext(ImGuiContext);
            var fonts = ImGui.GetIO().Fonts;
            ImGui.GetIO().Fonts.AddFontDefault();

            ImGuiIOPtr io = ImGui.GetIO();
            foreach (ImGuiKey imGuiKey in keys)
            {
                io.KeyMap[(int)imGuiKey] = (int)MapImGuiKeyToKeyboardKey(imGuiKey);
            }
            ReloadFonts();
        }

        private static KeyboardKey MapImGuiKeyToKeyboardKey(ImGuiKey imGuiKey)
        {
            return imGuiKey switch
            {
                ImGuiKey.Tab => KeyboardKey.KEY_TAB,
                ImGuiKey.LeftArrow => KeyboardKey.KEY_LEFT,
                ImGuiKey.RightArrow => KeyboardKey.KEY_RIGHT,
                ImGuiKey.UpArrow => KeyboardKey.KEY_UP,
                ImGuiKey.DownArrow => KeyboardKey.KEY_DOWN,
                ImGuiKey.PageUp => KeyboardKey.KEY_PAGE_UP,
                ImGuiKey.PageDown => KeyboardKey.KEY_PAGE_DOWN,
                ImGuiKey.Home => KeyboardKey.KEY_HOME,
                ImGuiKey.End => KeyboardKey.KEY_END,
                ImGuiKey.Delete => KeyboardKey.KEY_DELETE,
                ImGuiKey.Backspace => KeyboardKey.KEY_BACKSPACE,
                ImGuiKey.Enter => KeyboardKey.KEY_ENTER,
                ImGuiKey.Escape => KeyboardKey.KEY_ESCAPE,
                ImGuiKey.Space => KeyboardKey.KEY_SPACE,
                ImGuiKey.Insert => KeyboardKey.KEY_INSERT,
                ImGuiKey.A => KeyboardKey.KEY_A,
                ImGuiKey.C => KeyboardKey.KEY_C,
                ImGuiKey.V => KeyboardKey.KEY_V,
                ImGuiKey.X => KeyboardKey.KEY_X,
                ImGuiKey.Y => KeyboardKey.KEY_Y,
                ImGuiKey.Z => KeyboardKey.KEY_Z,
                _ => throw new ArgumentException($"Invalid ImGuiKey value {imGuiKey}")
            };
        }

        private static void NewFrame()
        {
            ImGuiIOPtr io = ImGui.GetIO();

            if (Raylib.IsWindowFullscreen())
            {
                int monitor = Raylib.GetCurrentMonitor();
                io.DisplaySize = new Vector2(Raylib.GetMonitorWidth(monitor), Raylib.GetMonitorHeight(monitor));
            }
            else
            {
                io.DisplaySize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            }

            io.DisplayFramebufferScale = new Vector2(1, 1);
            io.DeltaTime = Raylib.GetFrameTime();

            NewFrameKeys(io);
            if (NewFrameMouse(io))
            {
                return;
            }
            NewFrameCursor(io);
        }
        /// <summary>
        /// sets the new frame mouse event
        /// </summary>
        /// <param name="io"></param>
        /// <returns></returns>
        private static bool NewFrameMouse(ImGuiIOPtr io)
        {
            if (io.WantSetMousePos)
            {
                Raylib.SetMousePosition((int)io.MousePos.X, (int)io.MousePos.Y);
            }
            else
            {
                io.MousePos = Raylib.GetMousePosition();
            }

            io.MouseDown[0] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON);
            io.MouseDown[1] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_RIGHT_BUTTON);
            io.MouseDown[2] = Raylib.IsMouseButtonDown(MouseButton.MOUSE_MIDDLE_BUTTON);

            if (Raylib.GetMouseWheelMove() > 0)
                io.MouseWheel += 1;
            else if (Raylib.GetMouseWheelMove() < 0)
                io.MouseWheel -= 1;

            return (io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) != 0;
        }
        /// <summary>
        /// On New Frame Keys
        /// </summary>
        /// <param name="io"></param>
        private static void NewFrameKeys(ImGuiIOPtr io)
        {
            io.KeyCtrl = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_CONTROL) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL);
            io.KeyShift = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SHIFT) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT);
            io.KeyAlt = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_ALT) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT);
            io.KeySuper = Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT_SUPER) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_SUPER);
        }

        private static void NewFrameCursor(ImGuiIOPtr io)
        {
            ImGuiMouseCursor imgui_cursor = ImGui.GetMouseCursor();
            if (imgui_cursor == CurrentMouseCursor && !io.MouseDrawCursor)
            {
                return;
            }

            CurrentMouseCursor = imgui_cursor;
            if (!io.MouseDrawCursor && imgui_cursor != ImGuiMouseCursor.None)
            {
                Raylib.ShowCursor();

                if ((io.ConfigFlags & ImGuiConfigFlags.NoMouseCursorChange) == 0)
                {
                    Raylib.SetMouseCursor(!MouseCursorMap.ContainsKey(imgui_cursor)
                        ? MouseCursor.MOUSE_CURSOR_DEFAULT
                        : MouseCursorMap[imgui_cursor]);
                }
            }
            else
            {
                Raylib.HideCursor();
            }
        }


        private static void FrameEvents()
        {
            ImGuiIOPtr io = ImGui.GetIO();

            foreach (KeyboardKey key in KeyEnumMap)
            {
                io.KeysDown[(int)key] = Raylib.IsKeyDown(key);
            }

            uint pressed = (uint)Raylib.GetCharPressed();
            while (pressed != 0)
            {
                io.AddInputCharacter(pressed);
                pressed = (uint)Raylib.GetCharPressed();
            }
        }

        public static void Begin()
        {
            ImGui.SetCurrentContext(ImGuiContext);

            NewFrame();
            FrameEvents();
            ImGui.NewFrame();
        }

        private static void EnableScissor(float x, float y, float width, float height)
        {
            Rlgl.rlEnableScissorTest();
            Rlgl.rlScissor((int)x, Raylib.GetScreenHeight() - (int)(y + height), (int)width, (int)height);
        }

        private static void TriangleVert(ImDrawVertPtr idx_vert)
        {
            Vector4 color = ImGui.ColorConvertU32ToFloat4(idx_vert.col);

            Rlgl.rlColor4f(color.X, color.Y, color.Z, color.W);
            Rlgl.rlTexCoord2f(idx_vert.uv.X, idx_vert.uv.Y);
            Rlgl.rlVertex2f(idx_vert.pos.X, idx_vert.pos.Y);
        }

        private static void RenderTriangles(uint count, uint indexStart, ImVector<ushort> indexBuffer, ImPtrVector<ImDrawVertPtr> vertBuffer, IntPtr texturePtr)
        {
            if (count < 3)
                return;

            uint textureId = 0;
            if (texturePtr != IntPtr.Zero)
                textureId = (uint)texturePtr.ToInt32();

            Rlgl.rlBegin(DrawMode.TRIANGLES);
            Rlgl.rlSetTexture(textureId);

            for (int i = 0; i <= (count - 3); i += 3)
            {
                if (Rlgl.rlCheckRenderBatchLimit(3))
                {
                    Rlgl.rlBegin(DrawMode.TRIANGLES);
                    Rlgl.rlSetTexture(textureId);
                }

                ushort indexA = indexBuffer[(int)indexStart + i];
                ushort indexB = indexBuffer[(int)indexStart + i + 1];
                ushort indexC = indexBuffer[(int)indexStart + i + 2];

                ImDrawVertPtr vertexA = vertBuffer[indexA];
                ImDrawVertPtr vertexB = vertBuffer[indexB];
                ImDrawVertPtr vertexC = vertBuffer[indexC];

                TriangleVert(vertexA);
                TriangleVert(vertexB);
                TriangleVert(vertexC);
            }
            Rlgl.rlEnd();
        }

        private delegate void Callback(ImDrawListPtr list, ImDrawCmdPtr cmd);

        private static void RenderData()
        {
            Rlgl.rlDrawRenderBatchActive();
            Rlgl.rlDisableBackfaceCulling();

            var data = ImGui.GetDrawData();

            for (int l = 0; l < data.CmdListsCount; l++)
            {
                ImDrawListPtr commandList = data.CmdListsRange[l];

                for (int cmdIndex = 0; cmdIndex < commandList.CmdBuffer.Size; cmdIndex++)
                {
                    var cmd = commandList.CmdBuffer[cmdIndex];

                    EnableScissor(cmd.ClipRect.X - data.DisplayPos.X, cmd.ClipRect.Y - data.DisplayPos.Y, cmd.ClipRect.Z - (cmd.ClipRect.X - data.DisplayPos.X), cmd.ClipRect.W - (cmd.ClipRect.Y - data.DisplayPos.Y));
                    if (cmd.UserCallback != IntPtr.Zero)
                    {
                        Callback cb = Marshal.GetDelegateForFunctionPointer<Callback>(cmd.UserCallback);
                        cb(commandList, cmd);
                        continue;
                    }

                    RenderTriangles(cmd.ElemCount, cmd.IdxOffset, commandList.IdxBuffer, commandList.VtxBuffer, cmd.TextureId);

                    Rlgl.rlDrawRenderBatchActive();
                }
            }
            Rlgl.rlSetTexture(0);
            Rlgl.rlDisableScissorTest();
            Rlgl.rlEnableBackfaceCulling();
        }

        public static void End()
        {
            ImGui.SetCurrentContext(ImGuiContext);
            ImGui.Render();
            RenderData();
        }

        public static void Shutdown()
        {
            Raylib.UnloadTexture(FontTexture);
            ImGui.DestroyContext();
        }

        public static void Image(Texture2D image)
        {
            ImGui.Image(new IntPtr(image.id), new Vector2(image.width, image.height));
        }

        public static void ImageSize(Texture2D image, int width, int height)
        {
            ImGui.Image(new IntPtr(image.id), new Vector2(width, height));
        }

        public static void ImageSize(Texture2D image, Vector2 size)
        {
            ImGui.Image(new IntPtr(image.id), size);
        }

        public static void ImageRect(Texture2D image, int destWidth, int destHeight, Rectangle sourceRect)
        {
            Vector2 uv0 = new Vector2();
            Vector2 uv1 = new Vector2();

            if (sourceRect.width < 0)
            {
                uv0.X = -((float)sourceRect.x / image.width);
                uv1.X = (uv0.X - (float)(Math.Abs(sourceRect.width) / image.width));
            }
            else
            {
                uv0.X = (float)sourceRect.x / image.width;
                uv1.X = uv0.X + (float)(sourceRect.width / image.width);
            }

            if (sourceRect.height < 0)
            {
                uv0.Y = -((float)sourceRect.y / image.height);
                uv1.Y = (uv0.Y - (float)(Math.Abs(sourceRect.height) / image.height));
            }
            else
            {
                uv0.Y = (float)sourceRect.y / image.height;
                uv1.Y = uv0.Y + (float)(sourceRect.height / image.height);
            }

            ImGui.Image(new IntPtr(image.id), new Vector2(destWidth, destHeight), uv0, uv1);
        }
    }
}
