using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{
		[OutputPropperty]
		public Color colorOut = Color.white;

		public override void CalculateChange()
		{

            base.CalculateChange();
		}

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);
            name = "Kleuren Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 65));
            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleExtraArea);

            if(GUI.Button(new Rect(0, nodeAreas[2].y + 5, nodeAreas[2].width, 20), "set color red"))
            {
                colorOut = Color.red;
                this.CalculateChange();
            }
            if (GUI.Button(new Rect(0, nodeAreas[2].y + 25, nodeAreas[2].width, 20), "set color Green"))
            {
                colorOut = Color.green;
                this.CalculateChange();
            }
            if (GUI.Button(new Rect(0, nodeAreas[2].y + 45, nodeAreas[2].width, 20), "set color Blue"))
            {
                colorOut = Color.blue;
                this.CalculateChange();
            }

            GUI.Box(nodeAreas[3], "", styleBottomArea);

            GUI.EndGroup();
        }
    }
}

