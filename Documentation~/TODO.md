# TODO (internal)

## Update the README Content block

The `Content` block in `README.md` is out of date. Below is a draft of a new block, made from the current code. Check each item against the code and move it to `README.md` by hand.

- [ ] Check each item of the draft and move it to `README.md`.
- [ ] Fix the old names in `README.md`:
    - `MathUtilities` → `MathUtility`
    - `RegexPatterns` → `RegexPattern`
    - `OpenInFileManager` → `FileExplorer`
    - `FPSCounter` → `FpsCounter` (marked `[Obsolete]` in the code)
    - `Spawner` → `BaseSpawner<T>` and four spawners based on it
    - `Texture` in the extension methods list → `Texture2D`

## Draft of the new Content block

### Coroutines

- `StaticCoroutine` - Starts and stops coroutines outside of `MonoBehaviour`.
- `Delay` - Calls a function after N frames or N seconds.
- `Repeat` - Calls a function every frame, every N frames or every N seconds.
- `WrappedCoroutine` - Coroutine that knows its owner and whether it is running. Start it with `StartWrappedCoroutine`.

### Utilities

- `Screenshot` - Takes a screenshot from a camera. Saves it to a file or passes it to a callback. Settings: `ScreenshotSettings`.
- `Performance` - Measures how long a function takes to run.
- `BinarySerializer` - Saves objects to bytes or binary files and loads them back.
- `StringEncryptor` - Encrypts and decrypts strings with a pass phrase.
- `Enum<T>` - Parses enums. Returns all values, their count or a random value.
- `MathUtility` - Points on a circle, a sphere and a phyllotaxis spiral. Angle normalization.
- `RegexPattern` - Common regular expressions: URL, email, hex color, IP address, HTML tag, text in brackets.
- `Validate` - Checks internet reachability, URLs and IP addresses.
- `DevLogs` - `Debug.Log` that works only in the Editor and in development builds.
- `GameObjectFinder` - Finds objects by name, including inactive ones, or by an indexed path.
- `ScreenSize` - Screen size in world units.
- `AspectRatio` - Common aspect ratios: widest phones, tablets, Full HD.
- `PathUtility` - Changes file names in paths and fixes slashes.
- `Animation` - Coroutines that fade a 3D object in or out through its materials.
- `AndroidNativeHelper` - Returns the height of the on-screen keyboard on Android.
- `AssetDatabaseHelper` - Finds the first asset of a given type. Works only in the Editor.
- `Condition` - Delegate that returns `bool`, with null-safe checks.
- `GizmosExtend` - Extra gizmos: points, arrows, circles, cones, capsules, camera frustums and more.
- `GizmosCustom` - Draws a `Rect` as a gizmo.
- `GizmosColorScope` - Sets `Gizmos.color` inside a `using` block and restores it after.

### Components

Most components are in the **Add Component → Evolunity** menu.

#### General

- `PeriodicBehaviour` - Calls a function every N seconds. Base class for spawners and sensors.
- `DelayedEvent` - Invokes a `UnityEvent` after a delay in frames or seconds.
- `Initializer` - Calls `Initialize()` on the given `IInitializable` objects in `Awake` or `Start`.
- `Lifetime` - Destroys the GameObject after a set time. Override `Die()` to use a pool instead.
- `DevelopmentOnly` - Destroys or disables the GameObject if the *DEVELOPMENT* define is not set.
- `PlatformDependent` - Destroys or disables the GameObject on platforms that are not selected.
- `DontDestroyOnLoad` - Keeps the GameObject when a new scene loads.
- `DisableOnAwake` - Disables the GameObject on `Awake`.
- `FrameRateConfig` - Sets the target frame rate and the VSync count.
- `InternetChecker` - Pings a URL. Reports when the internet connection is lost or restored.
- `MainThreadDispatcher` - Runs actions from other threads on the main thread.
- `MultiSourceActivationTracker` - Keeps the GameObject active while at least one component requests it.
- `SingletonBehaviour<T>` - Singleton `MonoBehaviour`.
- `TextureScroller` - Scrolls a material texture.
- `RectBorders` - Creates four `BoxCollider2D` borders around a `RectTransform` or the camera view.
- `LevelBordersRect` - Four level corners that move to enclose the given objects. Useful with Cinemachine Target Group.
- `OrientationChangeListener` - Fires an event when the screen turns between portrait and landscape.
- `OrientationChangeFov` - Changes the camera field of view when the orientation changes.

