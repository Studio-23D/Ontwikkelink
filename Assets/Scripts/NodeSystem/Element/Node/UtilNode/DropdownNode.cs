using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{

    public abstract class DropdownNode <T> : Node
    {
        protected string dropdownName;

        protected Vector2 ElementSize = new Vector2(100, 100);
        protected int rowLimit = 2;

        protected List<DropdownElement<T>> dropdownElements = new List<DropdownElement<T>>();

        protected T chosenValue;

        protected Rect dropdrownRect;

        protected Vector2 dropdownSize;
        protected Vector2 originalSize;

        private bool toggleDropdown = false;

		public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
		{
			base.Init(position, eventHandeler);
			nodeAreas.Add(new Rect(0, nodeAreas[nodeAreas.Count - 1].y + nodeAreas[nodeAreas.Count - 1].height, 200, 230));

			chosenValue = dropdownElements[0].value;
		}

        public override void Draw()
        {
            base.Draw();
            GUI.Box(nodeAreas[2], "", styleExtraArea);
            

            if (GUI.Button(new Rect(0, nodeAreas[2].y, 200, 20), dropdownName))
            {
                ToggleDropdown();
            }
            rect.size = originalSize;

            if (!toggleDropdown)
             return;

            rect.size = dropdownSize;

            float dropdownWidthCap = dropdrownRect.position.x + rowLimit * ElementSize.x;

            GUI.Box(dropdrownRect, "");
            int xIndex = 0;
            int yIndex = 0;
            for (int i = 0; i < dropdownElements.Count; i++)
            {

                Vector2 position = new Vector2();
                Vector2 size = ElementSize;

                if (dropdrownRect.position.x + xIndex * size.x >= dropdownWidthCap)
                {
                    xIndex = 0;
                    yIndex++;
                }

                position.x = dropdrownRect.position.x + xIndex * size.x;
                position.y = dropdrownRect.position.y + yIndex * size.y;

                if (GUI.Button(new Rect(position, size), dropdownElements[i].visual))
                {
                    ToggleDropdown();
                    chosenValue = dropdownElements[i].value;
                    this.CalculateChange();
                }

                xIndex++;
            }
        }

        private void ToggleDropdown()
        {
            toggleDropdown = !toggleDropdown;
            Vector2 dropdownElementSize = new Vector2
            {
                x = ElementSize.x * rowLimit,
                y = ElementSize.y * (dropdownElements.Count / rowLimit)
            };

            dropdrownRect = new Rect(Size.x, Size.y / 4, dropdownElementSize.x, dropdownElementSize.y);
            this.dropdownSize = Size + dropdownElementSize * rowLimit;
        }
    }
}
