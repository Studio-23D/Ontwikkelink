using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{
		[OutputPropperty]
		public Color color = Color.white;

        private Rect palleteRect;
        private Rect draggableRect;
        private Rect draggableContainRect;

        private Color previewColor;
        private Color selectionColor = Color.red;

        private Texture2D previewTexture;
        private Texture2D selectionTexture = Resources.Load<Texture2D>("NodeSystem/ColorNode/ColorSpectrum");
        private Texture2D draggableTexture = Resources.Load<Texture2D>("NodeSystem/ColorNode/ColorPicker");

        private Vector2 draggableCenter;

        private Vector2 resolutionMultiplier; 

        private bool isSelectingColor = false;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            name = "Kleur";
            primaireColor = new Color32(241, 242, 228, 255);
            secondaireColor = new Color32(153, 206, 180, 255);

            nodeImage = Resources.Load<Texture2D>("NodeSystem/Overhaul/Icon_Kleuren");

            connectionPointStartOffset = 90;
            connectionPointOffset = 40;

            height = 310;
            width = 180;

            base.Init(position, eventHandeler);

            palleteRect = new Rect(NodePosition.x + NodeSize.x / 2 - 70, NodePosition.y + elementY, 140, 140);
            draggableRect = new Rect(NodePosition.x + NodeSize.x / 2 - 70, NodePosition.y + elementY, 32, 32);
            resolutionMultiplier = new Vector2(selectionTexture.width / palleteRect.width, selectionTexture.height / palleteRect.height);
        }

        public override void Draw()
        {
            base.Draw();
            GUI.color = Color.white;

            GUI.DrawTexture(palleteRect, selectionTexture);

            GUI.color = selectionColor;

            if (GUI.Button(draggableRect, draggableTexture, noStyle))
            {
                isSelectingColor = !isSelectingColor;
                CalculateChange();
            }

            SelectColor();
            GUI.color = Color.white;

            GUI.EndGroup();
        }

        private void SelectColor()
        {
            if (!palleteRect.Contains(eventHandeler.MousePosition))
                return;

            if (isSelectingColor)
            {
                draggableCenter.x = eventHandeler.MousePosition.x - (draggableRect.width / 2);
                draggableCenter.y = eventHandeler.MousePosition.y - (draggableRect.height / 2);

                draggableRect.x = draggableCenter.x;
                draggableRect.y = draggableCenter.y;

                float x = ((draggableRect.x - (NodePosition.x + NodeSize.x / 2 - 70)) + (draggableRect.width / 2));
                float y = ((draggableRect.y - (NodePosition.y + elementY)) + (draggableRect.width / 2));

                selectionColor = selectionTexture.GetPixel((int)(x * resolutionMultiplier.x), (int)(y * resolutionMultiplier.y));
            }
        }

        public override void CalculateChange()
        {
            color = selectionColor;
            base.CalculateChange();
        }
    }
}

