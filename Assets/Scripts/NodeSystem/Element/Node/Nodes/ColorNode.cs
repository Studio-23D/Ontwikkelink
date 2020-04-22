using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{
		[OutputPropperty]
		public Color colorOut1 = Color.white;

        public override void CalculateChange()
		{
			throw new NotImplementedException();
		}

		public override void Init(Vector2 position)
		{
			base.Init(position);
            name = "Color Node";

            nodeMiddleSecondRect = new Rect(0, nodeTopRect.height + nodeMiddleRect.height, 200, 65);
            nodeBottomRect = new Rect(0, nodeTopRect.height + nodeMiddleRect.height + nodeMiddleSecondRect.height, 200, 10);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeMiddleSecondRect, "", styleMiddleSecond);

            if(GUI.Button(new Rect(0, nodeMiddleSecondRect.y + 5, nodeMiddleSecondRect.width, 20), "set color red"))
            {
                colorOut1 = Color.red;
            }
            if (GUI.Button(new Rect(0, nodeMiddleSecondRect.y + 25, nodeMiddleSecondRect.width, 20), "set color Green"))
            {
                colorOut1 = Color.green;
            }
            if (GUI.Button(new Rect(0, nodeMiddleSecondRect.y + 45, nodeMiddleSecondRect.width, 20), "set color Blue"))
            {
                colorOut1 = Color.blue;
            }

            GUI.Box(nodeBottomRect, "", styleBottom);

            GUI.EndGroup();
        }
    }
}

