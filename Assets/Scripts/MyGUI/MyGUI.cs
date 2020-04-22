using System;
using System.Collections.Generic;
using UnityEngine;
/*
public static class MyGUI
{
    public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
    {
        pointA.x = (int)pointA.x; pointA.y = (int)pointA.y;
        pointB.x = (int)pointB.x; pointB.y = (int)pointB.y;

        Texture2D lineTex = null;

        if (!lineTex) { lineTex = new Texture2D(1, 1); }
        Color savedColor = GUI.color;
        GUI.color = color;

        Matrix4x4 matrixBackup = GUI.matrix;

        float angle = Mathf.Atan2(pointB.y - pointA.y, pointB.x - pointA.x) * 180f / Mathf.PI;
        float length = (pointA - pointB).magnitude;
        GUIUtility.RotateAroundPivot(angle, pointA);
        GUI.DrawTexture(new Rect(pointA.x, pointA.y, length, width), lineTex);

        GUI.matrix = matrixBackup;
        GUI.color = savedColor;
    }
    
    public static T DrawDropdown<T>(Vector2 position, List<T> list, string title)
    {
        T result = default(T);

        GUI.Box(new Rect(position, new Vector2(100, 100)), title);

        for(int i = 0; i < list.Count; i++)
        {
            GUI.Button(new Rect(new Vector2(0, 0), new Vector2(50, 20)), list[i] as Texture2D);
        }

        return result;
    }
}
*/