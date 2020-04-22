using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class MasterNode : Node
    {
        [InputPropperty]
        public GameObject hair;

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

            nodeMiddleSecondRect = new Rect(0, nodeTopRect.height + nodeMiddleRect.height, 200, 0);
            nodeBottomRect = new Rect(0, nodeTopRect.height + nodeMiddleRect.height + nodeMiddleSecondRect.height, 200, 10);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeMiddleSecondRect, "", styleMiddleSecond);
            GUI.Box(nodeBottomRect, "", styleBottom);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            
        }
    }
}