using System;
using System.Collections.Generic;
using UnityEngine;


namespace NodeSystem
{
    struct MenuEntry
    {
        public string name;
        public Action OnClick;
    }

    public class Menu : Element
    {
        private List<MenuEntry> menuEntries;

        private Vector2 mousePosition;

        GUIStyle mainStyle;
        GUIStyle buttonStyle;

        Texture2D mainBackground;
        Texture2D buttonBackground;

        public Menu(Vector2 mousePosition)
        {
            this.mousePosition = mousePosition;

            this.Init();
        }

        private void Init()
        {
            menuEntries = new List<MenuEntry>();
            mainBackground = Resources.Load("MenuBackground") as Texture2D;
            buttonBackground = Resources.Load("ButtonBackground") as Texture2D;

            mainStyle = new GUIStyle();
            mainStyle.normal.background = mainBackground;

            buttonStyle = new GUIStyle();
            buttonStyle.normal.textColor = Color.white;
            buttonStyle.padding = new RectOffset(20, 0, 10, 10);
            buttonStyle.hover.background = buttonBackground;
            buttonStyle.alignment = TextAnchor.MiddleLeft;
        }

        public void CreateMenuEntry(String node, Action OnClick)
        {
            MenuEntry menuEntry = new MenuEntry
            {
                name = node,
                OnClick = OnClick
            };
            menuEntries.Add(menuEntry);
        }

        public override void Draw()
        {
            GUI.BeginGroup(new Rect(mousePosition.x, mousePosition.y, 150, 300));
            GUI.Box(new Rect(0, 0, 150, menuEntries.Count * 25), "", mainStyle);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                if (GUI.Button(new Rect(0, 0 + 25 * i, 150, 25), menuEntries[i].name, buttonStyle))
                {
                    menuEntries[i].OnClick();
                }
            }
            GUI.EndGroup();
        }
    }
}
