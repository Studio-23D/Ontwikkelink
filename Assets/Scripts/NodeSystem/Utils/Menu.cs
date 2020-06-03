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

        private bool show = false;

        GUIStyle mainStyle;
        GUIStyle buttonStyle;

        Texture2D mainBackground;
        Texture2D buttonBackground;

        public Menu()
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

        public void Init(Vector2 position)
        {
            base.Init(position, this.eventHandeler);

            if (!show) show = true;
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

        public void ToggleShow()
        {
            show = false;
        }

        public override void Draw()
        {
            if (!show) return;

            GUI.BeginGroup(new Rect(Position.x, Position.y, 150, 300));
            GUI.Box(new Rect(0, 0, 150, menuEntries.Count * 25), "", mainStyle);

            for (int i = 0; i < menuEntries.Count; i++)
            {
                if (GUI.Button(new Rect(0, 0 + 25 * i, 150, 25), menuEntries[i].name, buttonStyle))
                {
                    menuEntries[i].OnClick();
                    ToggleShow();
                }
            }
            GUI.EndGroup();
        }
    }
}
