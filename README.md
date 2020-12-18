# Evolunity

Well-designed package with useful scripting tools for Unity development​.

## Content

### Extension methods
- System types:
  - char
  - string
  - Dictionary
  - IEnumerable
  - IComparable
- Unity types
  - Animator
  - Color
  - Graphic
  - LayerMask
  - Quaternion
  - Rect
  - Renderer
  - Transform
  - Vector
### Utilities
- Binary serializer
- Delay
- Performance meter
- Static coroutine
- String encryptor
- Regex patterns
### Unity components (some with cool inspectors)
- PeriodicBehaviour
- InputReader (drag, zoom, and click)
- LongTapReader
- Spawner
- Comment
- DevelopmentOnly
- DontDestroyOnLoad
- PlatformDependent
- Singleton
### Editor tools
- Constants generator (tags, layers, scenes, etc.)
- Useful menu items
- Camera screenshot
- Defines management

## Warning

Evolunity may receive breaking changes, so be sure to make a backup before updating the package.

## Install
Open `{ProjectFolder}/Packages/manifest.json` and add following line:
```
{
  "dependencies":
  {
    "com.evolutex.evolunity": "https://github.com/Bodix/Evolunity.git",
    ...
  }
}
```

## Requirements
- Unity 2018.4+
- Git (added to **PATH** environment variable)


## License

[**CC BY-ND 4.0**](https://creativecommons.org/licenses/by-nd/4.0/)

1. **You can use this package in commercial projects.**

2. You can modify or extend this package only for your own use but you can't distribute modified version.
    - **Note:** You can submit a pull request to this repository and if your change is useful, I'll be sure to add it!

3. You must indicate the author.
    - **Note:** You don't need to take any further action on this point, because all credits is written at the head of the scripts!