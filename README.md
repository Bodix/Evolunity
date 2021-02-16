# 🌿 Evolunity

![Unity version](https://img.shields.io/badge/unity-2018.4%2B-blue?logo=unity)
[![License](https://img.shields.io/badge/license-CC%20BY--ND%204.0-green)](#license)

Well-designed package with useful scripting tools for Unity development.

## Content

### Extension methods

- System types:
  - `char`
  - `string`
  - `byte[]`
  - `IDictionary`
  - `IEnumerable`
  - `IComparable`

- Unity types:
  - `Animator`
  - `Color`
  - `Graphic`
  - `LayerMask`
  - `Quaternion`
  - `Rect`
  - `Renderer`
  - `Transform`
  - `Vector`

### Utilities

- `StaticCoroutine` - Static coroutine.
- `Delay` - Utility for calling functions with a delay. Based on `StaticCoroutine`.
- `Performance` - Utility for measuring functions performance.
- `BinarySerializer` - Utility for serializing objects.
- `StringEncryptor` - Utility for encrypting strings.
- `Enum` - Utility for parsing and working with enums.
- `Angle` - Utility for working with angles.
- `RegexPatterns` - Set of default regular expression patterns.

### Unity components

- `PeriodicBehaviour` - Calls the given function periodically.
- `Spawner` - Spawns objects one-time or periodically. Based on `PeriodicBehaviour`.
- `InputReader` - Reads click, drag and zoom (cross-platform).
- `LongPressReader` - Reads long press (cross-platform).
- `FPSCounter` - Counts FPS and outputs it to the `Text` component.
- `Comment` - Contains a comment to the GameObject.
- `DevelopmentOnly` - Destroys/disable the object if the *DEVELOPMENT* define is not set in the project settings.
- `PlatformDependent` - Destroys/disable the object if the platform specified in it does not match the current one.
- `DontDestroyOnLoad` - Makes GameObject persistent.
- `SingletonBehaviour` - Singleton `MonoBehaviour`.

### Editor

- `UnityConstantsGenerator` - Tool for generating static classes with tags, layers, scenes, and input axes.
- `CameraScreenshot` - Tool for taking screenshot from the main camera.
- `MenuItems` - Useful menu items.
- `Config` - Editor window with different project settings (e.g., target frame rate).
- `LayerDrawer` - Property drawer for `LayerAttribute` that shows a popup with layers (not mask).
- `Define` - Defines management.
- `EditorConsole` - Utility for working with the Editor console.
- `OpenInFileManager` - Utility to open the given path in the file manager.

### Structs

- `Direction` - Direction given by vector.
- `FloatRange` - Range given by two floats.
- `IntRange` - Range given by two ints.

### Other

- `StateMachine` - Immutable state machine without using strings, enums or reflections.
- `WeightQueue` - Queue filled with elements in which the number of each element is determined by its weight.

## Dependencies

- [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)

## Warning

Evolunity may receive breaking changes, so be sure to make a backup before updating the package.

## Install

- **Unity 2019.3 and above:**

  Use the following URL in the **Package Manager**:
  `https://github.com/Bodix/Evolunity.git`

  [Manual](https://docs.unity3d.com/2019.3/Documentation/Manual/upm-ui-giturl.html)

- **Before Unity 2019.3:**

  Open `{ProjectFolder}/Packages/manifest.json` and add the following line:

    ```json
    {
      "dependencies":
      {
        "com.evolutex.evolunity": "https://github.com/Bodix/Evolunity.git",
        ...
      }
    }
    ```

## Requirements

- Unity 2018.4+<br>
  *(You can try the lower version, but I haven't tested that)*

- Git<br>
  *(Must be added to the **PATH** environment variable)*

## License

[**CC BY-ND 4.0**](https://creativecommons.org/licenses/by-nd/4.0/)

1. **You can use this package in commercial projects.**

2. You can modify or extend this package only for your own use but you can't distribute the modified version.
    >**Note:** You can submit a pull request to this repository and if your change is useful, I'll be sure to add it!

3. You must indicate the author.
    >**Note:** You don't need to take any action on this point, because all attributions are written at the head of the scripts!
