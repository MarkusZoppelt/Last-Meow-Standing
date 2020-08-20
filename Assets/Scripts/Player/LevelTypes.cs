using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{
    White = 1,
    Red = 2,
    Green = 4,
    Blue = 8,
    Yellow = Red | Green,
    Magenta = Blue | Red,
    Cyan = Blue | Green,
    Pink = Red | White,
    Neon = Green | White,
    Azur = Blue | White,
    Rainbow = Blue | Green | Red | White,
}

public static class Levels
{
    public static Dictionary<LevelType, Color> Colors = new Dictionary<LevelType, Color>()
    {
        { LevelType.White, Color.white },
        { LevelType.Red, Color.red },
        { LevelType.Green, Color.green },
        { LevelType.Blue, Color.blue },
        { LevelType.Yellow, Color.yellow },
        { LevelType.Magenta, Color.magenta },
        { LevelType.Cyan, Color.cyan },
        { LevelType.Pink, new Color(1, .713f, 0.756f) },
        { LevelType.Neon, new Color(.224f, 1, 0.078f) },
        { LevelType.Azur, new Color(.847f, 1, 1f) },
        { LevelType.Rainbow, Color.black },
    };
}
