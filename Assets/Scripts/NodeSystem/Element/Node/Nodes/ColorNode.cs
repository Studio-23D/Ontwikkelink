using System;
using System.Reflection;
using UnityEngine;

namespace NodeSystem
{
	public class ColorNode : Node
	{
		[OutputPropperty]
		public Color colorOut = Color.white;

        private Rect previewRect;
        private Rect palleteRect;
        private Rect draggableRect;
        private Rect draggableContainRect;

        private Color previewColor;
        private Color selectionColor = Color.red;

        private GUIStyle noBoxStyle = new GUIStyle();

        private Texture2D previewTexture;
        private Texture2D selectionTexture = Resources.Load<Texture2D>("NodeSystem/ColorNode/ColorSpectrum");
        private Texture2D draggableTexture;

        private Vector2 draggableCenter;

        private Vector2 multi; 

        private bool isSelectingColor = false;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);
            name = "Kleuren Node";

            previewTexture = new Texture2D(1, 1);
            previewTexture.SetPixel(0, 0, previewColor);
            previewTexture.Apply();

            multi = new Vector2(selectionTexture.width / palleteRect.width, selectionTexture.height / palleteRect.height);

            Debug.Log(selectionTexture);
        }

        public override void Draw()
        {
            base.Draw();


            GUI.DrawTextureWithTexCoords(previewRect, previewTexture, new Rect(0, 0, 1, -1));

            GUI.DrawTextureWithTexCoords(palleteRect, selectionTexture, new Rect(0, 0, 1, -1));

            draggableTexture = new Texture2D(1, 1);
            draggableTexture.SetPixel(0, 0, Color.black);
            draggableTexture.Apply();

            if (GUI.Button(draggableRect, draggableTexture))
            {
                isSelectingColor = !isSelectingColor;
                CalculateChange();
            }

            SelectColor();
            SetPreview();

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

                //selectionColor = selectionTexture.GetPixel((int)(draggableCenter.x * multi.x), (int)(draggableCenter.y * multi.y - nodeAreas[nodeAreas.Count - 2].y - 16));
            }
        }

        private void SetPreview()
        {
            previewColor = selectionColor;
            previewTexture.SetPixel(0, 0, previewColor);
            previewTexture.Apply();
        }

        public override void CalculateChange()
        {
            colorOut = selectionColor;
            base.CalculateChange();
        }
    }
}

