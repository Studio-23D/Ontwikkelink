using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class CharacterNode : Node
    {
        private List<Texture2D> icons = new List<Texture2D>();
        private List<Rect> iconPosition = new List<Rect>();

        [InputPropperty]
        public Color skin = Color.white;

        [InputPropperty]
        public AppearanceItem hair;

        [InputPropperty]
        public AppearanceItem torso;

        [InputPropperty]
        public AppearanceItem legs;

        [InputPropperty]
        public AppearanceItem shoes;

        public CharacterAppearance characterAppearance;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            name = "Personage";
            primaireColor = new Color32(241, 242, 228, 255);
            secondaireColor = new Color32(226, 171, 95, 255);

            connectionPointStartOffset = 90;
            connectionPointOffset = 40;

            height = 500;
            width = 180;

            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Huid"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Haar"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Shirt"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/Icon_Broek"));
            icons.Add(Resources.Load<Texture2D>("NodeSystem/Overhaul/PersonageNode/icon_Shoenen"));

            base.Init(position, eventHandeler);

            for (int i = 0; i < icons.Count; i++)
            {
                iconPosition.Add(new Rect(NodePosition.x + NodeSize.x / 2 - 40, NodePosition.y + elementY, 80, 80));
                elementY += 80;
            }
        }

        public override void Draw()
        {
            base.Draw();

            for(int i = 0; i < icons.Count; i++)
            {
                GUI.DrawTexture(iconPosition[i], icons[i], ScaleMode.ScaleToFit, true);
            }

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            if (skin != null)
            {
                characterAppearance.SetSkin(skin);
            }
            if (hair != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Hair, hair));
            }
            if (torso != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Torso, torso));
            }
            if (legs != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Legs, legs));
            }
            if (shoes != null)
            {
                characterAppearance.SetAppearanceItem(new KeyValuePair<AppearanceItemType, AppearanceItem>(AppearanceItemType.Feet, shoes));
            }
            base.CalculateChange();
        }
    }
}