#### Animations

- `AnimationBehaviour` - Base class for coroutine animations. Implements `IAnimation` from Perfect Foundation.
- `TwoAnimationBehaviours` - Pair of show and hide animations. Implements `IShowHideAnimations` from Perfect Foundation.
- `InOutBehaviour` - Base class for animations with "in" and "out" parts.
- `Fade` - Fades an `Image` in and out.
- `AnimationEventsHandler` - Turns animation events into C# events.
- `RetargetRootMotion` - Applies root motion to a `CharacterController`, a `Rigidbody`, a `Transform` or your own `IRootMotionReceiver`.

#### Input

- `InputReader` - Reads click, drag, two-finger drag and zoom on a UI element (cross-platform).
- `LongPressReader` - Reads long press on a UI element (cross-platform).

#### Look At

- `LookAt` - Turns the object to a target every frame.
- `LookAtCamera` - Turns the object to the main camera every frame.

#### Physics

- `BoxOverlap` - Finds colliders inside a box.
- `ConicalOverlap` - Finds colliders inside a cone. Uses `BoxOverlap`.
- `BoxTriggerEvents` - Trigger events of a `BoxCollider` as C# events.
- `Projectile` - `Rigidbody` projectile with hit detection, effects and lifetime.
- `TrajectoryRenderer` - Draws a predicted flight path with a `LineRenderer`.

#### Scenes

- `SceneTransition` - Plays an "in" animation, then loads a scene.
- `SceneWatcher` - Remembers the previous and the current scene.

#### Sensors

All sensors are based on `PeriodicBehaviour`.

- `RangeSensor<T>` - Finds targets in range. Reports when a target comes in or goes out.
- `DirectionSensor<T>` - Tracks the direction to a target.
- `RaycastSensor` - Checks if a ray hits something.

#### Spawners

All spawners are based on `BaseSpawner<T>`. They spawn once, periodically or every frame. They can check the spawn point with a raycast or a sphere.

- `SimpleSpawner` - Spawns at its own position.
- `CircleZoneSpawner` - Spawns at random points inside a circle.
- `RadiusSpawner` - Spawns at random points between two radii around an origin.
- `SpawnPointsSpawner` - Spawns at the given points in random order.

#### Transform

- `TransformCopier` - Copies position, rotation and scale from a target without parenting. You can pick the axes.
- `GlobalScaleConstraint` - Keeps a fixed global scale whatever the parent scale is.
- `ScreenFitter` - Scales the object to the screen size in world units.
- `SetParentOnAwake` - Sets the parent on `Awake`.
- `UnparentOnAwake` - Removes the parent on `Awake`.

#### Triggers

- `Trigger` - Calls an `ITriggerable` when an object with the allowed tag enters the collider.
- `ButtonTrigger` - Shows a UI button while an object is inside the collider. The button calls the `ITriggerable`.
- `SceneChangeTrigger` - Loads a scene when an object enters the collider.
- `HeightThresholdTrigger` - Invokes an event when the object crosses a given height.
- `TriggerableEvent`, `TriggerableUnityObject` - Ready `ITriggerable` types: a `UnityEvent` or a Unity object that implements `ITriggerable`.

#### UI

- `SafeArea` - Fits a `RectTransform` into the screen safe area.
- `GifImage` - Plays an array of sprites like a GIF.
- `ImageAspectRatioFitter` - Sets the `AspectRatioFitter` ratio from the image sprite.
- `DisableButtonAfterClick` - Makes a button non-interactable after the first click.
- `ScrollRectViewportFix` - Stretches the `ScrollRect` viewport to fill its parent on start.
- `FpsCounter` - Shows FPS in a `Text`. Obsolete: use the Graphy package or the Stats panel instead.

#### Debug

- `DebugSelector`, `DebugArraySelector` - Switch between objects with the Page Down key. Helps to find slow objects or to compare variants.

### Editor

Most menu items are in **Tools → Evolunity**.

