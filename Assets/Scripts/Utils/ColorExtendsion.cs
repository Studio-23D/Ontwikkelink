using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static Color CombineColors(this Color color, params Color [] otherColors)
    {
        List<Color> allColors = new List<Color>();
        allColors.AddRange(otherColors);
        allColors.Add(color);
        Color result = new Color(0, 0, 0, 0);
        foreach (Color c in allColors)
        {
            result += c;
        }
        result /= otherColors.Length;
        return new Color(result.r, result.g, result.b);
    }
}
