using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class PersonageNode : Node
    {
        [InputPropperty]
        public GameObject hair;

        [InputPropperty]
        public GameObject torso;

        [InputPropperty]
        public GameObject legs;

        [InputPropperty]
        public GameObject feet;

        public Dictionary<string, GameObject> appearenceItems = new Dictionary<string, GameObject>();

        public CharacterAppearence characterAppearence;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Personage Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);

            appearenceItems.Add("hair", hair);
            appearenceItems.Add("torso", torso); 
            appearenceItems.Add("legs", legs);
            appearenceItems.Add("feet", feet);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleBottomArea);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            foreach(KeyValuePair<string, GameObject> appearenceItem in appearenceItems)
            {
                if(appearenceItem.Value != null)
                {
                    characterAppearence.SetAppearenceItem(appearenceItem);
                }
            }
        }
    }
}