- `MenuItems` - Create group (Ctrl+G), toggle Inspector lock (Ctrl+Alt+E), take screenshot (Alt+S), open common folders, add or remove the *DEVELOPMENT* define, clear the AssetBundles cache, reserialize assets.
- `ComponentMenuItems` - **Collapse All** and **Expand All** in the component context menu.
- `PlayModeStartScene` - Sets the scene that Play Mode always starts from.
- `WebGLBuildProfiles` - Switches WebGL build settings between Development, Debug and Release. Menu: **Build → WebGL Profiles**.
- `UnityConstantsGenerator` - Generates `UnityConstants.cs` with tags, sorting layers, layers, scenes and input axes.
- `ConfigsGenerator` - Generates `ConfigCatalog.cs` with a typed field for each `DataAsset`. Needs a `ConfigsGeneratorSettings` asset.
- `DataAssetsIdGenerator` - Regenerates IDs of all or selected `DataAsset`s.
- `SpriteTagsGenerator` - Generates `SpriteTags.cs` with `<sprite name="...">` tags from a TMP Sprite Asset. Needs a `SpriteTagsGeneratorSettings` asset.
- `WebGlTitlePostprocessor` - Replaces the WebGL page title with the product name. Toggle: **Tools → WebGL → Remove Web Player from Title**.
- `WebGlFaviconPostprocessor` - Copies a favicon into the WebGL build. The default path is `Assets/favicon.ico`.
- `CameraScreenshot` - Saves a screenshot from the main camera to the `Screenshots` folder next to `Assets`.
- `AutoExpandHierarchy` - Expands the named objects in the Hierarchy when a scene opens.
- `SceneHierarchy` - Expands or collapses objects in the Hierarchy from code.
- `Define` - Adds or removes scripting define symbols.
- `EditorConsole` - Clears the Console.
- `FileExplorer` - Opens a path in the system file manager.
- `LastActiveSceneCache` - Saves the active scene path to `EditorPrefs` before Play Mode starts.
- `LayerDrawer` - Shows a layer popup for `int` fields with `[Layer]`.
- `RangeDrawer` - Draws `FloatRange` and `IntRange` as min and max fields in one line.
- `PeriodicBehaviourEditor`, `UniversalLootDropDrawer` - Inspectors for `PeriodicBehaviour` and loot tables.

### Collections

- `LootTable<T>` - Loot table asset. Drops: a single item (`ItemDrop`), a weighted pool (`WeightedPoolDrop`) and a nested table (`NestedTableDrop`). `GameObjectLootTable` is a ready example. See the [loot table guide](Scripts/Runtime/Collections/LootTable/README.md) (in Russian).
- `WeightedPool` - Picks random entries by weight. `PickDistinct` picks several different entries.
- `WeightQueue<T>` - Queue where the number of copies of each item depends on its weight.
- `ObservableList<T>` - Serializable list with change events.
- `RingBuffer<T>` - Fixed-size buffer indexed by tick.

### Patterns

- `Singleton<T>` - Singleton for plain C# classes.
- `StateMachine` - State machine where each state is a class. Switch states with `EnterState<TState>()`.
- `ObservableProperty<T>`, `ObservableValue<T>`, `ObservableInt`, `ObservableFloat` - Values with a change event.
- `ObservableReference<T>` - Observable reference that is never null. Made for dependency injection.
- `Optional<T>` - Optional dependency for VContainer.

### Services

- `PauseService` - Pauses and resumes the game through `Time.timeScale`. Fires an event on change.

### Structs

- `Direction` - Direction given by a vector.
- `FloatRange`, `IntRange` - Min and max range with a random value, clamp and contains checks.
- `PoseData` - Serializable `Pose`.
- `PoseDelta` - Difference between two poses. You can add it to a `Pose` or a `Transform`.

### Attributes

- `LayerAttribute` - Shows a layer popup for an `int` field.

### Extension methods

- System types: `T[]`, `byte[]`, `char`, `IComparable`, `IDictionary`, `IEnumerable`, `string`, `StringBuilder`, any type (`With`, `WithValue`).
- Unity types: `Animator`, `CharacterController`, `Color`, `GameObject`, `Graphic`, `LayerMask`, `Material`, `MonoBehaviour`, `Object`, `Pose`, `Quaternion`, `Rect`, `RectTransform`, `Renderer`, `SpriteRenderer`, `Texture2D`, `ToggleGroup`, `Transform`, `UnityWebRequest`, `Vector2`, `Vector3`.
- Rich text: `Bold`, `Italic`, `Size` and `Color` for `string`.
