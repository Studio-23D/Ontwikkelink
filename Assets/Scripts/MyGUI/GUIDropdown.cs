using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DropdownElement<T>
{
    public object visual;
    public object obj;
}

namespace MyGUI
{
    public class GUIDropdown<T>
    { 
        private string title;
        private Vector2 position;

        private List<DropdownElement<T>> list;

        public GUIDropdown(Vector2 position, List<DropdownElement<T>> list, string title)
        {
            this.position = position;
            this.list = list;
            this.title = title;
        }

        public T Draw()
        {
            T result = default(T);

            GUI.Box(new Rect(position, new Vector2(100, 100)), title);

            for (int i = 0; i < list.Count; i++)
            {
                object visual = list[i].visual;

                if(list[i].visual is string)
                {
                    GUI.Button(new Rect(new Vector2(0, 0), new Vector2(50, 20)), visual as string);
                }
                else if (list[i].visual is Texture2D)
                {
                    GUI.Button(new Rect(new Vector2(0, 0), new Vector2(50, 20)), visual as Texture2D);
                }
                else
                {
                    Debug.LogError("Cannot Get a visual from this");
                }
            }

            return result;
        }
    }
}