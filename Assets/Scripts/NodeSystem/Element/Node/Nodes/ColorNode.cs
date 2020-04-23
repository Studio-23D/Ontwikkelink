using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{

		[InputPropperty]
		public Color colorOut1 = Color.white;

		[OutputPropperty]
		public Color colorOut2 = Color.white;

		public override void CalculateChange()
		{
			throw new NotImplementedException();
		}

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);
            name = "Kleuren Node";

            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 65));
            nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 10));

            Debug.Log("1 " + nodeAreas[0].y);

            for (int i = 0; i < nodeAreas.Count; i++)
            {
                if(i > 0)
                Debug.Log(nodeAreas[i - 1].y);
            }

            rect.size = new Vector2(200, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height);
        }

        public override void Draw()
        {
            base.Draw();

            GUI.Box(nodeAreas[2], "", styleExtraArea);

            if(GUI.Button(new Rect(0, nodeAreas[2].y + 5, nodeAreas[2].width, 20), "set color red"))
            {
                colorOut2 = Color.red;
            }
            if (GUI.Button(new Rect(0, nodeAreas[2].y + 25, nodeAreas[2].width, 20), "set color Green"))
            {
                colorOut2 = Color.green;
            }
            if (GUI.Button(new Rect(0, nodeAreas[2].y + 45, nodeAreas[2].width, 20), "set color Blue"))
            {
                colorOut2 = Color.blue;
            }


            GUI.Box(nodeAreas[3], "", styleBottomArea);

            GUI.EndGroup();
        }
    }
}

