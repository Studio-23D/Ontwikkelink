using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeSystem
{
    public struct DropdownElement
    {
        public Texture2D visual;
        public object value;
    }

    public abstract class DropdownNode : Node
    {
        protected string dropdownName;

        protected List<DropdownElement> dropdownElements = new List<DropdownElement>();

        protected object chosenValue;

        protected Rect dropdrownRect;

        private bool toggleDropdown = false;

        public override void Init(Vector2 position, SystemEventHandeler eventHandeler)
        {
            base.Init(position, eventHandeler);

            chosenValue = dropdownElements[0].value;

            dropdrownRect = new Rect(nodeAreas[2].width, 100, 200, 200);

            nodeAreas.Add(new Rect(0, CalculateAreaY(2), 200, 230));

        }

        public override void Draw()
        {
            base.Draw();
            GUI.Box(nodeAreas[2], "", styleExtraArea);

            if (GUI.Button(new Rect(0, 100, nodeAreas[2].width, 20), dropdownName))
            {
                ToggleDropdown();
            }

            if (!toggleDropdown)
             return;

            GUI.Box(dropdrownRect, "");
            for(int i = 0; i < dropdownElements.Count; i++)
            {
                if(GUI.Button(new Rect(nodeAreas[2].width + (i % 2) * (nodeAreas[2].width / 2), dropdrownRect.position.y + Mathf.FloorToInt(i / 2) * (dropdrownRect.height / 2), dropdrownRect.width / 2, dropdrownRect.height / 2), dropdownElements[i].visual))
                {
                    ToggleDropdown();
                    chosenValue = dropdownElements[i].value;
                }
            }
        }

        private void ToggleDropdown()
        {
            toggleDropdown = !toggleDropdown;
        }
    }
}
