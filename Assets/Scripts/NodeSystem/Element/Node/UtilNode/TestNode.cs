using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
    public class TestNode : Node
    {
        /*[OutputPropperty]
        public GameObject hair = null;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);
            name = "Test Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 65));
            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            hair = Resources.Load<GameObject>("Test");

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleExtraArea);
 
            GUI.Box(nodeAreas[3], "", styleBottomArea);

            GUI.EndGroup();
        }

        public override void CalculateChange()
        {
            base.CalculateChange();
        }*/
    }
}

