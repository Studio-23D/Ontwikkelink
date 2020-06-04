using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{

    public abstract class SliderNode <T> : Node
    {
        protected List<SliderElement<T>> sliderElements = new List<SliderElement<T>>();

        protected T chosenValue;

        protected Rect sliderRect;
        protected int current = 0;

        protected Texture2D currentVisual;

        protected Rect buttonLeftRect;
        protected Rect buttonRightRect;

        protected Texture2D buttonLeft;
        protected Texture2D buttonRight;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
            connectionPointStartOffset = 50;
            connectionPointOffset = 10;

            height = 310;
            width = 180;

            base.Init(position, eventHandeler);

            sliderRect = new Rect(NodePosition.x + NodeSize.x / 2 - 70, NodePosition.y + elementY, 140, 140);

            buttonLeftRect = new Rect(0, sliderRect.y + sliderRect.height / 2, 45, 90);
            buttonRightRect = new Rect(rect.width - 45, sliderRect.y + sliderRect.height / 2, 45, 90);

            buttonLeft = Resources.Load<Texture2D>("NodeSystem/Overhaul/pijl_knop_links");
            buttonRight = Resources.Load<Texture2D>("NodeSystem/Overhaul/pijl_knop_rechts");

            chosenValue = sliderElements[0].value;
            currentVisual = sliderElements[0].visual;
		}

        public override void Draw()
        {
            base.Draw();

            if(GUI.Button(buttonLeftRect, buttonLeft, noStyle))
            {
                if (current <= 0)
                {
                    current = sliderElements.Count;
                }
                current--;

                chosenValue = sliderElements[current].value;
                this.CalculateChange();
            }

            if (GUI.Button(buttonRightRect, buttonRight, noStyle))
            {
                if (current >= sliderElements.Count)
                {
                    current = 0;
                }
                current++;

                chosenValue = sliderElements[current].value;
                this.CalculateChange();
            }

            GUI.BeginGroup(sliderRect);

            GUI.DrawTexture(new Rect(0, 0, 140, 140), currentVisual);

            GUI.EndGroup();
        }
    }
}
