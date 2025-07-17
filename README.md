# 🌿 Evolunity

![Unity version](https://img.shields.io/badge/unity-2019.3%2B-blue?logo=unity)
[![License](https://img.shields.io/badge/license-CC%20BY--ND%204.0-green)](#license)

Well-designed package with useful scripting tools for Unity development.

## Cheatsheet

### Coroutines

```csharp
// Calls the function in the next frame.
Delay.ForOneFrame(() => Debug.Log("Hello in the next frame"));
// Calls the function after a N of seconds.
Delay.ForSeconds(3, () => Debug.Log("Hello after three seconds"));
// Calls the function after a N of frames.
Delay.ForFrames(300, () => Debug.Log("Hello after three hundred frames"));

// Calls the function periodically every N seconds.
Repeat.EverySeconds(1, () => Debug.Log("Hello every second"));
// Calls the function periodically every N frames.
Repeat.EveryFrames(10, () => Debug.Log("Hello every ten frames"));
// Calls the function periodically every frame.
// Analogous to "Update", but you can use it not only from MonoBehaviour classes.
Repeat.EveryFrame(() => Debug.Log("Hello every frame"));

// Starts a static coroutine. You can use this outside of MonoBehaviour.
StaticCoroutine.Start(SomeCoroutine());

// You can cache a coroutine instance and stop it at any time.
Coroutine delayCoroutine = Delay.ForSeconds(60, () => Debug.Log("Delay coroutine"));
Coroutine repeatCoroutine = Repeat.EverySeconds(60, () => Debug.Log("Repeat coroutine"));
Coroutine staticCoroutine = StaticCoroutine.Start(SomeCoroutine());
// To stop a cached coroutine instance use StaticCoroutine.Stop method.
// See the description of the StaticCoroutine.Stop method for details.
StaticCoroutine.Stop(delayCoroutine);
StaticCoroutine.Stop(repeatCoroutine);
StaticCoroutine.Stop(staticCoroutine);

// You can specify the MonoBehaviour instance on which to execute the coroutine.
ExampleBehaviour exampleBehaviour = GetComponent<ExampleBehaviour>();
Coroutine delayCoroutine2 = Delay.ForSeconds(60, () => Debug.Log("Delay coroutine"), exampleBehaviour);
Coroutine repeatCoroutine2 = Repeat.EverySeconds(60, () => Debug.Log("Repeat coroutine"), this);
// In this case, you can stop the coroutine as usual.
exampleBehaviour.StopCoroutine(delayCoroutine2);
this.StopCoroutine(repeatCoroutine2);
```

### IEnumerable Extensions

```csharp
GameObject[] objects =
{
    new GameObject("Cube"),
    new GameObject("Sphere"),
    new GameObject("Cone")
};

// Output the array to the console.
// Output: Cone (UnityEngine.GameObject), Sphere (UnityEngine.GameObject), Cube (UnityEngine.GameObject)
Debug.Log(objects.AsString());
// Output the array to the console by specifying the string selector and separator.
// Output: Cone : Sphere : Cube
Debug.Log(objects.AsString(item => item.name, " : "));

// Get random object from the array.
GameObject randomObj = objects.Random();

// Shuffle the array.
objects = objects.Shuffle().ToArray();

// Remove duplicates from the array.
objects = objects.RemoveDuplicates().ToArray();

// ForEach as extension method.
objects.ForEach(Debug.Log);
objects.ForEach((x, index) => Debug.Log(index + " : " + x.name + ", "));
// ForEach as extension method with lazy execution.
objects.ForEachLazy(Debug.Log);
objects.ForEachLazy((x, index) => Debug.Log(index + " : " + x.name + ", "));
```

> Cheatsheet still WIP

## Content

### Utilities

- `StaticCoroutine` - Static coroutine.
- `Delay` - Utility for calling functions with a delay. Based on `StaticCoroutine`.
- `Screenshot` - Utility for quick and easy screenshots.
- `Performance` - Utility for measuring functions performance.
- `BinarySerializer` - Utility for serializing objects.
- `StringEncryptor` - Utility for encrypting strings.
- `Enum` - Utility for parsing and working with enums.
- `Angle` - Utility for working with angles.
- `MathUtilities` - Math utilities.
- `RegexPatterns` - Set of default regular expression patterns.
- `Validate` - Utility for validating various things.
- `WrappedCoroutine` - Coroutine, which contains useful data and functions for the job.

### Unity components

- `PeriodicBehaviour` - Calls the given function periodically.
- `Spawner` - Spawns objects one-time or periodically. Based on `PeriodicBehaviour`.
- `InputReader` - Reads click, drag and zoom (cross-platform).
- `LongPressReader` - Reads long press (cross-platform).
- `GifImage` - Plays an array of sprites like a gif.
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

- `Singleton` - [POCO](https://ru.wikipedia.org/wiki/Plain_old_CLR_object) singleton.
- `StateMachine` - Immutable state machine without using strings, enums or reflections.
- `WeightQueue` - Queue filled with elements in which the number of each element is determined by its weight.

### Extension methods

- System types:
  - `T[]`
  - `byte[]`
  - `char`
  - `IComparable`
  - `IDictionary`
  - `IEnumerable`
  - `string`

- Unity types:
  - `Animator`
  - `Color`
  - `Graphic`
  - `LayerMask`
  - `MonoBehaviour`
  - `Object`
  - `Quaternion`
  - `Rect`
  - `RectTransform`
  - `Renderer`
  - `Texture`
  - `ToggleGroup`
  - `Transform`
  - `UnityWebRequest`
  - `Vector`

## Dependencies

- [NaughtyAttributes](https://github.com/dbrizov/NaughtyAttributes)
- [Unity Interface Support](https://github.com/TheDudeFromCI/Unity-Interface-Support)

## Warning

Evolunity may receive breaking changes, so be sure to make a backup before updating the package.

## Install

Use the following URL in the **Package Manager**:

`https://github.com/Bodix/Evolunity.git`

  [Manual](https://docs.unity3d.com/2019.3/Documentation/Manual/upm-ui-giturl.html)

## Requirements

1. Unity 2019.3+

2. Git
  *(Must be added to the **PATH** environment variable)*

## License

[**CC BY-ND 4.0**](https://creativecommons.org/licenses/by-nd/4.0/)

1. **You can use this package in commercial projects.**

2. You can modify or extend this package only for your own use but you can't distribute the modified version.
    >**Note:** You can submit a pull request to this repository and if your change is useful, I'll be sure to add it!

3. You must indicate the author.
    >**Note:** You don't need to take any action on this point, because all attributions are written at the head of the scripts!
