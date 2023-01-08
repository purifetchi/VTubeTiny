# VTubeTiny
VTubeTiny is a small VTuber/PNGTuber suite (written in C# and utilizing Raylib) for easily and quickly animating png models.

**VTubeTiny stage view**

![Demo](/Meta/vttiny.gif)

**VTubeTiny editor**

![Editor Demo](/Meta/editor.gif)

## Requirements

* .NET 6
* Windows 7+ (64-bit)
* A GPU that supports OpenGL ES 3.

## Usage

All of the VTubeTiny data is described in a JSON config file, from which VTubeTiny generates the **Stage**. Every **Stage** is split into **Actors**, which can have **Components** attached to them. Components serve multiple purposes, from rendering textures or text, to animating characters based on the microphone levels.

Every stage also has an **Asset Database** attached to it, that stores all of the **Assets** it uses. Assets are anything loaded from disk that can be then used by Components. (For now, the only Asset type that's properly supported are textures, but more will be added as more components are added in.)

Assets help reduce the amount of loaded data, as common assets can be shared between many components.

This is how a sample VTubeTiny configuration file looks like:

```json
{
	"Dimensions": {
		"X": 800,
		"Y": 480
	},
	"ClearColor": {
		"R": 0, 
		"G": 255, 
		"B": 0, 
		"A": 255
	},

	"Actors": [
		{
			"Name": "Text",
			"Position": {
				"X": 0,
				"Y": 0
			},

			"Components": [
				{
					"Type": "TextRendererComponent",
					"Parameters": {
						"Text": "Hello from VTubeTiny!"
					}
				}
			]
		}
	]
}
```

After having generated a config file, you can launch VTubeTiny's **Stage Viewer** mode by providing it the path to your config file, like so:

```
VTubeTiny.exe -s -f config.json
```

The **-s** parameters instructs VTubeTiny to launch into the slimmed-down **Stage Viewer**, which only processes the stage and skips the overhead of the Editor.

## Editor

When you launch VTubeTiny without any parameters, it launches into a fully built-in **Stage Editor** mode!

It allows for full stage editing, exporting, actor editing and more! It completely erases the need to construct configuration files by themselves.


## Extensibility

VTubeTiny can be extended with custom components. All that's neccessary is deriving from the *Component* class, and implementing any of the component functions. (implementing InheritParametersFromConfig is required for loading from the config file.)

A sample component can look something like this:

```cs
using Raylib_cs;

namespace VTTiny.Components
{
    public class RectangleRendererComponent : RendererComponent
    {
    	// This is called every frame, after the Update() call.
        public override void Render()
        {
            // Parent is the owning actor.
            // Every actor also has a transform component auto attached to them.
            Raylib.DrawRectangle(Parent.Transform.Position.X, Parent.Transform.Position.Y, 10, 10, Color.RED);
        }
    }
}
```

Attaching this component to an actor will draw a red 10x10 square at the position of the actor. All of the currently existing components can be viewed in the *Components* subdirectory.

Components can also be extended with the ability to be modified at runtime inside of the VTubeTiny editor. To implement that, a component must override the **RenderEditorGUI** function.

For example, if we'd want the rectangle's dimensions to be editable, one could change the definition to look more like:

```cs
using Raylib_cs;
using VTTiny.Editor;

namespace VTTiny.Components
{
    public class RectangleRendererComponent : RendererComponent
    {
        public Vector2Int Dimensions { get; set; }

    	// This is called every frame, after the Update() call.
        public override void Render()
        {
            // Parent is the owning actor.
            // Every actor also has a transform component auto attached to them.
            Raylib.DrawRectangle(Parent.Transform.Position.X, Parent.Transform.Position.Y, Dimensions.X, Dimensions.Y, Color.RED);
        }

        internal override void RenderEditorGUI()
        {
            Dimensions = EditorGUI.DragVector2("Dimensions", Dimensions);
        }
    }
}
```

Now, if we were to attach this component to an actor and enter editor mode, the component's dimensions will be fully editable!

## Future Goals

* Much better and tidier editor UI.
* Proper cross-platform support.
* More components!
* Editor localization?

## Contributing Guide

If you'd really want to contribute to this project (thank you!) please adhere to the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) commit format as much as you can. 