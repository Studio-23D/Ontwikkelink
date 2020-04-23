using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class MasterNode : Node
    {
        [InputPropperty]
        public Color hair;

        [InputPropperty]
        public GameObject torso;

        [InputPropperty]
        public GameObject leggs;

        [InputPropperty]
        public GameObject feet;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Charakter Node";

            nodeAreas.Add(new Rect(0, CalculateAreaY(2), 200, 10));

            rect.size = new Vector2(200, CalculateAreaY(nodeAreas.Count));
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleBottomArea);

            Debug.Log(hair);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            
        }
    }
}