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
